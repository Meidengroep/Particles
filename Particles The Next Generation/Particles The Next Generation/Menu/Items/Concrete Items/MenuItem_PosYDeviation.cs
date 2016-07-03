using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Particles_The_Next_Generation
{
    class MenuItem_PosYDeviation : MenuItemBar
    {
        protected PParticleFactory m_System;

        public MenuItem_PosYDeviation(Vector2 position, Texture2D selectedSprite, Texture2D deselectedSprite, PParticleFactory obj, float min, float max, float current)
            : base(position, selectedSprite, deselectedSprite, obj, min, max, current)
        {
            this.m_System = obj;

            this.m_Marker = Utility.Load<Texture2D>("Menu/Items/PosDeviation/posdev_marker");

            InitMarker();
        }

        public override bool Update()
        {
            bool change = base.Update();

            m_System.YDeviation = m_CurrentMul;

            return change;
        }

        public override void Draw(SpriteBatch sb)
        {
            base.Draw(sb);
        }
    }
}
