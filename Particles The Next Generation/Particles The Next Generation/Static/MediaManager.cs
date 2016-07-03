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

namespace Particles_The_Next_Generation
{
    public static class MediaManager
    {
        #region Members

        static Color currentColor;
        static ColorManager_Beat colorManager;
        static FMOD_Player fmod;
        static int spectrumSize;
        static bool beatQueue;
        static Equalizer equalizer;
        #endregion

        public static void Initialize()
        {
            spectrumSize = 1024;

            fmod = new FMOD_Player(spectrumSize);

            currentColor = Color.White;

            equalizer = new Equalizer(Utility.Load<Texture2D>("equalizer"));

            colorManager = new ColorManager_Beat(Color.Blue, Color.Red, 200);
        }

        public static void ShutDown()
        {
            FMOD_Wrapper.Shutdown();
        }

        public static void Pause()
        {
            if (fmod.IsMusicPlaying())
                fmod.Pause();
        }

        public static void Resume()
        {
            fmod.Resume();
        }

        public static bool BeatQueue
        {
            set { beatQueue = value; }
        }

        public static FMOD_Player FMOD_Wrapper
        {
            get { return fmod; }
        }

        private static void FMOD_LoadSong(string path)
        {
            fmod.PlayMusic(path);
        }

        public static Color CurrentColor
        {
            get { return currentColor; }
        }

        public static Vector3 test;

        public static void Update(float dt)
        {
            #region Input
            if (Input.KeyPressed(Keys.L))
            {
                fmod.Pause();
                string path = FileBrowser.GetUserFilePath();

                try
                {
                    FMOD_LoadSong(path);
                }
                catch
                {
                    fmod.Resume();
                }
            }

            if (Input.KeyPressed(Keys.P))
                if (fmod.IsMusicPlaying())
                    Pause();
                else Resume();

            if (Input.KeyPressed(Keys.R))
            {
                equalizer.Enabled = !equalizer.Enabled;
            }
            #endregion

            colorManager.Update(dt, beatQueue);
            beatQueue = false;

            currentColor = colorManager.CurrentColor;
        }


        public static float variable = 0;

        public static void Draw(SpriteBatch sb)
        {
            if (Global.ShowInfo)
            {
                StringDrawer.DrawLine(sb, StringAlignment.Center, "Test: " + test.X + ", " + test.Y + ", " + test.Z, Color.Green);
                StringDrawer.DrawLine(sb, StringAlignment.Center, "Variable: " + variable, Color.Green);
                StringDrawer.DrawLine(sb, StringAlignment.Center, "Playing?: " + fmod.IsMusicPlaying(), Color.Green);
            }

            fmod.Draw(sb);

            equalizer.Draw(sb);
        }
    }
}
