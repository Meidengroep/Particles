using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Particles_The_Next_Generation
{
    public class Equalizer
    {
        protected bool m_Enabled;
        protected Texture2D m_Sprite;
        protected int m_SelectedSample;

        public Equalizer(Texture2D sprite)
        {
            this.m_Enabled = false;
            this.m_Sprite = sprite;
            this.m_SelectedSample = -1;
        }

        public bool Enabled
        {
            get { return this.m_Enabled; }
            set { this.m_Enabled = value; }
        }

        public void Draw(SpriteBatch sb)
        {
            if (m_Enabled)
            {
                int yRange = 300;
                int beginX = 105;
                int beginY = 500;

                for (int i = 0; i < Global.Frequencies.Count(); i++)
                {                    
                    Vector2 pos = new Vector2(beginX + i * m_Sprite.Width, 0);

                    pos.Y = beginY - Global.Frequencies[i] * 100 * yRange;

                    sb.Draw(m_Sprite, pos, Color.White);
                }
            }
        }
    }
}
