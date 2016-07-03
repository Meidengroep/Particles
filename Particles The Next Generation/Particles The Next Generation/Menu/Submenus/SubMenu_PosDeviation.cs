using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Particles_The_Next_Generation
{
    public class SubMenu_PosDeviation : SubMenu
    {
        public SubMenu_PosDeviation(Vector2 position, Vector2 center)
            : base(position, center)
        {
            this.m_ButtonDeselected = Utility.Load<Texture2D>("Menu/SubMenus/PosDeviation/posdev_deselected");
            this.m_ButtonSelected = Utility.Load<Texture2D>("Menu/SubMenus/PosDeviation/posdev_selected");
            this.m_TitleTexture = Utility.Load<Texture2D>("Menu/SubMenus/PosDeviation/posdev_title");

            Initialize(position);

            MenuItem_PosXDeviation posXItem = new MenuItem_PosXDeviation(new Vector2(center.X, center.Y + 100),
                Utility.Load<Texture2D>("Menu/Items/PosDeviation/posdev_selected"),
                Utility.Load<Texture2D>("Menu/Items/PosDeviation/posdev_deselected"),
                Global.Game.PhysicsSystem.ParticleFactory,
                0, 
                600,
                Global.Game.PhysicsSystem.ParticleFactory.XDeviation);

            m_Items.Add(posXItem);

            MenuItem_PosYDeviation posYItem = new MenuItem_PosYDeviation(new Vector2(center.X, center.Y + 230),
                Utility.Load<Texture2D>("Menu/Items/PosDeviation/posdev_selected"),
                Utility.Load<Texture2D>("Menu/Items/PosDeviation/posdev_deselected"),
                Global.Game.PhysicsSystem.ParticleFactory,
                0,
                360,
                Global.Game.PhysicsSystem.ParticleFactory.YDeviation);

            m_Items.Add(posYItem);

            BackButton backButton = new BackButton(new Vector2(center.X, center.Y + 330),
                Utility.Load<Texture2D>("Menu/Items/BackButton/back_selected"),
                Utility.Load<Texture2D>("Menu/Items/BackButton/back_deselected"),
                Global.Game.Menu);

            m_Items.Add(backButton);
        }
    }
}
