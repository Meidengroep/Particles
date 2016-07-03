using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using System.Globalization;
using Microsoft.Xna.Framework.Input;

namespace Particles_The_Next_Generation
{
    public enum StringAlignment
    {
        Left,
        Right,
        Center
    }
    public static class StringDrawer
    {
        static Vector2 leftVector, rightVector, centerVector;
        static float yTop, ySpacing;

        public static void Init(int width, int height)
        {
            yTop = 10;
            ySpacing = 20;

            leftVector = new Vector2(20, yTop);
            rightVector = new Vector2(width - 20, yTop);
            centerVector = new Vector2(width / 2, yTop);
            ResetPositions();
        }

        public static void ResetPositions()
        {
            leftVector.Y = yTop;
            rightVector.Y = yTop;
            centerVector.Y = yTop;
        }

        public static void DrawLine(SpriteBatch sb, StringAlignment alignment, string text, Color color)
        {
            Vector2 width = Global.StandardFont.MeasureString(text);
            width.Y = 0;

            switch (alignment)
            {
                case StringAlignment.Left:
                    sb.DrawString(Global.StandardFont, text, leftVector, color);
                    leftVector.Y += ySpacing;
                    return;
                case StringAlignment.Right:
                    sb.DrawString(Global.StandardFont, text, rightVector - width, color);
                    rightVector.Y += ySpacing;
                    return;
                case StringAlignment.Center:
                    sb.DrawString(Global.StandardFont, text, centerVector, color);
                    centerVector.Y += ySpacing;
                    return;
            }
        }
    }
}
