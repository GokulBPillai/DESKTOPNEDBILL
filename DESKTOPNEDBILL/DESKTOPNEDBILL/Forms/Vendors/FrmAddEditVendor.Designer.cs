namespace DESKTOPNEDBILL.Forms.Vendors
{
    partial class FrmAddEditVendor
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
            this.label13 = new System.Windows.Forms.Label();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.LblHeader = new System.Windows.Forms.Label();
            this.paneHeaderLine = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.panelFooter = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.BtnRefresh = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.TxtPIN = new System.Windows.Forms.TextBox();
            this.TxtOpBal = new System.Windows.Forms.TextBox();
            this.TxtAddress2 = new System.Windows.Forms.TextBox();
            this.TxtContactNo = new System.Windows.Forms.TextBox();
            this.TxtGSTNo = new System.Windows.Forms.TextBox();
            this.TxtTotBal = new System.Windows.Forms.TextBox();
            this.TxtVendor = new System.Windows.Forms.TextBox();
            this.CmbVendorType = new System.Windows.Forms.ComboBox();
            this.CmbStatus = new System.Windows.Forms.ComboBox();
            this.CmbState = new System.Windows.Forms.ComboBox();
            this.TxtAddress1 = new System.Windows.Forms.TextBox();
            this.panelHeader.SuspendLayout();
            this.paneHeaderLine.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panelFooter.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.DarkGray;
            this.label13.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(870, 10);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(21, 23);
            this.label13.TabIndex = 7;
            this.label13.Text = "X";
            this.label13.Click += new System.EventHandler(this.LblClose_Click);
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.SystemColors.Control;
            this.panelHeader.Controls.Add(this.label1);
            this.panelHeader.Controls.Add(this.LblHeader);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panelHeader.Location = new System.Drawing.Point(10, 40);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(884, 52);
            this.panelHeader.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(649, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(227, 18);
            this.label1.TabIndex = 1;
            this.label1.Text = "* mandatory fields to be filled";
            // 
            // LblHeader
            // 
            this.LblHeader.AutoSize = true;
            this.LblHeader.Location = new System.Drawing.Point(9, 21);
            this.LblHeader.Name = "LblHeader";
            this.LblHeader.Size = new System.Drawing.Size(178, 23);
            this.LblHeader.TabIndex = 0;
            this.LblHeader.Text = "Add New Vendor";
            // 
            // paneHeaderLine
            // 
            this.paneHeaderLine.BackColor = System.Drawing.SystemColors.Control;
            this.paneHeaderLine.Controls.Add(this.tableLayoutPanel1);
            this.paneHeaderLine.Dock = System.Windows.Forms.DockStyle.Top;
            this.paneHeaderLine.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.paneHeaderLine.Location = new System.Drawing.Point(10, 92);
            this.paneHeaderLine.Name = "paneHeaderLine";
            this.paneHeaderLine.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.paneHeaderLine.Size = new System.Drawing.Size(884, 27);
            this.paneHeaderLine.TabIndex = 9;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(112)))), ((int)(((byte)(173)))));
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(10, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(864, 27);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(3, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(139, 21);
            this.label2.TabIndex = 0;
            this.label2.Text = "Vendor Details";
            // 
            // panelFooter
            // 
            this.panelFooter.BackColor = System.Drawing.SystemColors.Control;
            this.panelFooter.Controls.Add(this.btnClose);
            this.panelFooter.Controls.Add(this.btnSave);
            this.panelFooter.Controls.Add(this.BtnRefresh);
            this.panelFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelFooter.Location = new System.Drawing.Point(10, 345);
            this.panelFooter.Name = "panelFooter";
            this.panelFooter.Size = new System.Drawing.Size(884, 86);
            this.panelFooter.TabIndex = 10;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(112)))), ((int)(((byte)(173)))));
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Century Gothic", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(740, 23);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(133, 43);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "Close [Esc]";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(112)))), ((int)(((byte)(173)))));
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Century Gothic", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(600, 23);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(133, 43);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Save [F1]";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // BtnRefresh
            // 
            this.BtnRefresh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(112)))), ((int)(((byte)(173)))));
            this.BtnRefresh.FlatAppearance.BorderSize = 0;
            this.BtnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnRefresh.Font = new System.Drawing.Font("Century Gothic", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnRefresh.ForeColor = System.Drawing.Color.White;
            this.BtnRefresh.Location = new System.Drawing.Point(459, 24);
            this.BtnRefresh.Name = "BtnRefresh";
            this.BtnRefresh.Size = new System.Drawing.Size(133, 43);
            this.BtnRefresh.TabIndex = 0;
            this.BtnRefresh.Text = "Refresh [F4]";
            this.BtnRefresh.UseVisualStyleBackColor = false;
            this.BtnRefresh.Click += new System.EventHandler(this.BtnRefresh_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.tableLayoutPanel2);
            this.panel1.Location = new System.Drawing.Point(10, 119);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(10);
            this.panel1.Size = new System.Drawing.Size(884, 231);
            this.panel1.TabIndex = 11;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 4;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel2.Controls.Add(this.label3, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.label4, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.label5, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.label6, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.label7, 0, 4);
            this.tableLayoutPanel2.Controls.Add(this.label8, 0, 5);
            this.tableLayoutPanel2.Controls.Add(this.label9, 2, 1);
            this.tableLayoutPanel2.Controls.Add(this.label10, 2, 2);
            this.tableLayoutPanel2.Controls.Add(this.label11, 2, 3);
            this.tableLayoutPanel2.Controls.Add(this.label12, 2, 4);
            this.tableLayoutPanel2.Controls.Add(this.label14, 2, 5);
            this.tableLayoutPanel2.Controls.Add(this.TxtPIN, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.TxtOpBal, 1, 4);
            this.tableLayoutPanel2.Controls.Add(this.TxtAddress2, 3, 1);
            this.tableLayoutPanel2.Controls.Add(this.TxtContactNo, 3, 2);
            this.tableLayoutPanel2.Controls.Add(this.TxtGSTNo, 3, 3);
            this.tableLayoutPanel2.Controls.Add(this.TxtTotBal, 3, 4);
            this.tableLayoutPanel2.Controls.Add(this.TxtVendor, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.CmbVendorType, 1, 3);
            this.tableLayoutPanel2.Controls.Add(this.CmbStatus, 1, 5);
            this.tableLayoutPanel2.Controls.Add(this.CmbState, 3, 5);
            this.tableLayoutPanel2.Controls.Add(this.TxtAddress1, 1, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(10, 10);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 6;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(864, 211);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(3, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 21);
            this.label3.TabIndex = 0;
            this.label3.Text = "Name*";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(3, 42);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(91, 21);
            this.label4.TabIndex = 1;
            this.label4.Text = "Address 1";
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(3, 77);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(91, 21);
            this.label5.TabIndex = 2;
            this.label5.Text = "PIN Code";
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(3, 112);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(116, 21);
            this.label6.TabIndex = 3;
            this.label6.Text = "Vendor Type";
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(3, 147);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(109, 21);
            this.label7.TabIndex = 4;
            this.label7.Text = "OP Balance";
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(3, 182);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(61, 21);
            this.label8.TabIndex = 5;
            this.label8.Text = "Status";
            // 
            // label9
            // 
            this.label9.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(434, 42);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(91, 21);
            this.label9.TabIndex = 6;
            this.label9.Text = "Address 2";
            // 
            // label10
            // 
            this.label10.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(434, 77);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(116, 21);
            this.label10.TabIndex = 7;
            this.label10.Text = "Contact No.";
            // 
            // label11
            // 
            this.label11.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(434, 112);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(114, 21);
            this.label11.TabIndex = 8;
            this.label11.Text = "GST/Vat No.";
            // 
            // label12
            // 
            this.label12.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(434, 147);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(125, 21);
            this.label12.TabIndex = 9;
            this.label12.Text = "Total Balance";
            // 
            // label14
            // 
            this.label14.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(434, 182);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(108, 21);
            this.label14.TabIndex = 10;
            this.label14.Text = "State Code";
            // 
            // TxtPIN
            // 
            this.TxtPIN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtPIN.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TxtPIN.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtPIN.Location = new System.Drawing.Point(175, 73);
            this.TxtPIN.MaxLength = 6;
            this.TxtPIN.Name = "TxtPIN";
            this.TxtPIN.Size = new System.Drawing.Size(253, 28);
            this.TxtPIN.TabIndex = 4;
            this.TxtPIN.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtPIN_KeyDown);
            this.TxtPIN.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtPIN_KeyPress);
            // 
            // TxtOpBal
            // 
            this.TxtOpBal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtOpBal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TxtOpBal.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtOpBal.Location = new System.Drawing.Point(175, 143);
            this.TxtOpBal.Name = "TxtOpBal";
            this.TxtOpBal.Size = new System.Drawing.Size(253, 28);
            this.TxtOpBal.TabIndex = 8;
            this.TxtOpBal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxtOpBal.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtOpBal_KeyDown);
            this.TxtOpBal.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtOpBal_KeyPress);
            // 
            // TxtAddress2
            // 
            this.TxtAddress2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtAddress2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TxtAddress2.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtAddress2.Location = new System.Drawing.Point(606, 38);
            this.TxtAddress2.Name = "TxtAddress2";
            this.TxtAddress2.Size = new System.Drawing.Size(255, 28);
            this.TxtAddress2.TabIndex = 3;
            this.TxtAddress2.TextChanged += new System.EventHandler(this.TxtAddress2_TextChanged);
            this.TxtAddress2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtAddress2_KeyDown);
            // 
            // TxtContactNo
            // 
            this.TxtContactNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtContactNo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TxtContactNo.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtContactNo.Location = new System.Drawing.Point(606, 73);
            this.TxtContactNo.MaxLength = 10;
            this.TxtContactNo.Name = "TxtContactNo";
            this.TxtContactNo.Size = new System.Drawing.Size(255, 28);
            this.TxtContactNo.TabIndex = 5;
            this.TxtContactNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtContactNo_KeyDown);
            this.TxtContactNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtContactNo_KeyPress);
            // 
            // TxtGSTNo
            // 
            this.TxtGSTNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtGSTNo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TxtGSTNo.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtGSTNo.Location = new System.Drawing.Point(606, 108);
            this.TxtGSTNo.Name = "TxtGSTNo";
            this.TxtGSTNo.Size = new System.Drawing.Size(255, 28);
            this.TxtGSTNo.TabIndex = 7;
            this.TxtGSTNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtGSTNo_KeyDown);
            // 
            // TxtTotBal
            // 
            this.TxtTotBal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtTotBal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TxtTotBal.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtTotBal.Location = new System.Drawing.Point(606, 143);
            this.TxtTotBal.Name = "TxtTotBal";
            this.TxtTotBal.Size = new System.Drawing.Size(255, 28);
            this.TxtTotBal.TabIndex = 9;
            this.TxtTotBal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxtTotBal.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtTotBal_KeyDown);
            this.TxtTotBal.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtTotBal_KeyPress);
            // 
            // TxtVendor
            // 
            this.TxtVendor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tableLayoutPanel2.SetColumnSpan(this.TxtVendor, 3);
            this.TxtVendor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TxtVendor.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtVendor.Location = new System.Drawing.Point(175, 3);
            this.TxtVendor.Name = "TxtVendor";
            this.TxtVendor.Size = new System.Drawing.Size(686, 28);
            this.TxtVendor.TabIndex = 1;
            this.TxtVendor.TabIndexChanged += new System.EventHandler(this.TxtVendor_TabIndexChanged);
            this.TxtVendor.TextChanged += new System.EventHandler(this.TxtVendor_TextChanged);
            this.TxtVendor.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtVendor_KeyDown);
            // 
            // CmbVendorType
            // 
            this.CmbVendorType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CmbVendorType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbVendorType.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CmbVendorType.FormattingEnabled = true;
            this.CmbVendorType.Items.AddRange(new object[] {
            "Local",
            "Interstate"});
            this.CmbVendorType.Location = new System.Drawing.Point(175, 108);
            this.CmbVendorType.Name = "CmbVendorType";
            this.CmbVendorType.Size = new System.Drawing.Size(253, 29);
            this.CmbVendorType.TabIndex = 6;
            this.CmbVendorType.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CmbVendorType_KeyDown);
            // 
            // CmbStatus
            // 
            this.CmbStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbStatus.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CmbStatus.FormattingEnabled = true;
            this.CmbStatus.Items.AddRange(new object[] {
            "Active",
            "InActive"});
            this.CmbStatus.Location = new System.Drawing.Point(175, 178);
            this.CmbStatus.Name = "CmbStatus";
            this.CmbStatus.Size = new System.Drawing.Size(253, 29);
            this.CmbStatus.TabIndex = 10;
            this.CmbStatus.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CmbStatus_KeyDown);
            // 
            // CmbState
            // 
            this.CmbState.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CmbState.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbState.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CmbState.FormattingEnabled = true;
            this.CmbState.Location = new System.Drawing.Point(606, 178);
            this.CmbState.Name = "CmbState";
            this.CmbState.Size = new System.Drawing.Size(255, 29);
            this.CmbState.TabIndex = 11;
            // 
            // TxtAddress1
            // 
            this.TxtAddress1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtAddress1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TxtAddress1.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtAddress1.Location = new System.Drawing.Point(175, 38);
            this.TxtAddress1.Name = "TxtAddress1";
            this.TxtAddress1.Size = new System.Drawing.Size(253, 28);
            this.TxtAddress1.TabIndex = 2;
            this.TxtAddress1.TextChanged += new System.EventHandler(this.TxtAddress1_TextChanged);
            this.TxtAddress1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtAddress1_KeyDown);
            // 
            // FrmAddEditVendor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(112)))), ((int)(((byte)(173)))));
            this.ClientSize = new System.Drawing.Size(904, 441);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelFooter);
            this.Controls.Add(this.paneHeaderLine);
            this.Controls.Add(this.panelHeader);
            this.Controls.Add(this.label13);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "FrmAddEditVendor";
            this.Padding = new System.Windows.Forms.Padding(10, 40, 10, 10);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FrmAddEditVendor";
            this.Load += new System.EventHandler(this.FrmAddEditVendor_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmAddEditVendor_KeyDown);
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.paneHeaderLine.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panelFooter.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label LblHeader;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel paneHeaderLine;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panelFooter;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button BtnRefresh;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox TxtVendor;
        private System.Windows.Forms.TextBox TxtAddress1;
        private System.Windows.Forms.TextBox TxtPIN;
        private System.Windows.Forms.TextBox TxtOpBal;
        private System.Windows.Forms.TextBox TxtAddress2;
        private System.Windows.Forms.TextBox TxtContactNo;
        private System.Windows.Forms.TextBox TxtGSTNo;
        private System.Windows.Forms.TextBox TxtTotBal;
        private System.Windows.Forms.ComboBox CmbVendorType;
        private System.Windows.Forms.ComboBox CmbStatus;
        private System.Windows.Forms.ComboBox CmbState;
    }
}