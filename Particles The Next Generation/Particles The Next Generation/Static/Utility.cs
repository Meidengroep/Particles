using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Microsoft.Xna.Framework;
using System.IO;
using TApplication = System.Windows.Forms.Application;
using Microsoft.Xna.Framework.Media;

namespace Particles_The_Next_Generation
{
    static class Utility
    {
        public static float NextFloat(Random r)
        {
            return (float)(r.NextDouble() - r.NextDouble());
        }

        #region Vector Operations
        public static float Dot(Vector2 v1, Vector2 v2)
        {
            return v1.X * v2.X + v1.Y * v2.Y;
        }

        public static Vector2 RotateVector_Degrees(Vector2 vec, float angle)
        {
            angle = MathHelper.ToRadians(angle);
            return RotateVector_Radians(vec, angle);
        }

        public static Vector2 RotateVector_Radians(Vector2 vec, float angle)
        {
            float sin = (float)Math.Sin(angle);
            float cos = (float)Math.Cos(angle);
            Vector2 result = Vector2.Zero;

            result.X = vec.X * cos - vec.Y * sin;
            result.Y = vec.X * sin + vec.Y * cos;

            return result;
        }

        public static float PercentualDistance(Vector2 myPos, Vector2 otherPos, float minRadius, float maxRadius)
        {
            Vector2 direction = otherPos - myPos;
            float length = direction.Length();
            return PercentualDistance(length, minRadius, maxRadius);
        }

        public static float PercentualDistance(float length, float minRadius, float maxRadius)
        {
            float range = maxRadius - minRadius;

            float lengthInRange = length - minRadius;

            return lengthInRange / range;
        }

        public static float CCWAngle(Vector2 vector)
        {
            return (float)Math.Atan2(vector.Y, vector.X);
        }

        #endregion

        #region Color Operations

        public static Color Lerp(Color beginColor, Color endColor, float amount)
        {
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

            return new Color((int)r, (int)g, (int)b);
        }

        #endregion

        public static T Load<T>(string assetName)
        {
            return Global.Game.Content.Load<T>(assetName);
        }


        public static Song LoadSong(string path)
        {
            var ctor = typeof(Song).GetConstructor(
                BindingFlags.NonPublic | BindingFlags.Instance, null,
                new[] { typeof(string), typeof(string), typeof(int) }, null);

            Song song = (Song)ctor.Invoke(new object[] { "name", @path, 0 });
            
            return song;
        }
    }
}
