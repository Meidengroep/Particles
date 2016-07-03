using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Particles_The_Next_Generation
{
    public class Nozzle
    {
        protected float m_CurrentAngle;
        protected float m_SpreadArc;
        protected float m_Length;
        protected float m_StartStrength, m_EndStrength, m_StrengthDifference;
        protected int m_NrOfSprays;
        protected Random r;
        protected Vector2 m_ZeroAngle;

        public Nozzle(float spreadArc, float length, float startStrength, float endStrength, int nrOfSprays)
        {
            this.m_CurrentAngle = 0f;
            this.m_SpreadArc = MathHelper.ToRadians(spreadArc / 2);
            this.m_Length = length;
            this.m_StartStrength = startStrength;
            this.m_EndStrength = endStrength;
            this.m_StrengthDifference = m_StartStrength - m_EndStrength;
            this.m_NrOfSprays = nrOfSprays;
            this.r = new Random();
            this.m_ZeroAngle = new Vector2(1, 0);
        }

        public float Angle
        {
            get { return this.m_CurrentAngle; }
            set { this.m_CurrentAngle = value; }
        }

        public void Fire(float dt, Vector2 position, ForceGrid grid)
        {
            for (int i = 0; i < m_NrOfSprays; i++)
            {
                float randomAngle = m_CurrentAngle + Utility.NextFloat(r) * m_SpreadArc;
                Vector2 direction = Utility.RotateVector_Radians(m_ZeroAngle, randomAngle);

                direction.Normalize();

                Vector2 directionVariable;

                for (int j = 0; j < m_Length; j++)
                {
                    directionVariable = direction * j;
                    grid.IncreaseForce(position.X + directionVariable.X, position.Y + directionVariable.Y, (m_EndStrength + m_StrengthDifference * (j / m_Length)) * direction * dt);
                }
            }
        }
    }
}
