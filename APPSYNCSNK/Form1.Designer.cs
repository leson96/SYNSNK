namespace APPSYNCSNK
{
    partial class SYNC_SNK
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
            this.btnon = new System.Windows.Forms.Button();
            this.btnoff = new System.Windows.Forms.Button();
            this.txtminutes = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnon
            // 
            this.btnon.Location = new System.Drawing.Point(49, 100);
            this.btnon.Name = "btnon";
            this.btnon.Size = new System.Drawing.Size(75, 23);
            this.btnon.TabIndex = 0;
            this.btnon.Text = "On";
            this.btnon.UseVisualStyleBackColor = true;
            this.btnon.Click += new System.EventHandler(this.btnon_Click);
            // 
            // btnoff
            // 
            this.btnoff.Location = new System.Drawing.Point(142, 100);
            this.btnoff.Name = "btnoff";
            this.btnoff.Size = new System.Drawing.Size(75, 23);
            this.btnoff.TabIndex = 1;
            this.btnoff.Text = "Off";
            this.btnoff.UseVisualStyleBackColor = true;
            this.btnoff.Click += new System.EventHandler(this.btnoff_Click);
            // 
            // txtminutes
            // 
            this.txtminutes.Location = new System.Drawing.Point(88, 46);
            this.txtminutes.Name = "txtminutes";
            this.txtminutes.Size = new System.Drawing.Size(101, 20);
            this.txtminutes.TabIndex = 2;
            // 
            // SYNC_SNK
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.txtminutes);
            this.Controls.Add(this.btnoff);
            this.Controls.Add(this.btnon);
            this.Name = "SYNC_SNK";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SYNCSNK";
            this.Load += new System.EventHandler(this.SYNC_SNK_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnon;
        private System.Windows.Forms.Button btnoff;
        private System.Windows.Forms.TextBox txtminutes;
    }
}

