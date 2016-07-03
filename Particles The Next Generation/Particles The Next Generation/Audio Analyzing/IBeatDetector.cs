using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Particles_The_Next_Generation
{
    interface IBeatDetector
    {
        void DoDetection(Spectrum spectrum);
        void ResetBuffer(int newSize);
        bool BeatLastIteration();
    }
}
