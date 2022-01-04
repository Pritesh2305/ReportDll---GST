using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Drawing.Printing;

namespace ReportDll
{
    public partial class frmReport : Form
    {
        private static frmReport _instance;
        private clsGeneral objclsgen = new clsGeneral();
        private clsMsSqlDbFunction objclsmssqldb = new clsMsSqlDbFunction();
        private clsPublicVariables.enumRMSForms _reportname;
        private string _moduleid;
        private string RptFolderName = "";
        private DateTime _fromdate;
        private Int64 _para;

        public Int64 Para
        {
            get { return _para; }
            set { _para = value; }
        }

        public DateTime Fromdate
        {
            get { return _fromdate; }
            set { _fromdate = value; }
        }
        private DateTime _todate;

        public DateTime Todate
        {
            get { return _todate; }
            set { _todate = value; }
        }

        # region property name

        public frmReport()
        {
            InitializeComponent();
        }

        public frmReport Instance
        {
            get
            {
                if (frmReport._instance == null)
                {
                    frmReport._instance = new frmReport();
                }

                return frmReport._instance;
            }
        }

        public string ModuleId
        {
            get { return this._moduleid; }
            set { this._moduleid = value; }
        }

        public clsPublicVariables.enumRMSForms ReportName
        {
            get { return this._reportname; }
            set { this._reportname = value; }
        }

        #endregion

        private void frmReport_Load(object sender, EventArgs e)
        {
            try
            {
                this.Fill_ComboBox();

                //switch (ReportName)
                //{
                //    case clsPublicVariables.enumRMSForms.RMS_BILL:
                //    case clsPublicVariables.enumRMSForms.RMS_KOT:                  
                //      RptFolderName = ReportName.ToString();

                //        break;                    
                //    default:
                //        break;
                //}

                //this.FillReportType();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occured in frmReport_Load() " + ex.Message.ToString(), clsPublicVariables.Project_Title, MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        private void frmReport_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    frmReport._instance = null;
                    this.Close();
                    break;

                case Keys.Enter:
                    SendKeys.Send("{TAB}");
                    break;

                case Keys.F5:
                    //string CntType = "";
                    //objclsgenral.FindControlType(this.ActiveControl, out CntType);
                    //objclsgenral.SetDefaultValues(this.Name, this.ActiveControl, CntType, "AdmissionDetails");
                    break;

                default:
                    break;
            }
        }

