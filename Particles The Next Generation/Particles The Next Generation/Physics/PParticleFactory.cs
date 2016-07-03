using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Globalization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Particles_The_Next_Generation
{
    public class PParticleFactory
    {
        #region Members
        protected Texture2D[] m_Sprites;
        protected int m_SpriteCount;

        protected BaseParticleProperties m_BaseParticleProperties;
        protected DeviationProperties m_DeviationProperties;

        protected Random r;
        #endregion

        public PParticleFactory(Texture2D[] sprites, string baseType, string devType)
        {
            this.m_Sprites = sprites;
            this.m_SpriteCount = m_Sprites.Count();

            this.m_BaseParticleProperties = ReadBaseParticleProperties(baseType);
            this.m_DeviationProperties = ReadDeviationProperties(devType);

            this.r = new Random();
        }

        #region Properties
        public BaseParticleProperties BaseParticleProperties
        {
            get { return this.m_BaseParticleProperties; }
        }

        public DeviationProperties DeviationProperties
        {
            get { return this.m_DeviationProperties; }
        }

        public float XDeviation
        {
            get { return this.m_DeviationProperties.PositionXDeviation; }
            set { this.m_DeviationProperties.PositionXDeviation = value; }
        }

        public float YDeviation
        {
            get { return this.m_DeviationProperties.PositionYDeviation; }
            set { this.m_DeviationProperties.PositionYDeviation = value; }
        }
        #endregion

        public IPhysicsParticle GenerateParticle(Vector2 position, string type)
        {
            switch (type)
            {
                case "regular":
                    return GenerateRegularParticle(position);
                case "music":
                    return GenerateMusicParticle(position);
            }
            return GenerateRegularParticle(position);
        }

        protected IPhysicsParticle GenerateRegularParticle(Vector2 position)
        {
            int doubleup = r.Next(m_SpriteCount);
            Texture2D sprite = m_Sprites[r.Next(m_SpriteCount)];
            float ttl = m_BaseParticleProperties.TTL + m_DeviationProperties.TTLDeviation * Utility.NextFloat(r);

            position.X += m_DeviationProperties.PositionXDeviation * Utility.NextFloat(r);
            position.Y += m_DeviationProperties.PositionYDeviation * Utility.NextFloat(r);

            float scaleMassMul = Utility.NextFloat(r);
            float mass = m_BaseParticleProperties.Mass + m_DeviationProperties.MassDeviation * scaleMassMul;
            float scale = m_BaseParticleProperties.Scale + m_DeviationProperties.ScaleDeviation * scaleMassMul;

            Color startColor = new Color(), endColor = new Color();

            startColor.R = (byte)r.Next(0, 256);
            startColor.G = (byte)r.Next(0, 256);
            startColor.B = (byte)r.Next(0, 256);
            startColor.A = 255;

            endColor.R = (byte)r.Next(0, 256);
            endColor.G = (byte)r.Next(0, 256);
            endColor.B = (byte)r.Next(0, 256);
            endColor.A = 255;

            return new PhysicsParticle(sprite, position, mass, ttl, scale,
                m_BaseParticleProperties.CollisionFraction,
                m_BaseParticleProperties.BounceDamping,
                m_BaseParticleProperties.GroundDamping,
                m_BaseParticleProperties.CollisionDamping,
                m_BaseParticleProperties.FrictionDamping,
                startColor, endColor);
        }

        protected IPhysicsParticle GenerateMusicParticle(Vector2 position)
        {
            int doubleup = r.Next(m_SpriteCount);
            Texture2D sprite = m_Sprites[r.Next(m_SpriteCount)];
            float ttl = m_BaseParticleProperties.TTL + m_DeviationProperties.TTLDeviation * Utility.NextFloat(r);

            position.X += m_DeviationProperties.PositionXDeviation * Utility.NextFloat(r);
            position.Y += m_DeviationProperties.PositionYDeviation * Utility.NextFloat(r);

            float scaleMassMul = Utility.NextFloat(r);
            float mass = m_BaseParticleProperties.Mass + m_DeviationProperties.MassDeviation * scaleMassMul;
            float scale = m_BaseParticleProperties.Scale + m_DeviationProperties.ScaleDeviation * scaleMassMul;

            return new MusicParticle(sprite, position, mass, ttl, scale,
                m_BaseParticleProperties.CollisionFraction,
                m_BaseParticleProperties.BounceDamping,
                m_BaseParticleProperties.GroundDamping,
                m_BaseParticleProperties.CollisionDamping,
                m_BaseParticleProperties.FrictionDamping);
        }

        #region Reading shit

        protected BaseParticleProperties ReadBaseParticleProperties(string type)
        {
            StreamReader reader = new StreamReader("Content/Settings/Particles/" + type + ".txt");
            List<string[]> properties = new List<string[]>();

            string line = reader.ReadLine();

            while (line != null && line != "")
            {
                properties.Add(line.Split(' '));
                line = reader.ReadLine();
            }

            return new BaseParticleProperties(
                float.Parse(properties[0][1], NumberStyles.Any),
                float.Parse(properties[1][1], NumberStyles.Any),
                float.Parse(properties[2][1], NumberStyles.Any),
                float.Parse(properties[3][1], NumberStyles.Any),
                float.Parse(properties[4][1], NumberStyles.Any),
                float.Parse(properties[5][1], NumberStyles.Any),
                float.Parse(properties[6][1], NumberStyles.Any),
                float.Parse(properties[7][1], NumberStyles.Any));
        }

        protected DeviationProperties ReadDeviationProperties(string type)
        {
            StreamReader reader = new StreamReader("Content/Settings/Deviations/" + type + ".txt");

            List<string[]> properties = new List<string[]>();

            string line = reader.ReadLine();

            while (line != null && line != "")
            {
                properties.Add(line.Split(' '));
                line = reader.ReadLine();
            }

            int currentProp = 0;

            return new DeviationProperties(
                float.Parse(properties[currentProp++][1], NumberStyles.Any),
                float.Parse(properties[currentProp++][1], NumberStyles.Any),
                float.Parse(properties[currentProp++][1], NumberStyles.Any),
                float.Parse(properties[currentProp++][1], NumberStyles.Any),
                float.Parse(properties[currentProp++][1], NumberStyles.Any));
        }

        #endregion
    }
}
