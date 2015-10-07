namespace coe_fastTest
{
    partial class Form1
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panelCurrent = new System.Windows.Forms.Panel();
            this.cooldownLabel = new System.Windows.Forms.Label();
            this.panelNext = new System.Windows.Forms.Panel();
            this.panelCurrent.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(37, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "current";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(194, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(27, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "next";
            // 
            // panelCurrent
            // 
            this.panelCurrent.Controls.Add(this.cooldownLabel);
            this.panelCurrent.Location = new System.Drawing.Point(40, 86);
            this.panelCurrent.Name = "panelCurrent";
            this.panelCurrent.Size = new System.Drawing.Size(106, 116);
            this.panelCurrent.TabIndex = 2;
            // 
            // cooldownLabel
            // 
            this.cooldownLabel.AutoSize = true;
            this.cooldownLabel.Location = new System.Drawing.Point(46, 57);
            this.cooldownLabel.Name = "cooldownLabel";
            this.cooldownLabel.Size = new System.Drawing.Size(54, 13);
            this.cooldownLabel.TabIndex = 0;
            this.cooldownLabel.Text = "Cooldown";
            // 
            // panelNext
            // 
            this.panelNext.Location = new System.Drawing.Point(166, 86);
            this.panelNext.Name = "panelNext";
            this.panelNext.Size = new System.Drawing.Size(106, 116);
            this.panelNext.TabIndex = 3;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.panelNext);
            this.Controls.Add(this.panelCurrent);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "COE Timer";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panelCurrent.ResumeLayout(false);
            this.panelCurrent.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panelCurrent;
        private System.Windows.Forms.Label cooldownLabel;
        private System.Windows.Forms.Panel panelNext;
    }
}

