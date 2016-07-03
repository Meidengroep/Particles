using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Particles_The_Next_Generation
{
    public struct BaseParticleProperties
    {
        public float Mass, TTL, Scale, CollisionFraction, BounceDamping, GroundDamping, CollisionDamping, FrictionDamping;

        public BaseParticleProperties(float mass, float ttl,
            float scale, float collisionFraction,
            float bounceDamping, float groundDamping, float collisionDamping, float frictionDamping)
        {
            this.Mass = mass;
            this.TTL = ttl;
            this.Scale = scale;
            this.CollisionFraction = collisionFraction;
            this.BounceDamping = bounceDamping;
            this.GroundDamping = groundDamping;
            this.CollisionDamping = collisionDamping;
            this.FrictionDamping = frictionDamping;
        }
    }
}
