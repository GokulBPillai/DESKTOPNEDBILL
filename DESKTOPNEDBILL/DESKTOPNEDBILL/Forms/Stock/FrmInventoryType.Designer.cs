﻿namespace DESKTOPNEDBILL.Forms.Stock
{
    partial class FrmInventoryType
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelBody = new System.Windows.Forms.Panel();
            this.GrdInventoryType = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Edit = new System.Windows.Forms.DataGridViewImageColumn();
            this.panelFooter = new System.Windows.Forms.Panel();
            this.BtnClose = new System.Windows.Forms.Button();
            this.panelUpperBody = new System.Windows.Forms.Panel();
            this.BtnAddnew = new System.Windows.Forms.Button();
            this.panelHeaderLine = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridViewImageColumn1 = new System.Windows.Forms.DataGridViewImageColumn();
            this.panelBody.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GrdInventoryType)).BeginInit();
            this.panelFooter.SuspendLayout();
            this.panelUpperBody.SuspendLayout();
            this.panelHeaderLine.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panelHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelBody
            // 
            this.panelBody.Controls.Add(this.GrdInventoryType);
            this.panelBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelBody.Location = new System.Drawing.Point(0, 158);
            this.panelBody.Name = "panelBody";
            this.panelBody.Padding = new System.Windows.Forms.Padding(10);
            this.panelBody.Size = new System.Drawing.Size(1242, 495);
            this.panelBody.TabIndex = 15;
            // 
            // GrdInventoryType
            // 
            this.GrdInventoryType.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            this.GrdInventoryType.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.GrdInventoryType.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.GrdInventoryType.BackgroundColor = System.Drawing.Color.White;
            this.GrdInventoryType.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.GrdInventoryType.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(112)))), ((int)(((byte)(173)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(112)))), ((int)(((byte)(173)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GrdInventoryType.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.GrdInventoryType.ColumnHeadersHeight = 35;
            this.GrdInventoryType.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Edit});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.GrdInventoryType.DefaultCellStyle = dataGridViewCellStyle3;
            this.GrdInventoryType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GrdInventoryType.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.GrdInventoryType.EnableHeadersVisualStyles = false;
            this.GrdInventoryType.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.GrdInventoryType.Location = new System.Drawing.Point(10, 10);
            this.GrdInventoryType.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.GrdInventoryType.Name = "GrdInventoryType";
            this.GrdInventoryType.ReadOnly = true;
            this.GrdInventoryType.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.GrdInventoryType.RowHeadersVisible = false;
            this.GrdInventoryType.RowHeadersWidth = 70;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GrdInventoryType.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.GrdInventoryType.RowTemplate.Height = 30;
            this.GrdInventoryType.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.GrdInventoryType.Size = new System.Drawing.Size(1222, 475);
            this.GrdInventoryType.TabIndex = 0;
            this.GrdInventoryType.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GrdInventoryType_CellClick);
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "InventoryTypeId";
            this.Column1.HeaderText = "Stock TypeId";
            this.Column1.MinimumWidth = 6;
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Visible = false;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "TypeName";
            this.Column2.HeaderText = "Stock Type";
            this.Column2.MinimumWidth = 6;
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "IsStkDeduct";
            this.Column3.HeaderText = "Is Stock Deduct";
            this.Column3.MinimumWidth = 6;
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "Status";
            this.Column4.HeaderText = "Status";
            this.Column4.MinimumWidth = 6;
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // Edit
            // 
            this.Edit.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Edit.HeaderText = "Edit";
            this.Edit.Image = global::DESKTOPNEDBILL.Properties.Resources.icons8_edit_20__1_;
            this.Edit.MinimumWidth = 6;
            this.Edit.Name = "Edit";
            this.Edit.ReadOnly = true;
            this.Edit.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Edit.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Edit.Width = 68;
            // 
            // panelFooter
            // 
            this.panelFooter.Controls.Add(this.BtnClose);
            this.panelFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelFooter.Location = new System.Drawing.Point(0, 653);
            this.panelFooter.Name = "panelFooter";
            this.panelFooter.Size = new System.Drawing.Size(1242, 82);
            this.panelFooter.TabIndex = 14;
            // 
            // BtnClose
            // 
            this.BtnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(112)))), ((int)(((byte)(173)))));
            this.BtnClose.FlatAppearance.BorderSize = 0;
            this.BtnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnClose.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnClose.ForeColor = System.Drawing.Color.White;
            this.BtnClose.Location = new System.Drawing.Point(1085, 18);
            this.BtnClose.Name = "BtnClose";
            this.BtnClose.Size = new System.Drawing.Size(145, 47);
            this.BtnClose.TabIndex = 1;
            this.BtnClose.Text = "Close [Esc]";
            this.BtnClose.UseVisualStyleBackColor = false;
            this.BtnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // panelUpperBody
            // 
            this.panelUpperBody.Controls.Add(this.BtnAddnew);
            this.panelUpperBody.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelUpperBody.Location = new System.Drawing.Point(0, 79);
            this.panelUpperBody.Name = "panelUpperBody";
            this.panelUpperBody.Padding = new System.Windows.Forms.Padding(20);
            this.panelUpperBody.Size = new System.Drawing.Size(1242, 79);
            this.panelUpperBody.TabIndex = 13;
            // 
            // BtnAddnew
            // 
            this.BtnAddnew.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.BtnAddnew.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(112)))), ((int)(((byte)(173)))));
            this.BtnAddnew.FlatAppearance.BorderSize = 0;
            this.BtnAddnew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnAddnew.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnAddnew.ForeColor = System.Drawing.Color.White;
            this.BtnAddnew.Location = new System.Drawing.Point(12, 15);
            this.BtnAddnew.Name = "BtnAddnew";
            this.BtnAddnew.Size = new System.Drawing.Size(277, 50);
            this.BtnAddnew.TabIndex = 0;
            this.BtnAddnew.Text = "Add New Stock Type [F2]";
            this.BtnAddnew.UseVisualStyleBackColor = false;
            this.BtnAddnew.Click += new System.EventHandler(this.BtnAddnew_Click);
            // 
            // panelHeaderLine
            // 
            this.panelHeaderLine.Controls.Add(this.tableLayoutPanel1);
            this.panelHeaderLine.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeaderLine.Location = new System.Drawing.Point(0, 52);
            this.panelHeaderLine.Name = "panelHeaderLine";
            this.panelHeaderLine.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.panelHeaderLine.Size = new System.Drawing.Size(1242, 27);
            this.panelHeaderLine.TabIndex = 12;
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
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1222, 27);
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
            this.label2.Size = new System.Drawing.Size(115, 21);
            this.label2.TabIndex = 1;
            this.label2.Text = "Type Details";
            // 
            // panelHeader
            // 
            this.panelHeader.Controls.Add(this.label1);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1242, 52);
            this.panelHeader.TabIndex = 11;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(13, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(206, 23);
            this.label1.TabIndex = 3;
            this.label1.Text = "Maintain Stock Type";
            // 
            // dataGridViewImageColumn1
            // 
            this.dataGridViewImageColumn1.HeaderText = "Edit";
            this.dataGridViewImageColumn1.Image = global::DESKTOPNEDBILL.Properties.Resources.icons8_edit_20__1_;
            this.dataGridViewImageColumn1.MinimumWidth = 6;
            this.dataGridViewImageColumn1.Name = "dataGridViewImageColumn1";
            this.dataGridViewImageColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewImageColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dataGridViewImageColumn1.Width = 407;
            // 
            // FrmInventoryType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1242, 735);
            this.Controls.Add(this.panelBody);
            this.Controls.Add(this.panelFooter);
            this.Controls.Add(this.panelUpperBody);
            this.Controls.Add(this.panelHeaderLine);
            this.Controls.Add(this.panelHeader);
            this.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmInventoryType";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FrmInventoryType";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmInventoryType_FormClosed);
            this.Load += new System.EventHandler(this.FrmInventoryType_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmInventoryType_KeyDown);
            this.panelBody.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GrdInventoryType)).EndInit();
            this.panelFooter.ResumeLayout(false);
            this.panelUpperBody.ResumeLayout(false);
            this.panelHeaderLine.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelBody;
        private System.Windows.Forms.DataGridView GrdInventoryType;
        private System.Windows.Forms.Panel panelFooter;
        private System.Windows.Forms.Button BtnClose;
        private System.Windows.Forms.Panel panelUpperBody;
        private System.Windows.Forms.Button BtnAddnew;
        private System.Windows.Forms.Panel panelHeaderLine;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewImageColumn Edit;
    }
}