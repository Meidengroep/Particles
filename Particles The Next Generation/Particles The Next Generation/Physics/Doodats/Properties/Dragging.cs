using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace Particles_The_Next_Generation
{
    public class Dragging
    {
        protected bool m_IsDragging;
        protected int m_ID;

        public Dragging()
        {
            m_IsDragging = false;
            m_ID = DragManager.ObtainIndex();
        }

        public bool IsDragging
        {
            get { return this.m_IsDragging; }
        }

        private void CheckMouseClick()
        {
            if (Input.LMB_Pressed)
                TurnOnDragging();
            else if (!Input.LMB_Pressed)
                TurnOffDragging();
        }

        private void TurnOnDragging()
        {
            m_IsDragging = true;
            DragManager.NewDragger(m_ID);
        }

        private void TurnOffDragging()
        {
            m_IsDragging = false;
            DragManager.Undrag();
        }

        private Vector2 DoDragging(Vector2 position)
        {
            if (m_IsDragging)
            {
                return Input.MousePosition;
            }

            return position;
        }


        public Vector2 Update(Vector2 position, float width)
        {
            if (!DragManager.SomeoneDragging || DragManager.WhosDragging == m_ID)
            {
                float distance = (position - Input.MousePosition).Length();

                if (distance < width)
                {
                    CheckMouseClick();
                }
            }

            return DoDragging(position);
        }

        public Vector2 Update(Vector2 position, Rectangle boundingbox)
        {
            if (!DragManager.SomeoneDragging || DragManager.WhosDragging == m_ID)
            {
                if (boundingbox.Contains(Input.MousePosition_Point))
                {
                    CheckMouseClick();
                }
            }

            return DoDragging(position);
        }

    }
}
