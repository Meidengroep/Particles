using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Particles_The_Next_Generation
{
    public class GravityWellSlider : MenuItemBar
    {
        protected GravityWell m_Well;

        public GravityWellSlider(Vector2 position, Texture2D selectedSprite, Texture2D deselectedSprite, GravityWell obj, float min, float max, float current)
            : base(position, selectedSprite, deselectedSprite, obj, min, max, current)
        {
            this.m_Well = obj;

            this.m_Marker = Utility.Load<Texture2D>("Menu/Items/PosDeviation/posdev_marker");

            InitMarker();
        }

        public Vector2 Position
        {
            get { return this.m_Position; }
            set
            {
                this.m_Position = value;
                m_Slidebar.NewPos(m_Position.X - m_Origin.X, m_Position.X + m_Origin.X);
                m_MarkerPosition.X = m_Slidebar.MarkerX;
            }
        }

        public override bool Update()
        {
            bool result = base.Update();

            m_Well.Strength = m_Slidebar.CurrentValue;

            return result;
        }

        public override void Draw(SpriteBatch sb)
        {
            base.Draw(sb);
        }
    }
}
