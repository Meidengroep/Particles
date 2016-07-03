using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Particles_The_Next_Generation
{
    public abstract class SubMenu
    {
        protected List<MenuItem> m_Items;
        protected Texture2D m_ButtonSelected, m_ButtonDeselected;
        protected Vector2 m_ButtonPosition, m_ButtonOrigin, m_CenterX, m_TitleOrigin;
        protected Rectangle m_BoundingBox;
        protected bool m_IsSelected, m_JustTransfered;
        protected Texture2D m_TitleTexture;

        public SubMenu(Vector2 position, Vector2 centerX)
        {
            this.m_ButtonPosition = position;
            this.m_CenterX = centerX;
            this.m_JustTransfered = false;
        }

        protected void Initialize(Vector2 position)
        {
            this.m_Items = new List<MenuItem>();
            this.m_TitleOrigin = new Vector2(m_TitleTexture.Width / 2, 0);
            this.m_ButtonOrigin = new Vector2(m_ButtonSelected.Width / 2, 0);

            this.m_BoundingBox = new Rectangle((int)(m_ButtonPosition.X - m_ButtonOrigin.X), (int)(m_ButtonPosition.Y- m_ButtonOrigin.Y), m_ButtonSelected.Width, m_ButtonSelected.Height);
        }

        public Rectangle BoundingBox
        {
            get { return m_BoundingBox; }
        }

        public bool IsSelected
        {
            get { return this.m_IsSelected; }
            set { m_IsSelected = value; }
        }

        public bool Transfering
        {
            set { this.m_JustTransfered = true; }
        }

        public void Update()
        {
            if (!m_JustTransfered)
            {
                for (int i = 0; i < m_Items.Count; i++)
                    m_Items[i].Update();
            }
            else m_JustTransfered = Input.LMB_Pressed;
        }

        public void DrawButton(SpriteBatch sb)
        {
            if (m_IsSelected)
                sb.Draw(m_ButtonSelected, m_ButtonPosition - m_ButtonOrigin, Color.White);
            else
                sb.Draw(m_ButtonDeselected, m_ButtonPosition - m_ButtonOrigin, Color.White);
        }

        public void DrawMenu(SpriteBatch sb)
        {
            sb.Draw(m_TitleTexture, m_CenterX - m_TitleOrigin, Color.White);

            for (int i = 0; i < m_Items.Count; i++)
                m_Items[i].Draw(sb);
        }
    }
}
