namespace ReportDll
{
    partial class frmReport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmReport));
            this.tbrMain = new System.Windows.Forms.ToolStrip();
            this.btnPrint = new System.Windows.Forms.ToolStripButton();
            this.ToolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnPreview = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.btnExpPdf = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnEmail = new System.Windows.Forms.ToolStripButton();
            this.btnClose = new System.Windows.Forms.ToolStripButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.cmbPrinter = new System.Windows.Forms.ComboBox();
            this.cmbHeaderwith = new System.Windows.Forms.ComboBox();
            this.btnDefault = new System.Windows.Forms.Button();
            this.cmbPaperOrientation = new System.Windows.Forms.ComboBox();
            this.cmbPaperType = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtLeftMargin = new System.Windows.Forms.TextBox();
            this.txtFooterMargin = new System.Windows.Forms.TextBox();
            this.txtTopMargin = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cmbTemplateName = new System.Windows.Forms.ComboBox();
            this.cmbReportType = new System.Windows.Forms.ComboBox();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnModify = new System.Windows.Forms.Button();
            this.btnSaveAs = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbrMain.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbrMain
            // 
            this.tbrMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(243)))), ((int)(((byte)(254)))));
            this.tbrMain.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbrMain.ImageScalingSize = new System.Drawing.Size(40, 40);
            this.tbrMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnPrint,
            this.ToolStripSeparator1,
            this.btnPreview,
            this.toolStripSeparator4,
            this.btnExpPdf,
            this.toolStripSeparator3,
            this.btnEmail,
            this.btnClose});
            this.tbrMain.Location = new System.Drawing.Point(0, 0);
            this.tbrMain.Name = "tbrMain";
            this.tbrMain.Size = new System.Drawing.Size(444, 64);
            this.tbrMain.TabIndex = 16;
            // 
            // btnPrint
            // 
            this.btnPrint.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.Image = ((System.Drawing.Image)(resources.GetObject("btnPrint.Image")));
            this.btnPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(44, 61);
            this.btnPrint.Text = "P&rint";
            this.btnPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnPrint.ToolTipText = "Print Summary";
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // ToolStripSeparator1
            // 
            this.ToolStripSeparator1.Name = "ToolStripSeparator1";
            this.ToolStripSeparator1.Size = new System.Drawing.Size(6, 64);
            // 
            // btnPreview
            // 
            this.btnPreview.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPreview.Image = ((System.Drawing.Image)(resources.GetObject("btnPreview.Image")));
            this.btnPreview.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Size = new System.Drawing.Size(59, 61);
            this.btnPreview.Text = "&Preview";
            this.btnPreview.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnPreview.Click += new System.EventHandler(this.btnPreview_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 64);
            // 
            // btnExpPdf
            // 
            this.btnExpPdf.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExpPdf.Image = ((System.Drawing.Image)(resources.GetObject("btnExpPdf.Image")));
            this.btnExpPdf.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExpPdf.Name = "btnExpPdf";
            this.btnExpPdf.Size = new System.Drawing.Size(99, 61);
            this.btnExpPdf.Text = "Export To PDF";
            this.btnExpPdf.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnExpPdf.Click += new System.EventHandler(this.btnExpPdf_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 64);
            // 
            // btnEmail
            // 
            this.btnEmail.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEmail.Image = ((System.Drawing.Image)(resources.GetObject("btnEmail.Image")));
            this.btnEmail.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEmail.Name = "btnEmail";
            this.btnEmail.Size = new System.Drawing.Size(44, 61);
            this.btnEmail.Text = "&Email";
            this.btnEmail.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnEmail.Visible = false;
            // 
            // btnClose
            // 
            this.btnClose.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(44, 61);
            this.btnClose.Text = "&Close";
            this.btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 64);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(444, 145);
            this.panel1.TabIndex = 17;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(14, 1);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(114, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "Paper Settings";
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.cmbPrinter);
            this.panel3.Controls.Add(this.cmbHeaderwith);
            this.panel3.Controls.Add(this.btnDefault);
            this.panel3.Controls.Add(this.cmbPaperOrientation);
            this.panel3.Controls.Add(this.cmbPaperType);
            this.panel3.Controls.Add(this.label14);
            this.panel3.Controls.Add(this.txtLeftMargin);
            this.panel3.Controls.Add(this.txtFooterMargin);
            this.panel3.Controls.Add(this.txtTopMargin);
            this.panel3.Controls.Add(this.label10);
            this.panel3.Controls.Add(this.label9);
            this.panel3.Controls.Add(this.label7);
            this.panel3.Controls.Add(this.label6);
            this.panel3.Controls.Add(this.label8);
            this.panel3.Controls.Add(this.label13);
            this.panel3.Controls.Add(this.label12);
            this.panel3.Controls.Add(this.label11);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel3.Location = new System.Drawing.Point(3, 8);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(436, 132);
            this.panel3.TabIndex = 0;
            // 
            // cmbPrinter
            // 
            this.cmbPrinter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPrinter.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmbPrinter.FormattingEnabled = true;
            this.cmbPrinter.Location = new System.Drawing.Point(88, 12);
            this.cmbPrinter.Name = "cmbPrinter";
            this.cmbPrinter.Size = new System.Drawing.Size(337, 25);
            this.cmbPrinter.TabIndex = 2;
            // 
            // cmbHeaderwith
            // 
            this.cmbHeaderwith.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbHeaderwith.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmbHeaderwith.FormattingEnabled = true;
            this.cmbHeaderwith.Location = new System.Drawing.Point(278, 97);
            this.cmbHeaderwith.Name = "cmbHeaderwith";
            this.cmbHeaderwith.Size = new System.Drawing.Size(147, 25);
            this.cmbHeaderwith.TabIndex = 2;
            // 
            // btnDefault
            // 
            this.btnDefault.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDefault.Location = new System.Drawing.Point(417, 100);
            this.btnDefault.Name = "btnDefault";
            this.btnDefault.Size = new System.Drawing.Size(19, 29);
            this.btnDefault.TabIndex = 1;
            this.btnDefault.Text = "Default";
            this.btnDefault.UseVisualStyleBackColor = true;
            this.btnDefault.Visible = false;
            // 
            // cmbPaperOrientation
            // 
            this.cmbPaperOrientation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPaperOrientation.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmbPaperOrientation.FormattingEnabled = true;
            this.cmbPaperOrientation.Location = new System.Drawing.Point(278, 68);
            this.cmbPaperOrientation.Name = "cmbPaperOrientation";
            this.cmbPaperOrientation.Size = new System.Drawing.Size(147, 25);
            this.cmbPaperOrientation.TabIndex = 2;
            // 
            // cmbPaperType
            // 
            this.cmbPaperType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPaperType.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmbPaperType.FormattingEnabled = true;
            this.cmbPaperType.Location = new System.Drawing.Point(87, 96);
            this.cmbPaperType.Name = "cmbPaperType";
            this.cmbPaperType.Size = new System.Drawing.Size(112, 25);
            this.cmbPaperType.TabIndex = 2;
            this.cmbPaperType.SelectedIndexChanged += new System.EventHandler(this.cmbPaperType_SelectedIndexChanged);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(38, 16);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(49, 17);
            this.label14.TabIndex = 0;
            this.label14.Text = "Printer";
            // 
            // txtLeftMargin
            // 
            this.txtLeftMargin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLeftMargin.Location = new System.Drawing.Point(278, 40);
            this.txtLeftMargin.Name = "txtLeftMargin";
            this.txtLeftMargin.Size = new System.Drawing.Size(48, 25);
            this.txtLeftMargin.TabIndex = 1;
            this.txtLeftMargin.Text = "0";
            this.txtLeftMargin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtLeftMargin.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtLeftMargin_KeyPress);
            // 
            // txtFooterMargin
            // 
            this.txtFooterMargin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFooterMargin.Location = new System.Drawing.Point(88, 68);
            this.txtFooterMargin.Name = "txtFooterMargin";
            this.txtFooterMargin.Size = new System.Drawing.Size(48, 25);
            this.txtFooterMargin.TabIndex = 1;
            this.txtFooterMargin.Text = "0";
            this.txtFooterMargin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtFooterMargin.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFooterMargin_KeyPress);
            // 
            // txtTopMargin
            // 
            this.txtTopMargin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTopMargin.Location = new System.Drawing.Point(88, 40);
            this.txtTopMargin.Name = "txtTopMargin";
            this.txtTopMargin.Size = new System.Drawing.Size(48, 25);
            this.txtTopMargin.TabIndex = 1;
            this.txtTopMargin.Text = "0";
            this.txtTopMargin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtTopMargin.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTopMargin_KeyPress);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label10.Location = new System.Drawing.Point(198, 72);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(77, 17);
            this.label10.TabIndex = 0;
            this.label10.Text = "Orientation";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label9.Location = new System.Drawing.Point(194, 101);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(82, 17);
            this.label9.TabIndex = 0;
            this.label9.Text = "Header with";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label7.Location = new System.Drawing.Point(-8, 71);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(95, 17);
            this.label7.TabIndex = 0;
            this.label7.Text = "Footer Margin";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label6.Location = new System.Drawing.Point(198, 44);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 17);
            this.label6.TabIndex = 0;
            this.label6.Text = "Left Margin";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label8.Location = new System.Drawing.Point(40, 99);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(43, 17);
            this.label8.TabIndex = 0;
            this.label8.Text = "Paper";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label13.Location = new System.Drawing.Point(141, 70);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(42, 17);
            this.label13.TabIndex = 0;
            this.label13.Text = "(Inch)";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label12.Location = new System.Drawing.Point(332, 44);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(42, 17);
            this.label12.TabIndex = 0;
            this.label12.Text = "(Inch)";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label11.Location = new System.Drawing.Point(139, 43);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(42, 17);
            this.label11.TabIndex = 0;
            this.label11.Text = "(Inch)";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label5.Location = new System.Drawing.Point(7, 43);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(78, 17);
            this.label5.TabIndex = 0;
            this.label5.Text = "Top Margin";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Template Details";
            this.label1.Visible = false;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.cmbTemplateName);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.cmbReportType);
            this.panel2.Controls.Add(this.btnDelete);
            this.panel2.Controls.Add(this.btnModify);
            this.panel2.Controls.Add(this.btnSaveAs);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Location = new System.Drawing.Point(308, 12);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(132, 33);
            this.panel2.TabIndex = 0;
            this.panel2.Visible = false;
            // 
            // cmbTemplateName
            // 
            this.cmbTemplateName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTemplateName.FormattingEnabled = true;
            this.cmbTemplateName.Location = new System.Drawing.Point(100, 69);
            this.cmbTemplateName.Name = "cmbTemplateName";
            this.cmbTemplateName.Size = new System.Drawing.Size(238, 21);
            this.cmbTemplateName.TabIndex = 2;
            // 
            // cmbReportType
            // 
            this.cmbReportType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbReportType.FormattingEnabled = true;
            this.cmbReportType.Location = new System.Drawing.Point(100, 46);
            this.cmbReportType.Name = "cmbReportType";
            this.cmbReportType.Size = new System.Drawing.Size(238, 21);
            this.cmbReportType.TabIndex = 2;
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(179, 14);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 29);
            this.btnDelete.TabIndex = 1;
            this.btnDelete.Text = "&Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            // 
            // btnModify
            // 
            this.btnModify.Location = new System.Drawing.Point(101, 14);
            this.btnModify.Name = "btnModify";
            this.btnModify.Size = new System.Drawing.Size(75, 29);
            this.btnModify.TabIndex = 1;
            this.btnModify.Text = "&Modify";
            this.btnModify.UseVisualStyleBackColor = true;
            // 
            // btnSaveAs
            // 
            this.btnSaveAs.Location = new System.Drawing.Point(16, 14);
            this.btnSaveAs.Name = "btnSaveAs";
            this.btnSaveAs.Size = new System.Drawing.Size(82, 29);
            this.btnSaveAs.TabIndex = 1;
            this.btnSaveAs.Text = "Save As ...";
            this.btnSaveAs.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 73);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(97, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Template Name";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Report Type";
            // 
            // frmReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(224)))), ((int)(((byte)(245)))));
            this.ClientSize = new System.Drawing.Size(444, 209);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.tbrMain);
            this.Controls.Add(this.panel2);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmReport";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Print Report";
            this.Load += new System.EventHandler(this.frmReport_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmReport_KeyDown);
            this.tbrMain.ResumeLayout(false);
            this.tbrMain.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.ToolStrip tbrMain;
        public System.Windows.Forms.ToolStripButton btnClose;
        public System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.Panel panel3;
        public System.Windows.Forms.Panel panel2;
        public System.Windows.Forms.ToolStripSeparator ToolStripSeparator1;
        public System.Windows.Forms.ToolStripButton btnPreview;
        public System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        public System.Windows.Forms.ToolStripButton btnEmail;
        public System.Windows.Forms.ToolStripButton btnPrint;
        public System.Windows.Forms.Label label2;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.Label label3;
        public System.Windows.Forms.Label label4;
        public System.Windows.Forms.Button btnDelete;
        public System.Windows.Forms.Button btnModify;
        public System.Windows.Forms.Button btnSaveAs;
        public System.Windows.Forms.ComboBox cmbTemplateName;
        public System.Windows.Forms.ComboBox cmbReportType;
        public System.Windows.Forms.Label label9;
        public System.Windows.Forms.Label label7;
        public System.Windows.Forms.Label label6;
        public System.Windows.Forms.Label label8;
        public System.Windows.Forms.Label label5;
        public System.Windows.Forms.Label label10;
        public System.Windows.Forms.TextBox txtTopMargin;
        public System.Windows.Forms.TextBox txtLeftMargin;
        public System.Windows.Forms.Label label11;
        public System.Windows.Forms.Label label12;
        public System.Windows.Forms.ComboBox cmbHeaderwith;
        public System.Windows.Forms.ComboBox cmbPaperOrientation;
        public System.Windows.Forms.ComboBox cmbPaperType;
        public System.Windows.Forms.TextBox txtFooterMargin;
        public System.Windows.Forms.Label label13;
        public System.Windows.Forms.Button btnDefault;
        public System.Windows.Forms.ComboBox cmbPrinter;
        public System.Windows.Forms.Label label14;
        public System.Windows.Forms.ToolStripButton btnExpPdf;
        public System.Windows.Forms.ToolStripSeparator toolStripSeparator3;


    }
}

