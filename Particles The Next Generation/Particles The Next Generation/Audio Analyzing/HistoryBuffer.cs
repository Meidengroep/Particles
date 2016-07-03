using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Particles_The_Next_Generation
{
    public class HistoryBuffer
    {
        protected float[] m_Buffer;
        protected int m_CurrentIndex, m_Size;

        public HistoryBuffer(int size = 42)
        {
            m_CurrentIndex = 0;
            m_Size = size;

            m_Buffer = new float[size];
        }

        public int Size
        {
            get { return this.m_Size; }
        }

        public float[] Buffer
        {
            get { return this.m_Buffer; }
        }

        public void AddSample(float sample)
        {
            m_Buffer[m_CurrentIndex++] = sample;
            m_CurrentIndex %= m_Size;
        }
    }
}
