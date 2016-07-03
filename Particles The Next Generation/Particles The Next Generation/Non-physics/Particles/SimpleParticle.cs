using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Particles_The_Next_Generation
{
    public class SimpleParticle : IParticle
    {
        #region private members

        protected Vector2 m_Position, m_Velocity, m_Origin;
        protected Texture2D m_Sprite;

        #endregion

        #region public members

        protected float m_TTL, m_Scale, m_Rotation;
        protected byte m_Opacity;

        #endregion


        public SimpleParticle(Texture2D sprite, Vector2 position, Vector2 velocity, float ttl, byte opacity, float scale, float rotation)
        {
            m_Sprite = sprite;
            m_Origin = new Vector2(sprite.Width / 2, sprite.Height / 2);

            m_Position = position;
            m_Velocity = velocity;
            m_TTL = ttl;
            m_Opacity = opacity;
            m_Scale = scale;
            m_Rotation = rotation;
        }

        public void UpdateParameters(float dt, float ttlChange, byte opacityChange, float scaleChange, float rotationChange)
        {
            m_TTL -= ttlChange * dt;

            byte opacityDiff = (byte)((float)opacityChange * dt / 3);

            if (opacityDiff < m_Opacity)
                m_Opacity -= opacityDiff;
            else m_Opacity = 0;

            float scaleDiff = scaleChange * dt;

            if (-scaleDiff < m_Scale)
                m_Scale += scaleDiff;
            else m_Scale = 0;

            m_Rotation += rotationChange * dt;
        }

        public float TTL
        {
            get { return this.m_TTL; }
        }

        protected virtual void UpdateVelocity(float dt)
        {

        }

        public void Update(float dt)
        {
            UpdateVelocity(dt);
            m_Position += m_Velocity * dt;
            this.m_TTL -= dt;
        }

        public void Draw(SpriteBatch sb)
        {
            Color color = Color.White;
            color.A = m_Opacity;
            
            
            sb.Draw(m_Sprite, m_Position, null, color, m_Rotation, m_Origin, m_Scale, SpriteEffects.None, 0);
        }
    }
}
