using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Particles_The_Next_Generation
{
    class MenuItem_GravityStrengthText : MenuItemTextBox
    {
        protected Physics_System m_System;
        protected Checkbox m_Checkbox;

        public MenuItem_GravityStrengthText(Vector2 position, Texture2D selectedSprite, Texture2D deselectedSprite, Physics_System obj, float min, float max, float current)
            : base(position, selectedSprite, deselectedSprite, obj, min, max, current)
        {
            this.m_System = obj;

            this.m_Checkbox = new Checkbox(new Vector2(position.X, position.Y + deselectedSprite.Height),
                Utility.Load<Texture2D>("Menu/Items/Gravity/checkbox_selected"),
                Utility.Load<Texture2D>("Menu/Items/Gravity/checkbox_deselected"),
                true,
                "Gravity Enabled");
        }

        public override bool Update()
        {
            bool change = base.Update();

            change = change || m_Checkbox.Update();

            if (m_Checkbox.Checked)
            {
                if (m_CurrentMul != 0)
                    m_System.GravityStrength = m_CurrentMul;
            }
            else m_System.GravityStrength = 0;

            return change;
        }

        public override void Draw(SpriteBatch sb)
        {
            base.Draw(sb);
            m_Checkbox.Draw(sb);
        }
    }
}
