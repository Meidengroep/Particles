using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using Audio_Analyzing_CsGL_Tool.Source.Rendering;
using Audio_Analyzing_CsGL_Tool.Source.Rendering.Scenes;
using ED_Project.Source.Audio_Analyzing;
using Tao.Platform.Windows;

namespace Audio_Analyzing_CsGL_Tool
{
    public partial class MainForm : Form
    {
        #region Main Function (don't mess around with this)

        [STAThread]
        static void Main()
        {
            HiResTimer time = new HiResTimer();
            float PrevTime = (float)time.Value / (float)time.Frequency;

            MainForm form = new MainForm(); // Create the form
            Application.EnableVisualStyles();
            form.Show();    // When everything is initialized, show the form

            while (form.Created) // This is our message loop
            {
                float CurrTime = (float)time.Value / (float)time.Frequency;
                float dt = (CurrTime - PrevTime);

                form.Update(dt);

                Application.DoEvents(); // Process the events, like keyboard and mouse input d
                PrevTime = CurrTime;
            }
        }
        #endregion

        #region Fields
        // It's anoying that I need this component manager. It's necessary to change the play button's image
        // to pause when a music is playing. Perhaps there's a better way?
        private readonly ComponentResourceManager resourcesInTheProgram = null;

        private readonly FMOD_Wrapper fmod;
        private OpenGLRenderable scene = null;
        private SpectrumBuffer specBuffer;

        public static uint framesDrawn = 0;
        private uint framesPerSecond = 0;
        float secondTimer = 0;

        readonly List<string> musicFilePaths = new List<string>();
        private String prevLoadMusicDir = String.Empty;
        private int playingMusicIndex = 0;

        private bool beginPlaying = false;
        private bool shuffleMusic = false;
        private bool loopMusic = true;

        #region Mouse and Keyboard
        /// <summary>
        /// Current keyboard state.  The key's integer value, is the index for this array.  If that 
        /// index is true, the key is being pressed, if it's false, the key is not being pressed.  You 
        /// should likely mark the key as handled, by setting it's index to false when you've processed it.
        /// </summary>
        public static bool[] KeyState = new bool[256];									// Keyboard State

        /// <summary>
        /// Current mouse state.
        /// </summary>
        public struct Mouse
        {
            /// <summary>
            /// X-axis position in the view.
            /// </summary>
            public static int X;

            /// <summary>
            /// Y-axis position in the view.
            /// </summary>
            public static int Y;

            /// <summary>
            /// Previous X-axis position in the view.
            /// </summary>
            public static int LastX;

            /// <summary>
            /// Previous Y-axis position in the view.
            /// </summary>
            public static int LastY;

            /// <summary>
            /// Difference between the current and previous X-axis position in the view.
            /// </summary>
            public static int DifferenceX;

            /// <summary>
            /// Difference between the current and previous Y-axis position in the view.
            /// </summary>
            public static int DifferenceY;

            /// <summary>
            /// Is left mouse button pressed?
            /// </summary>
            public static bool LeftButton;

            /// <summary>
            /// Is middle mouse button pressed?
            /// </summary>
            public static bool MiddleButton;

            /// <summary>
            /// Is right mouse button pressed?
            /// </summary>
            public static bool RightButton;

            /// <summary>
            /// Is X button 1 (Intellimouse) pressed?  Windows 2000 and above only.
            /// </summary>
            public static bool XButton1;

            /// <summary>
            /// Is X button 2 (Intellimouse) pressed?  Windows 2000 and above only.
            /// </summary>
            public static bool XButton2;
        }
        #endregion Mouse and Keyboard

        #endregion Fields

        #region Constructor

        public MainForm()
        {
            InitializeComponent();
            simpleOpenGlControl.InitializeContexts();

            fmod = new FMOD_Wrapper();
            fmod.Initialize();
            specBuffer = SpectrumBuffer.Instance;
            resourcesInTheProgram = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));

            // TODO: Remove this from final build
            String folder = @"C:\Documents and Settings\Samuel\My Documents\My Music\Rez Gamer's Guide to.._\";
            DirectoryInfo dirInfo = new DirectoryInfo(folder);
            int rootDirLength = folder.Length;
            if (dirInfo.Exists)
            {
                FileInfo[] files = dirInfo.GetFiles();
                foreach (FileInfo file in files)
                {
                    // note that we trim off the MusicDirectory part of name
                    // it will be added back later in PlayMusic
                    if (file.FullName.Contains(".mp3") || file.FullName.Contains(".wma")
                        || file.FullName.Contains(".flac") || file.FullName.Contains(".ogg"))
                    {
                        lstMusic.Items.Add(file.FullName.Substring(rootDirLength + 1));
                        musicFilePaths.Add(file.FullName);
                    }
                }
            }

