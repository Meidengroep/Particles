using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Particles_The_Next_Generation
{
    public abstract class MenuItemBar : MenuItem
    {
        protected float m_MinMul, m_MaxMul, m_CurrentMul, m_Width;
        protected Texture2D m_Marker;
        protected Vector2 m_MarkerPosition, m_MarkerOrigin;
        protected Slidebar m_Slidebar;

        public MenuItemBar(Vector2 position, Texture2D selectedSprite, Texture2D deselectedSprite, object obj, float min, float max, float current)
            :base (position, selectedSprite, deselectedSprite, obj)
        {
            m_MinMul = min;
            m_MaxMul = max;
            m_CurrentMul = current;
            m_Width = selectedSprite.Width;

            m_Slidebar = new Slidebar(min, max, current, position.X - m_Origin.X, position.X + m_Origin.X);
        }

        protected void InitMarker()
        {
            m_MarkerPosition = m_Position - m_Origin;
            m_MarkerOrigin = new Vector2(m_Marker.Width / 2, 0);
            m_MarkerPosition.X = m_Slidebar.MarkerX;
        }

        public override bool Update()
        {
            bool resutl1 = base.Update();

            if (m_IsSelected)
            {
                m_Slidebar.Update();

                m_MarkerPosition.X = m_Slidebar.MarkerX;
                m_CurrentMul = m_Slidebar.CurrentValue;
            }

            m_MarkerPosition.Y = m_Position.Y;

            return resutl1;
        }

        public override void Draw(SpriteBatch sb)
        {
            base.Draw(sb);

            sb.DrawString(Global.StandardFont, "Value (" + m_MinMul + " - " + m_MaxMul + "): " + m_CurrentMul, new Vector2(m_Position.X - m_Origin.X, m_Position.Y - 30), Color.Green);
            sb.Draw(m_Marker, m_MarkerPosition - m_MarkerOrigin, Color.White);
        }
    }
}
