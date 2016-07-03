using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Tao.OpenGl;

namespace Audio_Analyzing_CsGL_Tool.Source.Rendering
{
    public class OpenGLRenderable
    {
        #region Constructor and Resize

        public OpenGLRenderable(Size openGlControlSize)
        {
            Gl.glShadeModel(Gl.GL_FLAT);                                      // Enable Smooth Shading
            Gl.glClearColor(0, 0, 0, 0.5f);                                     // Black Background
            Gl.glClearDepth(16);                                                 // Depth Buffer Setup
            Gl.glEnable(Gl.GL_DEPTH_TEST);                                      // Enables Depth Testing
            Gl.glDepthFunc(Gl.GL_LEQUAL);                                       // The Type Of Depth Testing To Do
            Gl.glHint(Gl.GL_PERSPECTIVE_CORRECTION_HINT, Gl.GL_NICEST);         // Really Nice Perspective Calculations

            Resize(openGlControlSize.Width, openGlControlSize.Height);
        }

        public virtual void Resize(int width, int height)
        {
            double aspect_ratio = (double)width / (double)height;
            Gl.glViewport(0, 0, width, height); 
            Gl.glMatrixMode(Gl.GL_PROJECTION); // Select The Projection Matrix
            Gl.glLoadIdentity(); // Reset The Projection Matrix
            // Calculate The Aspect Ratio Of The Window
            Glu.gluPerspective(45.0f, aspect_ratio, 0.1f, 100.0f);
            Gl.glMatrixMode(Gl.GL_MODELVIEW); // Select The Modelview Matrix
            Gl.glLoadIdentity();// Reset The Modelview Matrix           
        }

        #endregion Constructor and Resize

        #region Virtual Methods ( Initialize, Update, Render and Shutdown)

        public virtual void Initialize() { }

        public virtual void Update(float dt) { }

        public virtual void Render() { }

        public virtual void Shutdown() { }

        #endregion Virtual Methods ( Initialize, Update, Render and Shutdown)
    }
}
