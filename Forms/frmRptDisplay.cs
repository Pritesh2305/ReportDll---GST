using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;

namespace ReportDll
{
    public partial class frmRptDisplay : Form
    {
        private string _formulastr;
        private ReportDocument _rptdocument;
        private clsGeneral.enumRMSForms _reportnm;

        public clsGeneral.enumRMSForms Reportnm
        {
            get { return _reportnm; }
            set { _reportnm = value; }
        }
        
        private static frmRptDisplay _instance;

        public frmRptDisplay Instance
        {
            get
            {
                if (frmRptDisplay._instance == null)
                {
                    frmRptDisplay._instance = new frmRptDisplay();
                }

                return frmRptDisplay._instance;
            }
        }

        public string FormulaStr
        {
            get { return this._formulastr; }
            set { this._formulastr = value; }
        }

        public ReportDocument ReportDocument
        {
            get { return this._rptdocument; }
            set { this._rptdocument = value; }
        }

        public frmRptDisplay()
        {
            InitializeComponent();
        }

        private void frmRptDisplay_Load(object sender, EventArgs e)
        {
            //crptViewer.ParameterFieldInfo.
            crptViewer.SelectionFormula = FormulaStr;
            crptViewer.ReportSource = ReportDocument;
            
            switch (Reportnm)
            {
                case clsPublicVariables.enumRMSForms.POS_40BARCODELABELA4:
                    crptViewer.RefreshReport();
                    break;
                case clsPublicVariables.enumRMSForms.POS_NOOFITEMBARCODE:
                    crptViewer.RefreshReport();
                    break;
                case clsPublicVariables.enumRMSForms.POS_ITEMMASTERWITHBARCODE:
                    crptViewer.RefreshReport();
                    break;
                case clsPublicVariables.enumRMSForms.RMS_40BARCODELABELA4:
                    crptViewer.RefreshReport();
                    break;
                
                default:
                    break;
            }

        }
    }
}
