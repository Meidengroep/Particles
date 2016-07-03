using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Particles_The_Next_Generation
{
    public class Simple_System : IParticleSystem
    {

        #region private members

        protected Vector2 m_Position;
        protected List<IParticle> m_Particles;
        protected List<Texture2D> m_Sprites;
        protected Random r;

        // The initial velocity of a generated particle.
        protected Vector2 m_InitialVelocity, m_VelocityDeviation;

        // The initial time to live for a generated particle, in milliseconds.
        protected float m_InitialTTL, m_TTLDeviation;

        protected byte m_InitialOpacity, m_OpacityDeviation;
        protected float m_InitialScale, m_ScaleDeviation;
        protected float m_InitialRotation, m_RotationDeviation;

        protected int m_SpriteCount;

        protected bool autoGeneration;

        #endregion

        public Simple_System(List<Texture2D> sprites, Vector2 startPosition)
        {
            Initialize(startPosition, sprites);

            m_InitialVelocity = Vector2.Zero;
            m_InitialTTL = 1000;
            m_InitialOpacity = 1;
            m_InitialScale = 1;
            m_InitialRotation = 0;

            m_VelocityDeviation = Vector2.Zero;
            m_TTLDeviation = 0;
            m_OpacityDeviation = 0;
            m_ScaleDeviation = 0;
            m_RotationDeviation = 0;
        }

        public Simple_System(List<Texture2D> sprites, Vector2 startPosition, Vector2 initialVelocity, float initialTTL, byte initialOpacity, float initialScale, float initialRotation)
        {
            Initialize(startPosition, sprites);

            m_InitialVelocity = initialVelocity;
            m_InitialTTL = initialTTL;
            m_InitialOpacity = initialOpacity;
            m_InitialScale = initialScale;
            m_InitialRotation = initialRotation;

            m_VelocityDeviation = Vector2.Zero;
            m_TTLDeviation = 0;
            m_OpacityDeviation = 0;
            m_ScaleDeviation = 0;
            m_RotationDeviation = 0;

            autoGeneration = false;
        }

        public Simple_System(List<Texture2D> sprites, Vector2 startPosition,
            Vector2 initialVelocity, float initialTTL, byte initialOpacity, float initialScale, float initialRotation,
            Vector2 velocityDeviation, float ttlDeviation, byte opacityDeviation, float scaleDeviation, float rotationDeviation)
        {
            Initialize(startPosition, sprites);

            m_InitialVelocity = initialVelocity;
            m_InitialTTL = initialTTL;
            m_InitialOpacity = initialOpacity;
            m_InitialScale = initialScale;
            m_InitialRotation = initialRotation;

            m_VelocityDeviation = velocityDeviation;
            m_TTLDeviation = ttlDeviation;
            m_OpacityDeviation = opacityDeviation;
            m_ScaleDeviation = scaleDeviation;
            m_RotationDeviation = rotationDeviation;
        }

        private void Initialize(Vector2 startPosition, List<Texture2D> sprites)
        {
            m_Position = startPosition;

            m_Particles = new List<IParticle>();
            m_Sprites = new List<Texture2D>(sprites);
            m_SpriteCount = m_Sprites.Count;

            r = new Random();
        }

        public void GenerateNewParticles()
        {
            m_Particles.Add(GenerateSingleParticle());
        }

        public virtual IParticle GenerateSingleParticle()
        {
            float velocity_x = Input.MouseVelocity.X / 80 + (float)(r.NextDouble() - r.NextDouble()) * m_VelocityDeviation.X;
            float velocity_y = Input.MouseVelocity.Y / 80 + (float)(r.NextDouble() - r.NextDouble()) * m_VelocityDeviation.Y;
            Vector2 velocity = new Vector2(velocity_x, velocity_y);

            float ttl = m_InitialTTL + (float)(r.NextDouble() - r.NextDouble()) * m_TTLDeviation;
            byte opacity = (byte)(m_InitialOpacity - (r.NextDouble() * m_OpacityDeviation));
            float scale = m_InitialScale + (float)(r.NextDouble() - r.NextDouble()) * m_ScaleDeviation;
            float rotation = m_InitialRotation + (float)(r.NextDouble() - r.NextDouble()) * m_RotationDeviation;

            Texture2D sprite = m_Sprites[r.Next(m_SpriteCount)];

            return new SimpleParticle(sprite, m_Position, velocity, ttl, opacity, scale, rotation);
        }

        public void UpdateParticles(float dt, Vector2 position)
        {
            m_Position = position;

            if (Input.RMB_Clicked)
            {
                autoGeneration = !autoGeneration;
            }

            if (Input.LMB_Clicked || autoGeneration)
                GenerateNewParticles();

            for (int i = 0; i < m_Particles.Count; i++)
            {
                if (m_Particles[i].TTL < 0)
                {
                    m_Particles.RemoveAt(i);
                    i--;
                    continue;
                }
                m_Particles[i].UpdateParameters(dt, 0, 0, -0.001f, 0);
                m_Particles[i].Update(dt);
            }
        }

        public void DrawParticles(SpriteBatch sb)
        {
            foreach (IParticle particle in m_Particles)
            {
                particle.Draw(sb);
            }
        }
    }
}
