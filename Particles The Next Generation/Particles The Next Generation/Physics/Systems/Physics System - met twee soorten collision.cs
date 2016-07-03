using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using System.Globalization;
using Microsoft.Xna.Framework.Input;

namespace Particles_The_Next_Generation
{
    public class Physics_System : IPhysicsParticleSystem
    {
        #region members

        protected Vector2 m_Position;
        protected IPhysicsParticle[] m_Particles;
        protected Texture2D[] m_Sprites;
        protected Random r;
        protected int m_SpriteCount, m_MaxParticles;

        protected bool autoGeneration, gridCollision;

        protected BaseParticleProperties m_BaseParticleProperties;
        protected DeviationProperties m_DeviationProperties;
        protected Vector2 m_GravityDirection, m_Gravity;
        protected float m_GravityStrength;
        protected float m_MouseForceMultiplier, m_MouseRadius;

        protected GravWellContainer m_GravWellManager;

        protected Stack<int> m_Indexes;

        protected float m_Timer, m_SpawnDelay, m_SpawnDelayInc, m_ReceivedDT;

        protected int m_CurrentCollisionChunk, m_MaxCollisionChunk, m_CollisionChunkSize;
        protected int m_CollisionGridWidth, m_CollisionGridHeight;
        protected int m_CollisionGridCellSize;
        protected List<int>[,] collisionGrid;

        #endregion

        public Physics_System(Vector2 position, Texture2D[] sprites)
        {
            this.m_BaseParticleProperties = ReadBaseParticleProperties("physics1");
            this.m_DeviationProperties = ReadSystemProperties("physics1");

            this.m_CollisionGridCellSize = (int)Math.Ceiling((m_BaseParticleProperties.Scale + m_DeviationProperties.ScaleDeviation) * sprites[0].Width);

            this.m_CollisionGridWidth = (int)Math.Ceiling(Global.ScreenWidth / (float)m_CollisionGridCellSize); 
            this.m_CollisionGridHeight = (int)Math.Ceiling(Global.ScreenHeight / (float)m_CollisionGridCellSize);

            this.m_ReceivedDT = 0;
            this.m_Timer = 0;

            this.m_CurrentCollisionChunk = 0;

            this.m_CollisionChunkSize = (int)Math.Floor((float)this.m_MaxParticles / m_MaxCollisionChunk);

            this.m_Indexes = new Stack<int>(m_MaxParticles);
            for (int i = m_MaxParticles - 1; i >= 0; i--)
                m_Indexes.Push(i);

            this.m_Particles = new IPhysicsParticle[m_MaxParticles];
            this.m_Sprites = sprites;
            this.m_Position = position;
            this.m_Sprites = sprites;
            this.m_SpriteCount = m_Sprites.Count();

            this.r = new Random();
        }

        #region Properties

        public float XDeviation
        {
            get { return this.m_DeviationProperties.PositionXDeviation; }
            set { this.m_DeviationProperties.PositionXDeviation = value; }
        }

        public float YDeviation
        {
            get { return this.m_DeviationProperties.PositionYDeviation; }
            set { this.m_DeviationProperties.PositionYDeviation = value; }
        }

        #endregion

        #region Generating shit
        public IPhysicsParticle GenerateSingleParticle()
        {
            int doubleup = r.Next(m_SpriteCount);
            Texture2D sprite = m_Sprites[r.Next(m_SpriteCount)];
            float mass = m_BaseParticleProperties.Mass + m_DeviationProperties.MassDeviation * NextDouble();
            float ttl = m_BaseParticleProperties.TTL + m_DeviationProperties.TTLDeviation * NextDouble();
            Vector2 position = m_Position;
            position.X += m_DeviationProperties.PositionXDeviation * NextDouble();
            position.Y += m_DeviationProperties.PositionYDeviation * NextDouble();

            float scale = m_BaseParticleProperties.Scale + m_DeviationProperties.ScaleDeviation * NextDouble();

            Color startColor = new Color(), endColor = new Color();

            startColor.R = (byte)r.Next(0, 256);
            startColor.G = (byte)r.Next(0, 256);
            startColor.B = (byte)r.Next(0, 256);
            startColor.A = 255;

            endColor.R = (byte)r.Next(0, 256);
            endColor.G = (byte)r.Next(0, 256);
            endColor.B = (byte)r.Next(0, 256);
            endColor.A = 255;

            //startColor = Color.Red;
            //endColor = Color.Blue;

            return new PhysicsParticle(sprite, position, mass, ttl, scale, 
                m_BaseParticleProperties.CollisionFraction,
                m_BaseParticleProperties.BounceDamping,
                m_BaseParticleProperties.GroundDamping,
                m_BaseParticleProperties.CollisionDamping,
                m_BaseParticleProperties.FrictionDamping,
                startColor, endColor);
        }

