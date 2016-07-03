using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Particles_The_Next_Generation
{
    public class GravityParticle: SimpleParticle
    {
        protected float m_GravitationalAcceleration;

        public GravityParticle(Texture2D sprite, Vector2 position, Vector2 velocity, float ttl, byte opacity, float scale, float rotation, float gravityAcceleration)
            : base(sprite, position, velocity, ttl, opacity, scale, rotation)
        {
            this.m_GravitationalAcceleration = gravityAcceleration;
        }

        protected override void UpdateVelocity(float dt)
        {
            m_Velocity.Y += dt * m_GravitationalAcceleration;
        }
    }
}
