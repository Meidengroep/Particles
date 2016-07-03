using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;


namespace Particles_The_Next_Generation
{
    public class MusicParticle : PhysicsParticle
    {
        public MusicParticle(Texture2D sprite, Vector2 position, float mass, float ttl,
            float scale, float collisionFraction,
            float bounceDamping, float groundDamping, float collisionDamping, float frictionDamping)
            : base(sprite, position, mass, ttl, scale, collisionFraction, bounceDamping, groundDamping, collisionDamping, frictionDamping, new Color(), new Color())
        {
        }

        public override void Draw(SpriteBatch sb)
        {
            Color c = new Color();

            c = MediaManager.CurrentColor;

            sb.Draw(m_Sprite, m_Position, null, c, m_Rotation, m_Origin, m_Scale, SpriteEffects.None, 0);
            //sb.Draw(m_Sprite, m_Position - m_Origin, c);
        }
    }
}
