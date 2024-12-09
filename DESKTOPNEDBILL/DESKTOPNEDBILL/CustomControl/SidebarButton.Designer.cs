namespace DESKTOPNEDBILL.CustomControl
{
    partial class SidebarButton
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelHighlight = new System.Windows.Forms.Panel();
            this.btnSide = new System.Windows.Forms.Button();
            this.panelHighlight.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelHighlight
            // 
            this.panelHighlight.Controls.Add(this.btnSide);
            this.panelHighlight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelHighlight.Location = new System.Drawing.Point(0, 0);
            this.panelHighlight.Name = "panelHighlight";
            this.panelHighlight.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.panelHighlight.Size = new System.Drawing.Size(215, 54);
            this.panelHighlight.TabIndex = 0;
            // 
            // btnSide
            // 
            this.btnSide.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(112)))), ((int)(((byte)(173)))));
            this.btnSide.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSide.FlatAppearance.BorderSize = 0;
            this.btnSide.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSide.ForeColor = System.Drawing.Color.White;
            this.btnSide.Location = new System.Drawing.Point(10, 0);
            this.btnSide.Name = "btnSide";
            this.btnSide.Size = new System.Drawing.Size(205, 54);
            this.btnSide.TabIndex = 0;
            this.btnSide.Text = "button1";
            this.btnSide.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSide.UseVisualStyleBackColor = false;
            this.btnSide.Click += new System.EventHandler(this.btnSide_Click);
            this.btnSide.Paint += new System.Windows.Forms.PaintEventHandler(this.btnSide_Paint);
            // 
            // SidebarButton
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(112)))), ((int)(((byte)(173)))));
            this.Controls.Add(this.panelHighlight);
            this.Font = new System.Drawing.Font("Century Gothic", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.Name = "SidebarButton";
            this.Size = new System.Drawing.Size(215, 54);
            this.panelHighlight.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelHighlight;
        private System.Windows.Forms.Button btnSide;
    }
}
