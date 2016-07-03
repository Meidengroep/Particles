using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Particles_The_Next_Generation
{
    public class BackButton : MenuItem
    {
        //Menu m_ReturnMenu;

        public BackButton(Vector2 position, Texture2D selectedSprite, Texture2D deselectedSprite, object obj)
            : base(position, selectedSprite, deselectedSprite, obj)
        {
            //m_ReturnMenu = (Menu)obj;
        }

        public override bool Update()
        {
            bool change = base.Update();

            if (m_IsSelected && Input.LMB_Clicked)
            {
                Global.Game.Menu.Return();
                return true;
            }
            return change;
        }
    }
}
