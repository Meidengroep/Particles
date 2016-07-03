using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Particles_The_Next_Generation
{
    public class FrequencyBandDetector : IBeatDetector
    {
        private HistoryBuffer[] m_Buffers;
        private float[] m_DegressionValues;
        private int m_NumberOfBuffers;
        private int m_MinBeatIndex, m_MaxBeatIndex;
        private bool m_BeatLastIteration;

        public FrequencyBandDetector(int numberOfBuffers, int minBeatIndex, int maxBeatIndex)
        {
            this.m_NumberOfBuffers = numberOfBuffers;
            this.m_MinBeatIndex = minBeatIndex;
            this.m_MaxBeatIndex = maxBeatIndex;
            Global.Beats = new bool[m_NumberOfBuffers];
            Global.BeatYs = new float[m_NumberOfBuffers];
            m_Buffers = new HistoryBuffer[numberOfBuffers];
            m_DegressionValues = new float[2];
            m_DegressionValues[0] = -0.0025714f;
            m_DegressionValues[1] = 1.5132857f;

            m_BeatLastIteration = false;
        }

        public bool BeatLastIteration()
        {
            return this.m_BeatLastIteration;
        }

        public void ResetBuffer(int newSize)
        {
            for (int i = 0; i < m_Buffers.Count(); i++)
            {
                m_Buffers[i] = new HistoryBuffer(newSize);
            }
        }

        public void DoDetection(Spectrum spectrum)
        {
            int bufferWidth = (int)Math.Floor((float)spectrum.SpectrumSize / m_NumberOfBuffers);
            for (int i = 0; i < m_NumberOfBuffers; i++)
            {
                    SingleBandDetection(spectrum, i * bufferWidth, (i + 1) * bufferWidth - 1, i);
            }
        }

        private void SingleBandDetection(Spectrum spectrum, int startIndex, int endIndex, int bufferIndex)
        {
            HistoryBuffer buffer = m_Buffers[bufferIndex];

            // Step 1: Compute instant energy
            float instantEnergy = 0;

            for (int i = startIndex; i < endIndex; i++)
            {
                instantEnergy += spectrum.LeftSpectrum[i] * spectrum.LeftSpectrum[i];
                instantEnergy += spectrum.RightSpectrum[i] * spectrum.RightSpectrum[i];
            }
             
            // Step 2: Compute the average over the history buffer.
            float bufferEnergy = 0;

            for (int i = 0; i < buffer.Size; i++)
            {
                bufferEnergy += buffer.Buffer[i];
            }

            float averageEnergy = bufferEnergy / buffer.Size;

            // Step 4: Compute the variance of the buffer.
            float varianceTemp = 0;

            for (int i = 0; i < buffer.Size; i++)
                varianceTemp += (float)Math.Pow(buffer.Buffer[i] - averageEnergy, 2);

            float variance = varianceTemp / buffer.Size;

            // Step 5: Calculate the constant to detect a beat.
            float constant = m_DegressionValues[0] * variance + m_DegressionValues[1];

            // Step 6: Check for the beat.
            if (bufferIndex >= m_MinBeatIndex && bufferIndex <= m_MaxBeatIndex && instantEnergy > constant * averageEnergy)
            {
                if (!m_BeatLastIteration)
                {
                    MediaManager.BeatQueue = true;
                    Global.Beats[bufferIndex] = true;
                    m_BeatLastIteration = true;
                    Console.Write("I");
                }
            }
            else
                m_BeatLastIteration = false;


            Global.Frequencies[bufferIndex] = instantEnergy;
            buffer.AddSample(instantEnergy);
        }
    }
}
