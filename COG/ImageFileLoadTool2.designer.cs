namespace COG
{
    partial class ImageFileLoadTool
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
            this.BTN_IMG_LOAD = new System.Windows.Forms.Button();
            this.GB_IMAGEFILE = new System.Windows.Forms.GroupBox();
            this.BTN_NEXT_IMG = new System.Windows.Forms.Button();
            this.BTN_PRE_IMG = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.LB_IMAGEFILE_SELECT_NAME = new System.Windows.Forms.Label();
            this.TB_IMAGEFILE_FOLDER_NAME = new System.Windows.Forms.TextBox();
            this.LB_load_Image_Cnt = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.TB_Current_Image = new System.Windows.Forms.TextBox();
            this.GB_IMAGEFILE.SuspendLayout();
            this.SuspendLayout();
            // 
            // BTN_IMG_LOAD
            // 
            this.BTN_IMG_LOAD.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.BTN_IMG_LOAD.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.BTN_IMG_LOAD.FlatAppearance.BorderSize = 0;
            this.BTN_IMG_LOAD.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LawnGreen;
            this.BTN_IMG_LOAD.Font = new System.Drawing.Font("맑은 고딕", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.BTN_IMG_LOAD.Location = new System.Drawing.Point(587, 13);
            this.BTN_IMG_LOAD.Name = "BTN_IMG_LOAD";
            this.BTN_IMG_LOAD.Size = new System.Drawing.Size(75, 68);
            this.BTN_IMG_LOAD.TabIndex = 161;
            this.BTN_IMG_LOAD.Text = "OPEN";
            this.BTN_IMG_LOAD.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.BTN_IMG_LOAD.UseVisualStyleBackColor = false;
            this.BTN_IMG_LOAD.Click += new System.EventHandler(this.BTN_IMG_LOAD_Click);
            // 
            // GB_IMAGEFILE
            // 
            this.GB_IMAGEFILE.BackColor = System.Drawing.Color.Transparent;
            this.GB_IMAGEFILE.Controls.Add(this.BTN_NEXT_IMG);
            this.GB_IMAGEFILE.Controls.Add(this.BTN_PRE_IMG);
            this.GB_IMAGEFILE.Controls.Add(this.label3);
            this.GB_IMAGEFILE.Controls.Add(this.label2);
            this.GB_IMAGEFILE.Controls.Add(this.LB_IMAGEFILE_SELECT_NAME);
            this.GB_IMAGEFILE.Controls.Add(this.TB_IMAGEFILE_FOLDER_NAME);
            this.GB_IMAGEFILE.Controls.Add(this.BTN_IMG_LOAD);
            this.GB_IMAGEFILE.Controls.Add(this.LB_load_Image_Cnt);
            this.GB_IMAGEFILE.Controls.Add(this.label1);
            this.GB_IMAGEFILE.Controls.Add(this.TB_Current_Image);
            this.GB_IMAGEFILE.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.GB_IMAGEFILE.ForeColor = System.Drawing.Color.White;
            this.GB_IMAGEFILE.Location = new System.Drawing.Point(3, 3);
            this.GB_IMAGEFILE.Name = "GB_IMAGEFILE";
            this.GB_IMAGEFILE.Size = new System.Drawing.Size(829, 90);
            this.GB_IMAGEFILE.TabIndex = 326;
            this.GB_IMAGEFILE.TabStop = false;
            this.GB_IMAGEFILE.Text = "IMAGE LOAD";
            // 
            // BTN_NEXT_IMG
            // 
            this.BTN_NEXT_IMG.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.BTN_NEXT_IMG.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.BTN_NEXT_IMG.Location = new System.Drawing.Point(748, 17);
            this.BTN_NEXT_IMG.Name = "BTN_NEXT_IMG";
            this.BTN_NEXT_IMG.Size = new System.Drawing.Size(75, 45);
            this.BTN_NEXT_IMG.TabIndex = 9;
            this.BTN_NEXT_IMG.Text = ">>>";
            this.BTN_NEXT_IMG.UseVisualStyleBackColor = false;
            this.BTN_NEXT_IMG.Click += new System.EventHandler(this.BTN_NEXT_IMG_Click);
            // 
            // BTN_PRE_IMG
            // 
            this.BTN_PRE_IMG.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.BTN_PRE_IMG.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.BTN_PRE_IMG.Location = new System.Drawing.Point(667, 17);
            this.BTN_PRE_IMG.Name = "BTN_PRE_IMG";
            this.BTN_PRE_IMG.Size = new System.Drawing.Size(75, 45);
            this.BTN_PRE_IMG.TabIndex = 8;
            this.BTN_PRE_IMG.Text = "<<<";
            this.BTN_PRE_IMG.UseVisualStyleBackColor = false;
            this.BTN_PRE_IMG.Click += new System.EventHandler(this.BTN_PRE_IMG_Click);
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.Font = new System.Drawing.Font("맑은 고딕", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label3.Location = new System.Drawing.Point(5, 54);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(109, 26);
            this.label3.TabIndex = 163;
            this.label3.Text = "FILE PATH:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Font = new System.Drawing.Font("맑은 고딕", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.Location = new System.Drawing.Point(5, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(109, 25);
            this.label2.TabIndex = 162;
            this.label2.Text = "FOLDER PATH:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LB_IMAGEFILE_SELECT_NAME
            // 
            this.LB_IMAGEFILE_SELECT_NAME.BackColor = System.Drawing.Color.Transparent;
            this.LB_IMAGEFILE_SELECT_NAME.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LB_IMAGEFILE_SELECT_NAME.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.LB_IMAGEFILE_SELECT_NAME.Location = new System.Drawing.Point(119, 54);
            this.LB_IMAGEFILE_SELECT_NAME.Name = "LB_IMAGEFILE_SELECT_NAME";
            this.LB_IMAGEFILE_SELECT_NAME.Size = new System.Drawing.Size(449, 26);
            this.LB_IMAGEFILE_SELECT_NAME.TabIndex = 30;
            this.LB_IMAGEFILE_SELECT_NAME.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TB_IMAGEFILE_FOLDER_NAME
            // 
            this.TB_IMAGEFILE_FOLDER_NAME.BackColor = System.Drawing.SystemColors.Window;
            this.TB_IMAGEFILE_FOLDER_NAME.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.TB_IMAGEFILE_FOLDER_NAME.ForeColor = System.Drawing.Color.Black;
            this.TB_IMAGEFILE_FOLDER_NAME.Location = new System.Drawing.Point(119, 22);
            this.TB_IMAGEFILE_FOLDER_NAME.Name = "TB_IMAGEFILE_FOLDER_NAME";
            this.TB_IMAGEFILE_FOLDER_NAME.ReadOnly = true;
            this.TB_IMAGEFILE_FOLDER_NAME.Size = new System.Drawing.Size(449, 25);
            this.TB_IMAGEFILE_FOLDER_NAME.TabIndex = 23;
            // 
            // LB_load_Image_Cnt
            // 
            this.LB_load_Image_Cnt.BackColor = System.Drawing.Color.Transparent;
            this.LB_load_Image_Cnt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LB_load_Image_Cnt.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.LB_load_Image_Cnt.Location = new System.Drawing.Point(760, 62);
            this.LB_load_Image_Cnt.Name = "LB_load_Image_Cnt";
            this.LB_load_Image_Cnt.Size = new System.Drawing.Size(46, 24);
            this.LB_load_Image_Cnt.TabIndex = 6;
            this.LB_load_Image_Cnt.Text = "123";
            this.LB_load_Image_Cnt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.Location = new System.Drawing.Point(734, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(20, 26);
            this.label1.TabIndex = 10;
            this.label1.Text = "/";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TB_Current_Image
            // 
            this.TB_Current_Image.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.TB_Current_Image.ForeColor = System.Drawing.Color.Black;
            this.TB_Current_Image.Location = new System.Drawing.Point(682, 63);
            this.TB_Current_Image.Name = "TB_Current_Image";
            this.TB_Current_Image.Size = new System.Drawing.Size(46, 23);
            this.TB_Current_Image.TabIndex = 7;
            this.TB_Current_Image.Text = "123";
            // 
            // ImageFileLoadTool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(44)))), ((int)(((byte)(44)))));
            this.Controls.Add(this.GB_IMAGEFILE);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "ImageFileLoadTool";
            this.Size = new System.Drawing.Size(851, 109);
            this.GB_IMAGEFILE.ResumeLayout(false);
            this.GB_IMAGEFILE.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button BTN_IMG_LOAD;
        private System.Windows.Forms.GroupBox GB_IMAGEFILE;
        private System.Windows.Forms.Label LB_IMAGEFILE_SELECT_NAME;
        private System.Windows.Forms.TextBox TB_IMAGEFILE_FOLDER_NAME;
        private System.Windows.Forms.Label LB_load_Image_Cnt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button BTN_NEXT_IMG;
        private System.Windows.Forms.Button BTN_PRE_IMG;
        private System.Windows.Forms.TextBox TB_Current_Image;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
    }
}
