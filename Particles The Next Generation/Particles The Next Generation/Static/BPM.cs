using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Particles_The_Next_Generation
{
    public class BPM
    {
        private float m_BPM;
        private int m_NrOfBeats;
        private bool[] m_History;
        private int m_CurrentHistoryIndex, m_MaxHistoryIndex;
        private float m_TimeFrame;

        public BPM(int historyCount, float timestep)
        {
            m_TimeFrame = (historyCount * timestep) / 60000;
            m_BPM = 0;
            m_NrOfBeats = 0;

            m_CurrentHistoryIndex = 0;
            m_MaxHistoryIndex = historyCount;
            m_History = new bool[historyCount];
        }

        public void Reset()
        {
            m_BPM = 0;
        }

        public void Update(bool isBeat)
        {
            if (isBeat)
                m_NrOfBeats++;

            if (m_History[m_CurrentHistoryIndex])
                m_NrOfBeats--;

            m_History[m_CurrentHistoryIndex] = isBeat;
            m_CurrentHistoryIndex = (m_CurrentHistoryIndex + 1) % m_MaxHistoryIndex;

            m_BPM = m_NrOfBeats / m_TimeFrame;

            //PhysicsProperties.VelocityMultiplier = m_BPM / 1000f;
        }

        public void Draw(SpriteBatch sb)
        {
            if (Global.ShowInfo)
            {
                StringDrawer.DrawLine(sb, StringAlignment.Center, "Nr of Beats:  " + m_NrOfBeats.ToString(), Color.White);
                StringDrawer.DrawLine(sb, StringAlignment.Center, "BPM: " + m_BPM.ToString(), Color.White);
                StringDrawer.DrawLine(sb, StringAlignment.Center, "BPS: " + (m_BPM / 60).ToString(), Color.White);
                StringDrawer.DrawLine(sb, StringAlignment.Center, "CurrentIndex: " + m_CurrentHistoryIndex.ToString(), Color.White);
            }
        }
    }
}
