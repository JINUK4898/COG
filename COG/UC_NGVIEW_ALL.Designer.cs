namespace COG
{
    partial class UC_NGVIEW_ALL
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
            this.LB_ERRORCODE = new System.Windows.Forms.Label();
            this.uC_NGVIEW4 = new COG.UC_NGVIEW();
            this.uC_NGVIEW3 = new COG.UC_NGVIEW();
            this.uC_NGVIEW2 = new COG.UC_NGVIEW();
            this.uC_NGVIEW1 = new COG.UC_NGVIEW();
            this.SuspendLayout();
            // 
            // LB_ERRORCODE
            // 
            this.LB_ERRORCODE.BackColor = System.Drawing.Color.DarkTurquoise;
            this.LB_ERRORCODE.Location = new System.Drawing.Point(293, 527);
            this.LB_ERRORCODE.Name = "LB_ERRORCODE";
            this.LB_ERRORCODE.Size = new System.Drawing.Size(73, 34);
            this.LB_ERRORCODE.TabIndex = 13;
            this.LB_ERRORCODE.Text = "0";
            this.LB_ERRORCODE.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.LB_ERRORCODE.Visible = false;
            // 
            // uC_NGVIEW4
            // 
            this.uC_NGVIEW4.AutoSize = true;
            this.uC_NGVIEW4.BackColor = System.Drawing.Color.DarkGray;
            this.uC_NGVIEW4.ControlPosSwap = true;
            this.uC_NGVIEW4.Location = new System.Drawing.Point(342, 291);
            this.uC_NGVIEW4.Name = "uC_NGVIEW4";
            this.uC_NGVIEW4.Size = new System.Drawing.Size(315, 270);
            this.uC_NGVIEW4.TabIndex = 3;
            this.uC_NGVIEW4.Click += new System.EventHandler(this.uC_NGVIEW4_Click);
            // 
            // uC_NGVIEW3
            // 
            this.uC_NGVIEW3.AutoSize = true;
            this.uC_NGVIEW3.BackColor = System.Drawing.Color.DarkGray;
            this.uC_NGVIEW3.ControlPosSwap = false;
            this.uC_NGVIEW3.Location = new System.Drawing.Point(3, 291);
            this.uC_NGVIEW3.Name = "uC_NGVIEW3";
            this.uC_NGVIEW3.Size = new System.Drawing.Size(315, 270);
            this.uC_NGVIEW3.TabIndex = 2;
            this.uC_NGVIEW3.Click += new System.EventHandler(this.uC_NGVIEW3_Click);
            // 
            // uC_NGVIEW2
            // 
            this.uC_NGVIEW2.AutoSize = true;
            this.uC_NGVIEW2.BackColor = System.Drawing.Color.DarkGray;
            this.uC_NGVIEW2.ControlPosSwap = true;
            this.uC_NGVIEW2.Location = new System.Drawing.Point(342, 0);
            this.uC_NGVIEW2.Name = "uC_NGVIEW2";
            this.uC_NGVIEW2.Size = new System.Drawing.Size(315, 270);
            this.uC_NGVIEW2.TabIndex = 1;
            this.uC_NGVIEW2.Click += new System.EventHandler(this.uC_NGVIEW2_Click);
            // 
            // uC_NGVIEW1
            // 
            this.uC_NGVIEW1.AutoSize = true;
            this.uC_NGVIEW1.BackColor = System.Drawing.Color.DarkGray;
            this.uC_NGVIEW1.ControlPosSwap = false;
            this.uC_NGVIEW1.Location = new System.Drawing.Point(3, 3);
            this.uC_NGVIEW1.Name = "uC_NGVIEW1";
            this.uC_NGVIEW1.Size = new System.Drawing.Size(315, 270);
            this.uC_NGVIEW1.TabIndex = 0;
            this.uC_NGVIEW1.Click += new System.EventHandler(this.uC_NGVIEW1_Click);
            // 
            // UC_NGVIEW_ALL
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DimGray;
            this.Controls.Add(this.LB_ERRORCODE);
            this.Controls.Add(this.uC_NGVIEW4);
            this.Controls.Add(this.uC_NGVIEW3);
            this.Controls.Add(this.uC_NGVIEW2);
            this.Controls.Add(this.uC_NGVIEW1);
            this.Name = "UC_NGVIEW_ALL";
            this.Size = new System.Drawing.Size(662, 567);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private UC_NGVIEW uC_NGVIEW1;
        private UC_NGVIEW uC_NGVIEW2;
        private UC_NGVIEW uC_NGVIEW3;
        private UC_NGVIEW uC_NGVIEW4;
        private System.Windows.Forms.Label LB_ERRORCODE;
    }
}
