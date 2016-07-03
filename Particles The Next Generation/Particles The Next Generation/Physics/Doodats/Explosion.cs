using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Particles_The_Next_Generation
{
    public class Explosion
    {
        protected float m_CurrentStrength, m_MinStrength, m_MaxStrength, m_StrengthRange;
        protected float m_TTL, m_MaxTTL;
        protected float m_MinRadius, m_MaxRadius, m_CurrentMax, m_RadiusRange;
        protected Vector2 m_Position;

        public Explosion(Vector2 position, float maxStrength, float minStrength, float ttl, float minRadius, float maxRadius)
        {
            this.m_Position = position;

            this.m_MinStrength = minStrength;
            this.m_MaxStrength = maxStrength;
            this.m_CurrentStrength = maxStrength;
            this.m_StrengthRange = maxStrength - minStrength;

            this.m_TTL = ttl;
            this.m_MaxTTL = ttl;

            this.m_MinRadius = minRadius;
            this.m_MaxRadius = maxRadius;
            this.m_RadiusRange = maxRadius - minRadius;
        }

        public float TTL
        {
            get { return this.m_TTL; }
        }

        public virtual Vector2 GetForce(IPhysicsParticle particle)
        {
            Vector2 direction = particle.Position - m_Position;

            if (direction.Length() > m_CurrentMax)
                return Vector2.Zero;

            return direction * m_CurrentStrength;
        }

        public virtual void Update(float dt)
        {
            Update_Aux(dt);
        }
        public virtual void Update(float dt, Vector2 position)
        {
            this.m_Position = position;
            Update_Aux(dt);
        }

        protected virtual void Update_Aux(float dt)
        {
            m_TTL -= dt;

            float mul = 1 - (m_TTL / m_MaxTTL);

            m_CurrentMax = m_MinRadius + mul * m_RadiusRange;
            m_CurrentStrength = m_MaxStrength - mul * m_StrengthRange;
        }

        public virtual void Draw(SpriteBatch sb)
        {
            //sb.DrawString(Global.StandardFont, "CurrentMax: " + m_CurrentMax + "Currentstrength: " + m_CurrentStrength, m_Position, Color.Red);
        }
    }
}
