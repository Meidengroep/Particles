using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Particles_The_Next_Generation
{
    public class Singularity
    {
        protected Explosion m_Explosion;
        protected Implosion m_Implosion;
        protected float m_ImplodeTimer;

        public Singularity(float implodeTimer,
                           Vector2 expPosition, float expMaxStrength, float expMinStrength, float expTtl, float expMinRadius, float expMaxRadius,
                           Vector2 impPosition, float impMaxStrength, float impMinStrength, float impTtl, float impMinRadius, float impMaxRadius)
        {
            this.m_ImplodeTimer = implodeTimer;

            m_Explosion = new Explosion(expPosition, expMaxStrength, expMinStrength, expTtl, expMinRadius, expMaxRadius);
            m_Implosion = new Implosion(impPosition, impMaxStrength, impMinStrength, impTtl, impMinRadius, impMaxRadius);
        }

        public float TTL
        {
            get { return m_Explosion.TTL; }
        }

        public Vector2 GetForce(IPhysicsParticle particle)
        {
            if (m_ImplodeTimer > 0)
                return m_Implosion.GetForce(particle);
            else return m_Explosion.GetForce(particle);
        }

        public void Update(float dt)
        {
            m_ImplodeTimer -= dt;

            if (m_ImplodeTimer > 0)
            {
                m_Implosion.Update(dt);
            }
            else
            {
                m_Explosion.Update(dt);
            }
        }
    }
}
