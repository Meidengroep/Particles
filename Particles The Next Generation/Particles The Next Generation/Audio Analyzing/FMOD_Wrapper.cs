using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using FMOD;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace Particles_The_Next_Generation
{
    public class FMOD_Player
    {
        protected readonly object spectrumLock = new object();

        private Spectrum m_Spectrum;
        private IBeatDetector m_BeatDetector;
        private BPM m_BPM;
        private float m_Timestep;
        private int m_SpectrumSize;
        private Stopwatch m_StopWatch;
        private Thread m_Thread;

        public FMOD_Player(int spectrumSize)
        {
            m_Spectrum = new Spectrum(spectrumSize);

            int nrofbuffers = 256;
            m_BeatDetector = new FrequencyBandDetector(nrofbuffers, 0, 0);

            #region Fmod
            // load FMOD
            Verify(FMOD.Factory.System_Create(ref fmodSystem));
            // check it's right version
            uint version = 0;
            Verify(fmodSystem.getVersion(ref version));
            if (version < FMOD.VERSION.number)
            {
                throw new ApplicationException("Invalid FMOD version");
            }
            Verify(fmodSystem.init(numSoundChannels, FMOD.INITFLAGS.NORMAL, (IntPtr)null));
            sounds = new Dictionary<string, FMOD.Sound>();

            FFT_WINDOW_TYPE = (int)FMOD.DSP_FFT_WINDOW.BLACKMAN;
            #endregion

            this.m_SpectrumSize = spectrumSize;
            Global.Frequencies = new float[nrofbuffers];

            m_Timestep = 1000f / 42f;

            float histSize = 2000f / m_Timestep;

            m_BPM = new BPM((int)histSize, m_Timestep);

            m_StopWatch = new Stopwatch();
            ThreadStart start = new ThreadStart(BusyWait);
            m_Thread = new Thread(start);
            m_Thread.Start();
        }

        private void BusyWait()
        {
            m_StopWatch.Start();

            while (true)
            {
                float currentTime = m_StopWatch.ElapsedMilliseconds;

                while (currentTime < m_Timestep)
                {
                    float elapsed = m_StopWatch.ElapsedMilliseconds;
                    if (m_Timestep - elapsed >= 0)
                        Thread.Sleep((int)(m_Timestep - elapsed));
                    currentTime = m_StopWatch.ElapsedMilliseconds;
                }

                DoBeatDetection(null);

                m_StopWatch.Restart();
                currentTime = 0;
            }
        }

        private void DoBeatDetection(object o)
        {
           MediaManager.variable++;

            if (IsMusicPlaying())
            {
                lock (spectrumLock)
                {
                    UpdateSpectrum();
                }

                m_BeatDetector.DoDetection(m_Spectrum);
                m_BPM.Update(m_BeatDetector.BeatLastIteration());

                UpdateFMOD();
            }
        }

        public void Draw(SpriteBatch sb)
        {
            m_BPM.Draw(sb);
        }

        private void UpdateSpectrum()
        {
            fmodSystem.getSpectrum(m_Spectrum.LeftSpectrum, m_Spectrum.SpectrumSize, 0, (DSP_FFT_WINDOW)FFT_WINDOW_TYPE);
            fmodSystem.getSpectrum(m_Spectrum.RightSpectrum, m_Spectrum.SpectrumSize, 0, (DSP_FFT_WINDOW)FFT_WINDOW_TYPE);
        }

        #region Shutdown and Update

        private void UpdateFMOD()
        {
            try
            {
                fmodSystem.update();
            }
            catch (System.Exception)
            {
            }
        }

        public void Shutdown()
        {
            lock (spectrumLock)
            {
                foreach (FMOD.Sound sound in sounds.Values)
                {
                    Verify(sound.release());
                }
                sounds.Clear();
                StopMusic();
                if (null != fmodSystem)
                {
                    fmodSystem.release();
                    fmodSystem = null;
                }
            }
            m_Thread.Abort();
        }

        #endregion

        #region Play, Stop, IsPlaying , Volume control Methods

        /// <summary>
        /// Set a piece of music to be played
        /// </summary>
        /// <param name="filename">Name of the file holding the music</param>
        public void PlayMusic(string filename)
        {
            // if we're already playing music, stop it
            StopMusic();
            // play the new music (if the file exists)
            String path = filename;
            if (VerifyFileExists(path))
            {
                lock (spectrumLock)
                {
                    FMOD.MODE mode = FMOD.MODE.SOFTWARE | FMOD.MODE.LOOP_OFF | FMOD.MODE.ACCURATETIME;
                    Verify(fmodSystem.createStream(path, mode, ref music));
                    Verify(fmodSystem.playSound(FMOD.CHANNELINDEX.FREE, music, true, ref musicChannel));
                    Verify(musicChannel.setVolume(musicVolume));
                    Verify(musicChannel.setPaused(false));
                    float songFrequency = 44100;
                    musicChannel.getFrequency(ref songFrequency);
                }
            }

            // Reset the whole thing.
            float freq = 0;
            musicChannel.getFrequency(ref freq);

            int bufferSize = (int)(freq / m_SpectrumSize);
            m_BeatDetector.ResetBuffer(bufferSize);

            m_Timestep = 1000f / bufferSize;

            //ChangeTimer(1000 / bufferSize);
        }


        /// <summary>
        /// stop playing music
        /// </summary>
        /// <remarks>This function MUST NOT THROW, because it's called by Dispose</remarks>
        public void StopMusic()
        {
            if (null != musicChannel)
            {
                lock(spectrumLock)
                {
                    try
                    {
                        musicChannel.stop();
                    }
                    catch (System.Exception)
                    {}
                    musicChannel = null;
                }
                if (null != music)
                {
                    music.release();
                    music = null;
                }
            }
        }

        public void Pause()
        {
            if (musicChannel != null)
                musicChannel.setPaused(true);
        }

        public void Resume()
        {
            if(musicChannel != null)
                musicChannel.setPaused(false);
        }

        /// <summary>
        /// Is background music currently playing?
        /// </summary>
        /// <returns>true if it's playing</returns>
        public bool IsMusicPlaying()
        {
            bool isPlaying = false;
            if (null != musicChannel)
            {
                if (FMOD.RESULT.OK != musicChannel.isPlaying(ref isPlaying))
                {
                    isPlaying = false;
                }
            }
            return isPlaying;
        }

        /// <summary>
        /// The volume sound will be played at (0 = off, 1 = max)
        /// </summary>
        public float MusicVolume
        {
            get { return musicVolume; }
            set
            {
                if ((value >= 0.0f) && (value <= 1.0f))
                    musicVolume = value;
                if (null != musicChannel)
                {
                    musicChannel.setVolume(musicVolume);
                }
            }
        }

        #endregion

        #region Verification Methods

        /// <summary>
        /// Check the result from an FMOD function call, and throw if it's in error
        /// </summary>
        /// <param name="result">return value from an FMOD function to test</param>
        private static void Verify(FMOD.RESULT result)
        {
            if (FMOD.RESULT.OK != result)
            {
                throw new ApplicationException("FMOD error:" + FMOD.Error.String(result));
            }
        }

        /// <summary>
        /// Throw error if file doesn't exist (and we've got checking turned on
        /// </summary>
        /// <param name="pathname">full pathname of file to check</param>
        private bool VerifyFileExists(string pathname)
        {
            if (File.Exists(pathname))
            {
                return true;
            }
            return false;
        }

        #endregion

        #region Fields

        private volatile int FFT_WINDOW_TYPE;

        /// <summary>
        /// The wrapped FMOD engine
        /// </summary>
        private FMOD.System fmodSystem;

        /// <summary>
        /// Internal cache of sounds (gunshot, etc.)
        /// </summary>
        private Dictionary<string, FMOD.Sound> sounds;

        /// <summary>
        /// The actual music
        /// </summary>
        private FMOD.Sound music;

        /// <summary>
        /// Channel used to play background music
        /// </summary>
        private FMOD.Channel musicChannel;

        /// <summary>
        /// Number of sound channels we want FMOD to have
        /// </summary>
        const int numSoundChannels = 8;

        /// <summary>
        /// The volume sound will be played at (0 = off, 1 = max)
        /// </summary>
        public float soundVolume = 1.0f;

        /// <summary>
        /// The volume sound will be played at (0 = off, 1 = max)
        /// </summary>
        public float musicVolume = 1.0f;

        #endregion Fields
    }
}