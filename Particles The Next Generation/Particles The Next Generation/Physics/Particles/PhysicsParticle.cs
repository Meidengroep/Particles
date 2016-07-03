using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;


namespace Particles_The_Next_Generation
{
    public class PhysicsParticle : IPhysicsParticle
    {
        protected Vector2 m_Position;
        protected Vector2 m_Velocity;
        protected Vector2 m_Origin;
        protected float m_Mass;
        protected float m_BounceDamping, m_GroundDamping, m_CollisionDamping, m_FrictionDamping, m_FrictionDampingComplement;
        protected float m_CollisionRadius;
        protected float m_TTL, m_FullTTL;
        protected float m_Scale;
        protected bool m_Destroy;
        protected Color m_StartColor, m_EndColor;

        protected float m_Rotation;

        protected Texture2D m_Sprite;

        public PhysicsParticle(Texture2D sprite, Vector2 position, float mass, float ttl, 
            float scale, float collisionFraction, 
            float bounceDamping, float groundDamping, float collisionDamping, float frictionDamping,
            Color startColor, Color endColor)
        {
            this.m_StartColor = startColor;
            this.m_EndColor = endColor;

            this.m_Scale = Math.Abs(scale);
            this.m_Destroy = false;
            this.m_TTL = ttl;
            this.m_FullTTL = ttl;
            this.m_Sprite = sprite;
            this.m_Origin = new Vector2(sprite.Width / 2, sprite.Height / 2);// *m_Scale;
            this.m_Position = position;
            this.m_Mass = mass;
            this.m_BounceDamping = bounceDamping;
            this.m_GroundDamping = groundDamping;
            this.m_CollisionDamping = collisionDamping;
            this.m_FrictionDamping = frictionDamping;
            this.m_FrictionDampingComplement = 1 - m_FrictionDamping;
            this.m_CollisionRadius = (m_Sprite.Width / collisionFraction) * m_Scale;
        }

        public Vector2 Position
        {
            get { return this.m_Position; }
        }

        public Vector2 Velocity
        {
            get { return this.m_Velocity; }
        }

        public float Mass
        {
            get { return this.m_Mass; }
        }

        public float CollisionRadius
        {
            get { return this.m_CollisionRadius; }
        }

        public bool Destroy
        {
            get { return this.m_Destroy; }
        }

        public void ApplyForce(Vector2 force)
        {
            Vector2 acceleration = Vector2.Zero;

            acceleration = force / Mass;

            m_Velocity += acceleration;
        }

        public void ApplyCollision(Vector2 newVelocity, Vector2 newPosition, bool damping)
        {
            m_Velocity = newVelocity;
            m_Position = newPosition;

            if (damping)
                m_Velocity *= m_CollisionDamping;
        }

        public void Update(float dt)
        {
            m_TTL -= dt;
            if (m_TTL < 0)
            {
                m_Destroy = true;
                return;
            }

            m_Rotation += MathHelper.ToRadians(dt * m_Velocity.Length());
            m_Rotation %= 2 * (float)Math.PI;

            float length = m_Velocity.Length();

            if (length > PhysicsProperties.MaxPhysicsVelocity)
            {
                m_Velocity.Normalize();
                m_Velocity *= PhysicsProperties.MaxPhysicsVelocity;
            }

            float frictionMod = dt * 1000f / 16f;
            m_Velocity *= 1 - (m_FrictionDampingComplement * (frictionMod));

            m_Position += m_Velocity * PhysicsProperties.VelocityMultiplier;

            if (m_Position.X + m_Origin.X > Global.ScreenWidth)
            {
                m_Velocity.X *= -1;
                m_Velocity *= m_BounceDamping;
                m_Position.X = Global.ScreenWidth - m_Origin.X;
            }
            else if (m_Position.X < 0)
            {
                m_Velocity.X *= -1;
                m_Velocity *= m_BounceDamping;
                m_Position.X = 0;
            }
            if (m_Position.Y + m_Origin.Y > Global.ScreenHeight)
            {
                m_Velocity.Y *= -1;
                m_Velocity *= m_GroundDamping;
                m_Position.Y = Global.ScreenHeight - m_Origin.Y;
            }
            else if (m_Position.Y < 0)
            {
                m_Velocity.Y *= -1;
                m_Velocity *= m_GroundDamping;
                m_Position.Y = 0;
            }
        }

        public virtual void Draw(SpriteBatch sb)
        {
            Color c = new Color();
            float fraction = (m_TTL / m_FullTTL);
            float complement = 1 - fraction;

            //complement = Position.Y / Global.ScreenHeight;

            c = Color.Lerp(m_StartColor, m_EndColor, complement);
            //c = Color.Red;
            c.A = (byte)(fraction * 255);

            sb.Draw(m_Sprite, m_Position , null, c, m_Rotation, m_Origin, m_Scale, SpriteEffects.None, 0);
            //sb.Draw(m_Sprite, m_Position - m_Origin, c);
        }
    }
}
