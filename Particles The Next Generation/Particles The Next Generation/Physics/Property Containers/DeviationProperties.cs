using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Particles_The_Next_Generation
{
    public struct DeviationProperties
    {
        public float MassDeviation, TTLDeviation, PositionXDeviation, PositionYDeviation, ScaleDeviation;

        public DeviationProperties(float massDeviation, float ttlDeviation, float positionXDeviation, float positionYDeviation, float scaleDeviation)
        {
            this.MassDeviation = massDeviation;
            this.TTLDeviation = ttlDeviation;
            this.PositionXDeviation = positionXDeviation;
            this.PositionYDeviation = positionYDeviation;
            this.ScaleDeviation = scaleDeviation;
        }
    }
}
