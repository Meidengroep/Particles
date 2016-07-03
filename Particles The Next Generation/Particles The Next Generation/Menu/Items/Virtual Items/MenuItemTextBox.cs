using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Particles_The_Next_Generation
{
    public abstract class MenuItemTextBox : MenuItem
    {
        protected float m_MinMul, m_MaxMul, m_CurrentMul;
        protected TextBox m_TextBox;

        public MenuItemTextBox(Vector2 position, Texture2D selectedSprite, Texture2D deselectedSprite, object obj, float min, float max, float current)
            : base(position, selectedSprite, deselectedSprite, obj)
        {
            m_MinMul = min;
            m_MaxMul = max;
            m_CurrentMul = current;
            
            string currentValue = current.ToString();
            Vector2 measurements = Global.StandardFont.MeasureString(currentValue);
            m_TextBox = new TextBox(position - m_Origin, (int)deselectedSprite.Width, (int)deselectedSprite.Height, currentValue);
        }

        protected override Rectangle GetBoundingBox()
        {
            return m_TextBox.BoundingBox;
        }

        public override bool Update()
        {
            bool resutl1 = base.Update();

            m_TextBox.Update();
            if (Input.KeyPressed(Keys.Enter))
            {
                float value = 0;
                try
                {
                    value = float.Parse(m_TextBox.Text, NumberStyles.Any);
                    if (value >= m_MinMul && value <= m_MaxMul)
                        m_CurrentMul = value;
                    else m_TextBox.Text = m_CurrentMul.ToString();
                }
                catch
                {
                    m_TextBox.Text = m_CurrentMul.ToString();
                }
            }

            return resutl1;
        }

        public override void Draw(SpriteBatch sb)
        {
            base.Draw(sb);

            sb.DrawString(Global.StandardFont, "Value (" + m_MinMul + " - " + m_MaxMul + "): " + m_CurrentMul, new Vector2(m_Position.X - m_Origin.X, m_Position.Y - 30), Color.Green);
            m_TextBox.Draw(sb);
        }
    }
}
