using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Particles_The_Next_Generation
{
    public interface ICollidableObject
    {
        bool HandleCollision(IPhysicsParticle particle);
        void Update();
        void Draw(SpriteBatch sb);
    }
}
