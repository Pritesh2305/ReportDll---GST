using System;
using System.IO;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Windows.Forms;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using System.Data.SqlClient;

namespace ReportDll
{
    public class clsGeneral : clsPublicVariables
    {
        private DateTime _fromdate;

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

        private Int64 _para1;

        public Int64 Para1
        {
            get { return _para1; }
            set { _para1 = value; }
        }

        private void GetConnectionDetails()
        {
            try
            {
                ServerName1 = System.Configuration.ConfigurationManager.AppSettings["SERVER"];
                DatabaseName1 = System.Configuration.ConfigurationManager.AppSettings["DATABASE"];
                UserName1 = System.Configuration.ConfigurationManager.AppSettings["DBUSERID"];
                Password1 = System.Configuration.ConfigurationManager.AppSettings["DBPASSWORD"];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString() + " Error occures in GetConnectionDetails())", clsGeneral.Project_Title);
            }
        }

        private void GetSettingDetails()
        {
            DataTable dtsetting = new DataTable();
            clsMsSqlDbFunction mysql = new clsMsSqlDbFunction();
            try
            {
                dtsetting = mysql.FillDataTable("SELECT * FROM RMSSETTING", "RMSSETTING");

                RptBillTitle = "";
                RptTitle1 = "";
                RptTitle2 = "";
                RptTitle3 = "";
                RptTitle4 = "";
                RptTitle5 = "";
                RptTitle6 = "";
                RptTitle7 = "";
                RptTitle8 = "";

                RptPrintername = "";
                RptBillPrintername = "";
                RptKotPrintername = "";
                RptBarPrintername = "";
                RptReportPrintername = "";
                GENCOSTUMEPRINTER = "";
                GENTICKETPRINTER = "";

                RPtFooter1 = "";
                RPtFooter2 = "";
                RPtFooter3 = "";
                RPtFooter4 = "";
                RPtFooter5 = "";
                RPtFooter6 = "";
                RPtFooter7 = "";

                RPtBqHeader1 = "";
                RPtBqHeader2 = "";
                RPtBqHeader3 = "";
                RPtBqHeader4 = "";
                RPtBqHeader5 = "";
                RPtBqHeader6 = "";
                RPtBqHeader7 = "";
                RPtBqHeader8 = "";

                RPtBqFooter1 = "";
                RPtBqFooter2 = "";
                RPtBqFooter3 = "";
                RPtBqFooter4 = "";
                RPtBqFooter5 = "";
                RPtBqFooter6 = "";
                RPtBqFooter7 = "";
                RPtBqFooter8 = "";

                GENBILLNOBASEDON = "";
                GENCBHEADER1 = "";
                GENCBHEADER2 = "";
                GENCBHEADER3 = "";
                GENCBFOOTER1 = "";
                GENCBFOOTER2 = "";
                GENCBFOOTER3 = "";

                if (dtsetting.Rows.Count > 0)
                {
                    foreach (DataRow row in dtsetting.Rows)
                    {
                        RptTitle1 = (row["GENERALTITLE1"]) + "".Trim();
                        RptTitle2 = (row["GENERALTITLE2"]) + "".Trim();
                        RptTitle3 = (row["GENERALTITLE3"]) + "".Trim();
                        RptTitle4 = (row["GENERALTITLE4"]) + "".Trim();
                        RptTitle5 = (row["GENERALTITLE5"]) + "".Trim();
                        RptPrintername = (row["REPORTPRINTER"]) + "".Trim();
                        RPtFooter1 = (row["GENERALFOOTER1"]) + "".Trim();
                        RPtFooter2 = (row["GENERALFOOTER2"]) + "".Trim();
                        RPtFooter3 = (row["GENERALFOOTER3"]) + "".Trim();
                        RPtFooter4 = (row["GENERALFOOTER4"]) + "".Trim();
                        RPtFooter5 = (row["GENERALFOOTER5"]) + "".Trim();

                        try
                        { RPtBqHeader1 = (row["BANQHEADER1"]) + "".Trim(); }
                        catch (Exception) { }

                        try
                        { RPtBqHeader2 = (row["BANQHEADER2"]) + "".Trim(); }
                        catch (Exception) { }

                        try
                        { RPtBqHeader3 = (row["BANQHEADER3"]) + "".Trim(); }
                        catch (Exception) { }

                        try
                        { RPtBqHeader4 = (row["BANQHEADER4"]) + "".Trim(); }
                        catch (Exception) { }

                        try
                        { RPtBqHeader5 = (row["BANQHEADER5"]) + "".Trim(); }
                        catch (Exception) { }

                        try
                        { RPtBqFooter1 = (row["BANQFOOTER1"]) + "".Trim(); }
                        catch (Exception) { }

                        try
                        { RPtBqFooter2 = (row["BANQFOOTER2"]) + "".Trim(); }
                        catch (Exception) { }

                        try
                        { RPtBqFooter3 = (row["BANQFOOTER3"]) + "".Trim(); }
                        catch (Exception) { }

                        try
                        { RPtBqFooter4 = (row["BANQFOOTER4"]) + "".Trim(); }
                        catch (Exception) { }

                        try
                        { RPtBqFooter5 = (row["BANQFOOTER5"]) + "".Trim(); }
                        catch (Exception) { }

                        try
                        { GENBILLNOBASEDON = (row["BILLNOBASEDON"]) + "".Trim(); }
                        catch (Exception) { }

                        /// Load Setting
                        string priterpath = clsPublicVariables.AppPath + "\\printersetting.txt";
                        string printername1 = "";
                        string[] printernm;
                        if (System.IO.File.Exists(priterpath))
                        {
                            TextReader tr = new StreamReader(priterpath);
                            // read a line of text
                            printername1 = tr.ReadLine().ToString();
                            printernm = printername1.Split('|');

                            //foreach (string print1 in printernm)
                            //{

                            RptKotPrintername = printernm[0].ToString() + "";
                            RptBillPrintername = printernm[1].ToString() + "";
                            RptReportPrintername = printernm[2].ToString() + "";

                            if (DatabaseName1 + "".ToUpper() != "SPOS")
                            {
                                RptBarPrintername = printernm[3].ToString() + "";
                            }
                            try { RptKot2Printername = printernm[4].ToString() + ""; }
                            catch (Exception) { }
                            try { RptlabelPrintername = printernm[5].ToString() + ""; }
                            catch (Exception) { }
                            try { GENCOSTUMEPRINTER = printernm[10].ToString() + ""; }
                            catch (Exception) { }
                            try { GENTICKETPRINTER = printernm[11].ToString() + ""; }
                            catch (Exception) { }
                            // }
                            tr.Close();
                        }
                        else
                        {
                            RptKotPrintername = (row["KITPRINTER"]) + "".Trim();
                            RptBillPrintername = (row["BILLINGPRINTER"]) + "".Trim();
                            RptReportPrintername = (row["REPORTPRINTER"]) + "".Trim();

                            if (DatabaseName1 + "".ToUpper() != "SPOS")
                            {
                                RptBarPrintername = (row["BARPRINTER"]) + "".Trim();
                            }
                            try
                            { RptKot2Printername = (row["KIT2PRINTER"]) + "".Trim(); }
                            catch (Exception) { }
                            try
                            { RptlabelPrintername = (row["LABLEPRINTER"]) + "".Trim(); }
                            catch (Exception) { }
                            try
                            { GENCOSTUMEPRINTER = (row["COSTUMEPRINTER"]) + "".Trim(); }
                            catch (Exception) { }
                            try
                            { GENTICKETPRINTER = (row["TICKETPRINTER"]) + "".Trim(); }
                            catch (Exception) { }
                        }

                        RptBillTitle = (row["GENBILLTITLE"]) + "".Trim();
                        RptTitle6 = (row["GENERALTITLE6"]) + "".Trim();
                        RptTitle7 = (row["GENERALTITLE7"]) + "".Trim();
                        RptTitle8 = (row["GENERALTITLE8"]) + "".Trim();
                        RPtFooter6 = (row["GENERALFOOTER6"]) + "".Trim();
                        RPtFooter7 = (row["GENERALFOOTER7"]) + "".Trim();
                        RPtBqHeader6 = (row["BANQHEADER6"]) + "".Trim();
                        RPtBqHeader7 = (row["BANQHEADER7"]) + "".Trim();
                        RPtBqHeader8 = (row["BANQHEADER8"]) + "".Trim();
                        RPtBqFooter6 = (row["BANQFOOTER6"]) + "".Trim();
                        RPtBqFooter7 = (row["BANQFOOTER7"]) + "".Trim();
                        RPtBqFooter8 = (row["BANQFOOTER8"]) + "".Trim();

                        GENCBHEADER1 = (row["ECHEADER1"]) + "".Trim();
                        GENCBHEADER2 = (row["ECHEADER2"]) + "".Trim();
                        GENCBHEADER3 = (row["ECHEADER3"]) + "".Trim();
                        GENCBFOOTER1 = (row["ECFOOTER1"]) + "".Trim();
                        GENCBFOOTER2 = (row["ECFOOTER2"]) + "".Trim();
                        GENCBFOOTER3 = (row["ECFOOTER3"]) + "".Trim();

                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString() + " Error occures in GetSettingDetails())", clsPublicVariables.Project_Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public double Null2Dbl(object Numbr1)
        {
            Double Amt1;
            try
            {

                if (DBNull.Value == Numbr1)
                {
                    Amt1 = 0;
                }

                else if ((string.IsNullOrEmpty(Numbr1.ToString()) || (Numbr1.ToString().Trim() == "")))
                {
                    Amt1 = 0;
                }
                else
                {
                    Amt1 = (double)Numbr1;
                }

                return (double)Amt1;
            }
            catch (Exception)
            {

                return 0;
            }
        }

        public long Null2lng(object Numbr1)
        {
            long Amt1;
            try
            {

                if (DBNull.Value == Numbr1)
                {
                    Amt1 = 0;
                }

                else if ((string.IsNullOrEmpty(Numbr1.ToString()) || (Numbr1.ToString().Trim() == "")))
                {
                    Amt1 = 0;
                }
                else
                {
                    Amt1 = Convert.ToInt64(Numbr1);
                }

                return Convert.ToInt64(Amt1);
            }
            catch (Exception)
            {

                return 0;
            }
        }

        public DateTime Null2Date(object Dt1)
        {
            DateTime tDt1;
            //DateTime eDt1;
            bool IsEmpDateYn;
            try
            {
                if (DBNull.Value == Dt1)
                {
                    IsEmpDateYn = true;
                }
                else if (Dt1.ToString() == "1/1/1753")
                {
                    IsEmpDateYn = true;
                }
                else if ((Dt1.ToString().Trim() == ""))
                {
                    IsEmpDateYn = true;
                }
                else
                {
                    IsEmpDateYn = false;
                }
                if ((IsEmpDateYn == true))
                {
                    tDt1 = DateTime.MinValue;
                }
                else
                {
                    tDt1 = ((DateTime)(Dt1));
                }

                return tDt1;
            }
            catch (Exception)
            {

                return (DateTime)Dt1;
            }
        }

        public string Null2Str(object Str1)
        {
            string tStr1;
            try
            {
                if (DBNull.Value == (Str1))
                {
                    tStr1 = " ";
                }
                else if (string.IsNullOrEmpty(Str1.ToString()))
                {
                    tStr1 = " ";
                }
                else
                {
                    tStr1 = Str1.ToString();
                }
                return tStr1;
            }
            catch (Exception)
            {
                return Str1.ToString();
            }
        }

        public void OpenForm(string FormName_1, string ModuleId_1)
        {
            frmImage frmimg = new frmImage();
            try
            {
                this.GetConnectionDetails();

                this.GetSettingDetails();

                clsPublicVariables.enumRMSForms FormEnum = (clsPublicVariables.enumRMSForms)Enum.Parse(typeof(clsPublicVariables.enumRMSForms), FormName_1, true);

                switch (FormEnum)
                {

                    case enumRMSForms.RMS_BILL:
                        frmReport frm2 = new frmReport();
                        frm2.Instance.ReportName = FormEnum;
                        frm2.Instance.ModuleId = ModuleId_1;
                        frm2.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_BILLB:
                        frmReport frmb2 = new frmReport();
                        frmb2.Instance.ReportName = FormEnum;
                        frmb2.Instance.ModuleId = ModuleId_1;
                        frmb2.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_KOT:
                        frmReport frmReceipts = new frmReport();
                        frmReceipts.Instance.ReportName = FormEnum;
                        frmReceipts.Instance.ModuleId = ModuleId_1;
                        frmReceipts.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_KOTB:
                        frmReport frmReceiptsb = new frmReport();
                        frmReceiptsb.Instance.ReportName = FormEnum;
                        frmReceiptsb.Instance.ModuleId = ModuleId_1;
                        frmReceiptsb.Instance.ShowDialog();
                        break;
                    case enumRMSForms.POS_BILL:
                        frmReport frmposbill = new frmReport();
                        frmposbill.Instance.ReportName = FormEnum;
                        frmposbill.Instance.ModuleId = ModuleId_1;
                        frmposbill.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_BANQBOOKING:
                        frmReport frmbanqbo = new frmReport();
                        frmbanqbo.Instance.ReportName = FormEnum;
                        frmbanqbo.Instance.ModuleId = ModuleId_1;
                        frmbanqbo.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_BANQBILLINGINFO:
                        frmReport frmbanqbobill = new frmReport();
                        frmbanqbobill.Instance.ReportName = FormEnum;
                        frmbanqbobill.Instance.ModuleId = ModuleId_1;
                        frmbanqbobill.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_BANQINQUIRY:
                        frmReport frmbanqinq = new frmReport();
                        frmbanqinq.Instance.ReportName = FormEnum;
                        frmbanqinq.Instance.ModuleId = ModuleId_1;
                        frmbanqinq.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_BQBOOKING:
                        frmReport frmbanqbo1 = new frmReport();
                        frmbanqbo1.Instance.ReportName = FormEnum;
                        frmbanqbo1.Instance.ModuleId = ModuleId_1;
                        frmbanqbo1.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_ITEMPURCHASE:
                        frmReport frmip = new frmReport();
                        frmip.Instance.ReportName = FormEnum;
                        frmip.Instance.ModuleId = ModuleId_1;
                        frmip.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_STOCKISSUE:
                        frmReport frmstkis = new frmReport();
                        frmstkis.Instance.ReportName = FormEnum;
                        frmstkis.Instance.ModuleId = ModuleId_1;
                        frmstkis.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_CASHONHAND:
                        frmReport frmcashonhand1 = new frmReport();
                        frmcashonhand1.Instance.ReportName = FormEnum;
                        frmcashonhand1.Instance.ModuleId = ModuleId_1;
                        frmcashonhand1.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_PAYMENTINFO:
                        frmReport frmpayvour = new frmReport();
                        frmpayvour.Instance.ReportName = FormEnum;
                        frmpayvour.Instance.ModuleId = ModuleId_1;
                        frmpayvour.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_ITEMWISEPURCHASE:
                        frmReport frmitmwisepur = new frmReport();
                        frmitmwisepur.Instance.ReportName = FormEnum;
                        frmitmwisepur.Instance.ModuleId = ModuleId_1;
                        frmitmwisepur.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_PURREQORDER:
                        frmReport frmpurreq = new frmReport();
                        frmpurreq.Instance.ReportName = FormEnum;
                        frmpurreq.Instance.ModuleId = ModuleId_1;
                        frmpurreq.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_TABLEWAITING:
                        frmReport frmrpt1 = new frmReport();
                        frmrpt1.Instance.ReportName = FormEnum;
                        frmrpt1.Instance.ModuleId = ModuleId_1;
                        frmrpt1.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_TABLERESERVATION:
                        frmReport frmrpt2 = new frmReport();
                        frmrpt2.Instance.ReportName = FormEnum;
                        frmrpt2.Instance.ModuleId = ModuleId_1;
                        frmrpt2.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_COSTUMEBILL:
                        frmReport frmrpt3 = new frmReport();
                        frmrpt3.Instance.ReportName = FormEnum;
                        frmrpt3.Instance.ModuleId = ModuleId_1;
                        frmrpt3.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_ENTRYTICKET:
                        frmReport frmrpt4 = new frmReport();
                        frmrpt4.Instance.ReportName = FormEnum;
                        frmrpt4.Instance.ModuleId = ModuleId_1;
                        frmrpt4.Instance.ShowDialog();
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString() + " Error occures in OpenForm())", clsPublicVariables.Project_Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public void Open_NoParameter_Reportform(string FormName_1)
        {

            DateTime fdate1 = new DateTime(2000, 01, 01);
            DateTime tdate1 = new DateTime(2050, 01, 01);

            try
            {
                this.GetConnectionDetails();
                this.GetSettingDetails();

                clsPublicVariables.enumRMSForms FormEnum = (clsPublicVariables.enumRMSForms)Enum.Parse(typeof(clsPublicVariables.enumRMSForms), FormName_1, true);

                switch (FormEnum)
                {
                    case enumRMSForms.RMS_MSTITEM:
                        frmReport frmrpt1 = new frmReport();
                        frmrpt1.Instance.ReportName = FormEnum;
                        frmrpt1.Instance.ModuleId = "";
                        frmrpt1.Instance.Fromdate = Convert.ToDateTime(fdate1);
                        frmrpt1.Instance.Todate = Convert.ToDateTime(tdate1);
                        frmrpt1.Instance.ShowDialog();
                        break;

                    case enumRMSForms.POS_ITEMMASTERWITHBARCODE:
                        frmReport frmrpt2 = new frmReport();
                        frmrpt2.Instance.ReportName = FormEnum;
                        frmrpt2.Instance.ModuleId = "";
                        frmrpt2.Instance.Fromdate = Convert.ToDateTime(fdate1);
                        frmrpt2.Instance.Todate = Convert.ToDateTime(tdate1);
                        frmrpt2.Instance.ShowDialog();
                        break;

                    case enumRMSForms.POS_NOOFITEMBARCODE:
                        frmReport frmrpt3 = new frmReport();
                        frmrpt3.Instance.ReportName = FormEnum;
                        frmrpt3.Instance.ModuleId = "";
                        frmrpt3.Instance.Fromdate = Convert.ToDateTime(fdate1);
                        frmrpt3.Instance.Todate = Convert.ToDateTime(tdate1);
                        frmrpt3.Instance.ShowDialog();
                        break;
                    case enumRMSForms.POS_40BARCODELABELA4:
                        frmReport frmrpt4 = new frmReport();
                        frmrpt4.Instance.ReportName = FormEnum;
                        frmrpt4.Instance.ModuleId = "";
                        frmrpt4.Instance.Fromdate = Convert.ToDateTime(fdate1);
                        frmrpt4.Instance.Todate = Convert.ToDateTime(tdate1);
                        frmrpt4.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_40BARCODELABELA4:
                        frmReport frmrpt5 = new frmReport();
                        frmrpt5.Instance.ReportName = FormEnum;
                        frmrpt5.Instance.ModuleId = "";
                        frmrpt5.Instance.Fromdate = Convert.ToDateTime(fdate1);
                        frmrpt5.Instance.Todate = Convert.ToDateTime(tdate1);
                        frmrpt5.Instance.ShowDialog();
                        break;

                    default:
                        break;
                }

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString() + " Error occures in Open_NoParameter_Reportform())", clsPublicVariables.Project_Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        public void OpenFrom_To_Date_Para1_Parameter_Reportform(string FormName_1, DateTime Fromdate1, DateTime Todate1, Int64 Para1)
        {
            try
            {
                this.GetConnectionDetails();
                this.GetSettingDetails();

                this.Fromdate = Fromdate1;
                this.Todate = Todate1;

                clsPublicVariables.enumRMSForms FormEnum = (clsPublicVariables.enumRMSForms)Enum.Parse(typeof(clsPublicVariables.enumRMSForms), FormName_1, true);

                switch (FormEnum)
                {
                    case enumRMSForms.RMS_CUSTOUTSTANDINGDTL:
                        frmReport frm1 = new frmReport();
                        frm1.Instance.ReportName = FormEnum;
                        frm1.Instance.ModuleId = "";
                        frm1.Instance.Fromdate = Fromdate;
                        frm1.Instance.Todate = Todate;
                        frm1.Instance.Para = Para1;
                        frm1.Instance.Instance.ShowDialog();
                        break;

                    case enumRMSForms.RMS_CUSTOUTSTANDINGDTLREFNO:
                        frmReport frm4 = new frmReport();
                        frm4.Instance.ReportName = FormEnum;
                        frm4.Instance.ModuleId = "";
                        frm4.Instance.Fromdate = Fromdate;
                        frm4.Instance.Todate = Todate;
                        frm4.Instance.Para = Para1;
                        frm4.Instance.Instance.ShowDialog();
                        break;

                    case enumRMSForms.RMS_BILLDTLCUSTOMERWISEREFNO:
                        frmReport frm2 = new frmReport();
                        frm2.Instance.ReportName = FormEnum;
                        frm2.Instance.ModuleId = "";
                        frm2.Instance.Fromdate = Fromdate;
                        frm2.Instance.Todate = Todate;
                        frm2.Instance.Para = Para1;
                        frm2.Instance.Instance.ShowDialog();
                        break;

                    case enumRMSForms.RMS_BILLDTLCUSTOMERWISE:
                        frmReport frm3 = new frmReport();
                        frm3.Instance.ReportName = FormEnum;
                        frm3.Instance.ModuleId = "";
                        frm3.Instance.Fromdate = Fromdate;
                        frm3.Instance.Todate = Todate;
                        frm3.Instance.Para = Para1;
                        frm3.Instance.Instance.ShowDialog();
                        break;

                    default:
                        break;
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString() + " Error occures in OpenFrom_To_Date_Para1_Parameter_Reportform())", clsPublicVariables.Project_Title, MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        public void OpenFrom_To_Date_Parameter_Reportform(string FormName_1, DateTime Fromdate1, DateTime Todate1)
        {
            frmImage frmimg = new frmImage();
            try
            {
                this.GetConnectionDetails();
                this.GetSettingDetails();

                this.Fromdate = Fromdate1;
                this.Todate = Todate1;

                clsPublicVariables.enumRMSForms FormEnum = (clsPublicVariables.enumRMSForms)Enum.Parse(typeof(clsPublicVariables.enumRMSForms), FormName_1, true);

                switch (FormEnum)
                {
                    case enumRMSForms.RMS_KOTREGISTER:
                        frmReport frm1 = new frmReport();
                        frm1.Instance.ReportName = FormEnum;
                        frm1.Instance.ModuleId = "";
                        frm1.Instance.Fromdate = Fromdate;
                        frm1.Instance.Todate = Todate;
                        frm1.Instance.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_KOTBREGISTER:
                        frmReport frmb1 = new frmReport();
                        frmb1.Instance.ReportName = FormEnum;
                        frmb1.Instance.ModuleId = "";
                        frmb1.Instance.Fromdate = Fromdate;
                        frmb1.Instance.Todate = Todate;
                        frmb1.Instance.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_BILLREGISTER:
                        frmReport frm2 = new frmReport();
                        frm2.Instance.ReportName = FormEnum;
                        frm2.Instance.ModuleId = "";
                        frm2.Instance.Fromdate = Fromdate;
                        frm2.Instance.Todate = Todate;
                        frm2.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_BILLREGISTER_REFNO:
                        frmReport frm2r = new frmReport();
                        frm2r.Instance.ReportName = FormEnum;
                        frm2r.Instance.ModuleId = "";
                        frm2r.Instance.Fromdate = Fromdate;
                        frm2r.Instance.Todate = Todate;
                        frm2r.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_BILLBREGISTER:
                        frmReport frmb2 = new frmReport();
                        frmb2.Instance.ReportName = FormEnum;
                        frmb2.Instance.ModuleId = "";
                        frmb2.Instance.Fromdate = Fromdate;
                        frmb2.Instance.Todate = Todate;
                        frmb2.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_BILLREG:
                        frmReport frm3 = new frmReport();
                        frm3.Instance.ReportName = FormEnum;
                        frm3.Instance.ModuleId = "";
                        frm3.Instance.Fromdate = Fromdate;
                        frm3.Instance.Todate = Todate;
                        frm3.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_BILLREG_REFNO:
                        frmReport frm3ref = new frmReport();
                        frm3ref.Instance.ReportName = FormEnum;
                        frm3ref.Instance.ModuleId = "";
                        frm3ref.Instance.Fromdate = Fromdate;
                        frm3ref.Instance.Todate = Todate;
                        frm3ref.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_BILLBREG:
                        frmReport frmb3 = new frmReport();
                        frmb3.Instance.ReportName = FormEnum;
                        frmb3.Instance.ModuleId = "";
                        frmb3.Instance.Fromdate = Fromdate;
                        frmb3.Instance.Todate = Todate;
                        frmb3.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_KOTREG:
                        frmReport frm4 = new frmReport();
                        frm4.Instance.ReportName = FormEnum;
                        frm4.Instance.ModuleId = "";
                        frm4.Instance.Fromdate = Fromdate;
                        frm4.Instance.Todate = Todate;
                        frm4.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_KOTEDITREG:
                        frmReport frm41 = new frmReport();
                        frm41.Instance.ReportName = FormEnum;
                        frm41.Instance.ModuleId = "";
                        frm41.Instance.Fromdate = Fromdate;
                        frm41.Instance.Todate = Todate;
                        frm41.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_KOTDELETEREG:
                        frmReport frm42 = new frmReport();
                        frm42.Instance.ReportName = FormEnum;
                        frm42.Instance.ModuleId = "";
                        frm42.Instance.Fromdate = Fromdate;
                        frm42.Instance.Todate = Todate;
                        frm42.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_KOTBREG:
                        frmReport frmb4 = new frmReport();
                        frmb4.Instance.ReportName = FormEnum;
                        frmb4.Instance.ModuleId = "";
                        frmb4.Instance.Fromdate = Fromdate;
                        frmb4.Instance.Todate = Todate;
                        frmb4.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_SETTLEMENT:
                        frmReport frm5 = new frmReport();
                        frm5.Instance.ReportName = FormEnum;
                        frm5.Instance.ModuleId = "";
                        frm5.Instance.Fromdate = Fromdate;
                        frm5.Instance.Todate = Todate;
                        frm5.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_SETTLEMENTREFNO:
                        frmReport frm5r = new frmReport();
                        frm5r.Instance.ReportName = FormEnum;
                        frm5r.Instance.ModuleId = "";
                        frm5r.Instance.Fromdate = Fromdate;
                        frm5r.Instance.Todate = Todate;
                        frm5r.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_SETTLEMENTB:
                        frmReport frmb5 = new frmReport();
                        frmb5.Instance.ReportName = FormEnum;
                        frmb5.Instance.ModuleId = "";
                        frmb5.Instance.Fromdate = Fromdate;
                        frmb5.Instance.Todate = Todate;
                        frmb5.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_PURCHAESREGISTER:
                        frmReport frmb6 = new frmReport();
                        frmb6.Instance.ReportName = FormEnum;
                        frmb6.Instance.ModuleId = "";
                        frmb6.Instance.Fromdate = Fromdate;
                        frmb6.Instance.Todate = Todate;
                        frmb6.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_PAYMENTREGISTER:
                        frmReport frmb7 = new frmReport();
                        frmb7.Instance.ReportName = FormEnum;
                        frmb7.Instance.ModuleId = "";
                        frmb7.Instance.Fromdate = Fromdate;
                        frmb7.Instance.Todate = Todate;
                        frmb7.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_CUSTOUTSTANDING:
                        frmReport frmb8 = new frmReport();
                        frmb8.Instance.ReportName = FormEnum;
                        frmb8.Instance.ModuleId = "";
                        frmb8.Instance.Fromdate = Fromdate;
                        frmb8.Instance.Todate = Todate;
                        frmb8.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_BILLEDITREG:
                        frmReport frm421 = new frmReport();
                        frm421.Instance.ReportName = FormEnum;
                        frm421.Instance.ModuleId = "";
                        frm421.Instance.Fromdate = Fromdate;
                        frm421.Instance.Todate = Todate;
                        frm421.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_BILLEDITREGREFNO:
                        frmReport frm421r = new frmReport();
                        frm421r.Instance.ReportName = FormEnum;
                        frm421r.Instance.ModuleId = "";
                        frm421r.Instance.Fromdate = Fromdate;
                        frm421r.Instance.Todate = Todate;
                        frm421r.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_BILLDELETEREG:
                        frmReport frm423 = new frmReport();
                        frm423.Instance.ReportName = FormEnum;
                        frm423.Instance.ModuleId = "";
                        frm423.Instance.Fromdate = Fromdate;
                        frm423.Instance.Todate = Todate;
                        frm423.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_BILLDELETEREGREFNO:
                        frmReport frm423r = new frmReport();
                        frm423r.Instance.ReportName = FormEnum;
                        frm423r.Instance.ModuleId = "";
                        frm423r.Instance.Fromdate = Fromdate;
                        frm423r.Instance.Todate = Todate;
                        frm423r.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_ITEMWISESALES:
                        frmReport frmb9 = new frmReport();
                        frmb9.Instance.ReportName = FormEnum;
                        frmb9.Instance.ModuleId = "";
                        frmb9.Instance.Fromdate = Fromdate;
                        frmb9.Instance.Todate = Todate;
                        frmb9.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_DATEWISEBILLING:
                        frmReport frmb91 = new frmReport();
                        frmb91.Instance.ReportName = FormEnum;
                        frmb91.Instance.ModuleId = "";
                        frmb91.Instance.Fromdate = Fromdate;
                        frmb91.Instance.Todate = Todate;
                        frmb91.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_REVISEDBILLREG:
                        frmReport frmb92 = new frmReport();
                        frmb92.Instance.ReportName = FormEnum;
                        frmb92.Instance.ModuleId = "";
                        frmb92.Instance.Fromdate = Fromdate;
                        frmb92.Instance.Todate = Todate;
                        frmb92.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_REVISEDBILLREG_REFNO:
                        frmReport frmb92r = new frmReport();
                        frmb92r.Instance.ReportName = FormEnum;
                        frmb92r.Instance.ModuleId = "";
                        frmb92r.Instance.Fromdate = Fromdate;
                        frmb92r.Instance.Todate = Todate;
                        frmb92r.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_CAPCOMMIREGISTER:
                        frmReport frmb93 = new frmReport();
                        frmb93.Instance.ReportName = FormEnum;
                        frmb93.Instance.ModuleId = "";
                        frmb93.Instance.Fromdate = Fromdate;
                        frmb93.Instance.Todate = Todate;
                        frmb93.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_CAPCOMMIREGISTERREFNO:
                        frmReport frmb93r = new frmReport();
                        frmb93r.Instance.ReportName = FormEnum;
                        frmb93r.Instance.ModuleId = "";
                        frmb93r.Instance.Fromdate = Fromdate;
                        frmb93r.Instance.Todate = Todate;
                        frmb93r.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_BILLWISESALESSUMMARY:
                        frmReport frmb94 = new frmReport();
                        frmb94.Instance.ReportName = FormEnum;
                        frmb94.Instance.ModuleId = "";
                        frmb94.Instance.Fromdate = Fromdate;
                        frmb94.Instance.Todate = Todate;
                        frmb94.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_BILLWISESALESSUMMARY_REFNO:
                        frmReport frmb94r = new frmReport();
                        frmb94r.Instance.ReportName = FormEnum;
                        frmb94r.Instance.ModuleId = "";
                        frmb94r.Instance.Fromdate = Fromdate;
                        frmb94r.Instance.Todate = Todate;
                        frmb94r.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_BANQBOOKINGREG:
                        frmReport frmb95 = new frmReport();
                        frmb95.Instance.ReportName = FormEnum;
                        frmb95.Instance.ModuleId = "";
                        frmb95.Instance.Fromdate = Fromdate;
                        frmb95.Instance.Todate = Todate;
                        frmb95.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_ITEMGROUPWISESALES:
                        frmReport frmb09 = new frmReport();
                        frmb09.Instance.ReportName = FormEnum;
                        frmb09.Instance.ModuleId = "";
                        frmb09.Instance.Fromdate = Fromdate;
                        frmb09.Instance.Todate = Todate;
                        frmb09.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_VATREGISTER:
                        frmReport frmb091 = new frmReport();
                        frmb091.Instance.ReportName = FormEnum;
                        frmb091.Instance.ModuleId = "";
                        frmb091.Instance.Fromdate = Fromdate;
                        frmb091.Instance.Todate = Todate;
                        frmb091.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_REPORTDEPARTWISESALES:
                        frmReport frmb0911 = new frmReport();
                        frmb0911.Instance.ReportName = FormEnum;
                        frmb0911.Instance.ModuleId = "";
                        frmb0911.Instance.Fromdate = Fromdate;
                        frmb0911.Instance.Todate = Todate;
                        frmb0911.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_CHECKLISTITEMSTOCK:
                        frmReport frmb09111 = new frmReport();
                        frmb09111.Instance.ReportName = FormEnum;
                        frmb09111.Instance.ModuleId = "";
                        frmb09111.Instance.Fromdate = Fromdate;
                        frmb09111.Instance.Todate = Todate;
                        frmb09111.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_TABLERUNNINGSUMMARY:
                        frmReport frmb09110 = new frmReport();
                        frmb09110.Instance.ReportName = FormEnum;
                        frmb09110.Instance.ModuleId = "";
                        frmb09110.Instance.Fromdate = Fromdate;
                        frmb09110.Instance.Todate = Todate;
                        frmb09110.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_TABLERUNNINGSUMMARYREFNO:
                        frmReport frmb09110r = new frmReport();
                        frmb09110r.Instance.ReportName = FormEnum;
                        frmb09110r.Instance.ModuleId = "";
                        frmb09110r.Instance.Fromdate = Fromdate;
                        frmb09110r.Instance.Todate = Todate;
                        frmb09110r.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_PURCHASEDETAILSSUPPLIERWISE:
                        frmReport frmb091101 = new frmReport();
                        frmb091101.Instance.ReportName = FormEnum;
                        frmb091101.Instance.ModuleId = "";
                        frmb091101.Instance.Fromdate = Fromdate;
                        frmb091101.Instance.Todate = Todate;
                        frmb091101.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_PURCHASEDETAILSITEMWISE:
                        frmReport frmb091102 = new frmReport();
                        frmb091102.Instance.ReportName = FormEnum;
                        frmb091102.Instance.ModuleId = "";
                        frmb091102.Instance.Fromdate = Fromdate;
                        frmb091102.Instance.Todate = Todate;
                        frmb091102.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_DATEWISEBUSINESSINFO:
                        frmReport frmb091103 = new frmReport();
                        frmb091103.Instance.ReportName = FormEnum;
                        frmb091103.Instance.ModuleId = "";
                        frmb091103.Instance.Fromdate = Fromdate;
                        frmb091103.Instance.Todate = Todate;
                        frmb091103.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_PURCHASEBILLINFO:
                        frmReport frmpurbillinfo = new frmReport();
                        frmpurbillinfo.Instance.ReportName = FormEnum;
                        frmpurbillinfo.Instance.ModuleId = "";
                        frmpurbillinfo.Instance.Fromdate = Fromdate;
                        frmpurbillinfo.Instance.Todate = Todate;
                        frmpurbillinfo.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_PURCHASEDETAILSITEMSUMMARY:
                        frmReport frmb091104 = new frmReport();
                        frmb091104.Instance.ReportName = FormEnum;
                        frmb091104.Instance.ModuleId = "";
                        frmb091104.Instance.Fromdate = Fromdate;
                        frmb091104.Instance.Todate = Todate;
                        frmb091104.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_SETTLEMENTWISEBILLSUMMARY:
                        frmReport frmb941 = new frmReport();
                        frmb941.Instance.ReportName = FormEnum;
                        frmb941.Instance.ModuleId = "";
                        frmb941.Instance.Fromdate = Fromdate;
                        frmb941.Instance.Todate = Todate;
                        frmb941.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_SETTLEMENTWISEBILLSUMMARYREFNO:
                        frmReport frmb941r = new frmReport();
                        frmb941r.Instance.ReportName = FormEnum;
                        frmb941r.Instance.ModuleId = "";
                        frmb941r.Instance.Fromdate = Fromdate;
                        frmb941r.Instance.Todate = Todate;
                        frmb941r.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_CASHONHANDREGSITER:
                        frmReport frmb942 = new frmReport();
                        frmb942.Instance.ReportName = FormEnum;
                        frmb942.Instance.ModuleId = "";
                        frmb942.Instance.Fromdate = Fromdate;
                        frmb942.Instance.Todate = Todate;
                        frmb942.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_BILLGIVETOCUSTOMERREG:
                        frmReport frmb943 = new frmReport();
                        frmb943.Instance.ReportName = FormEnum;
                        frmb943.Instance.ModuleId = "";
                        frmb943.Instance.Fromdate = Fromdate;
                        frmb943.Instance.Todate = Todate;
                        frmb943.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_BILLGIVETOCUSTOMERREGREFNO:
                        frmReport frmb943r = new frmReport();
                        frmb943r.Instance.ReportName = FormEnum;
                        frmb943r.Instance.ModuleId = "";
                        frmb943r.Instance.Fromdate = Fromdate;
                        frmb943r.Instance.Todate = Todate;
                        frmb943r.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_ITEMRECIPEUSAGESUMMARY:
                        frmReport frmb944 = new frmReport();
                        frmb944.Instance.ReportName = FormEnum;
                        frmb944.Instance.ModuleId = "";
                        frmb944.Instance.Fromdate = Fromdate;
                        frmb944.Instance.Todate = Todate;
                        frmb944.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_ITEMRECIPEUSAGEDETAILS:
                        frmReport frmb945 = new frmReport();
                        frmb945.Instance.ReportName = FormEnum;
                        frmb945.Instance.ModuleId = "";
                        frmb945.Instance.Fromdate = Fromdate;
                        frmb945.Instance.Todate = Todate;
                        frmb945.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_ITEMWISERECIPEUSAGEDETAILS:
                        frmReport frmb946 = new frmReport();
                        frmb946.Instance.ReportName = FormEnum;
                        frmb946.Instance.ModuleId = "";
                        frmb946.Instance.Fromdate = Fromdate;
                        frmb946.Instance.Todate = Todate;
                        frmb946.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_THERMAL_ITEMWISESALESREGISTER:
                        frmReport frmb947 = new frmReport();
                        frmb947.Instance.ReportName = FormEnum;
                        frmb947.Instance.ModuleId = "";
                        frmb947.Instance.Fromdate = Fromdate;
                        frmb947.Instance.Todate = Todate;
                        frmb947.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_THERMAL_DAILYBILLREGISTER:
                        frmReport frmb948 = new frmReport();
                        frmb948.Instance.ReportName = FormEnum;
                        frmb948.Instance.ModuleId = "";
                        frmb948.Instance.Fromdate = Fromdate;
                        frmb948.Instance.Todate = Todate;
                        frmb948.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_THERMAL_DAILYBILLREGISTERREFNO:
                        frmReport frmb948r = new frmReport();
                        frmb948r.Instance.ReportName = FormEnum;
                        frmb948r.Instance.ModuleId = "";
                        frmb948r.Instance.Fromdate = Fromdate;
                        frmb948r.Instance.Todate = Todate;
                        frmb948r.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_THERMAL_DEPARTMENTWISESALESREGISTER:
                        frmReport frmb949 = new frmReport();
                        frmb949.Instance.ReportName = FormEnum;
                        frmb949.Instance.ModuleId = "";
                        frmb949.Instance.Fromdate = Fromdate;
                        frmb949.Instance.Todate = Todate;
                        frmb949.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_BILLREGWITHPARCLEDTL:
                        frmReport frmb951 = new frmReport();
                        frmb951.Instance.ReportName = FormEnum;
                        frmb951.Instance.ModuleId = "";
                        frmb951.Instance.Fromdate = Fromdate;
                        frmb951.Instance.Todate = Todate;
                        frmb951.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_BILLREGWITHPARCLEDTL_REFNO:
                        frmReport frmb951r = new frmReport();
                        frmb951r.Instance.ReportName = FormEnum;
                        frmb951r.Instance.ModuleId = "";
                        frmb951r.Instance.Fromdate = Fromdate;
                        frmb951r.Instance.Todate = Todate;
                        frmb951r.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_BILLREGDATEWISE:
                        frmReport frmb952 = new frmReport();
                        frmb952.Instance.ReportName = FormEnum;
                        frmb952.Instance.ModuleId = "";
                        frmb952.Instance.Fromdate = Fromdate;
                        frmb952.Instance.Todate = Todate;
                        frmb952.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_BILLREGDATEWISE_REFNO:
                        frmReport frmb952r = new frmReport();
                        frmb952r.Instance.ReportName = FormEnum;
                        frmb952r.Instance.ModuleId = "";
                        frmb952r.Instance.Fromdate = Fromdate;
                        frmb952r.Instance.Todate = Todate;
                        frmb952r.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_PURISSUEDTLREG:
                        frmReport frmb953 = new frmReport();
                        frmb953.Instance.ReportName = FormEnum;
                        frmb953.Instance.ModuleId = "";
                        frmb953.Instance.Fromdate = Fromdate;
                        frmb953.Instance.Todate = Todate;
                        frmb953.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_DEPTPURISSUEDTLREG:
                        frmReport frmb954 = new frmReport();
                        frmb954.Instance.ReportName = FormEnum;
                        frmb954.Instance.ModuleId = "";
                        frmb954.Instance.Fromdate = Fromdate;
                        frmb954.Instance.Todate = Todate;
                        frmb954.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_PURITEMISSUESUMMARY:
                        frmReport frmb955 = new frmReport();
                        frmb955.Instance.ReportName = FormEnum;
                        frmb955.Instance.ModuleId = "";
                        frmb955.Instance.Fromdate = Fromdate;
                        frmb955.Instance.Todate = Todate;
                        frmb955.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_PURITEMDEPTISSUESUMMARY:
                        frmReport frmb956 = new frmReport();
                        frmb956.Instance.ReportName = FormEnum;
                        frmb956.Instance.ModuleId = "";
                        frmb956.Instance.Fromdate = Fromdate;
                        frmb956.Instance.Todate = Todate;
                        frmb956.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_PURISSUESTOCKREG:
                        frmReport frmb957 = new frmReport();
                        frmb957.Instance.ReportName = FormEnum;
                        frmb957.Instance.ModuleId = "";
                        frmb957.Instance.Fromdate = Fromdate;
                        frmb957.Instance.Todate = Todate;
                        frmb957.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_THERMAL_DATEWISEBILLREG:
                        frmReport frmb958 = new frmReport();
                        frmb958.Instance.ReportName = FormEnum;
                        frmb958.Instance.ModuleId = "";
                        frmb958.Instance.Fromdate = Fromdate;
                        frmb958.Instance.Todate = Todate;
                        frmb958.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_THERMAL_COMPLYBILLREG:
                        frmReport frmb959 = new frmReport();
                        frmb959.Instance.ReportName = FormEnum;
                        frmb959.Instance.ModuleId = "";
                        frmb959.Instance.Fromdate = Fromdate;
                        frmb959.Instance.Todate = Todate;
                        frmb959.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_THERMAL_GROUPWISESALESREGISTER:
                        frmReport frmb959a = new frmReport();
                        frmb959a.Instance.ReportName = FormEnum;
                        frmb959a.Instance.ModuleId = "";
                        frmb959a.Instance.Fromdate = Fromdate;
                        frmb959a.Instance.Todate = Todate;
                        frmb959a.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_THERMAL_COMPLYBILLREGREFNO:
                        frmReport frmb959r = new frmReport();
                        frmb959r.Instance.ReportName = FormEnum;
                        frmb959r.Instance.ModuleId = "";
                        frmb959r.Instance.Fromdate = Fromdate;
                        frmb959r.Instance.Todate = Todate;
                        frmb959r.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_BILLTYPEWISEREG:
                        frmReport frmb9590 = new frmReport();
                        frmb9590.Instance.ReportName = FormEnum;
                        frmb9590.Instance.ModuleId = "";
                        frmb9590.Instance.Fromdate = Fromdate;
                        frmb9590.Instance.Todate = Todate;
                        frmb9590.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_BILLTYPEWISEREG_REFNO:
                        frmReport frmb9590r = new frmReport();
                        frmb9590r.Instance.ReportName = FormEnum;
                        frmb9590r.Instance.ModuleId = "";
                        frmb9590r.Instance.Fromdate = Fromdate;
                        frmb9590r.Instance.Todate = Todate;
                        frmb9590r.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_CASHDRAWEROPENREG:
                        frmReport frmc1 = new frmReport();
                        frmc1.Instance.ReportName = FormEnum;
                        frmc1.Instance.ModuleId = "";
                        frmc1.Instance.Fromdate = Fromdate;
                        frmc1.Instance.Todate = Todate;
                        frmc1.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_PURCHASEUSAGESUMMARY:
                        frmReport frmc2 = new frmReport();
                        frmc2.Instance.ReportName = FormEnum;
                        frmc2.Instance.ModuleId = "";
                        frmc2.Instance.Fromdate = Fromdate;
                        frmc2.Instance.Todate = Todate;
                        frmc2.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_ITEMWISEPURCHASEITEMUSED:
                        frmReport frmc3 = new frmReport();
                        frmc3.Instance.ReportName = FormEnum;
                        frmc3.Instance.ModuleId = "";
                        frmc3.Instance.Fromdate = Fromdate;
                        frmc3.Instance.Todate = Todate;
                        frmc3.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_BANQBILLINGINFOREG:
                        frmReport frmc4 = new frmReport();
                        frmc4.Instance.ReportName = FormEnum;
                        frmc4.Instance.ModuleId = "";
                        frmc4.Instance.Fromdate = Fromdate;
                        frmc4.Instance.Todate = Todate;
                        frmc4.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_DAYWISEPAXSUMMARY:
                        frmReport frmc5 = new frmReport();
                        frmc5.Instance.ReportName = FormEnum;
                        frmc5.Instance.ModuleId = "";
                        frmc5.Instance.Fromdate = Fromdate;
                        frmc5.Instance.Todate = Todate;
                        frmc5.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_DAYWISEPAXSUMMARYREFNO:
                        frmReport frmc5r = new frmReport();
                        frmc5r.Instance.ReportName = FormEnum;
                        frmc5r.Instance.ModuleId = "";
                        frmc5r.Instance.Fromdate = Fromdate;
                        frmc5r.Instance.Todate = Todate;
                        frmc5r.Instance.ShowDialog();
                        break;
                    case enumRMSForms.POS_ITEMPURRATEWISEREG:
                        frmReport frmc6 = new frmReport();
                        frmc6.Instance.ReportName = FormEnum;
                        frmc6.Instance.ModuleId = "";
                        frmc6.Instance.Fromdate = Fromdate;
                        frmc6.Instance.Todate = Todate;
                        frmc6.Instance.ShowDialog();
                        break;
                    case enumRMSForms.POS_ITEMWISEBILLREG:
                        frmReport frmc7 = new frmReport();
                        frmc7.Instance.ReportName = FormEnum;
                        frmc7.Instance.ModuleId = "";
                        frmc7.Instance.Fromdate = Fromdate;
                        frmc7.Instance.Todate = Todate;
                        frmc7.Instance.ShowDialog();
                        break;
                    case enumRMSForms.POS_ITEMGROUPPWISEREG:
                        frmReport frmc8 = new frmReport();
                        frmc8.Instance.ReportName = FormEnum;
                        frmc8.Instance.ModuleId = "";
                        frmc8.Instance.Fromdate = Fromdate;
                        frmc8.Instance.Todate = Todate;
                        frmc8.Instance.ShowDialog();
                        break;
                    case enumRMSForms.POS_BILLREMARKREG:
                        frmReport frmc9 = new frmReport();
                        frmc9.Instance.ReportName = FormEnum;
                        frmc9.Instance.ModuleId = "";
                        frmc9.Instance.Fromdate = Fromdate;
                        frmc9.Instance.Todate = Todate;
                        frmc9.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_KOTREMARKREG:
                        frmReport frmc10 = new frmReport();
                        frmc10.Instance.ReportName = FormEnum;
                        frmc10.Instance.ModuleId = "";
                        frmc10.Instance.Fromdate = Fromdate;
                        frmc10.Instance.Todate = Todate;
                        frmc10.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_BILLREMARKREG:
                        frmReport frmc11 = new frmReport();
                        frmc11.Instance.ReportName = FormEnum;
                        frmc11.Instance.ModuleId = "";
                        frmc11.Instance.Fromdate = Fromdate;
                        frmc11.Instance.Todate = Todate;
                        frmc11.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_BILLREMARKREGREFNO:
                        frmReport frmc11r = new frmReport();
                        frmc11r.Instance.ReportName = FormEnum;
                        frmc11r.Instance.ModuleId = "";
                        frmc11r.Instance.Fromdate = Fromdate;
                        frmc11r.Instance.Todate = Todate;
                        frmc11r.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_BQBOOKINGINFOREG:
                        frmReport frmc12 = new frmReport();
                        frmc12.Instance.ReportName = FormEnum;
                        frmc12.Instance.ModuleId = "";
                        frmc12.Instance.Fromdate = Fromdate;
                        frmc12.Instance.Todate = Todate;
                        frmc12.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_BQINQUIRYINFOREG:
                        frmReport frmc13 = new frmReport();
                        frmc13.Instance.ReportName = FormEnum;
                        frmc13.Instance.ModuleId = "";
                        frmc13.Instance.Fromdate = Fromdate;
                        frmc13.Instance.Todate = Todate;
                        frmc13.Instance.ShowDialog();
                        break;
                    case enumRMSForms.POS_ITEMGROUPWISESALES:
                        frmReport frmc14 = new frmReport();
                        frmc14.Instance.ReportName = FormEnum;
                        frmc14.Instance.ModuleId = "";
                        frmc14.Instance.Fromdate = Fromdate;
                        frmc14.Instance.Todate = Todate;
                        frmc14.Instance.ShowDialog();
                        break;
                    case enumRMSForms.POS_STOCKREGISTER:
                        frmReport frmc15 = new frmReport();
                        frmc15.Instance.ReportName = FormEnum;
                        frmc15.Instance.ModuleId = "";
                        frmc15.Instance.Fromdate = Fromdate;
                        frmc15.Instance.Todate = Todate;
                        frmc15.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_REFBYBILLINFORMATION:
                        frmReport frmc16 = new frmReport();
                        frmc16.Instance.ReportName = FormEnum;
                        frmc16.Instance.ModuleId = "";
                        frmc16.Instance.Fromdate = Fromdate;
                        frmc16.Instance.Todate = Todate;
                        frmc16.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_REFBYBILLINFORMATION_REFNO:
                        frmReport frmc16r = new frmReport();
                        frmc16r.Instance.ReportName = FormEnum;
                        frmc16r.Instance.ModuleId = "";
                        frmc16r.Instance.Fromdate = Fromdate;
                        frmc16r.Instance.Todate = Todate;
                        frmc16r.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_KOTEDITDELETEREG:
                        frmReport frmc17 = new frmReport();
                        frmc17.Instance.ReportName = FormEnum;
                        frmc17.Instance.ModuleId = "";
                        frmc17.Instance.Fromdate = Fromdate;
                        frmc17.Instance.Todate = Todate;
                        frmc17.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_BILLEDITDELETEREG:
                        frmReport frmc18 = new frmReport();
                        frmc18.Instance.ReportName = FormEnum;
                        frmc18.Instance.ModuleId = "";
                        frmc18.Instance.Fromdate = Fromdate;
                        frmc18.Instance.Todate = Todate;
                        frmc18.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_BILLEDITDELETEREGREFNO:
                        frmReport frmc18r = new frmReport();
                        frmc18r.Instance.ReportName = FormEnum;
                        frmc18r.Instance.ModuleId = "";
                        frmc18r.Instance.Fromdate = Fromdate;
                        frmc18r.Instance.Todate = Todate;
                        frmc18r.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_KOTMACHWITHBILLREG:
                        frmReport frmc19 = new frmReport();
                        frmc19.Instance.ReportName = FormEnum;
                        frmc19.Instance.ModuleId = "";
                        frmc19.Instance.Fromdate = Fromdate;
                        frmc19.Instance.Todate = Todate;
                        frmc19.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_KOTMACHWITHBILLREGREFNO:
                        frmReport frmc19r = new frmReport();
                        frmc19r.Instance.ReportName = FormEnum;
                        frmc19r.Instance.ModuleId = "";
                        frmc19r.Instance.Fromdate = Fromdate;
                        frmc19r.Instance.Todate = Todate;
                        frmc19r.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_BILLTIMEWISESALESINFO:
                        frmReport frmc20 = new frmReport();
                        frmc20.Instance.ReportName = FormEnum;
                        frmc20.Instance.ModuleId = "";
                        frmc20.Instance.Fromdate = Fromdate;
                        frmc20.Instance.Todate = Todate;
                        frmc20.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_BILLTIMEWISESALESINFO_REFNO:
                        frmReport frmc20r = new frmReport();
                        frmc20r.Instance.ReportName = FormEnum;
                        frmc20r.Instance.ModuleId = "";
                        frmc20r.Instance.Fromdate = Fromdate;
                        frmc20r.Instance.Todate = Todate;
                        frmc20r.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_TIEUPCOMPANYBILLINFO:
                        frmReport frmc21 = new frmReport();
                        frmc21.Instance.ReportName = FormEnum;
                        frmc21.Instance.ModuleId = "";
                        frmc21.Instance.Fromdate = Fromdate;
                        frmc21.Instance.Todate = Todate;
                        frmc21.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_TIEUPCOMPANYBILLINFOREFNO:
                        frmReport frmc21r = new frmReport();
                        frmc21r.Instance.ReportName = FormEnum;
                        frmc21r.Instance.ModuleId = "";
                        frmc21r.Instance.Fromdate = Fromdate;
                        frmc21r.Instance.Todate = Todate;
                        frmc21r.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_BILLWISEDETAILSUMMARY:
                        frmReport frmc22 = new frmReport();
                        frmc22.Instance.ReportName = FormEnum;
                        frmc22.Instance.ModuleId = "";
                        frmc22.Instance.Fromdate = Fromdate;
                        frmc22.Instance.Todate = Todate;
                        frmc22.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_BILLWISEDETAILSUMMARY_REFNO:
                        frmReport frmc22r = new frmReport();
                        frmc22r.Instance.ReportName = FormEnum;
                        frmc22r.Instance.ModuleId = "";
                        frmc22r.Instance.Fromdate = Fromdate;
                        frmc22r.Instance.Todate = Todate;
                        frmc22r.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_OUTPUTVATREPORT:
                        frmReport frmc23 = new frmReport();
                        frmc23.Instance.ReportName = FormEnum;
                        frmc23.Instance.ModuleId = "";
                        frmc23.Instance.Fromdate = Fromdate;
                        frmc23.Instance.Todate = Todate;
                        frmc23.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_PURCHASESTOCKREG:
                        frmReport frmc24 = new frmReport();
                        frmc24.Instance.ReportName = FormEnum;
                        frmc24.Instance.ModuleId = "";
                        frmc24.Instance.Fromdate = Fromdate;
                        frmc24.Instance.Todate = Todate;
                        frmc24.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_PURCHASEITEMGROUPSTOCKREG:
                        frmReport frmc24a = new frmReport();
                        frmc24a.Instance.ReportName = FormEnum;
                        frmc24a.Instance.ModuleId = "";
                        frmc24a.Instance.Fromdate = Fromdate;
                        frmc24a.Instance.Todate = Todate;
                        frmc24a.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_SETTLEMENTOTHER:
                        frmReport frm6 = new frmReport();
                        frm6.Instance.ReportName = FormEnum;
                        frm6.Instance.ModuleId = "";
                        frm6.Instance.Fromdate = Fromdate;
                        frm6.Instance.Todate = Todate;
                        frm6.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_SETTLEMENTOTHERREFNO:
                        frmReport frm6r = new frmReport();
                        frm6r.Instance.ReportName = FormEnum;
                        frm6r.Instance.ModuleId = "";
                        frm6r.Instance.Fromdate = Fromdate;
                        frm6r.Instance.Todate = Todate;
                        frm6r.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_INCOMEEXPENCESUMMARYRPT:
                        frmReport frm7 = new frmReport();
                        frm7.Instance.ReportName = FormEnum;
                        frm7.Instance.ModuleId = "";
                        frm7.Instance.Fromdate = Fromdate;
                        frm7.Instance.Todate = Todate;
                        frm7.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_TABLEWISESALESREPORT:
                        frmReport frm8 = new frmReport();
                        frm8.Instance.ReportName = FormEnum;
                        frm8.Instance.ModuleId = "";
                        frm8.Instance.Fromdate = Fromdate;
                        frm8.Instance.Todate = Todate;
                        frm8.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_BILLCUSTOMERWISE:
                        frmReport frm9 = new frmReport();
                        frm9.Instance.ReportName = FormEnum;
                        frm9.Instance.ModuleId = "";
                        frm9.Instance.Fromdate = Fromdate;
                        frm9.Instance.Todate = Todate;
                        frm9.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_BILLCUSTOMERWISEREFNO:
                        frmReport frm10 = new frmReport();
                        frm10.Instance.ReportName = FormEnum;
                        frm10.Instance.ModuleId = "";
                        frm10.Instance.Fromdate = Fromdate;
                        frm10.Instance.Todate = Todate;
                        frm10.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_GSTREPORT:
                        frmReport frm11 = new frmReport();
                        frm11.Instance.ReportName = FormEnum;
                        frm11.Instance.ModuleId = "";
                        frm11.Instance.Fromdate = Fromdate;
                        frm11.Instance.Todate = Todate;
                        frm11.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_GSTREPORT_REFNO:
                        frmReport frm11a = new frmReport();
                        frm11a.Instance.ReportName = FormEnum;
                        frm11a.Instance.ModuleId = "";
                        frm11a.Instance.Fromdate = Fromdate;
                        frm11a.Instance.Todate = Todate;
                        frm11a.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_SUPPLIERWISEPAYMENTDETAIL:
                        frmReport frm12 = new frmReport();
                        frm12.Instance.ReportName = FormEnum;
                        frm12.Instance.ModuleId = "";
                        frm12.Instance.Fromdate = Fromdate;
                        frm12.Instance.Todate = Todate;
                        frm12.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_DATEWISEBILLSUMMARY:
                        frmReport frm13 = new frmReport();
                        frm13.Instance.ReportName = FormEnum;
                        frm13.Instance.ModuleId = "";
                        frm13.Instance.Fromdate = Fromdate;
                        frm13.Instance.Todate = Todate;
                        frm13.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_DATEWISEBILLSUMMARY_REFNO:
                        frmReport frm14 = new frmReport();
                        frm14.Instance.ReportName = FormEnum;
                        frm14.Instance.ModuleId = "";
                        frm14.Instance.Fromdate = Fromdate;
                        frm14.Instance.Todate = Todate;
                        frm14.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_BILLREGWITHSETTINFO_REFNO:
                        frmReport frm15 = new frmReport();
                        frm15.Instance.ReportName = FormEnum;
                        frm15.Instance.ModuleId = "";
                        frm15.Instance.Fromdate = Fromdate;
                        frm15.Instance.Todate = Todate;
                        frm15.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_BILLREGWITHSETTINFO:
                        frmReport frm16 = new frmReport();
                        frm16.Instance.ReportName = FormEnum;
                        frm16.Instance.ModuleId = "";
                        frm16.Instance.Fromdate = Fromdate;
                        frm16.Instance.Todate = Todate;
                        frm16.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_SALESSUMMARY:
                        frmReport frm17 = new frmReport();
                        frm17.Instance.ReportName = FormEnum;
                        frm17.Instance.ModuleId = "";
                        frm17.Instance.Fromdate = Fromdate;
                        frm17.Instance.Todate = Todate;
                        frm17.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_SALESSUMMARYREFNO:
                        frmReport frm18 = new frmReport();
                        frm18.Instance.ReportName = FormEnum;
                        frm18.Instance.ModuleId = "";
                        frm18.Instance.Fromdate = Fromdate;
                        frm18.Instance.Todate = Todate;
                        frm18.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_COMPLEMENTRYKOTREG:
                        frmReport frm19 = new frmReport();
                        frm19.Instance.ReportName = FormEnum;
                        frm19.Instance.ModuleId = "";
                        frm19.Instance.Fromdate = Fromdate;
                        frm19.Instance.Todate = Todate;
                        frm19.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_COMPLEMENTRYKOTREGREFNO:
                        frmReport frm20 = new frmReport();
                        frm20.Instance.ReportName = FormEnum;
                        frm20.Instance.ModuleId = "";
                        frm20.Instance.Fromdate = Fromdate;
                        frm20.Instance.Todate = Todate;
                        frm20.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_MESSCUSTOMERPOSITIONINFORMATION:
                        frmReport frm11b = new frmReport();
                        frm11b.Instance.ReportName = FormEnum;
                        frm11b.Instance.ModuleId = "";
                        frm11b.Instance.Fromdate = Fromdate;
                        frm11b.Instance.Todate = Todate;
                        frm11b.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_PURISTOCKFORMAT:
                        frmReport frmb957a = new frmReport();
                        frmb957a.Instance.ReportName = FormEnum;
                        frmb957a.Instance.ModuleId = "";
                        frmb957a.Instance.Fromdate = Fromdate;
                        frmb957a.Instance.Todate = Todate;
                        frmb957a.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_GSTPERWISESUMMARY:
                        frmReport frm12a = new frmReport();
                        frm12a.Instance.ReportName = FormEnum;
                        frm12a.Instance.ModuleId = "";
                        frm12a.Instance.Fromdate = Fromdate;
                        frm12a.Instance.Todate = Todate;
                        frm12a.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_GSTPERWISESUMMARY_REFNO:
                        frmReport frm12b = new frmReport();
                        frm12b.Instance.ReportName = FormEnum;
                        frm12b.Instance.ModuleId = "";
                        frm12b.Instance.Fromdate = Fromdate;
                        frm12b.Instance.Todate = Todate;
                        frm12b.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_DATEWISESETTSUMMARY:
                        frmReport frm21 = new frmReport();
                        frm21.Instance.ReportName = FormEnum;
                        frm21.Instance.ModuleId = "";
                        frm21.Instance.Fromdate = Fromdate;
                        frm21.Instance.Todate = Todate;
                        frm21.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_TIEUPCOMPANYWISESUMMARY:
                        frmReport frm22 = new frmReport();
                        frm22.Instance.ReportName = FormEnum;
                        frm22.Instance.ModuleId = "";
                        frm22.Instance.Fromdate = Fromdate;
                        frm22.Instance.Todate = Todate;
                        frm22.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_TIEUPCOMPANYWISESUMMARYREFNO:
                        frmReport frm23 = new frmReport();
                        frm23.Instance.ReportName = FormEnum;
                        frm23.Instance.ModuleId = "";
                        frm23.Instance.Fromdate = Fromdate;
                        frm23.Instance.Todate = Todate;
                        frm23.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_DAILYDETAILSBILLINFO:
                        frmReport frm24 = new frmReport();
                        frm24.Instance.ReportName = FormEnum;
                        frm24.Instance.ModuleId = "";
                        frm24.Instance.Fromdate = Fromdate;
                        frm24.Instance.Todate = Todate;
                        frm24.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_DAILYDETAILSBILLINFOREFNO:
                        frmReport frm25 = new frmReport();
                        frm25.Instance.ReportName = FormEnum;
                        frm25.Instance.ModuleId = "";
                        frm25.Instance.Fromdate = Fromdate;
                        frm25.Instance.Todate = Todate;
                        frm25.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_TIEUPCOMPANYWISEDETAILREFNO:
                        frmReport frm24a = new frmReport();
                        frm24a.Instance.ReportName = FormEnum;
                        frm24a.Instance.ModuleId = "";
                        frm24a.Instance.Fromdate = Fromdate;
                        frm24a.Instance.Todate = Todate;
                        frm24a.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_TIEUPCOMPANYWISEDETAIL:
                        frmReport frm24b = new frmReport();
                        frm24b.Instance.ReportName = FormEnum;
                        frm24b.Instance.ModuleId = "";
                        frm24b.Instance.Fromdate = Fromdate;
                        frm24b.Instance.Todate = Todate;
                        frm24b.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_HOMEDELIVERYREPORT:
                        frmReport frm24c = new frmReport();
                        frm24c.Instance.ReportName = FormEnum;
                        frm24c.Instance.ModuleId = "";
                        frm24c.Instance.Fromdate = Fromdate;
                        frm24c.Instance.Todate = Todate;
                        frm24c.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_HOMEDELIVERYREPORTREFNO:
                        frmReport frm24d = new frmReport();
                        frm24d.Instance.ReportName = FormEnum;
                        frm24d.Instance.ModuleId = "";
                        frm24d.Instance.Fromdate = Fromdate;
                        frm24d.Instance.Todate = Todate;
                        frm24d.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_THERMALBILLWISESETTREFNO:
                        frmReport frm24e = new frmReport();
                        frm24e.Instance.ReportName = FormEnum;
                        frm24e.Instance.ModuleId = "";
                        frm24e.Instance.Fromdate = Fromdate;
                        frm24e.Instance.Todate = Todate;
                        frm24e.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_THERMALBILLWISESETT:
                        frmReport frm24f = new frmReport();
                        frm24f.Instance.ReportName = FormEnum;
                        frm24f.Instance.ModuleId = "";
                        frm24f.Instance.Fromdate = Fromdate;
                        frm24f.Instance.Todate = Todate;
                        frm24f.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_GSTDATEANDPERWISEREPORT:
                        frmReport frm24g = new frmReport();
                        frm24g.Instance.ReportName = FormEnum;
                        frm24g.Instance.ModuleId = "";
                        frm24g.Instance.Fromdate = Fromdate;
                        frm24g.Instance.Todate = Todate;
                        frm24g.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_PURITEMSTOCKDATEWISE:
                        frmReport frm24h = new frmReport();
                        frm24h.Instance.ReportName = FormEnum;
                        frm24h.Instance.ModuleId = "";
                        frm24h.Instance.Fromdate = Fromdate;
                        frm24h.Instance.Todate = Todate;
                        frm24h.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_ITEMPROFITREPORT:
                        frmReport frm24i = new frmReport();
                        frm24i.Instance.ReportName = FormEnum;
                        frm24i.Instance.ModuleId = "";
                        frm24i.Instance.Fromdate = Fromdate;
                        frm24i.Instance.Todate = Todate;
                        frm24i.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_INCOMEDTLREPORT:
                        frmReport frm24j = new frmReport();
                        frm24j.Instance.ReportName = FormEnum;
                        frm24j.Instance.ModuleId = "";
                        frm24j.Instance.Fromdate = Fromdate;
                        frm24j.Instance.Todate = Todate;
                        frm24j.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_EXPENCEDTLREPORT:
                        frmReport frm24k = new frmReport();
                        frm24k.Instance.ReportName = FormEnum;
                        frm24k.Instance.ModuleId = "";
                        frm24k.Instance.Fromdate = Fromdate;
                        frm24k.Instance.Todate = Todate;
                        frm24k.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_DATEWISESALARYREPORT:
                        frmReport frm24l = new frmReport();
                        frm24l.Instance.ReportName = FormEnum;
                        frm24l.Instance.ModuleId = "";
                        frm24l.Instance.Fromdate = Fromdate;
                        frm24l.Instance.Todate = Todate;
                        frm24l.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_DATEWISEATTENDANCEREPORT:
                        frmReport frm24m = new frmReport();
                        frm24m.Instance.ReportName = FormEnum;
                        frm24m.Instance.ModuleId = "";
                        frm24m.Instance.Fromdate = Fromdate;
                        frm24m.Instance.Todate = Todate;
                        frm24m.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_BANQPAYMENTREG:
                        frmReport frm24n = new frmReport();
                        frm24n.Instance.ReportName = FormEnum;
                        frm24n.Instance.ModuleId = "";
                        frm24n.Instance.Fromdate = Fromdate;
                        frm24n.Instance.Todate = Todate;
                        frm24n.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_ENTRYTICKETDETAILCOLLECTIONRPT:
                        frmReport frm24o = new frmReport();
                        frm24o.Instance.ReportName = FormEnum;
                        frm24o.Instance.ModuleId = "";
                        frm24o.Instance.Fromdate = Fromdate;
                        frm24o.Instance.Todate = Todate;
                        frm24o.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_ENTRYTICKETSUMMARYCOLLECTIONRPT:
                        frmReport frm24p = new frmReport();
                        frm24p.Instance.ReportName = FormEnum;
                        frm24p.Instance.ModuleId = "";
                        frm24p.Instance.Fromdate = Fromdate;
                        frm24p.Instance.Todate = Todate;
                        frm24p.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_COSTUMERENTDETAILREPORT:
                        frmReport frm24q = new frmReport();
                        frm24q.Instance.ReportName = FormEnum;
                        frm24q.Instance.ModuleId = "";
                        frm24q.Instance.Fromdate = Fromdate;
                        frm24q.Instance.Todate = Todate;
                        frm24q.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_DATEWISEBELTTRANSACTION:
                        frmReport frm24r = new frmReport();
                        frm24r.Instance.ReportName = FormEnum;
                        frm24r.Instance.ModuleId = "";
                        frm24r.Instance.Fromdate = Fromdate;
                        frm24r.Instance.Todate = Todate;
                        frm24r.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_BELTISSUEREGISTER:
                        frmReport frm24s = new frmReport();
                        frm24s.Instance.ReportName = FormEnum;
                        frm24s.Instance.ModuleId = "";
                        frm24s.Instance.Fromdate = Fromdate;
                        frm24s.Instance.Todate = Todate;
                        frm24s.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_BELTRECHARGEREGISTER:
                        frmReport frm24t = new frmReport();
                        frm24t.Instance.ReportName = FormEnum;
                        frm24t.Instance.ModuleId = "";
                        frm24t.Instance.Fromdate = Fromdate;
                        frm24t.Instance.Todate = Todate;
                        frm24t.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_BELTSUBMITREGISTER:
                        frmReport frm24u = new frmReport();
                        frm24u.Instance.ReportName = FormEnum;
                        frm24u.Instance.ModuleId = "";
                        frm24u.Instance.Fromdate = Fromdate;
                        frm24u.Instance.Todate = Todate;
                        frm24u.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_COSTUMEISSUEITEMWISEREGISTER   :
                        frmReport frm24v = new frmReport();
                        frm24v.Instance.ReportName = FormEnum;
                        frm24v.Instance.ModuleId = "";
                        frm24v.Instance.Fromdate = Fromdate;
                        frm24v.Instance.Todate = Todate;
                        frm24v.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_COUPONBILLSUMMARYCOLLECTIONRPT:
                        frmReport frm24w = new frmReport();
                        frm24w.Instance.ReportName = FormEnum;
                        frm24w.Instance.ModuleId = "";
                        frm24w.Instance.Fromdate = Fromdate;
                        frm24w.Instance.Todate = Todate;
                        frm24w.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_COSTUMERETURNREGISTER:
                        frmReport frm24x = new frmReport();
                        frm24x.Instance.ReportName = FormEnum;
                        frm24x.Instance.ModuleId = "";
                        frm24x.Instance.Fromdate = Fromdate;
                        frm24x.Instance.Todate = Todate;
                        frm24x.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_ENTRYTICKETPAYMENTCOLLECTIONRPT:
                        frmReport frm24y = new frmReport();
                        frm24y.Instance.ReportName = FormEnum;
                        frm24y.Instance.ModuleId = "";
                        frm24y.Instance.Fromdate = Fromdate;
                        frm24y.Instance.Todate = Todate;
                        frm24y.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_SUPPLIERWISEPAYMENTREGISTER:
                        frmReport frm24z = new frmReport();
                        frm24z.Instance.ReportName = FormEnum;
                        frm24z.Instance.ModuleId = "";
                        frm24z.Instance.Fromdate = Fromdate;
                        frm24z.Instance.Todate = Todate;
                        frm24z.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_CUSTOMERWISESETTLEMENTREFNO:
                        frmReport frm25a = new frmReport();
                        frm25a.Instance.ReportName = FormEnum;
                        frm25a.Instance.ModuleId = "";
                        frm25a.Instance.Fromdate = Fromdate;
                        frm25a.Instance.Todate = Todate;
                        frm25a.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_CUSTOMERWISESETTLEMENT:
                        frmReport frm25b = new frmReport();
                        frm25b.Instance.ReportName = FormEnum;
                        frm25b.Instance.ModuleId = "";
                        frm25b.Instance.Fromdate = Fromdate;
                        frm25b.Instance.Todate = Todate;
                        frm25b.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_THERMALUSERWISEBUSINESSSUMMARY:
                        frmReport frm25c = new frmReport();
                        frm25c.Instance.ReportName = FormEnum;
                        frm25c.Instance.ModuleId = "";
                        frm25c.Instance.Fromdate = Fromdate;
                        frm25c.Instance.Todate = Todate;
                        frm25c.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_CHECKLISTITEMDETAILS:
                        frmReport frm25d = new frmReport();
                        frm25d.Instance.ReportName = FormEnum;
                        frm25d.Instance.ModuleId = "";
                        frm25d.Instance.Fromdate = Fromdate;
                        frm25d.Instance.Todate = Todate;
                        frm25d.Instance.ShowDialog();
                        break;
                    case enumRMSForms.RMS_SALARYSLIPDETAILS:
                        frmReport frm25e = new frmReport();
                        frm25e.Instance.ReportName = FormEnum;
                        frm25e.Instance.ModuleId = "";
                        frm25e.Instance.Fromdate = Fromdate;
                        frm25e.Instance.Todate = Todate;
                        frm25e.Instance.ShowDialog();
                        break;  

                    default:
                        break;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString() + " Error occures in OpenFrom_To_Date_Parameter_Reportform())", clsPublicVariables.Project_Title, MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

        }

        public void DirectPrintRMSReport(string FormName_1, string ModuleId_1)
        {
            ReportDocument reportdocument = new ReportDocument();
            frmImage frmimg = new frmImage();
            frmReport frmRpt = new frmReport();
            string ReportFilePath;
            string formulastr = "";

            try
            {
                ReportFilePath = "";
                clsPublicVariables.enumRMSForms FormEnum = (clsPublicVariables.enumRMSForms)Enum.Parse(typeof(clsPublicVariables.enumRMSForms), FormName_1, true);

                this.GetConnectionDetails();
                this.GetSettingDetails();

                switch (FormEnum)
                {
                    case clsPublicVariables.enumRMSForms.RMS_KOT:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\Kot.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_KOT2:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\Kot2.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_KOTB:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\Kotb.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILL:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\Bill.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLB:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\Billb.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.POS_BILL:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\PosBill.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BANQBOOKING:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\Banqbooking.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_CASHMEMOKOT:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\CashMemoKOT.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_CASHMEMOKOT2:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\CashMemoKOT2.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_CASHMEMOTOKEN:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\CashMemoToken.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_CASHMEMOSTICKER:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\CashMemoSticker.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BANQBILLINGINFO:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BanqBilling.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BANQINQUIRY:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BanqInquiry.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BQBOOKING:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BqBooking.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_ITEMPURCHASE:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\ItemPurchase.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_STOCKISSUE:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\Stockissue.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_CASHONHAND:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\CashOnHand.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_PAYMENTINFO:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\PaymentVoucher.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_ITEMWISEPURCHASE:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\ItemRecipe.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.POS_40BARCODELABELA4:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\40BARCODELABELA4.rpt";
                        reportdocument.Refresh();
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_40BARCODELABELA4:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\40BARCODELABELA4.rpt";
                        reportdocument.Refresh();
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_PURREQORDER:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\PurchaseOrderReq.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_TABLEWAITING:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\Tablewaiting.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_TABLERESERVATION:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\TableReservation.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_ENTRYTICKET:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\EntryTicket.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_COSTUMEBILL:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\CouponBill.rpt";
                        break;
                    default:
                        break;
                }

                reportdocument.PrintOptions.PrinterName = RptPrintername;

                if (FormEnum == clsPublicVariables.enumRMSForms.RMS_BILL)
                {
                    reportdocument.PrintOptions.PrinterName = RptBillPrintername;
                }
                else if (FormEnum == clsPublicVariables.enumRMSForms.RMS_KOT)
                {
                    reportdocument.PrintOptions.PrinterName = RptKotPrintername;
                }
                else if (FormEnum == clsPublicVariables.enumRMSForms.RMS_KOT2)
                {
                    reportdocument.PrintOptions.PrinterName = RptKot2Printername;
                }
                else if (FormEnum == clsPublicVariables.enumRMSForms.RMS_CASHMEMOKOT)
                {
                    reportdocument.PrintOptions.PrinterName = RptKotPrintername;
                }
                else if (FormEnum == clsPublicVariables.enumRMSForms.RMS_CASHMEMOKOT2)
                {
                    reportdocument.PrintOptions.PrinterName = RptKot2Printername;
                }
                else if (FormEnum == clsPublicVariables.enumRMSForms.RMS_CASHMEMOTOKEN)
                {
                    reportdocument.PrintOptions.PrinterName = RptBillPrintername;
                }
                else if (FormEnum == clsPublicVariables.enumRMSForms.RMS_CASHMEMOSTICKER)
                {
                    reportdocument.PrintOptions.PrinterName = RptlabelPrintername;
                }
                else if (FormEnum == clsPublicVariables.enumRMSForms.RMS_ENTRYTICKET)
                {
                    reportdocument.PrintOptions.PrinterName = GENTICKETPRINTER;
                }
                else if (FormEnum == clsPublicVariables.enumRMSForms.RMS_COSTUMEBILL)
                {
                    reportdocument.PrintOptions.PrinterName = GENCOSTUMEPRINTER;
                }

                reportdocument.Load(ReportFilePath, OpenReportMethod.OpenReportByDefault);
                reportdocument.SetDatabaseLogon(clsPublicVariables.UserName1, clsPublicVariables.Password1, clsPublicVariables.ServerName1, clsPublicVariables.DatabaseName1);
                this.AssignParameterToReport(reportdocument);

                formulastr = "";
                // Assign Formula String 
                switch (FormEnum)
                {
                    case clsPublicVariables.enumRMSForms.RMS_BILL:
                        formulastr = " {BILL.RId} = [" + ModuleId_1 + "] and {BILLDTL.DELFLG}=False";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLB:
                        formulastr = " {BILLb.RId} = [" + ModuleId_1 + "] and {BILLBDTL.DELFLG}=False";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_KOT:
                        formulastr = " {KOT.RID} = [" + ModuleId_1 + "] and {KOTDTL.DELFLG}=False";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_KOT2:
                        formulastr = " {KOT.RID} = [" + ModuleId_1 + "] and {KOTDTL.DELFLG}=False";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_KOTB:
                        formulastr = " {KOTb.RID} = [" + ModuleId_1 + "]and {KOTBDTL.DELFLG}=False";
                        break;
                    case clsPublicVariables.enumRMSForms.POS_BILL:
                        formulastr = " {BILL.RId} = [" + ModuleId_1 + "] and {BILLDTL.DELFLG}=False";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BANQBOOKING:
                        formulastr = " {BANQBOOKING.RId} = [" + ModuleId_1 + "] and {BANQBOOKINGDTL.DELFLG}=False";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_CASHMEMOKOT:
                        formulastr = " {BILL.RId} = [" + ModuleId_1 + "] and {BILLDTL.DELFLG}=False";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_CASHMEMOKOT2:
                        formulastr = " {BILL.RId} = [" + ModuleId_1 + "] and {BILLDTL.DELFLG}=False";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_CASHMEMOSTICKER:
                        formulastr = " {TEMPBILLREMARK.BILLRId} = [" + ModuleId_1 + "]";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_CASHMEMOTOKEN:
                        formulastr = " {BILL.RId} = [" + ModuleId_1 + "] and {BILLDTL.DELFLG}=False";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BANQBILLINGINFO:
                        formulastr = " {BANQBILLINFO.RId} = [" + ModuleId_1 + "] and {BANQBILLINFODETAIL.DELFLG}=False";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BQBOOKING:
                        formulastr = " {BQBOOKING.RId} = [" + ModuleId_1 + "] and {BQBOOKINGDTL.DELFLG}=False";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_ITEMPURCHASE:
                        formulastr = " {ITEMPURCHASE.RId} = [" + ModuleId_1 + "] and {ITEMPURCHASEDTL.DELFLG}=False";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_STOCKISSUE:
                        formulastr = " {STOCKISSUE.RId} = [" + ModuleId_1 + "] and {STOCKISSUEDTL.DELFLG}=False";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_CASHONHAND:
                        formulastr = " {CASHONHAND.RId} = [" + ModuleId_1 + "] and {CASHONHAND.DELFLG}=False";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_PAYMENTINFO:
                        formulastr = " {PAYMENTINFO.RId} = [" + ModuleId_1 + "] and {PAYMENTINFO.DELFLG}=0";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_ITEMWISEPURCHASE:
                        formulastr = " {ITEMWISEPURCHASE.RId} = [" + ModuleId_1 + "] and {ITEMWISEPURCHASE.DELFLG}=False";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_PURREQORDER:
                        formulastr = " {PURREQORDER.RId} = [" + ModuleId_1 + "] and {PURREQORDERDTL.DELFLG}=False";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_TABLEWAITING:
                        formulastr = " {TBLWAIT.RId} = [" + ModuleId_1 + "] and {TBLWAIT.DELFLG}=False";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_TABLERESERVATION:
                        formulastr = " {TABLERESERVATION.RId} = [" + ModuleId_1 + "] and {TABLERESERVATION.DELFLG}=False";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_ENTRYTICKET:
                        formulastr = " {ENTRYTICKET.RId} = [" + ModuleId_1 + "] and {ENTRYTICKET.DELFLG}=False";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_COSTUMEBILL:
                        formulastr = " {COUPONBILL.RId} = [" + ModuleId_1 + "] and {COUPONBILL.DELFLG}=False";
                        break;
                    default:
                        break;
                }

                Database crDatabase;
                Tables crTables;
                TableLogOnInfo crTableLogOnInfo;
                ConnectionInfo crConnectionInfo;

                crConnectionInfo = new ConnectionInfo();
                crConnectionInfo.ServerName = clsPublicVariables.ServerName1;
                crConnectionInfo.DatabaseName = clsPublicVariables.DatabaseName1;
                crConnectionInfo.UserID = clsPublicVariables.UserName1;
                crConnectionInfo.Password = clsPublicVariables.Password1;

                crDatabase = reportdocument.Database;
                crTables = crDatabase.Tables;

                foreach (CrystalDecisions.CrystalReports.Engine.Table crTable in crTables)
                {
                    crTableLogOnInfo = crTable.LogOnInfo;
                    crTableLogOnInfo.ConnectionInfo = crConnectionInfo;
                    crTable.ApplyLogOnInfo(crTableLogOnInfo);
                }
                //crystalReportViewer1.ReportSource = reportdocument;
                reportdocument.RecordSelectionFormula = formulastr;
                reportdocument.PrintToPrinter(1, false, 0, 0);

                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occured in DirectPrintRMS() " + ex.Message.ToString(), clsPublicVariables.Project_Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public void LoadReport(bool IsDefaultRpt, enumRMSForms RptName_1, string ModuleId_1, string RptFilePath_1, bool directprint_1, DateTime fromdate, DateTime todate, Int64 para1)
        {
            frmReport frmRpt = new frmReport();
            ReportDocument reportdocument = new ReportDocument();
            clsMsSqlDbFunction clsmssql = new clsMsSqlDbFunction();

            string ReportFilePath;
            string formulastr = "";

            try
            {
                Cursor.Current = Cursors.WaitCursor;

                ReportFilePath = "";

                this.Fromdate = fromdate;
                this.Todate = todate;
                this.Para1 = para1;
                this.GetConnectionDetails();
                this.GetSettingDetails();

                // MessageBox.Show("Report PAth : " + ReportPath);

                switch (RptName_1)
                {
                    case clsPublicVariables.enumRMSForms.RMS_CUSTOUTSTANDINGDTL:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\Customerwiseoutstandingdetails.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_CUSTOUTSTANDINGDTLREFNO:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\CustomerwiseoutstandingdetailsRefno.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLDTLCUSTOMERWISE:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BillDtlCustomerWise.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLDTLCUSTOMERWISEREFNO:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BillDtlCustomerWiseRefno.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_40BARCODELABELA4:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\40BARCODELABELA4.rpt";
                        break;
                    default:
                        break;
                }

                reportdocument.PrintOptions.PrinterName = frmRpt.Instance.cmbPrinter.Text;
                reportdocument.Load(ReportFilePath, OpenReportMethod.OpenReportByDefault);
                reportdocument.SetDatabaseLogon(clsPublicVariables.UserName1, clsPublicVariables.Password1, clsPublicVariables.ServerName1, clsPublicVariables.DatabaseName1);

                //Assign Parameter Value to Report
                this.AssignParameterToReport(reportdocument);

                //Set Margin To Reports
                //this.AssignMarginToReport(reportdocument);

                // Assign Formula String 
                switch (RptName_1)
                {
                    case clsPublicVariables.enumRMSForms.RMS_CUSTOUTSTANDINGDTL:
                        formulastr = "";
                        if (this.Para1 > 0)
                        {
                            formulastr = "{SP_CUSTOMERWISEOUTSTANDINGDETAILS;1.CUSTRID} = [" + this.Para1 + "]";
                        }
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_CUSTOUTSTANDINGDTLREFNO:
                        formulastr = "";
                        if (this.Para1 > 0)
                        {
                            formulastr = "{SP_CUSTOMERWISEOUTSTANDINGDETAILS;1.CUSTRID} = [" + this.Para1 + "]";
                        }
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLDTLCUSTOMERWISEREFNO:
                        formulastr = "";
                        if (this.Para1 > 0)
                        {
                            formulastr = "{SP_BILLDTLCUSTOMERWISE;1.CUSTRID} = [" + this.Para1 + "]";
                        }
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLDTLCUSTOMERWISE:
                        formulastr = "";
                        if (this.Para1 > 0)
                        {
                            formulastr = "{SP_BILLDTLCUSTOMERWISE;1.CUSTRID} = [" + this.Para1 + "]";
                        }
                        break;
                    default:
                        break;
                }

                Database crDatabase;
                Tables crTables;
                TableLogOnInfo crTableLogOnInfo;
                ConnectionInfo crConnectionInfo;

                crConnectionInfo = new ConnectionInfo();
                crConnectionInfo.ServerName = clsPublicVariables.ServerName1;
                crConnectionInfo.DatabaseName = clsPublicVariables.DatabaseName1;
                crConnectionInfo.UserID = clsPublicVariables.UserName1;
                crConnectionInfo.Password = clsPublicVariables.Password1;

                crDatabase = reportdocument.Database;
                crTables = crDatabase.Tables;

                foreach (CrystalDecisions.CrystalReports.Engine.Table crTable in crTables)
                {
                    crTableLogOnInfo = crTable.LogOnInfo;
                    crTableLogOnInfo.ConnectionInfo = crConnectionInfo;
                    crTable.ApplyLogOnInfo(crTableLogOnInfo);
                }

                if (directprint_1 == true)
                {
                    reportdocument.RecordSelectionFormula = formulastr;
                    reportdocument.PrintToPrinter(1, false, 0, 0);
                }
                else
                {
                    //Display Crystal Report Viewer
                    frmRptDisplay frm2 = new frmRptDisplay();
                    frm2.Instance.Reportnm = RptName_1;
                    frm2.Instance.ReportDocument = reportdocument;
                    frm2.Instance.FormulaStr = formulastr;
                    frm2.Instance.ShowDialog();
                }

                Cursor.Current = Cursors.Default;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occured in LoadReport " + ex.Message.ToString(), clsPublicVariables.Project_Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        public void LoadReport(bool IsDefaultRpt, enumRMSForms RptName_1, string ModuleId_1, string RptFilePath_1, bool directprint_1, DateTime fromdate, DateTime todate)
        {
            frmImage frmimg = new frmImage();
            frmReport frmRpt = new frmReport();
            ReportDocument reportdocument = new ReportDocument();
            clsMsSqlDbFunction clsmssql = new clsMsSqlDbFunction();

            string ReportFilePath;
            string formulastr = "";

            try
            {
                Cursor.Current = Cursors.WaitCursor;

                ReportFilePath = "";

                this.Fromdate = fromdate;
                this.Todate = todate;

                this.GetConnectionDetails();
                this.GetSettingDetails();

                // MessageBox.Show("Report PAth : " + ReportPath);

                switch (RptName_1)
                {
                    case clsPublicVariables.enumRMSForms.RMS_KOT:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\Kot.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_KOT2:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\Kot2.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_KOTB:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\Kotb.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILL:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\Bill.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLB:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\Billb.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLREGISTER:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BillRegister.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLREGISTER_REFNO:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BillRegisterRefno.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLBREGISTER:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BillbRegister.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_KOTREG:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\KotReg.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_KOTEDITREG:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\KotEditReg.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_KOTDELETEREG:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\KotDeleteReg.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_KOTBREG:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\KotbReg.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLREG:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BillReg.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLREG_REFNO:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BillRegRefno.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLBREG:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BillbReg.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_SETTLEMENT:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\SettlementReg.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_SETTLEMENTREFNO:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\SettlementRegRefno.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_SETTLEMENTB:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\SettlementbReg.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_KOTREGISTER:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\KotRegister.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_KOTBREGISTER:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\KotbRegister.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_PURCHAESREGISTER:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\ItemPurchaseRegister.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_PAYMENTREGISTER:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\PaymentRegister.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_CUSTOUTSTANDING:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\Custoutstanding.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLEDITREG:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BillEditReg.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLEDITREGREFNO:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BillEditRegRefno.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLDELETEREG:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BillDeleteReg.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLDELETEREGREFNO:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BillDeleteRegRefno.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_ITEMWISESALES:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\Itemsales.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_DATEWISEBILLING:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\Datewisebilling.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_REVISEDBILLREG:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\RevisedBillRegister.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_REVISEDBILLREG_REFNO:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\RevisedBillRegisterRefno.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_CAPCOMMIREGISTER:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\CapcommiRegister.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_CAPCOMMIREGISTERREFNO:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\CapcommiRegisterRefno.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLWISESALESSUMMARY:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BillWiseSalesSummary.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLWISESALESSUMMARY_REFNO:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BillWiseSalesSummaryRefno.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BANQBOOKINGREG:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BanquetBooking.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_ITEMGROUPWISESALES:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\ItemGroupSales.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.POS_BILL:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\PosBill.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_VATREGISTER:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\Vatregister.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_REPORTDEPARTWISESALES:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\ReportDepartmentSales.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_CHECKLISTITEMSTOCK:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\ChecklistItemStock.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_TABLERUNNINGSUMMARY:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\TableRunningSummary.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_TABLERUNNINGSUMMARYREFNO:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\TableRunningSummaryRefno.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BANQBOOKING:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\Banqbooking.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_PURCHASEDETAILSSUPPLIERWISE:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\Supplierwisepurdtl.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_PURCHASEDETAILSITEMWISE:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\Itemwisepurdtl.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_DATEWISEBUSINESSINFO:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\DatewiseBusinessInfo.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_PURCHASEBILLINFO:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\Purchasebillinfo.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_PURCHASEDETAILSITEMSUMMARY:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\Itemwisepurinfo.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_SETTLEMENTWISEBILLSUMMARY:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\Settlementwisesalessummary.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_SETTLEMENTWISEBILLSUMMARYREFNO:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\SettlementwisesalessummaryRefno.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_CASHONHANDREGSITER:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\Cashonhandregister.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_MSTITEM:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\ItemMaster.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLGIVETOCUSTOMERREG:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BillGivetoCustomerRegister.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLGIVETOCUSTOMERREGREFNO:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BillGivetoCustomerRegisterRefno.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_ITEMRECIPEUSAGESUMMARY:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\Itemrecipeusagesummary.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_ITEMRECIPEUSAGEDETAILS:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\ItemRecipeusageDetails.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_ITEMWISERECIPEUSAGEDETAILS:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\Itemwiserecipedetails.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_THERMAL_ITEMWISESALESREGISTER:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\ThermalItemWiseSalesReg.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_THERMAL_DAILYBILLREGISTER:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\ThermalBillRegister.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_THERMAL_DAILYBILLREGISTERREFNO:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\ThermalBillRegisterRefno.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_THERMAL_DEPARTMENTWISESALESREGISTER:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\ThermalDeptWiseSalesReg.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLREGWITHPARCLEDTL:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BillRegWithParcel.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLREGWITHPARCLEDTL_REFNO:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BillRegWithParcelRefno.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLREGDATEWISE:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BillRegDateWise.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLREGDATEWISE_REFNO:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BillRegDateWiseRefno.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_PURISSUEDTLREG:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\PurIssueDtlRegister.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_DEPTPURISSUEDTLREG:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\DeptWisePurIssueDtl.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_PURITEMISSUESUMMARY:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\PurItemIssueSummary.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_PURITEMDEPTISSUESUMMARY:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\PurItemDeptIssueSummary.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_PURISSUESTOCKREG:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\PurIssueStockReg.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_THERMAL_DATEWISEBILLREG:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\ThermalDateWiseBillReg.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_THERMAL_COMPLYBILLREG:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\ComplymentBillRegister.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_THERMAL_COMPLYBILLREGREFNO:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\ComplymentBillRegisterRefno.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLTYPEWISEREG:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BillRegBillType.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLTYPEWISEREG_REFNO:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BillRegBillTypeRefno.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_CASHDRAWEROPENREG:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\CashDrawerOpenRegister.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_PURCHASEUSAGESUMMARY:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\PurchaseUsageSummary.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_ITEMWISEPURCHASEITEMUSED:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\ItemWisePurchaseUsed.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BANQBILLINGINFO:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BanqBilling.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BANQBILLINGINFOREG:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BanqBillingInfoReg.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.POS_ITEMMASTERWITHBARCODE:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BarcodeItemMaster.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.POS_NOOFITEMBARCODE:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\ItemBarcode.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_DAYWISEPAXSUMMARY:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\DaySummaryPaxWise.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_DAYWISEPAXSUMMARYREFNO:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\DaySummaryPaxWiseRefno.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.POS_ITEMPURRATEWISEREG:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\ItemPurratewiseregister.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.POS_ITEMWISEBILLREG:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BillRegisterItemwise.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.POS_ITEMGROUPPWISEREG:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\ItemGroupwiseRegister.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.POS_BILLREMARKREG:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BillRemarkRegister.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BANQINQUIRY:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BanqInquiry.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_KOTREMARKREG:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\KotRemarkReg.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLREMARKREG:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BillRemarkReg.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLREMARKREGREFNO:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BillRemarkRegRefno.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BQBOOKINGINFOREG:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BqBookingInfoReg.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BQINQUIRYINFOREG:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BqInquiryInfoReg.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BQBOOKING:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BqBooking.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_ITEMPURCHASE:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\ItemPurchase.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_STOCKISSUE:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\StockIssue.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.POS_ITEMGROUPWISESALES:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\ItemGroupSales.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.POS_STOCKREGISTER:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\StockRegister.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_REFBYBILLINFORMATION:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\RefByBillInformation.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_REFBYBILLINFORMATION_REFNO:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\RefByBillInformationRefno.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_KOTEDITDELETEREG:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\KotEditDeleteInformation.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLEDITDELETEREG:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BillEditDeleteInformation.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLEDITDELETEREGREFNO:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BillEditDeleteInformationRefno.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_KOTMACHWITHBILLREG:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\KotMachwithBillReg.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_KOTMACHWITHBILLREGREFNO:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\KotMachwithBillRegRefno.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLTIMEWISESALESINFO:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BillTimeWiseInfo.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLTIMEWISESALESINFO_REFNO:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BillTimeWiseInfoRefno.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_TIEUPCOMPANYBILLINFO:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\TieupCompanyBillInfo.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_TIEUPCOMPANYBILLINFOREFNO:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\TieupCompanyBillInfoRefno.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLWISEDETAILSUMMARY:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BillWiseDetailSummary.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLWISEDETAILSUMMARY_REFNO:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BillWiseDetailSummaryRefno.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_OUTPUTVATREPORT:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\Outputvatreport.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_PURCHASESTOCKREG:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\PurchaseStockReg.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_PURCHASEITEMGROUPSTOCKREG:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\PurchaseItemGroupStockReg.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_CASHONHAND:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\CashOnHand.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_PAYMENTINFO:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\PaymentVoucher.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.POS_40BARCODELABELA4:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\40BARCODELABELA4.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_40BARCODELABELA4:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\40BARCODELABELA4.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_ITEMWISEPURCHASE:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\ItemRecipe.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_SETTLEMENTOTHER:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\OtherSettlementReg.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_SETTLEMENTOTHERREFNO:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\OtherSettlementRegRefno.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_INCOMEEXPENCESUMMARYRPT:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\IncomeExpenceSummary.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_TABLEWISESALESREPORT:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\TableWiseSalesReport.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLCUSTOMERWISE:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\CustomerWiseBillReport.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLCUSTOMERWISEREFNO:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\CustomerWiseBillReportRefno.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_GSTREPORT:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\Gstreport.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_GSTREPORT_REFNO:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\GstreportRefno.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_SUPPLIERWISEPAYMENTDETAIL:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\SupplierwisePendingPayment.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_DATEWISEBILLSUMMARY:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\DateWiseBillSummaryBillNo.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_DATEWISEBILLSUMMARY_REFNO:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\DateWiseBillSummaryRefBillNo.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLREGWITHSETTINFO_REFNO:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BillRegDtlWithSettInfoRefno.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLREGWITHSETTINFO:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BillRegDtlWithSettInfo.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_SALESSUMMARY:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\SalesSummary.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_SALESSUMMARYREFNO:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\SalesSummaryRefno.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_COMPLEMENTRYKOTREG:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\ComplementryKotReg.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_COMPLEMENTRYKOTREGREFNO:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\ComplementryKotRegRefno.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_MESSCUSTOMERPOSITIONINFORMATION:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\MessCustomerPaymentposition.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_PURREQORDER:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\PurchaseOrderReq.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_PURISTOCKFORMAT:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\PurItemStockFormat.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_GSTPERWISESUMMARY:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\GSTPERWISESUMMARY.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_GSTPERWISESUMMARY_REFNO:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\GSTPERWISESUMMARYREFNO.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_DATEWISESETTSUMMARY:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\DateWiseSettlementSummary.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_TIEUPCOMPANYWISESUMMARY:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\TieupCompanyWiseSummary.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_TIEUPCOMPANYWISESUMMARYREFNO:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\TieupCompanyWiseSummaryRefno.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_DAILYDETAILSBILLINFO:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\DailyBillDetails.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_DAILYDETAILSBILLINFOREFNO:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\DailyBillDetailsRefno.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_TIEUPCOMPANYWISEDETAILREFNO:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\TieupCompanyWiseDetailsRefno.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_TIEUPCOMPANYWISEDETAIL:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\TieupCompanyWiseDetails.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_HOMEDELIVERYREPORT:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BillDeliveryRegDtlWithSettInfo.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_HOMEDELIVERYREPORTREFNO:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BillDeliveryRegDtlWithSettInfoRefno.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_THERMALBILLWISESETTREFNO:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\ThermalBilWiseSettlementRefno.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_THERMALBILLWISESETT:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\ThermalBilWiseSettlement.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_TABLEWAITING:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\Tablewaiting.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_TABLERESERVATION:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\TableReservation.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_GSTDATEANDPERWISEREPORT:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\GstPerAndDateWiseReport.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_PURITEMSTOCKDATEWISE:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\PurItemStockDateWise.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_ITEMPROFITREPORT:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\ItemProfitReport.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_INCOMEDTLREPORT:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\IncomeDetailReport.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_EXPENCEDTLREPORT:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\ExpenceDetailReport.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_DATEWISESALARYREPORT:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\DateWiseSalary.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_DATEWISEATTENDANCEREPORT:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\DateWiseAttendanceandsalary.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BANQPAYMENTREG:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BanqPaymentReg.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_THERMAL_GROUPWISESALESREGISTER:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\ThermalGroupWiseSalesRegister.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_ENTRYTICKETDETAILCOLLECTIONRPT:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\EntryTicketDetailedCollection.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_ENTRYTICKETSUMMARYCOLLECTIONRPT:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\EntryTicketSummaryCollection.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_COSTUMEBILL:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\CouponBill.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_ENTRYTICKET:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\EntryTicket.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_COSTUMERENTDETAILREPORT:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\CouponBillDetailedCollection.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_DATEWISEBELTTRANSACTION:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\CardBeltHistoryDatewise.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BELTISSUEREGISTER:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BeltIssueRegister.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BELTRECHARGEREGISTER:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BeltRechargeRegister.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BELTSUBMITREGISTER:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BeltSubmitRegister.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_COSTUMEISSUEITEMWISEREGISTER:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\CostumeIssueItemwiseRegister.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_COUPONBILLSUMMARYCOLLECTIONRPT:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\CouponBillSummary.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_COSTUMERETURNREGISTER:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\CouponReturnRegister.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_ENTRYTICKETPAYMENTCOLLECTIONRPT:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\EntryTicketPaymentCollection.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_SUPPLIERWISEPAYMENTREGISTER:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\SupplierwisePaymentRegister.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_CUSTOMERWISESETTLEMENTREFNO:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\CustomerWiseSettlementRegisterRefno.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_CUSTOMERWISESETTLEMENT:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\CustomerWiseSettlementRegister.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_THERMALUSERWISEBUSINESSSUMMARY:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\UserwiseBusinessSummary.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_CHECKLISTITEMDETAILS:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\Checklistitemdetails.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_SALARYSLIPDETAILS:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\EmpSalarySlipDetails.rpt";
                        break;
                    default:
                        break;
                }

                reportdocument.PrintOptions.PrinterName = frmRpt.Instance.cmbPrinter.Text;
                reportdocument.Load(ReportFilePath, OpenReportMethod.OpenReportByDefault);
                reportdocument.SetDatabaseLogon(clsPublicVariables.UserName1, clsPublicVariables.Password1, clsPublicVariables.ServerName1, clsPublicVariables.DatabaseName1);

                //Assign Parameter Value to Report
                this.AssignParameterToReport(reportdocument);

                //Set Margin To Reports
                //this.AssignMarginToReport(reportdocument);

                // Assign Formula String 
                switch (RptName_1)
                {
                    case clsPublicVariables.enumRMSForms.RMS_BILL:
                        formulastr = " {BILL.RId} = [" + ModuleId_1 + "] and {BILLDTL.DELFLG}=False";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLB:
                        formulastr = " {BILLb.RId} = [" + ModuleId_1 + "] and {BILLBDTL.DELFLG}=False";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_KOT:
                        formulastr = " {KOT.RID} = [" + ModuleId_1 + "] and {KOTDTL.DELFLG}=False";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_KOT2:
                        formulastr = " {KOT.RID} = [" + ModuleId_1 + "] and {KOTDTL.DELFLG}=False";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_KOTB:
                        formulastr = " {KOTb.RID} = [" + ModuleId_1 + "]and {KOTBDTL.DELFLG}=False";
                        break;
                    case clsPublicVariables.enumRMSForms.POS_BILL:
                        formulastr = " {BILL.RId} = [" + ModuleId_1 + "] and {BILLDTL.DELFLG}=False";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BANQBOOKING:
                        formulastr = " {BANQBOOKING.RId} = [" + ModuleId_1 + "] and {BANQBOOKINGDTL.DELFLG}=False";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BANQBILLINGINFO:
                        formulastr = " {BANQBILLINFO.RId} = [" + ModuleId_1 + "] and {BANQBILLINFODETAIL.DELFLG}=False";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BANQINQUIRY:
                        formulastr = " {BANQINQUIRY.RId} = [" + ModuleId_1 + "] and {BANQINQUIRY.DELFLG}=False";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BQBOOKING:
                        formulastr = " {BQBOOKING.RId} = [" + ModuleId_1 + "] and {BQBOOKINGDTL.DELFLG}=False";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_ITEMPURCHASE:
                        formulastr = " {ITEMPURCHASE.RId} = [" + ModuleId_1 + "] and {ITEMPURCHASEDTL.DELFLG}=False";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_STOCKISSUE:
                        formulastr = " {STOCKISSUE.RId} = [" + ModuleId_1 + "] and {STOCKISSUEDTL.DELFLG}=False";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_CASHONHAND:
                        formulastr = " {CASHONHAND.RId} = [" + ModuleId_1 + "] and {CASHONHAND.DELFLG}=False";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_PAYMENTINFO:
                        formulastr = " {PAYMENTINFO.RId} = [" + ModuleId_1 + "] and {PAYMENTINFO.DELFLG}=0";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_ITEMWISEPURCHASE:
                        formulastr = " {ITEMWISEPURCHASE.RId} = [" + ModuleId_1 + "] and {ITEMWISEPURCHASE.DELFLG}=False";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_PURREQORDER:
                        formulastr = " {PURREQORDER.RId} = [" + ModuleId_1 + "] and {PURREQORDERDTL.DELFLG}=False";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_TABLEWAITING:
                        formulastr = " {TBLWAIT.RId} = [" + ModuleId_1 + "] and {TBLWAIT.DELFLG}=False";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_TABLERESERVATION:
                        formulastr = " {TABLERESERVATION.RId} = [" + ModuleId_1 + "] and {TABLERESERVATION.DELFLG}=False";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_COSTUMEBILL:
                        formulastr = " {COUPONBILL.RId} = [" + ModuleId_1 + "] and {COUPONBILL.DELFLG}=False";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_ENTRYTICKET:
                        formulastr = " {ENTRYTICKET.RId} = [" + ModuleId_1 + "] and {ENTRYTICKET.DELFLG}=False";
                        break;
                    default:
                        break;
                }

                Database crDatabase;
                Tables crTables;
                TableLogOnInfo crTableLogOnInfo;
                ConnectionInfo crConnectionInfo;

                crConnectionInfo = new ConnectionInfo();
                crConnectionInfo.ServerName = clsPublicVariables.ServerName1;
                crConnectionInfo.DatabaseName = clsPublicVariables.DatabaseName1;
                crConnectionInfo.UserID = clsPublicVariables.UserName1;
                crConnectionInfo.Password = clsPublicVariables.Password1;

                crDatabase = reportdocument.Database;
                crTables = crDatabase.Tables;

                foreach (CrystalDecisions.CrystalReports.Engine.Table crTable in crTables)
                {
                    crTableLogOnInfo = crTable.LogOnInfo;
                    crTableLogOnInfo.ConnectionInfo = crConnectionInfo;
                    crTable.ApplyLogOnInfo(crTableLogOnInfo);
                }


                //crystalReportViewer1.ReportSource = reportdocument;

                if (directprint_1 == true)
                {
                    reportdocument.RecordSelectionFormula = formulastr;
                    reportdocument.PrintToPrinter(1, false, 0, 0);
                }
                else
                {
                    //Display Crystal Report Viewer
                    frmRptDisplay frm2 = new frmRptDisplay();
                    frm2.Instance.Icon = frmimg.Icon;
                    frm2.Instance.Reportnm = RptName_1;
                    frm2.Instance.ReportDocument = reportdocument;
                    frm2.Instance.FormulaStr = formulastr;
                    frm2.Instance.ShowDialog();
                }


                // UPDATE BILL PRINT COUNTER
                if (RptName_1 == clsPublicVariables.enumRMSForms.RMS_BILL)
                {
                    string str1;
                    DataTable dtbill = new DataTable();
                    Int64 cntprint1;

                    cntprint1 = 0;
                    str1 = "select CNTPRINT from bill where rid = " + ModuleId_1;
                    dtbill = clsmssql.FillDataTable(str1, "bill");

                    if (dtbill.Rows.Count > 0)
                    {
                        foreach (DataRow row in dtbill.Rows)
                        {
                            Int64.TryParse(row["CNTPRINT"] + "", out cntprint1);
                            cntprint1 = cntprint1 + 1;
                        }

                        str1 = "UPDATE BILL SET CNTPRINT = " + cntprint1 + " WHERE RID = " + ModuleId_1;
                        clsmssql.ExecuteMsSqlCommand(str1);
                    }
                }

                Cursor.Current = Cursors.Default;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occured in LoadReport " + ex.Message.ToString(), clsPublicVariables.Project_Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public bool ExportReportToPdf(bool IsDefaultRpt, string RptName_1, string ModuleId_1, string RptFilePath_1, DateTime fromdate, DateTime todate)
        {
            frmImage frmimg = new frmImage();
            frmReport frmRpt = new frmReport();
            ReportDocument reportdocument = new ReportDocument();

            this.Fromdate = fromdate;
            this.Todate = todate;

            clsMsSqlDbFunction objclsdb = new clsMsSqlDbFunction();
            string filename;
            string ReportFilePath;
            string formulastr = "";

            string filenamestr_1 = "";
            //string namestr_1 = "";
            //string sqlstr;
            //DataTable Dt1;

            try
            {
                Cursor.Current = Cursors.WaitCursor;

                this.GetConnectionDetails();
                this.GetSettingDetails();

                clsPublicVariables.enumRMSForms RptEnum = (clsPublicVariables.enumRMSForms)Enum.Parse(typeof(clsPublicVariables.enumRMSForms), RptName_1, true);

                filename = "";
                ReportFilePath = "";
                filenamestr_1 = "";

                this.Fromdate = fromdate;
                this.Todate = todate;

                //if (RptFilePath_1.Trim() != "")
                //{
                //    ReportFilePath = RptFilePath_1;
                //}
                //else
                //{

                switch (RptEnum)
                {
                    case clsPublicVariables.enumRMSForms.RMS_KOT:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\Kot.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_KOT2:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\Kot2.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_KOTB:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\Kotb.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILL:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\Bill.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLB:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\Billb.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLREG:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BillReg.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLREG_REFNO:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BillRegRefno.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLBREG:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BillbReg.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_KOTREG:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\KotReg.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_KOTEDITREG:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\KotEditReg.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_KOTDELETEREG:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\KotDeleteReg.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_KOTBREG:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\KotbReg.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLREGISTER:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BillRegister.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLREGISTER_REFNO:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BillRegisterRefno.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLBREGISTER:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BillbRegister.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_KOTREGISTER:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\KotRegister.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_KOTBREGISTER:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\KotbRegister.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_PURCHAESREGISTER:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\ItemPurchaseRegister.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_PAYMENTREGISTER:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\PaymentRegister.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_CUSTOUTSTANDING:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\Custoutstanding.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLEDITREG:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BillEditReg.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLEDITREGREFNO:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BillEditRegRefno.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLDELETEREG:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BillDeleteReg.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLDELETEREGREFNO:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BillDeleteRegRefno.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_ITEMWISESALES:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\Itemsales.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_DATEWISEBILLING:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\Datewisebilling.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_REVISEDBILLREG:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\RevisedBillRegister.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_REVISEDBILLREG_REFNO:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\RevisedBillRegisterRefno.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_CAPCOMMIREGISTER:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\CapcommiRegister.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_CAPCOMMIREGISTERREFNO:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\CapcommiRegisterRefno.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLWISESALESSUMMARY:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BillWiseSalesSummary.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLWISESALESSUMMARY_REFNO:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BillWiseSalesSummaryRefno.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BANQBOOKINGREG:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BanquetBooking.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_ITEMGROUPWISESALES:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\ItemGroupSales.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.POS_BILL:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\PosBill.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_VATREGISTER:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\Vatregister.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_REPORTDEPARTWISESALES:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\ReportDepartmentSales.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_CHECKLISTITEMSTOCK:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\ChecklistItemStock.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_TABLERUNNINGSUMMARY:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\TableRunningSummary.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_TABLERUNNINGSUMMARYREFNO:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\TableRunningSummaryRefno.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BANQBOOKING:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\Banqbooking.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_PURCHASEDETAILSSUPPLIERWISE:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\Supplierwisepurdtl.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_PURCHASEDETAILSITEMWISE:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\Itemwisepurdtl.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_DATEWISEBUSINESSINFO:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\DatewiseBusinessInfo.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_PURCHASEBILLINFO:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\Purchasebillinfo.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_PURCHASEDETAILSITEMSUMMARY:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\Itemwisepurinfo.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_SETTLEMENTWISEBILLSUMMARY:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\Settlementwisesalessummary.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_SETTLEMENTWISEBILLSUMMARYREFNO:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\SettlementwisesalessummaryRefno.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_CASHONHANDREGSITER:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\Cashonhandregister.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_MSTITEM:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\ItemMaster.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLGIVETOCUSTOMERREG:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BillGivetoCustomerRegister.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLGIVETOCUSTOMERREGREFNO:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BillGivetoCustomerRegisterRefno.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_ITEMRECIPEUSAGESUMMARY:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\Itemrecipeusagesummary.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_ITEMRECIPEUSAGEDETAILS:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\ItemRecipeusageDetails.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_ITEMWISERECIPEUSAGEDETAILS:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\Itemwiserecipedetails.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_THERMAL_ITEMWISESALESREGISTER:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\ThermalItemWiseSalesReg.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_THERMAL_DAILYBILLREGISTER:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\ThermalBillRegister.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_THERMAL_DAILYBILLREGISTERREFNO:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\ThermalBillRegisterRefno.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_THERMAL_DEPARTMENTWISESALESREGISTER:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\ThermalDeptWiseSalesReg.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLREGWITHPARCLEDTL:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BillRegWithParcel.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLREGWITHPARCLEDTL_REFNO:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BillRegWithParcelRefno.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLREGDATEWISE:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BillRegDateWise.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLREGDATEWISE_REFNO:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BillRegDateWiseRefno.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_PURISSUEDTLREG:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\PurIssueDtlRegister.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_DEPTPURISSUEDTLREG:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\DeptWisePurIssueDtl.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_PURITEMISSUESUMMARY:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\PurItemIssueSummary.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_PURITEMDEPTISSUESUMMARY:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\PurItemDeptIssueSummary.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_PURISSUESTOCKREG:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\PurIssueStockReg.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_THERMAL_DATEWISEBILLREG:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\ThermalDateWiseBillReg.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_THERMAL_COMPLYBILLREG:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\ComplymentBillRegister.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_THERMAL_COMPLYBILLREGREFNO:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\ComplymentBillRegisterRefno.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLTYPEWISEREG:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BillRegBillType.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLTYPEWISEREG_REFNO:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BillRegBillTypeRefno.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_CASHDRAWEROPENREG:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\CashDrawerOpenRegister.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_PURCHASEUSAGESUMMARY:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\PurchaseUsageSummary.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_ITEMWISEPURCHASEITEMUSED:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\ItemWisePurchaseUsed.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BANQBILLINGINFO:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BanqBilling.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BANQBILLINGINFOREG:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BanqBillingInfoReg.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.POS_ITEMMASTERWITHBARCODE:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BarcodeItemMaster.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.POS_NOOFITEMBARCODE:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\ItemBarcode.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_DAYWISEPAXSUMMARY:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\DaySummaryPaxWise.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_DAYWISEPAXSUMMARYREFNO:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\DaySummaryPaxWiseRefno.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.POS_ITEMPURRATEWISEREG:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\ItemPurratewiseregister.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.POS_ITEMWISEBILLREG:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BillRegisterItemwise.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.POS_ITEMGROUPPWISEREG:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\ItemGroupwiseRegister.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.POS_BILLREMARKREG:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BillRemarkRegister.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BANQINQUIRY:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BanqInquiry.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_KOTREMARKREG:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\KotRemarkReg.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLREMARKREG:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BillRemarkReg.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLREMARKREGREFNO:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BillRemarkRegRefno.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BQBOOKINGINFOREG:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BqBookingInfoReg.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BQINQUIRYINFOREG:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BqInquiryInfoReg.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BQBOOKING:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BqBooking.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_ITEMPURCHASE:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\ItemPurchase.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_STOCKISSUE:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\StockIssue.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.POS_ITEMGROUPWISESALES:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\ItemGroupSales.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.POS_STOCKREGISTER:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\StockRegister.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_REFBYBILLINFORMATION:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\RefByBillInformation.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_REFBYBILLINFORMATION_REFNO:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\RefByBillInformationRefno.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_KOTEDITDELETEREG:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\KotEditDeleteInformation.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLEDITDELETEREG:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BillEditDeleteInformation.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLEDITDELETEREGREFNO:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BillEditDeleteInformationRefno.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_KOTMACHWITHBILLREG:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\KotMachwithBillReg.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_KOTMACHWITHBILLREGREFNO:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\KotMachwithBillRegRefno.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLTIMEWISESALESINFO:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BillTimeWiseInfo.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLTIMEWISESALESINFO_REFNO:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BillTimeWiseInfoRefno.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_TIEUPCOMPANYBILLINFO:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\TieupCompanyBillInfo.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_TIEUPCOMPANYBILLINFOREFNO:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\TieupCompanyBillInfoRefno.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLWISEDETAILSUMMARY:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BillWiseDetailSummary.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLWISEDETAILSUMMARY_REFNO:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BillWiseDetailSummaryRefno.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_OUTPUTVATREPORT:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\Outputvatreport.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_PURCHASESTOCKREG:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\PurchaseStockReg.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_CASHONHAND:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\CashOnHand.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_PAYMENTINFO:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\PaymentVoucher.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.POS_40BARCODELABELA4:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\40BARCODELABELA4.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_40BARCODELABELA4:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\40BARCODELABELA4.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_ITEMWISEPURCHASE:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\ItemRecipe.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_SETTLEMENTOTHER:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\OtherSettlementReg.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_SETTLEMENTOTHERREFNO:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\OtherSettlementRegRefno.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_INCOMEEXPENCESUMMARYRPT:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\IncomeExpenceSummary.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_TABLEWISESALESREPORT:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\TableWiseSalesReport.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLCUSTOMERWISE:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\CustomerWiseBillReport.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLCUSTOMERWISEREFNO:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\CustomerWiseBillReportRefno.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_GSTREPORT:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\Gstreport.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_GSTREPORT_REFNO:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\GstreportRefno.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_SUPPLIERWISEPAYMENTDETAIL:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\SupplierwisePendingPayment.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_DATEWISEBILLSUMMARY:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\DateWiseBillSummaryBillNo.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_DATEWISEBILLSUMMARY_REFNO:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\DateWiseBillSummaryRefBillNo.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLREGWITHSETTINFO_REFNO:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BillRegDtlWithSettInfoRefno.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLREGWITHSETTINFO:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BillRegDtlWithSettInfo.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_SALESSUMMARY:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\SalesSummary.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_SALESSUMMARYREFNO:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\SalesSummaryRefno.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_COMPLEMENTRYKOTREG:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\ComplementryKotReg.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_COMPLEMENTRYKOTREGREFNO:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\ComplementryKotRegRefno.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_MESSCUSTOMERPOSITIONINFORMATION:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\MessCustomerPaymentposition.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_PURCHASEITEMGROUPSTOCKREG:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\PurchaseItemGroupStockReg.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_PURREQORDER:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\PurchaseOrderReq.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_PURISTOCKFORMAT:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\PurItemStockFormat.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_GSTPERWISESUMMARY:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\GSTPERWISESUMMARY.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_GSTPERWISESUMMARY_REFNO:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\GSTPERWISESUMMARYREFNO.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_DATEWISESETTSUMMARY:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\DateWiseSettlementSummary.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_TIEUPCOMPANYWISESUMMARY:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\TieupCompanyWiseSummary.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_TIEUPCOMPANYWISESUMMARYREFNO:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\TieupCompanyWiseSummaryRefno.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_DAILYDETAILSBILLINFO:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\DailyBillDetails.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_DAILYDETAILSBILLINFOREFNO:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\DailyBillDetailsRefno.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_TIEUPCOMPANYWISEDETAILREFNO:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\TieupCompanyWiseDetailsRefno.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_TIEUPCOMPANYWISEDETAIL:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\TieupCompanyWiseDetails.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_HOMEDELIVERYREPORT:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BillDeliveryRegDtlWithSettInfo.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_HOMEDELIVERYREPORTREFNO:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BillDeliveryRegDtlWithSettInfoRefno.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_THERMALBILLWISESETTREFNO:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\ThermalBilWiseSettlementRefno.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_THERMALBILLWISESETT:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\ThermalBilWiseSettlement.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_TABLEWAITING:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\Tablewaiting.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_TABLERESERVATION:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\TableReservation.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_GSTDATEANDPERWISEREPORT:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\GstPerAndDateWiseReport.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_PURITEMSTOCKDATEWISE:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\PurItemStockDateWise.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_ITEMPROFITREPORT:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\ItemProfitReport.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_INCOMEDTLREPORT:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\IncomeDetailReport.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_EXPENCEDTLREPORT:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\ExpenceDetailReport.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_DATEWISESALARYREPORT:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\DateWiseSalary.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_DATEWISEATTENDANCEREPORT:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\DateWiseAttendanceandsalary.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BANQPAYMENTREG:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BanqPaymentReg.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_THERMAL_GROUPWISESALESREGISTER:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\ThermalGroupWiseSalesRegister.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_ENTRYTICKETDETAILCOLLECTIONRPT:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\EntryTicketDetailedCollection.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_ENTRYTICKETSUMMARYCOLLECTIONRPT:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\EntryTicketSummaryCollection.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_COSTUMEBILL:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\CouponBill.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_ENTRYTICKET:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\EntryTicket.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_COSTUMERENTDETAILREPORT:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\CouponBillDetailedCollection.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_DATEWISEBELTTRANSACTION:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\CardBeltHistoryDatewise.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BELTISSUEREGISTER :
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BeltIssueRegister.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BELTRECHARGEREGISTER:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BeltRechargeRegister.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BELTSUBMITREGISTER:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BeltSubmitRegister.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_COSTUMEISSUEITEMWISEREGISTER:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\CostumeIssueItemwiseRegister.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_COUPONBILLSUMMARYCOLLECTIONRPT:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\CouponBillSummary.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_COSTUMERETURNREGISTER:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\CouponReturnRegister.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_ENTRYTICKETPAYMENTCOLLECTIONRPT:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\EntryTicketPaymentCollection.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_SUPPLIERWISEPAYMENTREGISTER:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\SupplierwisePaymentRegister.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_CUSTOMERWISESETTLEMENTREFNO:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\CustomerWiseSettlementRegisterRefno.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_CUSTOMERWISESETTLEMENT:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\CustomerWiseSettlementRegister.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_THERMALUSERWISEBUSINESSSUMMARY:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\UserwiseBusinessSummary.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_CHECKLISTITEMDETAILS:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\Checklistitemdetails.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_SALARYSLIPDETAILS:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\EmpSalarySlipDetails.rpt";
                        break;

                    default:
                        break;
                }
                //}

                reportdocument.PrintOptions.PrinterName = frmRpt.Instance.cmbPrinter.Text;
                reportdocument.Load(ReportFilePath, OpenReportMethod.OpenReportByDefault);

                //Assign Parameter Value to Report
                this.AssignParameterToReport(reportdocument);

                //Set Margin To Reports
                //this.AssignMarginToReport(reportdocument);

                Database crDatabase;
                Tables crTables;
                TableLogOnInfo crTableLogOnInfo;
                ConnectionInfo crConnectionInfo;

                crConnectionInfo = new ConnectionInfo();
                crConnectionInfo.ServerName = clsPublicVariables.ServerName1;
                crConnectionInfo.DatabaseName = clsPublicVariables.DatabaseName1;
                crConnectionInfo.UserID = clsPublicVariables.UserName1;
                crConnectionInfo.Password = clsPublicVariables.Password1;

                crDatabase = reportdocument.Database;
                crTables = crDatabase.Tables;

                foreach (CrystalDecisions.CrystalReports.Engine.Table crTable in crTables)
                {
                    crTableLogOnInfo = crTable.LogOnInfo;
                    crTableLogOnInfo.ConnectionInfo = crConnectionInfo;
                    crTable.ApplyLogOnInfo(crTableLogOnInfo);
                }

                //crystalReportViewer1.ReportSource = reportdocument;

                //reportdocument.SetDatabaseLogon(UserName1, Password1, ServerName1, DatabaseName1);

                ////Assign Parameter Value to Report
                //this.AssignParameterToReport(reportdocument);

                ////Set Margin To Reports
                //this.AssignMarginToReport(reportdocument);

                // Assign Formula String 

                switch (RptEnum)
                {

                    case clsPublicVariables.enumRMSForms.RMS_BILL:
                        formulastr = " {BILL.RId} = [" + ModuleId_1 + "] and {BILLDTL.DELFLG}=False";
                        filenamestr_1 = "Bill No " + ModuleId_1;
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLB:
                        formulastr = " {BILLb.RId} = [" + ModuleId_1 + "] and {BILLbDTL.DELFLG}=False";
                        filenamestr_1 = "BillB No " + ModuleId_1;
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_KOT:
                        formulastr = " {KOT.RID} = [" + ModuleId_1 + "] and {KOTDTL.DELFLG}=False";
                        filenamestr_1 = "KOT No " + ModuleId_1;
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_KOT2:
                        formulastr = " {KOT.RID} = [" + ModuleId_1 + "] and {KOTDTL.DELFLG}=False";
                        filenamestr_1 = "KOT No " + ModuleId_1;
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_KOTB:
                        formulastr = " {KOTb.RID} = [" + ModuleId_1 + "] and {KOTbDTL.DELFLG}=False";
                        filenamestr_1 = "KOTB No " + ModuleId_1;
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_KOTREGISTER:
                        filenamestr_1 = "Kot Register";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_KOTBREGISTER:
                        filenamestr_1 = "Kotb Register ";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLREGISTER:
                        filenamestr_1 = "Bill Register";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLREGISTER_REFNO:
                        filenamestr_1 = "Bill Register";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLBREGISTER:
                        filenamestr_1 = "BillB Register ";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_SETTLEMENT:
                        filenamestr_1 = "Settlement Register ";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_SETTLEMENTREFNO:
                        filenamestr_1 = "Settlement Register ";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_SETTLEMENTB:
                        filenamestr_1 = "SettlementB Register";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_PURCHAESREGISTER:
                        filenamestr_1 = "Purchase Register";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_PAYMENTREGISTER:
                        filenamestr_1 = "Payment Register";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_CUSTOUTSTANDING:
                        filenamestr_1 = "Customer Outstanding Register";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_KOTEDITREG:
                        filenamestr_1 = "Kot Edit Register";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_KOTDELETEREG:
                        filenamestr_1 = "Kot Delete Register";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLEDITREG:
                        filenamestr_1 = "Bill Edit Register";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLEDITREGREFNO:
                        filenamestr_1 = "Bill Edit Register";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLDELETEREG:
                        filenamestr_1 = "Bill Delete Register";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLDELETEREGREFNO:
                        filenamestr_1 = "Bill Delete Register";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_ITEMWISESALES:
                        filenamestr_1 = "Item Sales Register";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_DATEWISEBILLING:
                        filenamestr_1 = "Date wise Billing Register";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_REVISEDBILLREG:
                        filenamestr_1 = "Revised Billing Register";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_REVISEDBILLREG_REFNO:
                        filenamestr_1 = "Revised Billing Register";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_CAPCOMMIREGISTER:
                        filenamestr_1 = "Captain Commission Register";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_CAPCOMMIREGISTERREFNO:
                        filenamestr_1 = "Captain Commission Register";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLWISESALESSUMMARY:
                        filenamestr_1 = "Bill Wise Sales Summary";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLWISESALESSUMMARY_REFNO:
                        filenamestr_1 = "Bill Wise Sales Summary";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BANQBOOKINGREG:
                        filenamestr_1 = "Banquet Booking Register";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_ITEMGROUPWISESALES:
                        filenamestr_1 = "Item Group Sales Register";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.POS_BILL:
                        formulastr = " {BILL.Rid } = " + ModuleId_1;
                        filenamestr_1 = "Bill No " + ModuleId_1;
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_VATREGISTER:
                        filenamestr_1 = "VAT Register";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_REPORTDEPARTWISESALES:
                        filenamestr_1 = "Report Department Wise Sales";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_CHECKLISTITEMSTOCK:
                        filenamestr_1 = "Checklist Item Stock";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_TABLERUNNINGSUMMARY:
                        filenamestr_1 = "Table Running Summary";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_TABLERUNNINGSUMMARYREFNO:
                        filenamestr_1 = "Table Running Summary";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BANQBOOKING:
                        formulastr = " {BANQBOOKING.RId} = [" + ModuleId_1 + "] and {BANQBOOKINGDTL.DELFLG}=False";
                        filenamestr_1 = "Banquet Booking";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_PURCHASEDETAILSSUPPLIERWISE:
                        filenamestr_1 = "Purchase Details Supplier Wise";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_PURCHASEDETAILSITEMWISE:
                        filenamestr_1 = "Purchase Details Item Wise";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_DATEWISEBUSINESSINFO:
                        filenamestr_1 = "Date Wise Business Info";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_PURCHASEBILLINFO:
                        filenamestr_1 = "Purchase Bill Info";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_PURCHASEDETAILSITEMSUMMARY:
                        filenamestr_1 = "Purchase Details Item Summary";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_SETTLEMENTWISEBILLSUMMARY:
                        filenamestr_1 = "Settlement/Bill Wise Sales Summary";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_SETTLEMENTWISEBILLSUMMARYREFNO:
                        filenamestr_1 = "Settlement/Bill Wise Sales Summary";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_CASHONHANDREGSITER:
                        filenamestr_1 = "Cash On Hand";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_MSTITEM:
                        filenamestr_1 = "Item Master";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLGIVETOCUSTOMERREG:
                        filenamestr_1 = "BillGivetoCustomerRegister";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLGIVETOCUSTOMERREGREFNO:
                        filenamestr_1 = "BillGivetoCustomerRegister";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_ITEMRECIPEUSAGESUMMARY:
                        filenamestr_1 = "ItemRecipeUsageSummary";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_ITEMRECIPEUSAGEDETAILS:
                        filenamestr_1 = "ItemRecipeUsageDetails";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_ITEMWISERECIPEUSAGEDETAILS:
                        filenamestr_1 = "ItemwiseRecipeUsageDetails";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_THERMAL_ITEMWISESALESREGISTER:
                        filenamestr_1 = "ItemWisesalesRegister";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_THERMAL_DAILYBILLREGISTER:
                        filenamestr_1 = "Daily Bill Register";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_THERMAL_DAILYBILLREGISTERREFNO:
                        filenamestr_1 = "Daily Bill Register";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_THERMAL_DEPARTMENTWISESALESREGISTER:
                        filenamestr_1 = "Department Wise Bill Register";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLREGWITHPARCLEDTL:
                        filenamestr_1 = "Bill Register with Parcel Details";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLREGWITHPARCLEDTL_REFNO:
                        filenamestr_1 = "Bill Register with Parcel Details";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLREGDATEWISE:
                        filenamestr_1 = "Bill Register Date Wise Details";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLREGDATEWISE_REFNO:
                        filenamestr_1 = "Bill Register Date Wise Details";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_PURISSUEDTLREG:
                        filenamestr_1 = "Purchase Issue Details Register";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_DEPTPURISSUEDTLREG:
                        filenamestr_1 = "Department Wise Purchase Issue Details Register";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_PURITEMISSUESUMMARY:
                        filenamestr_1 = "Item Issue Summary Register";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_PURITEMDEPTISSUESUMMARY:
                        filenamestr_1 = "Department Item Issue Summary Register";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_PURISSUESTOCKREG:
                        filenamestr_1 = "Purchase Issue Stock Register";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_THERMAL_DATEWISEBILLREG:
                        filenamestr_1 = "Date Wise Bill Register";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_THERMAL_COMPLYBILLREG:
                        filenamestr_1 = "Complyment Bill Register";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_THERMAL_COMPLYBILLREGREFNO:
                        filenamestr_1 = "Complyment Bill Register";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLTYPEWISEREG:
                        filenamestr_1 = "Bill Type Wise Register";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLTYPEWISEREG_REFNO:
                        filenamestr_1 = "Bill Type Wise Register";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_CASHDRAWEROPENREG:
                        filenamestr_1 = "Cash Drawer Open Register";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_PURCHASEUSAGESUMMARY:
                        filenamestr_1 = "PURCHASE Usage Register";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_ITEMWISEPURCHASEITEMUSED:
                        filenamestr_1 = "Item Wise Purchase Usage Register";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BANQBILLINGINFO:
                        filenamestr_1 = "Banquet Billing";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BANQBILLINGINFOREG:
                        filenamestr_1 = "Banquet Billing Register";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.POS_ITEMMASTERWITHBARCODE:
                        filenamestr_1 = "Item Master With Barcode";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.POS_NOOFITEMBARCODE:
                        filenamestr_1 = "No of Barcode";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_DAYWISEPAXSUMMARY:
                        filenamestr_1 = "Day Wise Pax Summary";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_DAYWISEPAXSUMMARYREFNO:
                        filenamestr_1 = "Day Wise Pax Summary";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.POS_ITEMPURRATEWISEREG:
                        filenamestr_1 = "Item Purchase Rate wise Register";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.POS_ITEMWISEBILLREG:
                        filenamestr_1 = "Item wise Bill Register";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.POS_ITEMGROUPPWISEREG:
                        filenamestr_1 = "Item Group wise Bill Register";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.POS_BILLREMARKREG:
                        filenamestr_1 = "Bill Remark Register";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BANQINQUIRY:
                        filenamestr_1 = "Banquet Inquiry ";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_KOTREMARKREG:
                        filenamestr_1 = "KOT Remark Register";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLREMARKREG:
                        filenamestr_1 = "Bill Remark Register";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLREMARKREGREFNO:
                        filenamestr_1 = "Bill Remark Register";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BQBOOKINGINFOREG:
                        filenamestr_1 = "Banquet booking Register";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BQINQUIRYINFOREG:
                        filenamestr_1 = "Banquet Inquiry Register";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BQBOOKING:
                        filenamestr_1 = "Banquet Booking ";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_ITEMPURCHASE:
                        filenamestr_1 = "Item Purchase";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_STOCKISSUE:
                        filenamestr_1 = "Stock Issue";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.POS_ITEMGROUPWISESALES:
                        filenamestr_1 = "Item Group Sales Register";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.POS_STOCKREGISTER:
                        filenamestr_1 = "Stock Register";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_REFBYBILLINFORMATION:
                        filenamestr_1 = "Ref By Bill Info";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_REFBYBILLINFORMATION_REFNO:
                        filenamestr_1 = "Ref By Bill Info";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_KOTEDITDELETEREG:
                        filenamestr_1 = "KotInfo";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLEDITDELETEREG:
                        filenamestr_1 = "BillInfo";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLEDITDELETEREGREFNO:
                        filenamestr_1 = "BillInfo";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_KOTMACHWITHBILLREG:
                        filenamestr_1 = "KotMatchBillInfo";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_KOTMACHWITHBILLREGREFNO:
                        filenamestr_1 = "KotMatchBillInfo";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLTIMEWISESALESINFO:
                        filenamestr_1 = "Billtimewisesaleinfo";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLTIMEWISESALESINFO_REFNO:
                        filenamestr_1 = "Billtimewisesaleinfo";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_TIEUPCOMPANYBILLINFO:
                        filenamestr_1 = "TieupCompanyBillInfo";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_TIEUPCOMPANYBILLINFOREFNO:
                        filenamestr_1 = "TieupCompanyBillInfo";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLWISEDETAILSUMMARY:
                        filenamestr_1 = "billWiseDetailsSummary";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLWISEDETAILSUMMARY_REFNO:
                        filenamestr_1 = "billWiseDetailsSummary";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_OUTPUTVATREPORT:
                        filenamestr_1 = "OutputVatReport";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_PURCHASESTOCKREG:
                        filenamestr_1 = "PurchaseStockReport";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_PURCHASEITEMGROUPSTOCKREG:
                        filenamestr_1 = "PurchaseItemgroupStockReport";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_CASHONHAND:
                        filenamestr_1 = "Cash on Hand";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_PAYMENTINFO:
                        filenamestr_1 = "Payment Voucher";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.POS_40BARCODELABELA4:
                        filenamestr_1 = "40 No of Barcode";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_40BARCODELABELA4:
                        filenamestr_1 = "40 No of Barcode";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_ITEMWISEPURCHASE:
                        filenamestr_1 = "Item Recipe";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_SETTLEMENTOTHER:
                        filenamestr_1 = "Other Settlement Register ";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_SETTLEMENTOTHERREFNO:
                        filenamestr_1 = "Other Settlement Register ";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_INCOMEEXPENCESUMMARYRPT:
                        filenamestr_1 = "IncomeExpenceSummary";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_TABLEWISESALESREPORT:
                        filenamestr_1 = "TableWiseSalesReport";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLCUSTOMERWISE:
                        filenamestr_1 = "BillCustomerWiseReport";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLCUSTOMERWISEREFNO:
                        filenamestr_1 = "BillCustomerWiseReport";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_GSTREPORT:
                        filenamestr_1 = "GST Report";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_GSTREPORT_REFNO:
                        filenamestr_1 = "GST Report";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_SUPPLIERWISEPAYMENTDETAIL:
                        filenamestr_1 = "Supplier Wise Payment Report";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_DATEWISEBILLSUMMARY:
                        filenamestr_1 = "Date Wise Bill Report";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_DATEWISEBILLSUMMARY_REFNO:
                        filenamestr_1 = "Date Wise Bill Report";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLREGWITHSETTINFO_REFNO:
                        filenamestr_1 = "Bill Reg With Sett Info";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLREGWITHSETTINFO:
                        filenamestr_1 = "Bill Reg With Sett Info";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_SALESSUMMARY:
                        filenamestr_1 = "Sales Summary";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_SALESSUMMARYREFNO:
                        filenamestr_1 = "Sales Summary";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_COMPLEMENTRYKOTREG:
                        filenamestr_1 = "Complementry KOT Register";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_COMPLEMENTRYKOTREGREFNO:
                        filenamestr_1 = "Complementry KOT Register";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_MESSCUSTOMERPOSITIONINFORMATION:
                        filenamestr_1 = "Mess Customer Position";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_PURREQORDER:
                        formulastr = " {PURREQORDER.RId} = [" + ModuleId_1 + "] and {PURREQORDERDTL.DELFLG}=False";
                        filenamestr_1 = "PURREQORDER No " + ModuleId_1;
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_PURISTOCKFORMAT:
                        filenamestr_1 = "Purchase Item Stock Format";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_GSTPERWISESUMMARY:
                        filenamestr_1 = "GST PER WISE SUMMARY";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_GSTPERWISESUMMARY_REFNO:
                        filenamestr_1 = "GST PER WISE SUMMARY";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_DATEWISESETTSUMMARY:
                        filenamestr_1 = "DATE WISE SETTLEMENT SUMMARY";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_TIEUPCOMPANYWISESUMMARY:
                        filenamestr_1 = "TIEUP COMPANY WISE SUMMARY";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_TIEUPCOMPANYWISESUMMARYREFNO:
                        filenamestr_1 = "TIEUP COMPANY WISE SUMMARY";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_DAILYDETAILSBILLINFO:
                        filenamestr_1 = "DAILY BILL DETAILS";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_DAILYDETAILSBILLINFOREFNO:
                        filenamestr_1 = "DAILY BILL DETAILS";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_TIEUPCOMPANYWISEDETAILREFNO:
                        filenamestr_1 = "TIEUP COMPANY BILL DETAILS";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_TIEUPCOMPANYWISEDETAIL:
                        filenamestr_1 = "TIEUP COMPANY BILL DETAILS";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_HOMEDELIVERYREPORT:
                        filenamestr_1 = "HOME DELIVERY DETAILS";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_HOMEDELIVERYREPORTREFNO:
                        filenamestr_1 = "HOME DELIVERY DETAILS";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_THERMALBILLWISESETTREFNO:
                        filenamestr_1 = "Bill Wise Settlement";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_THERMALBILLWISESETT:
                        filenamestr_1 = "Bill Wise Settlemnt";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_GSTDATEANDPERWISEREPORT:
                        filenamestr_1 = "GST DETAILS";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_PURITEMSTOCKDATEWISE:
                        filenamestr_1 = "Purchase Item Stock DETAILS";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_ITEMPROFITREPORT:
                        filenamestr_1 = "Item Profit DETAILS";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_INCOMEDTLREPORT:
                        filenamestr_1 = "Income DETAILS";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_EXPENCEDTLREPORT:
                        filenamestr_1 = "Expences DETAILS";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_DATEWISESALARYREPORT:
                        filenamestr_1 = "Date Wise Salary DETAILS";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_DATEWISEATTENDANCEREPORT:
                        filenamestr_1 = "Date Wise Attendance and Salary DETAILS";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BANQPAYMENTREG :
                        filenamestr_1 = "Banquet Payment Register";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_THERMAL_GROUPWISESALESREGISTER:
                        filenamestr_1 = "Group Wise Sales Register";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_ENTRYTICKETDETAILCOLLECTIONRPT:
                        filenamestr_1 = "Ticket Collection Report";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_ENTRYTICKETSUMMARYCOLLECTIONRPT:
                        filenamestr_1 = "Ticket Summary Report";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_COSTUMEBILL:
                        formulastr = " {COUPONBILL.RId} = [" + ModuleId_1 + "] and {COUPONBILL.DELFLG}=False";
                        filenamestr_1 = "Costume Bill";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_ENTRYTICKET:
                        formulastr = " {ENTRYTICKET.RId} = [" + ModuleId_1 + "] and {ENTRYTICKET.DELFLG}=False";
                        filenamestr_1 = "Entry Ticket";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_COSTUMERENTDETAILREPORT:
                        filenamestr_1 = "Details Report";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_DATEWISEBELTTRANSACTION:
                        filenamestr_1 = "Belt Transaction Report";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BELTISSUEREGISTER:
                        filenamestr_1 = "Belt Issue Register Report";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BELTRECHARGEREGISTER:
                        filenamestr_1 = "Belt Recharge Register Report";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BELTSUBMITREGISTER:
                        filenamestr_1 = "Belt Submit Register Report";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_COSTUMEISSUEITEMWISEREGISTER:
                        filenamestr_1 = "Costume Item Wise Register";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_COUPONBILLSUMMARYCOLLECTIONRPT:
                        filenamestr_1 = "Costume Summary";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_COSTUMERETURNREGISTER:
                        filenamestr_1 = "Costume Return";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_ENTRYTICKETPAYMENTCOLLECTIONRPT:
                        filenamestr_1 = "Entry Payment";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_SUPPLIERWISEPAYMENTREGISTER:
                        filenamestr_1 = "SupplierWise Payment";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_CUSTOMERWISESETTLEMENTREFNO:
                        filenamestr_1 = "CustomerwisePayment";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_CUSTOMERWISESETTLEMENT:
                        filenamestr_1 = "CustomerwisePayment";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_THERMALUSERWISEBUSINESSSUMMARY:
                        filenamestr_1 = "User wise Business";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_CHECKLISTITEMDETAILS:
                        filenamestr_1 = "Checklistitemdetails";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_SALARYSLIPDETAILS:
                        filenamestr_1 = "Salary Slip Details";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;

                    default:
                        break;
                }

                // Get the report document

                reportdocument.RecordSelectionFormula = formulastr;
                reportdocument.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                reportdocument.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                DiskFileDestinationOptions objDiskOpt = new DiskFileDestinationOptions();
                objDiskOpt.DiskFileName = filename;
                reportdocument.ExportOptions.DestinationOptions = objDiskOpt;
                reportdocument.Export();

                Cursor.Current = Cursors.Default;
                return true;

            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool ExportReportToPdf(bool IsDefaultRpt, string RptName_1, string ModuleId_1, string RptFilePath_1, DateTime fromdate, DateTime todate, Int64 para1)
        {
            frmImage frmimg = new frmImage();
            frmReport frmRpt = new frmReport();
            ReportDocument reportdocument = new ReportDocument();

            this.Fromdate = fromdate;
            this.Todate = todate;

            clsMsSqlDbFunction objclsdb = new clsMsSqlDbFunction();
            string filename;
            string ReportFilePath;
            string formulastr = "";

            string filenamestr_1 = "";
            //string namestr_1 = "";
            //string sqlstr;
            //DataTable Dt1;

            try
            {
                Cursor.Current = Cursors.WaitCursor;

                this.GetConnectionDetails();
                this.GetSettingDetails();

                clsPublicVariables.enumRMSForms RptEnum = (clsPublicVariables.enumRMSForms)Enum.Parse(typeof(clsPublicVariables.enumRMSForms), RptName_1, true);

                filename = "";
                ReportFilePath = "";
                filenamestr_1 = "";

                this.Fromdate = fromdate;
                this.Todate = todate;

                //if (RptFilePath_1.Trim() != "")
                //{
                //    ReportFilePath = RptFilePath_1;
                //}
                //else
                //{

                switch (RptEnum)
                {
                    case clsPublicVariables.enumRMSForms.RMS_KOT:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\Kot.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_KOT2:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\Kot2.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_KOTB:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\Kotb.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILL:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\Bill.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLB:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\Billb.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLREG:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BillReg.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLREG_REFNO:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BillRegRefno.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLBREG:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BillbReg.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_KOTREG:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\KotReg.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_KOTEDITREG:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\KotEditReg.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_KOTDELETEREG:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\KotDeleteReg.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_KOTBREG:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\KotbReg.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLREGISTER:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BillRegister.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLREGISTER_REFNO:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BillRegisterRefno.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLBREGISTER:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BillbRegister.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_KOTREGISTER:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\KotRegister.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_KOTBREGISTER:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\KotbRegister.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_PURCHAESREGISTER:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\ItemPurchaseRegister.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_PAYMENTREGISTER:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\PaymentRegister.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_CUSTOUTSTANDING:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\Custoutstanding.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLEDITREG:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BillEditReg.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLEDITREGREFNO:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BillEditRegRefno.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLDELETEREG:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BillDeleteReg.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLDELETEREGREFNO:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BillDeleteRegRefno.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_ITEMWISESALES:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\Itemsales.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_DATEWISEBILLING:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\Datewisebilling.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_REVISEDBILLREG:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\RevisedBillRegister.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_REVISEDBILLREG_REFNO:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\RevisedBillRegisterRefno.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_CAPCOMMIREGISTER:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\CapcommiRegister.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_CAPCOMMIREGISTERREFNO:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\CapcommiRegisterRefno.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLWISESALESSUMMARY:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BillWiseSalesSummary.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLWISESALESSUMMARY_REFNO:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BillWiseSalesSummaryRefno.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BANQBOOKINGREG:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BanquetBooking.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_ITEMGROUPWISESALES:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\ItemGroupSales.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.POS_BILL:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\PosBill.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_VATREGISTER:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\Vatregister.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_REPORTDEPARTWISESALES:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\ReportDepartmentSales.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_CHECKLISTITEMSTOCK:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\ChecklistItemStock.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_TABLERUNNINGSUMMARY:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\TableRunningSummary.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_TABLERUNNINGSUMMARYREFNO:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\TableRunningSummaryRefno.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BANQBOOKING:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\Banqbooking.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_PURCHASEDETAILSSUPPLIERWISE:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\Supplierwisepurdtl.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_PURCHASEDETAILSITEMWISE:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\Itemwisepurdtl.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_DATEWISEBUSINESSINFO:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\DatewiseBusinessInfo.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_PURCHASEBILLINFO:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\Purchasebillinfo.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_PURCHASEDETAILSITEMSUMMARY:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\Itemwisepurinfo.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_SETTLEMENTWISEBILLSUMMARY:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\Settlementwisesalessummary.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_SETTLEMENTWISEBILLSUMMARYREFNO:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\SettlementwisesalessummaryRefno.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_CASHONHANDREGSITER:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\Cashonhandregister.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_MSTITEM:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\ItemMaster.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLGIVETOCUSTOMERREG:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BillGivetoCustomerRegister.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLGIVETOCUSTOMERREGREFNO:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BillGivetoCustomerRegisterRefno.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_ITEMRECIPEUSAGESUMMARY:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\Itemrecipeusagesummary.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_ITEMRECIPEUSAGEDETAILS:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\ItemRecipeusageDetails.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_ITEMWISERECIPEUSAGEDETAILS:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\Itemwiserecipedetails.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_THERMAL_ITEMWISESALESREGISTER:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\ThermalItemWiseSalesReg.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_THERMAL_DAILYBILLREGISTER:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\ThermalBillRegister.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_THERMAL_DAILYBILLREGISTERREFNO:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\ThermalBillRegisterRefno.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_THERMAL_DEPARTMENTWISESALESREGISTER:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\ThermalDeptWiseSalesReg.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLREGWITHPARCLEDTL:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BillRegWithParcel.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLREGWITHPARCLEDTL_REFNO:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BillRegWithParcelRefno.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLREGDATEWISE:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BillRegDateWise.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLREGDATEWISE_REFNO:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BillRegDateWiseRefno.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_PURISSUEDTLREG:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\PurIssueDtlRegister.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_DEPTPURISSUEDTLREG:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\DeptWisePurIssueDtl.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_PURITEMISSUESUMMARY:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\PurItemIssueSummary.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_PURITEMDEPTISSUESUMMARY:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\PurItemDeptIssueSummary.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_PURISSUESTOCKREG:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\PurIssueStockReg.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_THERMAL_DATEWISEBILLREG:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\ThermalDateWiseBillReg.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_THERMAL_COMPLYBILLREG:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\ComplymentBillRegister.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_THERMAL_COMPLYBILLREGREFNO:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\ComplymentBillRegisterRefno.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLTYPEWISEREG:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BillRegBillType.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLTYPEWISEREG_REFNO:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BillRegBillTypeRefno.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_CASHDRAWEROPENREG:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\CashDrawerOpenRegister.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_PURCHASEUSAGESUMMARY:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\PurchaseUsageSummary.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_ITEMWISEPURCHASEITEMUSED:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\ItemWisePurchaseUsed.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BANQBILLINGINFO:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BanqBilling.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BANQBILLINGINFOREG:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BanqBillingInfoReg.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.POS_ITEMMASTERWITHBARCODE:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BarcodeItemMaster.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.POS_NOOFITEMBARCODE:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\ItemBarcode.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_DAYWISEPAXSUMMARY:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\DaySummaryPaxWise.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_DAYWISEPAXSUMMARYREFNO:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\DaySummaryPaxWiseRefno.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.POS_ITEMPURRATEWISEREG:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\ItemPurratewiseregister.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.POS_ITEMWISEBILLREG:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BillRegisterItemwise.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.POS_ITEMGROUPPWISEREG:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\ItemGroupwiseRegister.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.POS_BILLREMARKREG:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BillRemarkRegister.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BANQINQUIRY:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BanqInquiry.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_KOTREMARKREG:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\KotRemarkReg.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLREMARKREG:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BillRemarkReg.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLREMARKREGREFNO:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BillRemarkRegRefno.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BQBOOKINGINFOREG:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BqBookingInfoReg.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BQINQUIRYINFOREG:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BqInquiryInfoReg.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BQBOOKING:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BqBooking.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_ITEMPURCHASE:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\ItemPurchase.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_STOCKISSUE:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\Stockissue.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.POS_ITEMGROUPWISESALES:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\ItemGroupSales.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.POS_STOCKREGISTER:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\StockRegister.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_REFBYBILLINFORMATION:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\RefByBillInformation.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_REFBYBILLINFORMATION_REFNO:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\RefByBillInformationRefno.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_KOTEDITDELETEREG:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\KotEditDeleteInformation.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLEDITDELETEREG:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BillEditDeleteInformation.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLEDITDELETEREGREFNO:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BillEditDeleteInformationRefno.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_KOTMACHWITHBILLREG:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\KotMachwithBillReg.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_KOTMACHWITHBILLREGREFNO:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\KotMachwithBillRegRefno.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLTIMEWISESALESINFO:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BillTimeWiseInfo.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLTIMEWISESALESINFO_REFNO:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BillTimeWiseInfoRefno.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_TIEUPCOMPANYBILLINFO:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\TieupCompanyBillInfo.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_TIEUPCOMPANYBILLINFOREFNO:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\TieupCompanyBillInfoRefno.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLWISEDETAILSUMMARY:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BillWiseDetailSummary.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLWISEDETAILSUMMARY_REFNO:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BillWiseDetailSummaryRefno.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_OUTPUTVATREPORT:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\Outputvatreport.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_PURCHASESTOCKREG:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\PurchaseStockReg.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_PURCHASEITEMGROUPSTOCKREG:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\PurchaseItemGroupStockReg.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_CASHONHAND:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\CashOnHand.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_PAYMENTINFO:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\PaymentVoucher.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.POS_40BARCODELABELA4:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\40BARCODELABELA4.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_40BARCODELABELA4:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\40BARCODELABELA4.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_ITEMWISEPURCHASE:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\ItemRecipe.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_SETTLEMENTOTHER:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\OtherSettlementReg.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_SETTLEMENTOTHERREFNO:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\OtherSettlementRegRefno.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_INCOMEEXPENCESUMMARYRPT:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\IncomeExpenceSummary.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_TABLEWISESALESREPORT:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\TableWiseSalesReport.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLCUSTOMERWISE:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\CustomerWiseBillReport.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLCUSTOMERWISEREFNO:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\CustomerWiseBillReportRefno.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_GSTREPORT:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\Gstreport.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_GSTREPORT_REFNO:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\GstreportRefno.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_SUPPLIERWISEPAYMENTDETAIL:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\SupplierwisePendingPayment.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_DATEWISEBILLSUMMARY:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\DateWiseBillSummaryBillNo.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_DATEWISEBILLSUMMARY_REFNO:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\DateWiseBillSummaryRefBillNo.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLREGWITHSETTINFO_REFNO:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BillRegDtlWithSettInfoRefno.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLREGWITHSETTINFO:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BillRegDtlWithSettInfo.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_SALESSUMMARY:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\SalesSummary.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_SALESSUMMARYREFNO:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\SalesSummaryRefno.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_COMPLEMENTRYKOTREG:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\ComplementryKotReg.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_COMPLEMENTRYKOTREGREFNO:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\ComplementryKotRegRefno.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_MESSCUSTOMERPOSITIONINFORMATION:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\MessCustomerPaymentposition.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_PURREQORDER:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\PurchaseOrderReq.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_PURISTOCKFORMAT:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\PurItemStockFormat.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_GSTPERWISESUMMARY:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\GSTPERWISESUMMARY.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_GSTPERWISESUMMARY_REFNO:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\GSTPERWISESUMMARYREFNO.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_DATEWISESETTSUMMARY:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\DateWiseSettlementSummary.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_TIEUPCOMPANYWISESUMMARY:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\TieupCompanyWiseSummary.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_TIEUPCOMPANYWISESUMMARYREFNO:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\TieupCompanyWiseSummaryRefno.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_DAILYDETAILSBILLINFO:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\DailyBillDetails.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_DAILYDETAILSBILLINFOREFNO:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\DailyBillDetailsRefno.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_TIEUPCOMPANYWISEDETAILREFNO:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\TieupCompanyWiseDetailsRefno.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_TIEUPCOMPANYWISEDETAIL:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\TieupCompanyWiseDetails.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_HOMEDELIVERYREPORT:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BillDeliveryRegDtlWithSettInfo.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_HOMEDELIVERYREPORTREFNO:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BillDeliveryRegDtlWithSettInfoRefno.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_THERMALBILLWISESETTREFNO:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\ThermalBilWiseSettlementRefno.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_THERMALBILLWISESETT:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\ThermalBilWiseSettlement.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_TABLEWAITING:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\Tablewaiting.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_TABLERESERVATION:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\TableReservation.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_GSTDATEANDPERWISEREPORT:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\GstPerAndDateWiseReport.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_PURITEMSTOCKDATEWISE:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\PurItemStockDateWise.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_ITEMPROFITREPORT:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\ItemProfitReport.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_INCOMEDTLREPORT:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\IncomeDetailReport.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_EXPENCEDTLREPORT:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\ExpenceDetailReport.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_DATEWISESALARYREPORT:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\DateWiseSalary.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_DATEWISEATTENDANCEREPORT:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\DateWiseAttendanceandsalary.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BANQPAYMENTREG:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BanqPaymentReg.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_THERMAL_GROUPWISESALESREGISTER:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\ThermalGroupWiseSalesRegister.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_ENTRYTICKETDETAILCOLLECTIONRPT:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\EntryTicketDetailedCollection.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_ENTRYTICKETSUMMARYCOLLECTIONRPT:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\EntryTicketSummaryCollection.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_COSTUMEBILL:                        
                        ReportFilePath = clsPublicVariables.ReportPath + "\\CouponBill.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_ENTRYTICKET:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\EntryTicket.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_COSTUMERENTDETAILREPORT:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\CouponBillDetailedCollection.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_DATEWISEBELTTRANSACTION:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\CardBeltHistoryDatewise.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BELTISSUEREGISTER:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BeltIssueRegister.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BELTRECHARGEREGISTER:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BeltRechargeRegister.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BELTSUBMITREGISTER:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\BeltSubmitRegister.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_COSTUMEISSUEITEMWISEREGISTER:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\CostumeIssueItemwiseRegister.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_COUPONBILLSUMMARYCOLLECTIONRPT:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\CouponBillSummary.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_COSTUMERETURNREGISTER:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\CouponReturnRegister.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_ENTRYTICKETPAYMENTCOLLECTIONRPT:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\EntryTicketPaymentCollection.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_SUPPLIERWISEPAYMENTREGISTER:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\SupplierwisePaymentRegister.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_CUSTOMERWISESETTLEMENTREFNO:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\CustomerWiseSettlementRegisterRefno.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_CUSTOMERWISESETTLEMENT:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\CustomerWiseSettlementRegister.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_THERMALUSERWISEBUSINESSSUMMARY:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\UserwiseBusinessSummary.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_CHECKLISTITEMDETAILS:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\Checklistitemdetails.rpt";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_SALARYSLIPDETAILS:
                        ReportFilePath = clsPublicVariables.ReportPath + "\\EmpSalarySlipDetails.rpt";
                        break;

                    default:
                        break;
                }
                //}

                reportdocument.PrintOptions.PrinterName = frmRpt.Instance.cmbPrinter.Text;
                reportdocument.Load(ReportFilePath, OpenReportMethod.OpenReportByDefault);

                //Assign Parameter Value to Report
                this.AssignParameterToReport(reportdocument);

                //Set Margin To Reports
                //this.AssignMarginToReport(reportdocument);

                Database crDatabase;
                Tables crTables;
                TableLogOnInfo crTableLogOnInfo;
                ConnectionInfo crConnectionInfo;
                crConnectionInfo = new ConnectionInfo();
                crConnectionInfo.ServerName = clsPublicVariables.ServerName1;
                crConnectionInfo.DatabaseName = clsPublicVariables.DatabaseName1;
                crConnectionInfo.UserID = clsPublicVariables.UserName1;
                crConnectionInfo.Password = clsPublicVariables.Password1;

                crDatabase = reportdocument.Database;
                crTables = crDatabase.Tables;

                foreach (CrystalDecisions.CrystalReports.Engine.Table crTable in crTables)
                {
                    crTableLogOnInfo = crTable.LogOnInfo;
                    crTableLogOnInfo.ConnectionInfo = crConnectionInfo;
                    crTable.ApplyLogOnInfo(crTableLogOnInfo);
                }

                //crystalReportViewer1.ReportSource = reportdocument;
                //reportdocument.SetDatabaseLogon(UserName1, Password1, ServerName1, DatabaseName1);
                ////Assign Parameter Value to Report
                //this.AssignParameterToReport(reportdocument);
                ////Set Margin To Reports
                //this.AssignMarginToReport(reportdocument);

                // Assign Formula String 

                switch (RptEnum)
                {

                    case clsPublicVariables.enumRMSForms.RMS_BILL:
                        formulastr = " {BILL.RId} = [" + ModuleId_1 + "] and {BILLDTL.DELFLG}=False";
                        filenamestr_1 = "Bill No " + ModuleId_1;
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLB:
                        formulastr = " {BILLb.RId} = [" + ModuleId_1 + "] and {BILLbDTL.DELFLG}=False";
                        filenamestr_1 = "BillB No " + ModuleId_1;
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_KOT:
                        formulastr = " {KOT.RID} = [" + ModuleId_1 + "] and {KOTDTL.DELFLG}=False";
                        filenamestr_1 = "KOT No " + ModuleId_1;
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_KOT2:
                        formulastr = " {KOT.RID} = [" + ModuleId_1 + "] and {KOTDTL.DELFLG}=False";
                        filenamestr_1 = "KOT No " + ModuleId_1;
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_KOTB:
                        formulastr = " {KOTb.RID} = [" + ModuleId_1 + "] and {KOTbDTL.DELFLG}=False";
                        filenamestr_1 = "KOTB No " + ModuleId_1;
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_KOTREGISTER:
                        filenamestr_1 = "Kot Register";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_KOTBREGISTER:
                        filenamestr_1 = "Kotb Register ";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLREGISTER:
                        filenamestr_1 = "Bill Register";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLREGISTER_REFNO:
                        filenamestr_1 = "Bill Register";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLBREGISTER:
                        filenamestr_1 = "BillB Register ";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_SETTLEMENT:
                        filenamestr_1 = "Settlement Register ";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_SETTLEMENTREFNO:
                        filenamestr_1 = "Settlement Register ";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_SETTLEMENTB:
                        filenamestr_1 = "SettlementB Register";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_PURCHAESREGISTER:
                        filenamestr_1 = "Purchase Register";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_PAYMENTREGISTER:
                        filenamestr_1 = "Payment Register";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_CUSTOUTSTANDING:
                        filenamestr_1 = "Customer Outstanding Register";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_KOTEDITREG:
                        filenamestr_1 = "Kot Edit Register";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_KOTDELETEREG:
                        filenamestr_1 = "Kot Delete Register";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLEDITREG:
                        filenamestr_1 = "Bill Edit Register";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLEDITREGREFNO:
                        filenamestr_1 = "Bill Edit Register";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLDELETEREG:
                        filenamestr_1 = "Bill Delete Register";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLDELETEREGREFNO:
                        filenamestr_1 = "Bill Delete Register";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_ITEMWISESALES:
                        filenamestr_1 = "Item Sales Register";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_DATEWISEBILLING:
                        filenamestr_1 = "Date wise Billing Register";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_REVISEDBILLREG:
                        filenamestr_1 = "Revised Billing Register";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_REVISEDBILLREG_REFNO:
                        filenamestr_1 = "Revised Billing Register";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_CAPCOMMIREGISTER:
                        filenamestr_1 = "Captain Commission Register";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_CAPCOMMIREGISTERREFNO:
                        filenamestr_1 = "Captain Commission Register";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLWISESALESSUMMARY:
                        filenamestr_1 = "Bill Wise Sales Summary";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLWISESALESSUMMARY_REFNO:
                        filenamestr_1 = "Bill Wise Sales Summary";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BANQBOOKINGREG:
                        filenamestr_1 = "Banquet Booking Register";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_ITEMGROUPWISESALES:
                        filenamestr_1 = "Item Group Sales Register";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.POS_BILL:
                        formulastr = " {BILL.Rid } = " + ModuleId_1;
                        filenamestr_1 = "Bill No " + ModuleId_1;
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_VATREGISTER:
                        filenamestr_1 = "VAT Register";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_REPORTDEPARTWISESALES:
                        filenamestr_1 = "Report Department Wise Sales";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_CHECKLISTITEMSTOCK:
                        filenamestr_1 = "Checklist Item Stock";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_TABLERUNNINGSUMMARY:
                        filenamestr_1 = "Table Running Summary";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_TABLERUNNINGSUMMARYREFNO:
                        filenamestr_1 = "Table Running Summary";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BANQBOOKING:
                        formulastr = " {BANQBOOKING.RId} = [" + ModuleId_1 + "] and {BANQBOOKINGDTL.DELFLG}=False";
                        filenamestr_1 = "Banquet Booking";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_PURCHASEDETAILSSUPPLIERWISE:
                        filenamestr_1 = "Purchase Details Supplier Wise";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_PURCHASEDETAILSITEMWISE:
                        filenamestr_1 = "Purchase Details Item Wise";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_DATEWISEBUSINESSINFO:
                        filenamestr_1 = "Date Wise Business Info";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_PURCHASEBILLINFO:
                        filenamestr_1 = "Purchase Bill Info";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_PURCHASEDETAILSITEMSUMMARY:
                        filenamestr_1 = "Purchase Details Item Summary";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_SETTLEMENTWISEBILLSUMMARY:
                        filenamestr_1 = "Settlement/Bill Wise Sales Summary";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_SETTLEMENTWISEBILLSUMMARYREFNO:
                        filenamestr_1 = "Settlement/Bill Wise Sales Summary";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_CASHONHANDREGSITER:
                        filenamestr_1 = "Cash On Hand";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_MSTITEM:
                        filenamestr_1 = "Item Master";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLGIVETOCUSTOMERREG:
                        filenamestr_1 = "BillGivetoCustomerRegister";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLGIVETOCUSTOMERREGREFNO:
                        filenamestr_1 = "BillGivetoCustomerRegister";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_ITEMRECIPEUSAGESUMMARY:
                        filenamestr_1 = "ItemRecipeUsageSummary";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_ITEMRECIPEUSAGEDETAILS:
                        filenamestr_1 = "ItemRecipeUsageDetails";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_ITEMWISERECIPEUSAGEDETAILS:
                        filenamestr_1 = "ItemwiseRecipeUsageDetails";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_THERMAL_ITEMWISESALESREGISTER:
                        filenamestr_1 = "ItemWisesalesRegister";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_THERMAL_DAILYBILLREGISTER:
                        filenamestr_1 = "Daily Bill Register";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_THERMAL_DAILYBILLREGISTERREFNO:
                        filenamestr_1 = "Daily Bill Register";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_THERMAL_DEPARTMENTWISESALESREGISTER:
                        filenamestr_1 = "Department Wise Bill Register";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLREGWITHPARCLEDTL:
                        filenamestr_1 = "Bill Register with Parcel Details";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLREGWITHPARCLEDTL_REFNO:
                        filenamestr_1 = "Bill Register with Parcel Details";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLREGDATEWISE:
                        filenamestr_1 = "Bill Register Date Wise Details";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLREGDATEWISE_REFNO:
                        filenamestr_1 = "Bill Register Date Wise Details";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_PURISSUEDTLREG:
                        filenamestr_1 = "Purchase Issue Details Register";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_DEPTPURISSUEDTLREG:
                        filenamestr_1 = "Department Wise Purchase Issue Details Register";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_PURITEMISSUESUMMARY:
                        filenamestr_1 = "Item Issue Summary Register";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_PURITEMDEPTISSUESUMMARY:
                        filenamestr_1 = "Department Item Issue Summary Register";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_PURISSUESTOCKREG:
                        filenamestr_1 = "Purchase Issue Stock Register";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_THERMAL_DATEWISEBILLREG:
                        filenamestr_1 = "Date Wise Bill Register";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_THERMAL_COMPLYBILLREG:
                        filenamestr_1 = "Complyment Bill Register";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_THERMAL_COMPLYBILLREGREFNO:
                        filenamestr_1 = "Complyment Bill Register";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLTYPEWISEREG:
                        filenamestr_1 = "Bill Type Wise Register";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLTYPEWISEREG_REFNO:
                        filenamestr_1 = "Bill Type Wise Register";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_CASHDRAWEROPENREG:
                        filenamestr_1 = "Cash Drawer Open Register";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_PURCHASEUSAGESUMMARY:
                        filenamestr_1 = "PURCHASE Usage Register";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_ITEMWISEPURCHASEITEMUSED:
                        filenamestr_1 = "Item Wise Purchase Usage Register";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BANQBILLINGINFO:
                        filenamestr_1 = "Banquet Billing";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BANQBILLINGINFOREG:
                        filenamestr_1 = "Banquet Billing Register";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.POS_ITEMMASTERWITHBARCODE:
                        filenamestr_1 = "Item Master With Barcode";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.POS_NOOFITEMBARCODE:
                        filenamestr_1 = "No of Barcode";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_DAYWISEPAXSUMMARY:
                        filenamestr_1 = "Day Wise Pax Summary";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_DAYWISEPAXSUMMARYREFNO:
                        filenamestr_1 = "Day Wise Pax Summary";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.POS_ITEMPURRATEWISEREG:
                        filenamestr_1 = "Item Purchase Rate wise Register";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.POS_ITEMWISEBILLREG:
                        filenamestr_1 = "Item wise Bill Register";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.POS_ITEMGROUPPWISEREG:
                        filenamestr_1 = "Item Group wise Bill Register";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.POS_BILLREMARKREG:
                        filenamestr_1 = "Bill Remark Register";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BANQINQUIRY:
                        filenamestr_1 = "Banquet Inquiry ";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_KOTREMARKREG:
                        filenamestr_1 = "KOT Remark Register";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLREMARKREG:
                        filenamestr_1 = "Bill Remark Register";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLREMARKREGREFNO:
                        filenamestr_1 = "Bill Remark Register";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BQBOOKINGINFOREG:
                        filenamestr_1 = "Banquet booking Register";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BQINQUIRYINFOREG:
                        filenamestr_1 = "Banquet Inquiry Register";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BQBOOKING:
                        filenamestr_1 = "Banquet Booking ";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_ITEMPURCHASE:
                        filenamestr_1 = "Item Purchase";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_STOCKISSUE:
                        filenamestr_1 = "Stock Issue";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.POS_ITEMGROUPWISESALES:
                        filenamestr_1 = "Item Group Sales Register";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.POS_STOCKREGISTER:
                        filenamestr_1 = "Stock Register";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_REFBYBILLINFORMATION:
                        filenamestr_1 = "Ref By Bill Info";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_REFBYBILLINFORMATION_REFNO:
                        filenamestr_1 = "Ref By Bill Info";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_KOTEDITDELETEREG:
                        filenamestr_1 = "KotInfo";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLEDITDELETEREG:
                        filenamestr_1 = "BillInfo";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLEDITDELETEREGREFNO:
                        filenamestr_1 = "BillInfo";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_KOTMACHWITHBILLREG:
                        filenamestr_1 = "KotMatchBillInfo";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_KOTMACHWITHBILLREGREFNO:
                        filenamestr_1 = "KotMatchBillInfo";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLTIMEWISESALESINFO:
                        filenamestr_1 = "Billtimewisesaleinfo";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLTIMEWISESALESINFO_REFNO:
                        filenamestr_1 = "Billtimewisesaleinfo";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_TIEUPCOMPANYBILLINFO:
                        filenamestr_1 = "TieupCompanyBillInfo";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_TIEUPCOMPANYBILLINFOREFNO:
                        filenamestr_1 = "TieupCompanyBillInfo";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLWISEDETAILSUMMARY:
                        filenamestr_1 = "billWiseDetailsSummary";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLWISEDETAILSUMMARY_REFNO:
                        filenamestr_1 = "billWiseDetailsSummary";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_OUTPUTVATREPORT:
                        filenamestr_1 = "OutputVatReport";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_PURCHASESTOCKREG:
                        filenamestr_1 = "PurchaseStockReport";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_PURCHASEITEMGROUPSTOCKREG:
                        filenamestr_1 = "PurchaseItemgroupStockReport";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_CASHONHAND:
                        filenamestr_1 = "Cash on Hand";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_PAYMENTINFO:
                        filenamestr_1 = "Payment Voucher";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.POS_40BARCODELABELA4:
                        filenamestr_1 = "40 No of Barcode";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_40BARCODELABELA4:
                        filenamestr_1 = "40 No of Barcode";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_ITEMWISEPURCHASE:
                        filenamestr_1 = "Item Recipe";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_SETTLEMENTOTHER:
                        filenamestr_1 = "Other Settlement Register ";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_SETTLEMENTOTHERREFNO:
                        filenamestr_1 = "Other Settlement Register ";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_INCOMEEXPENCESUMMARYRPT:
                        filenamestr_1 = "IncomeExpenceSummary";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_TABLEWISESALESREPORT:
                        filenamestr_1 = "TableWiseSalesReport";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLCUSTOMERWISE:
                        filenamestr_1 = "BillCustomerWiseReport";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLCUSTOMERWISEREFNO:
                        filenamestr_1 = "BillCustomerWiseReport";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_GSTREPORT:
                        filenamestr_1 = "GST Report";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_GSTREPORT_REFNO:
                        filenamestr_1 = "GST Report";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_SUPPLIERWISEPAYMENTDETAIL:
                        filenamestr_1 = "Supplier Wise Payment Report";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_DATEWISEBILLSUMMARY:
                        filenamestr_1 = "Date Wise Bill Report";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_DATEWISEBILLSUMMARY_REFNO:
                        filenamestr_1 = "Date Wise Bill Report";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLREGWITHSETTINFO_REFNO:
                        filenamestr_1 = "Bill Reg With Sett Info";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_BILLREGWITHSETTINFO:
                        filenamestr_1 = "Bill Reg With Sett Info";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_SALESSUMMARY:
                        filenamestr_1 = "Sales Summary";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_SALESSUMMARYREFNO:
                        filenamestr_1 = "Sales Summary";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_COMPLEMENTRYKOTREG:
                        filenamestr_1 = "Complementry KOT Register";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_COMPLEMENTRYKOTREGREFNO:
                        filenamestr_1 = "Complementry KOT Register";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_MESSCUSTOMERPOSITIONINFORMATION:
                        filenamestr_1 = "MessCustomerPosition";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_PURREQORDER:
                        formulastr = " {PURREQORDER.RId} = [" + ModuleId_1 + "] and {PURREQORDERDTL.DELFLG}=False";
                        filenamestr_1 = "Pur Order No " + ModuleId_1;
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_PURISTOCKFORMAT:
                        filenamestr_1 = "Purchase Item Stock Format";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_GSTPERWISESUMMARY:
                        filenamestr_1 = "GST Per Wise Report";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_GSTPERWISESUMMARY_REFNO:
                        filenamestr_1 = "GST Per Wise Report";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_DATEWISESETTSUMMARY:
                        filenamestr_1 = "Date Wise Settlement Summary";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_TIEUPCOMPANYWISESUMMARY:
                        filenamestr_1 = "Tieup Company Wise Summary";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_TIEUPCOMPANYWISESUMMARYREFNO:
                        filenamestr_1 = "Tieup Company Wise Summary";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_DAILYDETAILSBILLINFO:
                        filenamestr_1 = "Daily Bill Details";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_DAILYDETAILSBILLINFOREFNO:
                        filenamestr_1 = "Daily Bill Details";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_TIEUPCOMPANYWISEDETAILREFNO:
                        filenamestr_1 = "TIEUP COMPANY Bill Details";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_TIEUPCOMPANYWISEDETAIL:
                        filenamestr_1 = "TIEUP COMPANY Bill Details";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_HOMEDELIVERYREPORT:
                        filenamestr_1 = "HOME DELIVERY DETAILS";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_HOMEDELIVERYREPORTREFNO:
                        filenamestr_1 = "HOME DELIVERY DETAILS";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_THERMALBILLWISESETTREFNO:
                        filenamestr_1 = "BILL WISE SETTLEMENT";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_THERMALBILLWISESETT:
                        filenamestr_1 = "BILL WISE SETTLEMENT";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_TABLEWAITING:
                        formulastr = " {TBLWAIT.RId} = [" + ModuleId_1 + "] and {TBLWAIT.DELFLG}=False";
                        filenamestr_1 = "Waiting No " + ModuleId_1;
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_GSTDATEANDPERWISEREPORT:
                         filenamestr_1 = "GST REPORT";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_TABLERESERVATION:
                        formulastr = " {TABLERESERVATION.RId} = [" + ModuleId_1 + "] and {TABLERESERVATION.DELFLG}=False";
                        filenamestr_1 = "Table Reservation " + ModuleId_1;
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_THERMAL_GROUPWISESALESREGISTER:
                        filenamestr_1 = "Group Wise Sales Report";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;                        
                    case clsPublicVariables.enumRMSForms.RMS_COSTUMEBILL:
                        filenamestr_1 = "Costume Bill";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;                        
                    case clsPublicVariables.enumRMSForms.RMS_ENTRYTICKET:
                        filenamestr_1 = "Entry Ticket";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_THERMALUSERWISEBUSINESSSUMMARY:
                        filenamestr_1 = "User Summary";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_CHECKLISTITEMDETAILS:
                        filenamestr_1 = "Checklistitemdetails";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    case clsPublicVariables.enumRMSForms.RMS_SALARYSLIPDETAILS:
                        filenamestr_1 = "Salary Slip Details";
                        filename = AppPath + "\\Export To Pdf\\" + filenamestr_1.Trim() + DateTime.Now.ToString("ddmmyyhhmmss") + ".pdf";
                        break;
                    default:
                        break;
                }

                // Get the report document

                reportdocument.RecordSelectionFormula = formulastr;
                reportdocument.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                reportdocument.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                DiskFileDestinationOptions objDiskOpt = new DiskFileDestinationOptions();
                objDiskOpt.DiskFileName = filename;
                reportdocument.ExportOptions.DestinationOptions = objDiskOpt;
                reportdocument.Export();

                Cursor.Current = Cursors.Default;
                return true;

            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool ReportEmail(bool IsDefaultRpt, enumRMSForms RptName_1, string ModuleId_1, string RptFilePath_1)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                Cursor.Current = Cursors.Default;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void AssignParameterToReport(ReportDocument RptDoc_1)
        {
            try
            {
                long paramcount = 0;
                paramcount = RptDoc_1.ParameterFields.Count;

                if (paramcount > 0)
                {
                    try { RptDoc_1.SetParameterValue("Title1", RptTitle1); }
                    catch (Exception) { }
                    try { RptDoc_1.SetParameterValue("Title2", RptTitle2); }
                    catch (Exception) { }
                    try { RptDoc_1.SetParameterValue("Title3", RptTitle3); }
                    catch (Exception) { }
                    try { RptDoc_1.SetParameterValue("Title4", RptTitle4); }
                    catch (Exception) { }
                    try { RptDoc_1.SetParameterValue("Title5", RptTitle5); }
                    catch (Exception) { }
                    try { RptDoc_1.SetParameterValue("Title6", RptTitle6); }
                    catch (Exception) { }
                    try { RptDoc_1.SetParameterValue("Title7", RptTitle7); }
                    catch (Exception) { }
                    try { RptDoc_1.SetParameterValue("Title8", RptTitle8); }
                    catch (Exception) { }
                    try { RptDoc_1.SetParameterValue("BillTitle", RptBillTitle); }
                    catch (Exception) { }
                    try { RptDoc_1.SetParameterValue("@p_fromdate", this.Fromdate.ToString("yyyy/MM/dd")); }
                    catch (Exception) { }
                    try { RptDoc_1.SetParameterValue("@p_todate", this.Todate.ToString("yyyy/MM/dd")); }
                    catch (Exception) { }
                    try { RptDoc_1.SetParameterValue("Footer1", RPtFooter1); }
                    catch (Exception) { }
                    try { RptDoc_1.SetParameterValue("Footer2", RPtFooter2); }
                    catch (Exception) { }
                    try { RptDoc_1.SetParameterValue("Footer3", RPtFooter3); }
                    catch (Exception) { }
                    try { RptDoc_1.SetParameterValue("Footer4", RPtFooter4); }
                    catch (Exception) { }
                    try { RptDoc_1.SetParameterValue("Footer5", RPtFooter5); }
                    catch (Exception) { }
                    try { RptDoc_1.SetParameterValue("Footer6", RPtFooter6); }
                    catch (Exception) { }
                    try { RptDoc_1.SetParameterValue("Footer7", RPtFooter7); }
                    catch (Exception) { }
                    try { RptDoc_1.SetParameterValue("BqHeader1", RPtBqHeader1); }
                    catch (Exception) { }
                    try { RptDoc_1.SetParameterValue("BqHeader2", RPtBqHeader2); }
                    catch (Exception) { }
                    try { RptDoc_1.SetParameterValue("BqHeader3", RPtBqHeader3); }
                    catch (Exception) { }
                    try { RptDoc_1.SetParameterValue("BqHeader4", RPtBqHeader4); }
                    catch (Exception) { }
                    try { RptDoc_1.SetParameterValue("BqHeader5", RPtBqHeader5); }
                    catch (Exception) { }
                    try { RptDoc_1.SetParameterValue("BqFooter1", RPtBqFooter1); }
                    catch (Exception) { }
                    try { RptDoc_1.SetParameterValue("BqFooter2", RPtBqFooter2); }
                    catch (Exception) { }
                    try { RptDoc_1.SetParameterValue("BqFooter3", RPtBqFooter3); }
                    catch (Exception) { }
                    try { RptDoc_1.SetParameterValue("BqFooter4", RPtBqFooter4); }
                    catch (Exception) { }
                    try { RptDoc_1.SetParameterValue("BqFooter5", RPtBqFooter5); }
                    catch (Exception) { }
                    try { RptDoc_1.SetParameterValue("@p_para1", this.Para1); }
                    catch (Exception) { }
                    try { RptDoc_1.SetParameterValue("echeader1", GENCBHEADER1); }
                    catch (Exception) { }
                    try { RptDoc_1.SetParameterValue("echeader2", GENCBHEADER2); }
                    catch (Exception) { }
                    try { RptDoc_1.SetParameterValue("echeader3", GENCBHEADER3); }
                    catch (Exception) { }
                    try { RptDoc_1.SetParameterValue("ecfooter1", GENCBFOOTER1); }
                    catch (Exception) { }
                    try { RptDoc_1.SetParameterValue("ecfooter2", GENCBFOOTER2); }
                    catch (Exception) { }
                    try { RptDoc_1.SetParameterValue("ecfooter3", GENCBFOOTER3); }
                    catch (Exception) { }
                }
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show("Error occured in AssignParameterToReport() " + ex.Message.ToString(), clsPublicVariables.Project_Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void AssignMarginToReport(ReportDocument RptDoc_1)
        {
            int TopMarg;
            int BtmMarg;
            int LftMarg;

            PageMargins margins;
            frmReport frm1 = new frmReport();

            TopMarg = Convert.ToInt32(Convert.ToDecimal(frm1.Instance.txtTopMargin.Text) * 1440);
            LftMarg = Convert.ToInt32(Convert.ToDecimal(frm1.Instance.txtLeftMargin.Text) * 1440);
            BtmMarg = Convert.ToInt32(Convert.ToDecimal(frm1.Instance.txtFooterMargin.Text) * 1440);

            margins = RptDoc_1.PrintOptions.PageMargins;
            margins.bottomMargin = 0;
            margins.leftMargin = LftMarg;
            margins.topMargin = TopMarg;
            RptDoc_1.PrintOptions.ApplyPageMargins(margins);
        }
    }
}
