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
    static class PhysicsProperties
    {
        #region Members

        static float maxPhysicsVelocity;
        static float velocityMultiplier;

        #endregion

        public static void Initialize()
        {
            maxPhysicsVelocity = 1000;
            velocityMultiplier = 1.0f;
        }

        public static float MaxPhysicsVelocity
        {
            get { return maxPhysicsVelocity; }
            set { maxPhysicsVelocity = value; }
        }

        public static float VelocityMultiplier
        {
            get { return velocityMultiplier; }
            set { velocityMultiplier = value; }
        }
    }
}
