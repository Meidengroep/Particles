using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Particles_The_Next_Generation
{
    public class CollidableObjectManager
    {
        protected List<ICollidableObject> m_Objects;
        protected List<Player> m_Players;

        public CollidableObjectManager()
        {
            m_Objects = new List<ICollidableObject>();
            m_Players = new List<Player>();

            m_Objects.Add(new CollidableBox(
                Utility.Load<Texture2D>("Doodats/smallbox"),
                new Vector2(900, 200))
                );
            m_Objects.Add(new CollidableBox(
                Utility.Load<Texture2D>("Doodats/smallbox"),
                new Vector2(900, 500))
                );
            m_Objects.Add(new CollidableBox(
                Utility.Load<Texture2D>("Doodats/smallbox"),
                new Vector2(900, 250))
                );
            m_Objects.Add(new CollidableBox(
                Utility.Load<Texture2D>("Doodats/smallbox"),
                new Vector2(900, 360))
                );
        }

        public void AddObject(ICollidableObject obj)
        {
            m_Objects.Add(obj);
        }

        public void AddPlayer(Player playa)
        {
            m_Players.Add(playa);
        }

        public void DoCollisions(IPhysicsParticle particle)
        {
            for (int i = 0; i < m_Objects.Count; i++)
            {
                m_Objects[i].HandleCollision(particle);
            }
            for (int i = 0; i < m_Players.Count; i++)
            {
                m_Players[i].HandleCollision(particle);
            }
        }

        public void Update(float dt)
        {
            for (int i = 0; i < m_Objects.Count; i++)
            {
                m_Objects[i].Update(dt);
            }
        }

        public void Draw(SpriteBatch sb)
        {
            for (int i = 0; i < m_Objects.Count; i++)
            {
                m_Objects[i].Draw(sb);
            }
        }
    }
}
