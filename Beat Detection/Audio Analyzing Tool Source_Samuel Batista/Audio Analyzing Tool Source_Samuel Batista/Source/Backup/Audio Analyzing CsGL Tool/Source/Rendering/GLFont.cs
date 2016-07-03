/*
    //**************************************
    // for :OpenGL font renderer with formatting
    //**************************************

    Use it for whatever you want, I'd really
    apprechiate credit though as I worked hard to get this to work correctly.
    //**************************************
    // Name: OpenGL font renderer with formatting
    // Description:Renders text in OpenGL using the Windows API and
    the Tao Framework.
    I created it because I really needed a way to render
    some simple 2D text in my OpenGL apps with formatting
    and automatic text-alignment according to it's position
    on the screen, like Half-Life.
    // By: James J Kelly Jr
    //
    //
    // Inputs:Text is positioned by a floating point number, like Half-Life or Quake,
    insted of pixels.
    The text is centered at coordinate 0.0, 0.0.
    All the way to the left is -1.0, and to the right 1.0.
    All the way on the top is 1.0 and the bottom -1.0.
    Unfortunately the text has no alpha-blending support
    yet but I plan to add that in the future.
    Newlines (including \r, \n and \r\n) and
    tab spaces are supported!
    You can also colorize text by prefixing the text
    you want to colorize with ^ followed by a number 0 to 9, or - to return it to the previously used color e.g.:
    "^0Hello\n^5World^-!"
    Hello should be white and World should be green and
    the exclamation mark should be white again.
    //
    // Returns:None
    //
    //Assumes:You will need the Tao Framework to use this
    at http://www.taoframework.com
    You might be able to use it with a different OpenGL
    wrapper though.
    This code is not cross-platform, unfortunately, so
    I might update it to use FreeType in the future insted of the Windows API.
    //
    //Side Effects:I initially thought this code would be slow and
    severly effect the framerate but to my suprise I
    was able to render a fairly complicated string 30
    times per frame without even scratching the framerate.
    //This code is copyrighted and has limited warranties.
    //Please see http://www.Planet-Source-Code.com/xq/ASP/txtCodeId.6418/lngWId.10/qx/vb/scripts/ShowCode.htm
    //for details.
    //**************************************
    
    /*
    * Created by SharpDevelop.
    * User: James
    * Date: 2/26/2008
    * Time: 5:24 PM
    * 
    * To change this template use Tools | Options | Coding | Edit Standard Headers.
    */

using System;
using System.Drawing;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Audio_Analyzing_CsGL_Tool;
using System.IO;
using System.Text;

using Tao.FreeGlut;
using Tao.OpenGl;

namespace Tao.OpenGl
{

    public class GLFont : IDisposable
    {
        public enum COLORS
        {
            WHITE = 0,
            GREY,
            BLACK,
            RED,
            BLUE,
            GREEN,
            MAGENTA,
            YELLOW,
            CYAN,
            INDIGO,
            COUNT
        }

        private static float[,] COLORS_DATA = new float[,] {
    			{ 1.0f, 1.0f, 1.0f }, // White
    			{ 0.5f, 0.5f, 0.5f }, // Grey
    			{ 0.0f, 0.0f, 0.0f }, // Black
    			{ 1.0f, 0.0f, 0.0f }, // Red
    			{ 0.0f, 0.0f, 1.0f }, // Blue
    			{ 0.0f, 1.0f, 0.0f }, // Green
    			{ 1.0f, 0.0f, 1.0f }, // Magenta
    			{ 1.0f, 1.0f, 0.0f }, // Yellow
    			{ 0.0f, 1.0f, 1.0f }, // Cyan
    			{ 0.4f, 0.0f, 1.0f }, // Indigo
    		};

        private IntPtr fontFamily = Glut.GLUT_BITMAP_HELVETICA_12;
        private Size viewPort;

        /// <summary>
        /// Available Font Families:
        /// Glut.GLUT_BITMAP_8_BY_13, Glut.GLUT_BITMAP_9_BY_15, Glut.GLUT_BITMAP_HELVETICA_10,
        /// Glut.GLUT_BITMAP_HELVETICA_12, Glut.GLUT_BITMAP_HELVETICA_18, Glut.GLUT_BITMAP_TIMES_ROMAN_10,
        /// Glut.GLUT_BITMAP_TIMES_ROMAN_24.
        /// </summary>
        /// <param name="FontFamily"></param>
        /// <param name="ViewPortSize"></param>
        public GLFont(IntPtr FontFamily, Size ViewPortSize)
        {
            fontFamily = FontFamily;
            viewPort = ViewPortSize;
        }

        ~GLFont()
        {
            Dispose();
        }

        public void Resize(Size newViewPortSize)
        {
            viewPort = newViewPortSize;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public void Print(int x, int y, string value, COLORS color)
        {

            if (value == null || value.Length < 1) return;

            int lines = 0;

            /*
             * Prepare the OpenGL state
             */
            Gl.glDisable(Gl.GL_LIGHTING);
            Gl.glDisable(Gl.GL_DEPTH_TEST);
            Gl.glMatrixMode(Gl.GL_PROJECTION);
            Gl.glPushMatrix();
            Gl.glLoadIdentity();

            /*
             * Have an orthogonal projection matrix set
             */
            Gl.glOrtho(0, viewPort.Width,
                     0, viewPort.Height,
                     -1, +1
            );

            Gl.glScalef(1, -1, 1);
            // mover the origin from the bottom left corner
            // to the upper left corner
            Gl.glTranslatef(0, -viewPort.Height, 0);

            /*
             * Now the matrix mode
             */
            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            Gl.glPushMatrix();
            Gl.glLoadIdentity();

            /*
             * Now the main text
             */
            Gl.glColor3fv(ref COLORS_DATA[(int)color, 0]);
            Gl.glRasterPos2i(x, y);

            for (int i = 0; i < value.Length; i++)
            {
                char p = value[i];
                if (p == '\n')
                {
                    lines++;
                    Gl.glRasterPos2i(x, y - (lines * 18));
                }

                Glut.glutBitmapCharacter(fontFamily, (int)p);
            }

            /*
             * Revert to the old matrix modes
             */
            Gl.glMatrixMode(Gl.GL_PROJECTION);
            Gl.glPopMatrix();

            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            Gl.glPopMatrix();

            /*
             * Restore the old OpenGL states
             */
            Gl.glColor4f(1.0f, 1.0f, 1.0f, 1.0f);
            Gl.glEnable(Gl.GL_DEPTH_TEST);
            Gl.glEnable(Gl.GL_LIGHTING);
        }
    }
}

