using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Particles_The_Next_Generation
{
    public class CollisionGridManager
    {
        protected int m_CollisionGridWidth, m_CollisionGridHeight;
        protected int m_CollisionGridCellSize;
        protected List<int>[,] m_CollisionGrid;

        protected bool m_CollisionsEnabled;

        public CollisionGridManager(int width, int height, int cellsize)
        {
            this.m_CollisionGridWidth = width;
            this.m_CollisionGridHeight = height;
            this.m_CollisionGridCellSize = cellsize;
        }

        public bool CollisionsEnabled
        {
            get { return this.m_CollisionsEnabled; }
            set { this.m_CollisionsEnabled = value; }
        }

        int collisionIterations = 0, currentIterations = 0;

        protected bool CheckCollision(IPhysicsParticle me, IPhysicsParticle other)
        {
            float distance = (me.Position - other.Position).Length();
            if (distance < me.CollisionRadius + other.CollisionRadius && distance > 0)
            {
                Vector2 x = me.Position - other.Position;

                Vector2 centerPosition = other.Position + x * (other.CollisionRadius / (me.CollisionRadius + other.CollisionRadius));

                x.Normalize();

                Vector2 myNewPosition = me.Position;// centerPosition - (me.CollisionRadius + 1) * x;
                Vector2 otherNewPosition = other.Position; //centerPosition + (other.CollisionRadius + 1) * x;


                // me
                Vector2 v1 = me.Velocity;
                float x1 = Utility.Dot(x, v1);

                Vector2 v1x = x * x1;
                Vector2 v1y = v1 - v1x;

                float m1 = me.Mass;
                //////

                // other
                x *= -1;

                Vector2 v2 = other.Velocity;
                float x2 = Utility.Dot(x, v2);

                Vector2 v2x = x * x2;
                Vector2 v2y = v2 - v2x;

                float m2 = other.Mass;
                //////

                Vector2 myNewVelocity = v1x * ((m1 - m2) / (m1 + m2)) + v2x * ((2 * m2) / (m1 + m2)) + v1y;
                Vector2 otherNewVelocity = v1x * ((2 * m1) / (m1 + m2)) + v2x * ((m2 - m1) / (m1 + m2)) + v2y;

                me.ApplyCollision(myNewVelocity, myNewPosition, false);
                other.ApplyCollision(otherNewVelocity, otherNewPosition, false);
                return true;
            }
            return false;
        }

        public void DoCollisionGridCollisions(IPhysicsParticle[] particles)
        {
            if (m_CollisionsEnabled)
            {
                for (int y = 0; y < m_CollisionGridHeight; y++)
                    for (int x = 0; x < m_CollisionGridWidth; x++)
                        if (m_CollisionGrid[x, y] != null && m_CollisionGrid[x, y].Count > 1)
                        {
                            List<int> reference = m_CollisionGrid[x, y];
                            int count = reference.Count;

                            for (int i = 0; i < count; i++)
                                for (int j = 0; j < count; j++)
                                {
                                    currentIterations++;

                                    if (CheckCollision(particles[reference[i]], particles[reference[j]]))
                                    {
                                        reference.RemoveAt(i);
                                        reference.RemoveAt(j - 1);
                                        i--;
                                        count -= 2;
                                        break;
                                    }
                                }
                        }
            }
        }

        public void AddToCollisiongrid(IPhysicsParticle reference, int index)
        {
            if (m_CollisionsEnabled)
            {
                int gridX = (int)reference.Position.X / m_CollisionGridCellSize;
                int gridY = (int)reference.Position.Y / m_CollisionGridCellSize;

                Vector2 cellCenter = new Vector2(this.m_CollisionGridWidth * gridX + 0.5f * m_CollisionGridCellSize,
                    this.m_CollisionGridHeight * gridY - 0.5f * m_CollisionGridCellSize);

                Vector2 distance = reference.Position - cellCenter;
                distance.Normalize();

                int xRounded;
                int yRounded;

                if (distance.X > 0)
                    xRounded = 1;
                else xRounded = -1;

                if (distance.Y > 0)
                    yRounded = 1;
                else yRounded = -1;

                AddToCollisionGrid_Aux(index, gridX, gridY);
                AddToCollisionGrid_Aux(index, gridX + xRounded, gridY);
                AddToCollisionGrid_Aux(index, gridX, gridY + yRounded);
                AddToCollisionGrid_Aux(index, gridX + xRounded, gridY + yRounded);
            }
        }

        protected void AddToCollisionGrid_Aux(int index, int x, int y)
        {
            if (x >= 0 && x < m_CollisionGridWidth
                &&
                y >= 0 && y < m_CollisionGridHeight)
            {
                if (m_CollisionGrid[x, y] == null)
                    m_CollisionGrid[x, y] = new List<int>();

                m_CollisionGrid[x, y].Add(index);
            }
        }

        public void ResetGrid()
        {
            if (m_CollisionsEnabled)
            {
                m_CollisionGrid = new List<int>[0, 0];

                m_CollisionGridWidth = (int)Math.Ceiling(Global.ScreenWidth / (float)m_CollisionGridCellSize);
                m_CollisionGridHeight = (int)Math.Ceiling(Global.ScreenHeight / (float)m_CollisionGridCellSize);
                m_CollisionGrid = new List<int>[m_CollisionGridWidth, m_CollisionGridHeight];
            }
            
        }

        public void Draw(SpriteBatch sb)
        {
            sb.DrawString(Global.StandardFont, "Collision Iterations " + collisionIterations + ", Cell size: " + m_CollisionGridCellSize, new Vector2(500, 70), Color.Green);
        }
    }
}
