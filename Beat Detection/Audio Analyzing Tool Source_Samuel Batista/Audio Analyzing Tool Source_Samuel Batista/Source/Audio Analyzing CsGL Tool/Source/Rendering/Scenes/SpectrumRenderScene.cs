using System.Drawing;
using System.Windows.Forms;
using ED_Project.Source.Audio_Analyzing;
using Tao.OpenGl;

namespace Audio_Analyzing_CsGL_Tool.Source.Rendering.Scenes
{
    class SpectrumRenderScene : OpenGLRenderable
    {
        #region Fields

        private readonly SpectrumBuffer buff;
        private SpectrumData data;

        private float x, y, z, rot;

        #endregion Fields

        #region Constructor

        public SpectrumRenderScene(Size openGlControlSize)
            : base(openGlControlSize)
        {
            buff = SpectrumBuffer.Instance;
        }

        #endregion Constructor

        #region Initialize and Shutdown

        public override void Initialize()
        {
            x = 0.0f;
            y = 2.0f;
            z = -3.0f;

            rot = 0;

            Gl.glDisable(Gl.GL_LIGHTING);
        }

        public override void Shutdown()
        {
            
        }

        #endregion

        #region Update and Render

        public override void Update(float dt)
        {
            ProcessInput(dt);
        }

        public override void Render()
        {
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT); // Clear The Screen And The Depth Buffer 
            Gl.glLoadIdentity(); // Reset The Current Modelview Matrix 

            data = buff.GetLatestData();

            Gl.glTranslatef(x, y, z);
            Gl.glRotatef(90, 1, 0, 0);
            Gl.glRotatef(rot, 0, 0, 1);

            int SPECLEN = data.spectrumSize;

            double wth = 3.5 / (2 * SPECLEN), tmp, tmp2;
            int j, i;
            for (j = 0; j < SPECLEN; j += 1)
            {
                i = SPECLEN - 1 - j;
                tmp = 15 * data.specL[i];
                tmp2 = tmp / 2 + 2;
                Gl.glBegin(Gl.GL_LINE_LOOP);
                
                Gl.glColor4f(2 * data.specL[j], 0.75f, data.specL[i], 0.4f);
                //Gl.GlColor4f(300 * data.averageL, data.averageL * 500, data.specL[j], 0.4f);
                Gl.glVertex3f((float)(wth * j - 1.75), 0.5f, (float)tmp2);
                Gl.glVertex3f((float)(wth * j - 1.75 + wth), 0.5f, (float)tmp2);
                Gl.glVertex3f((float)(wth * j - 1.75 + wth), 0.5f, (float)(-tmp + tmp2));
                Gl.glVertex3f((float)(wth * j - 1.75), 0.5f, (float)(-tmp + tmp2));
                Gl.glEnd();

                tmp = 15 * data.specR[j]; tmp2 = tmp / 2 + 2;
                Gl.glBegin(Gl.GL_LINE_LOOP);

                Gl.glColor4f(2 * data.specR[j], 0.75f, data.specR[j], 0.4f);
                //Gl.GlColor4f(300 * data.averageR, data.averageR * 500, data.specR[j], 0.4f);
                Gl.glVertex3f((float)(wth * SPECLEN + wth * j - 1.75), 0.5f, (float)tmp2);
                Gl.glVertex3f((float)(wth * SPECLEN + wth * j - 1.75 + wth), 0.5f,(float)tmp2);
                Gl.glVertex3f((float)(wth * SPECLEN + wth * j - 1.75 + wth), 0.5f, (float)(-tmp + tmp2));
                Gl.glVertex3f((float)(wth * SPECLEN + wth * j - 1.75), 0.5f, (float)(-tmp + tmp2));
                Gl.glEnd();
            };
            Gl.glDisable(Gl.GL_BLEND);

            //Gl.glFlush(); // I don't understand the need for this so I leave it commented. 
        }
        #endregion Update and Render

        #region ProcessInput

        public void ProcessInput(float dt)
        {
            float vel = 1.0f;
            if (MainForm.KeyState[(int)Keys.ShiftKey])
                vel *= 5.0f;

            if (MainForm.KeyState[(int)Keys.W])
                z += vel * dt;
            if (MainForm.KeyState[(int)Keys.A])
                x += vel * dt;
            if (MainForm.KeyState[(int)Keys.S])
                z -= vel * dt;
            if (MainForm.KeyState[(int)Keys.D])
                x -= vel * dt;

            if (MainForm.KeyState[(int)Keys.Q])
                rot += dt * 20.0f;
            if (MainForm.KeyState[(int)Keys.E])
                rot -= dt * 20.0f;
        }

        #endregion ProcessInput
    }
}