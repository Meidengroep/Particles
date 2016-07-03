namespace Audio_Analyzing_CsGL_Tool
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.tabControlMain = new System.Windows.Forms.TabControl();
            this.tabAudio = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnClearMusicList = new System.Windows.Forms.Button();
            this.btnLoad = new System.Windows.Forms.Button();
            this.lstMusic = new System.Windows.Forms.ListBox();
            this.btnDirectory = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cmbBeatDetection = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.chkFFT = new System.Windows.Forms.CheckBox();
            this.btnShuffle = new System.Windows.Forms.CheckBox();
            this.chkLoop = new System.Windows.Forms.CheckBox();
            this.cmbFFTType = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbSpectrumSize = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tabRender = new System.Windows.Forms.TabPage();
            this.radButtonTest = new System.Windows.Forms.RadioButton();
            this.radButtonBeatDetection = new System.Windows.Forms.RadioButton();
            this.radButtonRotSpheres = new System.Windows.Forms.RadioButton();
            this.radButtonSpectrum = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.FPSLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.AnalysisPerSecondLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolTipFFT = new System.Windows.Forms.ToolTip(this.components);
            this.panel2 = new System.Windows.Forms.Panel();
            this.simpleOpenGlControl = new Tao.Platform.Windows.SimpleOpenGlControl();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.numVolume = new System.Windows.Forms.NumericUpDown();
            this.btnPrev = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnPlay = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.musicTrack = new System.Windows.Forms.TrackBar();
            this.tabControlMain.SuspendLayout();
            this.tabAudio.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabRender.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numVolume)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.musicTrack)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControlMain
            // 
            this.tabControlMain.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlMain.Controls.Add(this.tabAudio);
            this.tabControlMain.Controls.Add(this.tabRender);
            this.tabControlMain.Location = new System.Drawing.Point(806, 27);
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.SelectedIndex = 0;
            this.tabControlMain.Size = new System.Drawing.Size(348, 487);
            this.tabControlMain.TabIndex = 1;
            // 
            // tabAudio
            // 
            this.tabAudio.Controls.Add(this.groupBox3);
            this.tabAudio.Controls.Add(this.groupBox2);
            this.tabAudio.Location = new System.Drawing.Point(4, 22);
            this.tabAudio.Name = "tabAudio";
            this.tabAudio.Padding = new System.Windows.Forms.Padding(3);
            this.tabAudio.Size = new System.Drawing.Size(340, 461);
            this.tabAudio.TabIndex = 0;
            this.tabAudio.Text = "Audio Controls";
            this.tabAudio.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.btnClearMusicList);
            this.groupBox3.Controls.Add(this.btnLoad);
            this.groupBox3.Controls.Add(this.lstMusic);
            this.groupBox3.Controls.Add(this.btnDirectory);
            this.groupBox3.Location = new System.Drawing.Point(-4, 191);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(338, 287);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Audio Files";
            // 
            // btnClearMusicList
            // 
            this.btnClearMusicList.Location = new System.Drawing.Point(238, 19);
            this.btnClearMusicList.Name = "btnClearMusicList";
            this.btnClearMusicList.Size = new System.Drawing.Size(89, 23);
            this.btnClearMusicList.TabIndex = 13;
            this.btnClearMusicList.Text = "Clear List";
            this.btnClearMusicList.UseVisualStyleBackColor = true;
            this.btnClearMusicList.Click += new System.EventHandler(this.btnClearMusicList_Click);
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(12, 19);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(92, 23);
            this.btnLoad.TabIndex = 12;
            this.btnLoad.Text = "Load File(s)";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // lstMusic
            // 
            this.lstMusic.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lstMusic.FormattingEnabled = true;
            this.lstMusic.Location = new System.Drawing.Point(12, 47);
            this.lstMusic.Name = "lstMusic";
            this.lstMusic.Size = new System.Drawing.Size(315, 212);
            this.lstMusic.TabIndex = 11;
            this.lstMusic.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstMusic_MouseDoubleClick);
            // 
            // btnDirectory
            // 
            this.btnDirectory.Location = new System.Drawing.Point(110, 19);
            this.btnDirectory.Name = "btnDirectory";
            this.btnDirectory.Size = new System.Drawing.Size(92, 23);
            this.btnDirectory.TabIndex = 9;
            this.btnDirectory.Text = "Open Directory";
            this.btnDirectory.UseVisualStyleBackColor = true;
            this.btnDirectory.Click += new System.EventHandler(this.btnDirectory_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cmbBeatDetection);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.chkFFT);
            this.groupBox2.Controls.Add(this.btnShuffle);
            this.groupBox2.Controls.Add(this.chkLoop);
            this.groupBox2.Controls.Add(this.cmbFFTType);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.cmbSpectrumSize);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(3, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(334, 182);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "FMOD Controls";
            // 
            // cmbBeatDetection
            // 
            this.cmbBeatDetection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBeatDetection.FormattingEnabled = true;
            this.cmbBeatDetection.Items.AddRange(new object[] {
            "None",
            "Simple Sound Energy",
            "Frequency Selected Sound Energy"});
            this.cmbBeatDetection.Location = new System.Drawing.Point(104, 81);
            this.cmbBeatDetection.Name = "cmbBeatDetection";
            this.cmbBeatDetection.Size = new System.Drawing.Size(222, 21);
            this.cmbBeatDetection.TabIndex = 9;
            this.cmbBeatDetection.SelectedIndexChanged += new System.EventHandler(this.cmbBeatDetection_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 84);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Beat Detection: ";
            // 
            // chkFFT
            // 
            this.chkFFT.AutoSize = true;
            this.chkFFT.Checked = true;
            this.chkFFT.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkFFT.Location = new System.Drawing.Point(15, 145);
            this.chkFFT.Name = "chkFFT";
            this.chkFFT.Size = new System.Drawing.Size(114, 17);
            this.chkFFT.TabIndex = 7;
            this.chkFFT.Text = "Run FFT Algorithm";
            this.chkFFT.UseVisualStyleBackColor = true;
            this.chkFFT.CheckedChanged += new System.EventHandler(this.chkFFT_CheckedChanged);
            // 
            // btnShuffle
            // 
            this.btnShuffle.AutoSize = true;
            this.btnShuffle.Location = new System.Drawing.Point(276, 145);
            this.btnShuffle.Name = "btnShuffle";
            this.btnShuffle.Size = new System.Drawing.Size(59, 17);
            this.btnShuffle.TabIndex = 5;
            this.btnShuffle.Text = "Shuffle";
            this.btnShuffle.UseVisualStyleBackColor = true;
            this.btnShuffle.CheckedChanged += new System.EventHandler(this.btnShuffle_CheckedChanged);
            // 
            // chkLoop
            // 
            this.chkLoop.AutoSize = true;
            this.chkLoop.Checked = true;
            this.chkLoop.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkLoop.Location = new System.Drawing.Point(220, 145);
            this.chkLoop.Name = "chkLoop";
            this.chkLoop.Size = new System.Drawing.Size(50, 17);
            this.chkLoop.TabIndex = 4;
            this.chkLoop.Text = "Loop";
            this.chkLoop.UseVisualStyleBackColor = true;
            this.chkLoop.CheckedChanged += new System.EventHandler(this.chkLoop_CheckedChanged);
            // 
            // cmbFFTType
            // 
            this.cmbFFTType.AccessibleDescription = "";
            this.cmbFFTType.Cursor = System.Windows.Forms.Cursors.Default;
            this.cmbFFTType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFFTType.FormattingEnabled = true;
            this.cmbFFTType.Items.AddRange(new object[] {
            "FFT_WINDOW_RECT",
            "FFT_WINDOW_TRIANGlE",
            "FFT_WINDOW_HAMMING",
            "FFT_WINDOW_HANNING",
            "FFT_WINDOW_BLACKMAN",
            "FFT_WINDOW_BLACKMANHARRIS"});
            this.cmbFFTType.Location = new System.Drawing.Point(118, 54);
            this.cmbFFTType.Name = "cmbFFTType";
            this.cmbFFTType.Size = new System.Drawing.Size(208, 21);
            this.cmbFFTType.TabIndex = 3;
            this.toolTipFFT.SetToolTip(this.cmbFFTType, resources.GetString("cmbFFTType.ToolTip"));
            this.cmbFFTType.SelectedIndexChanged += new System.EventHandler(this.cmbFFTType_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 57);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "FFT Window Type:";
            // 
            // cmbSpectrumSize
            // 
            this.cmbSpectrumSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSpectrumSize.FormattingEnabled = true;
            this.cmbSpectrumSize.Items.AddRange(new object[] {
            "256",
            "512",
            "1024",
            "2048"});
            this.cmbSpectrumSize.Location = new System.Drawing.Point(134, 27);
            this.cmbSpectrumSize.Name = "cmbSpectrumSize";
            this.cmbSpectrumSize.Size = new System.Drawing.Size(192, 21);
            this.cmbSpectrumSize.TabIndex = 1;
            this.cmbSpectrumSize.SelectedIndexChanged += new System.EventHandler(this.cmbSpectrumSize_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(116, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Spectrum Sample Size:";
            // 
            // tabRender
            // 
            this.tabRender.Controls.Add(this.radButtonTest);
            this.tabRender.Controls.Add(this.radButtonBeatDetection);
            this.tabRender.Controls.Add(this.radButtonRotSpheres);
            this.tabRender.Controls.Add(this.radButtonSpectrum);
            this.tabRender.Location = new System.Drawing.Point(4, 22);
            this.tabRender.Name = "tabRender";
            this.tabRender.Padding = new System.Windows.Forms.Padding(3);
            this.tabRender.Size = new System.Drawing.Size(340, 461);
            this.tabRender.TabIndex = 1;
            this.tabRender.Text = "Render Scenes";
            this.tabRender.UseVisualStyleBackColor = true;
            // 
            // radButtonTest
            // 
            this.radButtonTest.AutoSize = true;
            this.radButtonTest.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radButtonTest.Location = new System.Drawing.Point(38, 233);
            this.radButtonTest.Name = "radButtonTest";
            this.radButtonTest.Size = new System.Drawing.Size(53, 20);
            this.radButtonTest.TabIndex = 3;
            this.radButtonTest.Text = "Test";
            this.radButtonTest.UseVisualStyleBackColor = true;
            this.radButtonTest.CheckedChanged += new System.EventHandler(this.radButtonTest_CheckedChanged);
            // 
            // radButtonBeatDetection
            // 
            this.radButtonBeatDetection.AutoSize = true;
            this.radButtonBeatDetection.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radButtonBeatDetection.Location = new System.Drawing.Point(38, 172);
            this.radButtonBeatDetection.Name = "radButtonBeatDetection";
            this.radButtonBeatDetection.Size = new System.Drawing.Size(114, 20);
            this.radButtonBeatDetection.TabIndex = 2;
            this.radButtonBeatDetection.Text = "Beat Detection";
            this.radButtonBeatDetection.UseVisualStyleBackColor = true;
            this.radButtonBeatDetection.CheckedChanged += new System.EventHandler(this.radButtonBeatDetection_CheckedChanged);
            // 
            // radButtonRotSpheres
            // 
            this.radButtonRotSpheres.AutoSize = true;
            this.radButtonRotSpheres.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radButtonRotSpheres.Location = new System.Drawing.Point(38, 109);
            this.radButtonRotSpheres.Name = "radButtonRotSpheres";
            this.radButtonRotSpheres.Size = new System.Drawing.Size(130, 20);
            this.radButtonRotSpheres.TabIndex = 1;
            this.radButtonRotSpheres.Text = "Rotating Spheres";
            this.radButtonRotSpheres.UseVisualStyleBackColor = true;
            this.radButtonRotSpheres.CheckedChanged += new System.EventHandler(this.radButtonRotSpheres_CheckedChanged);
            // 
            // radButtonSpectrum
            // 
            this.radButtonSpectrum.AutoSize = true;
            this.radButtonSpectrum.Checked = true;
            this.radButtonSpectrum.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radButtonSpectrum.Location = new System.Drawing.Point(38, 46);
            this.radButtonSpectrum.Name = "radButtonSpectrum";
            this.radButtonSpectrum.Size = new System.Drawing.Size(139, 20);
            this.radButtonSpectrum.TabIndex = 0;
            this.radButtonSpectrum.TabStop = true;
            this.radButtonSpectrum.Text = "The Look of Sound";
            this.radButtonSpectrum.UseVisualStyleBackColor = true;
            this.radButtonSpectrum.CheckedChanged += new System.EventHandler(this.radButtonSpectrum_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
            this.panel1.Location = new System.Drawing.Point(637, 635);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 100);
            this.panel1.TabIndex = 22;
            this.panel1.Visible = false;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FPSLabel,
            this.AnalysisPerSecondLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 629);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1154, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // FPSLabel
            // 
            this.FPSLabel.AutoSize = false;
            this.FPSLabel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FPSLabel.Margin = new System.Windows.Forms.Padding(10, 3, 0, 2);
            this.FPSLabel.Name = "FPSLabel";
            this.FPSLabel.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.FPSLabel.Padding = new System.Windows.Forms.Padding(0, 0, 20, 0);
            this.FPSLabel.Size = new System.Drawing.Size(66, 17);
            this.FPSLabel.Text = "FPS: 0";
            this.FPSLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // AnalysisPerSecondLabel
            // 
            this.AnalysisPerSecondLabel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AnalysisPerSecondLabel.Name = "AnalysisPerSecondLabel";
            this.AnalysisPerSecondLabel.Padding = new System.Windows.Forms.Padding(50, 0, 50, 0);
            this.AnalysisPerSecondLabel.Size = new System.Drawing.Size(298, 17);
            this.AnalysisPerSecondLabel.Text = "Spectrum Analysis Per Second: 0";
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.menuStrip.Size = new System.Drawing.Size(1154, 24);
            this.menuStrip.TabIndex = 4;
            this.menuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(92, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            this.helpToolStripMenuItem.Text = "&About...";
            this.helpToolStripMenuItem.Click += new System.EventHandler(this.helpToolStripMenuItem_Click);
            // 
            // toolTipFFT
            // 
            this.toolTipFFT.AutoPopDelay = 5000000;
            this.toolTipFFT.InitialDelay = 500;
            this.toolTipFFT.ReshowDelay = 100;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.simpleOpenGlControl);
            this.panel2.Location = new System.Drawing.Point(0, 27);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(800, 600);
            this.panel2.TabIndex = 5;
            // 
            // simpleOpenGlControl
            // 
            this.simpleOpenGlControl.AccumBits = ((byte)(0));
            this.simpleOpenGlControl.AutoCheckErrors = false;
            this.simpleOpenGlControl.AutoFinish = false;
            this.simpleOpenGlControl.AutoMakeCurrent = true;
            this.simpleOpenGlControl.AutoSwapBuffers = true;
            this.simpleOpenGlControl.BackColor = System.Drawing.Color.Black;
            this.simpleOpenGlControl.ColorBits = ((byte)(32));
            this.simpleOpenGlControl.DepthBits = ((byte)(16));
            this.simpleOpenGlControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.simpleOpenGlControl.Location = new System.Drawing.Point(0, 0);
            this.simpleOpenGlControl.Name = "simpleOpenGlControl";
            this.simpleOpenGlControl.Size = new System.Drawing.Size(800, 600);
            this.simpleOpenGlControl.StencilBits = ((byte)(0));
            this.simpleOpenGlControl.TabIndex = 0;
            this.simpleOpenGlControl.Paint += new System.Windows.Forms.PaintEventHandler(this.simpleOpenGlControl_Paint);
            this.simpleOpenGlControl.MouseMove += new System.Windows.Forms.MouseEventHandler(this.simpleOpenGlControl_MouseMove);
            this.simpleOpenGlControl.Leave += new System.EventHandler(this.simpleOpenGlControl_Leave);
            this.simpleOpenGlControl.KeyUp += new System.Windows.Forms.KeyEventHandler(this.simpleOpenGlControl_KeyUp);
            this.simpleOpenGlControl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.simpleOpenGlControl_MouseDown);
            this.simpleOpenGlControl.Enter += new System.EventHandler(this.simpleOpenGlControl_Enter);
            this.simpleOpenGlControl.MouseUp += new System.Windows.Forms.MouseEventHandler(this.simpleOpenGlControl_MouseUp);
            this.simpleOpenGlControl.SizeChanged += new System.EventHandler(this.simpleOpenGlControl_SizeChanged);
            this.simpleOpenGlControl.MouseEnter += new System.EventHandler(this.simpleOpenGlControl_MouseEnter);
            this.simpleOpenGlControl.KeyDown += new System.Windows.Forms.KeyEventHandler(this.simpleOpenGlControl_KeyDown);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.numVolume);
            this.groupBox1.Controls.Add(this.btnPrev);
            this.groupBox1.Controls.Add(this.btnNext);
            this.groupBox1.Controls.Add(this.btnPlay);
            this.groupBox1.Controls.Add(this.btnStop);
            this.groupBox1.Controls.Add(this.musicTrack);
            this.groupBox1.Location = new System.Drawing.Point(806, 516);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(340, 106);
            this.groupBox1.TabIndex = 23;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Media Controls";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(251, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 16);
            this.label1.TabIndex = 20;
            this.label1.Text = "Vol:";
            // 
            // numVolume
            // 
            this.numVolume.DecimalPlaces = 2;
            this.numVolume.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numVolume.Location = new System.Drawing.Point(288, 40);
            this.numVolume.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numVolume.Name = "numVolume";
            this.numVolume.Size = new System.Drawing.Size(42, 20);
            this.numVolume.TabIndex = 19;
            this.numVolume.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numVolume.ValueChanged += new System.EventHandler(this.numVolume_ValueChanged);
            // 
            // btnPrev
            // 
            this.btnPrev.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnPrev.BackgroundImage")));
            this.btnPrev.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPrev.Location = new System.Drawing.Point(72, 28);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new System.Drawing.Size(40, 40);
            this.btnPrev.TabIndex = 18;
            this.btnPrev.UseVisualStyleBackColor = true;
            this.btnPrev.Click += new System.EventHandler(this.btnPrev_Click);
            // 
            // btnNext
            // 
            this.btnNext.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnNext.BackgroundImage")));
            this.btnNext.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnNext.Location = new System.Drawing.Point(194, 28);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(40, 40);
            this.btnNext.TabIndex = 17;
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnPlay
            // 
            this.btnPlay.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnPlay.BackgroundImage")));
            this.btnPlay.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPlay.Location = new System.Drawing.Point(121, 13);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(64, 64);
            this.btnPlay.TabIndex = 16;
            this.btnPlay.UseVisualStyleBackColor = true;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // btnStop
            // 
            this.btnStop.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnStop.BackgroundImage")));
            this.btnStop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnStop.Location = new System.Drawing.Point(15, 24);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(48, 48);
            this.btnStop.TabIndex = 15;
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // musicTrack
            // 
            this.musicTrack.BackColor = System.Drawing.SystemColors.ControlLight;
            this.musicTrack.Cursor = System.Windows.Forms.Cursors.Hand;
            this.musicTrack.Location = new System.Drawing.Point(5, 78);
            this.musicTrack.Maximum = 1000;
            this.musicTrack.Name = "musicTrack";
            this.musicTrack.Size = new System.Drawing.Size(332, 45);
            this.musicTrack.TabIndex = 21;
            this.musicTrack.TickStyle = System.Windows.Forms.TickStyle.None;
            this.musicTrack.Scroll += new System.EventHandler(this.musicTrack_Scroll);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1154, 651);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip);
            this.Controls.Add(this.tabControlMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip;
            this.Name = "MainForm";
            this.Text = "Samuel Batista - Audio Analyzing Tool";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.tabControlMain.ResumeLayout(false);
            this.tabAudio.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tabRender.ResumeLayout(false);
            this.tabRender.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numVolume)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.musicTrack)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControlMain;
        private System.Windows.Forms.TabPage tabAudio;
        private System.Windows.Forms.TabPage tabRender;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel FPSLabel;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbSpectrumSize;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbFFTType;
        private System.Windows.Forms.ListBox lstMusic;
        private System.Windows.Forms.Button btnDirectory;
        private System.Windows.Forms.ToolTip toolTipFFT;
        private System.Windows.Forms.CheckBox btnShuffle;
        private System.Windows.Forms.CheckBox chkLoop;
        private System.Windows.Forms.Panel panel2;
        private Tao.Platform.Windows.SimpleOpenGlControl simpleOpenGlControl;
        private System.Windows.Forms.ToolStripStatusLabel AnalysisPerSecondLabel;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.Button btnClearMusicList;
        private System.Windows.Forms.CheckBox chkFFT;
        private System.Windows.Forms.RadioButton radButtonRotSpheres;
        private System.Windows.Forms.RadioButton radButtonSpectrum;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numVolume;
        private System.Windows.Forms.Button btnPrev;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.TrackBar musicTrack;
        private System.Windows.Forms.RadioButton radButtonBeatDetection;
        private System.Windows.Forms.ComboBox cmbBeatDetection;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton radButtonTest;
    }
}

