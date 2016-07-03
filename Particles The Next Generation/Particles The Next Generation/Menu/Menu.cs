using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Particles_The_Next_Generation
{
    public class Menu
    {
        protected Texture2D m_Overlay;
        protected List<SubMenu> m_SubMenus;
        protected int m_CurrentSubMenu;
        protected Vector2 m_CenterX, m_OverlayOrigin;

        public Menu(Texture2D overlay)
        {
            this.m_CenterX = new Vector2(600, 100);
            this.m_CurrentSubMenu = -1;
            this.m_Overlay = overlay;
            this.m_OverlayOrigin = new Vector2(overlay.Width / 2, 0);
            m_SubMenus = new List<SubMenu>();
            m_SubMenus.Add(new SubMenu_Wind(m_CenterX + new Vector2(0, 50), m_CenterX));
            m_SubMenus.Add(new SubMenu_Gravity(m_CenterX + new Vector2(0, 150), m_CenterX));
            m_SubMenus.Add(new SubMenu_PosDeviation(m_CenterX + new Vector2(0, 250), m_CenterX));
        }

        public void Return()
        {
            this.m_CurrentSubMenu = -1;
        }

        public void Update()
        {
            if (m_CurrentSubMenu != -1)
                m_SubMenus[m_CurrentSubMenu].Update();
            else
            {
                for (int i = 0; i < m_SubMenus.Count; i++)
                {
                    if (m_SubMenus[i].BoundingBox.Contains(Input.MousePosition_Point))
                    {
                        m_SubMenus[i].IsSelected = true;
                        if (Input.LMB_Clicked)
                        {
                            m_CurrentSubMenu = i;
                            m_SubMenus[i].Transfering = true;
                        }
                    }
                    else m_SubMenus[i].IsSelected = false;
                }
            }
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(m_Overlay, m_CenterX - m_OverlayOrigin, Color.White);

            if (m_CurrentSubMenu == -1)
            {
                for (int i = 0; i < m_SubMenus.Count; i++)
                    m_SubMenus[i].DrawButton(sb);
            }
            else m_SubMenus[m_CurrentSubMenu].DrawMenu(sb);
        }
    }
}
