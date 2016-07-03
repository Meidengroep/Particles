using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Particles_The_Next_Generation
{
    public class WindSimulator
    {
        protected Texture2D m_Display;
        protected Texture2D m_Marker;
        protected int m_Radius, m_MarkerSize;
        protected Vector2 m_Position, m_MarkerPosition, m_RandomizePosition;
        protected Vector2 m_Origin, m_MarkerOrigin;
        protected Vector2 m_Direction;
        protected Vector2 m_DirectionRaw;
        protected float m_Multiplier, m_MenuMultiplier;
        protected bool m_Randomize;
        protected Random r;
        protected Vector2 m_RandomisedOffset, m_MaxOffset;
        protected bool m_Enabled;

        public WindSimulator(int radius, Vector2 position)
        {
            this.m_Enabled = false;

            this.m_Multiplier = 0;
            this.m_MenuMultiplier = 1;

            r = new Random();
            m_Randomize = true;
            m_MaxOffset = new Vector2(radius / 4, radius / 4);

            m_Position = position;
            m_RandomizePosition = position;
            m_RandomisedOffset = Vector2.Zero;
            m_MarkerPosition = position;

            m_MarkerSize = 5;
            m_Radius = radius;

            m_Origin = new Vector2(radius, radius);
            m_MarkerOrigin = new Vector2(m_MarkerSize / 2, m_MarkerSize / 2);

            m_Display = new Texture2D(Global.Game.GraphicsDevice, (int)(radius * 2), (int)(radius * 2));
            CreateCircle();
            CreateMarker();
        }

        #region Properties
        public bool Enabled
        {
            get { return this.m_Enabled; }
            set { this.m_Enabled = value; }
        }

        public Vector2 Direction
        {
            get { return m_Direction; }
        }

        public float Multiplier
        {
            get { return m_Multiplier * m_MenuMultiplier; }
        }

        public float MenuMultiplier
        {
            get { return this.m_MenuMultiplier; }
            set { this.m_MenuMultiplier = value; }
        }

        public bool Randomize
        {
            get { return this.m_Randomize; }
            set { this.m_Randomize = value; }
        }
        #endregion

        #region Create Shapes
        protected void CreateCircle()
        {
            Color[] data = new Color[m_Display.Width * m_Display.Height];

            for (float par = 0; par < 2 * Math.PI; par += 0.01f)
            {
                float x = (float)Math.Sin(par);
                float y = (float)Math.Cos(par);

                Vector2 pixelPosition = new Vector2(x, y);
                pixelPosition *= m_Radius;
                pixelPosition += m_Origin;

                int arrayPosition = (int)pixelPosition.X + (int)pixelPosition.Y * m_Display.Width;

                if (arrayPosition < data.Count() && arrayPosition >= 0)
                    data[arrayPosition] = Color.Red;
            }

            m_Display.SetData<Color>(data);
        }

        protected void CreateMarker()
        {
            m_Marker = new Texture2D(Global.Game.GraphicsDevice, 5, 5);

            Color[] data = new Color[m_MarkerSize * m_MarkerSize];

            for (int x = 0; x < m_MarkerSize; x++)
                for (int y = 0; y < m_MarkerSize; y++)
                {
                    int arrayPosition = x + y * m_MarkerSize;
                    data[arrayPosition] = Color.Cyan;
                }

            m_Marker.SetData<Color>(data);
        }
        #endregion

        protected void UpdatePosition(Vector2 offset)
        {
            m_Direction = m_MarkerPosition - m_Position + offset;
            m_Direction.Normalize();
            m_DirectionRaw = m_MarkerPosition - m_Position;
            m_Multiplier = (m_MarkerPosition - m_Position).Length();
        }

        public void Update(bool takeInput)
        {
            if (m_Enabled)
            {
                if (m_Randomize)
                {
                    m_RandomisedOffset.X += 10 * (float)(r.NextDouble() - r.NextDouble());
                    m_RandomisedOffset.Y += 10 * (float)(r.NextDouble() - r.NextDouble());

                    m_RandomisedOffset.X = MathHelper.Clamp(m_RandomisedOffset.X, -m_MaxOffset.X, m_MaxOffset.X);
                    m_RandomisedOffset.Y = MathHelper.Clamp(m_RandomisedOffset.Y, -m_MaxOffset.Y, m_MaxOffset.Y);

                }
                if (takeInput && Input.LMB_Pressed && (Input.MousePosition - m_Position).Length() <= m_Radius)
                {
                    m_MarkerPosition = Input.MousePosition;
                }
                UpdatePosition(m_RandomisedOffset);
            }
        }

        public void Draw(SpriteBatch sb)
        {
            if (m_Enabled)
            {
                sb.Draw(m_Display, m_Position - m_Origin, Color.White);
                sb.Draw(m_Marker, m_MarkerPosition - m_MarkerOrigin, Color.White);
                sb.DrawString(Global.StandardFont, "Offset: " + m_RandomisedOffset.X + ", " + m_RandomisedOffset.Y, m_Position - new Vector2(0, 25) - m_Origin, Color.Red);
                sb.DrawString(Global.StandardFont, "Multiplier: " + m_MenuMultiplier * m_Multiplier, m_Position + new Vector2(0, 25 + m_Radius * 2) - m_Origin, Color.Red);
            }
        }
    }
}
