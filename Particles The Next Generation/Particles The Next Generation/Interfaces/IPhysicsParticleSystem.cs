using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Particles_The_Next_Generation
{
    public interface IPhysicsParticleSystem
    {
        void GenerateNewParticles();
        void UpdateParticles(float dt, Vector2 position, bool takeInput);
        void DrawParticles(SpriteBatch sb);
    }
}
