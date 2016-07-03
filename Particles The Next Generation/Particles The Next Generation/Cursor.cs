using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Particles_The_Next_Generation
{

    public class Cursor
    {
        public Cursor(Texture2D sprite)
        {
            this.m_Sprite = sprite;
        }

        Texture2D m_Sprite;
        Vector2 m_Position;

        public void Update()
        {
            m_Position = new Vector2(Input.MousePosition.X - m_Sprite.Width / 2, Input.MousePosition.Y - m_Sprite.Height / 2);
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(m_Sprite, m_Position, Color.White);
        }
    }
}