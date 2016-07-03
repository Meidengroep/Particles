using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Particles_The_Next_Generation
{
    public abstract class MenuItem
    {
        protected Texture2D m_Sprite_Deselected, m_Sprite_Selected;
        protected bool m_IsSelected;
        protected Rectangle m_BoundingBox;
        protected Vector2 m_Position, m_Origin;

        public MenuItem(Vector2 position, Texture2D selectedSprite, Texture2D deselectedSprite, object obj)
        {
            this.m_Position = position;
            this.m_IsSelected = false;
            this.m_Sprite_Selected = selectedSprite;
            this.m_Sprite_Deselected = deselectedSprite;
            this.m_Origin = new Vector2(deselectedSprite.Width / 2, 0);
            this.m_BoundingBox = new Rectangle((int)(position.X - m_Origin.X), (int)(position.Y - m_Origin.Y), deselectedSprite.Width, deselectedSprite.Height);
        }

        protected virtual Rectangle GetBoundingBox()
        {
            return new Rectangle((int)(m_Position.X - m_Origin.X), (int)(m_Position.Y - m_Origin.Y), m_Sprite_Deselected.Width, m_Sprite_Deselected.Height);
        }

        public virtual bool Update()
        {
            this.m_BoundingBox = GetBoundingBox();

            if (m_BoundingBox.Contains(Input.MousePosition_Point))
                m_IsSelected = true;
            else m_IsSelected = false;

            return m_IsSelected;
        }

        public virtual void Draw(SpriteBatch sb)
        {
            if (m_IsSelected)
                sb.Draw(m_Sprite_Selected, m_Position - m_Origin, Color.White);
            else sb.Draw(m_Sprite_Deselected, m_Position - m_Origin, Color.White);
        }
    }
}
