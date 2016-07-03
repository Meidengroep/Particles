//using System;
//using System.Collections.Generic;
//using System.Drawing;
//using System.Linq;
//using System.Text;
//using System.Threading;
//using System.Windows.Forms;

//namespace Audio_Analyzing_CsGL_Tool.Source.Rendering
//{
//    /// <summary>
//    /// This is the generic OpenGL Control. How to use:
//    /// Have a CsGLControl field in the MainForm and initialize it
//    /// to any derived class from CsGLControl.
//    /// All derived classes from CsGLControl must have an overrided
//    /// Update(float dt) and glDraw() methods.
//    /// </summary>
//    class CsGLControl : OpenGLControl
//    {
//        #region Mouse and Keyboard Fields
//        /// <summary>
//        /// Current keyboard state.  The key's integer value, is the index for this array.  If that 
//        /// index is true, the key is being pressed, if it's false, the key is not being pressed.  You 
//        /// should likely mark the key as handled, by setting it's index to false when you've processed it.
//        /// </summary>
//        public static bool[] KeyState = new bool[256];									// Keyboard State

//        /// <summary>
//        /// Current mouse state.
//        /// </summary>
//        public struct Mouse
//        {
//            /// <summary>
//            /// X-axis position in the view.
//            /// </summary>
//            public static int X;

//            /// <summary>
//            /// Y-axis position in the view.
//            /// </summary>
//            public static int Y;

//            /// <summary>
//            /// Previous X-axis position in the view.
//            /// </summary>
//            public static int LastX;

//            /// <summary>
//            /// Previous Y-axis position in the view.
//            /// </summary>
//            public static int LastY;

//            /// <summary>
//            /// Difference between the current and previous X-axis position in the view.
//            /// </summary>
//            public static int DifferenceX;

//            /// <summary>
//            /// Difference between the current and previous Y-axis position in the view.
//            /// </summary>
//            public static int DifferenceY;

//            /// <summary>
//            /// Is left mouse button pressed?
//            /// </summary>
//            public static bool LeftButton;

//            /// <summary>
//            /// Is middle mouse button pressed?
//            /// </summary>
//            public static bool MiddleButton;

//            /// <summary>
//            /// Is right mouse button pressed?
//            /// </summary>
//            public static bool RightButton;

//            /// <summary>
//            /// Is X button 1 (Intellimouse) pressed?  Windows 2000 and above only.
//            /// </summary>
//            public static bool XButton1;

//            /// <summary>
//            /// Is X button 2 (Intellimouse) pressed?  Windows 2000 and above only.
//            /// </summary>
//            public static bool XButton2;
//        }
//        #endregion Mouse and Keyboard Fields

//        #region InitGLContext + Contructor

//        public CsGLControl() : base()
//        {

//        }


//        protected override void InitGLContext()
//        {
//            GL.glShadeModel(GL.GL_SMOOTH);
//            // Set Smooth Shading 
//            GL.glClearColor(0.0f, 0.0f, 0.0f, 0.5f);
//            // BackGround Color 
//            GL.glClearDepth(1.0f);
//            // Depth buffer setup 
//            GL.glEnable(GL.GL_DEPTH_TEST);
//            // Enables Depth Testing 
//            GL.glDepthFunc(GL.GL_LEQUAL);
//            // The Type Of Depth Test To Do 
//            GL.glHint(GL.GL_PERSPECTIVE_CORRECTION_HINT, GL.GL_NICEST);
//            /* Really Nice Perspective Calculations */
//        }

//        #endregion InitGLContext + Contructor

//        #region Update + Render + Input
//        // Draw function must be declared in Derived Classes.
//        // public virtual void glDraw() {}

//        public virtual void Update(float dt) { }


//        public virtual void ProcessInput(float dt) { }
//        #endregion Update and Render

//        #region OnSizeChanged
//        protected override void OnSizeChanged(EventArgs e)
//        {
//            base.OnSizeChanged(e);
//            Size s = Size;
//            double aspect_ratio = (double)s.Width / (double)s.Height;
//            GL.glMatrixMode(GL.GL_PROJECTION); // Select The Projection Matrix
//            GL.glLoadIdentity(); // Reset The Projection Matrix
//            // Calculate The Aspect Ratio Of The Window
//            GL.gluPerspective(45.0f, aspect_ratio, 0.1f, 100.0f);
//            GL.glMatrixMode(GL.GL_MODELVIEW); // Select The Modelview Matrix
//            GL.glLoadIdentity();// Reset The Modelview Matrix
//        }
//        #endregion Events (Declare them in Constructor)

//        #region Input Events

//        /// <summary>
//        /// Handles the MouseDown event.
//        /// </summary>
//        /// <param name="e">The MouseEventArgs.</param>
//        protected override void OnMouseDown(MouseEventArgs e)
//        {
//            if (e.Button == MouseButtons.Left)
//            {
//                Mouse.LeftButton = true;
//            }
//            else if (e.Button == MouseButtons.Middle)
//            {
//                Mouse.MiddleButton = true;
//            }
//            else if (e.Button == MouseButtons.Right)
//            {
//                Mouse.RightButton = true;
//            }
//        }

//        /// <summary>
//        /// Handles the MouseEnter event.
//        /// </summary>
//        /// <param name="e">The EventArgs.</param>
//        protected override void OnMouseEnter(EventArgs e)
//        {
//            Mouse.DifferenceX = 0;
//            Mouse.DifferenceY = 0;
//        }

//        /// <summary>
//        /// Handles the MouseMove event.
//        /// </summary>
//        /// <param name="e">The MouseEventArgs.</param>
//        protected override void OnMouseMove(MouseEventArgs e)
//        {
//            Mouse.LastX = Mouse.X;
//            Mouse.LastY = Mouse.Y;
//            Mouse.X = e.X;
//            Mouse.Y = e.Y;
//            Mouse.DifferenceX = Mouse.X - Mouse.LastX;
//            Mouse.DifferenceY = Mouse.Y - Mouse.LastY;
//        }

//        /// <summary>
//        /// Handles the MouseUp event.
//        /// </summary>
//        /// <param name="e">The MouseEventArgs.</param>
//        protected override void OnMouseUp(MouseEventArgs e)
//        {
//            if (e.Button == MouseButtons.Left)
//            {
//                Mouse.LeftButton = false;
//            }
//            else if (e.Button == MouseButtons.Middle)
//            {
//                Mouse.MiddleButton = false;
//            }
//            else if (e.Button == MouseButtons.Right)
//            {
//                Mouse.RightButton = false;
//            }
//            else if (e.Button == MouseButtons.XButton1)
//            {
//                Mouse.XButton1 = false;
//            }
//            else if (e.Button == MouseButtons.XButton2)
//            {
//                Mouse.XButton2 = false;
//            }
//        }

//        #endregion Input Events
//    }
//}


