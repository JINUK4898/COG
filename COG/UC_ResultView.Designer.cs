namespace COG
{
    partial class UC_ResultView
    {
        /// <summary> 
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UC_ResultView));
            this.LB_FOLDER_PATH = new System.Windows.Forms.Label();
            this.uC_NGVIEW_ALL1 = new COG.UC_NGVIEW_ALL();
            this.uC_ResultView_Label7 = new COG.UC_ResultView_Label();
            this.uC_ResultView_Label6 = new COG.UC_ResultView_Label();
            this.uC_ResultView_Label5 = new COG.UC_ResultView_Label();
            this.uC_ResultView_Label4 = new COG.UC_ResultView_Label();
            this.uC_ResultView_Label3 = new COG.UC_ResultView_Label();
            this.uC_ResultView_Label2 = new COG.UC_ResultView_Label();
            this.uC_ResultView_Label1 = new COG.UC_ResultView_Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.MainDisplay = new Cognex.VisionPro.CogRecordDisplay();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.MainDisplay2 = new Cognex.VisionPro.CogRecordDisplay();
            this.LIB_LOG_PATH = new System.Windows.Forms.ListBox();
            this.BTN_GetPath = new System.Windows.Forms.Button();
            this.BTN_CLEAR = new System.Windows.Forms.Button();
            this.NUD_COUNT = new System.Windows.Forms.NumericUpDown();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MainDisplay)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MainDisplay2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUD_COUNT)).BeginInit();
            this.SuspendLayout();
            // 
            // LB_FOLDER_PATH
            // 
            this.LB_FOLDER_PATH.BackColor = System.Drawing.Color.White;
            this.LB_FOLDER_PATH.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LB_FOLDER_PATH.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.LB_FOLDER_PATH.Location = new System.Drawing.Point(3, 563);
            this.LB_FOLDER_PATH.Name = "LB_FOLDER_PATH";
            this.LB_FOLDER_PATH.Size = new System.Drawing.Size(1019, 35);
            this.LB_FOLDER_PATH.TabIndex = 103;
            this.LB_FOLDER_PATH.Text = "PATH";
            this.LB_FOLDER_PATH.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // uC_NGVIEW_ALL1
            // 
            this.uC_NGVIEW_ALL1.BackColor = System.Drawing.Color.DimGray;
            this.uC_NGVIEW_ALL1.Location = new System.Drawing.Point(1028, 23);
            this.uC_NGVIEW_ALL1.Name = "uC_NGVIEW_ALL1";
            this.uC_NGVIEW_ALL1.Size = new System.Drawing.Size(661, 575);
            this.uC_NGVIEW_ALL1.TabIndex = 106;
            // 
            // uC_ResultView_Label7
            // 
            this.uC_ResultView_Label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.uC_ResultView_Label7.CamIndex = 6;
            this.uC_ResultView_Label7.Location = new System.Drawing.Point(300, 2);
            this.uC_ResultView_Label7.MaxCount = 25;
            this.uC_ResultView_Label7.Name = "uC_ResultView_Label7";
            this.uC_ResultView_Label7.Size = new System.Drawing.Size(50, 558);
            this.uC_ResultView_Label7.TabIndex = 6;
            // 
            // uC_ResultView_Label6
            // 
            this.uC_ResultView_Label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.uC_ResultView_Label6.CamIndex = 5;
            this.uC_ResultView_Label6.Location = new System.Drawing.Point(250, 2);
            this.uC_ResultView_Label6.MaxCount = 25;
            this.uC_ResultView_Label6.Name = "uC_ResultView_Label6";
            this.uC_ResultView_Label6.Size = new System.Drawing.Size(50, 558);
            this.uC_ResultView_Label6.TabIndex = 5;
            // 
            // uC_ResultView_Label5
            // 
            this.uC_ResultView_Label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.uC_ResultView_Label5.CamIndex = 4;
            this.uC_ResultView_Label5.Location = new System.Drawing.Point(200, 2);
            this.uC_ResultView_Label5.MaxCount = 25;
            this.uC_ResultView_Label5.Name = "uC_ResultView_Label5";
            this.uC_ResultView_Label5.Size = new System.Drawing.Size(50, 558);
            this.uC_ResultView_Label5.TabIndex = 4;
            // 
            // uC_ResultView_Label4
            // 
            this.uC_ResultView_Label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.uC_ResultView_Label4.CamIndex = 3;
            this.uC_ResultView_Label4.Location = new System.Drawing.Point(150, 2);
            this.uC_ResultView_Label4.MaxCount = 25;
            this.uC_ResultView_Label4.Name = "uC_ResultView_Label4";
            this.uC_ResultView_Label4.Size = new System.Drawing.Size(50, 558);
            this.uC_ResultView_Label4.TabIndex = 3;
            // 
            // uC_ResultView_Label3
            // 
            this.uC_ResultView_Label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.uC_ResultView_Label3.CamIndex = 2;
            this.uC_ResultView_Label3.Location = new System.Drawing.Point(100, 2);
            this.uC_ResultView_Label3.MaxCount = 25;
            this.uC_ResultView_Label3.Name = "uC_ResultView_Label3";
            this.uC_ResultView_Label3.Size = new System.Drawing.Size(50, 558);
            this.uC_ResultView_Label3.TabIndex = 2;
            // 
            // uC_ResultView_Label2
            // 
            this.uC_ResultView_Label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.uC_ResultView_Label2.CamIndex = 1;
            this.uC_ResultView_Label2.Location = new System.Drawing.Point(50, 2);
            this.uC_ResultView_Label2.MaxCount = 25;
            this.uC_ResultView_Label2.Name = "uC_ResultView_Label2";
            this.uC_ResultView_Label2.Size = new System.Drawing.Size(50, 558);
            this.uC_ResultView_Label2.TabIndex = 1;
            // 
            // uC_ResultView_Label1
            // 
            this.uC_ResultView_Label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.uC_ResultView_Label1.CamIndex = 0;
            this.uC_ResultView_Label1.Location = new System.Drawing.Point(0, 2);
            this.uC_ResultView_Label1.MaxCount = 25;
            this.uC_ResultView_Label1.Name = "uC_ResultView_Label1";
            this.uC_ResultView_Label1.Size = new System.Drawing.Size(50, 558);
            this.uC_ResultView_Label1.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(349, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(673, 558);
            this.tabControl1.TabIndex = 107;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.MainDisplay);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(665, 532);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "PAGE 1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // MainDisplay
            // 
            this.MainDisplay.ColorMapLowerClipColor = System.Drawing.Color.Black;
            this.MainDisplay.ColorMapLowerRoiLimit = 0D;
            this.MainDisplay.ColorMapPredefined = Cognex.VisionPro.Display.CogDisplayColorMapPredefinedConstants.None;
            this.MainDisplay.ColorMapUpperClipColor = System.Drawing.Color.Black;
            this.MainDisplay.ColorMapUpperRoiLimit = 1D;
            this.MainDisplay.DoubleTapZoomCycleLength = 2;
            this.MainDisplay.DoubleTapZoomSensitivity = 2.5D;
            this.MainDisplay.Location = new System.Drawing.Point(0, 0);
            this.MainDisplay.MouseWheelMode = Cognex.VisionPro.Display.CogDisplayMouseWheelModeConstants.Zoom1;
            this.MainDisplay.MouseWheelSensitivity = 1D;
            this.MainDisplay.Name = "MainDisplay";
            this.MainDisplay.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("MainDisplay.OcxState")));
            this.MainDisplay.Size = new System.Drawing.Size(662, 532);
            this.MainDisplay.TabIndex = 135;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.MainDisplay2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(665, 532);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "PAGE 2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // MainDisplay2
            // 
            this.MainDisplay2.ColorMapLowerClipColor = System.Drawing.Color.Black;
            this.MainDisplay2.ColorMapLowerRoiLimit = 0D;
            this.MainDisplay2.ColorMapPredefined = Cognex.VisionPro.Display.CogDisplayColorMapPredefinedConstants.None;
            this.MainDisplay2.ColorMapUpperClipColor = System.Drawing.Color.Black;
            this.MainDisplay2.ColorMapUpperRoiLimit = 1D;
            this.MainDisplay2.DoubleTapZoomCycleLength = 2;
            this.MainDisplay2.DoubleTapZoomSensitivity = 2.5D;
            this.MainDisplay2.Location = new System.Drawing.Point(0, 0);
            this.MainDisplay2.MouseWheelMode = Cognex.VisionPro.Display.CogDisplayMouseWheelModeConstants.Zoom1;
            this.MainDisplay2.MouseWheelSensitivity = 1D;
            this.MainDisplay2.Name = "MainDisplay2";
            this.MainDisplay2.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("MainDisplay2.OcxState")));
            this.MainDisplay2.Size = new System.Drawing.Size(662, 532);
            this.MainDisplay2.TabIndex = 136;
            // 
            // LIB_LOG_PATH
            // 
            this.LIB_LOG_PATH.FormattingEnabled = true;
            this.LIB_LOG_PATH.ItemHeight = 12;
            this.LIB_LOG_PATH.Location = new System.Drawing.Point(3, 604);
            this.LIB_LOG_PATH.Name = "LIB_LOG_PATH";
            this.LIB_LOG_PATH.Size = new System.Drawing.Size(1019, 196);
            this.LIB_LOG_PATH.TabIndex = 108;
            this.LIB_LOG_PATH.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.LIB_LOG_PATH_MouseDoubleClick);
            // 
            // BTN_GetPath
            // 
            this.BTN_GetPath.BackColor = System.Drawing.Color.LightGreen;
            this.BTN_GetPath.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.BTN_GetPath.Location = new System.Drawing.Point(1028, 634);
            this.BTN_GetPath.Name = "BTN_GetPath";
            this.BTN_GetPath.Size = new System.Drawing.Size(122, 52);
            this.BTN_GetPath.TabIndex = 109;
            this.BTN_GetPath.Text = "Get Data";
            this.BTN_GetPath.UseVisualStyleBackColor = false;
            this.BTN_GetPath.Click += new System.EventHandler(this.BTN_GetPath_Click);
            // 
            // BTN_CLEAR
            // 
            this.BTN_CLEAR.BackColor = System.Drawing.Color.LightGreen;
            this.BTN_CLEAR.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.BTN_CLEAR.Location = new System.Drawing.Point(1028, 748);
            this.BTN_CLEAR.Name = "BTN_CLEAR";
            this.BTN_CLEAR.Size = new System.Drawing.Size(122, 52);
            this.BTN_CLEAR.TabIndex = 110;
            this.BTN_CLEAR.Text = "Clear";
            this.BTN_CLEAR.UseVisualStyleBackColor = false;
            // 
            // NUD_COUNT
            // 
            this.NUD_COUNT.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.NUD_COUNT.Location = new System.Drawing.Point(1028, 605);
            this.NUD_COUNT.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.NUD_COUNT.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.NUD_COUNT.Name = "NUD_COUNT";
            this.NUD_COUNT.Size = new System.Drawing.Size(122, 29);
            this.NUD_COUNT.TabIndex = 111;
            this.NUD_COUNT.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // UC_ResultView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.Controls.Add(this.NUD_COUNT);
            this.Controls.Add(this.BTN_CLEAR);
            this.Controls.Add(this.BTN_GetPath);
            this.Controls.Add(this.LIB_LOG_PATH);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.uC_NGVIEW_ALL1);
            this.Controls.Add(this.LB_FOLDER_PATH);
            this.Controls.Add(this.uC_ResultView_Label7);
            this.Controls.Add(this.uC_ResultView_Label6);
            this.Controls.Add(this.uC_ResultView_Label5);
            this.Controls.Add(this.uC_ResultView_Label4);
            this.Controls.Add(this.uC_ResultView_Label3);
            this.Controls.Add(this.uC_ResultView_Label2);
            this.Controls.Add(this.uC_ResultView_Label1);
            this.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Name = "UC_ResultView";
            this.Size = new System.Drawing.Size(1697, 812);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.MainDisplay)).EndInit();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.MainDisplay2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUD_COUNT)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private UC_ResultView_Label uC_ResultView_Label1;
        private UC_ResultView_Label uC_ResultView_Label2;
        private UC_ResultView_Label uC_ResultView_Label3;
        private UC_ResultView_Label uC_ResultView_Label4;
        private UC_ResultView_Label uC_ResultView_Label5;
        private UC_ResultView_Label uC_ResultView_Label6;
        private UC_ResultView_Label uC_ResultView_Label7;
        public System.Windows.Forms.Label LB_FOLDER_PATH;
        private UC_NGVIEW_ALL uC_NGVIEW_ALL1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private Cognex.VisionPro.CogRecordDisplay MainDisplay;
        private Cognex.VisionPro.CogRecordDisplay MainDisplay2;
        private System.Windows.Forms.ListBox LIB_LOG_PATH;
        private System.Windows.Forms.Button BTN_GetPath;
        private System.Windows.Forms.Button BTN_CLEAR;
        private System.Windows.Forms.NumericUpDown NUD_COUNT;
    }
}