            cmbSpectrumSize.SelectedItem = "1024";
            cmbFFTType.SelectedItem = "FFT_WINDOW_BLACKMAN";
            cmbBeatDetection.SelectedItem = "Simple Sound Energy";

            scene = new SpectrumRenderScene(simpleOpenGlControl.Size);
            scene.Initialize();
        }
        #endregion

        #region Update Method

        private void Update(float dt)
        {
            if (scene != null)
            {
                simpleOpenGlControl.Refresh(); // Invalidate the control so it renders.
                scene.Update(dt);
            }

            // Update the Music Track Bar
            if (fmod.IsMusicPlaying())
            {
                int val = (int)(fmod.GetMusicPos());
                if (val > musicTrack.Maximum)
                    musicTrack.Maximum = (int)fmod.GetMusicLength();

                musicTrack.Value = val < musicTrack.Maximum ? val : val - musicTrack.Maximum;
            }
            else if ((loopMusic && beginPlaying))
                btnNext.PerformClick();

            // Display FPS
            secondTimer += dt;
            if (secondTimer >= 1.0f)
            {
                framesPerSecond = (uint)(framesDrawn);
                framesDrawn = 0;
                secondTimer = 0;

                FPSLabel.Text = "FPS: " + framesPerSecond;
                AnalysisPerSecondLabel.Text = "Spectrum Analysis Per Second: " + fmod.SpectrumAnalysisPerSecond;
                fmod.SpectrumAnalysisPerSecond = 0;
            }
        }

        #endregion Update Method

        #region OpenGL_Control_Events

        private void simpleOpenGlControl_Paint(object sender, PaintEventArgs e)
        {
            scene.Render();
            framesDrawn++;
        }

        private void simpleOpenGlControl_KeyDown(object sender, KeyEventArgs e)
        {
            KeyState[(int)e.KeyCode] = true;
        }

        private void simpleOpenGlControl_KeyUp(object sender, KeyEventArgs e)
        {
            KeyState[(int)e.KeyCode] = false;
        }

        private void simpleOpenGlControl_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Mouse.LeftButton = true;
            }
            else if (e.Button == MouseButtons.Middle)
            {
                Mouse.MiddleButton = true;
            }
            else if (e.Button == MouseButtons.Right)
            {
                Mouse.RightButton = true;
            }
        }

        private void simpleOpenGlControl_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Mouse.LeftButton = false;
            }
            else if (e.Button == MouseButtons.Middle)
            {
                Mouse.MiddleButton = false;
            }
            else if (e.Button == MouseButtons.Right)
            {
                Mouse.RightButton = false;
            }
            else if (e.Button == MouseButtons.XButton1)
            {
                Mouse.XButton1 = false;
            }
            else if (e.Button == MouseButtons.XButton2)
            {
                Mouse.XButton2 = false;
            }
        }

        private void simpleOpenGlControl_MouseEnter(object sender, EventArgs e)
        {
            Mouse.DifferenceX = 0;
            Mouse.DifferenceY = 0;
        }


        private void simpleOpenGlControl_MouseMove(object sender, MouseEventArgs e)
        {
            Mouse.LastX = Mouse.X;
            Mouse.LastY = Mouse.Y;
            Mouse.X = e.X;
            Mouse.Y = e.Y;
            Mouse.DifferenceX = Mouse.X - Mouse.LastX;
            Mouse.DifferenceY = Mouse.Y - Mouse.LastY;
        }

        private void simpleOpenGlControl_SizeChanged(object sender, EventArgs e)
        {
            if (scene != null)
            {
                scene.Resize(simpleOpenGlControl.Size.Width, simpleOpenGlControl.Size.Height);
            }
        }

        private void simpleOpenGlControl_Enter(object sender, EventArgs e)
        {
            simpleOpenGlControl.BorderStyle = BorderStyle.Fixed3D;
        }

        private void simpleOpenGlControl_Leave(object sender, EventArgs e)
        {
            simpleOpenGlControl.BorderStyle = BorderStyle.None;
        }

        #endregion OpenGL_Control_Events

        #region Music Controls

        private void numVolume_ValueChanged(object sender, EventArgs e)
        {
            if (fmod != null)
            {
                fmod.MusicVolume = (float)numVolume.Value;
            }
        }

        private void btnDirectory_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            dlg.Description = "Chose a directory with music files.";
            dlg.RootFolder = System.Environment.SpecialFolder.Desktop;
          
            if (DialogResult.OK == dlg.ShowDialog())
            {
                String folder = dlg.SelectedPath;

                DirectoryInfo dirInfo = new DirectoryInfo(folder);
                int rootDirLength = folder.Length;
                if (dirInfo.Exists)
                {
                    FileInfo[] files = dirInfo.GetFiles();
                    foreach (FileInfo file in files)
                    {
                        // note that we trim off the MusicDirectory part of name
                        // it will be added back later in PlayMusic
                        if (file.FullName.Contains(".mp3") || file.FullName.Contains(".wma")
                            || file.FullName.Contains(".flac") || file.FullName.Contains(".ogg"))
                        {
                            lstMusic.Items.Add(file.FullName.Substring(rootDirLength + 1));
                            musicFilePaths.Add(file.FullName);
                        }
                    }
                }
            }
            else
                return;
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.CheckFileExists = true;
            dlg.Multiselect = true;
            dlg.Title = "Select Music Files";
            dlg.Filter = "MP3|*.mp3|WMA|*.wma|FLAC|*.flac|OGG|*.ogg";
            if (prevLoadMusicDir != String.Empty)
                dlg.InitialDirectory = prevLoadMusicDir;
            if (DialogResult.OK == dlg.ShowDialog())
            {
                List<String> songNames = new List<string>(dlg.SafeFileNames);
                List<String> songPaths = new List<string>(dlg.FileNames);

                if (songPaths.Count > 0)
                {
                    String dir = songPaths[0];
                    dir.Replace(songNames[0], String.Empty);
                    prevLoadMusicDir = dir;
                }

                for (int i = 0; i < songNames.Count; ++i)
                {
                    // note that we trim off the MusicDirectory part of name
                    // it will be added back later in PlayMusic
                    lstMusic.Items.Add(songNames[i]);
                    musicFilePaths.Add(songPaths[i]);
                }
            }
        }

        private void btnClearMusicList_Click(object sender, EventArgs e)
        {
            lstMusic.Items.Clear();
            musicFilePaths.Clear();
        }

        private void lstMusic_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (!fmod.IsMusicPlaying())
            {
                btnPlay.PerformClick();
            }
            else
            {
                if (lstMusic.SelectedItem != null)
                {
                    lock (fmod)
                    {
                        playingMusicIndex = lstMusic.SelectedIndex;
                        fmod.PlayMusic(musicFilePaths[playingMusicIndex]);
                    }
                    //btnPlay.Text = "Pause";
                    btnPlay.BackgroundImage = ((System.Drawing.Image)(resourcesInTheProgram.GetObject("panel1.BackgroundImage")));
                }
            }
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {

            if (!fmod.IsMusicPlaying())
            {
                if (lstMusic.SelectedItem != null)
                {
                    lock (fmod)
                    {
                        playingMusicIndex = lstMusic.SelectedIndex;
                        fmod.PlayMusic(musicFilePaths[playingMusicIndex]);
                        musicTrack.Maximum = (int)(fmod.GetMusicLength());
                        musicTrack.Value = 0;
                        musicTrack.LargeChange = musicTrack.Maximum / 10;
                        musicTrack.SmallChange = musicTrack.Maximum / 100;

                        beginPlaying = true;
                    }
                    //btnPlay.Text = "Pause";
                    btnPlay.BackgroundImage = ((System.Drawing.Image)(resourcesInTheProgram.GetObject("panel1.BackgroundImage")));
                }
            }
            else if (fmod.musicPaused == true)
            {
                lock (fmod)
                {
                    fmod.ResumeMusic();
                }
                //btnPlay.Text = "Pause";
                btnPlay.BackgroundImage = ((System.Drawing.Image)(resourcesInTheProgram.GetObject("panel1.BackgroundImage")));
            }
            else // Music is playing, pause it.
            {
                lock (fmod)
                {
                    fmod.PauseMusic();
                }
                //btnPlay.Text = "Resume";
                btnPlay.BackgroundImage = ((System.Drawing.Image)(resourcesInTheProgram.GetObject("btnPlay.BackgroundImage")));
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            //btnPlay.Text = "Play";
            btnPlay.BackgroundImage = ((System.Drawing.Image)(resourcesInTheProgram.GetObject("btnPlay.BackgroundImage")));
            musicTrack.Value = 0;
            lock (fmod)
            {
                if (fmod.IsMusicPlaying())
                {
                    fmod.StopMusic();
                    beginPlaying = false;
                }
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (lstMusic.Items.Count > 0)
            {
                if (shuffleMusic)
                {
                    Random rand = new Random();
                    playingMusicIndex = rand.Next(lstMusic.Items.Count);
                    lstMusic.SelectedIndex = playingMusicIndex;

                    if (fmod.IsMusicPlaying() || (loopMusic && beginPlaying))
                    {
                        fmod.PlayMusic(musicFilePaths[playingMusicIndex]);
                        musicTrack.Maximum = (int)(fmod.GetMusicLength());
                        musicTrack.Value = 0;
                        musicTrack.LargeChange = musicTrack.Maximum / 10;
                        musicTrack.SmallChange = musicTrack.Maximum / 100;
                    }
                }
                else
                {

                    playingMusicIndex++;
                    if (playingMusicIndex > lstMusic.Items.Count - 1)
                        playingMusicIndex = 0;
                    lstMusic.SelectedIndex = playingMusicIndex;

                    if (fmod.IsMusicPlaying() || (loopMusic && beginPlaying))
                    {
                        fmod.PlayMusic(musicFilePaths[playingMusicIndex]);
                        musicTrack.Maximum = (int)(fmod.GetMusicLength());
                        musicTrack.Value = 0;
                        musicTrack.LargeChange = musicTrack.Maximum / 10;
                        musicTrack.SmallChange = musicTrack.Maximum / 100;
                    }
                }
            }
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (lstMusic.Items.Count > 0)
            {
                playingMusicIndex--;
                if (playingMusicIndex < 0)
                    playingMusicIndex = lstMusic.Items.Count - 1;

                lstMusic.SelectedIndex = playingMusicIndex;

                if (fmod.IsMusicPlaying() || (loopMusic && beginPlaying))
                {
                    fmod.PlayMusic(musicFilePaths[playingMusicIndex]);
                    musicTrack.Maximum = (int)(fmod.GetMusicLength());
                    musicTrack.Value = 0;
                    musicTrack.LargeChange = musicTrack.Maximum / 10;
                    musicTrack.SmallChange = musicTrack.Maximum / 100;
                }
            }
        }

        private void musicTrack_Scroll(object sender, EventArgs e)
        {
            if (fmod.IsMusicPlaying())
            {
                fmod.SetMusicPos((uint)musicTrack.Value);
            }
        }

        #endregion

        #region FMOD Controls

        private void cmbSpectrumSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            fmod.Reset(int.Parse(cmbSpectrumSize.Text), (int)cmbFFTType.SelectedIndex);
        }

        private void cmbFFTType_SelectedIndexChanged(object sender, EventArgs e)
        {
            fmod.Reset(int.Parse(cmbSpectrumSize.Text), (int)cmbFFTType.SelectedIndex);
        }

        private void cmbBeatDetection_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch(cmbBeatDetection.Text)
            {
                case "None":                    
                        fmod.runBeatDetection = FMOD_Wrapper.BeatDetectionType.None;
                        break;                    
                case "Simple Sound Energy":                    
                        fmod.runBeatDetection = FMOD_Wrapper.BeatDetectionType.Simple;
                        break;                    
                case "Frequency Selected Sound Energy":                    
                        fmod.runBeatDetection = FMOD_Wrapper.BeatDetectionType.FrequencyBands;
                        chkLoop.Checked = true;
                        break;                    
            }
        }

        private void chkLoop_CheckedChanged(object sender, EventArgs e)
        {
            loopMusic = chkLoop.Checked;
            if (loopMusic == false)
                beginPlaying = false;
        }

        private void btnShuffle_CheckedChanged(object sender, EventArgs e)
        {
            shuffleMusic = btnShuffle.Checked;
        }

        private void chkFFT_CheckedChanged(object sender, EventArgs e)
        {
            fmod.useFFT = chkFFT.Checked;
        }

        #endregion FMOD Controls

        #region Render Controls

        private void radButtonSpectrum_CheckedChanged(object sender, EventArgs e)
        {
            if (!(scene is SpectrumRenderScene))
            {
                scene.Shutdown();
                scene = null;
                scene = new SpectrumRenderScene(simpleOpenGlControl.Size);
                scene.Initialize();
            }
        }

        private void radButtonRotSpheres_CheckedChanged(object sender, EventArgs e)
        {
            if (!(scene is RotatingSpheresScene))
            {
                scene.Shutdown();
                scene = null;
                scene = new RotatingSpheresScene(simpleOpenGlControl.Size);
                scene.Initialize();
            }
        }

        private void radButtonBeatDetection_CheckedChanged(object sender, EventArgs e)
        {
            if (!(scene is BeatDetectionScene))
            {
                scene.Shutdown();
                scene = null;
                scene = new BeatDetectionScene(simpleOpenGlControl.Size);
                scene.Initialize();
            }
        }

        private void radButtonTest_CheckedChanged(object sender, EventArgs e)
        {
            if (!(scene is TestScene))
            {
                scene.Shutdown();
                scene = null;
                scene = new TestScene(simpleOpenGlControl.Size);
                scene.Initialize();
            }
        }

        #endregion Render Controls

        #region FormClosing

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            fmod.Shutdown();
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                simpleOpenGlControl.DestroyContexts();
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        #endregion FormClosing

        #region Toolbar Controls (Exit, About, Options)

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Samuel Batista FullSail ©2009", "About", MessageBoxButtons.OK);
        }

        #endregion Toolbar Controls (Exit, About, Options)
    }
}
