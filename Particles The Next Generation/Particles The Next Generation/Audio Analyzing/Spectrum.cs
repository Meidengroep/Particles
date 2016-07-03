using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Particles_The_Next_Generation
{
    public class Spectrum
    {
        protected float[] m_LeftSpectrum, m_RightSpectrum;
        protected int m_SpectrumSize;

        public Spectrum(int spectrumSize)
        {
            this.m_SpectrumSize = spectrumSize;

            m_LeftSpectrum = new float[spectrumSize];
            m_RightSpectrum = new float[spectrumSize];
        }

        public float[] LeftSpectrum
        {
            get { return this.m_LeftSpectrum; }
        }

        public int SpectrumSize
        {
            get { return this.m_SpectrumSize; }
        }

        public float[] RightSpectrum
        {
            get { return this.m_RightSpectrum; }
        }

        public void ChangeSpectrumSize(int newSize)
        {
            m_LeftSpectrum = new float[newSize];
            m_RightSpectrum = new float[newSize];
        }
    }
}
