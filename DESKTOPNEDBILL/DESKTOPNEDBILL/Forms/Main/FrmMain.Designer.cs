namespace DESKTOPNEDBILL.Forms.Main
{
    partial class FrmMain
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
            this.components = new System.ComponentModel.Container();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.panelHeaderMiddle = new System.Windows.Forms.Panel();
            this.LblFinYear = new System.Windows.Forms.Label();
            this.LblCompanyName = new System.Windows.Forms.Label();
            this.panelHeaderRight = new System.Windows.Forms.Panel();
            this.BtnMinimize = new System.Windows.Forms.Button();
            this.BtnClose = new System.Windows.Forms.Button();
            this.lblTime = new System.Windows.Forms.Label();
            this.lblDate = new System.Windows.Forms.Label();
            this.panelHeaderLeft = new System.Windows.Forms.Panel();
            this.panelSidebar = new System.Windows.Forms.Panel();
            this.flowLayoutPanelSidebar = new System.Windows.Forms.FlowLayoutPanel();
            this.sidebarButton = new DESKTOPNEDBILL.CustomControl.SidebarButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnBackExit = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnModuleName = new System.Windows.Forms.Button();
            this.panelFooter = new System.Windows.Forms.Panel();
            this.lblUser = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panelHeader.SuspendLayout();
            this.panelHeaderMiddle.SuspendLayout();
            this.panelHeaderRight.SuspendLayout();
            this.panelSidebar.SuspendLayout();
            this.flowLayoutPanelSidebar.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panelFooter.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(112)))), ((int)(((byte)(173)))));
            this.panelHeader.Controls.Add(this.panelHeaderMiddle);
            this.panelHeader.Controls.Add(this.panelHeaderRight);
            this.panelHeader.Controls.Add(this.panelHeaderLeft);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1178, 60);
            this.panelHeader.TabIndex = 0;
            // 
            // panelHeaderMiddle
            // 
            this.panelHeaderMiddle.Controls.Add(this.LblFinYear);
            this.panelHeaderMiddle.Controls.Add(this.LblCompanyName);
            this.panelHeaderMiddle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelHeaderMiddle.Location = new System.Drawing.Point(169, 0);
            this.panelHeaderMiddle.Name = "panelHeaderMiddle";
            this.panelHeaderMiddle.Size = new System.Drawing.Size(779, 60);
            this.panelHeaderMiddle.TabIndex = 4;
            // 
            // LblFinYear
            // 
            this.LblFinYear.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.LblFinYear.AutoSize = true;
            this.LblFinYear.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblFinYear.ForeColor = System.Drawing.Color.White;
            this.LblFinYear.Location = new System.Drawing.Point(348, 41);
            this.LblFinYear.Name = "LblFinYear";
            this.LblFinYear.Size = new System.Drawing.Size(62, 21);
            this.LblFinYear.TabIndex = 1;
            this.LblFinYear.Text = "label1";
            // 
            // LblCompanyName
            // 
            this.LblCompanyName.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.LblCompanyName.AutoSize = true;
            this.LblCompanyName.Font = new System.Drawing.Font("Georgia", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblCompanyName.ForeColor = System.Drawing.Color.White;
            this.LblCompanyName.Location = new System.Drawing.Point(343, 11);
            this.LblCompanyName.Name = "LblCompanyName";
            this.LblCompanyName.Size = new System.Drawing.Size(81, 27);
            this.LblCompanyName.TabIndex = 0;
            this.LblCompanyName.Text = "label1";
            // 
            // panelHeaderRight
            // 
            this.panelHeaderRight.Controls.Add(this.BtnMinimize);
            this.panelHeaderRight.Controls.Add(this.BtnClose);
            this.panelHeaderRight.Controls.Add(this.lblTime);
            this.panelHeaderRight.Controls.Add(this.lblDate);
            this.panelHeaderRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelHeaderRight.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panelHeaderRight.Location = new System.Drawing.Point(948, 0);
            this.panelHeaderRight.Name = "panelHeaderRight";
            this.panelHeaderRight.Size = new System.Drawing.Size(230, 60);
            this.panelHeaderRight.TabIndex = 3;
            // 
            // BtnMinimize
            // 
            this.BtnMinimize.BackgroundImage = global::DESKTOPNEDBILL.Properties.Resources.icons8_minimize_50__1_;
            this.BtnMinimize.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.BtnMinimize.FlatAppearance.BorderSize = 0;
            this.BtnMinimize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnMinimize.Location = new System.Drawing.Point(150, 13);
            this.BtnMinimize.Name = "BtnMinimize";
            this.BtnMinimize.Size = new System.Drawing.Size(31, 31);
            this.BtnMinimize.TabIndex = 3;
            this.BtnMinimize.UseVisualStyleBackColor = true;
            this.BtnMinimize.Click += new System.EventHandler(this.BtnMinimize_Click);
            // 
            // BtnClose
            // 
            this.BtnClose.BackgroundImage = global::DESKTOPNEDBILL.Properties.Resources.icons8_close_50__1_;
            this.BtnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.BtnClose.FlatAppearance.BorderSize = 0;
            this.BtnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnClose.Location = new System.Drawing.Point(185, 13);
            this.BtnClose.Name = "BtnClose";
            this.BtnClose.Size = new System.Drawing.Size(31, 31);
            this.BtnClose.TabIndex = 2;
            this.BtnClose.UseVisualStyleBackColor = true;
            this.BtnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.Font = new System.Drawing.Font("Century Gothic", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTime.ForeColor = System.Drawing.Color.White;
            this.lblTime.Location = new System.Drawing.Point(7, 9);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(64, 22);
            this.lblTime.TabIndex = 0;
            this.lblTime.Text = "label1";
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Font = new System.Drawing.Font("Century Gothic", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDate.ForeColor = System.Drawing.Color.White;
            this.lblDate.Location = new System.Drawing.Point(7, 30);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(64, 22);
            this.lblDate.TabIndex = 1;
            this.lblDate.Text = "label2";
            // 
            // panelHeaderLeft
            // 
            this.panelHeaderLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelHeaderLeft.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panelHeaderLeft.Location = new System.Drawing.Point(0, 0);
            this.panelHeaderLeft.Name = "panelHeaderLeft";
            this.panelHeaderLeft.Size = new System.Drawing.Size(169, 60);
            this.panelHeaderLeft.TabIndex = 2;
            // 
            // panelSidebar
            // 
            this.panelSidebar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(112)))), ((int)(((byte)(173)))));
            this.panelSidebar.Controls.Add(this.flowLayoutPanelSidebar);
            this.panelSidebar.Controls.Add(this.panel2);
            this.panelSidebar.Controls.Add(this.panel1);
            this.panelSidebar.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelSidebar.Location = new System.Drawing.Point(944, 60);
            this.panelSidebar.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelSidebar.Name = "panelSidebar";
            this.panelSidebar.Padding = new System.Windows.Forms.Padding(0, 0, 10, 27);
            this.panelSidebar.Size = new System.Drawing.Size(234, 822);
            this.panelSidebar.TabIndex = 1;
            // 
            // flowLayoutPanelSidebar
            // 
            this.flowLayoutPanelSidebar.Controls.Add(this.sidebarButton);
            this.flowLayoutPanelSidebar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanelSidebar.Location = new System.Drawing.Point(0, 54);
            this.flowLayoutPanelSidebar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.flowLayoutPanelSidebar.Name = "flowLayoutPanelSidebar";
            this.flowLayoutPanelSidebar.Size = new System.Drawing.Size(224, 687);
            this.flowLayoutPanelSidebar.TabIndex = 3;
            // 
            // sidebarButton
            // 
            this.sidebarButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(112)))), ((int)(((byte)(173)))));
            this.sidebarButton.ButtonModId = "";
            this.sidebarButton.ButtonName = "button1";
            this.sidebarButton.Font = new System.Drawing.Font("Century Gothic", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sidebarButton.Location = new System.Drawing.Point(0, 0);
            this.sidebarButton.Margin = new System.Windows.Forms.Padding(0);
            this.sidebarButton.Name = "sidebarButton";
            this.sidebarButton.Size = new System.Drawing.Size(220, 46);
            this.sidebarButton.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnBackExit);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 741);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(12, 0, 5, 0);
            this.panel2.Size = new System.Drawing.Size(224, 54);
            this.panel2.TabIndex = 2;
            // 
            // btnBackExit
            // 
            this.btnBackExit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnBackExit.FlatAppearance.BorderSize = 0;
            this.btnBackExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBackExit.Font = new System.Drawing.Font("Georgia", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBackExit.ForeColor = System.Drawing.Color.White;
            this.btnBackExit.Location = new System.Drawing.Point(12, 0);
            this.btnBackExit.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnBackExit.Name = "btnBackExit";
            this.btnBackExit.Size = new System.Drawing.Size(207, 54);
            this.btnBackExit.TabIndex = 1;
            this.btnBackExit.Text = "E X I T";
            this.btnBackExit.UseVisualStyleBackColor = false;
            this.btnBackExit.Click += new System.EventHandler(this.btnBackExit_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnModuleName);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(13, 0, 5, 0);
            this.panel1.Size = new System.Drawing.Size(224, 54);
            this.panel1.TabIndex = 1;
            // 
            // btnModuleName
            // 
            this.btnModuleName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnModuleName.FlatAppearance.BorderSize = 0;
            this.btnModuleName.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnModuleName.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnModuleName.ForeColor = System.Drawing.Color.White;
            this.btnModuleName.Location = new System.Drawing.Point(13, 0);
            this.btnModuleName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnModuleName.Name = "btnModuleName";
            this.btnModuleName.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnModuleName.Size = new System.Drawing.Size(206, 54);
            this.btnModuleName.TabIndex = 0;
            this.btnModuleName.Text = "Module";
            this.btnModuleName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnModuleName.UseVisualStyleBackColor = false;
            // 
            // panelFooter
            // 
            this.panelFooter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(112)))), ((int)(((byte)(173)))));
            this.panelFooter.Controls.Add(this.lblUser);
            this.panelFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelFooter.Location = new System.Drawing.Point(0, 855);
            this.panelFooter.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelFooter.Name = "panelFooter";
            this.panelFooter.Size = new System.Drawing.Size(944, 27);
            this.panelFooter.TabIndex = 2;
            // 
            // lblUser
            // 
            this.lblUser.AutoSize = true;
            this.lblUser.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUser.ForeColor = System.Drawing.Color.White;
            this.lblUser.Location = new System.Drawing.Point(13, 0);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(60, 21);
            this.lblUser.TabIndex = 0;
            this.lblUser.Text = "label1";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1178, 882);
            this.Controls.Add(this.panelFooter);
            this.Controls.Add(this.panelSidebar);
            this.Controls.Add(this.panelHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.IsMdiContainer = true;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "FrmMain";
            this.Text = "NEDBILL";
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmMain_KeyDown);
            this.panelHeader.ResumeLayout(false);
            this.panelHeaderMiddle.ResumeLayout(false);
            this.panelHeaderMiddle.PerformLayout();
            this.panelHeaderRight.ResumeLayout(false);
            this.panelHeaderRight.PerformLayout();
            this.panelSidebar.ResumeLayout(false);
            this.flowLayoutPanelSidebar.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panelFooter.ResumeLayout(false);
            this.panelFooter.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Panel panelSidebar;
        private System.Windows.Forms.Panel panelFooter;
        private System.Windows.Forms.Button btnModuleName;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnBackExit;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelSidebar;
        private CustomControl.SidebarButton sidebarButton1;
        private CustomControl.SidebarButton sidebarButton;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Panel panelHeaderLeft;
        private System.Windows.Forms.Panel panelHeaderRight;
        private System.Windows.Forms.Button BtnMinimize;
        private System.Windows.Forms.Button BtnClose;
        private System.Windows.Forms.Panel panelHeaderMiddle;
        private System.Windows.Forms.Label LblCompanyName;
        private System.Windows.Forms.Label LblFinYear;
        private System.Windows.Forms.Label lblUser;
    }
}