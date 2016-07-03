using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Particles_The_Next_Generation
{
    public class Implosion : Explosion
    {
        public Implosion(Vector2 position, float maxStrength, float minStrength, float ttl, float minRadius, float maxRadius)
            : base(position, maxStrength, minStrength, ttl, minRadius, maxRadius)
        {

        }

        public override Vector2 GetForce(IPhysicsParticle particle)
        {
            Vector2 direction = m_Position - particle.Position;

            if (direction.Length() > m_CurrentMax)
                return Vector2.Zero;

            return direction * m_CurrentStrength;
        }

        protected override void Update_Aux(float dt)
        {
            m_TTL -= dt;

            float mulStr =  (m_TTL / m_MaxTTL);
            float mul = 1 - mulStr;

            m_CurrentMax = m_MinRadius + m_RadiusRange;
            m_CurrentStrength = m_MinStrength + mul * m_StrengthRange;
        }
    }
}
