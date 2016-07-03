using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Particles_The_Next_Generation
{
    public class Slidebar
    {
        protected float m_Min, m_Max, m_Current;
        protected float m_XMin, m_XMax, m_XDistance;
        protected float m_MarkerXCoordinate;

        public Slidebar(float min, float max, float current, float xMin, float xMax)
        {
            this.m_Min = min;
            this.m_Max = max;
            this.m_Current = current; 
            this.m_XMin = xMin;
            this.m_XMax = xMax;
            this.m_XDistance = xMax - xMin;
            this.m_MarkerXCoordinate = xMin + (current / max) * m_XDistance;
        }

        public float CurrentValue
        {
            get { return m_Current; }
        }

       

        public float MarkerX
        {
            get { return m_MarkerXCoordinate; }
        }

        public void NewPos(float xMin, float xMax)
        {
            this.m_XMin = xMin;
            this.m_XMax = xMax;
            this.m_XDistance = xMax - xMin;
            this.m_MarkerXCoordinate = xMin + (m_Current / m_Max) * m_XDistance;
        }

        public void Update()
        {            
            if (Input.LMB_Pressed)
            {
                float distance = Input.MousePosition.X - m_XMin;
                m_Current = (distance / m_XDistance) * (m_Max - m_Min);

                m_MarkerXCoordinate = m_XMin + distance;
            }
        }
    }
}
