using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Particles_The_Next_Generation
{
    public class EarthShatter
    {
        protected int m_Width, m_Height, m_Spacing;
        protected float m_SpreadArc;
        protected float m_Strength;
        protected Vector2 m_Up;
        Random r;

        public EarthShatter(int width, int heigth, int spacing, float spreadArc, float strength, Vector2 up)
        {
            this.m_Width = width / 2;
            this.m_Height = heigth / 2;
            this.m_Spacing = spacing;
            this.m_SpreadArc = MathHelper.ToRadians(spreadArc / 2);
            this.m_Strength = strength;
            this.m_Up = up;
            r = new Random();
        }

        public void Fire(float dt, ForceGrid grid, Vector2 position)
        {
            for (int x = -m_Width; x <= m_Width; x += m_Spacing)
                for (int y = -m_Height; y <= m_Height; y += m_Spacing)
                {
                    float randomAngle = Utility.NextFloat(r) * m_SpreadArc;
                    Vector2 direction = Utility.RotateVector_Radians(m_Up, randomAngle);

                    grid.IncreaseForce(position.X + x, position.Y + y, direction * m_Strength * dt);
                }
        }
    }
}
