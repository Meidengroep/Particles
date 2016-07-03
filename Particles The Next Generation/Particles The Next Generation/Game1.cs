using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Threading;

namespace Particles_The_Next_Generation
{
    public partial class TheNextGeneration : Game
    {
        public GraphicsDeviceManager graphics;
        SpriteBatch sb;
        IPhysicsParticleSystem m_PParticleSystem;
        WindSimulator m_WindInput;
        Menu m_Menu;
        bool m_MenuEnabled;
        bool m_DoAnything;
        Tester m_tester;
        Cursor m_Cursor;

        float m_particleDTDivider;

        public TheNextGeneration()
        {
            m_DoAnything = true;
            m_MenuEnabled = false;
            m_particleDTDivider = 1000;
            IsFixedTimeStep = false;

            IsMouseVisible = false;
            graphics = new GraphicsDeviceManager(this);

            int width = 1200, height = 720;
            graphics.PreferredBackBufferWidth = width;
            graphics.PreferredBackBufferHeight = height;
            graphics.SynchronizeWithVerticalRetrace = false;
            Content.RootDirectory = "Content";


            Input.Update();
            FileBrowser.Init();
            StringDrawer.Init(width, height);
            DragManager.Init();

            m_tester = new Tester();
            m_tester.Initialize(this);

            Activated += new EventHandler<EventArgs>(TheNextGeneration_Activated);
            Deactivated += new EventHandler<EventArgs>(TheNextGeneration_Deactivated);
            Exiting += new EventHandler<EventArgs>(TheNextGeneration_Exiting);
        }

        #region Events
        void TheNextGeneration_Exiting(object sender, EventArgs e)
        {
            MediaManager.ShutDown();
        }

        void TheNextGeneration_Activated(object sender, EventArgs e)
        {
            MediaManager.Resume();

            m_DoAnything = true;
        }

        void TheNextGeneration_Deactivated(object sender, EventArgs e)
        {
            MediaManager.Pause();

            m_DoAnything = false;
        }

        protected override void OnExiting(object sender, EventArgs args)
        {
            MediaManager.FMOD_Wrapper.Shutdown();
            base.OnExiting(sender, args);
        }
        #endregion

        #region Properties

        public WindSimulator WindInput
        {
            get { return this.m_WindInput; }
        }

        public Physics_System PhysicsSystem
        {
            get { return (Physics_System)this.m_PParticleSystem; }
        }

        public Menu Menu
        {
            get { return this.m_Menu; }
        }

        #endregion

        #region Loading...

        protected Physics_System CreatePhysicsSystem()
        {
            int spriteCount = 2, startIndex = 3;

            Texture2D[] sprites = new Texture2D[spriteCount];
            //sprites[0] = Utility.Load<Texture2D>("Particles/target1");

            for (int i = 0; i < spriteCount; i++) sprites[i] = Content.Load<Texture2D>("Particles/simple" + (startIndex + i));

            Vector2 startPosition = new Vector2(200, 200);

            m_particleDTDivider = 1000;

            return new Physics_System(startPosition, sprites);
        }

        protected override void LoadContent()
        {
            SpriteFont standardFont = Content.Load<SpriteFont>("Fonts/Font_Standard");
            Global.Initialize(this, standardFont);
            PhysicsProperties.Initialize();
            MediaManager.Initialize();

            sb = new SpriteBatch(GraphicsDevice);

            m_PParticleSystem = PSystemFactory("physics");

            int radius = 100;
            Vector2 position = new Vector2(110, 150);

            m_WindInput = new WindSimulator(radius, position);

            Texture2D overlay = Content.Load<Texture2D>("Menu/Overlays/menu_overlay");

            m_Menu = new Menu(overlay);
            m_Cursor = new Cursor(Content.Load<Texture2D>("Player/crosshair"));
            m_tester.LoadContent(this);
        }

        #endregion

        protected override void Update(GameTime gameTime)
        {
            if (m_DoAnything)
            {
                float dt = gameTime.ElapsedGameTime.Milliseconds;

                Global.Update(dt);
                Input.Update();
                MediaManager.Update(dt);

                m_tester.Update(dt);

                if (Input.KeyPressed(Keys.M))
                {
                    m_MenuEnabled = !m_MenuEnabled;
                    IsMouseVisible = m_MenuEnabled;
                }

                if (m_MenuEnabled)
                {
                    m_Menu.Update();
                }
                else m_Cursor.Update();

                m_WindInput.Update(true);

                MouseState mouse = Mouse.GetState();

                Vector2 mousepos = new Vector2(mouse.X, mouse.Y);

                float particleDT = Math.Max(0.0001f, dt / m_particleDTDivider);
                m_PParticleSystem.UpdateParticles(particleDT, mousepos, !m_MenuEnabled);

                base.Update(gameTime);
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            if (m_DoAnything)
            {
                GraphicsDevice.Clear(Color.Black);

                sb.Begin(SpriteSortMode.Texture, BlendState.Additive);

                MediaManager.Draw(sb);

                m_PParticleSystem.DrawParticles(sb);

                m_WindInput.Draw(sb);

                if (m_MenuEnabled)
                    m_Menu.Draw(sb);
                else m_Cursor.Draw(sb);

                m_tester.Draw(sb);

                sb.End();

                base.Draw(gameTime);

                StringDrawer.ResetPositions();
            }
        }
    }
}
