using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

public enum SampleType
{
    Frequency,
    Samples,
    Count
}

namespace Particles_The_Next_Generation
{
    public static class MediaManager
    {
        #region Members

        static VisualizationData data;
        static float freqR, freqG, freqB, currentTime, timestep, amount;
        static Color beginColor, endColor, currentColor;
        static SampleType samplerType;
        static Equalizer equalizer;
        static FMOD_Wrapper fmod;
        static SpectrumBuffer buffer;
        static bool beatQueue;
        #endregion

        public static void Initialize()
        {
            fmod = new FMOD_Wrapper();
            fmod.Initialize();
            buffer = SpectrumBuffer.Instance;

            MediaPlayer.IsVisualizationEnabled = true;
            MediaPlayer.IsRepeating = true;
            samplerType = (SampleType)0;

            MediaPlayer.Volume = 0.05f;

            data = new VisualizationData();
            beginColor = Color.White;
            endColor = Color.White;
            currentColor = Color.White;
            currentTime = 0;
            timestep = 1;

            equalizer = new Equalizer(Utility.Load<Texture2D>("equalizer"));
        }

        public static bool BeatQueue
        {
            set { beatQueue = value; }
        }

        public static FMOD_Wrapper FMOD_Wrapper
        {
            get { return fmod; }
        }

        private static void FMOD_Load(string path)
        {
            fmod.PlayMusic(path);
        }

        public static Color FreqColor
        {
            get { return currentColor; }
        }

        public static ReadOnlyCollection<float> GetSamplerCollection
        {
            get
            {
                switch (samplerType)
                {
                    case SampleType.Frequency:
                        return data.Frequencies;
                    case SampleType.Samples:
                        return data.Samples;
                }
                throw new NotImplementedException("Sampler state is not yet defined");
            }
            set { throw new Exception("I do what I want!"); }
        }

        public static VisualizationData VisData
        {
            get { return data; }
        }

        public static Vector3 test;

        public static void Update(float dt)
        {
            #region Input
            if (Input.KeyPressed(Keys.L))
            {
                MediaPlayer.Pause();
                string path = FileBrowser.GetUserFilePath();

                try
                {
                    FMOD_Load(path);
                }
                catch
                {
                    fmod.ResumeMusic();
                }
                /*
                try
                {
                    Song song = Utility.LoadSong(path);
                    MediaPlayer.Play(song);
                }
                catch
                {
                    MediaPlayer.Resume();
                }*/
            }

            if (Input.KeyPressed(Keys.E))
                equalizer.Enabled = !equalizer.Enabled;


            if (Input.KeyPressed(Keys.OemQuestion))
            {
                samplerType = (SampleType)((int)(samplerType + 1) % (int)SampleType.Count);
            }

            if (Input.KeyPressed(Keys.U))
            {
                if (MediaPlayer.State == MediaState.Playing)
                    MediaPlayer.Pause();
                else
                    MediaPlayer.Resume();
            }

            if (Input.KeyDown(Keys.Add))
            {
                MediaPlayer.Volume += 0.0005f * dt;
            }

            if (Input.KeyDown(Keys.Subtract))
            {
                MediaPlayer.Volume -= 0.0005f * dt;
            }
            #endregion

            currentTime += dt;
            MediaPlayer.GetVisualizationData(data);

            if (currentTime > timestep)
            {
                #region Calculate New Begin Color

                currentTime %= timestep;

                if (MediaPlayer.State == MediaState.Playing)
                {
                    Linear_OneThird();
                }

                if (fmod.IsMusicPlaying())
                {
                    beginColor = endColor;

                    if (beatQueue)
                        endColor = Color.Red;
                    else endColor = new Color(0, 0, 0, 0);// new Color((int)(endColor.R * 0.9f), (int)(endColor.G * 0.9f), (int)(endColor.B * 0.9f));

                    beatQueue = false;
                }

                #endregion
            }
            
            {
                #region Interpolate between Begin and End Colors

                amount = 0;

                amount = currentTime / timestep;

                float r, g, b;
                float rDiff, gDiff, bDiff;

                rDiff = Math.Abs(beginColor.R - endColor.R);
                gDiff = Math.Abs(beginColor.G - endColor.G);
                bDiff = Math.Abs(beginColor.B - endColor.B);

                if (beginColor.R > endColor.R)
                    r = endColor.R + amount * rDiff;
                else r = endColor.R - amount * rDiff;

                if (beginColor.G > endColor.G)
                    g = endColor.G + amount * gDiff;
                else g = endColor.G - amount * gDiff;

                if (beginColor.B > endColor.B)
                    b = endColor.B + amount * bDiff;
                else b = endColor.B - amount * bDiff;

                r = MathHelper.Clamp(r, 0, 255);
                g = MathHelper.Clamp(g, 0, 255);
                b = MathHelper.Clamp(b, 0, 255);

                //currentColor = Color.Lerp(beginColor, endColor, amount);
                currentColor = new Color((int)r, (int)g, (int)b);

                #endregion
            }
        }

