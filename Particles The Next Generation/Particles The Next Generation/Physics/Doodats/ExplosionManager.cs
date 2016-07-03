using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Particles_The_Next_Generation
{
    public class ExplosionManager
    {
        protected List<Explosion> m_Explosions;
        protected List<Singularity> m_Singularities;

        public ExplosionManager()
        {
            m_Explosions = new List<Explosion>();
            m_Singularities = new List<Singularity>();
        }

        public Vector2 GetForce(IPhysicsParticle particle)
        {
            Vector2 force = Vector2.Zero;

            for (int i = 0; i < m_Explosions.Count; i++)
                force += m_Explosions[i].GetForce(particle);

            for (int s = 0; s < m_Singularities.Count; s++)
                force += m_Singularities[s].GetForce(particle);

            return force;
        }

        public void AddExplosion(Vector2 position, float maxStrength, float minStrength, float ttl, float minRadius, float maxRadius)
        {
            m_Explosions.Add(new Explosion(position, maxStrength, minStrength, ttl, minRadius, maxRadius));
        }

        public void AddImplosion(Vector2 position, float maxStrength, float minStrength, float ttl, float minRadius, float maxRadius)
        {
            m_Explosions.Add(new Implosion(position, maxStrength, minStrength, ttl, minRadius, maxRadius));
        }

        public void AddSingularity(float implodeTimer,
                           Vector2 expPosition, float expMaxStrength, float expMinStrength, float expTtl, float expMinRadius, float expMaxRadius,
                           Vector2 impPosition, float impMaxStrength, float impMinStrength, float impTtl, float impMinRadius, float impMaxRadius)
        {
            m_Singularities.Add(new Singularity(implodeTimer,
                expPosition, expMaxStrength, expMinStrength, expTtl, expMinRadius, expMaxRadius, 
                impPosition, impMaxStrength, impMinStrength, impTtl, impMinRadius, impMaxRadius));
        }

        public void Update(float dt)
        {
            for (int i = m_Explosions.Count - 1; i >= 0; i--)
            {
                m_Explosions[i].Update(dt);
                if (m_Explosions[i].TTL < 0)
                    m_Explosions.RemoveAt(i);
            }

            for (int s = m_Singularities.Count - 1; s >= 0; s--)
            {
                m_Singularities[s].Update(dt);
                if (m_Singularities[s].TTL < 0)
                    m_Singularities.RemoveAt(s);
            }
        }

        public void Draw(SpriteBatch sb)
        {
            for (int i = m_Explosions.Count - 1; i >= 0; i--)
            {
                m_Explosions[i].Draw(sb);
            }
        }
    }
}
