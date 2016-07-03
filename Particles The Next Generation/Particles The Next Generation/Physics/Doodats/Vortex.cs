using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Particles_The_Next_Generation
{
    public class Vortex : GravityWell
    {
        protected float m_MaximumRadius;
        protected float m_BaseAngle, m_DegreesInwards, m_UsedAngle;
        protected float m_Rotation;
        bool m_ClockWise;
        Checkbox m_Checkbox;

        public Vortex(Vector2 position, float strength, float maxStrength, Texture2D sprite, bool clockwise, float degreesInwards, float minRadius, float maxRadius)
            : base(position, strength, maxStrength, sprite)
        {
            this.m_ClockWise = clockwise;
            if (clockwise)
            {
                this.m_BaseAngle = 90;
                this.m_UsedAngle = m_BaseAngle + degreesInwards;
            }
            else 
            {   
                this.m_BaseAngle = -90;
                this.m_UsedAngle = m_BaseAngle - degreesInwards;
            }

            this.m_DegreesInwards = degreesInwards;
            this.m_MaximumRadius = maxRadius;
            this.m_MinimumRadius = minRadius;

            this.m_Rotation = 0;

            this.m_Checkbox = new Checkbox(new Vector2(600, 360), Utility.Load<Texture2D>("Menu/Items/Gravity/checkbox_selected"), Utility.Load<Texture2D>("Menu/Items/Gravity/checkbox_deselected"), true, "Clockwise");
        }

        public override Vector2 GetForce(Vector2 position, float mass)
        {
            if (position == m_Position)
                return Vector2.Zero;

            Vector2 direction = position - m_Position;
            float distance = direction.Length();

            if (distance < m_MinimumRadius || distance > m_MaximumRadius)
                return Vector2.Zero;

            direction = Utility.RotateVector_Degrees(direction, m_UsedAngle);

            float strength = (m_Strength * mass) * Utility.PercentualDistance(distance, m_MinimumRadius, m_MaximumRadius);
            direction.Normalize();

            return direction * m_Strength;

        }

        public override bool Update(float dt, bool menuChange)
        {
            bool change = menuChange;

            m_Checkbox.Position = this.m_Position + new Vector2(0, 50);

            if(!menuChange)
                change = change || m_Checkbox.Update();

            if (m_Checkbox.Checked)
            {
                m_ClockWise = true;
                m_BaseAngle = 90;
                m_UsedAngle = m_BaseAngle + m_DegreesInwards;
            }
            else
            {
                m_ClockWise = false;
                m_BaseAngle = -90;
                m_UsedAngle = m_BaseAngle - m_DegreesInwards;
            }

            change = change || base.Update(dt, change);

            if (m_ClockWise)
                m_Rotation += MathHelper.ToRadians(dt * 300);
            else m_Rotation -= MathHelper.ToRadians(dt * 300);

            m_Rotation %= 2 * (float)Math.PI;

            return change;
        }

        public override void Draw(SpriteBatch sb)
        {
            sb.Draw(m_Sprite, m_Position, null, Color.White, m_Rotation, m_Origin, 1, SpriteEffects.None, 0);
            if (m_SliderActive)
            {
                m_Slider.Draw(sb);
                m_Checkbox.Draw(sb);
            }
        }
    }

    public struct BaseVortexProperties
    {
        public float strength, maxStrength, degreesInwards, minRadius, maxRadius;
        public bool clockwise;
        public Texture2D sprite;
        public Vector2 position;

        public BaseVortexProperties(Vector2 position, float strength, float maxstrength, bool clockwise, float degreesinwards, float minradius, float maxradius, Texture2D sprite)
        {
            this.position = position;
            this.sprite = sprite;
            this.strength = strength;
            this.maxStrength = maxstrength;
            this.clockwise = clockwise;
            this.degreesInwards = degreesinwards;
            this.minRadius = minradius;
            this.maxRadius = maxradius;
        }
    }
}
