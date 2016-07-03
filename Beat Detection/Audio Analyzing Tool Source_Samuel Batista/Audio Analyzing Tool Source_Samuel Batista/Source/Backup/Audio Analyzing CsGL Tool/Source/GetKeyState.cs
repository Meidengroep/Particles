using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Audio_Analyzing_CsGL_Tool.Source
{
    public class WinInput
    {
        public static bool IsKeyDown(Keys k)
        {
            return (GetAsyncKeyState((int)k) & 0x8000) != 0;
        }

        // Imported Win32-func

        [DllImport("User32.dll")]
        private static extern short GetAsyncKeyState(int keyCode);
    }
}