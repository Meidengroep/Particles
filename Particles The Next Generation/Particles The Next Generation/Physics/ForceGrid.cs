using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Particles_The_Next_Generation
{
    public class ForceGrid
    {
        protected Vector2[,] m_Grid;
        protected int m_WidthDivider, m_HeightDivider;
        protected int m_GridWidth, m_GridHeigth;

        public ForceGrid(int width, int height, int widthDivider, int heightDivider)
        {
            m_WidthDivider = widthDivider;
            m_HeightDivider = heightDivider;

            m_GridWidth = (int)Math.Ceiling((float)width / m_WidthDivider);
            m_GridHeigth = (int)Math.Ceiling((float)height / m_HeightDivider);

            m_Grid = new Vector2[m_GridWidth, m_GridHeigth];
        }

        public void IncreaseForce(float x, float y, Vector2 force)
        {
            int xIndex = GetXIndex(x);
            int yIndex = GetYIndex(y);

            m_Grid[xIndex, yIndex] += force;
        }

        public Vector2 GetForce(float x, float y)
        {
            int xIndex = GetXIndex(x);
            int yIndex = GetYIndex(y);

            return m_Grid[xIndex, yIndex];
        }

        private int GetXIndex(float x)
        {
            return (int)MathHelper.Clamp((float)Math.Floor(x / m_WidthDivider), 0, m_GridWidth - 1);
        }

        private int GetYIndex(float y)
        {
            return (int)MathHelper.Clamp((float)Math.Floor(y / m_HeightDivider), 0, m_GridHeigth - 1);
        }

        public void ClearGrid()
        {
            for (int x = 0; x < m_GridWidth; x++)
                for (int y = 0; y < m_GridHeigth; y++)
                    m_Grid[x, y] = Vector2.Zero;
        }
    }
}
