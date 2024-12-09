namespace DESKTOPNEDBILL.Forms.Banks
{
    partial class FrmAddEditBank
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.BtnSave = new System.Windows.Forms.Button();
            this.BtnClose = new System.Windows.Forms.Button();
            this.BtnClear = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.LblHeader = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.label12 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.TxtBankName = new System.Windows.Forms.TextBox();
            this.TxtAddress = new System.Windows.Forms.TextBox();
            this.TxtAcNo = new System.Windows.Forms.TextBox();
            this.TxtChequefrom = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.TxtBranch = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.TxtChequeto = new System.Windows.Forms.TextBox();
            this.CmbAcType = new System.Windows.Forms.ComboBox();
            this.TxtEmail = new System.Windows.Forms.TextBox();
            this.TxtCreditlimit = new System.Windows.Forms.TextBox();
            this.TxtPINCode = new System.Windows.Forms.TextBox();
            this.TxtBankabbrivation = new System.Windows.Forms.TextBox();
            this.CmbStatus = new System.Windows.Forms.ComboBox();
            this.TxtPhone = new System.Windows.Forms.TextBox();
            this.TxtIFC = new System.Windows.Forms.TextBox();
            this.LblClose = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panelHeader.SuspendLayout();
            this.panel5.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.BtnSave);
            this.panel1.Controls.Add(this.BtnClose);
            this.panel1.Controls.Add(this.BtnClear);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(10, 421);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(699, 113);
            this.panel1.TabIndex = 7;
            // 
            // BtnSave
            // 
            this.BtnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(112)))), ((int)(((byte)(173)))));
            this.BtnSave.FlatAppearance.BorderSize = 0;
            this.BtnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnSave.Font = new System.Drawing.Font("Century Gothic", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSave.ForeColor = System.Drawing.Color.White;
            this.BtnSave.Location = new System.Drawing.Point(438, 44);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(121, 43);
            this.BtnSave.TabIndex = 27;
            this.BtnSave.Text = "Save [F1]";
            this.BtnSave.UseVisualStyleBackColor = false;
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // BtnClose
            // 
            this.BtnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(112)))), ((int)(((byte)(173)))));
            this.BtnClose.FlatAppearance.BorderSize = 0;
            this.BtnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnClose.Font = new System.Drawing.Font("Century Gothic", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnClose.ForeColor = System.Drawing.Color.White;
            this.BtnClose.Location = new System.Drawing.Point(567, 44);
            this.BtnClose.Name = "BtnClose";
            this.BtnClose.Size = new System.Drawing.Size(121, 43);
            this.BtnClose.TabIndex = 29;
            this.BtnClose.Text = "Close [Esc]";
            this.BtnClose.UseVisualStyleBackColor = false;
            this.BtnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // BtnClear
            // 
            this.BtnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnClear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(112)))), ((int)(((byte)(173)))));
            this.BtnClear.FlatAppearance.BorderSize = 0;
            this.BtnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnClear.Font = new System.Drawing.Font("Century Gothic", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnClear.ForeColor = System.Drawing.Color.White;
            this.BtnClear.Location = new System.Drawing.Point(301, 44);
            this.BtnClear.Name = "BtnClear";
            this.BtnClear.Size = new System.Drawing.Size(129, 43);
            this.BtnClear.TabIndex = 28;
            this.BtnClear.Text = "Refresh [F4]";
            this.BtnClear.UseVisualStyleBackColor = false;
            this.BtnClear.Click += new System.EventHandler(this.BtnClear_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.Control;
            this.panel2.Controls.Add(this.tableLayoutPanel1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(10, 92);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.panel2.Size = new System.Drawing.Size(699, 27);
            this.panel2.TabIndex = 6;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(112)))), ((int)(((byte)(173)))));
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(10, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(679, 27);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Century Gothic", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(3, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(114, 21);
            this.label3.TabIndex = 0;
            this.label3.Text = "Bank Details";
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.SystemColors.Control;
            this.panelHeader.Controls.Add(this.label2);
            this.panelHeader.Controls.Add(this.LblHeader);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panelHeader.Location = new System.Drawing.Point(10, 40);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(699, 52);
            this.panelHeader.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(454, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(227, 18);
            this.label2.TabIndex = 1;
            this.label2.Text = "* mandatory fields to be filled";
            // 
            // LblHeader
            // 
            this.LblHeader.AutoSize = true;
            this.LblHeader.Location = new System.Drawing.Point(9, 21);
            this.LblHeader.Name = "LblHeader";
            this.LblHeader.Size = new System.Drawing.Size(149, 23);
            this.LblHeader.TabIndex = 0;
            this.LblHeader.Text = "Add/Edit Bank";
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.SystemColors.Control;
            this.panel5.Controls.Add(this.tableLayoutPanel4);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(10, 119);
            this.panel5.Name = "panel5";
            this.panel5.Padding = new System.Windows.Forms.Padding(10);
            this.panel5.Size = new System.Drawing.Size(699, 302);
            this.panel5.TabIndex = 8;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 4;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel4.Controls.Add(this.label12, 2, 6);
            this.tableLayoutPanel4.Controls.Add(this.label5, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.label7, 0, 7);
            this.tableLayoutPanel4.Controls.Add(this.label11, 0, 6);
            this.tableLayoutPanel4.Controls.Add(this.label6, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.label10, 0, 5);
            this.tableLayoutPanel4.Controls.Add(this.label15, 2, 3);
            this.tableLayoutPanel4.Controls.Add(this.label14, 2, 1);
            this.tableLayoutPanel4.Controls.Add(this.label17, 2, 4);
            this.tableLayoutPanel4.Controls.Add(this.label18, 2, 5);
            this.tableLayoutPanel4.Controls.Add(this.TxtBankName, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.TxtAddress, 1, 1);
            this.tableLayoutPanel4.Controls.Add(this.TxtAcNo, 3, 1);
            this.tableLayoutPanel4.Controls.Add(this.TxtChequefrom, 3, 5);
            this.tableLayoutPanel4.Controls.Add(this.label16, 0, 2);
            this.tableLayoutPanel4.Controls.Add(this.TxtBranch, 1, 2);
            this.tableLayoutPanel4.Controls.Add(this.label9, 2, 2);
            this.tableLayoutPanel4.Controls.Add(this.label8, 0, 4);
            this.tableLayoutPanel4.Controls.Add(this.label1, 0, 3);
            this.tableLayoutPanel4.Controls.Add(this.TxtChequeto, 1, 6);
            this.tableLayoutPanel4.Controls.Add(this.CmbAcType, 1, 4);
            this.tableLayoutPanel4.Controls.Add(this.TxtEmail, 3, 4);
            this.tableLayoutPanel4.Controls.Add(this.TxtCreditlimit, 1, 5);
            this.tableLayoutPanel4.Controls.Add(this.TxtPINCode, 3, 2);
            this.tableLayoutPanel4.Controls.Add(this.TxtBankabbrivation, 3, 6);
            this.tableLayoutPanel4.Controls.Add(this.CmbStatus, 1, 7);
            this.tableLayoutPanel4.Controls.Add(this.TxtPhone, 1, 3);
            this.tableLayoutPanel4.Controls.Add(this.TxtIFC, 3, 3);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(10, 10);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 8;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(679, 282);
            this.tableLayoutPanel4.TabIndex = 1;
            // 
            // label12
            // 
            this.label12.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(341, 217);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(102, 21);
            this.label12.TabIndex = 7;
            this.label12.Text = "Bank Abrv.";
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(3, 7);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(118, 21);
            this.label5.TabIndex = 0;
            this.label5.Text = "Bank Name *";
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(3, 253);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(61, 21);
            this.label7.TabIndex = 2;
            this.label7.Text = "Status";
            // 
            // label11
            // 
            this.label11.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(3, 217);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(86, 21);
            this.label11.TabIndex = 6;
            this.label11.Text = "Cheq. To";
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(3, 42);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 21);
            this.label6.TabIndex = 1;
            this.label6.Text = "Address";
            // 
            // label10
            // 
            this.label10.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(3, 182);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(103, 21);
            this.label10.TabIndex = 5;
            this.label10.Text = "Credit Limit";
            // 
            // label15
            // 
            this.label15.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(341, 112);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(45, 21);
            this.label15.TabIndex = 10;
            this.label15.Text = "IFSC";
            // 
            // label14
            // 
            this.label14.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(341, 42);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(75, 21);
            this.label14.TabIndex = 9;
            this.label14.Text = "A/c No.";
            // 
            // label17
            // 
            this.label17.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(341, 147);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(52, 21);
            this.label17.TabIndex = 12;
            this.label17.Text = "Email";
            // 
            // label18
            // 
            this.label18.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(341, 182);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(106, 21);
            this.label18.TabIndex = 13;
            this.label18.Text = "Cheq. From";
            // 
            // TxtBankName
            // 
            this.TxtBankName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tableLayoutPanel4.SetColumnSpan(this.TxtBankName, 3);
            this.TxtBankName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TxtBankName.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtBankName.Location = new System.Drawing.Point(138, 3);
            this.TxtBankName.Name = "TxtBankName";
            this.TxtBankName.Size = new System.Drawing.Size(538, 28);
            this.TxtBankName.TabIndex = 1;
            this.TxtBankName.TextChanged += new System.EventHandler(this.TxtBankName_TextChanged);
            this.TxtBankName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtBankName_KeyDown);
            // 
            // TxtAddress
            // 
            this.TxtAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtAddress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TxtAddress.Font = new System.Drawing.Font("Century Gothic", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtAddress.Location = new System.Drawing.Point(138, 38);
            this.TxtAddress.Name = "TxtAddress";
            this.TxtAddress.Size = new System.Drawing.Size(197, 30);
            this.TxtAddress.TabIndex = 2;
            this.TxtAddress.TextChanged += new System.EventHandler(this.TxtAddress_TextChanged);
            this.TxtAddress.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtAddress_KeyDown);
            // 
            // TxtAcNo
            // 
            this.TxtAcNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtAcNo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TxtAcNo.Font = new System.Drawing.Font("Century Gothic", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtAcNo.Location = new System.Drawing.Point(476, 38);
            this.TxtAcNo.Name = "TxtAcNo";
            this.TxtAcNo.Size = new System.Drawing.Size(200, 30);
            this.TxtAcNo.TabIndex = 3;
            this.TxtAcNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtAcNo_KeyDown);
            this.TxtAcNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtAcNo_KeyPress);
            // 
            // TxtChequefrom
            // 
            this.TxtChequefrom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtChequefrom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TxtChequefrom.Font = new System.Drawing.Font("Century Gothic", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtChequefrom.Location = new System.Drawing.Point(476, 178);
            this.TxtChequefrom.Name = "TxtChequefrom";
            this.TxtChequefrom.Size = new System.Drawing.Size(200, 30);
            this.TxtChequefrom.TabIndex = 11;
            this.TxtChequefrom.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxtChequefrom.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtChequefrom_KeyDown);
            // 
            // label16
            // 
            this.label16.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(3, 77);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(68, 21);
            this.label16.TabIndex = 11;
            this.label16.Text = "Branch";
            // 
            // TxtBranch
            // 
            this.TxtBranch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtBranch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TxtBranch.Font = new System.Drawing.Font("Century Gothic", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtBranch.Location = new System.Drawing.Point(138, 73);
            this.TxtBranch.Name = "TxtBranch";
            this.TxtBranch.Size = new System.Drawing.Size(197, 30);
            this.TxtBranch.TabIndex = 4;
            this.TxtBranch.TextChanged += new System.EventHandler(this.TxtBranch_TextChanged);
            this.TxtBranch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtBranch_KeyDown);
            // 
            // label9
            // 
            this.label9.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(341, 77);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(91, 21);
            this.label9.TabIndex = 4;
            this.label9.Text = "PIN Code";
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(3, 147);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(85, 21);
            this.label8.TabIndex = 3;
            this.label8.Text = "A/c Type";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 112);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 21);
            this.label1.TabIndex = 25;
            this.label1.Text = "Phone";
            // 
            // TxtChequeto
            // 
            this.TxtChequeto.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtChequeto.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TxtChequeto.Font = new System.Drawing.Font("Century Gothic", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtChequeto.Location = new System.Drawing.Point(138, 213);
            this.TxtChequeto.Name = "TxtChequeto";
            this.TxtChequeto.Size = new System.Drawing.Size(197, 30);
            this.TxtChequeto.TabIndex = 12;
            this.TxtChequeto.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtChequeto_KeyDown);
            // 
            // CmbAcType
            // 
            this.CmbAcType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CmbAcType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbAcType.Font = new System.Drawing.Font("Century Gothic", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CmbAcType.FormattingEnabled = true;
            this.CmbAcType.Items.AddRange(new object[] {
            "Current",
            "Savings",
            "Fixed Deposit",
            "N.T.L",
            "OverDraft",
            "Cash Credit",
            "Vehicle Loan"});
            this.CmbAcType.Location = new System.Drawing.Point(138, 143);
            this.CmbAcType.Name = "CmbAcType";
            this.CmbAcType.Size = new System.Drawing.Size(197, 29);
            this.CmbAcType.TabIndex = 8;
            this.CmbAcType.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CmbType_KeyDown);
            // 
            // TxtEmail
            // 
            this.TxtEmail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtEmail.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower;
            this.TxtEmail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TxtEmail.Font = new System.Drawing.Font("Century Gothic", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtEmail.Location = new System.Drawing.Point(476, 143);
            this.TxtEmail.Name = "TxtEmail";
            this.TxtEmail.Size = new System.Drawing.Size(200, 30);
            this.TxtEmail.TabIndex = 9;
            this.TxtEmail.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxtEmail.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtEmail_KeyDown);
            // 
            // TxtCreditlimit
            // 
            this.TxtCreditlimit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtCreditlimit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TxtCreditlimit.Font = new System.Drawing.Font("Century Gothic", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtCreditlimit.Location = new System.Drawing.Point(138, 178);
            this.TxtCreditlimit.Name = "TxtCreditlimit";
            this.TxtCreditlimit.Size = new System.Drawing.Size(197, 30);
            this.TxtCreditlimit.TabIndex = 10;
            this.TxtCreditlimit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxtCreditlimit.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtCreditlimit_KeyDown);
            this.TxtCreditlimit.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtCreditlimit_KeyPress);
            // 
            // TxtPINCode
            // 
            this.TxtPINCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtPINCode.Font = new System.Drawing.Font("Century Gothic", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtPINCode.Location = new System.Drawing.Point(476, 73);
            this.TxtPINCode.Name = "TxtPINCode";
            this.TxtPINCode.Size = new System.Drawing.Size(200, 30);
            this.TxtPINCode.TabIndex = 39;
            this.TxtPINCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtPINCode_KeyDown);
            this.TxtPINCode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtPINCode_KeyPress);
            // 
            // TxtBankabbrivation
            // 
            this.TxtBankabbrivation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtBankabbrivation.Location = new System.Drawing.Point(476, 213);
            this.TxtBankabbrivation.Name = "TxtBankabbrivation";
            this.TxtBankabbrivation.Size = new System.Drawing.Size(197, 30);
            this.TxtBankabbrivation.TabIndex = 14;
            this.TxtBankabbrivation.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxtBankabbrivation.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtBankabbrivation_KeyDown);
            // 
            // CmbStatus
            // 
            this.CmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbStatus.Font = new System.Drawing.Font("Century Gothic", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CmbStatus.FormattingEnabled = true;
            this.CmbStatus.Items.AddRange(new object[] {
            "Active",
            "Inactive"});
            this.CmbStatus.Location = new System.Drawing.Point(138, 248);
            this.CmbStatus.Name = "CmbStatus";
            this.CmbStatus.Size = new System.Drawing.Size(197, 29);
            this.CmbStatus.TabIndex = 26;
            this.CmbStatus.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CmbStatus_KeyDown);
            // 
            // TxtPhone
            // 
            this.TxtPhone.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtPhone.Location = new System.Drawing.Point(138, 108);
            this.TxtPhone.Name = "TxtPhone";
            this.TxtPhone.Size = new System.Drawing.Size(197, 30);
            this.TxtPhone.TabIndex = 19;
            this.TxtPhone.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxtPhone.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtPhone_KeyDown);
            // 
            // TxtIFC
            // 
            this.TxtIFC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtIFC.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.TxtIFC.Location = new System.Drawing.Point(476, 108);
            this.TxtIFC.Name = "TxtIFC";
            this.TxtIFC.Size = new System.Drawing.Size(200, 30);
            this.TxtIFC.TabIndex = 23;
            this.TxtIFC.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxtIFC.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtIFC_KeyDown);
            // 
            // LblClose
            // 
            this.LblClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LblClose.AutoSize = true;
            this.LblClose.BackColor = System.Drawing.Color.DarkGray;
            this.LblClose.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblClose.Location = new System.Drawing.Point(685, 7);
            this.LblClose.Name = "LblClose";
            this.LblClose.Size = new System.Drawing.Size(21, 23);
            this.LblClose.TabIndex = 31;
            this.LblClose.Text = "X";
            this.LblClose.Click += new System.EventHandler(this.LblClose_Click);
            // 
            // FrmAddEditBank
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(112)))), ((int)(((byte)(173)))));
            this.ClientSize = new System.Drawing.Size(719, 544);
            this.Controls.Add(this.LblClose);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panelHeader);
            this.Font = new System.Drawing.Font("Century Gothic", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmAddEditBank";
            this.Padding = new System.Windows.Forms.Padding(10, 40, 10, 10);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FrmAddEditBank";
            this.Load += new System.EventHandler(this.FrmAddEditBank_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmAddEditBank_KeyDown);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button BtnSave;
        private System.Windows.Forms.Button BtnClose;
        private System.Windows.Forms.Button BtnClear;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label LblHeader;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox TxtBankName;
        private System.Windows.Forms.TextBox TxtAddress;
        private System.Windows.Forms.TextBox TxtAcNo;
        private System.Windows.Forms.TextBox TxtChequefrom;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox TxtBranch;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TxtChequeto;
        private System.Windows.Forms.ComboBox CmbAcType;
        private System.Windows.Forms.TextBox TxtEmail;
        private System.Windows.Forms.TextBox TxtCreditlimit;
        private System.Windows.Forms.ComboBox CmbStatus;
        private System.Windows.Forms.TextBox TxtPhone;
        private System.Windows.Forms.TextBox TxtIFC;
        private System.Windows.Forms.TextBox TxtBankabbrivation;
        private System.Windows.Forms.Label LblClose;
        private System.Windows.Forms.TextBox TxtPINCode;
    }
}