        private static void Singular_OneThird_MaxValue()
        {
            float maxR = 0;
            float maxG = 0;
            float maxB = 0;

            float minR = 0;
            float minG = 0;
            float minB = 0;

            int oneThird = (int)Math.Floor((data.Frequencies.Count * 0.9f) / 3f);
            ReadOnlyCollection<float> currentValues = GetSamplerCollection;

            int start1 = 0; int end1 = oneThird;
            int start2 = end1; int end2 = start2 + oneThird;
            int start3 = end2; int end3 = currentValues.Count;

            for (int i = start1; i < end1; i++)
            {
                if (maxR < currentValues[i]) maxR = currentValues[i];
                if (minR > currentValues[i]) minR = currentValues[i];
            }

            for (int i = start2; i < end2; i++)
            {
                if (maxG < currentValues[i]) maxG = currentValues[i];
                if (minG > currentValues[i]) minG = currentValues[i];
            }

            for (int i = start3; i < end3; i++)
            {
                if (maxB < currentValues[i]) maxB = currentValues[i];
                if (minB > currentValues[i]) minB = currentValues[i];
            }

            freqR = minR + (maxR - minR) / 2;
            freqG = minG + (maxG - minG) / 2;
            freqB = minB + (maxB - minG) / 2;

            test.X = freqR;
            test.Y = freqG;
            test.Z = freqB;

            endColor = new Color(beginColor.R, beginColor.G, beginColor.B);
            beginColor = new Color((int)MathHelper.Clamp(freqR * 255, 0, 255), (int)MathHelper.Clamp(freqG * 255, 0, 255), (int)MathHelper.Clamp(freqB * 255, 0, 255));

            test.X = freqR;
            test.Y = freqG;
            test.Z = freqB;
        }

        private static void Linear_OneThird()
        {
            float totalR = 0;
            float totalG = 0;
            float totalB = 0;

            int oneThird = (int)Math.Floor((data.Frequencies.Count * 0.9f) / 3f);
            ReadOnlyCollection<float> currentValues = GetSamplerCollection;

            int start1 = 0; int end1 = oneThird;
            int start2 = end1; int end2 = start2 + oneThird;
            int start3 = end2; int end3 = currentValues.Count;

            for (int i = start1; i < end1; i++)
            {
                totalR += currentValues[i];
            }

            for (int i = start2; i < end2; i++)
            {
                totalG += currentValues[i];
            }

            for (int i = start3; i < end3; i++)
            {
                totalB += currentValues[i];
            }

            freqR = Math.Abs(totalR / (oneThird));
            freqG = Math.Abs(totalG / (oneThird));
            freqB = Math.Abs(totalB / (oneThird));

            test.X = freqR;
            test.Y = freqG;
            test.Z = freqB;

            endColor = new Color(beginColor.R, beginColor.G, beginColor.B);
            beginColor = new Color((int)MathHelper.Clamp(freqR * 255, 0, 255), (int)MathHelper.Clamp(freqG * 255, 0, 255), (int)MathHelper.Clamp(freqB * 255, 0, 255));

            test.X = beginColor.R;
            test.Y = beginColor.G;
            test.Z = beginColor.B;
        }

        private static void Quadratic_OneThird()
        {
            float totalR = 0;
            float totalG = 0;
            float totalB = 0;

            int oneThird = (int)Math.Floor((data.Frequencies.Count * 0.9f) / 3f);
            ReadOnlyCollection<float> currentValues = GetSamplerCollection;

            int start1 = 0; int end1 = oneThird;
            int start2 = end1; int end2 = start2 + oneThird;
            int start3 = end2; int end3 = currentValues.Count;

            for (int i = start1; i < end1; i++)
            {
                totalR += currentValues[i] * currentValues[i];
            }

            for (int i = start2; i < end2; i++)
            {
                totalG += currentValues[i] * currentValues[i];
            }

            for (int i = start3; i < end3; i++)
            {
                totalB += currentValues[i] * currentValues[i];
            }

            freqR = Math.Abs(totalR / (oneThird));
            freqG = Math.Abs(totalG / (oneThird));
            freqB = Math.Abs(totalB / (oneThird));

            test.X = freqR;
            test.Y = freqG;
            test.Z = freqB;

            endColor = new Color(beginColor.R, beginColor.G, beginColor.B);
            beginColor = new Color((int)MathHelper.Clamp(freqR * 255, 0, 255), (int)MathHelper.Clamp(freqG * 255, 0, 255), (int)MathHelper.Clamp(freqB * 255, 0, 255));

            test.X = beginColor.R;
            test.Y = beginColor.G;
            test.Z = beginColor.B;
        }

        public static void Draw(SpriteBatch sb)
        {
            if (Global.ShowInfo)
            {
                StringDrawer.DrawLine(sb, StringAlignment.Center, "Test: " + MediaManager.test.X + ", " + MediaManager.test.Y + ", " + MediaManager.test.Z, Color.Green);
            }

            equalizer.Draw(sb);
        }
    }
}
