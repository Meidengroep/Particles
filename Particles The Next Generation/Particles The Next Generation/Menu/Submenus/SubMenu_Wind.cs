using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Particles_The_Next_Generation
{
    public class SubMenu_Wind : SubMenu
    {
        public SubMenu_Wind(Vector2 position, Vector2 center)
            : base(position, center)
        {
            this.m_ButtonDeselected = Utility.Load<Texture2D>("Menu/SubMenus/Wind/wind_deselected");
            this.m_ButtonSelected = Utility.Load<Texture2D>("Menu/SubMenus/Wind/wind_selected");
            this.m_TitleTexture = Utility.Load<Texture2D>("Menu/SubMenus/Wind/wind_title");

            Initialize(position);

            MenuItem_Windstrength strengthItem = new MenuItem_Windstrength(new Vector2(center.X, center.Y + 100),
                Utility.Load<Texture2D>("Menu/Items/WindInput/wind_selected"),
                Utility.Load<Texture2D>("Menu/Items/WindInput/wind_deselected"),
                Global.Game.WindInput,
                0, 
                1000,
                Global.Game.WindInput.MenuMultiplier);

            m_Items.Add(strengthItem);

            BackButton backButton = new BackButton(new Vector2(center.X, center.Y + 300),
                Utility.Load<Texture2D>("Menu/Items/BackButton/back_selected"),
                Utility.Load<Texture2D>("Menu/Items/BackButton/back_deselected"),
                Global.Game.Menu);

            m_Items.Add(backButton);
        }
    }
}
