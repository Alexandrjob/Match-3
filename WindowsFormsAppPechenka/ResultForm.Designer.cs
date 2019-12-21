namespace WindowsFormsAppPechenka
{
    partial class ResultForm
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
            this.menu = new System.Windows.Forms.Button();
            this.gameagain = new System.Windows.Forms.Button();
            this.labelgamepoint = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // menu
            // 
            this.menu.Location = new System.Drawing.Point(227, 166);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(100, 35);
            this.menu.TabIndex = 1;
            this.menu.Text = "Меню";
            this.menu.UseVisualStyleBackColor = true;
            this.menu.Click += new System.EventHandler(this.ExitToMainFormButton);
            // 
            // gameagain
            // 
            this.gameagain.Location = new System.Drawing.Point(45, 166);
            this.gameagain.Name = "gameagain";
            this.gameagain.Size = new System.Drawing.Size(100, 35);
            this.gameagain.TabIndex = 2;
            this.gameagain.Text = "Заново";
            this.gameagain.UseVisualStyleBackColor = true;
            this.gameagain.Click += new System.EventHandler(this.GameAgainButton);
            // 
            // labelgamepoint
            // 
            this.labelgamepoint.AutoSize = true;
            this.labelgamepoint.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelgamepoint.Location = new System.Drawing.Point(154, 90);
            this.labelgamepoint.Name = "labelgamepoint";
            this.labelgamepoint.Size = new System.Drawing.Size(27, 29);
            this.labelgamepoint.TabIndex = 3;
            this.labelgamepoint.Text = "0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(76, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(213, 20);
            this.label1.TabIndex = 4;
            this.label1.Text = "Набранные очки за игру";
            // 
            // ResultForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(382, 246);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labelgamepoint);
            this.Controls.Add(this.gameagain);
            this.Controls.Add(this.menu);
            this.Name = "ResultForm";
            this.Text = "ResultForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button menu;
        private System.Windows.Forms.Button gameagain;
        private System.Windows.Forms.Label labelgamepoint;
        private System.Windows.Forms.Label label1;
    }
}