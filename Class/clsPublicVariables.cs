using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Reflection;
using System.IO;

namespace ReportDll
{
    public class clsPublicVariables
    {
        public enum enumRMSForms
        {
            RMS_MSTUNIT,
            RMS_MSTDEPT,
            RMS_MSTITEMGROUP,
            RMS_MSTITEM,
            RMS_MSTTABLE,
            RMS_MSTEMPCAT,
            RMS_MSTEMP,
            RMS_MSTITEMPRICELIST,
            RMS_KOT,
            RMS_KOT2,
            RMS_BILL,
            RMS_SETTLEMENT,
            RMS_SETTLEMENTREFNO,
            RMS_KOTREGISTER,
            RMS_BILLREGISTER,
            RMS_BILLREGISTER_REFNO,
            RMS_KOTREG,
            RMS_BILLREG,
            RMS_BILLREG_REFNO,
            RMS_KOTB,
            RMS_BILLB,
            RMS_SETTLEMENTB,
            RMS_KOTBREGISTER,
            RMS_BILLBREGISTER,
            RMS_KOTBREG,
            RMS_BILLBREG,
            RMS_PURCHAESREGISTER,
            RMS_PAYMENTREGISTER,
            RMS_CUSTOUTSTANDING,
            RMS_KOTEDITREG,
            RMS_KOTDELETEREG,
            RMS_BILLEDITREG,
            RMS_BILLEDITREGREFNO,
            RMS_BILLDELETEREG,
            RMS_BILLDELETEREGREFNO,
            RMS_ITEMWISESALES,
            POS_BILLREGISTER,
            POS_BILLBREGISTER,
            POS_BILLREG,
            POS_BILLBREG,
            POS_SETTLEMENT,
            POS_SETTLEMENTB,
            POS_SUPPLIERREGISTER,
            POS_PURCHAESREGISTER,
            POS_PAYMENTREGISTER,
            POS_CUSTOUTSTANDING,
            POS_BILLEDITREG,
            POS_BILLDELETEREG,
            POS_ITEMWISESALES,
            RMS_DATEWISEBILLING,
            RMS_REVISEDBILLREG,
            RMS_REVISEDBILLREG_REFNO,
            RMS_CAPCOMMIREGISTER,
            RMS_CAPCOMMIREGISTERREFNO,
            RMS_BILLWISESALESSUMMARY,
            RMS_BILLWISESALESSUMMARY_REFNO,
            RMS_BANQBOOKINGREG,
            RMS_ITEMGROUPWISESALES,
            POS_BILL,
            RMS_VATREGISTER,
            RMS_REPORTDEPARTWISESALES,
            RMS_CHECKLISTITEMSTOCK,
            RMS_TABLERUNNINGSUMMARY,
            RMS_TABLERUNNINGSUMMARYREFNO,
            RMS_BANQBOOKING,
            RMS_PURCHASEDETAILSSUPPLIERWISE,
            RMS_PURCHASEDETAILSITEMWISE,
            RMS_DATEWISEBUSINESSINFO,
            RMS_PURCHASEBILLINFO,
            RMS_PURCHASEDETAILSITEMSUMMARY,
            RMS_SETTLEMENTWISEBILLSUMMARY,
            RMS_SETTLEMENTWISEBILLSUMMARYREFNO,
            RMS_CASHONHANDREGSITER,
            RMS_BILLGIVETOCUSTOMERREG,
            RMS_BILLGIVETOCUSTOMERREGREFNO,
            RMS_ITEMRECIPEUSAGESUMMARY,
            RMS_ITEMRECIPEUSAGEDETAILS,
            RMS_ITEMWISERECIPEUSAGEDETAILS,
            RMS_THERMAL_ITEMWISESALESREGISTER,
            RMS_THERMAL_DAILYBILLREGISTER,
            RMS_THERMAL_DAILYBILLREGISTERREFNO,
            RMS_THERMAL_GROUPWISESALESREGISTER,
            RMS_THERMAL_DEPARTMENTWISESALESREGISTER,
            RMS_BILLREGWITHPARCLEDTL,
            RMS_BILLREGWITHPARCLEDTL_REFNO,
            RMS_CASHMEMOKOT,
            RMS_CASHMEMOTOKEN,
            RMS_BILLREGDATEWISE,
            RMS_BILLREGDATEWISE_REFNO,
            RMS_PURISSUEDTLREG,
            RMS_DEPTPURISSUEDTLREG,
            RMS_PURITEMISSUESUMMARY,
            RMS_PURITEMDEPTISSUESUMMARY,
            RMS_PURISSUESTOCKREG,
            RMS_THERMAL_DATEWISEBILLREG,
            RMS_THERMAL_COMPLYBILLREG,
            RMS_THERMAL_COMPLYBILLREGREFNO,
            RMS_BILLTYPEWISEREG,
            RMS_BILLTYPEWISEREG_REFNO,
            RMS_CASHDRAWEROPENREG,
            RMS_PURCHASEUSAGESUMMARY,
            RMS_ITEMWISEPURCHASEITEMUSED,
            RMS_BANQBILLINGINFO,
            RMS_BANQBILLINGINFOREG,
            POS_ITEMMASTERWITHBARCODE,
            POS_NOOFITEMBARCODE,
            RMS_DAYWISEPAXSUMMARY,
            RMS_DAYWISEPAXSUMMARYREFNO,
            POS_ITEMPURRATEWISEREG,
            POS_ITEMWISEBILLREG,
            POS_ITEMGROUPPWISEREG,
            POS_BILLREMARKREG,
            RMS_BANQINQUIRY,
            RMS_KOTREMARKREG,
            RMS_BILLREMARKREG,
            RMS_BILLREMARKREGREFNO,
            RMS_BQBOOKINGINFOREG,
            RMS_BQINQUIRYINFOREG,
            RMS_BQBOOKING,
            RMS_ITEMPURCHASE,
            POS_ITEMGROUPWISESALES,
            POS_STOCKREGISTER,
            RMS_REFBYBILLINFORMATION,
            RMS_REFBYBILLINFORMATION_REFNO,
            RMS_KOTEDITDELETEREG,
            RMS_BILLEDITDELETEREG,
            RMS_BILLEDITDELETEREGREFNO,
            RMS_KOTMACHWITHBILLREG,
            RMS_KOTMACHWITHBILLREGREFNO,
            RMS_BILLTIMEWISESALESINFO,
            RMS_BILLTIMEWISESALESINFO_REFNO,
            RMS_TIEUPCOMPANYBILLINFO,
            RMS_TIEUPCOMPANYBILLINFOREFNO,
            RMS_BILLWISEDETAILSUMMARY,
            RMS_BILLWISEDETAILSUMMARY_REFNO,
            RMS_OUTPUTVATREPORT,
            RMS_PURCHASESTOCKREG,
            RMS_CASHONHAND,
            RMS_CASHMEMOKOT2,
            RMS_CASHMEMOSTICKER,
            RMS_PAYMENTINFO,
            POS_40BARCODELABELA4,
            RMS_ITEMWISEPURCHASE,
            RMS_SETTLEMENTOTHER,
            RMS_SETTLEMENTOTHERREFNO,
            RMS_INCOMEEXPENCESUMMARYRPT,
            RMS_TABLEWISESALESREPORT,
            RMS_BILLCUSTOMERWISEREFNO,
            RMS_BILLCUSTOMERWISE,
            RMS_GSTREPORT,
            RMS_SUPPLIERWISEPAYMENTDETAIL,
            RMS_DATEWISEBILLSUMMARY,
            RMS_DATEWISEBILLSUMMARY_REFNO,
            RMS_BILLREGWITHSETTINFO_REFNO,
            RMS_BILLREGWITHSETTINFO,
            RMS_CUSTOUTSTANDINGDTL,
            RMS_CUSTOUTSTANDINGDTLREFNO,
            RMS_BILLDTLCUSTOMERWISEREFNO,
            RMS_BILLDTLCUSTOMERWISE,
            RMS_SALESSUMMARY,
            RMS_SALESSUMMARYREFNO,
            RMS_COMPLEMENTRYKOTREGREFNO,
            RMS_COMPLEMENTRYKOTREG,
            RMS_GSTREPORT_REFNO,
            RMS_MESSCUSTOMERPOSITIONINFORMATION,
            RMS_STOCKISSUE,
            RMS_40BARCODELABELA4,
            RMS_PURCHASEITEMGROUPSTOCKREG,
            RMS_PURREQORDER,
            RMS_PURISTOCKFORMAT,
            RMS_GSTPERWISESUMMARY,
            RMS_GSTPERWISESUMMARY_REFNO,
            RMS_DATEWISESETTSUMMARY,
            RMS_TIEUPCOMPANYWISESUMMARY,
            RMS_TIEUPCOMPANYWISESUMMARYREFNO,
            RMS_DAILYDETAILSBILLINFO,
            RMS_DAILYDETAILSBILLINFOREFNO,
            RMS_TIEUPCOMPANYWISEDETAILREFNO,
            RMS_TIEUPCOMPANYWISEDETAIL,
            RMS_HOMEDELIVERYREPORTREFNO,
            RMS_HOMEDELIVERYREPORT,
            RMS_THERMALBILLWISESETTREFNO,
            RMS_THERMALBILLWISESETT,
            RMS_TABLEWAITING,
            RMS_GSTDATEANDPERWISEREPORT,
            RMS_PURITEMSTOCKDATEWISE,
            RMS_ITEMPROFITREPORT,
            RMS_INCOMEDTLREPORT,
            RMS_EXPENCEDTLREPORT,
            RMS_DATEWISESALARYREPORT,
            RMS_DATEWISEATTENDANCEREPORT,
            RMS_BANQPAYMENTREG,
            RMS_TABLERESERVATION,
            RMS_ENTRYTICKETDETAILCOLLECTIONRPT,
            RMS_ENTRYTICKETSUMMARYCOLLECTIONRPT,
            RMS_ENTRYTICKET,
            RMS_COSTUMEBILL,
            RMS_COSTUMERENTDETAILREPORT,
            RMS_DATEWISEBELTTRANSACTION,
            RMS_BELTISSUEREGISTER,
            RMS_BELTRECHARGEREGISTER,
            RMS_BELTSUBMITREGISTER,
            RMS_COSTUMEISSUEITEMWISEREGISTER,
            RMS_COUPONBILLSUMMARYCOLLECTIONRPT,
            RMS_COSTUMERETURNREGISTER,
            RMS_ENTRYTICKETPAYMENTCOLLECTIONRPT,
            RMS_SUPPLIERWISEPAYMENTREGISTER,
            RMS_CUSTOMERWISESETTLEMENTREFNO,
            RMS_CUSTOMERWISESETTLEMENT,
            RMS_THERMALUSERWISEBUSINESSSUMMARY,
            RMS_CHECKLISTITEMDETAILS,
            RMS_SALARYSLIPDETAILS
        };

