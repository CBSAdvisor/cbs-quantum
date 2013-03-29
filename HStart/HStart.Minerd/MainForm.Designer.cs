namespace HStart.Minerd
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this._btnRun = new System.Windows.Forms.Button();
            this._lblProcOutput = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // _btnRun
            // 
            this._btnRun.Location = new System.Drawing.Point(360, 139);
            this._btnRun.Name = "_btnRun";
            this._btnRun.Size = new System.Drawing.Size(75, 23);
            this._btnRun.TabIndex = 0;
            this._btnRun.Text = "Run";
            this._btnRun.UseVisualStyleBackColor = true;
            this._btnRun.Click += new System.EventHandler(this._btnRun_Click);
            // 
            // _lblProcOutput
            // 
            this._lblProcOutput.BackColor = System.Drawing.Color.Black;
            this._lblProcOutput.Font = new System.Drawing.Font("Courier New", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this._lblProcOutput.ForeColor = System.Drawing.Color.Lime;
            this._lblProcOutput.Location = new System.Drawing.Point(12, 102);
            this._lblProcOutput.Name = "_lblProcOutput";
            this._lblProcOutput.Size = new System.Drawing.Size(423, 23);
            this._lblProcOutput.TabIndex = 1;
            this._lblProcOutput.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(447, 174);
            this.Controls.Add(this._lblProcOutput);
            this.Controls.Add(this._btnRun);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hidden start minerd";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button _btnRun;
        private System.Windows.Forms.Label _lblProcOutput;
    }
}

