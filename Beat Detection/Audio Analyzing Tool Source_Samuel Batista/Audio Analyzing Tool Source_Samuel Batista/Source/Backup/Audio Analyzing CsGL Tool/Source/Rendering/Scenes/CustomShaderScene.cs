using System;
using System.Drawing;
using System.Windows.Forms;
using ED_Project.Source.Audio_Analyzing;
using Tao.OpenGl;
using System.Collections.Generic;

namespace Audio_Analyzing_CsGL_Tool.Source.Rendering.Scenes
{
    class TestScene : OpenGLRenderable
    {
        #region Fields

        private readonly SpectrumBuffer buff;
        private SpectrumData data;

        private float x, y, z, rot;
        private List<PointF> points;
        float minX, minY, maxX, maxY;

        Glu.GLUquadric quad;
        int height;
        float interp;

        struct myColor
        {
            public float R;
            public float G;
            public float B;
        }
        List<myColor> colors;
     
        #endregion Fields

        #region Constructor

        public TestScene(Size openGlControlSize)
            : base(openGlControlSize)
        {
            height = openGlControlSize.Height;
            buff = SpectrumBuffer.Instance;
        }

        #endregion Constructor

        #region Initialize and Shutdown

        public override void Initialize()
        {
            Gl.glClearColor(0.0f, 0.0f, 0.0f, 0.0f);
            Gl.glClearDepth(1);
            Gl.glClearStencil(0);

            x = 0.0f;
            y = 2.0f;
            z = -3.0f;

            rot = 0;
            interp = 0;


            // Rainbow Colors
            colors = new List<myColor>();
            myColor c = new myColor();
            c.R = 1.0f; c.G = 0.5f; c.B = 0.5f; colors.Add(c);
            c.R = 1.0f; c.G = 0.75f; c.B = 0.5f; colors.Add(c);
            c.R = 1.0f; c.G = 1.0f; c.B = 0.5f; colors.Add(c);
            c.R = 0.75f; c.G =1.0f; c.B = 0.5f; colors.Add(c);
            c.R = 0.5f; c.G = 1.0f; c.B = 0.5f; colors.Add(c);
            c.R = 0.5f; c.G = 1.0f; c.B = 0.75f; colors.Add(c);
            c.R = 0.5f; c.G = 1.0f; c.B = 1.0f; colors.Add(c);
            c.R = 0.5f; c.G = 0.75f; c.B = 1.0f; colors.Add(c);
            c.R = 0.5f; c.G = 0.5f; c.B = 1.0f; colors.Add(c);
            c.R = 0.75f; c.G = 0.5f; c.B = 1.0f; colors.Add(c);
            c.R = 1.0f; c.G = 0.5f; c.B = 1.0f; colors.Add(c);
            c.R = 1.0f; c.G = 0.5f; c.B = 0.75f; colors.Add(c);



            Gl.glDisable(Gl.GL_LIGHTING);

            points = new List<PointF>();

            quad = Glu.gluNewQuadric();
            data = buff.GetLatestData();
            minX = minY = maxX = maxY = 0;
        }

        public override void Shutdown()
        {
            Glu.gluDeleteQuadric(quad);
        }

        #endregion

        #region Update and Render

        public override void Update(float dt)
        {
            ProcessInput(dt);
            if (interp > 1.0f)
                interp = 0;
            interp += dt;
        }

        public override void Render()
        {
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT | Gl.GL_STENCIL_BUFFER_BIT);
            Gl.glLoadIdentity(); // Reset The Current Modelview Matrix 

            data = buff.GetLatestData();

            if(data.spectrumSize != points.Count)
            {
                points = null;
                points = new List<PointF>();
                Random rand = new Random();
                for (int i = 0; i < data.spectrumSize; ++i)
                {
                    float tempX = (rand.Next(300) - 100) / 50.0f;
                    float tempY = ((rand.Next(300) - 100) / 100.0f);
                    if (tempX > maxX)
                        maxX = tempX;
                    if (tempX < minX)
                        minX = tempX;
                    if (tempY > maxY)
                        maxY = tempY;
                    if (tempY < minY)
                        minY = tempY;

                    points.Add(new PointF(tempX, tempY));
                }
            }


            Gl.glTranslatef(x, y, z);
            Gl.glRotatef(rot, 0, 1, 0);

            Gl.glTranslatef(-1.0f, -2.5f, 0);

            for (int i = 0; i < data.spectrumSize; ++i)
            {
                Gl.glPushMatrix();
                Gl.glTranslatef(points[i].X, points[i].Y, 0.0f);

                Gl.glColor4f(colors[(int)(i * (12.0f / data.spectrumSize))].R,
                    colors[(int)(i * (12.0f / data.spectrumSize))].G,
                    colors[(int)(i * (12.0f / data.spectrumSize))].B, (data.specL[i] + data.specR[i]) * 100);

                float s = (data.specL[i] + data.specR[i]) * 30 + ((data.specL[i] + data.specR[i])>0? 0.5f : 1.0f);
                Gl.glScalef(s, s, s);

                Glu.gluSphere(quad, 0.01f, 10, 10);
                Gl.glPopMatrix();
            }
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
                rot -= dt * 20.0f;
            if (MainForm.KeyState[(int)Keys.E])
                rot += dt * 20.0f;
        }

        #endregion ProcessInput
    }
}