using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace Particles_The_Next_Generation
{
    public interface IParticle
    {
        float TTL { get; }
        void UpdateParameters(float dt, float ttlChange, byte opacityChange, float scaleChange, float rotationChange);
        void Update(float dt);
        void Draw(SpriteBatch sb);
    }
}
