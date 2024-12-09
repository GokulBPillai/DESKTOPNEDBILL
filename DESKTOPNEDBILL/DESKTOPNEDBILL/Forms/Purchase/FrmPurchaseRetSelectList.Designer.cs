namespace DESKTOPNEDBILL.Forms.Purchase
{
    partial class FrmPurchaseRetSelectList
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel2 = new System.Windows.Forms.Panel();
            this.GrdPurchaseInvoiceDetails = new System.Windows.Forms.DataGridView();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.purretno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CustomerId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PurInvNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StkTranId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.TxtPurRetRef = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.LblClose = new System.Windows.Forms.Label();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GrdPurchaseInvoiceDetails)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.Control;
            this.panel2.Controls.Add(this.GrdPurchaseInvoiceDetails);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Font = new System.Drawing.Font("Century Gothic", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel2.Location = new System.Drawing.Point(10, 90);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(15);
            this.panel2.Size = new System.Drawing.Size(780, 321);
            this.panel2.TabIndex = 9;
            // 
            // GrdPurchaseInvoiceDetails
            // 
            this.GrdPurchaseInvoiceDetails.AllowUserToAddRows = false;
            this.GrdPurchaseInvoiceDetails.AllowUserToResizeColumns = false;
            this.GrdPurchaseInvoiceDetails.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            this.GrdPurchaseInvoiceDetails.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.GrdPurchaseInvoiceDetails.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.GrdPurchaseInvoiceDetails.BackgroundColor = System.Drawing.Color.White;
            this.GrdPurchaseInvoiceDetails.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.GrdPurchaseInvoiceDetails.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(112)))), ((int)(((byte)(173)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Century Gothic", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(112)))), ((int)(((byte)(173)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GrdPurchaseInvoiceDetails.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.GrdPurchaseInvoiceDetails.ColumnHeadersHeight = 35;
            this.GrdPurchaseInvoiceDetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column4,
            this.purretno,
            this.CustomerId,
            this.PurInvNo,
            this.StkTranId,
            this.Column2,
            this.Column3});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Century Gothic", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.GrdPurchaseInvoiceDetails.DefaultCellStyle = dataGridViewCellStyle5;
            this.GrdPurchaseInvoiceDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GrdPurchaseInvoiceDetails.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.GrdPurchaseInvoiceDetails.EnableHeadersVisualStyles = false;
            this.GrdPurchaseInvoiceDetails.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.GrdPurchaseInvoiceDetails.Location = new System.Drawing.Point(15, 15);
            this.GrdPurchaseInvoiceDetails.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.GrdPurchaseInvoiceDetails.Name = "GrdPurchaseInvoiceDetails";
            this.GrdPurchaseInvoiceDetails.ReadOnly = true;
            this.GrdPurchaseInvoiceDetails.RowHeadersVisible = false;
            this.GrdPurchaseInvoiceDetails.RowHeadersWidth = 70;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GrdPurchaseInvoiceDetails.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.GrdPurchaseInvoiceDetails.RowTemplate.Height = 30;
            this.GrdPurchaseInvoiceDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.GrdPurchaseInvoiceDetails.Size = new System.Drawing.Size(750, 291);
            this.GrdPurchaseInvoiceDetails.TabIndex = 1;
            this.GrdPurchaseInvoiceDetails.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GrdPurchaseInvoiceDetails_CellClick);
            this.GrdPurchaseInvoiceDetails.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GrdPurchaseInvoiceDetails_KeyDown);
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "RetDate";
            this.Column4.HeaderText = "Dr Note Dt.";
            this.Column4.MinimumWidth = 6;
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // purretno
            // 
            this.purretno.DataPropertyName = "PurRetNo";
            this.purretno.HeaderText = "Dr Note No.";
            this.purretno.MinimumWidth = 6;
            this.purretno.Name = "purretno";
            this.purretno.ReadOnly = true;
            // 
            // CustomerId
            // 
            this.CustomerId.DataPropertyName = "InvDate";
            dataGridViewCellStyle3.Format = "d";
            dataGridViewCellStyle3.NullValue = null;
            this.CustomerId.DefaultCellStyle = dataGridViewCellStyle3;
            this.CustomerId.HeaderText = "Bill Date";
            this.CustomerId.MinimumWidth = 6;
            this.CustomerId.Name = "CustomerId";
            this.CustomerId.ReadOnly = true;
            // 
            // PurInvNo
            // 
            this.PurInvNo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.PurInvNo.DataPropertyName = "PurInvNo";
            this.PurInvNo.FillWeight = 6.067379F;
            this.PurInvNo.HeaderText = "Inv. No.";
            this.PurInvNo.MinimumWidth = 6;
            this.PurInvNo.Name = "PurInvNo";
            this.PurInvNo.ReadOnly = true;
            this.PurInvNo.Visible = false;
            // 
            // StkTranId
            // 
            this.StkTranId.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.StkTranId.DataPropertyName = "TranId";
            this.StkTranId.FillWeight = 6.067379F;
            this.StkTranId.HeaderText = "TranId";
            this.StkTranId.MinimumWidth = 6;
            this.StkTranId.Name = "StkTranId";
            this.StkTranId.ReadOnly = true;
            this.StkTranId.Visible = false;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "VendorName";
            this.Column2.HeaderText = "Vendor";
            this.Column2.MinimumWidth = 6;
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "InvAmount";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.Format = "N2";
            this.Column3.DefaultCellStyle = dataGridViewCellStyle4;
            this.Column3.HeaderText = "Dr Note Amt.";
            this.Column3.MinimumWidth = 6;
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.TxtPurRetRef);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(10, 40);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(10);
            this.panel1.Size = new System.Drawing.Size(780, 50);
            this.panel1.TabIndex = 8;
            // 
            // TxtPurRetRef
            // 
            this.TxtPurRetRef.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtPurRetRef.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TxtPurRetRef.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtPurRetRef.Location = new System.Drawing.Point(10, 10);
            this.TxtPurRetRef.Name = "TxtPurRetRef";
            this.TxtPurRetRef.Size = new System.Drawing.Size(760, 28);
            this.TxtPurRetRef.TabIndex = 0;
            this.TxtPurRetRef.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtPurRetRef_KeyDown);
            this.TxtPurRetRef.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtPurRetRef_KeyPress);
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(6, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(166, 21);
            this.label2.TabIndex = 12;
            this.label2.Text = "Select Debit Note";
            // 
            // LblClose
            // 
            this.LblClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LblClose.AutoSize = true;
            this.LblClose.BackColor = System.Drawing.Color.DarkGray;
            this.LblClose.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblClose.Location = new System.Drawing.Point(768, 10);
            this.LblClose.Name = "LblClose";
            this.LblClose.Size = new System.Drawing.Size(21, 23);
            this.LblClose.TabIndex = 11;
            this.LblClose.Text = "X";
            this.LblClose.Click += new System.EventHandler(this.LblClose_Click);
            // 
            // FrmPurchaseRetSelectList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(112)))), ((int)(((byte)(173)))));
            this.ClientSize = new System.Drawing.Size(800, 422);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.LblClose);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmPurchaseRetSelectList";
            this.Padding = new System.Windows.Forms.Padding(10, 40, 10, 10);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FrmPurchaseRetSelectList";
            this.Load += new System.EventHandler(this.FrmPurchaseRetSelectList_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmPurchaseRetSelectList_KeyDown);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GrdPurchaseInvoiceDetails)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView GrdPurchaseInvoiceDetails;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox TxtPurRetRef;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label LblClose;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn purretno;
        private System.Windows.Forms.DataGridViewTextBoxColumn CustomerId;
        private System.Windows.Forms.DataGridViewTextBoxColumn PurInvNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn StkTranId;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
    }
}