        protected float NextDouble()
        {
            return (float)(r.NextDouble() - r.NextDouble());
        }

        public void GenerateNewParticles()
        {
            int numberOfSpawns = (int)Math.Floor(m_Timer / m_SpawnDelay);
            m_Timer -= numberOfSpawns * m_SpawnDelay;

            for (int i = 0; i < numberOfSpawns; i++)
                if (m_Indexes.Count > 0)
                {
                    m_Particles[m_Indexes.Pop()] = GenerateSingleParticle();
                }
                else break;
        }
        #endregion

        protected void HandleInput(bool takeInput, float dt)
        {
            if (takeInput)
            {
                if (Input.KeyPressed(Keys.C))
                { Clear(); }

                if (Input.LMB_Clicked)
                {
                    m_Timer += dt;
                    GenerateNewParticles();
                    //GenerateDebugParticles();
                }

                if (Input.KeyPressed(Keys.W))
                {
                    m_GravWellManager.AddGravWell(Input.MousePosition);
                }

                if (Input.KeyPressed(Keys.V))
                {
                    m_GravWellManager.AddVortex(Input.MousePosition);
                }

                if (Input.KeyPressed(Keys.Right))
                {
                    m_MaxParticles += 200;
                    Clear();
                }

                if (Input.KeyPressed(Keys.Left))
                {
                    m_MaxParticles = Math.Max(0, m_MaxParticles - 200);
                    Clear();
                }

                if (Input.KeyPressed(Keys.Down))
                {
                    m_SpawnDelay = Math.Max(0, m_SpawnDelay - m_SpawnDelayInc);
                }
                
                if (Input.KeyPressed(Keys.Up))
                {
                    m_SpawnDelay += m_SpawnDelayInc;
                }
            }

            if (Input.KeyPressed(Keys.Enter))
            {
                autoGeneration = !autoGeneration;
            }
        }

        protected Vector2 CalculateForces(IPhysicsParticle particle, float dt)
        {
            Vector2 force = Vector2.Zero;

            float distance = (Input.MousePosition - particle.Position).Length();

            if (distance < m_MouseRadius && distance != 0)
                force += Input.MouseVelocity * m_MouseForceMultiplier * (m_MouseRadius / distance);

            force += Global.WindDirection * Global.WindStrength / particle.Mass;
            force += m_GravWellManager.GetGravityWellForce(particle.Position, particle.Mass);
            force += m_GravWellManager.GetVortexForce(particle.Position, particle.Mass);

            force *= dt;

            return force;
        }

        #region Collision Methods

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

