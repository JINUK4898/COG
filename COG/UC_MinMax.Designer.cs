namespace COG
{
    partial class UC_MinMax
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
            this.LB_NAME = new System.Windows.Forms.Label();
            this.NUD_MAX = new System.Windows.Forms.NumericUpDown();
            this.NUD_MIN = new System.Windows.Forms.NumericUpDown();
            this.CB_USE = new System.Windows.Forms.CheckBox();
            this.LB_MAX = new System.Windows.Forms.Label();
            this.LB_MIN = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.NUD_MAX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUD_MIN)).BeginInit();
            this.SuspendLayout();
            // 
            // LB_NAME
            // 
            this.LB_NAME.AutoSize = true;
            this.LB_NAME.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LB_NAME.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.LB_NAME.Location = new System.Drawing.Point(0, 0);
            this.LB_NAME.Name = "LB_NAME";
            this.LB_NAME.Size = new System.Drawing.Size(110, 15);
            this.LB_NAME.TabIndex = 58;
            this.LB_NAME.Text = "INPUT SPEC NAME";
            this.LB_NAME.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // NUD_MAX
            // 
            this.NUD_MAX.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.NUD_MAX.Location = new System.Drawing.Point(36, 15);
            this.NUD_MAX.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.NUD_MAX.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.NUD_MAX.Name = "NUD_MAX";
            this.NUD_MAX.Size = new System.Drawing.Size(60, 22);
            this.NUD_MAX.TabIndex = 57;
            this.NUD_MAX.Value = new decimal(new int[] {
            110,
            0,
            0,
            0});
            this.NUD_MAX.VisibleChanged += new System.EventHandler(this.NUD_MAX_VisibleChanged);
            // 
            // NUD_MIN
            // 
            this.NUD_MIN.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.NUD_MIN.Location = new System.Drawing.Point(36, 38);
            this.NUD_MIN.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.NUD_MIN.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.NUD_MIN.Name = "NUD_MIN";
            this.NUD_MIN.Size = new System.Drawing.Size(60, 22);
            this.NUD_MIN.TabIndex = 56;
            this.NUD_MIN.Value = new decimal(new int[] {
            80,
            0,
            0,
            0});
            this.NUD_MIN.VisibleChanged += new System.EventHandler(this.NUD_MIN_VisibleChanged);
            // 
            // CB_USE
            // 
            this.CB_USE.AutoSize = true;
            this.CB_USE.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.CB_USE.Location = new System.Drawing.Point(98, 28);
            this.CB_USE.Name = "CB_USE";
            this.CB_USE.Size = new System.Drawing.Size(46, 17);
            this.CB_USE.TabIndex = 60;
            this.CB_USE.Text = "USE";
            this.CB_USE.UseVisualStyleBackColor = true;
            this.CB_USE.CheckedChanged += new System.EventHandler(this.CB_USE_CheckedChanged);
            // 
            // LB_MAX
            // 
            this.LB_MAX.AutoSize = true;
            this.LB_MAX.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LB_MAX.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.LB_MAX.Location = new System.Drawing.Point(0, 16);
            this.LB_MAX.Name = "LB_MAX";
            this.LB_MAX.Size = new System.Drawing.Size(35, 15);
            this.LB_MAX.TabIndex = 61;
            this.LB_MAX.Text = "MAX";
            this.LB_MAX.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LB_MIN
            // 
            this.LB_MIN.AutoSize = true;
            this.LB_MIN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LB_MIN.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.LB_MIN.Location = new System.Drawing.Point(0, 43);
            this.LB_MIN.Name = "LB_MIN";
            this.LB_MIN.Size = new System.Drawing.Size(32, 15);
            this.LB_MIN.TabIndex = 62;
            this.LB_MIN.Text = "MIN";
            this.LB_MIN.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.Location = new System.Drawing.Point(7, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 33);
            this.label2.TabIndex = 59;
            this.label2.Text = "~";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label2.Visible = false;
            // 
            // UC_MinMax
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.Controls.Add(this.LB_MIN);
            this.Controls.Add(this.LB_MAX);
            this.Controls.Add(this.NUD_MIN);
            this.Controls.Add(this.NUD_MAX);
            this.Controls.Add(this.CB_USE);
            this.Controls.Add(this.LB_NAME);
            this.Controls.Add(this.label2);
            this.Name = "UC_MinMax";
            this.Size = new System.Drawing.Size(143, 63);
            ((System.ComponentModel.ISupportInitialize)(this.NUD_MAX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUD_MIN)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LB_NAME;
        private System.Windows.Forms.NumericUpDown NUD_MAX;
        private System.Windows.Forms.NumericUpDown NUD_MIN;
        private System.Windows.Forms.CheckBox CB_USE;
        private System.Windows.Forms.Label LB_MAX;
        private System.Windows.Forms.Label LB_MIN;
        private System.Windows.Forms.Label label2;
    }
}
