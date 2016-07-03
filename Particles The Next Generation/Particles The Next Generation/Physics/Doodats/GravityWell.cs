using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Particles_The_Next_Generation
{
    public class GravityWell
    {
        protected Vector2 m_Position, m_Origin;
        protected float m_Strength;
        protected float m_MinimumRadius;
        protected Texture2D m_Sprite;
        protected float m_Width;
        protected bool m_MarkedForDeath;
        protected GravityWellSlider m_Slider;
        protected bool m_SliderActive;
        protected Dragging m_Dragging;

        public GravityWell(Vector2 position, float strength, float maxStrength, Texture2D sprite)
        {
            m_SliderActive = false;
            m_MarkedForDeath = false;
            m_Position = position;
            m_Strength = strength;
            m_Sprite = sprite;
            m_Origin = new Vector2(sprite.Width / 2, sprite.Height / 2);
            m_MinimumRadius = sprite.Width;
            m_Width = sprite.Width;
            m_Slider = new GravityWellSlider(position + new Vector2(50, 0),
                Utility.Load<Texture2D>("Menu/Items/plainselected"),
                Utility.Load<Texture2D>("Menu/Items/plaindeselected"),
                this, 0, maxStrength, strength);
            m_Dragging = new Dragging();
        }

        public bool MarkedForDeath
        {
            get { return this.m_MarkedForDeath; }
        }

        public float Strength
        {
            get { return this.m_Strength; }
            set { this.m_Strength = value; }
        }

        public virtual Vector2 GetForce(Vector2 position, float mass)
        {
            if (position == m_Position)
                return Vector2.Zero;

            Vector2 direction = m_Position - position;
            float distance = direction.Length();
            distance = MathHelper.Clamp(distance, m_MinimumRadius, int.MaxValue);
            float strength = (m_Strength * mass) / distance;
            direction.Normalize();

            return direction * strength;
        }

        public virtual bool Update(float dt, bool menuChange)
        {
            bool change = menuChange;

            m_Position = m_Dragging.Update(m_Position, m_Width);
            if (m_Dragging.IsDragging)
                change = true;

            if (!menuChange)
            {
                if (m_SliderActive)
                {
                    m_Slider.Position = m_Position + new Vector2(100, 0);
                    bool result = m_Slider.Update();
                    change = change || result;
                }

                if (Input.KeyPressed(Keys.Escape))
                    m_SliderActive = false;

                float distance = (m_Position - Input.MousePosition).Length();

                if (distance < m_Width)
                {
                    if (Input.KeyPressed(Keys.Delete))
                    {
                        m_MarkedForDeath = true;
                        return true;
                    }

                    if (Input.RMB_Clicked)
                    {
                        m_SliderActive = true;
                        change = true;
                    }
                }
            }

            return change;
        }

        public virtual void Draw(SpriteBatch sb)
        {
            sb.Draw(m_Sprite, m_Position - m_Origin, Color.White);

            if(m_SliderActive)
                m_Slider.Draw(sb);
        }
    }

    public struct BaseGravityWellProperties
    {
        public float strength, maxStrength;
        public Texture2D sprite;
        public Vector2 position;

        public BaseGravityWellProperties(Vector2 position, float strength, float maxstrength, Texture2D sprite)
        {
            this.position = position;
            this.sprite = sprite;
            this.strength = strength;
            this.maxStrength = maxstrength;
        }
    }
}
