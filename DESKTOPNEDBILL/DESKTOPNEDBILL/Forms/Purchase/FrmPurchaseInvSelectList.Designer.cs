namespace DESKTOPNEDBILL.Forms.Purchase
{
    partial class FrmPurchaseInvSelectList
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.LblClose = new System.Windows.Forms.Label();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.TxtPurInvRef = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.GrdPurchaseInvoiceDetails = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PurchaseInvNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StkTranId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label3 = new System.Windows.Forms.Label();
            this.panelHeader.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GrdPurchaseInvoiceDetails)).BeginInit();
            this.SuspendLayout();
            // 
            // LblClose
            // 
            this.LblClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LblClose.AutoSize = true;
            this.LblClose.BackColor = System.Drawing.Color.DarkGray;
            this.LblClose.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblClose.Location = new System.Drawing.Point(767, 9);
            this.LblClose.Name = "LblClose";
            this.LblClose.Size = new System.Drawing.Size(21, 23);
            this.LblClose.TabIndex = 9;
            this.LblClose.Text = "X";
            this.LblClose.Click += new System.EventHandler(this.LblClose_Click);
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.SystemColors.Control;
            this.panelHeader.Controls.Add(this.TxtPurInvRef);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(10, 40);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Padding = new System.Windows.Forms.Padding(10);
            this.panelHeader.Size = new System.Drawing.Size(780, 46);
            this.panelHeader.TabIndex = 10;
            // 
            // TxtPurInvRef
            // 
            this.TxtPurInvRef.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtPurInvRef.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TxtPurInvRef.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtPurInvRef.Location = new System.Drawing.Point(10, 10);
            this.TxtPurInvRef.Name = "TxtPurInvRef";
            this.TxtPurInvRef.Size = new System.Drawing.Size(760, 28);
            this.TxtPurInvRef.TabIndex = 0;
            this.TxtPurInvRef.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtPurInvRef_KeyDown);
            this.TxtPurInvRef.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtPurInvRef_KeyPress);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.GrdPurchaseInvoiceDetails);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(10, 86);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(10);
            this.panel1.Size = new System.Drawing.Size(780, 339);
            this.panel1.TabIndex = 11;
            // 
            // GrdPurchaseInvoiceDetails
            // 
            this.GrdPurchaseInvoiceDetails.AllowUserToAddRows = false;
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
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(112)))), ((int)(((byte)(173)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GrdPurchaseInvoiceDetails.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.GrdPurchaseInvoiceDetails.ColumnHeadersHeight = 35;
            this.GrdPurchaseInvoiceDetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column5,
            this.PurchaseInvNo,
            this.StkTranId,
            this.Column4,
            this.Column7});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.GrdPurchaseInvoiceDetails.DefaultCellStyle = dataGridViewCellStyle4;
            this.GrdPurchaseInvoiceDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GrdPurchaseInvoiceDetails.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.GrdPurchaseInvoiceDetails.EnableHeadersVisualStyles = false;
            this.GrdPurchaseInvoiceDetails.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.GrdPurchaseInvoiceDetails.Location = new System.Drawing.Point(10, 10);
            this.GrdPurchaseInvoiceDetails.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.GrdPurchaseInvoiceDetails.Name = "GrdPurchaseInvoiceDetails";
            this.GrdPurchaseInvoiceDetails.ReadOnly = true;
            this.GrdPurchaseInvoiceDetails.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.GrdPurchaseInvoiceDetails.RowHeadersVisible = false;
            this.GrdPurchaseInvoiceDetails.RowHeadersWidth = 70;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GrdPurchaseInvoiceDetails.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.GrdPurchaseInvoiceDetails.RowTemplate.Height = 30;
            this.GrdPurchaseInvoiceDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.GrdPurchaseInvoiceDetails.Size = new System.Drawing.Size(760, 319);
            this.GrdPurchaseInvoiceDetails.TabIndex = 5;
            this.GrdPurchaseInvoiceDetails.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GrdPurchaseInvoiceDetails_CellClick);
            this.GrdPurchaseInvoiceDetails.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GrdPurchaseInvoiceDetails_KeyDown);
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "InvDate";
            this.Column1.HeaderText = "Bill Date";
            this.Column1.MinimumWidth = 6;
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column5
            // 
            this.Column5.DataPropertyName = "VendorName";
            this.Column5.HeaderText = "Vendor";
            this.Column5.MinimumWidth = 6;
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            // 
            // PurchaseInvNo
            // 
            this.PurchaseInvNo.DataPropertyName = "PurInvNo";
            this.PurchaseInvNo.HeaderText = "InvNo";
            this.PurchaseInvNo.MinimumWidth = 6;
            this.PurchaseInvNo.Name = "PurchaseInvNo";
            this.PurchaseInvNo.ReadOnly = true;
            this.PurchaseInvNo.Visible = false;
            // 
            // StkTranId
            // 
            this.StkTranId.DataPropertyName = "TranId";
            this.StkTranId.HeaderText = "StkTranId";
            this.StkTranId.MinimumWidth = 6;
            this.StkTranId.Name = "StkTranId";
            this.StkTranId.ReadOnly = true;
            this.StkTranId.Visible = false;
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "InvRef";
            this.Column4.HeaderText = "Bill No";
            this.Column4.MinimumWidth = 6;
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // Column7
            // 
            this.Column7.DataPropertyName = "InvAmount";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.Format = "N2";
            this.Column7.DefaultCellStyle = dataGridViewCellStyle3;
            this.Column7.HeaderText = "Bill Amount";
            this.Column7.MinimumWidth = 6;
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Century Gothic", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(16, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(222, 21);
            this.label3.TabIndex = 12;
            this.label3.Text = "Select Purchase Invoice";
            // 
            // FrmPurchaseInvSelectList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(112)))), ((int)(((byte)(173)))));
            this.ClientSize = new System.Drawing.Size(800, 437);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelHeader);
            this.Controls.Add(this.LblClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "FrmPurchaseInvSelectList";
            this.Padding = new System.Windows.Forms.Padding(10, 40, 10, 10);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FrmPurchaseInvSelectList";
            this.Load += new System.EventHandler(this.FrmPurchaseInvSelectList_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmPurchaseInvSelectList_KeyDown);
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GrdPurchaseInvoiceDetails)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LblClose;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.TextBox TxtPurInvRef;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView GrdPurchaseInvoiceDetails;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn PurchaseInvNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn StkTranId;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.Label label3;
    }
}