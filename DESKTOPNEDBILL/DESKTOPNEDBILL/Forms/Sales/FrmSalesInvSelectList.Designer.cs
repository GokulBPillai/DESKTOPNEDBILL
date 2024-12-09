namespace DESKTOPNEDBILL.Forms.Sales
{
    partial class FrmSalesInvSelectList
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
            this.GrdSalesInvoiceDetails = new System.Windows.Forms.DataGridView();
            this.CustomerId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SalesInvNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StkTranId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.TxtSalesInvRef = new System.Windows.Forms.TextBox();
            this.LblClose = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GrdSalesInvoiceDetails)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.Control;
            this.panel2.Controls.Add(this.GrdSalesInvoiceDetails);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Font = new System.Drawing.Font("Century Gothic", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel2.Location = new System.Drawing.Point(10, 90);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(15);
            this.panel2.Size = new System.Drawing.Size(780, 321);
            this.panel2.TabIndex = 5;
            // 
            // GrdSalesInvoiceDetails
            // 
            this.GrdSalesInvoiceDetails.AllowUserToAddRows = false;
            this.GrdSalesInvoiceDetails.AllowUserToResizeColumns = false;
            this.GrdSalesInvoiceDetails.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            this.GrdSalesInvoiceDetails.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.GrdSalesInvoiceDetails.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.GrdSalesInvoiceDetails.BackgroundColor = System.Drawing.Color.White;
            this.GrdSalesInvoiceDetails.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.GrdSalesInvoiceDetails.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(112)))), ((int)(((byte)(173)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Century Gothic", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(112)))), ((int)(((byte)(173)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GrdSalesInvoiceDetails.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.GrdSalesInvoiceDetails.ColumnHeadersHeight = 35;
            this.GrdSalesInvoiceDetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CustomerId,
            this.SalesInvNo,
            this.StkTranId,
            this.Column1,
            this.Column2,
            this.Column3});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Century Gothic", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.GrdSalesInvoiceDetails.DefaultCellStyle = dataGridViewCellStyle5;
            this.GrdSalesInvoiceDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GrdSalesInvoiceDetails.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.GrdSalesInvoiceDetails.EnableHeadersVisualStyles = false;
            this.GrdSalesInvoiceDetails.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.GrdSalesInvoiceDetails.Location = new System.Drawing.Point(15, 15);
            this.GrdSalesInvoiceDetails.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.GrdSalesInvoiceDetails.Name = "GrdSalesInvoiceDetails";
            this.GrdSalesInvoiceDetails.ReadOnly = true;
            this.GrdSalesInvoiceDetails.RowHeadersVisible = false;
            this.GrdSalesInvoiceDetails.RowHeadersWidth = 70;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GrdSalesInvoiceDetails.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.GrdSalesInvoiceDetails.RowTemplate.Height = 30;
            this.GrdSalesInvoiceDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.GrdSalesInvoiceDetails.Size = new System.Drawing.Size(750, 291);
            this.GrdSalesInvoiceDetails.TabIndex = 1;
            this.GrdSalesInvoiceDetails.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GrdSalesInvoiceDetails_CellClick);
            this.GrdSalesInvoiceDetails.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GrdSalesInvoiceDetails_KeyDown);
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
            // SalesInvNo
            // 
            this.SalesInvNo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.SalesInvNo.DataPropertyName = "SalesInvNo";
            this.SalesInvNo.FillWeight = 6.067379F;
            this.SalesInvNo.HeaderText = "Inv. No.";
            this.SalesInvNo.MinimumWidth = 6;
            this.SalesInvNo.Name = "SalesInvNo";
            this.SalesInvNo.ReadOnly = true;
            this.SalesInvNo.Visible = false;
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
            // Column1
            // 
            this.Column1.DataPropertyName = "SalesInvNo";
            this.Column1.HeaderText = "Bill No.";
            this.Column1.MinimumWidth = 6;
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "CustomerName";
            this.Column2.HeaderText = "Customer";
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
            this.Column3.HeaderText = "Bill Amount";
            this.Column3.MinimumWidth = 6;
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.TxtSalesInvRef);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(10, 40);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(10);
            this.panel1.Size = new System.Drawing.Size(780, 50);
            this.panel1.TabIndex = 4;
            // 
            // TxtSalesInvRef
            // 
            this.TxtSalesInvRef.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtSalesInvRef.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TxtSalesInvRef.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtSalesInvRef.Location = new System.Drawing.Point(10, 10);
            this.TxtSalesInvRef.Name = "TxtSalesInvRef";
            this.TxtSalesInvRef.Size = new System.Drawing.Size(760, 28);
            this.TxtSalesInvRef.TabIndex = 0;
            this.TxtSalesInvRef.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtSalesInvRef_KeyDown);
            this.TxtSalesInvRef.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtSalesInvRef_KeyPress);
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
            this.LblClose.TabIndex = 8;
            this.LblClose.Text = "X";
            this.LblClose.Click += new System.EventHandler(this.LblClose_Click);
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(18, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(136, 21);
            this.label2.TabIndex = 9;
            this.label2.Text = "Select Invoice";
            // 
            // FrmSalesInvSelectList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(112)))), ((int)(((byte)(173)))));
            this.ClientSize = new System.Drawing.Size(800, 422);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.LblClose);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Century Gothic", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmSalesInvSelectList";
            this.Padding = new System.Windows.Forms.Padding(10, 40, 10, 10);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FrmSalesInvSelectList";
            this.Load += new System.EventHandler(this.FrmSalesInvSelectList_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmSalesInvSelectList_KeyDown);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GrdSalesInvoiceDetails)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView GrdSalesInvoiceDetails;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox TxtSalesInvRef;
        private System.Windows.Forms.Label LblClose;
        private System.Windows.Forms.DataGridViewTextBoxColumn CustomerId;
        private System.Windows.Forms.DataGridViewTextBoxColumn SalesInvNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn StkTranId;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.Label label2;
    }
}