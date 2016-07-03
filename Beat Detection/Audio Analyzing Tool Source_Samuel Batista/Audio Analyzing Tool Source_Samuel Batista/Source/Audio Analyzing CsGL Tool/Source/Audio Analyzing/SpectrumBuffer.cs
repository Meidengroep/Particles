using System;

namespace ED_Project.Source.Audio_Analyzing
{
    public struct BeatData
    {
        public float instantEnergy;
        public float averageEnergy;
        public float variance;
        public float beatSensibility;
        public int historySize;
    }

    public struct SpectrumData
    {
        // specL and specR are the 2 sound channels that FMOD returns, one for left speaker and one for right speaker.
        public float[]  specL, specR;
        public float    averageL, averageR;
        public int      spectrumSize;
        public bool     isBeat;
        public BeatData beatData;
        //public BeatData beatData;

        public SpectrumData(int _spectrumSize)
        {
            spectrumSize = _spectrumSize;
            specL = new float[spectrumSize];
            specR = new float[spectrumSize];
            averageL = 0.0f;
            averageR = 0.0f;
            isBeat = false;

            beatData = new BeatData();
        }
        public void Set(SpectrumData data)
        {
            // Block copy is (probably) the fastest way to copy data in C#
            Buffer.BlockCopy(data.specL, 0, specL, 0, spectrumSize * sizeof(float));
            Buffer.BlockCopy(data.specR, 0, specR, 0, spectrumSize * sizeof(float));

            isBeat = data.isBeat;
            beatData = data.beatData;

            averageL = data.averageL;
            averageR = data.averageR;
        }
        public void Reset(int _spectrumSize )
        {
            // By assigning these values to null we ask the memory manager to delete their previous data.
            specL = null;
            specR = null;

            spectrumSize = _spectrumSize;

            specL = new float[spectrumSize];
            specR = new float[spectrumSize];
            isBeat = false;

        }
        public void Clear()
        {
            Array.Clear(specL, 0, spectrumSize);
            Array.Clear(specR, 0, spectrumSize);
            isBeat = false;
            averageL = 0.0f;
            averageR = 0.0f;
        }
    }


    public class SpectrumBuffer
    {

        #region Fields

        private static SpectrumBuffer instance;
        public static SpectrumBuffer Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SpectrumBuffer();
                }
                return instance;
            }
        }

        private SpectrumData data1;
        private SpectrumData data2;

        private volatile int updateBuff;
        private bool amReading;

        private int spectrumSize;
        public int SectrumSize
        {
            get{ return spectrumSize; }
            set { if(value == 256 || value == 512 || value == 1024 || value == 2048)
                spectrumSize = value;
                else
                spectrumSize = 512;
            } 
        }

        #endregion

        #region Initialize

        SpectrumBuffer()
        {
            spectrumSize = 512;
            data1 = new SpectrumData(spectrumSize);
            data2 = new SpectrumData(spectrumSize);
            updateBuff = 1;
            amReading = false;
        }

        /// <summary>
        /// Resets the fields according to the options set in OptionsMenuScreen. This is called by the Sound Manager class.
        /// </summary>
        public void Reset()
        {
            data1.Reset(spectrumSize);
            data2.Reset(spectrumSize);
        }

        #endregion

        #region DataFetching and Updating

        public void UpdateData(SpectrumData data)
        {
            if (amReading)
                return;

            if (updateBuff == 1)
            {
                data2.Set(data);
                updateBuff = 2;
            }
            else
            {
                data1.Set(data);
                updateBuff = 1;
            }
        }

        public SpectrumData GetLatestData()
        {
            amReading = true;

            amReading = false;
            if(updateBuff == 1)
                return data1;
            else
                return data2;
        }

        #endregion


    }
}
