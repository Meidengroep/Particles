// This file will conain notes i find useful throughout the development of this application.

// TODO: Implement saving and Loading music FilePaths

#region Fullscreen Code
/*
private void btnFullScreen_Click(object sender, EventArgs e)
{
    // Attempt Fullscreen Mode?
    Gdi.DEVMODE dmScreenSettings = new Gdi.DEVMODE();               // Device Mode
    // Size Of The Devmode Structure
    dmScreenSettings.dmSize = (short)Marshal.SizeOf(dmScreenSettings);
    dmScreenSettings.dmPelsWidth = 800;                           // Selected Screen Width
    dmScreenSettings.dmPelsHeight = 600;                         // Selected Screen Height
    dmScreenSettings.dmBitsPerPel = 16;                           // Selected Bits Per Pixel
    dmScreenSettings.dmFields = Gdi.DM_BITSPERPEL | Gdi.DM_PELSWIDTH | Gdi.DM_PELSHEIGHT;

    // Try To Set Selected Mode And Get Results.  NOTE: CDS_FULLSCREEN Gets Rid Of Start Bar.
    if (User.ChangeDisplaySettings(ref dmScreenSettings, User.CDS_FULLSCREEN) != User.DISP_CHANGE_SUCCESSFUL)
    {
        // If The Mode Fails, Offer Two Options.  Quit Or Use Windowed Mode.
        if (MessageBox.Show("The Requested Fullscreen Mode Is Not Supported By\nYour Video Card.  Use Windowed Mode Instead?", "NeHe GL",
            MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
        {
                 // Windowed Mode Selected.  Fullscreen = false
        }
        else
        {
            // Pop up A Message Box Lessing User Know The Program Is Closing.
            MessageBox.Show("Program Will Now Close.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            return;                                           // Return false
        }
    }

}
*/
#endregion Fullscreen Codes