using System;
using System.Drawing;
using System.Windows.Forms;
using ED_Project.Source.Audio_Analyzing;
using Tao.OpenGl;

namespace Audio_Analyzing_CsGL_Tool.Source.Rendering.Scenes
{
    class RotatingSpheresScene : OpenGLRenderable
    {
        #region Fields

        private readonly SpectrumBuffer buff;
        private SpectrumData data;

        private float rotx = 0, roty = 0, rotz = 0;
        private float x, y, z, rot;

        private int sphere;

        float[] ambientLight2 = { 0.4f, 0.3f, 0.3f, 1.0f };
        float[] diffuseLight2 = { 0.4f, 0.7f, 0.5f, 1.0f };
        float[] specularLight2 = { 1.0f, 0.8f, 1.0f, 1.0f };
        float[] positionLight2 = { 0.0f, 0.0f, -.8f, 1.0f };

        float[] ambientLight = { 0.4f, 0.8f, 0.8f, 1.0f };
        float[] diffuseLight = { 0.9f, 0.9f, 0.5f, 1.0f };
        float[] specularLight = { 1.0f, 0.8f, 1.0f, 1.0f };
        float[] position = { 1, -2, -3, 1 };

        #endregion Fields

        #region Constructor

        public RotatingSpheresScene(Size openGlControlSize)
            : base(openGlControlSize)
        {
            buff = SpectrumBuffer.Instance;
        }

        #endregion Constructor

        #region Initialize and Shutdown

        public override void Initialize()
        {
            x = 0.0f;
            y = -1.0f;
            z = -3.0f;

            rot = 0;

            Gl.glClearColor(0.2f, 0.2f, 0.2f, 0.0f);
            Gl.glClearDepth(1);
            Gl.glClearStencil(0);

            Gl.glEnable(Gl.GL_DEPTH_TEST);
            Gl.glEnable(Gl.GL_NORMALIZE);
            Gl.glEnable(Gl.GL_LIGHTING);
            Gl.glShadeModel(Gl.GL_SMOOTH);
            Gl.glEnable(Gl.GL_LINE_SMOOTH);
            Gl.glHint(Gl.GL_LINE_SMOOTH_HINT, Gl.GL_NICEST);

            Gl.glLightfv(Gl.GL_LIGHT0, Gl.GL_AMBIENT, ambientLight);
            Gl.glLightfv(Gl.GL_LIGHT0, Gl.GL_DIFFUSE, diffuseLight);
            Gl.glLightfv(Gl.GL_LIGHT0, Gl.GL_SPECULAR, specularLight);
            Gl.glLightfv(Gl.GL_LIGHT1, Gl.GL_POSITION, position);
            Gl.glLightf(Gl.GL_LIGHT0, Gl.GL_SPOT_CUTOFF, 80.0f);

            Gl.glLightfv(Gl.GL_LIGHT2, Gl.GL_AMBIENT, ambientLight2);
            Gl.glLightfv(Gl.GL_LIGHT2, Gl.GL_DIFFUSE, diffuseLight2);
            Gl.glLightfv(Gl.GL_LIGHT2, Gl.GL_SPECULAR, specularLight2);
            Gl.glLightf(Gl.GL_LIGHT2, Gl.GL_SPOT_EXPONENT, 90.0f);
            Gl.glLightf(Gl.GL_LIGHT0, Gl.GL_SPOT_CUTOFF, 10.0f);

            Gl.glEnable(Gl.GL_LIGHT0);
            Gl.glEnable(Gl.GL_LIGHT2);

            sphere = Gl.glGenLists(1);
        }

        public override void Shutdown()
        {
            Gl.glDeleteLists(sphere, 1);
        }

        #endregion

        #region Update and Render

        public override void Update(float dt)
        {
            ProcessInput(dt);

            rotx += 10.0f * dt;
            if (rotx > 360) rotx -= 360;
            roty += 15 * dt;
            if (roty > 360) roty -= 360;
            rotz += 25 * dt;
            if (rotz > 360) rotz -= 360;
        }

        public override void Render()
        {
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT | Gl.GL_STENCIL_BUFFER_BIT);
            Gl.glLoadIdentity(); // Reset The Current Modelview Matrix 

