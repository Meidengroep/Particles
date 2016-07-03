using System;
using System.Collections.Generic;
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
    static class Global
    {
        #region members

        static int screenWidth;
        static int screenHeight;
        static TheNextGeneration game;
        static SpriteFont standardFont;
        static bool showInfo;
        static bool[] beats;
        static float[] beatYs;
        static float[] frequencies;

        #endregion

        public static void Initialize(TheNextGeneration thegame, SpriteFont normalFont)
        {
            showInfo = false;
            standardFont = normalFont;
            game = thegame;
            screenWidth = game.graphics.GraphicsDevice.Viewport.Width;
            screenHeight = game.graphics.GraphicsDevice.Viewport.Height;
        }

        public static void Update(float dt)
        {
            screenWidth = game.graphics.GraphicsDevice.Viewport.Width;
            screenHeight = game.graphics.GraphicsDevice.Viewport.Height;

            if (Input.KeyPressed(Keys.I))
                showInfo = !showInfo;
        }

        public static bool[] Beats
        {
            get { return beats; }
            set { beats = value; }
        }

        public static float[] BeatYs
        {
            get { return beatYs; }
            set { beatYs = value; }
        }

        public static float[] Frequencies
        {
            get { return frequencies; }
            set { frequencies = value; }
        }

        public static bool ShowInfo
        {
            get { return showInfo; }
        }

        public static float WindStrength
        {

            get { return game.WindInput.Multiplier; }
        }

        public static Vector2 WindDirection
        {
            get { return game.WindInput.Direction; }
        }

        public static TheNextGeneration Game
        {
            get { return game; }
        }

        public static SpriteFont StandardFont
        {
            get { return standardFont; }
        }

        public static int ScreenWidth
        {
            get { return screenWidth; }
        }

        public static int ScreenHeight
        {
            get { return screenHeight; }
        }

    }
}
