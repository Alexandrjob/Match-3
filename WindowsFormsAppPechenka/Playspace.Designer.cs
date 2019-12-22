namespace WindowsFormsAppPechenka
{
    partial class PlayForm
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
            this.ExitToMainForm = new System.Windows.Forms.Button();
            this.labelTime = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            labelgamepoint = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ExitToMainForm
            // 
            this.ExitToMainForm.Location = new System.Drawing.Point(434, 172);
            this.ExitToMainForm.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ExitToMainForm.Name = "ExitToMainForm";
            this.ExitToMainForm.Size = new System.Drawing.Size(155, 50);
            this.ExitToMainForm.TabIndex = 0;
            this.ExitToMainForm.Text = "Выход ";
            this.ExitToMainForm.UseVisualStyleBackColor = true;
            this.ExitToMainForm.Click += new System.EventHandler(this.ExitToMainFormBotton);
            // 
            // labelTime
            // 
            this.labelTime.AutoSize = true;
            this.labelTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelTime.Location = new System.Drawing.Point(499, 34);
            this.labelTime.Name = "labelTime";
            this.labelTime.Size = new System.Drawing.Size(49, 36);
            this.labelTime.TabIndex = 1;
            this.labelTime.Text = "60";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(455, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(134, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Оставщееся время";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(455, 96);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(120, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "Колиество очков";
            // 
            // labelgamepoint
            // 
            labelgamepoint.AutoSize = true;
            labelgamepoint.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            labelgamepoint.Location = new System.Drawing.Point(500, 128);
            labelgamepoint.Name = "labelgamepoint";
            labelgamepoint.Size = new System.Drawing.Size(27, 29);
            labelgamepoint.TabIndex = 5;
            labelgamepoint.Text = "0";
            // 
            // PlayForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(707, 313);
            this.Controls.Add(labelgamepoint);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.labelTime);
            this.Controls.Add(this.ExitToMainForm);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "PlayForm";
            this.Text = "Match - 3";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Playspace_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ExitToMainForm;
        private System.Windows.Forms.Label labelTime;
        public static System.Windows.Forms.Label labelpoints;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        public static System.Windows.Forms.Label labelgamepoint;
    }
}