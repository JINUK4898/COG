namespace COG
{
    partial class UC_OFFPOS
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
            this.LB_X = new System.Windows.Forms.Label();
            this.NUD_OFF_X = new System.Windows.Forms.NumericUpDown();
            this.LB_Y = new System.Windows.Forms.Label();
            this.NUD_OFF_Y = new System.Windows.Forms.NumericUpDown();
            this.LB_W = new System.Windows.Forms.Label();
            this.NUD_WIDTH = new System.Windows.Forms.NumericUpDown();
            this.LB_H = new System.Windows.Forms.Label();
            this.NUD_HEIGHT = new System.Windows.Forms.NumericUpDown();
            this.LB_NAME = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.NUD_OFF_X)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUD_OFF_Y)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUD_WIDTH)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUD_HEIGHT)).BeginInit();
            this.SuspendLayout();
            // 
            // LB_X
            // 
            this.LB_X.AutoSize = true;
            this.LB_X.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LB_X.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.LB_X.ForeColor = System.Drawing.Color.White;
            this.LB_X.Location = new System.Drawing.Point(2, 23);
            this.LB_X.Name = "LB_X";
            this.LB_X.Size = new System.Drawing.Size(39, 15);
            this.LB_X.TabIndex = 63;
            this.LB_X.Text = "OFF X";
            this.LB_X.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // NUD_OFF_X
            // 
            this.NUD_OFF_X.DecimalPlaces = 1;
            this.NUD_OFF_X.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.NUD_OFF_X.Location = new System.Drawing.Point(56, 22);
            this.NUD_OFF_X.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.NUD_OFF_X.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.NUD_OFF_X.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.NUD_OFF_X.Name = "NUD_OFF_X";
            this.NUD_OFF_X.Size = new System.Drawing.Size(60, 22);
            this.NUD_OFF_X.TabIndex = 62;
            // 
            // LB_Y
            // 
            this.LB_Y.AutoSize = true;
            this.LB_Y.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LB_Y.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.LB_Y.ForeColor = System.Drawing.Color.White;
            this.LB_Y.Location = new System.Drawing.Point(2, 45);
            this.LB_Y.Name = "LB_Y";
            this.LB_Y.Size = new System.Drawing.Size(38, 15);
            this.LB_Y.TabIndex = 65;
            this.LB_Y.Text = "OFF Y";
            this.LB_Y.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // NUD_OFF_Y
            // 
            this.NUD_OFF_Y.DecimalPlaces = 1;
            this.NUD_OFF_Y.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.NUD_OFF_Y.Location = new System.Drawing.Point(56, 45);
            this.NUD_OFF_Y.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.NUD_OFF_Y.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.NUD_OFF_Y.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.NUD_OFF_Y.Name = "NUD_OFF_Y";
            this.NUD_OFF_Y.Size = new System.Drawing.Size(60, 22);
            this.NUD_OFF_Y.TabIndex = 64;
            // 
            // LB_W
            // 
            this.LB_W.AutoSize = true;
            this.LB_W.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LB_W.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.LB_W.ForeColor = System.Drawing.Color.White;
            this.LB_W.Location = new System.Drawing.Point(2, 68);
            this.LB_W.Name = "LB_W";
            this.LB_W.Size = new System.Drawing.Size(44, 15);
            this.LB_W.TabIndex = 67;
            this.LB_W.Text = "WIDTH";
            this.LB_W.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // NUD_WIDTH
            // 
            this.NUD_WIDTH.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.NUD_WIDTH.Location = new System.Drawing.Point(56, 68);
            this.NUD_WIDTH.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.NUD_WIDTH.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.NUD_WIDTH.Name = "NUD_WIDTH";
            this.NUD_WIDTH.Size = new System.Drawing.Size(60, 22);
            this.NUD_WIDTH.TabIndex = 66;
            this.NUD_WIDTH.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // LB_H
            // 
            this.LB_H.AutoSize = true;
            this.LB_H.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LB_H.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.LB_H.ForeColor = System.Drawing.Color.White;
            this.LB_H.Location = new System.Drawing.Point(2, 91);
            this.LB_H.Name = "LB_H";
            this.LB_H.Size = new System.Drawing.Size(48, 15);
            this.LB_H.TabIndex = 69;
            this.LB_H.Text = "HEIGHT";
            this.LB_H.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // NUD_HEIGHT
            // 
            this.NUD_HEIGHT.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.NUD_HEIGHT.Location = new System.Drawing.Point(56, 91);
            this.NUD_HEIGHT.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.NUD_HEIGHT.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.NUD_HEIGHT.Name = "NUD_HEIGHT";
            this.NUD_HEIGHT.Size = new System.Drawing.Size(60, 22);
            this.NUD_HEIGHT.TabIndex = 68;
            this.NUD_HEIGHT.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // LB_NAME
            // 
            this.LB_NAME.AutoSize = true;
            this.LB_NAME.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LB_NAME.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.LB_NAME.ForeColor = System.Drawing.Color.White;
            this.LB_NAME.Location = new System.Drawing.Point(56, 4);
            this.LB_NAME.Name = "LB_NAME";
            this.LB_NAME.Size = new System.Drawing.Size(40, 15);
            this.LB_NAME.TabIndex = 70;
            this.LB_NAME.Text = "NAME";
            this.LB_NAME.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // UC_OFFPOS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 11F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.Controls.Add(this.LB_NAME);
            this.Controls.Add(this.LB_H);
            this.Controls.Add(this.NUD_HEIGHT);
            this.Controls.Add(this.LB_W);
            this.Controls.Add(this.NUD_WIDTH);
            this.Controls.Add(this.LB_Y);
            this.Controls.Add(this.NUD_OFF_Y);
            this.Controls.Add(this.LB_X);
            this.Controls.Add(this.NUD_OFF_X);
            this.Font = new System.Drawing.Font("굴림", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Name = "UC_OFFPOS";
            this.Size = new System.Drawing.Size(122, 119);
            ((System.ComponentModel.ISupportInitialize)(this.NUD_OFF_X)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUD_OFF_Y)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUD_WIDTH)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUD_HEIGHT)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LB_X;
        private System.Windows.Forms.NumericUpDown NUD_OFF_X;
        private System.Windows.Forms.Label LB_Y;
        private System.Windows.Forms.NumericUpDown NUD_OFF_Y;
        private System.Windows.Forms.Label LB_W;
        private System.Windows.Forms.NumericUpDown NUD_WIDTH;
        private System.Windows.Forms.Label LB_H;
        private System.Windows.Forms.NumericUpDown NUD_HEIGHT;
        private System.Windows.Forms.Label LB_NAME;
    }
}
