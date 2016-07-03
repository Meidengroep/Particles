using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Particles_The_Next_Generation
{
    public class Gravity_System : Simple_System
    {
        protected float m_InitialGravity, m_GravityDeviation;

        public Gravity_System(List<Texture2D> sprites, Vector2 startPosition)
            : base(sprites, startPosition)
        {

        }

        public Gravity_System(List<Texture2D> sprites, Vector2 startPosition, Vector2 initialVelocity, float initialTTL, byte initialOpacity, float initialScale, float initialRotation, float initialGravity)
            : base(sprites, startPosition, initialVelocity, initialTTL, initialOpacity, initialScale, initialRotation)
        {
            this.m_InitialGravity = initialGravity;
        }

        public Gravity_System(List<Texture2D> sprites, Vector2 startPosition,
            Vector2 initialVelocity, float initialTTL, byte initialOpacity, float initialScale, float initialRotation, float initialGravity,
            Vector2 velocityDeviation, float ttlDeviation, byte opacityDeviation, float scaleDeviation, float rotationDeviation, float gravityDeviation)
            : 
            base(sprites, startPosition, 
            initialVelocity, initialTTL, initialOpacity, initialScale, initialRotation, 
            velocityDeviation, ttlDeviation, opacityDeviation, scaleDeviation, rotationDeviation)
        {
            this.m_InitialGravity = initialGravity;
            this.m_GravityDeviation = gravityDeviation;
        }

        public override IParticle GenerateSingleParticle()
        {
            float velocity_x = Input.MouseVelocity.X / 80 + (float)(r.NextDouble() - r.NextDouble()) * m_VelocityDeviation.X;
            float velocity_y = Input.MouseVelocity.Y / 80 + (float)(r.NextDouble() - r.NextDouble()) * m_VelocityDeviation.Y;
            Vector2 velocity = new Vector2(velocity_x, velocity_y);

            float gravity = m_InitialGravity + (float)(r.NextDouble() - r.NextDouble()) * m_GravityDeviation;

            float ttl = m_InitialTTL + (float)(r.NextDouble() - r.NextDouble()) * m_TTLDeviation;
            byte opacity = (byte)(m_InitialOpacity - (r.NextDouble() * m_OpacityDeviation));
            float scale = m_InitialScale + (float)(r.NextDouble() - r.NextDouble()) * m_ScaleDeviation;
            float rotation = m_InitialRotation + (float)(r.NextDouble() - r.NextDouble()) * m_RotationDeviation;

            Texture2D sprite = m_Sprites[r.Next(m_SpriteCount)];

            return new GravityParticle(sprite, m_Position, velocity, ttl, opacity, scale, rotation, gravity);
        }
    }
}
