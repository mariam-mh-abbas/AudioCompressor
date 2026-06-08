namespace AudioCompressor
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.panelMain = new System.Windows.Forms.Panel();
            this.panelTop = new System.Windows.Forms.Panel();
            this.lblStatus = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnDecompress = new System.Windows.Forms.Button();
            this.btnCompress = new System.Windows.Forms.Button();
            this.btnOpenFile = new System.Windows.Forms.Button();
            this.panelContent = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panelLeft = new System.Windows.Forms.Panel();
            this.grpFileInfo = new System.Windows.Forms.GroupBox();
            this.lblEncodingInfo = new System.Windows.Forms.Label();
            this.lblBitsPerSampleInfo = new System.Windows.Forms.Label();
            this.lblBitRateInfo = new System.Windows.Forms.Label();
            this.lblChannelsInfo = new System.Windows.Forms.Label();
            this.lblSampleRateInfo = new System.Windows.Forms.Label();
            this.lblDuration = new System.Windows.Forms.Label();
            this.lblFileSize = new System.Windows.Forms.Label();
            this.lblFileName = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.grpPlayback = new System.Windows.Forms.GroupBox();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnPause = new System.Windows.Forms.Button();
            this.btnPlay = new System.Windows.Forms.Button();
            this.picWaveform = new System.Windows.Forms.PictureBox();
            this.grpSettings = new System.Windows.Forms.GroupBox();
            this.panelAdaptiveDeltaSettings = new System.Windows.Forms.Panel();
            this.label19 = new System.Windows.Forms.Label();
            this.txtMaxStep = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.txtMinStep = new System.Windows.Forms.TextBox();
            this.panelDeltaSettings = new System.Windows.Forms.Panel();
            this.label17 = new System.Windows.Forms.Label();
            this.txtDeltaStep = new System.Windows.Forms.TextBox();
            this.panelPredictiveSettings = new System.Windows.Forms.Panel();
            this.label16 = new System.Windows.Forms.Label();
            this.cboPredictionOrder = new System.Windows.Forms.ComboBox();
            this.panelDpcmSettings = new System.Windows.Forms.Panel();
            this.label15 = new System.Windows.Forms.Label();
            this.cboDpcmBits = new System.Windows.Forms.ComboBox();
            this.panelQuantSettings = new System.Windows.Forms.Panel();
            this.label14 = new System.Windows.Forms.Label();
            this.txtMu = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.cboQuantLevels = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.cboAlgorithm = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtBitsPerSample = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtChannels = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtSampleRate = new System.Windows.Forms.TextBox();
            this.panelRight = new System.Windows.Forms.Panel();
            this.grpCharts = new System.Windows.Forms.GroupBox();
            this.plotSpeed = new OxyPlot.WindowsForms.PlotView();
            this.plotRatio = new OxyPlot.WindowsForms.PlotView();
            this.grpResults = new System.Windows.Forms.GroupBox();
            this.lblTimeElapsed = new System.Windows.Forms.Label();
            this.lblCompressedSize = new System.Windows.Forms.Label();
            this.lblOriginalSize = new System.Windows.Forms.Label();
            this.lblSpaceSaving = new System.Windows.Forms.Label();
            this.lblCompressionRatio = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.grpProgress = new System.Windows.Forms.GroupBox();
            this.lblProgressPercent = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.panelMain.SuspendLayout();
            this.panelTop.SuspendLayout();
            this.panelContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panelLeft.SuspendLayout();
            this.grpFileInfo.SuspendLayout();
            this.grpPlayback.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picWaveform)).BeginInit();
            this.grpSettings.SuspendLayout();
            this.panelAdaptiveDeltaSettings.SuspendLayout();
            this.panelDeltaSettings.SuspendLayout();
            this.panelPredictiveSettings.SuspendLayout();
            this.panelDpcmSettings.SuspendLayout();
            this.panelQuantSettings.SuspendLayout();
            this.panelRight.SuspendLayout();
            this.grpCharts.SuspendLayout();
            this.grpResults.SuspendLayout();
            this.grpProgress.SuspendLayout();
            this.SuspendLayout();
            //
            // panelMain
            //
            this.panelMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(245)))));
            this.panelMain.Controls.Add(this.panelContent);
            this.panelMain.Controls.Add(this.panelTop);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(1280, 720);
            this.panelMain.TabIndex = 0;
            //
            // panelTop
            //
            this.panelTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(45)))));
            this.panelTop.Controls.Add(this.lblStatus);
            this.panelTop.Controls.Add(this.btnSave);
            this.panelTop.Controls.Add(this.btnReset);
            this.panelTop.Controls.Add(this.btnCancel);
            this.panelTop.Controls.Add(this.btnDecompress);
            this.panelTop.Controls.Add(this.btnCompress);
            this.panelTop.Controls.Add(this.btnOpenFile);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Padding = new System.Windows.Forms.Padding(10);
            this.panelTop.Size = new System.Drawing.Size(1280, 60);
            this.panelTop.TabIndex = 0;
            //
            // lblStatus
            //
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(200)))));
            this.lblStatus.Location = new System.Drawing.Point(700, 22);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(79, 15);
            this.lblStatus.TabIndex = 6;
            this.lblStatus.Text = "No file loaded";
            //
            // btnSave
            //
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(134)))), ((int)(((byte)(193)))));
            this.btnSave.Enabled = false;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(580, 12);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 36);
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "💾 Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            //
            // btnReset
            //
            this.btnReset.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.btnReset.Enabled = false;
            this.btnReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReset.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnReset.ForeColor = System.Drawing.Color.White;
            this.btnReset.Location = new System.Drawing.Point(470, 12);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(100, 36);
            this.btnReset.TabIndex = 4;
            this.btnReset.Text = "↺ Reset";
            this.btnReset.UseVisualStyleBackColor = false;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            //
            // btnCancel
            //
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(57)))), ((int)(((byte)(43)))));
            this.btnCancel.Enabled = false;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(360, 12);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 36);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "✕ Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            //
            // btnDecompress
            //
            this.btnDecompress.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(174)))), ((int)(((byte)(96)))));
            this.btnDecompress.Enabled = false;
            this.btnDecompress.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDecompress.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnDecompress.ForeColor = System.Drawing.Color.White;
            this.btnDecompress.Location = new System.Drawing.Point(250, 12);
            this.btnDecompress.Name = "btnDecompress";
            this.btnDecompress.Size = new System.Drawing.Size(100, 36);
            this.btnDecompress.TabIndex = 2;
            this.btnDecompress.Text = "↓ Decompress";
            this.btnDecompress.UseVisualStyleBackColor = false;
            this.btnDecompress.Click += new System.EventHandler(this.btnDecompress_Click);
            //
            // btnCompress
            //
            this.btnCompress.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.btnCompress.Enabled = false;
            this.btnCompress.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCompress.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnCompress.ForeColor = System.Drawing.Color.White;
            this.btnCompress.Location = new System.Drawing.Point(140, 12);
            this.btnCompress.Name = "btnCompress";
            this.btnCompress.Size = new System.Drawing.Size(100, 36);
            this.btnCompress.TabIndex = 1;
            this.btnCompress.Text = "↑ Compress";
            this.btnCompress.UseVisualStyleBackColor = false;
            this.btnCompress.Click += new System.EventHandler(this.btnCompress_Click);
            //
            // btnOpenFile
            //
            this.btnOpenFile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnOpenFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOpenFile.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnOpenFile.ForeColor = System.Drawing.Color.White;
            this.btnOpenFile.Location = new System.Drawing.Point(15, 12);
            this.btnOpenFile.Name = "btnOpenFile";
            this.btnOpenFile.Size = new System.Drawing.Size(115, 36);
            this.btnOpenFile.TabIndex = 0;
            this.btnOpenFile.Text = "📂 Open File";
            this.btnOpenFile.UseVisualStyleBackColor = false;
            this.btnOpenFile.Click += new System.EventHandler(this.btnOpenFile_Click);
            //
            // panelContent
            //
            this.panelContent.Controls.Add(this.splitContainer1);
            this.panelContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContent.Location = new System.Drawing.Point(0, 60);
            this.panelContent.Name = "panelContent";
            this.panelContent.Size = new System.Drawing.Size(1280, 660);
            this.panelContent.TabIndex = 1;
            //
            // splitContainer1
            //
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            //
            // splitContainer1.Panel1
            //
            this.splitContainer1.Panel1.Controls.Add(this.panelLeft);
            this.splitContainer1.Panel1MinSize = 380;
            //
            // splitContainer1.Panel2
            //
            this.splitContainer1.Panel2.Controls.Add(this.panelRight);
            this.splitContainer1.Panel2MinSize = 450;
            this.splitContainer1.Size = new System.Drawing.Size(1280, 660);
            this.splitContainer1.SplitterDistance = 480;
            this.splitContainer1.TabIndex = 0;
            //
            // panelLeft
            //
            this.panelLeft.AutoScroll = true;
            this.panelLeft.Controls.Add(this.grpFileInfo);
            this.panelLeft.Controls.Add(this.grpPlayback);
            this.panelLeft.Controls.Add(this.grpSettings);
            this.panelLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelLeft.Location = new System.Drawing.Point(0, 0);
            this.panelLeft.Name = "panelLeft";
            this.panelLeft.Padding = new System.Windows.Forms.Padding(8);
            this.panelLeft.Size = new System.Drawing.Size(480, 660);
            this.panelLeft.TabIndex = 0;
            //
            // grpFileInfo
            //
            this.grpFileInfo.BackColor = System.Drawing.Color.White;
            this.grpFileInfo.Controls.Add(this.lblEncodingInfo);
            this.grpFileInfo.Controls.Add(this.lblBitsPerSampleInfo);
            this.grpFileInfo.Controls.Add(this.lblBitRateInfo);
            this.grpFileInfo.Controls.Add(this.lblChannelsInfo);
            this.grpFileInfo.Controls.Add(this.lblSampleRateInfo);
            this.grpFileInfo.Controls.Add(this.lblDuration);
            this.grpFileInfo.Controls.Add(this.lblFileSize);
            this.grpFileInfo.Controls.Add(this.lblFileName);
            this.grpFileInfo.Controls.Add(this.label8);
            this.grpFileInfo.Controls.Add(this.label7);
            this.grpFileInfo.Controls.Add(this.label6);
            this.grpFileInfo.Controls.Add(this.label5);
            this.grpFileInfo.Controls.Add(this.label4);
            this.grpFileInfo.Controls.Add(this.label3);
            this.grpFileInfo.Controls.Add(this.label2);
            this.grpFileInfo.Controls.Add(this.label1);
            this.grpFileInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpFileInfo.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.grpFileInfo.Location = new System.Drawing.Point(8, 8);
            this.grpFileInfo.Name = "grpFileInfo";
            this.grpFileInfo.Size = new System.Drawing.Size(464, 195);
            this.grpFileInfo.TabIndex = 0;
            this.grpFileInfo.TabStop = false;
            this.grpFileInfo.Text = "File Information";
            //
            // lblEncodingInfo
            //
            this.lblEncodingInfo.AutoSize = true;
            this.lblEncodingInfo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblEncodingInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.lblEncodingInfo.Location = new System.Drawing.Point(140, 165);
            this.lblEncodingInfo.Name = "lblEncodingInfo";
            this.lblEncodingInfo.Size = new System.Drawing.Size(19, 15);
            this.lblEncodingInfo.TabIndex = 15;
            this.lblEncodingInfo.Text = "--";
            //
            // lblBitsPerSampleInfo
            //
            this.lblBitsPerSampleInfo.AutoSize = true;
            this.lblBitsPerSampleInfo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblBitsPerSampleInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.lblBitsPerSampleInfo.Location = new System.Drawing.Point(140, 140);
            this.lblBitsPerSampleInfo.Name = "lblBitsPerSampleInfo";
            this.lblBitsPerSampleInfo.Size = new System.Drawing.Size(19, 15);
            this.lblBitsPerSampleInfo.TabIndex = 14;
            this.lblBitsPerSampleInfo.Text = "--";
            //
            // lblBitRateInfo
            //
            this.lblBitRateInfo.AutoSize = true;
            this.lblBitRateInfo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblBitRateInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.lblBitRateInfo.Location = new System.Drawing.Point(140, 115);
            this.lblBitRateInfo.Name = "lblBitRateInfo";
            this.lblBitRateInfo.Size = new System.Drawing.Size(19, 15);
            this.lblBitRateInfo.TabIndex = 13;
            this.lblBitRateInfo.Text = "--";
            //
            // lblChannelsInfo
            //
            this.lblChannelsInfo.AutoSize = true;
            this.lblChannelsInfo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblChannelsInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.lblChannelsInfo.Location = new System.Drawing.Point(140, 90);
            this.lblChannelsInfo.Name = "lblChannelsInfo";
            this.lblChannelsInfo.Size = new System.Drawing.Size(19, 15);
            this.lblChannelsInfo.TabIndex = 12;
            this.lblChannelsInfo.Text = "--";
            //
            // lblSampleRateInfo
            //
            this.lblSampleRateInfo.AutoSize = true;
            this.lblSampleRateInfo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblSampleRateInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.lblSampleRateInfo.Location = new System.Drawing.Point(140, 65);
            this.lblSampleRateInfo.Name = "lblSampleRateInfo";
            this.lblSampleRateInfo.Size = new System.Drawing.Size(19, 15);
            this.lblSampleRateInfo.TabIndex = 11;
            this.lblSampleRateInfo.Text = "--";
            //
            // lblDuration
            //
            this.lblDuration.AutoSize = true;
            this.lblDuration.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblDuration.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.lblDuration.Location = new System.Drawing.Point(140, 40);
            this.lblDuration.Name = "lblDuration";
            this.lblDuration.Size = new System.Drawing.Size(19, 15);
            this.lblDuration.TabIndex = 10;
            this.lblDuration.Text = "--";
            //
            // lblFileSize
            //
            this.lblFileSize.AutoSize = true;
            this.lblFileSize.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblFileSize.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.lblFileSize.Location = new System.Drawing.Point(140, 15);
            this.lblFileSize.Name = "lblFileSize";
            this.lblFileSize.Size = new System.Drawing.Size(19, 15);
            this.lblFileSize.TabIndex = 9;
            this.lblFileSize.Text = "--";
            //
            // lblFileName
            //
            this.lblFileName.AutoSize = true;
            this.lblFileName.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lblFileName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(45)))));
            this.lblFileName.Location = new System.Drawing.Point(5, -2);
            this.lblFileName.Name = "lblFileName";
            this.lblFileName.Size = new System.Drawing.Size(56, 19);
            this.lblFileName.TabIndex = 8;
            this.lblFileName.Text = "No File";
            //
            // label8
            //
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(5, 165);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(60, 15);
            this.label8.TabIndex = 7;
            this.label8.Text = "Encoding:";
            //
            // label7
            //
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(5, 140);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(88, 15);
            this.label7.TabIndex = 6;
            this.label7.Text = "Bits/Sample:";
            //
            // label6
            //
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(5, 115);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(57, 15);
            this.label6.TabIndex = 5;
            this.label6.Text = "Bit Rate:";
            //
            // label5
            //
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(5, 90);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 15);
            this.label5.TabIndex = 4;
            this.label5.Text = "Channels:";
            //
            // label4
            //
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(5, 65);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 15);
            this.label4.TabIndex = 3;
            this.label4.Text = "Sample Rate:";
            //
            // label3
            //
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 40);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 15);
            this.label3.TabIndex = 2;
            this.label3.Text = "Duration:";
            //
            // label2
            //
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "Size:";
            //
            // label1
            //
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 15);
            this.label1.TabIndex = 0;
            //
            // grpPlayback
            //
            this.grpPlayback.BackColor = System.Drawing.Color.White;
            this.grpPlayback.Controls.Add(this.picWaveform);
            this.grpPlayback.Controls.Add(this.btnStop);
            this.grpPlayback.Controls.Add(this.btnPause);
            this.grpPlayback.Controls.Add(this.btnPlay);
            this.grpPlayback.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpPlayback.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.grpPlayback.Location = new System.Drawing.Point(8, 203);
            this.grpPlayback.Name = "grpPlayback";
            this.grpPlayback.Size = new System.Drawing.Size(464, 160);
            this.grpPlayback.TabIndex = 1;
            this.grpPlayback.TabStop = false;
            this.grpPlayback.Text = "Audio Playback";
            //
            // btnStop
            //
            this.btnStop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.btnStop.Enabled = false;
            this.btnStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStop.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnStop.ForeColor = System.Drawing.Color.White;
            this.btnStop.Location = new System.Drawing.Point(155, 14);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(65, 28);
            this.btnStop.TabIndex = 3;
            this.btnStop.Text = "■ Stop";
            this.btnStop.UseVisualStyleBackColor = false;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            //
            // btnPause
            //
            this.btnPause.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(156)))), ((int)(((byte)(18)))));
            this.btnPause.Enabled = false;
            this.btnPause.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPause.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnPause.ForeColor = System.Drawing.Color.White;
            this.btnPause.Location = new System.Drawing.Point(80, 14);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(65, 28);
            this.btnPause.TabIndex = 2;
            this.btnPause.Text = "⏸ Pause";
            this.btnPause.UseVisualStyleBackColor = false;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            //
            // btnPlay
            //
            this.btnPlay.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(174)))), ((int)(((byte)(96)))));
            this.btnPlay.Enabled = false;
            this.btnPlay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPlay.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnPlay.ForeColor = System.Drawing.Color.White;
            this.btnPlay.Location = new System.Drawing.Point(9, 14);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(65, 28);
            this.btnPlay.TabIndex = 1;
            this.btnPlay.Text = "▶ Play";
            this.btnPlay.UseVisualStyleBackColor = false;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            //
            // picWaveform
            //
            this.picWaveform.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(45)))));
            this.picWaveform.Location = new System.Drawing.Point(9, 48);
            this.picWaveform.Name = "picWaveform";
            this.picWaveform.Size = new System.Drawing.Size(446, 100);
            this.picWaveform.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picWaveform.TabIndex = 4;
            this.picWaveform.TabStop = false;
            //
            // grpSettings
            //
            this.grpSettings.BackColor = System.Drawing.Color.White;
            this.grpSettings.Controls.Add(this.panelAdaptiveDeltaSettings);
            this.grpSettings.Controls.Add(this.panelDeltaSettings);
            this.grpSettings.Controls.Add(this.panelPredictiveSettings);
            this.grpSettings.Controls.Add(this.panelDpcmSettings);
            this.grpSettings.Controls.Add(this.panelQuantSettings);
            this.grpSettings.Controls.Add(this.label12);
            this.grpSettings.Controls.Add(this.cboAlgorithm);
            this.grpSettings.Controls.Add(this.label11);
            this.grpSettings.Controls.Add(this.txtBitsPerSample);
            this.grpSettings.Controls.Add(this.label10);
            this.grpSettings.Controls.Add(this.txtChannels);
            this.grpSettings.Controls.Add(this.label9);
            this.grpSettings.Controls.Add(this.txtSampleRate);
            this.grpSettings.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpSettings.Enabled = false;
            this.grpSettings.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.grpSettings.Location = new System.Drawing.Point(8, 363);
            this.grpSettings.Name = "grpSettings";
            this.grpSettings.Size = new System.Drawing.Size(464, 290);
            this.grpSettings.TabIndex = 2;
            this.grpSettings.TabStop = false;
            this.grpSettings.Text = "Compression Settings";
            //
            // panelAdaptiveDeltaSettings
            //
            this.panelAdaptiveDeltaSettings.Controls.Add(this.label19);
            this.panelAdaptiveDeltaSettings.Controls.Add(this.txtMaxStep);
            this.panelAdaptiveDeltaSettings.Controls.Add(this.label18);
            this.panelAdaptiveDeltaSettings.Controls.Add(this.txtMinStep);
            this.panelAdaptiveDeltaSettings.Location = new System.Drawing.Point(5, 235);
            this.panelAdaptiveDeltaSettings.Name = "panelAdaptiveDeltaSettings";
            this.panelAdaptiveDeltaSettings.Size = new System.Drawing.Size(450, 45);
            this.panelAdaptiveDeltaSettings.TabIndex = 12;
            this.panelAdaptiveDeltaSettings.Visible = false;
            //
            // label19
            //
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(230, 5);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(62, 15);
            this.label19.TabIndex = 3;
            this.label19.Text = "Max Step:";
            //
            // txtMaxStep
            //
            this.txtMaxStep.Location = new System.Drawing.Point(298, 2);
            this.txtMaxStep.Name = "txtMaxStep";
            this.txtMaxStep.Size = new System.Drawing.Size(60, 23);
            this.txtMaxStep.TabIndex = 4;
            this.txtMaxStep.Text = "5000";
            //
            // label18
            //
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(5, 5);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(57, 15);
            this.label18.TabIndex = 1;
            this.label18.Text = "Min Step:";
            //
            // txtMinStep
            //
            this.txtMinStep.Location = new System.Drawing.Point(68, 2);
            this.txtMinStep.Name = "txtMinStep";
            this.txtMinStep.Size = new System.Drawing.Size(60, 23);
            this.txtMinStep.TabIndex = 2;
            this.txtMinStep.Text = "50";
            //
            // panelDeltaSettings
            //
            this.panelDeltaSettings.Controls.Add(this.label17);
            this.panelDeltaSettings.Controls.Add(this.txtDeltaStep);
            this.panelDeltaSettings.Location = new System.Drawing.Point(5, 195);
            this.panelDeltaSettings.Name = "panelDeltaSettings";
            this.panelDeltaSettings.Size = new System.Drawing.Size(450, 35);
            this.panelDeltaSettings.TabIndex = 11;
            this.panelDeltaSettings.Visible = false;
            //
            // label17
            //
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(5, 8);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(65, 15);
            this.label17.TabIndex = 0;
            this.label17.Text = "Delta Step:";
            //
            // txtDeltaStep
            //
            this.txtDeltaStep.Location = new System.Drawing.Point(76, 5);
            this.txtDeltaStep.Name = "txtDeltaStep";
            this.txtDeltaStep.Size = new System.Drawing.Size(60, 23);
            this.txtDeltaStep.TabIndex = 1;
            this.txtDeltaStep.Text = "500";
            //
            // panelPredictiveSettings
            //
            this.panelPredictiveSettings.Controls.Add(this.label16);
            this.panelPredictiveSettings.Controls.Add(this.cboPredictionOrder);
            this.panelPredictiveSettings.Location = new System.Drawing.Point(5, 165);
            this.panelPredictiveSettings.Name = "panelPredictiveSettings";
            this.panelPredictiveSettings.Size = new System.Drawing.Size(450, 30);
            this.panelPredictiveSettings.TabIndex = 10;
            this.panelPredictiveSettings.Visible = false;
            //
            // label16
            //
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(5, 6);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(103, 15);
            this.label16.TabIndex = 0;
            this.label16.Text = "Prediction Order:";
            //
            // cboPredictionOrder
            //
            this.cboPredictionOrder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPredictionOrder.Items.AddRange(new object[] { "1", "2", "3", "4" });
            this.cboPredictionOrder.Location = new System.Drawing.Point(114, 3);
            this.cboPredictionOrder.Name = "cboPredictionOrder";
            this.cboPredictionOrder.Size = new System.Drawing.Size(60, 23);
            this.cboPredictionOrder.TabIndex = 1;
            //
            // panelDpcmSettings
            //
            this.panelDpcmSettings.Controls.Add(this.label15);
            this.panelDpcmSettings.Controls.Add(this.cboDpcmBits);
            this.panelDpcmSettings.Location = new System.Drawing.Point(5, 135);
            this.panelDpcmSettings.Name = "panelDpcmSettings";
            this.panelDpcmSettings.Size = new System.Drawing.Size(450, 30);
            this.panelDpcmSettings.TabIndex = 9;
            this.panelDpcmSettings.Visible = false;
            //
            // label15
            //
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(5, 6);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(71, 15);
            this.label15.TabIndex = 0;
            this.label15.Text = "DPCM Bits:";
            //
            // cboDpcmBits
            //
            this.cboDpcmBits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDpcmBits.Items.AddRange(new object[] { "2", "3", "4", "5", "6", "7", "8" });
            this.cboDpcmBits.Location = new System.Drawing.Point(82, 3);
            this.cboDpcmBits.Name = "cboDpcmBits";
            this.cboDpcmBits.Size = new System.Drawing.Size(60, 23);
            this.cboDpcmBits.TabIndex = 1;
            //
            // panelQuantSettings
            //
            this.panelQuantSettings.Controls.Add(this.label14);
            this.panelQuantSettings.Controls.Add(this.txtMu);
            this.panelQuantSettings.Controls.Add(this.label13);
            this.panelQuantSettings.Controls.Add(this.cboQuantLevels);
            this.panelQuantSettings.Location = new System.Drawing.Point(5, 95);
            this.panelQuantSettings.Name = "panelQuantSettings";
            this.panelQuantSettings.Size = new System.Drawing.Size(450, 40);
            this.panelQuantSettings.TabIndex = 8;
            this.panelQuantSettings.Visible = false;
            //
            // label14
            //
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(230, 8);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(28, 15);
            this.label14.TabIndex = 3;
            this.label14.Text = "Mu:";
            //
            // txtMu
            //
            this.txtMu.Location = new System.Drawing.Point(264, 5);
            this.txtMu.Name = "txtMu";
            this.txtMu.Size = new System.Drawing.Size(60, 23);
            this.txtMu.TabIndex = 4;
            this.txtMu.Text = "255";
            //
            // label13
            //
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(5, 8);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(104, 15);
            this.label13.TabIndex = 1;
            this.label13.Text = "Quant. Levels:";
            //
            // cboQuantLevels
            //
            this.cboQuantLevels.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboQuantLevels.Items.AddRange(new object[] { "8", "16", "32", "64", "128", "256" });
            this.cboQuantLevels.Location = new System.Drawing.Point(115, 5);
            this.cboQuantLevels.Name = "cboQuantLevels";
            this.cboQuantLevels.Size = new System.Drawing.Size(70, 23);
            this.cboQuantLevels.TabIndex = 2;
            //
            // label12
            //
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(5, 22);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(68, 15);
            this.label12.TabIndex = 7;
            this.label12.Text = "Algorithm:";
            //
            // cboAlgorithm
            //
            this.cboAlgorithm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboAlgorithm.Items.AddRange(new object[] {
            "Nonlinear Quantization (mu-law)",
            "DPCM",
            "Predictive Differential Coding",
            "Delta Modulation",
            "Adaptive Delta Modulation"});
            this.cboAlgorithm.Location = new System.Drawing.Point(79, 19);
            this.cboAlgorithm.Name = "cboAlgorithm";
            this.cboAlgorithm.Size = new System.Drawing.Size(220, 23);
            this.cboAlgorithm.TabIndex = 6;
            this.cboAlgorithm.SelectedIndexChanged += new System.EventHandler(this.cboAlgorithm_SelectedIndexChanged);
            //
            // label11
            //
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(5, 72);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(88, 15);
            this.label11.TabIndex = 5;
            this.label11.Text = "Bits/Sample:";
            //
            // txtBitsPerSample
            //
            this.txtBitsPerSample.Location = new System.Drawing.Point(99, 69);
            this.txtBitsPerSample.Name = "txtBitsPerSample";
            this.txtBitsPerSample.ReadOnly = true;
            this.txtBitsPerSample.Size = new System.Drawing.Size(60, 23);
            this.txtBitsPerSample.TabIndex = 4;
            this.txtBitsPerSample.Text = "16";
            //
            // label10
            //
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(310, 22);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(58, 15);
            this.label10.TabIndex = 3;
            this.label10.Text = "Channels:";
            //
            // txtChannels
            //
            this.txtChannels.Location = new System.Drawing.Point(374, 19);
            this.txtChannels.Name = "txtChannels";
            this.txtChannels.ReadOnly = true;
            this.txtChannels.Size = new System.Drawing.Size(40, 23);
            this.txtChannels.TabIndex = 2;
            this.txtChannels.Text = "1";
            //
            // label9
            //
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(310, 50);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(82, 15);
            this.label9.TabIndex = 1;
            this.label9.Text = "Sample Rate:";
            //
            // txtSampleRate
            //
            this.txtSampleRate.Location = new System.Drawing.Point(398, 47);
            this.txtSampleRate.Name = "txtSampleRate";
            this.txtSampleRate.Size = new System.Drawing.Size(60, 23);
            this.txtSampleRate.TabIndex = 0;
            this.txtSampleRate.Text = "44100";
            //
            // panelRight
            //
            this.panelRight.Controls.Add(this.grpCharts);
            this.panelRight.Controls.Add(this.grpResults);
            this.panelRight.Controls.Add(this.grpProgress);
            this.panelRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelRight.Location = new System.Drawing.Point(0, 0);
            this.panelRight.Name = "panelRight";
            this.panelRight.Padding = new System.Windows.Forms.Padding(8);
            this.panelRight.Size = new System.Drawing.Size(796, 660);
            this.panelRight.TabIndex = 0;
            //
            // grpCharts
            //
            this.grpCharts.BackColor = System.Drawing.Color.White;
            this.grpCharts.Controls.Add(this.plotSpeed);
            this.grpCharts.Controls.Add(this.plotRatio);
            this.grpCharts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpCharts.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.grpCharts.Location = new System.Drawing.Point(8, 165);
            this.grpCharts.Name = "grpCharts";
            this.grpCharts.Size = new System.Drawing.Size(780, 495);
            this.grpCharts.TabIndex = 2;
            this.grpCharts.TabStop = false;
            this.grpCharts.Text = "Real-Time Monitoring Charts";
            //
            // plotSpeed
            //
            this.plotSpeed.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plotSpeed.Location = new System.Drawing.Point(3, 253);
            this.plotSpeed.Name = "plotSpeed";
            this.plotSpeed.Size = new System.Drawing.Size(774, 236);
            this.plotSpeed.TabIndex = 1;
            //
            // plotRatio
            //
            this.plotRatio.Dock = System.Windows.Forms.DockStyle.Top;
            this.plotRatio.Location = new System.Drawing.Point(3, 17);
            this.plotRatio.Name = "plotRatio";
            this.plotRatio.Size = new System.Drawing.Size(774, 236);
            this.plotRatio.TabIndex = 0;
            //
            // grpResults
            //
            this.grpResults.BackColor = System.Drawing.Color.White;
            this.grpResults.Controls.Add(this.lblTimeElapsed);
            this.grpResults.Controls.Add(this.lblCompressedSize);
            this.grpResults.Controls.Add(this.lblOriginalSize);
            this.grpResults.Controls.Add(this.lblSpaceSaving);
            this.grpResults.Controls.Add(this.lblCompressionRatio);
            this.grpResults.Controls.Add(this.label26);
            this.grpResults.Controls.Add(this.label25);
            this.grpResults.Controls.Add(this.label24);
            this.grpResults.Controls.Add(this.label23);
            this.grpResults.Controls.Add(this.label22);
            this.grpResults.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpResults.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.grpResults.Location = new System.Drawing.Point(8, 57);
            this.grpResults.Name = "grpResults";
            this.grpResults.Size = new System.Drawing.Size(780, 108);
            this.grpResults.TabIndex = 1;
            this.grpResults.TabStop = false;
            this.grpResults.Text = "Compression Results";
            //
            // lblTimeElapsed
            //
            this.lblTimeElapsed.AutoSize = true;
            this.lblTimeElapsed.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTimeElapsed.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(174)))), ((int)(((byte)(96)))));
            this.lblTimeElapsed.Location = new System.Drawing.Point(640, 70);
            this.lblTimeElapsed.Name = "lblTimeElapsed";
            this.lblTimeElapsed.Size = new System.Drawing.Size(24, 19);
            this.lblTimeElapsed.TabIndex = 9;
            this.lblTimeElapsed.Text = "--";
            //
            // lblCompressedSize
            //
            this.lblCompressedSize.AutoSize = true;
            this.lblCompressedSize.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblCompressedSize.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(174)))), ((int)(((byte)(96)))));
            this.lblCompressedSize.Location = new System.Drawing.Point(440, 70);
            this.lblCompressedSize.Name = "lblCompressedSize";
            this.lblCompressedSize.Size = new System.Drawing.Size(24, 19);
            this.lblCompressedSize.TabIndex = 8;
            this.lblCompressedSize.Text = "--";
            //
            // lblOriginalSize
            //
            this.lblOriginalSize.AutoSize = true;
            this.lblOriginalSize.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblOriginalSize.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(174)))), ((int)(((byte)(96)))));
            this.lblOriginalSize.Location = new System.Drawing.Point(240, 70);
            this.lblOriginalSize.Name = "lblOriginalSize";
            this.lblOriginalSize.Size = new System.Drawing.Size(24, 19);
            this.lblOriginalSize.TabIndex = 7;
            this.lblOriginalSize.Text = "--";
            //
            // lblSpaceSaving
            //
            this.lblSpaceSaving.AutoSize = true;
            this.lblSpaceSaving.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblSpaceSaving.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(174)))), ((int)(((byte)(96)))));
            this.lblSpaceSaving.Location = new System.Drawing.Point(440, 30);
            this.lblSpaceSaving.Name = "lblSpaceSaving";
            this.lblSpaceSaving.Size = new System.Drawing.Size(24, 19);
            this.lblSpaceSaving.TabIndex = 6;
            this.lblSpaceSaving.Text = "--";
            //
            // lblCompressionRatio
            //
            this.lblCompressionRatio.AutoSize = true;
            this.lblCompressionRatio.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblCompressionRatio.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(174)))), ((int)(((byte)(96)))));
            this.lblCompressionRatio.Location = new System.Drawing.Point(240, 30);
            this.lblCompressionRatio.Name = "lblCompressionRatio";
            this.lblCompressionRatio.Size = new System.Drawing.Size(24, 19);
            this.lblCompressionRatio.TabIndex = 5;
            this.lblCompressionRatio.Text = "--";
            //
            // label26
            //
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(560, 72);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(78, 15);
            this.label26.TabIndex = 4;
            this.label26.Text = "Time Elapsed:";
            //
            // label25
            //
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(340, 72);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(94, 15);
            this.label25.TabIndex = 3;
            this.label25.Text = "Compressed Size:";
            //
            // label24
            //
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(140, 72);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(88, 15);
            this.label24.TabIndex = 2;
            this.label24.Text = "Original Size:";
            //
            // label23
            //
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(340, 32);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(82, 15);
            this.label23.TabIndex = 1;
            this.label23.Text = "Space Saving:";
            //
            // label22
            //
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(140, 32);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(96, 15);
            this.label22.TabIndex = 0;
            this.label22.Text = "Compression Ratio:";
            //
            // grpProgress
            //
            this.grpProgress.BackColor = System.Drawing.Color.White;
            this.grpProgress.Controls.Add(this.lblProgressPercent);
            this.grpProgress.Controls.Add(this.progressBar1);
            this.grpProgress.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpProgress.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.grpProgress.Location = new System.Drawing.Point(8, 8);
            this.grpProgress.Name = "grpProgress";
            this.grpProgress.Size = new System.Drawing.Size(780, 49);
            this.grpProgress.TabIndex = 0;
            this.grpProgress.TabStop = false;
            this.grpProgress.Text = "Progress";
            //
            // lblProgressPercent
            //
            this.lblProgressPercent.AutoSize = true;
            this.lblProgressPercent.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblProgressPercent.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.lblProgressPercent.Location = new System.Drawing.Point(720, 17);
            this.lblProgressPercent.Name = "lblProgressPercent";
            this.lblProgressPercent.Size = new System.Drawing.Size(26, 19);
            this.lblProgressPercent.TabIndex = 1;
            this.lblProgressPercent.Text = "0%";
            //
            // progressBar1
            //
            this.progressBar1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.progressBar1.Location = new System.Drawing.Point(3, 17);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(710, 29);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar1.TabIndex = 0;
            //
            // MainForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(245)))));
            this.ClientSize = new System.Drawing.Size(1280, 720);
            this.Controls.Add(this.panelMain);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.MinimumSize = new System.Drawing.Size(1000, 600);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Audio Compressor - Multimedia Systems Project";
            this.panelMain.ResumeLayout(false);
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.panelContent.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panelLeft.ResumeLayout(false);
            this.grpFileInfo.ResumeLayout(false);
            this.grpFileInfo.PerformLayout();
            this.grpPlayback.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picWaveform)).EndInit();
            this.grpSettings.ResumeLayout(false);
            this.grpSettings.PerformLayout();
            this.panelAdaptiveDeltaSettings.ResumeLayout(false);
            this.panelAdaptiveDeltaSettings.PerformLayout();
            this.panelDeltaSettings.ResumeLayout(false);
            this.panelDeltaSettings.PerformLayout();
            this.panelPredictiveSettings.ResumeLayout(false);
            this.panelDpcmSettings.ResumeLayout(false);
            this.panelDpcmSettings.PerformLayout();
            this.panelQuantSettings.ResumeLayout(false);
            this.panelQuantSettings.PerformLayout();
            this.panelRight.ResumeLayout(false);
            this.grpCharts.ResumeLayout(false);
            this.grpResults.ResumeLayout(false);
            this.grpResults.PerformLayout();
            this.grpProgress.ResumeLayout(false);
            this.grpProgress.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnDecompress;
        private System.Windows.Forms.Button btnCompress;
        private System.Windows.Forms.Button btnOpenFile;
        private System.Windows.Forms.Panel panelContent;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panelLeft;
        private System.Windows.Forms.GroupBox grpFileInfo;
        private System.Windows.Forms.Label lblEncodingInfo;
        private System.Windows.Forms.Label lblBitsPerSampleInfo;
        private System.Windows.Forms.Label lblBitRateInfo;
        private System.Windows.Forms.Label lblChannelsInfo;
        private System.Windows.Forms.Label lblSampleRateInfo;
        private System.Windows.Forms.Label lblDuration;
        private System.Windows.Forms.Label lblFileSize;
        private System.Windows.Forms.Label lblFileName;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox grpPlayback;
        private System.Windows.Forms.PictureBox picWaveform;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.GroupBox grpSettings;
        private System.Windows.Forms.Panel panelAdaptiveDeltaSettings;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox txtMaxStep;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox txtMinStep;
        private System.Windows.Forms.Panel panelDeltaSettings;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox txtDeltaStep;
        private System.Windows.Forms.Panel panelPredictiveSettings;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.ComboBox cboPredictionOrder;
        private System.Windows.Forms.Panel panelDpcmSettings;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ComboBox cboDpcmBits;
        private System.Windows.Forms.Panel panelQuantSettings;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtMu;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox cboQuantLevels;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox cboAlgorithm;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtBitsPerSample;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtChannels;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtSampleRate;
        private System.Windows.Forms.Panel panelRight;
        private System.Windows.Forms.GroupBox grpCharts;
        private OxyPlot.WindowsForms.PlotView plotSpeed;
        private OxyPlot.WindowsForms.PlotView plotRatio;
        private System.Windows.Forms.GroupBox grpResults;
        private System.Windows.Forms.Label lblTimeElapsed;
        private System.Windows.Forms.Label lblCompressedSize;
        private System.Windows.Forms.Label lblOriginalSize;
        private System.Windows.Forms.Label lblSpaceSaving;
        private System.Windows.Forms.Label lblCompressionRatio;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.GroupBox grpProgress;
        private System.Windows.Forms.Label lblProgressPercent;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}
