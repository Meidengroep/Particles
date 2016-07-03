using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Particles_The_Next_Generation
{
    public static class FileBrowser
    {
        static OpenFileDialog dialog;

        public static void Init()
        {
            dialog = new OpenFileDialog();
            dialog.Multiselect = false;
            dialog.Title = "Select a song";
        }

        public static string GetUserFilePath()
        {
            dialog.ShowDialog();
            return dialog.FileName;
        }
    }
}
