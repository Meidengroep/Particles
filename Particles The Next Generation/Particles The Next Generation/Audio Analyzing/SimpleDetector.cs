using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Particles_The_Next_Generation
{
    public class SimpleDetector : IBeatDetector
    {
        private HistoryBuffer m_Buffer;
        private float[] m_DegressionValues;
        private bool m_BeatLastIteration;

        public SimpleDetector()
        {
            m_Buffer = new HistoryBuffer();
            m_DegressionValues = new float[2];
            m_DegressionValues[0] = -0.0025714f;
            m_DegressionValues[1] = 1.5132857f;
            m_BeatLastIteration = false;
        }

        public bool BeatLastIteration()
        {
            return m_BeatLastIteration;
        }

        public void ResetBuffer(int newSize)
        {
            m_Buffer = new HistoryBuffer(newSize);
        }

        public void DoDetection(Spectrum spectrum)
        {
            // Step 1: Compute instant energy
            float instantEnergy = 0;

            for (int i = 0; i < spectrum.SpectrumSize; i++)
            {
                instantEnergy += spectrum.LeftSpectrum[i] * spectrum.LeftSpectrum[i];
                instantEnergy += spectrum.RightSpectrum[i] * spectrum.RightSpectrum[i];
            }

            // Step 2: Compute the average over the history buffer.
            float bufferEnergy = 0;

            for (int i = 0; i < m_Buffer.Size; i++)
            {
                bufferEnergy += m_Buffer.Buffer[i];
            }

            float averageEnergy = bufferEnergy / m_Buffer.Size;

            // Step 4: Compute the variance of the buffer.
            float varianceTemp = 0;

            for (int i = 0; i < m_Buffer.Size; i++)
                varianceTemp += (float)Math.Pow(m_Buffer.Buffer[i] - averageEnergy, 2);

            float variance = varianceTemp / m_Buffer.Size;

            // Step 5: Calculate the constant to detect a beat.
            float constant = m_DegressionValues[0] * variance + m_DegressionValues[1];

            // Step 6: Check for the beat.
            if (instantEnergy > constant * averageEnergy)
            {
                MediaManager.BeatQueue = true;
                m_BeatLastIteration = true;
            }
            else m_BeatLastIteration = false;

            m_Buffer.AddSample(instantEnergy);
        }
    }
}