        private void Fill_ComboBox()
        {

            // Paper Orientation
            this.cmbPaperOrientation.Items.Clear();
            this.cmbPaperOrientation.Items.Add("Portrait");
            this.cmbPaperOrientation.Items.Add("Landscape");

            //Paper
            this.cmbPaperType.Items.Clear();
            this.cmbPaperType.Items.Add("Plain Paper");
            this.cmbPaperType.Items.Add("Letter Head");

            //Paper Header With
            this.cmbHeaderwith.Items.Clear();
            this.cmbHeaderwith.Items.Add("Without Image");
            this.cmbHeaderwith.Items.Add("Left Image");
            this.cmbHeaderwith.Items.Add("Right Image");
            this.cmbHeaderwith.Items.Add("Center Image");

            //Fill Printer Combo
            this.cmbPrinter.Items.Clear();
            PrinterSettings settings = new PrinterSettings();
            int i = 0;
            int deflprintind = 0;

            foreach (string printer in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
            {
                i = i + 1;
                settings.PrinterName = printer;
                if (settings.IsDefaultPrinter)
                {
                    deflprintind = i;
                }
                this.cmbPrinter.Items.Add(printer);
            }

            if (this.cmbPrinter.Items.Count > 0)
            {
                if (deflprintind > 0)
                {
                    this.cmbPrinter.SelectedIndex = deflprintind - 1;
                }
                else
                {
                    this.cmbPrinter.SelectedIndex = 0;
                }
            }

            if (this.cmbPaperOrientation.Items.Count > 0) { this.cmbPaperOrientation.SelectedIndex = 0; }
            if (this.cmbPaperType.Items.Count > 0) { this.cmbPaperType.SelectedIndex = 0; }
            if (this.cmbHeaderwith.Items.Count > 0) { this.cmbHeaderwith.SelectedIndex = 0; }

        }

        private void FillReportType()
        {
            try
            {
                string DirPath = "";
                string filename_1 = "";
                int strlength = 0;

                this.cmbReportType.Items.Clear();
                this.cmbReportType.Items.Add("[Default]");

                DirPath = clsPublicVariables.ReportPath + "\\" + RptFolderName;

                if (Directory.Exists(DirPath))
                {
                    DirectoryInfo dir = new DirectoryInfo(DirPath);
                    foreach (FileInfo file in dir.GetFiles())
                    {
                        strlength = (file.Name.IndexOf(".", StringComparison.Ordinal));
                        filename_1 = file.Name.Substring(0, Math.Min(file.Name.Length, strlength));

                        this.cmbReportType.Items.Add(filename_1);
                    }
                }

                if (this.cmbReportType.Items.Count > 0) { this.cmbReportType.SelectedIndex = 0; }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occured in FillReportType() " + ex.Message.ToString(), clsPublicVariables.Project_Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            frmReport._instance = null;
            this.Close();
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            try
            {
                string RptFileName = "";

                //if (this.cmbReportType.Text == "[Default]")
                //{
                //    objclsgen.LoadReport(true, ReportName, ModuleId, "", true);
                //}
                //else
                //{
                //RptFileName = clsPublicVariables.ReportPath + "\\" + this.cmbReportType.Text;

                switch (ReportName)
                {
                    case clsPublicVariables.enumRMSForms.RMS_CUSTOUTSTANDINGDTL:
                        objclsgen.LoadReport(false, ReportName, ModuleId, RptFileName, false, Fromdate, Todate, this.Instance.Para);
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_CUSTOUTSTANDINGDTLREFNO:
                        objclsgen.LoadReport(false, ReportName, ModuleId, RptFileName, false, Fromdate, Todate, this.Instance.Para);
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLDTLCUSTOMERWISEREFNO:
                        objclsgen.LoadReport(false, ReportName, ModuleId, RptFileName, false, Fromdate, Todate, this.Instance.Para);
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLDTLCUSTOMERWISE:
                        objclsgen.LoadReport(false, ReportName, ModuleId, RptFileName, false, Fromdate, Todate, this.Instance.Para);
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_40BARCODELABELA4:
                        objclsgen.LoadReport(false, ReportName, ModuleId, RptFileName, false, Fromdate, Todate, this.Instance.Para);
                        break;

                    default:
                        objclsgen.LoadReport(false, ReportName, ModuleId, RptFileName, false, Fromdate, Todate);
                        break;
                }
            }
            catch (Exception)
            { }
        }

        private void txtTopMargin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= 48 && e.KeyChar <= 57)
            {
                e.Handled = false;
            }
            else if (e.KeyChar == (char)Keys.Back)
            {
                e.Handled = false;
            }
            else if (e.KeyChar == 46)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtLeftMargin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= 48 && e.KeyChar <= 57)
            {
                e.Handled = false;
            }
            else if (e.KeyChar == (char)Keys.Back)
            {
                e.Handled = false;
            }
            else if (e.KeyChar == 46)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtFooterMargin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= 48 && e.KeyChar <= 57)
            {
                e.Handled = false;
            }
            else if (e.KeyChar == (char)Keys.Back)
            {
                e.Handled = false;
            }
            else if (e.KeyChar == 46)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                string RptFileName = "";
                //if (this.cmbReportType.Text == "[Default]")
                //{
                //    objclsgen.LoadReport(true, ReportName, ModuleId, "", true);
                //}
                //else
                //{
                //RptFileName = clsPublicVariables.ReportPath + "\\" + this.cmbReportType.Text;
                switch (ReportName)
                {
                    case clsPublicVariables.enumRMSForms.RMS_CUSTOUTSTANDINGDTL:
                        objclsgen.LoadReport(false, ReportName, ModuleId, RptFileName, true, this.Instance.Fromdate, this.Instance.Todate, this.Instance.Para);
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_CUSTOUTSTANDINGDTLREFNO:
                        objclsgen.LoadReport(false, ReportName, ModuleId, RptFileName, true, this.Instance.Fromdate, this.Instance.Todate, this.Instance.Para);
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLDTLCUSTOMERWISEREFNO:
                        objclsgen.LoadReport(false, ReportName, ModuleId, RptFileName, true, this.Instance.Fromdate, this.Instance.Todate, this.Instance.Para);
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLDTLCUSTOMERWISE:
                        objclsgen.LoadReport(false, ReportName, ModuleId, RptFileName, true, this.Instance.Fromdate, this.Instance.Todate, this.Instance.Para);
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_40BARCODELABELA4:
                        objclsgen.LoadReport(false, ReportName, ModuleId, RptFileName, true, this.Instance.Fromdate, this.Instance.Todate, this.Instance.Para);
                        break;                    
                    default:
                        objclsgen.LoadReport(false, ReportName, ModuleId, RptFileName, true, this.Instance.Fromdate, this.Instance.Todate);
                        break;
                }
            }
            catch (Exception)
            { }
        }

        private void btnExpPdf_Click(object sender, EventArgs e)
        {
            try
            {
                string RptFileName = "";

                //if (this.cmbReportType.Text == "[Default]")
                //{
                //    objclsgen.ExportReportToPdf(true, ReportName.ToString(), ModuleId, "");
                //}
                //else
                //{
                //RptFileName = clsPublicVariables.ReportPath + "\\" + RptFolderName + "\\" + this.cmbReportType.Text;

                switch (ReportName)
                {
                    case clsPublicVariables.enumRMSForms.RMS_CUSTOUTSTANDINGDTL:
                        objclsgen.ExportReportToPdf(false, ReportName.ToString(), ModuleId, RptFileName, this.Instance.Fromdate, this.Instance.Todate, this.Instance.Para);
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_CUSTOUTSTANDINGDTLREFNO:
                        objclsgen.ExportReportToPdf(false, ReportName.ToString(), ModuleId, RptFileName, this.Instance.Fromdate, this.Instance.Todate, this.Instance.Para);
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLDTLCUSTOMERWISEREFNO:
                        objclsgen.ExportReportToPdf(false, ReportName.ToString(), ModuleId, RptFileName, this.Instance.Fromdate, this.Instance.Todate, this.Instance.Para);
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLDTLCUSTOMERWISE:
                        objclsgen.ExportReportToPdf(false, ReportName.ToString(), ModuleId, RptFileName, this.Instance.Fromdate, this.Instance.Todate, this.Instance.Para);
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_40BARCODELABELA4:
                        objclsgen.ExportReportToPdf(false, ReportName.ToString(), ModuleId, RptFileName, this.Instance.Fromdate, this.Instance.Todate, this.Instance.Para);
                        break;
                    default:
                        objclsgen.ExportReportToPdf(false, ReportName.ToString(), ModuleId, RptFileName, this.Instance.Fromdate, this.Instance.Todate);
                        break;
                }
            }
            catch (Exception)
            { }          
        }

        private void cmbPaperType_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.cmbHeaderwith.Enabled = true;

            if (cmbPaperType.Text != "Plain Paper")
            {
                this.cmbHeaderwith.Enabled = false;
            }
        }


    }
}
