using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Particles_The_Next_Generation
{
    public class Player : CollidableBox
    {
        protected float m_Velocity;
        protected Nozzle m_Nozzle;
        protected EarthShatter m_EarthShatter;

        public Player(Texture2D collisionSprite, Vector2 position, float velocity)
            : base(collisionSprite, position)
        {
            this.m_Position = position;
            this.m_Velocity = velocity;
            this.m_Nozzle = new Nozzle(45, 250, 5, 0.5f, 10);
            this.m_EarthShatter = new EarthShatter(400, 200, 10, 45, 35.5f, new Vector2(0, -1));
        }

        public override void Update(float dt) { }

        public void Update(float dt, ForceGrid grid)
        {
            Vector2 newPos = Position;
            if (Input.KeyDown(Keys.A))
            {
                newPos.X -= m_Velocity * dt;
            }
            if (Input.KeyDown(Keys.D))
            {
                newPos.X += m_Velocity * dt;
            }
            if (Input.KeyDown(Keys.W))
            {
                newPos.Y -= m_Velocity * dt;
            }
            if (Input.KeyDown(Keys.S))
            {
                newPos.Y += m_Velocity * dt;
            }

            #region Nozzle

            float angleToMouse = 0;

            Vector2 direction = Input.MousePosition - m_Position;

            angleToMouse = Utility.CCWAngle(direction);
            m_Nozzle.Angle = angleToMouse;


            if (Input.KeyDown(Keys.Space))
                m_Nozzle.Fire(dt, m_Position, grid);
            if (Input.KeyDown(Keys.E))
                m_EarthShatter.Fire(dt, grid, Input.MousePosition);

            #endregion

            Position = newPos;

            m_Position.X = MathHelper.Clamp(m_Position.X, m_Sprite.Width - m_Origin.X, Global.ScreenWidth - m_Sprite.Width + m_Origin.X);
            m_Position.Y = MathHelper.Clamp(m_Position.Y, m_Sprite.Height - m_Origin.Y, Global.ScreenHeight - m_Sprite.Height + m_Origin.Y);
        }

        public override void Draw(SpriteBatch sb)
        {
            sb.Draw(m_Sprite, m_Position - m_Origin, Color.White);
            StringDrawer.DrawLine(sb, StringAlignment.Center, "Angle: " + m_Nozzle.Angle, Color.Pink);
        }
    }
}