        public enum enumReportFilterCondition
        {
            Random = 1
        };

        public static string AppPath = Path.GetDirectoryName(Application.ExecutablePath).ToString();
        public static string ReportPath = AppPath + "\\Report";
        public static string Project_Title = "KRUPA INFOTECH";

        public static string err;
        public static string errForm;
        public static string frmname;
        public static string LineNumber;

        public static string DatabaseType1;
        public static string ServerName1;
        public static string DatabaseName1;
        public static string UserName1;
        public static string Password1;
        public static string DataPort1;

        public static string RptBillTitle;
        public static string RptTitle1;
        public static string RptTitle2;
        public static string RptTitle3;
        public static string RptTitle4;
        public static string RptTitle5;
        public static string RptTitle6;
        public static string RptTitle7;
        public static string RptTitle8;

        public static string RptPrintername;
        public static string RptBillPrintername;
        public static string RptKotPrintername;
        public static string RptKot2Printername;
        public static string RptBarPrintername;
        public static string RptReportPrintername;
        public static string RptlabelPrintername;
        public static string GENCOSTUMEPRINTER = "";
        public static string GENTICKETPRINTER = "";

        public static string GENCBHEADER1 = "";
        public static string GENCBHEADER2 = "";
        public static string GENCBHEADER3 = "";
        public static string GENCBFOOTER1 = "";
        public static string GENCBFOOTER2 = "";
        public static string GENCBFOOTER3 = "";


        public static string RPtFooter1;
        public static string RPtFooter2;
        public static string RPtFooter3;
        public static string RPtFooter4;
        public static string RPtFooter5;
        public static string RPtFooter6;
        public static string RPtFooter7;

        public static string RPtBqHeader1;
        public static string RPtBqHeader2;
        public static string RPtBqHeader3;
        public static string RPtBqHeader4;
        public static string RPtBqHeader5;
        public static string RPtBqHeader6;
        public static string RPtBqHeader7;
        public static string RPtBqHeader8;

        public static string RPtBqFooter1;
        public static string RPtBqFooter2;
        public static string RPtBqFooter3;
        public static string RPtBqFooter4;
        public static string RPtBqFooter5;
        public static string RPtBqFooter6;
        public static string RPtBqFooter7;
        public static string RPtBqFooter8;

        public static string GENBILLNOBASEDON = "";
    }
}
