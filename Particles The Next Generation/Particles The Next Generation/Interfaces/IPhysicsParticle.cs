using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Particles_The_Next_Generation
{
    public interface IPhysicsParticle
    {
        void ApplyForce(Vector2 force);
        Vector2 Position { get; }
        Vector2 Velocity { get; }
        float Mass { get; }
        float CollisionRadius { get; }
        bool Destroy { get; }
        void ApplyCollision(Vector2 newVelocity, Vector2 newPosition, bool damping);
        void Update(float dt);
        void Draw(SpriteBatch sb);
    }
}
