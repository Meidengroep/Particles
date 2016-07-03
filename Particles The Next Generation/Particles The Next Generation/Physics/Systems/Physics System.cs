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
    public partial class Physics_System : IPhysicsParticleSystem
    {
        #region members

        protected Vector2 m_Position;
        protected IPhysicsParticle[] m_Particles;
        protected Random r;
        protected int m_MaxParticles;

        protected bool autoGeneration;

        protected Vector2 m_GravityDirection, m_Gravity;
        protected float m_GravityStrength;
        protected float m_MouseForceMultiplier, m_MouseRadius;

        protected GravWellContainer m_GravWellManager;
        protected ExplosionManager m_Explosions;

        protected Stack<int> m_Indexes;

        protected float m_Timer, m_SpawnDelay, m_SpawnDelayInc, m_ReceivedDT;

        protected CollisionGridManager m_GridCollisionHandler;
        protected CollidableObjectManager m_ObjectCollisionHandler;

        protected ForceGrid m_ForceGrid;

        protected PParticleFactory m_ParticleFactory;

        protected string m_CurrentParticleType;
        protected int m_CurrentParticleTypeIndex;
        protected List<string> m_AllParticleTypes;

        #endregion

        public Physics_System(Vector2 position, Texture2D[] sprites)
        {
            m_AllParticleTypes = new List<string>();
            m_CurrentParticleTypeIndex = 0;
            m_AllParticleTypes.Add("regular");
            m_AllParticleTypes.Add("music");

            m_CurrentParticleType = m_AllParticleTypes[m_CurrentParticleTypeIndex];

            m_CurrentParticleType = "music";

            m_ParticleFactory = new PParticleFactory(sprites, "physics1", "physics1");

            ReadAndInitSystemProperties("physics1", sprites[0].Width);

            m_Explosions = new ExplosionManager();
            m_ObjectCollisionHandler = new CollidableObjectManager();

            m_ReceivedDT = 0;
            m_Timer = 0;

            m_Indexes = new Stack<int>(m_MaxParticles);
            for (int i = m_MaxParticles - 1; i >= 0; i--)
                m_Indexes.Push(i);

            m_Particles = new IPhysicsParticle[m_MaxParticles];
            m_Position = position;

            r = new Random();

            m_ForceGrid = new ForceGrid(Global.ScreenWidth, Global.ScreenHeight, 10, 10);
            /*for (float x = 0; x < 500; x += 1f)
                for (float y = 0; y < 500; y += 1f)
                {
                    m_ForceGrid.IncreaseForce(x, y, new Vector2(10000, 0));
                }*/
        }

        public void AddCollidableObject(ICollidableObject obj)
        {
            m_ObjectCollisionHandler.AddObject(obj);
        }

        public void AddPlayer(Player playa)
        {
            m_ObjectCollisionHandler.AddPlayer(playa);
        }

        #region Properties

        public PParticleFactory ParticleFactory
        {
            get { return this.m_ParticleFactory; }
        }

        public ForceGrid ForceGrid
        {
            get { return this.m_ForceGrid; }
        }

        public float GravityStrength
        {
            get { return this.m_GravityStrength; }
            set
            {
                this.m_GravityStrength = value;
                this.m_Gravity = m_GravityDirection * m_GravityStrength;
            }
        }

        #endregion

        #region Generating shit

        public void GenerateNewParticles()
        {
            int numberOfSpawns = (int)Math.Floor(m_Timer / m_SpawnDelay);
            m_Timer -= numberOfSpawns * m_SpawnDelay;

            for (int i = 0; i < numberOfSpawns; i++)
                if (m_Indexes.Count > 0)
                {
                    m_Particles[m_Indexes.Pop()] = m_ParticleFactory.GenerateParticle(m_Position, m_CurrentParticleType);
                }
                else break;
        }
        #endregion

        protected Vector2 CalculateForces(IPhysicsParticle particle, float dt)
        {
            Vector2 force = m_Gravity;

            float distance = (Input.MousePosition - particle.Position).Length();

            if (distance < m_MouseRadius && distance != 0)
                force += Input.MouseVelocity * m_MouseForceMultiplier * (m_MouseRadius / distance);

            force += Global.WindDirection * Global.WindStrength / particle.Mass;
            force += m_GravWellManager.GetGravityWellForce(particle.Position, particle.Mass);
            force += m_GravWellManager.GetVortexForce(particle.Position, particle.Mass);
            force += m_Explosions.GetForce(particle);
            force += m_ForceGrid.GetForce(particle.Position.X, particle.Position.Y);

            force *= dt;

            return force;
        }

        public void UpdateParticles(float dt, Vector2 position, bool takeInput)
        {
            bool doExplosions = !m_GravWellManager.Update(dt, takeInput);
            m_Explosions.Update(dt);
            m_ObjectCollisionHandler.Update(dt);

            #region Input and generation
            m_ReceivedDT = dt;
            m_Position = position;

            HandleInput(takeInput, dt, doExplosions);

            if (autoGeneration)
            {
                m_Timer += dt;
                GenerateNewParticles();
            }
            #endregion

            m_GridCollisionHandler.ResetGrid();

            #region  Update all particles
            for (int i = 0; i < m_MaxParticles; i++)
            {
                IPhysicsParticle reference = m_Particles[i];
                if (reference == null)
                    continue;

                Vector2 force = CalculateForces(reference, dt);

                reference.ApplyForce(force);

                reference.Update(dt);

                if (reference.Destroy)
                {
                    m_Particles[i] = null;
                    m_Indexes.Push(i);
                    continue;
                }

                m_GridCollisionHandler.AddToCollisiongrid(reference, i);
                m_ObjectCollisionHandler.DoCollisions(reference);
            }
            #endregion
            m_GridCollisionHandler.DoCollisionGridCollisions(m_Particles);

            m_ForceGrid.ClearGrid();
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
            if (Global.ShowInfo)
            {
                StringDrawer.DrawLine(sb, StringAlignment.Center, "DT = " + m_ReceivedDT, Color.Green);
                StringDrawer.DrawLine(sb, StringAlignment.Center, "Count = " + (m_MaxParticles - m_Indexes.Count).ToString() + " / " + m_MaxParticles, Color.Green);
                StringDrawer.DrawLine(sb, StringAlignment.Center, "Spawn Delay = " + m_SpawnDelay, Color.Green);
                StringDrawer.DrawLine(sb, StringAlignment.Center, "Collision Enabled: " + m_GridCollisionHandler.CollisionsEnabled, Color.Green);
                StringDrawer.DrawLine(sb, StringAlignment.Center, "Current Particle Type: " + m_CurrentParticleType, Color.Green);
            }

            m_Explosions.Draw(sb);

            m_ObjectCollisionHandler.Draw(sb);

            m_GravWellManager.Draw(sb);

            for (int i = 0; i < m_MaxParticles; i++)
            {
                if (m_Particles[i] != null)
                    m_Particles[i].Draw(sb);
            }
        }
    }
}
