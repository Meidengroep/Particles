using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Particles_The_Next_Generation
{
    public class Checkbox
    {
        protected bool m_Checked;
        protected Texture2D m_UncheckedSprite, m_CheckedSprite;
        protected Vector2 m_Position, m_Origin;
        protected Rectangle m_BoundingBox;
        protected string m_Text;

        public Checkbox(Vector2 position, Texture2D checkedSprite, Texture2D uncheckedSprite, bool isChecked, string text)
        {
            this.m_Position = position;
            this.m_Checked = isChecked;
            this.m_CheckedSprite = checkedSprite;
            this.m_UncheckedSprite = uncheckedSprite;
            this.m_Origin = new Vector2(m_UncheckedSprite.Width / 2, 0);
            this.m_Text = text;

            this.m_BoundingBox = new Rectangle((int)(this.m_Position.X - this.m_Origin.X), (int)(this.m_Position.Y - this.m_Origin.Y), uncheckedSprite.Width, uncheckedSprite.Height);
        }

        public bool Checked
        {
            get { return this.m_Checked; }
            set { this.m_Checked = value; }
        }

        public Vector2 Position
        {
            get { return this.m_Position; }
            set { this.m_Position = value;
            this.m_BoundingBox = new Rectangle((int)(this.m_Position.X - this.m_Origin.X), (int)(this.m_Position.Y - this.m_Origin.Y), m_UncheckedSprite.Width, m_CheckedSprite.Height);
            }
        }

        public bool Update()
        {
            if (Input.LMB_Clicked && m_BoundingBox.Contains(Input.MousePosition_Point))
            {
                m_Checked = !m_Checked;
                return true;
            }
            return false;
        }

        public void Draw(SpriteBatch sb)
        {
            if (m_Checked)
                sb.Draw(m_CheckedSprite, m_Position - m_Origin, Color.White);
            else
                sb.Draw(m_UncheckedSprite, m_Position - m_Origin, Color.White);

            sb.DrawString(Global.StandardFont, m_Text, m_Position + m_Origin, Color.DeepSkyBlue);
        }
    }
}
