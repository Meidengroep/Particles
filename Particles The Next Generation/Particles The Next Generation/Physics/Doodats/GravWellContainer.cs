using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Particles_The_Next_Generation
{
    public class GravWellContainer
    {
        protected BaseGravityWellProperties m_BaseGravityWellProperties;
        protected BaseVortexProperties m_BaseVortexProperties;

        protected List<GravityWell> m_GravityWells;
        protected List<Vortex> m_Vortexes;

        public GravWellContainer(BaseGravityWellProperties wellprops, BaseVortexProperties vortexprops)
        {
            this.m_GravityWells = new List<GravityWell>();
            this.m_Vortexes = new List<Vortex>();
            this.m_BaseGravityWellProperties = wellprops;
            this.m_BaseVortexProperties = vortexprops;
        }

        public void AddGravWell(Vector2 position)
        {
            m_GravityWells.Add(new GravityWell(
                position,
                m_BaseGravityWellProperties.strength,
                m_BaseGravityWellProperties.maxStrength,
                m_BaseGravityWellProperties.sprite));
        }

        public void AddVortex(Vector2 position)
        {
            m_Vortexes.Add(new Vortex(
                position,
                m_BaseVortexProperties.strength,
                m_BaseVortexProperties.maxStrength,
                m_BaseVortexProperties.sprite,
                m_BaseVortexProperties.clockwise,
                m_BaseVortexProperties.degreesInwards,
                m_BaseVortexProperties.minRadius,
                m_BaseVortexProperties.maxRadius));
        }

        public Vector2 GetGravityWellForce(Vector2 position, float mass)
        {
            Vector2 force = Vector2.Zero;

            foreach (GravityWell well in m_GravityWells)
            {
                force += well.GetForce(position, mass);
            }

            return force;
        }

        public Vector2 GetVortexForce(Vector2 position, float mass)
        {
            Vector2 force = Vector2.Zero;

            foreach (Vortex vortex in m_Vortexes)
            {
                force += vortex.GetForce(position, mass);
            }

            return force;
        }

        protected bool HandleInput(float dt, bool takeInput)
        {
            bool change = false;

            if (takeInput)
            {
                for (int i = m_GravityWells.Count - 1; i >= 0; i--)
                {
                    change = m_GravityWells[i].Update(dt, change);
                    if (m_GravityWells[i].MarkedForDeath)
                    {
                        m_GravityWells.RemoveAt(i);
                        continue;
                    }
                }

                for (int i = m_Vortexes.Count - 1; i >= 0; i--)
                {
                    change = m_Vortexes[i].Update(dt, change);
                    if (m_Vortexes[i].MarkedForDeath)
                    {
                        m_Vortexes.RemoveAt(i);
                        continue;
                    }
                }
            }

            return change;
        }

        public bool Update(float dt, bool takeInput)
        {
            return HandleInput(dt, takeInput);
        }

        public void Draw(SpriteBatch sb)
        {
            foreach (GravityWell well in m_GravityWells)
            {
                well.Draw(sb);
            }

            foreach (Vortex vortex in m_Vortexes)
            {
                vortex.Draw(sb);
            }

        }
    }
}