            data = buff.GetLatestData();

            Gl.glPushMatrix();
            Gl.glRotatef(rotx, 1, 0, 0);
            Gl.glRotatef(roty, 0, -1, 0);
            float[] p = { 0, 0, 0 };
            Gl.glLightfv(Gl.GL_LIGHT2, Gl.GL_POSITION, p);
            Gl.glLightfv(Gl.GL_LIGHT2, Gl.GL_AMBIENT, ambientLight2);
            Gl.glLightfv(Gl.GL_LIGHT2, Gl.GL_DIFFUSE, diffuseLight2);
            Gl.glLightfv(Gl.GL_LIGHT2, Gl.GL_SPECULAR, specularLight2);
            Gl.glLightfv(Gl.GL_LIGHT2, Gl.GL_POSITION, positionLight2);
            Gl.glPopMatrix();

            Gl.glEnable(Gl.GL_STENCIL_TEST);
            Gl.glStencilFunc(Gl.GL_ALWAYS, 1, 1);
            Gl.glStencilOp(Gl.GL_KEEP, Gl.GL_KEEP, Gl.GL_REPLACE);

            Gl.glEnable(Gl.GL_DEPTH_TEST);
            Gl.glColorMask(1, 1, 1, 1);
            Gl.glStencilFunc(Gl.GL_EQUAL, 1, 1);

            Gl.glStencilOp(Gl.GL_KEEP, Gl.GL_KEEP, Gl.GL_KEEP);
            Gl.glEnable(Gl.GL_CLIP_PLANE0);
            double[] eqr = { 0, -0.0, 0, 0 };
            Gl.glClipPlane(Gl.GL_CLIP_PLANE0, eqr);
            Gl.glPushMatrix();
            Gl.glScalef(1, -1, 1);

            Gl.glDisable(Gl.GL_LIGHTING);
            RenderSpheres();

            Gl.glPopMatrix();
            Gl.glDisable(Gl.GL_CLIP_PLANE0);
            Gl.glDisable(Gl.GL_STENCIL_TEST);


            //Gl.glFlush(); // I don't understand the need for this so I leave it commented. 
        }
        #endregion Update and Render

        #region Sphere Rendering

