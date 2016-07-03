using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Particles_The_Next_Generation
{
    public class TextBox
    {
        protected Vector2 m_Position;
        protected Rectangle m_Boundingbox;
        protected bool m_IsSelected;
        protected string m_CurrentText;
        protected Texture2D m_Pixel;
        protected List<Keys> m_PressedKeys;

        public TextBox(Vector2 position, int width, int height, string text)
        {
            this.m_Position = position;
            this.m_Boundingbox = new Rectangle((int)m_Position.X, (int)m_Position.Y, width, height);
            this.m_CurrentText = text;
            this.m_Pixel = Utility.Load<Texture2D>("Menu/Items/pixel");

            this.m_IsSelected = false;
            this.m_PressedKeys = new List<Keys>();
        }

        public string Text
        {
            get { return this.m_CurrentText; }
            set { this.m_CurrentText = value; }
        }

        public Rectangle BoundingBox
        {
            get { return this.m_Boundingbox; }
        }

        public void Update()
        {
            if (Input.LMB_Clicked)
            {
                if (m_Boundingbox.Contains(Input.MousePosition_Point))
                    m_IsSelected = true;
                else m_IsSelected = false;
            }


            if (Input.KeyPressed(Keys.Escape))
                m_IsSelected = false;

            List<Keys> stillPressed = new List<Keys>();

            if (m_IsSelected)
            {
                Keys[] keys = Input.KeyboardState.GetPressedKeys();
                foreach (Keys key in keys)
                {
                    if (!m_PressedKeys.Contains(key))
                    {
                        m_PressedKeys.Add(key);
                        string charString = ValidInput(key);
                        if (charString != "")
                        {
                            switch (charString)
                            {
                                case "comma":
                                    m_CurrentText += ',';
                                    break;
                                case "backspace":
                                    if (m_CurrentText.Length > 0)
                                        m_CurrentText = m_CurrentText.Substring(0, m_CurrentText.Length - 1);
                                    break;
                                default :
                                    m_CurrentText += charString;
                                    break;
                            }
                        }
                    }
                    stillPressed.Add(key);
                }
            }
            else
                m_PressedKeys.Clear();

            for (int i = 0; i < m_PressedKeys.Count; i++)
            {
                if (!stillPressed.Contains(m_PressedKeys[i]))
                {
                    m_PressedKeys.RemoveAt(i);
                    i--;
                }
            }
        }

        private string ValidInput(Keys key)
        {
            string charString = key.ToString();
            if (charString.Substring(0, 1) == "D")
                return charString = charString.Substring(1);
            else if (charString == "OemComma")
                return "comma";
            else if (charString == "Back")
            {
                return "backspace";
            }

            return "";
        }

        public void Draw(SpriteBatch sb)
        {
            sb.End();
            sb.Begin();
            if (m_IsSelected)
                sb.Draw(m_Pixel, m_Boundingbox, Color.Green);
            else sb.Draw(m_Pixel, m_Boundingbox, Color.White);
            sb.DrawString(Global.StandardFont, m_CurrentText, m_Position, Color.Red);
            sb.End();
            sb.Begin(SpriteSortMode.Texture, BlendState.Additive);
        }
    }
}
