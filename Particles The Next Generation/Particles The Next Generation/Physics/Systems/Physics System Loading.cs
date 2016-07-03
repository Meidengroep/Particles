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
    public partial class Physics_System
    {
        protected void ReadAndInitSystemProperties(string type, int spriteWidth)
        {
            StreamReader reader = new StreamReader("Content/Settings/Systems/" + type + ".txt");

            List<string[]> properties = new List<string[]>();

            string line = reader.ReadLine();

            while (line != null && line != "")
            {
                properties.Add(line.Split(' '));
                line = reader.ReadLine();
            }

            int currentProp = 0;

            this.m_MaxParticles = int.Parse(properties[currentProp++][1]);
            this.m_SpawnDelay = float.Parse(properties[currentProp++][1], NumberStyles.Any);
            this.m_SpawnDelayInc = m_SpawnDelay / 10;
            this.m_MouseRadius = float.Parse(properties[currentProp++][1], NumberStyles.Any);
            this.m_MouseForceMultiplier = float.Parse(properties[currentProp++][1], NumberStyles.Any);

            this.m_GravityDirection = Vector2.Zero;
            this.m_GravityDirection.X = float.Parse(properties[currentProp++][1], NumberStyles.Any);
            this.m_GravityDirection.Y = float.Parse(properties[currentProp++][1], NumberStyles.Any);
            this.m_GravityStrength = float.Parse(properties[currentProp++][1], NumberStyles.Any);
            this.m_Gravity = m_GravityDirection * m_GravityStrength;

            bool collisionEnabled = "true" == properties[currentProp++][1];

            int cellSize = (int)Math.Ceiling((m_ParticleFactory.BaseParticleProperties.Scale + m_ParticleFactory.DeviationProperties.ScaleDeviation) * spriteWidth);

            int gridWidth = (int)Math.Ceiling(Global.ScreenWidth / (float)cellSize);
            int gridHeight = (int)Math.Ceiling(Global.ScreenHeight / (float)cellSize);

            m_GridCollisionHandler = new CollisionGridManager(gridWidth, gridHeight, cellSize);
            m_GridCollisionHandler.CollisionsEnabled = collisionEnabled;

            BaseGravityWellProperties wellprops = new BaseGravityWellProperties(
                new Vector2(float.Parse(properties[currentProp][1], NumberStyles.Any), float.Parse(properties[currentProp][2], NumberStyles.Any)),
                float.Parse(properties[currentProp][3], NumberStyles.Any),
                float.Parse(properties[currentProp++][4], NumberStyles.Any),
                Global.Game.Content.Load<Texture2D>("Doodats/well"));

            BaseVortexProperties vortexprops = new BaseVortexProperties(
                new Vector2(float.Parse(properties[currentProp][1], NumberStyles.Any), float.Parse(properties[currentProp][2], NumberStyles.Any)),
                float.Parse(properties[currentProp][3], NumberStyles.Any),
                float.Parse(properties[currentProp][4], NumberStyles.Any),
                properties[currentProp][5] == "true",
                float.Parse(properties[currentProp][6], NumberStyles.Any),
                float.Parse(properties[currentProp][7], NumberStyles.Any),
                float.Parse(properties[currentProp++][8], NumberStyles.Any),
                Global.Game.Content.Load<Texture2D>("Doodats/vortex"));

            this.m_GravWellManager = new GravWellContainer(wellprops, vortexprops);
        }
    }
}
