using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Particles_The_Next_Generation
{
    public class SubMenu_Gravity : SubMenu
    {
        public SubMenu_Gravity(Vector2 position, Vector2 center)
            : base(position, center)
        {
            this.m_ButtonDeselected = Utility.Load<Texture2D>("Menu/SubMenus/Gravity/gravity_deselected");
            this.m_ButtonSelected = Utility.Load<Texture2D>("Menu/SubMenus/Gravity/gravity_selected");
            this.m_TitleTexture = Utility.Load<Texture2D>("Menu/SubMenus/Gravity/gravity_title");

            Initialize(position);

            MenuItem_GravityStrengthText strengthItem = new MenuItem_GravityStrengthText(new Vector2(center.X, center.Y + 100),
                Utility.Load<Texture2D>("Menu/Items/Gravity/gravitystrength_selected"),
                Utility.Load<Texture2D>("Menu/Items/Gravity/gravitystrength_deselected"),
                Global.Game.PhysicsSystem,
                0,
                100,
                Global.Game.PhysicsSystem.GravityStrength);

            m_Items.Add(strengthItem);
       
            BackButton backButton = new BackButton(new Vector2(center.X, center.Y + 330),
                Utility.Load<Texture2D>("Menu/Items/BackButton/back_selected"),
                Utility.Load<Texture2D>("Menu/Items/BackButton/back_deselected"),
                Global.Game.Menu);

            m_Items.Add(backButton);
        }
    }
}