                me.ApplyCollision(myNewVelocity, myNewPosition);
                other.ApplyCollision(otherNewVelocity, otherNewPosition);
                return true;
            }
            return false;
        }

        protected void AddToCollisiongrid(IPhysicsParticle reference, int index)
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

        protected void AddToCollisionGrid_Aux(int index, int x, int y)
        {
            if (x >= 0 && x < m_CollisionGridWidth
                &&
                y >= 0 && y < m_CollisionGridHeight)
            {
                if (collisionGrid[x, y] == null)
                    collisionGrid[x, y] = new List<int>();

                collisionGrid[x, y].Add(index);
            }
        }

        protected void NormalColllision(IPhysicsParticle reference, int index)
        {
            for (int other = m_CurrentCollisionChunk * m_CollisionChunkSize;
                other < (m_CurrentCollisionChunk + 1) * m_CollisionChunkSize;
                other++)
            {
                IPhysicsParticle referenceOther = m_Particles[other];
                if (other != index && referenceOther != null && CheckCollision(reference, m_Particles[other]))
                    break;
                currentIterations++;
            }
        }

        protected void DoCollisionGridCollisions()
        {
            for (int y = 0; y < m_CollisionGridHeight; y++)
                for (int x = 0; x < m_CollisionGridWidth; x++)
                    if (collisionGrid[x, y] != null && collisionGrid[x, y].Count > 1)
                    {
                        List<int> reference = collisionGrid[x, y];
                        int count = reference.Count;

                        for (int i = 0; i < count; i++)
                            for (int j = 0; j < count; j++)
                            {
                                currentIterations++;

                                if (CheckCollision(m_Particles[reference[i]], m_Particles[reference[j]]))
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

        #endregion

        public void UpdateParticles(float dt, Vector2 position, bool takeInput)
        {
            m_GravWellManager.Update(dt, takeInput);

            #region Input and generation
            m_ReceivedDT = dt;
            m_Position = position;

            HandleInput(takeInput, dt);

            if (autoGeneration)
            {
                m_Timer += dt;
                GenerateNewParticles();
                //GenerateDebugParticles();
            }
            #endregion

            #region Init Collision Grid
            collisionGrid = new List<int>[0,0];

            if (gridCollision)
            {
                this.m_CollisionGridWidth = (int)Math.Ceiling(Global.ScreenWidth / (float)m_CollisionGridCellSize);
                this.m_CollisionGridHeight = (int)Math.Ceiling(Global.ScreenHeight / (float)m_CollisionGridCellSize);
                collisionGrid = new List<int>[m_CollisionGridWidth, m_CollisionGridHeight];
            }
            #endregion

            currentIterations = 0;

            for (int i = 0; i < m_MaxParticles; i++)
            {
                IPhysicsParticle reference = m_Particles[i];
                if (reference == null)
                    continue;

                Vector2 force = CalculateForces(reference, dt);

                reference.ApplyForce(force);

                reference.Update(dt, m_Gravity);

                if (reference.Destroy)
                {
                    m_Particles[i] = null;
                    m_Indexes.Push(i);
                    continue;
                }

                if (gridCollision)
                {
                    AddToCollisiongrid(reference, i);
                }
                else
                {
                    NormalColllision(reference, i);
                }                 
            }

            if (gridCollision)
            {
                DoCollisionGridCollisions();
            }

            if (currentIterations > collisionIterations)
                collisionIterations = currentIterations;

            m_CurrentCollisionChunk++;
            m_CurrentCollisionChunk %= m_MaxCollisionChunk;
        }

        protected void Clear()
        {
            m_Indexes.Clear();
            m_Particles = new IPhysicsParticle[m_MaxParticles];
            for (int i = 0; i < m_MaxParticles; i++)
            {
                m_Indexes.Push(i);
            }
        }

        public void DrawParticles(SpriteBatch sb)
        {
            sb.DrawString(Global.StandardFont, "DT = " + m_ReceivedDT, new Vector2(500, 10), Color.Green);
            sb.DrawString(Global.StandardFont, "Count = " + (m_MaxParticles - m_Indexes.Count).ToString() + " / " + m_MaxParticles, new Vector2(500, 30), Color.Green);
            sb.DrawString(Global.StandardFont, "Spawn Delay = " + m_SpawnDelay, new Vector2(500, 50), Color.Green);
            sb.DrawString(Global.StandardFont, "Chunk = " + m_CurrentCollisionChunk, new Vector2(500, 70), Color.Green);
            sb.DrawString(Global.StandardFont, "Collision Iterations " + collisionIterations + ", Cell size: " + m_CollisionGridCellSize, new Vector2(500, 90), Color.Green);

            //sb.End();
            //sb.Begin(SpriteSortMode.Texture, BlendState.Additive);

            for (int i = 0; i < m_MaxParticles; i++)
            {
                if (m_Particles[i] != null)
                    m_Particles[i].Draw(sb);
            }

            //sb.End();
            //sb.Begin();
        }

        #region Loading shit
        protected BaseParticleProperties ReadBaseParticleProperties(string type)
        {
            StreamReader reader = new StreamReader("Content/Settings/Particles/" + type + ".txt");
            List<string[]> properties = new List<string[]>();

            string line = reader.ReadLine();

            while (line != null && line != "")
            {
                properties.Add(line.Split(' '));
                line = reader.ReadLine();
            }

            return new BaseParticleProperties(
                float.Parse(properties[0][1], NumberStyles.Any),
                float.Parse(properties[1][1], NumberStyles.Any),
                float.Parse(properties[2][1], NumberStyles.Any),
                float.Parse(properties[3][1], NumberStyles.Any),
                float.Parse(properties[4][1], NumberStyles.Any),
                float.Parse(properties[5][1], NumberStyles.Any),
                float.Parse(properties[6][1], NumberStyles.Any),
                float.Parse(properties[7][1], NumberStyles.Any));
        }

        protected DeviationProperties ReadSystemProperties(string type)
        {
            StreamReader reader = new StreamReader("Content/Settings/Systems/" + type + ".txt");

            List<string[]> properties = new List<string[]>();

            string line = reader.ReadLine();

            while (line != null && line != "")
            {
                properties.Add(line.Split(' '));
                line = reader.ReadLine();
            }

            this.m_MaxParticles = int.Parse(properties[0][1]);
            this.m_SpawnDelay = float.Parse(properties[1][1], NumberStyles.Any);
            this.m_SpawnDelayInc = m_SpawnDelay / 10;
            this.m_MouseRadius = float.Parse(properties[2][1], NumberStyles.Any);
            this.m_MouseForceMultiplier = float.Parse(properties[3][1], NumberStyles.Any);
            this.m_MaxCollisionChunk = int.Parse(properties[4][1], NumberStyles.Any);

            this.m_Gravity = Vector2.Zero;
            this.m_Gravity.X = float.Parse(properties[5][1], NumberStyles.Any);
            this.m_Gravity.Y = float.Parse(properties[6][1], NumberStyles.Any);
            this.m_GravityStrength = float.Parse(properties[7][1], NumberStyles.Any);
            this.m_Gravity *= m_GravityStrength;

            this.gridCollision = 1 == float.Parse(properties[8][1], NumberStyles.Any);
            this.m_CollisionGridCellSize = int.Parse(properties[9][1], NumberStyles.Any);

            int deviationStart = 10;

            BaseGravityWellProperties wellprops = new BaseGravityWellProperties(
                new Vector2(float.Parse(properties[15][1], NumberStyles.Any), float.Parse(properties[15][2], NumberStyles.Any)),
                float.Parse(properties[15][3], NumberStyles.Any),
                Global.Game.Content.Load<Texture2D>("Doodats/well"));

            BaseVortexProperties vortexprops = new BaseVortexProperties(
                new Vector2(float.Parse(properties[16][1], NumberStyles.Any), float.Parse(properties[16][2], NumberStyles.Any)),
                float.Parse(properties[16][3], NumberStyles.Any),
                properties[16][4] == "true",
                float.Parse(properties[16][5], NumberStyles.Any),
                float.Parse(properties[16][6], NumberStyles.Any),
                float.Parse(properties[16][7], NumberStyles.Any),
                Global.Game.Content.Load<Texture2D>("Doodats/vortex"));

            this.m_GravWellManager = new GravWellContainer(wellprops, vortexprops);

            /*for (int i = 15; i < properties.Count; i++)
            {
                switch (properties[i][0])
                {
                    case "well":
                        m_GravityWells.Add(new GravityWell(
                                new Vector2(float.Parse(properties[i][1], NumberStyles.Any), float.Parse(properties[i][2], NumberStyles.Any)),
                                float.Parse(properties[i][3], NumberStyles.Any),
                                Global.Game.Content.Load<Texture2D>("Doodats/well")));
                        break;
                    case "vortex":
                        m_Vortexes.Add(new Vortex(
                               new Vector2(float.Parse(properties[i][1], NumberStyles.Any), float.Parse(properties[i][2], NumberStyles.Any)),
                               float.Parse(properties[i][3], NumberStyles.Any),
                               Global.Game.Content.Load<Texture2D>("Doodats/vortex"),
                               properties[i][4] == "true",
                               float.Parse(properties[i][5], NumberStyles.Any),
                               float.Parse(properties[i][6], NumberStyles.Any),
                               float.Parse(properties[i][7], NumberStyles.Any))
                               );
                        break;
                }
            }*/

            return new DeviationProperties(
                float.Parse(properties[deviationStart][1], NumberStyles.Any),
                float.Parse(properties[deviationStart + 1][1], NumberStyles.Any),
                float.Parse(properties[deviationStart + 2][1], NumberStyles.Any),
                float.Parse(properties[deviationStart + 3][1], NumberStyles.Any),
                float.Parse(properties[deviationStart + 4][1], NumberStyles.Any));
        }
        #endregion
    }

    public struct BaseParticleProperties
    {
        public float Mass, TTL, Scale, CollisionFraction, BounceDamping, GroundDamping, CollisionDamping, FrictionDamping;

        public BaseParticleProperties(float mass, float ttl,
            float scale, float collisionFraction,
            float bounceDamping, float groundDamping, float collisionDamping, float frictionDamping)
        {
            this.Mass = mass;
            this.TTL = ttl;
            this.Scale = scale;
            this.CollisionFraction = collisionFraction;
            this.BounceDamping = bounceDamping;
            this.GroundDamping = groundDamping;
            this.CollisionDamping = collisionDamping;
            this.FrictionDamping = frictionDamping;
        }
    }

    public struct DeviationProperties
    {
        public float MassDeviation, TTLDeviation, PositionXDeviation, PositionYDeviation, ScaleDeviation;

        public DeviationProperties(float massDeviation, float ttlDeviation, float positionXDeviation, float positionYDeviation, float scaleDeviation)
        {
            this.MassDeviation = massDeviation;
            this.TTLDeviation = ttlDeviation;
            this.PositionXDeviation = positionXDeviation;
            this.PositionYDeviation = positionYDeviation;
            this.ScaleDeviation = scaleDeviation;
        }
    }
}
