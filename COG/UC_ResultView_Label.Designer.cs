namespace COG
{
    partial class UC_ResultView_Label
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
            this.PN_PANEL = new System.Windows.Forms.Panel();
            this.LB_NAME = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // PN_PANEL
            // 
            this.PN_PANEL.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PN_PANEL.BackColor = System.Drawing.Color.DarkGray;
            this.PN_PANEL.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.PN_PANEL.Location = new System.Drawing.Point(0, 28);
            this.PN_PANEL.Name = "PN_PANEL";
            this.PN_PANEL.Size = new System.Drawing.Size(54, 675);
            this.PN_PANEL.TabIndex = 0;
            // 
            // LB_NAME
            // 
            this.LB_NAME.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LB_NAME.BackColor = System.Drawing.Color.White;
            this.LB_NAME.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LB_NAME.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.LB_NAME.Location = new System.Drawing.Point(0, 0);
            this.LB_NAME.Name = "LB_NAME";
            this.LB_NAME.Size = new System.Drawing.Size(54, 26);
            this.LB_NAME.TabIndex = 102;
            this.LB_NAME.Text = "CAM";
            this.LB_NAME.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // UC_ResultView_Label
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.Controls.Add(this.LB_NAME);
            this.Controls.Add(this.PN_PANEL);
            this.Name = "UC_ResultView_Label";
            this.Size = new System.Drawing.Size(57, 703);
            this.ClientSizeChanged += new System.EventHandler(this.UC_ResultView_Label_ClientSizeChanged);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel PN_PANEL;
        public System.Windows.Forms.Label LB_NAME;
    }
}