        void RenderSpheres()
        {
            double ssx, ssy, ssz;

            Gl.glTranslatef(x, y, z);
            Gl.glRotatef(rot, 0, 1, 0);

            Gl.glPushMatrix();

                Gl.glTranslatef(0, .8f, -.7f);
                Gl.glColor4f(1, data.specL[0], 1, 0.7f);

                Gl.glLightf(Gl.GL_LIGHT0, Gl.GL_SPOT_CUTOFF, (float)(12 * Math.Sqrt(data.specL[0])));

                //single sphere
                Gl.glPushMatrix();
                    ssx = sphereScaler(data.specL[0]);
                    ssy = sphereScaler(data.specL[1]);
                    ssz = sphereScaler(data.specL[2]);
                    Gl.glScalef((float)ssx, (float)ssy, (float)ssz);
                    Gl.glTranslatef(-0.8f, 0, 0);
                    Gl.glRotatef(rotx, 1, 0, 0);
                    Gl.glRotatef(roty, 0, 1, 0);
                    Gl.glRotatef(rotz, 0, 0, 1);

                    Gl.glNewList(sphere, Gl.GL_COMPILE);
                    uglSphere(0.5f, 10, 20, true, Math.PI, 2 * Math.PI, data.specL);
                    Gl.glEndList();

                    Gl.glPolygonMode(Gl.GL_FRONT_AND_BACK, Gl.GL_FILL);
                    Gl.glEnable(Gl.GL_POLYGON_OFFSET_FILL);
                    Gl.glEnable(Gl.GL_LINE_SMOOTH);
                    Gl.glEnable(Gl.GL_LIGHTING);
                    Gl.glPolygonOffset(1, 1);
                    Gl.glCallList(sphere);
                    Gl.glDisable(Gl.GL_LINE_SMOOTH);
                    Gl.glDisable(Gl.GL_POLYGON_OFFSET_FILL);

                    Gl.glEnable(Gl.GL_BLEND);
                    Gl.glBlendFunc(Gl.GL_SRC_ALPHA, Gl.GL_ONE_MINUS_SRC_ALPHA);
                    Gl.glEnable(Gl.GL_DEPTH_TEST);
                    Gl.glDisable(Gl.GL_LIGHTING);
                    Gl.glPolygonMode(Gl.GL_FRONT_AND_BACK, Gl.GL_LINE);
                    Gl.glCallList(sphere);
                    Gl.glEnable(Gl.GL_LIGHTING);
                    Gl.glDisable(Gl.GL_BLEND);
                    Gl.glPolygonMode(Gl.GL_FRONT_AND_BACK, Gl.GL_FILL);
                Gl.glPopMatrix();

                //single sphere
                Gl.glPushMatrix();
                    ssx = sphereScaler(data.specR[0]);
                    ssy = sphereScaler(data.specR[1]);
                    ssz = sphereScaler(data.specR[2]);
                    Gl.glScalef((float)ssx, (float)ssy, (float)ssz);
                    Gl.glTranslatef(0.8f, 0, 0);
                    Gl.glRotatef(rotx, -1, 0, 0);
                    Gl.glRotatef(roty, 0, -1, 0);
                    Gl.glRotatef(rotz, 0, 0, -1);

                    Gl.glNewList(sphere, Gl.GL_COMPILE);
                    uglSphere(0.5, 10, 20, true, Math.PI, Math.PI * 2, data.specR);
                    Gl.glEndList();

                    Gl.glPolygonMode(Gl.GL_FRONT_AND_BACK, Gl.GL_FILL);
                    Gl.glEnable(Gl.GL_POLYGON_OFFSET_FILL);
                    Gl.glEnable(Gl.GL_LINE_SMOOTH);
                    Gl.glPolygonOffset(1, 1);
                    Gl.glCallList(sphere);
                    Gl.glDisable(Gl.GL_LINE_SMOOTH);
                    Gl.glDisable(Gl.GL_POLYGON_OFFSET_FILL);

                    Gl.glEnable(Gl.GL_BLEND);
                    Gl.glBlendFunc(Gl.GL_SRC_ALPHA, Gl.GL_ONE_MINUS_SRC_ALPHA);
                    Gl.glEnable(Gl.GL_DEPTH_TEST);
                    Gl.glDisable(Gl.GL_LIGHTING);
                    Gl.glPolygonMode(Gl.GL_FRONT_AND_BACK, Gl.GL_LINE);
                    Gl.glCallList(sphere);
                    Gl.glEnable(Gl.GL_LIGHTING);
                    Gl.glDisable(Gl.GL_BLEND);


                    Gl.glPolygonMode(Gl.GL_FRONT_AND_BACK, Gl.GL_FILL);
                Gl.glPopMatrix();

            Gl.glPopMatrix();
        }
        /* render single sphere using one of two channels */
        //void uglSphere( double radius, int slices, int stacks, bool half = false,  double tang = Math.PI, double pang = 2*Math.PI)
        void uglSphere(double radius, int slices, int stacks, bool half, double tang, double pang, float[] spec)
        {
            double theta = 0.0, dtheta = tang / (double)slices;
            double phi = 0.0, dphi = pang / ((double)stacks);
            double[] phis = new double[stacks + 2];//(double*)calloc( stacks+2, sizeof(double) );
            double[] phic = new double[stacks + 1];//(double*)calloc( stacks+1, sizeof(double) );
            double[] thetas = new double[slices + 1];//(double*)calloc( slices+1, sizeof(double) );
            double[] thetac = new double[slices + 1];//(double*)calloc( slices+1, sizeof(double) );
            double[] ox = new double[stacks + 1];//(double*)calloc( stacks+1, sizeof(double) );
            double[] oy = new double[stacks + 1];//(double*)calloc( stacks+1, sizeof(double) );
            double[] oz = new double[stacks + 1];//(double*)calloc( stacks+1, sizeof(double) );
            int i = 0;
            int j = 0;
            int specidx = 0;
            double ct, st;

            theta = Math.PI / 2;
            ct = Math.Cos(theta); st = Math.Sin(theta);
            i = 0;
            for (j = 0; j <= stacks; j++)
            {
                phi = dphi * (double)j;
                phis[j] = Math.Sin(phi);
                phic[j] = Math.Cos(phi);
                ox[j] = st * phic[j];
                oy[j] = ct;
                oz[j] = st * phis[j];
            };
            phis[stacks + 1] = phis[0];

            i = 1;
            int slice = 0;
            Gl.glPushMatrix();
            Gl.glScalef((float)radius, (float)radius, (float)radius);
            // glBegin(GL_LINE_LOOP);

            double[] x = new double[2];
            double[] y = new double[2];
            double[] z = new double[2];
            double a = 0.0;

            while (i < (slices >> 1) + 1)
            {
                theta = Math.PI / 2 - dtheta * i;
                ct = Math.Cos(theta); st = Math.Sin(theta);
                x[0] = st * phic[0];
                y[0] = ct;
                z[0] = st * phis[0];

                for (j = 1; j <= stacks; j++)
                {
                    x[1] = st * phic[j];
                    y[1] = ct;
                    z[1] = st * phis[j];

                    for (int k = -1; k <= 1; k += 2)
                    {

                        slice = Math.Abs(i - (slices >> 1));
                        if (k > 0) slice = Math.Abs(slices - slice) - 1;

                        if (j % 2 > 0 && slice % 2 == 0)
                        {
                            a = spherePeek(spec[specidx++]);
                            specidx += 1;
                        }
                        else a = 0;

                        Gl.glPushMatrix();
                        Gl.glScalef(1, (float)k, 1);
                        Gl.glBegin(Gl.GL_QUADS);

                        Gl.glNormal3f((float)-x[0], (float)-y[0], (float)-z[0]);
                        Gl.glColor4f(0, 0, 0, 0.4f);
                        Gl.glVertex3d(x[0] + a * x[0], y[0] + a * y[0], z[0] + a * z[0]);

                        Gl.glNormal3d(-ox[j - 1], -oy[j - 1], -oz[j - 1]);
                        Gl.glColor4f(0, 0, 0, 0.4f);
                        Gl.glVertex3d(ox[j - 1] + a * ox[j - 1], oy[j - 1] + a * oy[j - 1], oz[j - 1] + a * oz[j - 1]);

                        Gl.glNormal3d(-ox[j], -oy[j], -oz[j]);
                        Gl.glColor4f(0, 0, 0, 0.4f);
                        Gl.glVertex3d(ox[j] + a * ox[j], oy[j] + a * oy[j], oz[j] + a * oz[j]);

                        Gl.glNormal3d(-x[1], -y[1], -z[1]);
                        Gl.glColor4f(0, 0, 0, 0.4f);
                        Gl.glVertex3d(x[1] + a * x[1], y[1] + a * y[1], z[1] + a * z[1]);

                        if (a > 0)  //jak jest a to trzeba polaczyc odstajace ze sfera :) hehe
                        {
                            //1
                            Gl.glNormal3f((float)(-ct * phic[j]), (float)-st, (float)(-ct * phis[j]));
                            Gl.glColor4f(0, 0, 0, 0.4f);
                            Gl.glVertex3d(x[0] + a * x[0], y[0] + a * y[0], z[0] + a * z[0]);
                            Gl.glColor4f(0, 0, 0, 0.4f);
                            Gl.glVertex3d(x[1] + a * x[1], y[1] + a * y[1], z[1] + a * z[1]);
                            Gl.glColor4f(0, 0, 0, 0.4f);
                            Gl.glVertex3d(x[1], y[1], z[1]);
                            Gl.glColor4f(0, 0, 0, 0.4f);
                            Gl.glVertex3d(x[0], y[0], z[0]);
                            //2
                            Gl.glNormal3f((float)(ct * phic[j]), (float)-st, (float)(ct * phis[j]));
                            Gl.glColor4f(0, 0, 0, 0.4f);
                            Gl.glVertex3d(x[0] + a * x[0], y[0] + a * y[0], z[0] + a * z[0]);
                            Gl.glColor4f(0, 0, 0, 0.4f);
                            Gl.glVertex3d(ox[j - 1] + a * ox[j - 1], oy[j - 1] + a * oy[j - 1], oz[j - 1] + a * oz[j - 1]);
                            Gl.glColor4f(0, 0, 0, 0.4f);
                            Gl.glVertex3d(ox[j - 1], oy[j - 1], oz[j - 1]);
                            Gl.glColor4f(0, 0, 0, 0.4f);
                            Gl.glVertex3d(x[0], y[0], z[0]);
                            //3
                            Gl.glNormal3f((float)(-ct * phic[j]), (float)st, (float)(-ct * phis[j]));
                            Gl.glColor4f(0, 0, 0, 0.4f);
                            Gl.glVertex3d(x[1] + a * x[1], y[1] + a * y[1], z[1] + a * z[1]);
                            Gl.glColor4f(0, 0, 0, 0.4f);
                            Gl.glVertex3d(ox[j] + a * ox[j], oy[j] + a * oy[j], oz[j] + a * oz[j]);
                            Gl.glColor4f(0, 0, 0, 0.4f);
                            Gl.glVertex3d(ox[j], oy[j], oz[j]);
                            Gl.glColor4f(0, 0, 0, 0.4f);
                            Gl.glVertex3d(x[1], y[1], z[1]);
                            //4
                            Gl.glNormal3f((float)(ct * phic[j]), (float)st, (float)(ct * phis[j]));
                            Gl.glColor4f(0, 0, 0, 0.4f);
                            Gl.glVertex3d(ox[j - 1] + a * ox[j - 1], oy[j - 1] + a * oy[j - 1], oz[j - 1] + a * oz[j - 1]);
                            Gl.glColor4f(0, 0, 0, 0.4f);
                            Gl.glVertex3d(ox[j] + a * ox[j], oy[j] + a * oy[j], oz[j] + a * oz[j]);
                            Gl.glColor4f(0, 0, 0, 0.4f);
                            Gl.glVertex3d(ox[j], oy[j], oz[j]);
                            Gl.glColor4f(0, 0, 0, 0.4f);
                            Gl.glVertex3d(ox[j - 1], oy[j - 1], oz[j - 1]);
                        };

                        Gl.glEnd();
                        Gl.glPopMatrix();
                    };

                    if (!half)
                    {
                        Gl.glNormal3f((float)-x[0], (float)y[0], (float)-z[0]);
                        Gl.glColor4f(0, 0, 0, 0.4f);
                        Gl.glVertex3d(x[0] + a * x[0], -y[0] - a * y[0], z[0] + a * z[0]);
                        Gl.glNormal3d(-ox[j], oy[j], -oz[j]);
                        Gl.glColor4f(0, 0, 0, 0.4f);
                        Gl.glVertex3d(ox[j] + a * ox[j], -oy[j] - a * oy[j], oz[j] + a * oz[j]);
                        Gl.glNormal3d(-ox[j + 1], oy[j + 1], -oz[j + 1]);
                        Gl.glColor4f(0, 0, 0, 0.4f);
                        Gl.glVertex3d(ox[j + 1] + a * ox[j + 1], -oy[j + 1] - a * oy[j + 1], oz[j + 1] + a * oz[j + 1]);
                        Gl.glNormal3d(-x[1], y[1], -z[1]);
                        Gl.glColor4f(0, 0, 0, 0.4f);
                        Gl.glVertex3d(x[1] + a * x[1], -y[1] - a * y[1], z[1] + a * z[1]);
                    };

                    ox[j - 1] = x[0]; oy[j - 1] = y[0]; oz[j - 1] = z[0];
                    x[0] = x[1];
                    y[0] = y[1];
                    z[0] = z[1];
                }
                ox[stacks] = x[0]; oy[stacks] = y[0]; oz[stacks] = z[0];
                i++;
            };

            Gl.glPopMatrix();

            phis = null;
            phic = null;
            thetas = null;
            thetac = null;
            ox = null;
            oy = null;
            oz = null;
        }


        /* change this to customize effect */
        double spherePeek(double value)
        {
            double a;
            a = Math.Log(1 + Math.Pow(value, 0.3));
            a += 6 * (Math.Abs(value) - Math.Pow(Math.Abs(value), 4));
            //if ( a>1 ) a = 0;
            return a;
        }
        double sphereScaler(double value)
        {
            return 1 + 2 * Math.Pow(value, 2);//cos(log(1+1.2*sqrt(value)));
        }

        #endregion

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