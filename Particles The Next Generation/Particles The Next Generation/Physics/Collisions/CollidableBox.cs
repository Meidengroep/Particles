using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Particles_The_Next_Generation
{
    public class CollidableBox : ICollidableObject
    {
        protected Rectangle m_BoundingBox;
        protected Vector2 m_Position, m_Origin;
        protected Dragging m_Dragging;

        protected Texture2D m_Sprite;

        public CollidableBox(Texture2D sprite, Vector2 position)
        {
            m_Sprite = sprite;
            m_Origin = new Vector2(sprite.Width / 2, sprite.Height / 2);
            Position = position;
            m_Dragging = new Dragging();
        }

        public Vector2 Position
        {
            get { return this.m_Position; }
            set
            {
                this.m_Position = value;
                m_BoundingBox = new Rectangle((int)(m_Position.X - m_Origin.X), (int)(m_Position.Y - m_Origin.Y), m_Sprite.Width, m_Sprite.Height);
            }
        }

        public bool HandleCollision(IPhysicsParticle particle)
        {
            // Save the bounding box of the particle
            float particleRight = particle.Position.X + particle.CollisionRadius;
            float particleLeft = particle.Position.X - particle.CollisionRadius;
            float particleTop = particle.Position.Y + particle.CollisionRadius;
            float particleBottom = particle.Position.Y - particle.CollisionRadius;

            // Check if the particle collides with the box
            if (particleRight > m_BoundingBox.Left 
                && particleLeft < m_BoundingBox.Right
                && particleTop > m_BoundingBox.Top 
                && particleBottom < m_BoundingBox.Bottom)
            {
                // Find how far the particle is from the center of the box.
                Vector2 direction = particle.Position - m_Position;

                float newX = particle.Position.X, newY = particle.Position.Y;
                float xDistance, yDistance;
                bool isLeft = false, isTop = false;

                // Check if the particle is to the right or left of the center, and calculate the distance to the side of the box accordingly.
                if (direction.X > 0)
                {
                    xDistance = (float)Math.Abs(m_BoundingBox.Right - particle.Position.X);
                }
                else
                {
                    xDistance = (float)Math.Abs(m_BoundingBox.Left - particle.Position.X);
                    isLeft = true;
                }
                
                // Do the same for the top and bottom.
                if (direction.Y > 0)
                {
                    yDistance = (float)Math.Abs(m_BoundingBox.Bottom - particle.Position.Y);
                }
                else
                {
                    yDistance = (float)Math.Abs(m_BoundingBox.Top - particle.Position.Y);
                    isTop = true;
                }

                Vector2 newVelocity = particle.Velocity;
                Vector2 newPosition = particle.Position;

                if (xDistance < yDistance)
                {
                    // It probably collided with the side
                    newVelocity.X *= -1;
                    if (isLeft)
                        newPosition.X = m_BoundingBox.Left - particle.CollisionRadius - 1;
                    else newPosition.X = m_BoundingBox.Right + particle.CollisionRadius + 1;
                }
                else
                {
                    // It probably collided with the top/bottom
                    newVelocity.Y *= -1;
                    if (isTop)
                        newPosition.Y = m_BoundingBox.Top - particle.CollisionRadius - 1;
                    else newPosition.Y = m_BoundingBox.Bottom + particle.CollisionRadius + 1;
                }

                particle.ApplyCollision(newVelocity, newPosition, true);
                return true;
            }
            return false;
        }

        public virtual void Update(float dt)
        {
            Position = m_Dragging.Update(m_Position, m_BoundingBox);
        }

        public virtual void Draw(SpriteBatch sb)
        {
            sb.Draw(m_Sprite, m_Position - m_Origin, Color.White);
        }
    }
}
