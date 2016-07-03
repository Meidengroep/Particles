using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Particles_The_Next_Generation
{
    public class ColorManager_Beat
    {
        private Color m_IdleColor, m_BeatColor, m_CurrentColor;
        private float m_BeatTime, m_TimeSinceLastBeat;
        private float m_CurrentIntensity, m_MinIntensity, m_MaxIntensity, m_IntensityDifference;
        private bool m_Epilepsy;

        public ColorManager_Beat(Color idleColor, Color beatColor, float beatTime)
        {
            this.m_IdleColor = idleColor;
            this.m_BeatColor = beatColor;

            this.m_CurrentColor = idleColor;

            this.m_BeatTime = beatTime;
            this.m_TimeSinceLastBeat = beatTime;

            this.m_MaxIntensity = 1f;
            this.m_CurrentIntensity = m_MaxIntensity;
            this.m_MinIntensity = 0.2f;

            this.m_IntensityDifference = m_MaxIntensity - m_MinIntensity;

            this.m_Epilepsy = false;
        }

        public Color CurrentColor
        {
            get { return this.m_CurrentColor; }
        }

        public void Update(float dt, bool isBeat)
        {
            if (Input.KeyPressed(Keys.Z))
                m_Epilepsy = !m_Epilepsy;

            if (isBeat)
            {
                m_CurrentColor = m_BeatColor;
                m_TimeSinceLastBeat = 0;
                m_CurrentIntensity = 1;
            }
            else
            {
                m_TimeSinceLastBeat += dt;

                m_TimeSinceLastBeat = MathHelper.Clamp(m_TimeSinceLastBeat, 0, m_BeatTime);

                float amount = m_TimeSinceLastBeat / m_BeatTime;

                m_CurrentIntensity = m_MinIntensity + (1 - amount) * m_IntensityDifference;

                m_CurrentColor = Utility.Lerp(m_IdleColor, m_BeatColor, amount);
                m_CurrentColor = new Color((byte)(m_CurrentColor.R * m_CurrentIntensity), 
                                            (byte)(m_CurrentColor.G * m_CurrentIntensity),          
                                            (byte)(m_CurrentColor.B * m_CurrentIntensity), 
                                            (byte)(m_CurrentColor.A * m_CurrentIntensity));
            }
            
            if (m_Epilepsy)
                m_CurrentColor = m_IdleColor;
        }
    }
}
