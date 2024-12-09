namespace DESKTOPNEDBILL.Forms.Sales
{
    partial class FrmAddEditCustomer
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
            this.panelHeader = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.LblHeader = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.BtnSave = new System.Windows.Forms.Button();
            this.BtnClose = new System.Windows.Forms.Button();
            this.BtnClear = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.TxtCreditLimit = new System.Windows.Forms.TextBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.CmbState = new System.Windows.Forms.ComboBox();
            this.CmbStatus = new System.Windows.Forms.ComboBox();
            this.CmbPriceGroup = new System.Windows.Forms.ComboBox();
            this.CmbLocation = new System.Windows.Forms.ComboBox();
            this.TxtOpening = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.TxtCustomerName = new System.Windows.Forms.TextBox();
            this.TxtAddress1 = new System.Windows.Forms.TextBox();
            this.TxtAddress2 = new System.Windows.Forms.TextBox();
            this.TxtGSTNo = new System.Windows.Forms.TextBox();
            this.CmbPaymenttype = new System.Windows.Forms.ComboBox();
            this.label16 = new System.Windows.Forms.Label();
            this.TxtContactNo = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.TxtEmailID = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.TxtPaymentTerms = new System.Windows.Forms.TextBox();
            this.CmbTaxtype = new System.Windows.Forms.ComboBox();
            this.LblClose = new System.Windows.Forms.Label();
            this.panelHeader.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel5.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.SuspendLayout();
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
            this.panelHeader.Size = new System.Drawing.Size(884, 52);
            this.panelHeader.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(639, 26);
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
            this.LblHeader.Size = new System.Drawing.Size(201, 23);
            this.LblHeader.TabIndex = 0;
            this.LblHeader.Text = "Add New Customer";
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
            this.panel1.Size = new System.Drawing.Size(884, 86);
            this.panel1.TabIndex = 1;
            // 
            // BtnSave
            // 
            this.BtnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(112)))), ((int)(((byte)(173)))));
            this.BtnSave.FlatAppearance.BorderSize = 0;
            this.BtnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnSave.Font = new System.Drawing.Font("Century Gothic", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSave.ForeColor = System.Drawing.Color.White;
            this.BtnSave.Location = new System.Drawing.Point(600, 21);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(133, 43);
            this.BtnSave.TabIndex = 16;
            this.BtnSave.Text = "Save [F1]";
            this.BtnSave.UseVisualStyleBackColor = false;
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // BtnClose
            // 
            this.BtnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(112)))), ((int)(((byte)(173)))));
            this.BtnClose.FlatAppearance.BorderSize = 0;
            this.BtnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnClose.Font = new System.Drawing.Font("Century Gothic", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnClose.ForeColor = System.Drawing.Color.White;
            this.BtnClose.Location = new System.Drawing.Point(741, 21);
            this.BtnClose.Name = "BtnClose";
            this.BtnClose.Size = new System.Drawing.Size(133, 43);
            this.BtnClose.TabIndex = 17;
            this.BtnClose.Text = "Close [Esc]";
            this.BtnClose.UseVisualStyleBackColor = false;
            this.BtnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // BtnClear
            // 
            this.BtnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnClear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(112)))), ((int)(((byte)(173)))));
            this.BtnClear.FlatAppearance.BorderSize = 0;
            this.BtnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnClear.Font = new System.Drawing.Font("Century Gothic", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnClear.ForeColor = System.Drawing.Color.White;
            this.BtnClear.Location = new System.Drawing.Point(450, 21);
            this.BtnClear.Name = "BtnClear";
            this.BtnClear.Size = new System.Drawing.Size(142, 43);
            this.BtnClear.TabIndex = 18;
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
            this.panel2.Size = new System.Drawing.Size(884, 27);
            this.panel2.TabIndex = 2;
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
            this.tableLayoutPanel1.Size = new System.Drawing.Size(864, 27);
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
            this.label3.Size = new System.Drawing.Size(158, 21);
            this.label3.TabIndex = 0;
            this.label3.Text = "Customer Details";
            // 
            // label11
            // 
            this.label11.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(3, 217);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(166, 21);
            this.label11.TabIndex = 6;
            this.label11.Text = "Pay in Terms [Days]";
            // 
            // label12
            // 
            this.label12.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(434, 217);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(108, 21);
            this.label12.TabIndex = 7;
            this.label12.Text = "Price Group";
            // 
            // TxtCreditLimit
            // 
            this.TxtCreditLimit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtCreditLimit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TxtCreditLimit.Font = new System.Drawing.Font("Century Gothic", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtCreditLimit.Location = new System.Drawing.Point(606, 108);
            this.TxtCreditLimit.Name = "TxtCreditLimit";
            this.TxtCreditLimit.Size = new System.Drawing.Size(255, 30);
            this.TxtCreditLimit.TabIndex = 7;
            this.TxtCreditLimit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxtCreditLimit.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtCreditLimit_KeyDown);
            this.TxtCreditLimit.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtCreditLimit_KeyPress);
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.SystemColors.Control;
            this.panel5.Controls.Add(this.tableLayoutPanel4);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(10, 119);
            this.panel5.Name = "panel5";
            this.panel5.Padding = new System.Windows.Forms.Padding(10);
            this.panel5.Size = new System.Drawing.Size(884, 302);
            this.panel5.TabIndex = 5;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 4;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel4.Controls.Add(this.CmbState, 3, 7);
            this.tableLayoutPanel4.Controls.Add(this.CmbStatus, 1, 7);
            this.tableLayoutPanel4.Controls.Add(this.CmbPriceGroup, 3, 6);
            this.tableLayoutPanel4.Controls.Add(this.CmbLocation, 1, 3);
            this.tableLayoutPanel4.Controls.Add(this.label12, 2, 6);
            this.tableLayoutPanel4.Controls.Add(this.TxtCreditLimit, 3, 3);
            this.tableLayoutPanel4.Controls.Add(this.TxtOpening, 1, 4);
            this.tableLayoutPanel4.Controls.Add(this.label5, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.label7, 0, 7);
            this.tableLayoutPanel4.Controls.Add(this.label11, 0, 6);
            this.tableLayoutPanel4.Controls.Add(this.label6, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.label10, 0, 5);
            this.tableLayoutPanel4.Controls.Add(this.label15, 2, 3);
            this.tableLayoutPanel4.Controls.Add(this.label14, 2, 1);
            this.tableLayoutPanel4.Controls.Add(this.label17, 2, 4);
            this.tableLayoutPanel4.Controls.Add(this.label18, 2, 5);
            this.tableLayoutPanel4.Controls.Add(this.TxtCustomerName, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.TxtAddress1, 1, 1);
            this.tableLayoutPanel4.Controls.Add(this.TxtAddress2, 3, 1);
            this.tableLayoutPanel4.Controls.Add(this.TxtGSTNo, 3, 5);
            this.tableLayoutPanel4.Controls.Add(this.CmbPaymenttype, 1, 5);
            this.tableLayoutPanel4.Controls.Add(this.label16, 0, 2);
            this.tableLayoutPanel4.Controls.Add(this.TxtContactNo, 1, 2);
            this.tableLayoutPanel4.Controls.Add(this.label9, 2, 2);
            this.tableLayoutPanel4.Controls.Add(this.TxtEmailID, 3, 2);
            this.tableLayoutPanel4.Controls.Add(this.label8, 0, 4);
            this.tableLayoutPanel4.Controls.Add(this.label1, 0, 3);
            this.tableLayoutPanel4.Controls.Add(this.label4, 2, 7);
            this.tableLayoutPanel4.Controls.Add(this.TxtPaymentTerms, 1, 6);
            this.tableLayoutPanel4.Controls.Add(this.CmbTaxtype, 3, 4);
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
            this.tableLayoutPanel4.Size = new System.Drawing.Size(864, 282);
            this.tableLayoutPanel4.TabIndex = 0;
            // 
            // CmbState
            // 
            this.CmbState.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CmbState.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbState.Font = new System.Drawing.Font("Century Gothic", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CmbState.FormattingEnabled = true;
            this.CmbState.Items.AddRange(new object[] {
            "1"});
            this.CmbState.Location = new System.Drawing.Point(606, 248);
            this.CmbState.Name = "CmbState";
            this.CmbState.Size = new System.Drawing.Size(255, 29);
            this.CmbState.TabIndex = 15;
            this.CmbState.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CmbState_KeyDown);
            // 
            // CmbStatus
            // 
            this.CmbStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbStatus.Font = new System.Drawing.Font("Century Gothic", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CmbStatus.FormattingEnabled = true;
            this.CmbStatus.Items.AddRange(new object[] {
            "Active",
            "InActive"});
            this.CmbStatus.Location = new System.Drawing.Point(175, 248);
            this.CmbStatus.Name = "CmbStatus";
            this.CmbStatus.Size = new System.Drawing.Size(253, 29);
            this.CmbStatus.TabIndex = 14;
            this.CmbStatus.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CmbStatus_KeyDown);
            // 
            // CmbPriceGroup
            // 
            this.CmbPriceGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CmbPriceGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbPriceGroup.Font = new System.Drawing.Font("Century Gothic", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CmbPriceGroup.FormattingEnabled = true;
            this.CmbPriceGroup.Items.AddRange(new object[] {
            "Selling Price A",
            "Selling Price B",
            "Selling Price C"});
            this.CmbPriceGroup.Location = new System.Drawing.Point(606, 213);
            this.CmbPriceGroup.Name = "CmbPriceGroup";
            this.CmbPriceGroup.Size = new System.Drawing.Size(255, 29);
            this.CmbPriceGroup.TabIndex = 13;
            this.CmbPriceGroup.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CmbPriceGroup_KeyDown);
            // 
            // CmbLocation
            // 
            this.CmbLocation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CmbLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbLocation.Font = new System.Drawing.Font("Century Gothic", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CmbLocation.FormattingEnabled = true;
            this.CmbLocation.Items.AddRange(new object[] {
            "Local",
            "Interstate"});
            this.CmbLocation.Location = new System.Drawing.Point(175, 108);
            this.CmbLocation.Name = "CmbLocation";
            this.CmbLocation.Size = new System.Drawing.Size(253, 29);
            this.CmbLocation.TabIndex = 6;
            this.CmbLocation.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CmbLocation_KeyDown);
            // 
            // TxtOpening
            // 
            this.TxtOpening.BackColor = System.Drawing.SystemColors.Window;
            this.TxtOpening.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtOpening.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TxtOpening.Font = new System.Drawing.Font("Century Gothic", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtOpening.Location = new System.Drawing.Point(175, 143);
            this.TxtOpening.Name = "TxtOpening";
            this.TxtOpening.Size = new System.Drawing.Size(253, 30);
            this.TxtOpening.TabIndex = 8;
            this.TxtOpening.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxtOpening.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtOpening_KeyDown);
            this.TxtOpening.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtOpening_KeyPress);
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(3, 7);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 21);
            this.label5.TabIndex = 0;
            this.label5.Text = "Name*";
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
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(3, 42);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(91, 21);
            this.label6.TabIndex = 1;
            this.label6.Text = "Address 1";
            // 
            // label10
            // 
            this.label10.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(3, 182);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(127, 21);
            this.label10.TabIndex = 5;
            this.label10.Text = "Payment Type";
            // 
            // label15
            // 
            this.label15.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(434, 112);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(103, 21);
            this.label15.TabIndex = 10;
            this.label15.Text = "Credit Limit";
            // 
            // label14
            // 
            this.label14.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(434, 42);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(91, 21);
            this.label14.TabIndex = 9;
            this.label14.Text = "Address 2";
            // 
            // label17
            // 
            this.label17.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(434, 147);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(120, 21);
            this.label17.TabIndex = 12;
            this.label17.Text = "Tax/GST Type";
            // 
            // label18
            // 
            this.label18.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(434, 182);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(110, 21);
            this.label18.TabIndex = 13;
            this.label18.Text = "Tax/GST No.";
            // 
            // TxtCustomerName
            // 
            this.TxtCustomerName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tableLayoutPanel4.SetColumnSpan(this.TxtCustomerName, 3);
            this.TxtCustomerName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TxtCustomerName.Font = new System.Drawing.Font("Century Gothic", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtCustomerName.Location = new System.Drawing.Point(175, 3);
            this.TxtCustomerName.Name = "TxtCustomerName";
            this.TxtCustomerName.Size = new System.Drawing.Size(686, 30);
            this.TxtCustomerName.TabIndex = 1;
            this.TxtCustomerName.TabIndexChanged += new System.EventHandler(this.TxtCustomerName_TabIndexChanged);
            this.TxtCustomerName.TextChanged += new System.EventHandler(this.TxtCustomerName_TextChanged);
            this.TxtCustomerName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtCustomerName_KeyDown);
            // 
            // TxtAddress1
            // 
            this.TxtAddress1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtAddress1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TxtAddress1.Font = new System.Drawing.Font("Century Gothic", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtAddress1.Location = new System.Drawing.Point(175, 38);
            this.TxtAddress1.Name = "TxtAddress1";
            this.TxtAddress1.Size = new System.Drawing.Size(253, 30);
            this.TxtAddress1.TabIndex = 2;
            this.TxtAddress1.TextChanged += new System.EventHandler(this.TxtAddress1_TextChanged);
            this.TxtAddress1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtAddress1_KeyDown);
            // 
            // TxtAddress2
            // 
            this.TxtAddress2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtAddress2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TxtAddress2.Font = new System.Drawing.Font("Century Gothic", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtAddress2.Location = new System.Drawing.Point(606, 38);
            this.TxtAddress2.Name = "TxtAddress2";
            this.TxtAddress2.Size = new System.Drawing.Size(255, 30);
            this.TxtAddress2.TabIndex = 3;
            this.TxtAddress2.TextChanged += new System.EventHandler(this.TxtAddress2_TextChanged);
            this.TxtAddress2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtAddress2_KeyDown);
            // 
            // TxtGSTNo
            // 
            this.TxtGSTNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtGSTNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.TxtGSTNo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TxtGSTNo.Font = new System.Drawing.Font("Century Gothic", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtGSTNo.Location = new System.Drawing.Point(606, 178);
            this.TxtGSTNo.Name = "TxtGSTNo";
            this.TxtGSTNo.Size = new System.Drawing.Size(255, 30);
            this.TxtGSTNo.TabIndex = 11;
            this.TxtGSTNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtGSTNo_KeyDown);
            // 
            // CmbPaymenttype
            // 
            this.CmbPaymenttype.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CmbPaymenttype.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbPaymenttype.Font = new System.Drawing.Font("Century Gothic", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CmbPaymenttype.FormattingEnabled = true;
            this.CmbPaymenttype.Items.AddRange(new object[] {
            "Cash",
            "Credit"});
            this.CmbPaymenttype.Location = new System.Drawing.Point(175, 178);
            this.CmbPaymenttype.Name = "CmbPaymenttype";
            this.CmbPaymenttype.Size = new System.Drawing.Size(253, 29);
            this.CmbPaymenttype.TabIndex = 10;
            this.CmbPaymenttype.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CmbPaymenttype_KeyDown);
            // 
            // label16
            // 
            this.label16.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(3, 77);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(116, 21);
            this.label16.TabIndex = 11;
            this.label16.Text = "Contact No.";
            // 
            // TxtContactNo
            // 
            this.TxtContactNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtContactNo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TxtContactNo.Font = new System.Drawing.Font("Century Gothic", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtContactNo.Location = new System.Drawing.Point(175, 73);
            this.TxtContactNo.MaxLength = 10;
            this.TxtContactNo.Name = "TxtContactNo";
            this.TxtContactNo.Size = new System.Drawing.Size(253, 30);
            this.TxtContactNo.TabIndex = 4;
            this.TxtContactNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtContactNo_KeyDown);
            this.TxtContactNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtContactNo_KeyPress);
            // 
            // label9
            // 
            this.label9.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(434, 77);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(52, 21);
            this.label9.TabIndex = 4;
            this.label9.Text = "Email";
            // 
            // TxtEmailID
            // 
            this.TxtEmailID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtEmailID.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TxtEmailID.Font = new System.Drawing.Font("Century Gothic", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtEmailID.Location = new System.Drawing.Point(606, 73);
            this.TxtEmailID.Name = "TxtEmailID";
            this.TxtEmailID.Size = new System.Drawing.Size(255, 30);
            this.TxtEmailID.TabIndex = 5;
            this.TxtEmailID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtEmailID_KeyDown);
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(3, 147);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(109, 21);
            this.label8.TabIndex = 3;
            this.label8.Text = "OP Balance";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 112);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 21);
            this.label1.TabIndex = 25;
            this.label1.Text = "Location";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(434, 253);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(114, 21);
            this.label4.TabIndex = 26;
            this.label4.Text = "State Code*";
            // 
            // TxtPaymentTerms
            // 
            this.TxtPaymentTerms.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtPaymentTerms.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TxtPaymentTerms.Font = new System.Drawing.Font("Century Gothic", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtPaymentTerms.Location = new System.Drawing.Point(175, 213);
            this.TxtPaymentTerms.Name = "TxtPaymentTerms";
            this.TxtPaymentTerms.Size = new System.Drawing.Size(253, 30);
            this.TxtPaymentTerms.TabIndex = 12;
            this.TxtPaymentTerms.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxtPaymentTerms.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtPaymentTerms_KeyDown);
            this.TxtPaymentTerms.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtPaymentTerms_KeyPress);
            // 
            // CmbTaxtype
            // 
            this.CmbTaxtype.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CmbTaxtype.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbTaxtype.Font = new System.Drawing.Font("Century Gothic", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CmbTaxtype.FormattingEnabled = true;
            this.CmbTaxtype.Items.AddRange(new object[] {
            "UnRegistered",
            "Registered"});
            this.CmbTaxtype.Location = new System.Drawing.Point(606, 143);
            this.CmbTaxtype.Name = "CmbTaxtype";
            this.CmbTaxtype.Size = new System.Drawing.Size(255, 29);
            this.CmbTaxtype.TabIndex = 9;
            this.CmbTaxtype.SelectionChangeCommitted += new System.EventHandler(this.CmbTaxtype_SelectionChangeCommitted);
            this.CmbTaxtype.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CmbTaxtype_KeyDown);
            // 
            // LblClose
            // 
            this.LblClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LblClose.AutoSize = true;
            this.LblClose.BackColor = System.Drawing.Color.DarkGray;
            this.LblClose.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblClose.Location = new System.Drawing.Point(870, 10);
            this.LblClose.Name = "LblClose";
            this.LblClose.Size = new System.Drawing.Size(21, 23);
            this.LblClose.TabIndex = 6;
            this.LblClose.Text = "X";
            this.LblClose.Click += new System.EventHandler(this.LblClose_Click);
            // 
            // FrmAddEditCustomer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(112)))), ((int)(((byte)(173)))));
            this.ClientSize = new System.Drawing.Size(904, 517);
            this.Controls.Add(this.LblClose);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelHeader);
            this.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "FrmAddEditCustomer";
            this.Padding = new System.Windows.Forms.Padding(10, 40, 10, 10);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "CompanyProfiles";
            this.Load += new System.EventHandler(this.FrmAddEditCustomer_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmAddEditCustomer_KeyDown);
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label LblHeader;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Button BtnClear;
        private System.Windows.Forms.Button BtnClose;
        private System.Windows.Forms.TextBox TxtCustomerName;
        private System.Windows.Forms.TextBox TxtAddress1;
        private System.Windows.Forms.TextBox TxtOpening;
        private System.Windows.Forms.TextBox TxtEmailID;
        private System.Windows.Forms.TextBox TxtAddress2;
        private System.Windows.Forms.TextBox TxtCreditLimit;
        private System.Windows.Forms.TextBox TxtContactNo;
        private System.Windows.Forms.TextBox TxtPaymentTerms;
        private System.Windows.Forms.TextBox TxtGSTNo;
        private System.Windows.Forms.ComboBox CmbTaxtype;
        private System.Windows.Forms.ComboBox CmbPaymenttype;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox CmbLocation;
        private System.Windows.Forms.Button BtnSave;
        private System.Windows.Forms.ComboBox CmbState;
        private System.Windows.Forms.ComboBox CmbStatus;
        private System.Windows.Forms.ComboBox CmbPriceGroup;
        private System.Windows.Forms.Label LblClose;
    }
}