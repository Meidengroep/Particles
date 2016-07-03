using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Particles_The_Next_Generation
{
    public class TextureSystem
    {
        protected int m_CurrentIndex, m_MaxIndex;
        protected int m_Width, m_Height;
        protected Texture2D m_Sprite;
        protected Vector2 m_Offset;

        protected Random r;

        protected Vector2[,] m_CurrentPositions, m_PreviousPositions;
        protected Vector2[,] m_CurrentVelocities, m_PreviousVelocities;
        protected Texture2D m_ScaleOrientationMass;
        protected Texture2D m_Visualiser;
        protected float[,] m_TimesOfBirth;

        public TextureSystem(Texture2D sprite, Vector2 offSet, int width, int height, GraphicsDevice device)
        {
            r = new Random(1337);

            m_Sprite = sprite;
            m_Offset = offSet;
            m_CurrentIndex = 0;
            m_MaxIndex = width * height - 1;
            m_Width = width;
            m_Height = height;

            m_CurrentPositions = new Vector2[width, height];
            m_PreviousPositions = new Vector2[width, height];
            m_CurrentVelocities = new Vector2[width, height];
            m_PreviousVelocities = new Vector2[width, height];
            m_ScaleOrientationMass = new Texture2D(device, width, height);
            m_TimesOfBirth = new float[width, height];
            m_Visualiser = new Texture2D(device, width, height);
        }

        protected void SpawnParticle(float currentTime, Vector2 position)
        {
            if (m_CurrentIndex > m_MaxIndex)
            {
                return;
                m_CurrentIndex = 0;
            }

            int y = m_CurrentIndex / m_Height;
            int x = m_CurrentIndex - y * m_Width;
            Color c = new Color(300, 300, 300);

            m_CurrentPositions[x, y] = position;

            Vector2 velocity = new Vector2(r.Next(-5, 5), r.Next(-5, 5));

            m_CurrentVelocities[x, y] = velocity;

            m_TimesOfBirth[x, y] = currentTime;

            m_CurrentIndex++;
        }

        public void Update(float dt, float currentTime, Vector2 position)
        {
            this.m_Offset = position;
            SpawnParticle(currentTime, position);

            for (int x = 0; x < m_Width; x++)
            {
                for (int y = 0; y < m_Height; y++)
                {
                    m_CurrentPositions[x,y] += m_CurrentVelocities[x,y];
                }
            }
        }

        public void Draw(SpriteBatch sb)
        {
            foreach (Vector2 position in m_CurrentPositions)
            {
                sb.Draw(m_Sprite, position, Color.White);
            }

            Color[] data = new Color[m_Width * m_Height];
            m_Visualiser.GetData<Color>(data);

            for(int x = 0; x < m_Width; x++)
                for (int y = 0; y < m_Height; y++)
                {

                }
        }
    }
}
