using System;
using System.Drawing;
using System.Windows.Forms;
using ED_Project.Source.Audio_Analyzing;
using Tao.FreeGlut;
using Tao.OpenGl;

namespace Audio_Analyzing_CsGL_Tool.Source.Rendering.Scenes
{
    class BeatDetectionScene : OpenGLRenderable
    {
        #region Fields
        private static bool glutInitializelyOnce = true;

        private readonly SpectrumBuffer buff;
        private SpectrumData data;

        private GLFont font = null;

        float timer;

        #endregion Fields

        #region Constructor

        public BeatDetectionScene(Size openGlControlSize)
            : base(openGlControlSize)
        {
            buff = SpectrumBuffer.Instance;

            font = new GLFont(Glut.GLUT_BITMAP_HELVETICA_18, openGlControlSize);

            Glut.glutInitWindowSize(openGlControlSize.Width, openGlControlSize.Height);
            if (glutInitializelyOnce == true)
            {
                Glut.glutInit();
                glutInitializelyOnce = false;
            }
        }

        #endregion Constructor

        #region Initialize and Shutdown

        public override void Initialize()
        {
            Gl.glClearColor(0.2f, 0.2f, 0.2f, 0.0f);
            Gl.glClearDepth(1);
            Gl.glClearStencil(0);

            timer = 0;
        }

        public override void Shutdown()
        {
        }

         public override void Resize(int width, int height)
         {
             base.Resize(width, height);
             if (font != null)
             font.Resize(new Size(width, height));
         }
        #endregion

        #region Update and Render

         public override void Update(float dt)
        {
            ProcessInput(dt);

            data = buff.GetLatestData();

            if (data.isBeat == true)    
                timer = 1.0f;
            else
            {
                timer -= dt*2;
            }
        }

        private static long i = 0;
        public override void Render()
        {    
            Gl.glClearColor(timer, 0, 0, 0.0f);                                   

            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT | Gl.GL_STENCIL_BUFFER_BIT);
            Gl.glLoadIdentity(); // Reset The Current Modelview Matrix 

           
            string test = "Beat: " + data.isBeat.ToString();      
            font.Print(10, 20, test, GLFont.COLORS.CYAN);
            test = "Instant Energy: " + data.beatData.instantEnergy;
            font.Print(10, 50, test, GLFont.COLORS.CYAN);
            test = "Average Energy: " + data.beatData.averageEnergy;
            font.Print(10, 80, test, GLFont.COLORS.CYAN);
            test = "Variance: " + data.beatData.variance;
            font.Print(10, 110, test, GLFont.COLORS.CYAN);

            //Gl.glPushMatrix();
            //Gl.glTranslatef(10, 100, 0);

            //Gl.glBegin(Gl.GL_LINE);
            //Gl.glColor4f(0, 0.75f, 0, 0.4f);
            ////Gl.GlColor4f(300 * data.averageR, data.averageR * 500, data.specR[j], 0.4f);
            //Gl.glVertex3f((float)(0), 0, 0);
            //Gl.glVertex3f((float)(data.beatData.instantEnergy / data.beatData.averageEnergy)*100, 0, 0);
            
            //Gl.glEnd();
            //Gl.glPopMatrix();
            
            Gl.glFlush(); // I don't understand the need for this so I leave it commented. 
        }
        #endregion Update and Render

        #region ProcessInput

        public void ProcessInput(float dt)
        {
            float vel = 1.0f;
            if (MainForm.KeyState[(int)Keys.ShiftKey])
                vel *= 5.0f;

            //if (MainForm.KeyState[(int)Keys.W])
            //    z += vel * dt;
            //if (MainForm.KeyState[(int)Keys.A])
            //    x += vel * dt;
            //if (MainForm.KeyState[(int)Keys.S])
            //    z -= vel * dt;
            //if (MainForm.KeyState[(int)Keys.D])
            //    x -= vel * dt;

            //if (MainForm.KeyState[(int)Keys.Q])
            //    rot -= dt * 20.0f;
            //if (MainForm.KeyState[(int)Keys.E])
            //    rot += dt * 20.0f;
        }

        #endregion ProcessInput
    }
}