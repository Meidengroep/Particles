using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;


namespace Particles_The_Next_Generation
{
    static class DragManager
    {
        static bool someoneDragging;
        static int whosDragging;
        static int currentFreeIndex;

        public static void Init()
        {
            currentFreeIndex = int.MinValue;
        }

        public static int ObtainIndex()
        {
            return currentFreeIndex++;
        }

        public static void NewDragger(int id)
        {
            someoneDragging = true;
            whosDragging = id;
        }

        public static void Undrag()
        {
            someoneDragging = false;
        }

        public static bool SomeoneDragging
        {
            get { return someoneDragging; }
            set { someoneDragging = value; }
        }

        public static int WhosDragging
        {
            get { return whosDragging; }
            set { whosDragging = value; }
        }
    }
}
