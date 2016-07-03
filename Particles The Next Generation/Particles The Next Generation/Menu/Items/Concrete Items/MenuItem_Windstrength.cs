using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Particles_The_Next_Generation
{
    class MenuItem_Windstrength : MenuItemBar
    {
        protected Checkbox m_Checkbox;
        protected WindSimulator m_WindInput;

        public MenuItem_Windstrength(Vector2 position, Texture2D selectedSprite, Texture2D deselectedSprite, WindSimulator obj, float min, float max, float current)
            : base(position, selectedSprite, deselectedSprite, obj, min, max, current)
        {
            this.m_WindInput = obj;

            this.m_Marker = Utility.Load<Texture2D>("Menu/Items/Windinput/wind_marker");

            this.m_Checkbox = new Checkbox(new Vector2(position.X, position.Y + deselectedSprite.Height),
                Utility.Load<Texture2D>("Menu/Items/Windinput/checkbox_selected"),
                Utility.Load<Texture2D>("Menu/Items/Windinput/checkbox_deselected"),
                true,
                "Wind Enabled");

            InitMarker();
        }

        public override bool Update()
        {
            bool change = base.Update();

            change = change || m_Checkbox.Update();
            if (m_Checkbox.Checked)
            {
                m_WindInput.MenuMultiplier = m_CurrentMul;
            }
            else m_WindInput.MenuMultiplier = 0;

            m_WindInput.Enabled = m_Checkbox.Checked;

            return change;
        }

        public override void Draw(SpriteBatch sb)
        {
            base.Draw(sb);

            m_Checkbox.Draw(sb);
        }
    }
}
