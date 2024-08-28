using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Net.Mail;
using System.Net;
using System.Collections;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using Microsoft.VisualBasic;
using System.Security.Cryptography;
//using RazorEngine.Templating;


namespace MailingSelectedUsers
{
    class Program
    {
        string body;

        //public static void SendSms(string folio, string fund, string trtype, string ihno, string mobile, string msg)
        //{
           
        //    using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MFDWEB"].ConnectionString))
        //    {
        //        connection.Open();

        //        using (var command = new SqlCommand("KTrack_Mob_InsertSMSLog_V17", connection))
        //        {
        //            command.CommandType = CommandType.StoredProcedure;

        //            command.Parameters.Add("@i_Folio", SqlDbType.VarChar, 150).Value = folio;
        //            command.Parameters.Add("@i_Fund", SqlDbType.VarChar, 150).Value = fund;
        //            command.Parameters.Add("@i_TrNo", SqlDbType.VarChar, 150).Value = "";
        //            command.Parameters.Add("@i_TrType", SqlDbType.VarChar, 150).Value = trtype;
        //            command.Parameters.Add("@i_IHNo", SqlDbType.VarChar, 150).Value = ihno;
        //            command.Parameters.Add("@i_MobileNo", SqlDbType.VarChar, 150).Value = mobile;
        //            command.Parameters.Add("@i_OtpNo", SqlDbType.VarChar, 150).Value = "";
        //            command.Parameters.Add("@i_customfund", SqlDbType.VarChar, 10).Value = fund;
        //            command.Parameters.Add("@i_Msg", SqlDbType.VarChar, 200).Value = msg;

        //            command.ExecuteNonQuery();
        //        }
        //    }
        //}
        public static void insertSmS(String msg, String Mobile, string trtype = "") //
        {
            //DataSet sessionds = HttpContext.Current.Session["inv_ds"] as DataSet;

            //String folio= sessionds.Tables[0].Rows[0]["Folio_No"].ToString();
            //String fund = sessionds.Tables[0].Rows[0]["Fund"].ToString();
            //String smsihno = sessionds.Tables[0].Rows[0]["IHNO"].ToString();

            if (!string.IsNullOrEmpty(Mobile))
            {
                var connection = ConfigurationManager.ConnectionStrings["MFDWEB"].ConnectionString;
                SqlConnection con = new SqlConnection(connection);
                SqlCommand cmd = new SqlCommand();
                DataSet dsms = new DataSet();

                SqlDataAdapter dataadp = new SqlDataAdapter("KTrack_Mob_InsertSMSLog_V17", connection);
                dataadp.SelectCommand.CommandType = CommandType.StoredProcedure;
                dataadp.SelectCommand.Parameters.Add("@i_Folio", SqlDbType.VarChar, 150).Value = "12345667";
                dataadp.SelectCommand.Parameters.Add("@i_Fund", SqlDbType.VarChar, 150).Value = "102";
                dataadp.SelectCommand.Parameters.Add("@i_TrNo", SqlDbType.VarChar, 150).Value = "";
                dataadp.SelectCommand.Parameters.Add("@i_TrType", SqlDbType.VarChar, 150).Value = trtype;
                dataadp.SelectCommand.Parameters.Add("@i_IHNo", SqlDbType.VarChar, 150).Value = "12345";
                dataadp.SelectCommand.Parameters.Add("@i_MobileNo", SqlDbType.VarChar, 150).Value = Mobile;
                dataadp.SelectCommand.Parameters.Add("@i_OtpNo", SqlDbType.VarChar, 150).Value = "";
                dataadp.SelectCommand.Parameters.Add("@i_customfund", SqlDbType.VarChar, 10).Value = "102";
                dataadp.SelectCommand.Parameters.Add("@i_Msg", SqlDbType.VarChar, 200).Value = msg;

                dataadp.Fill(dsms);

                cmd.Connection = con;
                con.Open();
                cmd.CommandTimeout = 90000;
                if (dsms.Tables.Count > 1)
                {
                    dsms.Tables[0].TableName = "Dtinformation";
                    dsms.Tables[1].TableName = "DtData";
                }
                else
                {
                    dsms.Tables[0].TableName = "Dtinformation";
                }

                con.Close();
            }
        }
//        public static void insertSmS(String msg, String Mobile, string trtype = "") //
//        {

//           // Mobile = "9618034828";
//            // Mobile = "8500518924";
           
//            if (!string.IsNullOrEmpty(Mobile))
//            {
//                var connection = ConfigurationManager.ConnectionStrings["conStr"].ConnectionString;
//                SqlConnection con = new SqlConnection(connection);
//                SqlCommand cmd = new SqlCommand();

//                cmd.CommandType = CommandType.Text;
//                //cmd.CommandText = "INSERT INTO [OldBridge].CommunicationLog.dbo.SMS_critical_ProcessedFeeds(pf_Fund,pf_branch, pf_trtype,pf_Mobile, pf_msgtrtype, pf_msg,pf_entdt,pf_priority,pf_acno)    values ('139','WB99' , "+trtype+",RIGHT(" + Mobile + ",10), "+trtype+", " + msg + ",getdate(),1,0)";


//                cmd.CommandText = "INSERT INTO [JMMF].[communicationlog].dbo.[SMS_ProcessedFeeds](pf_Fund,pf_branch, pf_trtype,pf_Mobile, pf_msgtrtype, pf_msg,pf_entdt,pf_priority,pf_acno)    values ('102','WB99' , '" + trtype + "',RIGHT(" + Mobile + ",10), '" + trtype + "', " + msg + ",getdate(),1,0)";

               
////                insert into #SMS_critical_ProcessedFeeds(pf_ihno)  
////select pf_ihno from [Quant].[communicationlog].dbo.[SMS_ProcessedFeeds]  where pf_id=@otp_id
//                cmd.Connection = con;
//                con.Open();
//                cmd.CommandTimeout = 90000;
//                cmd.ExecuteNonQuery();
//                con.Close();
//            }
//        }

        //public static void RegistrationConfirmationEmail(DataRow dr)
        //{

        //    string resultMsg = string.Empty;

        //    string CorporateEmail = "";
        //    if (dr["EmailId"] != null)
        //    {


        //        CorporateEmail = dr["EmailId"].ToString();
        //    }

        //    //#region Admin Alerts

        //    ////decimal Amount = 0;
        //    ////string TranAmountType = string.Empty;
        //    ////string strAACEmails = string.Empty, strAACMobileNo = string.Empty;
        //    ////try
        //    ////{
        //    ////    if (!string.IsNullOrEmpty(Convert.ToString(dr["dd_amt"])))
        //    ////    {
        //    ////        if (dr["dd_amt"].ToString().Split('/').Count() > 1 && dr["dd_amt"].ToString().Split('/')[0] != "")
        //    ////        {
        //    ////            Amount = Convert.ToDecimal(dr["dd_amt"].ToString().Split('/')[0]);
        //    ////            TranAmountType = "A";
        //    ////        }
        //    ////        else
        //    ////        {
        //    ////            Amount = Convert.ToDecimal(dr["dd_amt"].ToString());
        //    ////            TranAmountType = "A";
        //    ////        }
        //    ////        if (Amount == 0 && dr["dd_amt"].ToString().Split('/').Count() > 1 && dr["dd_amt"].ToString().Split('/')[1] != "")
        //    ////        {
        //    ////            Amount = Convert.ToDecimal(dr["dd_amt"].ToString().Split('/')[1]);
        //    ////            TranAmountType = "U";
        //    ////        }
        //    ////        if (Amount == 0)
        //    ////        {
        //    ////            TranAmountType = "U";
        //    ////        }
        //    ////    }
        //    ////    DataTable dtAACEmailAndMobileNo = GetAdminAlertConfigEmailAndMobileNo(dr["dd_fund"].ToString(), Amount, Convert.ToInt64(dr["Ihno"]), TranAmountType);
        //    ////    strAACEmails = (dtAACEmailAndMobileNo != null && dtAACEmailAndMobileNo.Rows.Count != 0) ? dtAACEmailAndMobileNo.Rows[0]["email"].ToString() : string.Empty;
        //    ////    strAACMobileNo = (dtAACEmailAndMobileNo != null && dtAACEmailAndMobileNo.Rows.Count != 0) ? dtAACEmailAndMobileNo.Rows[0]["mobile"].ToString() : string.Empty;
        //    ////}
        //    ////catch (Exception ex)
        //    ////{

        //    ////}

        //    //#endregion
        //    var Fund = "";
        //    var Ihno = "";
        //    var ActorType = "";
        //    if (dr["dd_fund"] != null)
        //    {
        //        Fund = dr["dd_fund"].ToString();
        //    }
        //    if (dr["Ihno"] != null)
        //    {
        //        Ihno = dr["Ihno"].ToString();
        //    }
        //    if (dr["ActorType"] != null)
        //    {
        //        ActorType = dr["ActorType"].ToString();
        //    }
        //    var XHeader = Fund + "_" + Ihno + "_" + ActorType;
        //    try
        //    {
        //        if (Convert.ToString(dr["UserType"]) == "D")
        //        {
        //            #region Distributor Transactions
        //            if (dr["dd_trtype"].ToString() == "Purchase")
        //            {
        //                if (dr["Allowmail"].ToString() == "1")
        //                {
        //                    string relativePath = dr["ActorType"].ToString() == "I" ? ConfigurationManager.AppSettings["Distributor_PurchaseInitiatedEmailToIntiator"] : ConfigurationManager.AppSettings["Distributor_PurchaseInitiated"];
        //                    StreamReader reader = new StreamReader(relativePath);
        //                    string body = PurchasebyDistributor(reader, dr);
        //                    string Subject = "[DIT] Purchase Transaction Recommended";
        //                    bool SentFlag = SendEmail(CorporateEmail, body, Subject, "", "", XHeader);
        //                    if (SentFlag)
        //                    {
        //                        resultMsg = " Mail Sent to  MailId";
        //                    }
        //                }
        //            }
        //            else if (dr["dd_trtype"].ToString() == "Redemption")
        //            {
        //                if (dr["Allowmail"].ToString() == "1")
        //                {
        //                    string relativePath = dr["ActorType"].ToString() == "I" ? ConfigurationManager.AppSettings["Distributor_RedemptionInitiatedEmailToIntiator"] : ConfigurationManager.AppSettings["Distributor_RedemptionInitiated"];
        //                    StreamReader reader = new StreamReader(relativePath);
        //                    string body = RedemptionbyDistributor(reader, dr);
        //                    string Subject = "[DIT] Redemption Transaction Recommended";
        //                    bool SentFlag = SendEmail(CorporateEmail, body, Subject, "", "", XHeader);
        //                    if (SentFlag)
        //                    {
        //                        resultMsg = " Mail Sent to  MailId";
        //                    }
        //                }
        //            }
        //            else if (dr["dd_trtype"].ToString() == "Switch")
        //            {
        //                if (dr["Allowmail"].ToString() == "1")
        //                {
        //                    string relativePath = dr["ActorType"].ToString() == "I" ? ConfigurationManager.AppSettings["Distributor_SwitchInitiatedEmailToIntiator"] : ConfigurationManager.AppSettings["Distributor_SwitchInitiated"];
        //                    StreamReader reader = new StreamReader(relativePath);
        //                    string body = SwitchbyDistributor(reader, dr);
        //                    string Subject = "[DIT] Switch Transaction Recommended";
        //                    bool SentFlag = SendEmail(CorporateEmail, body, Subject, "", "", XHeader);
        //                    if (SentFlag)
        //                    {
        //                        resultMsg = " Mail Sent to  MailId";
        //                    }
        //                }
        //            }
        //            #endregion
        //        }
        //        else
        //        {
        //            #region KropConnect Transactions
        //            // Purchase initiated
        //            if (dr["Action"].ToString() == "initiated" && dr["ActorType"].ToString() != "K" && dr["dd_trtype"].ToString() == "Purchase")
        //            {
        //                if (dr["Allowmail"].ToString() == "1")
        //                {

        //                    if (dr["ActorType"].ToString() == "SU")
        //                    {
        //                        if (dr["BankFlag"].ToString() == "I")
        //                        {

        //                            string relativePath = Convert.ToString(dr["ISAdminalert"]) == "1" ? ConfigurationManager.AppSettings["PurchasebyInitiator_AdminAlerts"] : ConfigurationManager.AppSettings["BajajTemplate"];
        //                            StreamReader reader = new StreamReader(relativePath);
        //                            string body = BajajTransactions(reader, dr);

        //                            string Subject = dr["Subject"].ToString();

        //                            bool SentFlag = SendEmail(CorporateEmail, body, Subject, "", "", XHeader);
        //                            if (SentFlag)
        //                            {
        //                                resultMsg = " Mail Sent to  MailId";
        //                            }
        //                        }
        //                        //if (dr["BankFlag"].ToString() == "H")
        //                        //{

        //                        //    string relativePath = Convert.ToString(dr["ISAdminalert"]) == "1" ? ConfigurationManager.AppSettings["PurchasebyInitiator_AdminAlerts"] : ConfigurationManager.AppSettings["ICICIPurchasebySingleUser"];
        //                        //    StreamReader reader = new StreamReader(relativePath);
        //                        //    string body = PurchaseBySingleUserOnlineBanking(reader, dr);

        //                        //    string Subject = dr["Subject"].ToString();

        //                        //    bool SentFlag = SendEmail(CorporateEmail, body, Subject, "", "");
        //                        //    if (SentFlag)
        //                        //    {
        //                        //        resultMsg = " Mail Sent to  MailId";
        //                        //    }
        //                        //}
        //                        else
        //                        {
        //                            string relativePath = Convert.ToString(dr["ISAdminalert"]) == "1" ? ConfigurationManager.AppSettings["PurchasebyInitiator_AdminAlerts"] : ConfigurationManager.AppSettings["HDFCPurchasebySingleUser"];
        //                            StreamReader reader = new StreamReader(relativePath);
        //                            string body = PurchasebySingleUser(reader, dr);

        //                            string Subject = dr["Subject"].ToString();

        //                            bool SentFlag = SendEmail(CorporateEmail, body, Subject, "", "", XHeader);
        //                            if (SentFlag)
        //                            {
        //                                resultMsg = " Mail Sent to  MailId";
        //                            }
        //                        }

        //                    }
        //                    else
        //                    {
        //                        //Purchase Initiated.
        //                        string relativePath = Convert.ToString(dr["ISAdminalert"]) == "1" ? ConfigurationManager.AppSettings["PurchasebyInitiator_AdminAlerts"] : ConfigurationManager.AppSettings["PurchaseTransactionInitiated"];
        //                        StreamReader reader = new StreamReader(relativePath);
        //                        string body = BajajTransactions(reader, dr);
        //                        string Subject = "Purchase Transaction Initiated";
        //                        if (dr["ActorType"].ToString() != "A")
        //                        {
        //                            bool SentFlag = SendEmail(CorporateEmail, body, Subject, "", "", XHeader);
        //                            if (SentFlag)
        //                            {
        //                                resultMsg = " Mail Sent to  MailId";
        //                            }
        //                            //Admin Alert
        //                            //if (!string.IsNullOrEmpty(strAACEmails))
        //                            //{
        //                            //    relativePath = ConfigurationManager.AppSettings["PurchasebyInitiator_AdminAlerts"];
        //                            //    reader = new StreamReader(relativePath);
        //                            //    body = PurchasebyInitiator(reader, dr);
        //                            //    foreach (string item in strAACEmails.Split(','))
        //                            //    {
        //                            //        SendEmail(item, body, Subject, "", "");
        //                            //    }
        //                            //}
        //                        }
        //                    }
        //                }
        //                // Purchase Initiated
        //                //string sms = "'Purchase Transaction has been initiated by initiator " + dr["Apprname"].ToString() + " and is pending for approval towards the investment in " + dr["dd_schemedesc"].ToString() + ", " + dr["dd_plandesc"].ToString() + ", " + dr["dd_optiondesc"].ToString() + " of Amt : " + dr["dd_amt"].ToString() + " date " + dr["dd_trdate"].ToString() + " in Folio No.  " + dr["dd_acno"].ToString() + ".Korp Connect'";
        //                string sms = "'Dear Investor, " + dr["dd_trtype"].ToString() + " Transaction has been initiated by initiator " + dr["Apprname"].ToString() + " and is pending for approval towards the investment under " + dr["dd_schemedesc"].ToString() + " " + dr["dd_plandesc"].ToString() + " " + dr["dd_optiondesc"].ToString() + " of Rs." + dr["dd_amt"].ToString() + " dated " + dr["dd_trdate"].ToString() + " in Folio-" + dr["dd_acno"].ToString() + ". Regards, Bajaj Finserv Mutual Fund.'";
        //                if (dr["AllowSMS"].ToString() == "1")
        //                {
        //                    insertSmS(sms, dr["MobileNumber"].ToString());
        //                    //Admin Alert
        //                    //if (!string.IsNullOrEmpty(strAACMobileNo))
        //                    //{
        //                    //    foreach (string aCCMobileNo in strAACMobileNo.Split(','))
        //                    //    {
        //                    //        if (!string.IsNullOrEmpty(aCCMobileNo))
        //                    //            insertSmS(sms, aCCMobileNo);
        //                    //    }
        //                    //}
        //                }

        //                if (dr["ActorType"].ToString() == "A")
        //                {
        //                    if (dr["Allowmail"].ToString() == "1")
        //                    {
        //                        string relativePath1 = ConfigurationManager.AppSettings["BajajTemplate"];
        //                        StreamReader reader1 = new StreamReader(relativePath1);
        //                        string body1 = BajajTransactions(reader1, dr);
        //                        string Subject1 = dr["Subject"].ToString();
        //                        bool SentFlag1 = SendEmail(CorporateEmail, body1, Subject1, "", "", XHeader);
        //                        if (SentFlag1)
        //                        {
        //                            resultMsg = " Mail Sent to  MailId";
        //                        }
        //                        //Admin Alerts
        //                        //if (!string.IsNullOrEmpty(strAACEmails))
        //                        //{
        //                        //    foreach (string item in strAACEmails.Split(','))
        //                        //    {
        //                        //        SendEmail(item, body1, Subject1, "", "");
        //                        //    }
        //                        //}
        //                    }
        //                }
        //            }
        //            // Switch Initiated
        //            if (dr["Action"].ToString() == "initiated" && dr["ActorType"].ToString() != "K" && dr["dd_trtype"].ToString() == "Switch")
        //            {
        //                if (dr["Allowmail"].ToString() == "1")
        //                {
        //                    string relativePath = ConfigurationManager.AppSettings["SwitchTransactionInitiated"];
        //                    StreamReader reader = new StreamReader(relativePath);
        //                    string body = BajajTransactions(reader, dr);
        //                    string Subject = "Switch Transaction Initiated";
        //                    bool SentFlag = SendEmail(CorporateEmail, body, Subject, "", "", XHeader);
        //                    if (SentFlag)
        //                    {
        //                        resultMsg = " Mail Sent to  MailId";
        //                    }
        //                    //Admin Alerts
        //                    //if (!string.IsNullOrEmpty(strAACEmails))
        //                    //{
        //                    //    foreach (string item in strAACEmails.Split(','))
        //                    //    {
        //                    //        SendEmail(item, body, Subject, "", "");
        //                    //    }
        //                    //}
        //                }

        //                // Switch Initiated
        //                //string sms = "'Dear Investor, " + dr["dd_trtype"].ToString() + " Transaction has been initiated by initiator " + dr["Apprname"].ToString() + " and is pending for approval towards the investment under " + dr["dd_schemedesc"].ToString() + dr["dd_plandesc"].ToString() + dr["dd_optiondesc"].ToString() + " of Rs." + dr["dd_amt"].ToString() + " dated " + dr["dd_trdate"].ToString() + " in Folio-" + dr["dd_acno"].ToString() + ". Regards, Bajaj Finserv Mutual Fund.'";
        //                string sms = "'Dear Investor, " + dr["dd_trtype"].ToString() + " Transaction has been initiated by initiator " + dr["Apprname"].ToString() + " and is pending for approval towards the investment under " + dr["dd_schemedesc"].ToString() + " " + dr["dd_plandesc"].ToString() + " " + dr["dd_optiondesc"].ToString() + " of Rs." + dr["dd_amt"].ToString() + " dated " + dr["dd_trdate"].ToString() + " in Folio-" + dr["dd_acno"].ToString() + ". Regards, Bajaj Finserv Mutual Fund.'";
        //                if (dr["AllowSMS"].ToString() == "1")
        //                {
        //                    insertSmS(sms, dr["MobileNumber"].ToString());
        //                    //if (!string.IsNullOrEmpty(strAACMobileNo))
        //                    //{
        //                    //    foreach (string aCCMobileNo in strAACMobileNo.Split(','))
        //                    //    {
        //                    //        if (!string.IsNullOrEmpty(aCCMobileNo))
        //                    //            insertSmS(sms, aCCMobileNo);
        //                    //    }
        //                    //}
        //                }



        //                if (dr["ActorType"].ToString() == "A")
        //                {
        //                    if (dr["Allowmail"].ToString() == "1")
        //                    {

        //                        string relativePath2 = ConfigurationManager.AppSettings["BajajTemplate"];
        //                        StreamReader reader2 = new StreamReader(relativePath2);
        //                        string body2 = BajajTransactions(reader2, dr);
        //                        string Subject2 = dr["Subject"].ToString();
        //                        bool SentFlag2 = SendEmail(CorporateEmail, body2, Subject2, "", "", XHeader);
        //                        if (SentFlag2)
        //                        {
        //                            resultMsg = " Mail Sent to  MailId";
        //                        }
        //                        //Admin Alerts
        //                        //if (!string.IsNullOrEmpty(strAACEmails))
        //                        //{
        //                        //    foreach (string item in strAACEmails.Split(','))
        //                        //    {
        //                        //        SendEmail(item, body2, Subject2, "", "");
        //                        //    }
        //                        //}
        //                    }
        //                }
        //            }
        //            if (dr["Action"].ToString() == "initiated" && dr["ActorType"].ToString() != "K" && dr["dd_trtype"].ToString() == "Redemption")
        //            {

        //                if (dr["Allowmail"].ToString() == "1")
        //                {

        //                    string relativePath = Convert.ToString(dr["ISAdminalert"]) == "1" ? ConfigurationManager.AppSettings["RedemptionbyInitiator_AdminAlerts"] : ConfigurationManager.AppSettings["RedemptionTransactionInitiated"];
        //                    StreamReader reader = new StreamReader(relativePath);
        //                    string body = BajajTransactions(reader, dr);
        //                    string Subject = "Redemption Transaction Initiated";
        //                    bool SentFlag = SendEmail(CorporateEmail, body, Subject, "", "", XHeader);
        //                    if (SentFlag)
        //                    {
        //                        resultMsg = " Mail Sent to  MailId";
        //                    }
        //                    //Admin Alerts
        //                    //if (!string.IsNullOrEmpty(strAACEmails))
        //                    //{
        //                    //    relativePath = ConfigurationManager.AppSettings["RedemptionbyInitiator_AdminAlerts"];
        //                    //    reader = new StreamReader(relativePath);
        //                    //    body = RedemptionbyInitiator(reader, dr);
        //                    //    foreach (string item in strAACEmails.Split(','))
        //                    //    {
        //                    //        SendEmail(item, body, Subject, "", "");
        //                    //    }
        //                    //}
        //                }
        //                string sms = "'Dear Investor, " + dr["dd_trtype"].ToString() + " Transaction has been initiated by initiator " + dr["Apprname"].ToString() + " and is pending for approval towards the investment under " + dr["dd_schemedesc"].ToString() + " " + dr["dd_plandesc"].ToString() + " " + dr["dd_optiondesc"].ToString() + " of Rs." + dr["dd_amt"].ToString() + " dated " + dr["dd_trdate"].ToString() + " in Folio-" + dr["dd_acno"].ToString() + ". Regards, Bajaj Finserv Mutual Fund.'";

        //                if (dr["AllowSMS"].ToString() == "1")
        //                {
        //                    insertSmS(sms, dr["MobileNumber"].ToString());
        //                    //Admin Alerts
        //                    //if (!string.IsNullOrEmpty(strAACMobileNo))
        //                    //{
        //                    //    foreach (string aCCMobileNo in strAACMobileNo.Split(','))
        //                    //    {
        //                    //        if (!string.IsNullOrEmpty(aCCMobileNo))
        //                    //            insertSmS(sms, aCCMobileNo);
        //                    //    }
        //                    //}
        //                }

        //                if (dr["ActorType"].ToString() == "A")
        //                {
        //                    if (dr["Allowmail"].ToString() == "1")
        //                    {

        //                        string relativePath3 = ConfigurationManager.AppSettings["BajajTemplate"];
        //                        StreamReader reader3 = new StreamReader(relativePath3);
        //                        string body3 = BajajTransactions(reader3, dr);
        //                        string Subject3 = dr["Subject"].ToString();
        //                        bool SentFlag3 = SendEmail(CorporateEmail, body3, Subject3, "", "", XHeader);
        //                        if (SentFlag3)
        //                        {
        //                            resultMsg = " Mail Sent to  MailId";
        //                        }
        //                        //Admin Alerts
        //                        //if (!string.IsNullOrEmpty(strAACEmails))
        //                        //{
        //                        //    foreach (string item in strAACEmails.Split(','))
        //                        //    {
        //                        //        SendEmail(item, body3, Subject3, "", "");
        //                        //    }

        //                        //}
        //                    }
        //                }

        //            }
        //            if (dr["Action"].ToString() == "initiated" && dr["ActorType"].ToString() != "K" && dr["dd_trtype"].ToString() == "SWP")
        //            {
        //                if (dr["Allowmail"].ToString() == "1")
        //                {

        //                    string relativePath = ConfigurationManager.AppSettings["SWPbyInitiator"];
        //                    StreamReader reader = new StreamReader(relativePath);
        //                    string body = SWPbyInitiator(reader, dr);
        //                    string Subject = dr["Subject"].ToString();
        //                    bool SentFlag = SendEmail(CorporateEmail, body, Subject, "", "", XHeader);
        //                    if (SentFlag)
        //                    {
        //                        resultMsg = " Mail Sent to  MailId";
        //                    }
        //                }
        //            }
        //            if (dr["Action"].ToString() == "initiated" && dr["ActorType"].ToString() != "K" && dr["dd_trtype"].ToString() == "STP")
        //            {
        //                if (dr["Allowmail"].ToString() == "1")
        //                {

        //                    string relativePath = ConfigurationManager.AppSettings["STPbyInitiator"];
        //                    StreamReader reader = new StreamReader(relativePath);
        //                    string body = STPbyInitiator(reader, dr);
        //                    string Subject = dr["Subject"].ToString();
        //                    bool SentFlag = SendEmail(CorporateEmail, body, Subject, "", "", XHeader);
        //                    if (SentFlag)
        //                    {
        //                        resultMsg = " Mail Sent to  MailId";
        //                    }
        //                }
        //            }
        //            if (dr["Action"].ToString() == "initiated" && dr["ActorType"].ToString() != "K" && dr["dd_trtype"].ToString() == "NEW")
        //            {
        //                //string relativePath = ConfigurationManager.AppSettings["NewPurchaseapprovedbyFinalApprover"];
        //                //StreamReader reader = new StreamReader(relativePath);
        //                //string body = NewPurchaseapprovedbyFinalApprover(reader, dr);
        //                if (dr["Allowmail"].ToString() == "1")
        //                {

        //                    string relativePath = ConfigurationManager.AppSettings["BajajTemplate"];
        //                    StreamReader reader = new StreamReader(relativePath);
        //                    string body = BajajTransactions(reader, dr);
        //                    string Subject = dr["Subject"].ToString();
        //                    bool SentFlag = SendEmail(CorporateEmail, body, Subject, "", "", XHeader);
        //                    if (SentFlag)
        //                    {
        //                        resultMsg = " Mail Sent to  MailId";
        //                    }
        //                }
        //                //string sms = "'Purchase Transaction has been initiated by initiator " + dr["Apprname"].ToString() + " and is pending for approval towards the investment in " + dr["dd_schemedesc"].ToString() + ", " + dr["dd_plandesc"].ToString() + ", " + dr["dd_optiondesc"].ToString() + " of Amt : " + dr["dd_amt"].ToString() + " date " + dr["dd_trdate"].ToString() + " in Folio No.  " + dr["dd_acno"].ToString() + ".Korp Connect'";
        //                string sms = "' Dear Investor, Purchase Transaction has been initiated by initiator " + dr["Apprname"].ToString() + " and is pending for approval towards the investment under " + dr["dd_schemedesc"].ToString() + ", " + dr["dd_plandesc"].ToString() + ", " + dr["dd_optiondesc"].ToString() + " of Rs. " + dr["dd_amt"].ToString() + " dated " + dr["dd_trdate"].ToString() + " in Folio No.  " + dr["dd_acno"].ToString() + ". Regards, Bajaj Finserv Mutual Fund.'";

        //                if (dr["AllowSMS"].ToString() == "1")
        //                {
        //                    insertSmS(sms, dr["MobileNumber"].ToString());
        //                }
        //                if (dr["ActorType"].ToString() == "A")
        //                {
        //                    if (dr["Allowmail"].ToString() == "1")
        //                    {
        //                        string relativePath4 = ConfigurationManager.AppSettings["PurchasebyInitiatorforApprover"];
        //                        StreamReader reader4 = new StreamReader(relativePath4);
        //                        string body4 = PurchasebyInitiator(reader4, dr);
        //                        string Subject4 = dr["Subject"].ToString();
        //                        bool SentFlag4 = SendEmail(CorporateEmail, body4, Subject4, "", "", XHeader);
        //                        if (SentFlag4)
        //                        {
        //                            resultMsg = " Mail Sent to  MailId";
        //                        }
        //                    }
        //                }
        //            }
        //            if (dr["ActorType"].ToString() == "K" && dr["dd_trtype"].ToString() == "Purchase")
        //            {
        //                if (dr["Allowmail"].ToString() == "1")
        //                {
        //                    string relativePath = ConfigurationManager.AppSettings["BajajTemplate"];
        //                    StreamReader reader = new StreamReader(relativePath);
        //                    string body = BajajTransactions(reader, dr);
        //                    string Subject = dr["Subject"].ToString();
        //                    bool SentFlag = SendEmail(CorporateEmail, body, Subject, "", "", XHeader);
        //                    if (SentFlag)
        //                    {
        //                        resultMsg = " Mail Sent to  MailId";
        //                    }
        //                    //Admin Alerts
        //                    //if (!string.IsNullOrEmpty(strAACEmails))
        //                    //{
        //                    //    foreach (string item in strAACEmails.Split(','))
        //                    //    {
        //                    //        SendEmail(item, body, Subject, "", "");
        //                    //    }
        //                    //}
        //                }

        //                string sms = "'Dear Investor, We have received your " + dr["dd_trtype"].ToString() + " request under " + dr["dd_schemedesc"].ToString() + " " + dr["dd_plandesc"].ToString() + " " + dr["dd_optiondesc"].ToString() + " of Rs." + dr["dd_amt"].ToString() + " dated" + dr["dd_trdate"].ToString() + " in Folio-" + dr["dd_acno"].ToString() + ". You shall receive a confirmation SMS once it is processed. Regards, Bajaj Finserv Mutual Fund.'";


        //                if (dr["AllowSMS"].ToString() == "1")
        //                {
        //                    insertSmS(sms, dr["MobileNumber"].ToString());
        //                    //Admin Alerts
        //                    //if (!string.IsNullOrEmpty(strAACMobileNo))
        //                    //{
        //                    //    foreach (string aCCMobileNo in strAACMobileNo.Split(','))
        //                    //    {
        //                    //        if (!string.IsNullOrEmpty(aCCMobileNo))
        //                    //            insertSmS(sms, aCCMobileNo);
        //                    //    }
        //                    //}
        //                }

        //            }
        //            if (dr["ActorType"].ToString() == "K" && dr["dd_trtype"].ToString() == "Redemption")
        //            {
        //                if (dr["Allowmail"].ToString() == "1")
        //                {

        //                    string relativePath = ConfigurationManager.AppSettings["BajajTemplate"];
        //                    StreamReader reader = new StreamReader(relativePath);
        //                    string body = BajajTransactions(reader, dr);
        //                    string Subject = dr["Subject"].ToString();
        //                    bool SentFlag = SendEmail(CorporateEmail, body, Subject, "", "", XHeader);
        //                    if (SentFlag)
        //                    {
        //                        resultMsg = " Mail Sent to  MailId";
        //                    }
        //                    //Admin Alerts
        //                    //if (!string.IsNullOrEmpty(strAACEmails))
        //                    //{
        //                    //    foreach (string item in strAACEmails.Split(','))
        //                    //    {
        //                    //        SendEmail(item, body, Subject, "", "");
        //                    //    }
        //                    //}
        //                }

        //                string sms = "'Dear Investor, We have received your " + dr["dd_trtype"].ToString() + " request under " + dr["dd_schemedesc"].ToString() + " " + dr["dd_plandesc"].ToString() + " " + dr["dd_optiondesc"].ToString() + " of Rs." + dr["dd_amt"].ToString() + " dated" + dr["dd_trdate"].ToString() + " in Folio-" + dr["dd_acno"].ToString() + ". You shall receive a confirmation SMS once it is processed. Regards, Bajaj Finserv Mutual Fund.'";

        //                if (dr["AllowSMS"].ToString() == "1")
        //                {
        //                    insertSmS(sms, dr["MobileNumber"].ToString());
        //                    //Admin Alerts
        //                    //if (!string.IsNullOrEmpty(strAACMobileNo))
        //                    //{
        //                    //    foreach (string aCCMobileNo in strAACMobileNo.Split(','))
        //                    //    {
        //                    //        if (!string.IsNullOrEmpty(aCCMobileNo))
        //                    //            insertSmS(sms, aCCMobileNo);
        //                    //    }
        //                    //}
        //                }

        //            }
        //            if (dr["ActorType"].ToString() == "K" && dr["dd_trtype"].ToString() == "Switch")
        //            {
        //                if (dr["Allowmail"].ToString() == "1")
        //                {


        //                    string relativePath = ConfigurationManager.AppSettings["BajajTemplate"];
        //                    StreamReader reader = new StreamReader(relativePath);
        //                    string body = BajajTransactions(reader, dr);
        //                    string Subject = dr["Subject"].ToString();
        //                    bool SentFlag = SendEmail(CorporateEmail, body, Subject, "", "", XHeader);
        //                    if (SentFlag)
        //                    {
        //                        resultMsg = " Mail Sent to  MailId";
        //                    }
        //                    //Admin Alerts
        //                    //if (!string.IsNullOrEmpty(strAACEmails))
        //                    //{
        //                    //    foreach (string item in strAACEmails.Split(','))
        //                    //    {
        //                    //        SendEmail(item, body, Subject, "", "");
        //                    //    }
        //                    //}
        //                }

        //                string sms = "'Dear Investor,We have received your Switch out request in   " + dr["dd_schemedesc"].ToString() + ',' + dr["dd_plandesc"].ToString() + "," + dr["dd_optiondesc"].ToString() + " of Amt/Units:" + dr["dd_amt"].ToString() + " date" + dr["dd_trdate"].ToString() + " in Folio No." + dr["dd_acno"].ToString() + " You shall receive a confirmation SMS once it is processed. Regards, Bajaj Finserv Mutual Fund.'";

        //                if (dr["AllowSMS"].ToString() == "1")
        //                {
        //                    insertSmS(sms, dr["MobileNumber"].ToString());
        //                    //Admin Alerts
        //                    //if (!string.IsNullOrEmpty(strAACMobileNo))
        //                    //{
        //                    //    foreach (string aCCMobileNo in strAACMobileNo.Split(','))
        //                    //    {
        //                    //        if (!string.IsNullOrEmpty(aCCMobileNo))
        //                    //            insertSmS(sms, aCCMobileNo);
        //                    //    }
        //                    //}
        //                }

        //            }
        //            if (dr["ActorType"].ToString() == "K" && dr["dd_trtype"].ToString() == "New")
        //            {
        //                if (dr["Allowmail"].ToString() == "1")
        //                {


        //                    string relativePath = ConfigurationManager.AppSettings["BajajTemplate"];
        //                    StreamReader reader = new StreamReader(relativePath);
        //                    string body = BajajTransactions(reader, dr);
        //                    string Subject = dr["Subject"].ToString();
        //                    bool SentFlag = SendEmail(CorporateEmail, body, Subject, "", "", XHeader);
        //                    if (SentFlag)
        //                    {
        //                        resultMsg = " Mail Sent to  MailId";
        //                    }
        //                }

        //                string sms = "'Dear Investor, We have received your " + dr["dd_trtype"].ToString() + " request under " + dr["dd_schemedesc"].ToString() + " " + dr["dd_plandesc"].ToString() + " " + dr["dd_optiondesc"].ToString() + " of Rs." + dr["dd_amt"].ToString() + " dated" + dr["dd_trdate"].ToString() + " in Folio-" + dr["dd_acno"].ToString() + ". You shall receive a confirmation SMS once it is processed. Regards, Bajaj Finserv Mutual Fund.'";
        //                if (dr["AllowSMS"].ToString() == "1")
        //                {
        //                    insertSmS(sms, dr["MobileNumber"].ToString());
        //                }
        //            }
        //            //Purchase Approved
        //            if (dr["Action"].ToString() == "Approved" && dr["dd_trtype"].ToString() == "Purchase")
        //            {
        //                if (dr["Status"].ToString() == "Pending")
        //                {
        //                    if (dr["Allowmail"].ToString() == "1")
        //                    {


        //                        string relativePath = ConfigurationManager.AppSettings["PurchaseTransactionPending"];//Purchase Transaction Pending
        //                        StreamReader reader = new StreamReader(relativePath);
        //                        string body = BajajTransactions(reader, dr);
        //                        string Subject = "Purchase Transaction Approved";
        //                        bool SentFlag = SendEmail(CorporateEmail, body, Subject, "", "", XHeader);
        //                        if (SentFlag)
        //                        {
        //                            resultMsg = " Mail Sent to  MailId";
        //                        }
        //                        //Admin Alerts
        //                        //if (!string.IsNullOrEmpty(strAACEmails))
        //                        //{
        //                        //    foreach (string item in strAACEmails.Split(','))
        //                        //    {
        //                        //        SendEmail(item, body, Subject, "", "");
        //                        //    }
        //                        //}
        //                    }
        //                    //Purchase Approved
        //                    string sms = "'Dear Investor, " + dr["dd_trtype"].ToString() + " Transaction has been approved for your investment under " + dr["dd_schemedesc"].ToString() + " " + dr["dd_plandesc"].ToString() + " " + dr["dd_optiondesc"].ToString() + " of Rs." + dr["dd_amt"].ToString() + "  dated " + dr["dd_trdate"].ToString() + " in Folio-" + dr["dd_acno"].ToString() + ". Regards, Bajaj Finserv Mutual Fund.'";

        //                    if (dr["AllowSMS"].ToString() == "1")
        //                    {
        //                        insertSmS(sms, dr["MobileNumber"].ToString());
        //                        //Admin Alerts
        //                        //if (!string.IsNullOrEmpty(strAACMobileNo))
        //                        //{
        //                        //    foreach (string aCCMobileNo in strAACMobileNo.Split(','))
        //                        //    {
        //                        //        if (!string.IsNullOrEmpty(aCCMobileNo))
        //                        //            insertSmS(sms, aCCMobileNo);
        //                        //    }
        //                        //}
        //                    }

        //                }
        //                if (dr["Status"].ToString() == "Approved")
        //                {
        //                    //string relativePath = ConfigurationManager.AppSettings["AdditionalPurchaseapprovedbyFinalApprover"];
        //                    //StreamReader reader = new StreamReader(relativePath);
        //                    //string body = AdditionalPurchaseapprovedbyFinalApprover(reader, dr);
        //                    if (dr["Allowmail"].ToString() == "1")
        //                    {
        //                        string relativePath = "";
        //                        if (dr["BankFlag"].ToString() == "I")
        //                        {

        //                            relativePath = ConfigurationManager.AppSettings["PurchaseTransactionApproved"];
        //                            // WriteLog(dr["BankFlag"].ToString(), relativePath, "");
        //                        }
        //                        else if (dr["BankFlag"].ToString() == "H")
        //                        {
        //                            relativePath = ConfigurationManager.AppSettings["PurchaseTransactionApproved"];
        //                            // WriteLog(dr["BankFlag"].ToString(), relativePath, "");

        //                        }
        //                        else
        //                        {
        //                            relativePath = ConfigurationManager.AppSettings["PurchaseTransactionApproved"];
        //                        }

        //                        //string relativePath = ConfigurationManager.AppSettings["Purchaseapprovalbyapprovers"];
        //                        StreamReader reader = new StreamReader(relativePath);
        //                        string body = BajajTransactions(reader, dr);
        //                        string Subject = "Purchase Transaction Approved";
        //                        bool SentFlag = SendEmail(CorporateEmail, body, Subject, "", "", XHeader);
        //                        if (SentFlag)
        //                        {
        //                            resultMsg = " Mail Sent to  MailId";
        //                        }
        //                        //Admin Alerts
        //                        //if (!string.IsNullOrEmpty(strAACEmails))
        //                        //{
        //                        //    foreach (string item in strAACEmails.Split(','))
        //                        //    {
        //                        //        SendEmail(item, body, Subject, "", "");
        //                        //    }
        //                        //}
        //                    }
        //                    string sms = "'Dear Investor, " + dr["dd_trtype"].ToString() + " Transaction has been approved for your investment under " + dr["dd_schemedesc"].ToString() + " " + dr["dd_plandesc"].ToString() + " " + dr["dd_optiondesc"].ToString() + " of Rs." + dr["dd_amt"].ToString() + "  dated " + dr["dd_trdate"].ToString() + " in Folio-" + dr["dd_acno"].ToString() + ". Regards, Bajaj Finserv Mutual Fund.'";

        //                    if (dr["AllowSMS"].ToString() == "1")
        //                    {
        //                        insertSmS(sms, dr["MobileNumber"].ToString());
        //                        //Admin Alerts
        //                        //if (!string.IsNullOrEmpty(strAACMobileNo))
        //                        //{
        //                        //    foreach (string aCCMobileNo in strAACMobileNo.Split(','))
        //                        //    {
        //                        //        if (!string.IsNullOrEmpty(aCCMobileNo))
        //                        //            insertSmS(sms, aCCMobileNo);
        //                        //    }
        //                        //}
        //                    }

        //                }

        //            }
        //            if (dr["Action"].ToString() == "Approved" && dr["dd_trtype"].ToString() == "Switch")
        //            {
        //                if (dr["Status"].ToString() == "Pending")
        //                {
        //                    if (dr["Allowmail"].ToString() == "1")
        //                    {

        //                        string relativePath = ConfigurationManager.AppSettings["SwitchTransactionPending"];// Switch Tranaction Pending
        //                        StreamReader reader = new StreamReader(relativePath);
        //                        string body = BajajTransactions(reader, dr);
        //                        string Subject = dr["Subject"].ToString();
        //                        bool SentFlag = SendEmail(CorporateEmail, body, Subject, "", "", XHeader);
        //                        if (SentFlag)
        //                        {
        //                            resultMsg = " Mail Sent to  MailId";
        //                        }
        //                        //Admin Alerts
        //                        //if (!string.IsNullOrEmpty(strAACEmails))
        //                        //{
        //                        //    foreach (string item in strAACEmails.Split(','))
        //                        //    {
        //                        //        SendEmail(item, body, Subject, "", "");
        //                        //    }
        //                        //}
        //                    }
        //                    string sms = "'Dear Investor, " + dr["dd_trtype"].ToString() + " Transaction has been approved for your investment under " + dr["dd_schemedesc"].ToString() + " " + dr["dd_plandesc"].ToString() + " " + dr["dd_optiondesc"].ToString() + " of Rs." + dr["dd_amt"].ToString() + "  dated " + dr["dd_trdate"].ToString() + " in Folio-" + dr["dd_acno"].ToString() + ". Regards, Bajaj Finserv Mutual Fund.'";

        //                    if (dr["AllowSMS"].ToString() == "1")
        //                    {
        //                        insertSmS(sms, dr["MobileNumber"].ToString());
        //                        //Admin Alerts
        //                        //if (!string.IsNullOrEmpty(strAACMobileNo))
        //                        //{
        //                        //    foreach (string aCCMobileNo in strAACMobileNo.Split(','))
        //                        //    {
        //                        //        if (!string.IsNullOrEmpty(aCCMobileNo))
        //                        //            insertSmS(sms, aCCMobileNo);
        //                        //    }
        //                        //}
        //                    }

        //                }
        //                if (dr["Status"].ToString() == "Approved")
        //                {
        //                    if (dr["Allowmail"].ToString() == "1")
        //                    {

        //                        string relativePath = ConfigurationManager.AppSettings["SwitchTransactionApproved"];// Switch Transaction Approved
        //                        StreamReader reader = new StreamReader(relativePath);
        //                        string body = BajajTransactions(reader, dr);
        //                        string Subject = dr["Subject"].ToString();
        //                        bool SentFlag = SendEmail(CorporateEmail, body, Subject, "", "", XHeader);
        //                        if (SentFlag)
        //                        {
        //                            resultMsg = " Mail Sent to  MailId";
        //                        }
        //                        //Admin Alerts
        //                        //if (!string.IsNullOrEmpty(strAACEmails))
        //                        //{
        //                        //    foreach (string item in strAACEmails.Split(','))
        //                        //    {
        //                        //        SendEmail(item, body, Subject, "", "");
        //                        //    }
        //                        //}
        //                    }
        //                    string sms = "'Dear Investor, " + dr["dd_trtype"].ToString() + " Transaction has been approved for your investment under " + dr["dd_schemedesc"].ToString() + " " + dr["dd_plandesc"].ToString() + " " + dr["dd_optiondesc"].ToString() + " of Rs." + dr["dd_amt"].ToString() + "  dated " + dr["dd_trdate"].ToString() + " in Folio-" + dr["dd_acno"].ToString() + ". Regards, Bajaj Finserv Mutual Fund.'";


        //                    if (dr["AllowSMS"].ToString() == "1")
        //                    {
        //                        insertSmS(sms, dr["MobileNumber"].ToString());
        //                        //Admin Alerts
        //                        //if (!string.IsNullOrEmpty(strAACMobileNo))
        //                        //{
        //                        //    foreach (string aCCMobileNo in strAACMobileNo.Split(','))
        //                        //    {
        //                        //        if (!string.IsNullOrEmpty(aCCMobileNo))
        //                        //            insertSmS(sms, aCCMobileNo);
        //                        //    }
        //                        //}
        //                    }

        //                }

        //            }
        //            //Redemption Approved
        //            if (dr["Action"].ToString() == "Approved" && dr["dd_trtype"].ToString() == "Redemption")
        //            {
        //                if (dr["Status"].ToString() == "Pending")
        //                {
        //                    if (dr["Allowmail"].ToString() == "1")
        //                    {

        //                        string relativePath = ConfigurationManager.AppSettings["RedemptionTransactionApproved"];//Redemption Transaction Pending
        //                        StreamReader reader = new StreamReader(relativePath);
        //                        string body = BajajTransactions(reader, dr);
        //                        string Subject = "Redemption Transaction Approved";
        //                        bool SentFlag = SendEmail(CorporateEmail, body, Subject, "", "", XHeader);
        //                        if (SentFlag)
        //                        {
        //                            resultMsg = " Mail Sent to  MailId";
        //                        }
        //                        //Admin Alerts
        //                        //if (!string.IsNullOrEmpty(strAACEmails))
        //                        //{
        //                        //    foreach (string item in strAACEmails.Split(','))
        //                        //    {
        //                        //        SendEmail(item, body, Subject, "", "");
        //                        //    }

        //                        //}
        //                    }
        //                    //string sms = "'Redemption Transaction has been approved for your investment in  " + dr["dd_schemedesc"].ToString() + ", " + dr["dd_plandesc"].ToString() + ", " + dr["dd_optiondesc"].ToString() + " of Amt/Units: " + dr["dd_amt"].ToString() + "  date " + dr["dd_trdate"].ToString() + " in Folio No.  " + dr["dd_acno"].ToString() + ".Korp Connect'";
        //                    //Redemption Approved
        //                    string sms = "'Dear Investor, " + dr["dd_trtype"].ToString() + " Transaction has been approved for your investment under " + dr["dd_schemedesc"].ToString() + " " + dr["dd_plandesc"].ToString() + " " + dr["dd_optiondesc"].ToString() + " of Rs." + dr["dd_amt"].ToString() + "  dated " + dr["dd_trdate"].ToString() + " in Folio-" + dr["dd_acno"].ToString() + ". Regards, Bajaj Finserv Mutual Fund.'";

        //                    if (dr["AllowSMS"].ToString() == "1")
        //                    {
        //                        insertSmS(sms, dr["MobileNumber"].ToString());
        //                        //Admin Alerts
        //                        //if (!string.IsNullOrEmpty(strAACMobileNo))
        //                        //{
        //                        //    foreach (string aCCMobileNo in strAACMobileNo.Split(','))
        //                        //    {
        //                        //        if (!string.IsNullOrEmpty(aCCMobileNo))
        //                        //            insertSmS(sms, aCCMobileNo);
        //                        //    }
        //                        //}
        //                    }

        //                }
        //                if (dr["Status"].ToString() == "Approved")
        //                {
        //                    //string relativePath = ConfigurationManager.AppSettings["RedemptionapprovalbyFinalApproval"];
        //                    //StreamReader reader = new StreamReader(relativePath);
        //                    //string body = RedemptionapprovalbyFinalApproval(reader, dr);
        //                    if (dr["Allowmail"].ToString() == "1")
        //                    {

        //                        string relativePath = ConfigurationManager.AppSettings["RedemptionTransactionApproved"];
        //                        StreamReader reader = new StreamReader(relativePath);
        //                        string body = BajajTransactions(reader, dr);
        //                        string Subject = "Redemption Transaction Approved";
        //                        bool SentFlag = SendEmail(CorporateEmail, body, Subject, "", "", XHeader);
        //                        if (SentFlag)
        //                        {
        //                            resultMsg = " Mail Sent to  MailId";
        //                        }
        //                        //Admin Alerts
        //                        //if (!string.IsNullOrEmpty(strAACEmails))
        //                        //{
        //                        //    foreach (string item in strAACEmails.Split(','))
        //                        //    {
        //                        //        SendEmail(item, body, Subject, "", "");
        //                        //    }
        //                        //}
        //                    }
        //                    //string sms = "'Redemption Transaction has been approved for your investment in  " + dr["dd_schemedesc"].ToString() + ", " + dr["dd_plandesc"].ToString() + ", " + dr["dd_optiondesc"].ToString() + " of Amt/Units: " + dr["dd_amt"].ToString() + "  date " + dr["dd_trdate"].ToString() + " in Folio No.  " + dr["dd_acno"].ToString() + ".Korp Connect'";

        //                    string sms = "'Dear Investor, " + dr["dd_trtype"].ToString() + " Transaction has been approved for your investment under " + dr["dd_schemedesc"].ToString() + " " + dr["dd_plandesc"].ToString() + " " + dr["dd_optiondesc"].ToString() + " of Rs." + dr["dd_amt"].ToString() + "  dated " + dr["dd_trdate"].ToString() + " in Folio-" + dr["dd_acno"].ToString() + ". Regards, Bajaj Finserv Mutual Fund.'";
        //                    if (dr["AllowSMS"].ToString() == "1")
        //                    {
        //                        insertSmS(sms, dr["MobileNumber"].ToString());
        //                        //Admin Alerts
        //                        //if (!string.IsNullOrEmpty(strAACMobileNo))
        //                        //{
        //                        //    foreach (string aCCMobileNo in strAACMobileNo.Split(','))
        //                        //    {
        //                        //        if (!string.IsNullOrEmpty(aCCMobileNo))
        //                        //            insertSmS(sms, aCCMobileNo);
        //                        //    }
        //                        //}
        //                    }

        //                }

        //            }
        //            if (dr["Action"].ToString() == "Approved" && dr["dd_trtype"].ToString() == "SWP")
        //            {
        //                if (dr["Allowmail"].ToString() == "1")
        //                {
        //                    if (dr["Status"].ToString() == "Pending")
        //                    {
        //                        string relativePath = ConfigurationManager.AppSettings["SWPapprovalbyapprovers"];
        //                        StreamReader reader = new StreamReader(relativePath);
        //                        string body = SWPapprovalbyapprovers(reader, dr);
        //                        string Subject = dr["Subject"].ToString();
        //                        bool SentFlag = SendEmail(CorporateEmail, body, Subject, "", "", XHeader);
        //                        if (SentFlag)
        //                        {
        //                            resultMsg = " Mail Sent to  MailId";
        //                        }
        //                    }
        //                }
        //                if (dr["Status"].ToString() == "Approved")
        //                {
        //                    if (dr["Allowmail"].ToString() == "1")
        //                    {
        //                        string relativePath = ConfigurationManager.AppSettings["SWPapprovalbyFinalApproval"];
        //                        StreamReader reader = new StreamReader(relativePath);
        //                        string body = SWPapprovalbyFinalApproval(reader, dr);
        //                        string Subject = dr["Subject"].ToString();
        //                        bool SentFlag = SendEmail(CorporateEmail, body, Subject, "", "", XHeader);
        //                        if (SentFlag)
        //                        {
        //                            resultMsg = " Mail Sent to  MailId";
        //                        }
        //                    }
        //                }

        //            }
        //            if (dr["Action"].ToString() == "Approved" && dr["dd_trtype"].ToString() == "STP")
        //            {
        //                if (dr["Status"].ToString() == "Pending")
        //                {
        //                    if (dr["Allowmail"].ToString() == "1")
        //                    {
        //                        string relativePath = ConfigurationManager.AppSettings["STPapprovalbyapprovers"];
        //                        StreamReader reader = new StreamReader(relativePath);
        //                        string body = STPapprovalbyapprovers(reader, dr);
        //                        string Subject = dr["Subject"].ToString();
        //                        bool SentFlag = SendEmail(CorporateEmail, body, Subject, "", "", XHeader);
        //                        if (SentFlag)
        //                        {
        //                            resultMsg = " Mail Sent to  MailId";
        //                        }
        //                    }
        //                }
        //                if (dr["Status"].ToString() == "Approved")
        //                {
        //                    if (dr["Allowmail"].ToString() == "1")
        //                    {
        //                        string relativePath = ConfigurationManager.AppSettings["STPapprovalbyFinalApproval"];
        //                        StreamReader reader = new StreamReader(relativePath);
        //                        string body = STPapprovalbyFinalApproval(reader, dr);
        //                        string Subject = dr["Subject"].ToString();
        //                        bool SentFlag = SendEmail(CorporateEmail, body, Subject, "", "", XHeader);
        //                        if (SentFlag)
        //                        {
        //                            resultMsg = " Mail Sent to  MailId";
        //                        }
        //                    }
        //                }

        //            }
        //            if (dr["Action"].ToString() == "Approved" && dr["dd_trtype"].ToString() == "NEW")
        //            {
        //                if (dr["Status"].ToString() == "Pending")
        //                {
        //                    if (dr["Allowmail"].ToString() == "1")
        //                    {

        //                        string relativePath = ConfigurationManager.AppSettings["Purchaseapprovalbyapprovers"];
        //                        StreamReader reader = new StreamReader(relativePath);
        //                        string body = Purchaseapprovalbyapprovers(reader, dr);
        //                        string Subject = dr["Subject"].ToString();
        //                        bool SentFlag = SendEmail(CorporateEmail, body, Subject, "", "", XHeader);
        //                        if (SentFlag)
        //                        {
        //                            resultMsg = " Mail Sent to  MailId";
        //                        }
        //                    }
        //                    //string sms = "'Purchase Transaction has been approved for your investment in  " + dr["dd_schemedesc"].ToString() + ", " + dr["dd_plandesc"].ToString() + ", " + dr["dd_optiondesc"].ToString() + " of Amt/Units: " + dr["dd_amt"].ToString() + "  date " + dr["dd_trdate"].ToString() + " in Folio No.  " + dr["dd_acno"].ToString() + ".Korp Connect'";
        //                    string sms = "'Dear Investor, " + dr["dd_trtype"].ToString() + " Transaction has been approved for your investment under " + dr["dd_schemedesc"].ToString() + " " + dr["dd_plandesc"].ToString() + " " + dr["dd_optiondesc"].ToString() + " of Rs." + dr["dd_amt"].ToString() + "  dated " + dr["dd_trdate"].ToString() + " in Folio-" + dr["dd_acno"].ToString() + ". Regards, Bajaj Finserv Mutual Fund.'";

        //                    if (dr["AllowSMS"].ToString() == "1")
        //                    {
        //                        insertSmS(sms, dr["MobileNumber"].ToString());
        //                    }
        //                }
        //                if (dr["Status"].ToString() == "Approved")
        //                {
        //                    if (dr["Allowmail"].ToString() == "1")
        //                    {

        //                        string relativePath = ConfigurationManager.AppSettings["NewPurchaseapprovedbyFinalApprover"];
        //                        StreamReader reader = new StreamReader(relativePath);
        //                        string body = NewPurchaseapprovedbyFinalApprover(reader, dr);
        //                        string Subject = dr["Subject"].ToString();
        //                        bool SentFlag = SendEmail(CorporateEmail, body, Subject, "", "", XHeader);
        //                        if (SentFlag)
        //                        {
        //                            resultMsg = " Mail Sent to  MailId";
        //                        }
        //                    }
        //                    //string sms = "'Purchase Transaction has been approved for your investment in  " + dr["dd_schemedesc"].ToString() + ", " + dr["dd_plandesc"].ToString() + ", " + dr["dd_optiondesc"].ToString() + " of Amt/Units: " + dr["dd_amt"].ToString() + "  date " + dr["dd_trdate"].ToString() + " in Folio No.  " + dr["dd_acno"].ToString() + ".Korp Connect'";
        //                    string sms = "'Dear Investor, " + dr["dd_trtype"].ToString() + " Transaction has been approved for your investment under " + dr["dd_schemedesc"].ToString() + " " + dr["dd_plandesc"].ToString() + " " + dr["dd_optiondesc"].ToString() + " of Rs." + dr["dd_amt"].ToString() + "  dated " + dr["dd_trdate"].ToString() + " in Folio-" + dr["dd_acno"].ToString() + ". Regards, Bajaj Finserv Mutual Fund.'";
        //                    if (dr["AllowSMS"].ToString() == "1")
        //                    {
        //                        insertSmS(sms, dr["MobileNumber"].ToString());
        //                    }
        //                }

        //            }

        //            //Purchase Rejected
        //            if (dr["Action"].ToString() == "Rejected" && dr["dd_trtype"].ToString() == "Purchase")
        //            {
        //                if (dr["Allowmail"].ToString() == "1")
        //                {

        //                    string relativePath = Convert.ToString(dr["ISAdminalert"]) == "1" ? ConfigurationManager.AppSettings["PurchaseRejectedbyApprover_AdminAlerts"] : ConfigurationManager.AppSettings["PurchaseTransactionRejected"];
        //                    StreamReader reader = new StreamReader(relativePath);
        //                    string body = BajajTransactions(reader, dr);
        //                    string Subject = "Purchase Transaction Rejected";
        //                    bool SentFlag = SendEmail(CorporateEmail, body, Subject, "", "", XHeader);
        //                    if (SentFlag)
        //                    {
        //                        resultMsg = " Mail Sent to  MailId";
        //                    }
        //                    //Admin Alerts
        //                    //if (!string.IsNullOrEmpty(strAACEmails))
        //                    //{
        //                    //    relativePath = ConfigurationManager.AppSettings["PurchaseRejectedbyApprover_AdminAlerts"];
        //                    //    reader = new StreamReader(relativePath);
        //                    //    body = PurchaseRejectedbyApprover(reader, dr);
        //                    //    foreach (string item in strAACEmails.Split(','))
        //                    //    {
        //                    //        SendEmail(item, body, Subject, "", "");
        //                    //    }
        //                    //}
        //                }
        //                //Purchase Rejected
        //                string sms = "'Dear Investor, We regret to inform that your " + dr["dd_trtype"].ToString() + " investment request ref.no-" + dr["refno"].ToString() + " dated" + dr["dd_trdate"].ToString() + " under " + dr["dd_schemedesc"].ToString() + " " + dr["dd_plandesc"].ToString() + " " + dr["dd_optiondesc"].ToString() + " has been rejected by " + dr["Apprname"].ToString() + ". Regards, Bajaj Finserv Mutual Fund.'";
        //                if (dr["AllowSMS"].ToString() == "1")
        //                {
        //                    insertSmS(sms, dr["MobileNumber"].ToString());
        //                    //Admin Alerts
        //                    //if (!string.IsNullOrEmpty(strAACMobileNo))
        //                    //{
        //                    //    foreach (string aCCMobileNo in strAACMobileNo.Split(','))
        //                    //    {
        //                    //        if (!string.IsNullOrEmpty(aCCMobileNo))
        //                    //            insertSmS(sms, aCCMobileNo);
        //                    //    }
        //                    //}
        //                }

        //            }
        //            if (dr["Action"].ToString() == "Rejected" && dr["dd_trtype"].ToString() == "Switch")
        //            {
        //                if (dr["Allowmail"].ToString() == "1")
        //                {

        //                    string relativePath = ConfigurationManager.AppSettings["BajajTemplate"];
        //                    StreamReader reader = new StreamReader(relativePath);
        //                    string body = BajajTransactions(reader, dr);
        //                    string Subject = dr["Subject"].ToString();
        //                    bool SentFlag = SendEmail(CorporateEmail, body, Subject, "", "", XHeader);
        //                    if (SentFlag)
        //                    {
        //                        resultMsg = " Mail Sent to  MailId";
        //                    }
        //                    //Admin Alerts
        //                    //if (!string.IsNullOrEmpty(strAACEmails))
        //                    //{
        //                    //    foreach (string item in strAACEmails.Split(','))
        //                    //    {
        //                    //        SendEmail(item, body, Subject, "", "");
        //                    //    }
        //                    //}
        //                }
        //                // Switch Rejected
        //                string sms = "'Dear Investor, We regret to inform that your " + dr["dd_trtype"].ToString() + " investment request ref.no-" + dr["refno"].ToString() + " dated" + dr["dd_trdate"].ToString() + " under " + dr["dd_schemedesc"].ToString() + " " + dr["dd_plandesc"].ToString() + " " + dr["dd_optiondesc"].ToString() + " has been rejected by " + dr["Apprname"].ToString() + ". Regards, Bajaj Finserv Mutual Fund.'";
        //                if (dr["AllowSMS"].ToString() == "1")
        //                {
        //                    insertSmS(sms, dr["MobileNumber"].ToString());
        //                    //Admin Alerts
        //                    //if (!string.IsNullOrEmpty(strAACMobileNo))
        //                    //{
        //                    //    foreach (string aCCMobileNo in strAACMobileNo.Split(','))
        //                    //    {
        //                    //        if (!string.IsNullOrEmpty(aCCMobileNo))
        //                    //            insertSmS(sms, aCCMobileNo);
        //                    //    }
        //                    //}
        //                }

        //            }
        //            //Redemption Rejected
        //            if (dr["Action"].ToString() == "Rejected" && dr["dd_trtype"].ToString() == "Redemption")
        //            {
        //                if (dr["Allowmail"].ToString() == "1")
        //                {

        //                    string relativePath = Convert.ToString(dr["ISAdminalert"]) == "1" ? ConfigurationManager.AppSettings["RedemptionRejectedbyApprover_AdminAlerts"] : ConfigurationManager.AppSettings["RedemptionTransactionRejected"];
        //                    StreamReader reader = new StreamReader(relativePath);
        //                    string body = BajajTransactions(reader, dr);
        //                    string Subject = "Redemption Transaction Rejected";
        //                    bool SentFlag = SendEmail(CorporateEmail, body, Subject, "", "", XHeader);
        //                    if (SentFlag)
        //                    {
        //                        resultMsg = " Mail Sent to  MailId";
        //                    }
        //                    //Admin Alerts
        //                    //if (!string.IsNullOrEmpty(strAACEmails))
        //                    //{
        //                    //    relativePath = ConfigurationManager.AppSettings["RedemptionRejectedbyApprover_AdminAlerts"];
        //                    //    reader = new StreamReader(relativePath);
        //                    //    body = RedemptionRejectedbyApprover(reader, dr);
        //                    //    foreach (string item in strAACEmails.Split(','))
        //                    //    {
        //                    //        SendEmail(item, body, Subject, "", "");
        //                    //    }
        //                    //}
        //                }
        //                //Redemption Rejected
        //                string sms = "'Dear Investor, We regret to inform that your " + dr["dd_trtype"].ToString() + " investment request ref.no-" + dr["refno"].ToString() + " dated" + dr["dd_trdate"].ToString() + " under " + dr["dd_schemedesc"].ToString() + " " + dr["dd_plandesc"].ToString() + " " + dr["dd_optiondesc"].ToString() + " has been rejected by " + dr["Apprname"].ToString() + ". Regards, Bajaj Finserv Mutual Fund.'";


        //                if (dr["AllowSMS"].ToString() == "1")
        //                {
        //                    insertSmS(sms, dr["MobileNumber"].ToString());
        //                    //Admin Alerts
        //                    //if (!string.IsNullOrEmpty(strAACMobileNo))
        //                    //{
        //                    //    foreach (string aCCMobileNo in strAACMobileNo.Split(','))
        //                    //    {
        //                    //        if (!string.IsNullOrEmpty(aCCMobileNo))
        //                    //            insertSmS(sms, aCCMobileNo);
        //                    //    }
        //                    //}
        //                }

        //            }
        //            if (dr["Action"].ToString() == "Rejected" && dr["dd_trtype"].ToString() == "SWP")
        //            {
        //                if (dr["Allowmail"].ToString() == "1")
        //                {
        //                    string relativePath = ConfigurationManager.AppSettings["SWPRejectedbyApprover"];
        //                    StreamReader reader = new StreamReader(relativePath);
        //                    string body = SWPRejectedbyApprover(reader, dr);
        //                    string Subject = dr["Subject"].ToString();
        //                    bool SentFlag = SendEmail(CorporateEmail, body, Subject, "", "", XHeader);
        //                    if (SentFlag)
        //                    {
        //                        resultMsg = " Mail Sent to  MailId";
        //                    }
        //                }
        //            }
        //            if (dr["Action"].ToString() == "Rejected" && dr["dd_trtype"].ToString() == "STP")
        //            {
        //                if (dr["Allowmail"].ToString() == "1")
        //                {
        //                    string relativePath = ConfigurationManager.AppSettings["STPRejectedbyApprover"];
        //                    StreamReader reader = new StreamReader(relativePath);
        //                    string body = STPRejectedbyApprover(reader, dr);
        //                    string Subject = dr["Subject"].ToString();
        //                    bool SentFlag = SendEmail(CorporateEmail, body, Subject, "", "", XHeader);
        //                    if (SentFlag)
        //                    {
        //                        resultMsg = " Mail Sent to  MailId";
        //                    }
        //                }
        //            }
        //            if (dr["Action"].ToString() == "Rejected" && dr["dd_trtype"].ToString() == "NEW")
        //            {
        //                if (dr["Allowmail"].ToString() == "1")
        //                {

        //                    string relativePath = ConfigurationManager.AppSettings["PurchaseRejectedbyApprover"];
        //                    StreamReader reader = new StreamReader(relativePath);
        //                    string body = PurchaseRejectedbyApprover(reader, dr);
        //                    string Subject = dr["Subject"].ToString();
        //                    bool SentFlag = SendEmail(CorporateEmail, body, Subject, "", "", XHeader);
        //                    if (SentFlag)
        //                    {
        //                        resultMsg = " Mail Sent to  MailId";
        //                    }
        //                }
        //                string sms = "'Dear Investor, We regret to inform that Purchase ref.no " + dr["refno"].ToString() + " dated " + dr["dd_trdate"].ToString() + "  for " + dr["dd_trdate"].ToString() + " has been rejected by " + dr["Apprname"].ToString() + ". Regards, Bajaj Finserv Mutual Fund.' ";

        //                if (dr["AllowSMS"].ToString() == "1")
        //                {
        //                    insertSmS(sms, dr["MobileNumber"].ToString());
        //                }
        //            }
        //            #endregion
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        WriteLog(ex.Message + " - Inner Exception : " + ex.InnerException, ex.StackTrace, "RegistrationConfirmationEmail", CorporateEmail + " XHeader : " + XHeader);
        //        resultMsg = "problem occured while sending confirmation email";
        //    }
        //}

        public static DataRow drExceptionRow;

        //public static void GetTransactionData()
        //{
        //    //saloni
        //    while (true)
        //    {
        //        try
        //        {
        //            DataTable dt = new DataTable();
        //            var connection = ConfigurationManager.ConnectionStrings["KBOLT"].ConnectionString;
        //            using (var connections = new SqlConnection(connection))
        //            {
        //                //var command = new SqlCommand("GetTransactionMailStatusdata", connections);

        //                var command = new SqlCommand("KTrack_sendemail_customfund_oldbridge", connections);
        //                command.CommandType = CommandType.StoredProcedure;
        //                command.CommandTimeout = 90000;
        //                connections.Open();
        //                SqlDataAdapter da = new SqlDataAdapter(command);
        //                da.Fill(dt);

        //                if (dt.Rows.Count > 0 && dt.Rows[0]["Error_number"].ToString() != "100")
        //                {
        //                    foreach (DataRow row in dt.Rows)
        //                    {
        //                        drExceptionRow = row;
        //                        RegistrationConfirmationEmail(row);
        //                        System.Threading.Thread.Sleep(5000);
        //                    }
        //                }
        //                else
        //                {
        //                    //  Console.WriteLine("No data available");
        //                }

        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            try
        //            {
        //                // WriteLog(ex.Message + " - Inner Exception : " + ex.InnerException, ex.StackTrace, "GetTransactionData", "");
        //            }
        //            catch (Exception ex1) { }
        //        }
        //        System.Threading.Thread.Sleep(5000);
        //    }

        //    //EmapanelmentOldBridge();
        //}
        
        
        //added by saloni

        private static string LIC(StreamReader reader, DataRow dr)
        {
            string body = string.Empty;

            try
            {
                using (reader)
                {
                    body = reader.ReadToEnd();
                }
                if (body.Contains("trtype"))
                {
                    if (dr["Trantype"] != null)
                    {
                        body = body.Replace("{trtype}", dr["Trantype"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{trtype}", "");
                    }
                }
                if (body.Contains("Cycledate"))
                {
                    if (dr["Cycledate"] != null)
                    {
                        body = body.Replace("{Cycledate}", dr["Cycledate"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{Cycledate}", "");
                    }
                }
                if (body.Contains("no_of_insts"))
                {
                    if (dr["no_of_insts"] != null)
                    {
                        body = body.Replace("{no_of_insts}", dr["no_of_insts"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{no_of_insts}", "");
                    }
                }
                if (body.Contains("sipamt"))
                {
                    if (dr["sipamt"] != null)
                    {
                        body = body.Replace("{sipamt}", dr["sipamt"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{sipamt}", "");
                    }
                }
                if (body.Contains("sip_frequency"))
                {
                    if (dr["sip_frequency"] != null)
                    {
                        body = body.Replace("{sip_frequency}", dr["sip_frequency"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{sip_frequency}", "");
                    }
                }
                if (body.Contains("investorname"))
                {
                    if (dr["Investor_name"] != null)
                    {
                        body = body.Replace("{investorname}", dr["Investor_name"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{investorname}", "");
                    }
                }
                if (body.Contains("TranDate"))
                {
                    if (dr["TranDate"] != null)
                    {
                        body = body.Replace("{TranDate}", dr["TranDate"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{TranDate}", "");
                    }
                }
                if (body.Contains("Email"))
                {
                    if (dr["Email"] != null)
                    {
                        body = body.Replace("{Email}", dr["Email"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{Email}", "");
                    }
                }
                if (body.Contains("Amount"))
                {
                    if (dr["Amount"] != null)
                    {
                        body = body.Replace("{Amount}", dr["Amount"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{Amount}", "");
                    }
                }
                if (body.Contains("plndesc"))
                {
                    if (dr["plndesc"] != null)
                    {
                        body = body.Replace("{plndesc}", dr["plndesc"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{plndesc}", "");
                    }
                }
                if (body.Contains("optdesc"))
                {
                    if (dr["optdesc"] != null)
                    {
                        body = body.Replace("{optdesc}", dr["optdesc"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{optdesc}", "");
                    }
                }
                if (body.Contains("schdesc"))
                {
                    if (dr["schdesc"] != null)
                    {
                        body = body.Replace("{schdesc}", dr["schdesc"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{schdesc}", "");
                    }
                }
                if (body.Contains("Folio_No"))
                {
                    if (dr["Folio_No"] != null)
                    {
                        body = body.Replace("{Folio_No}", dr["Folio_No"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{Folio_No}", "");
                    }
                }
                if (body.Contains("Investor_name"))
                {
                    if (dr["Investor_name"] != null)
                    {
                        body = body.Replace("{Investor_name}", dr["Investor_name"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{Investor_name}", "");
                    }
                }
                if (body.Contains("IHNO"))
                {
                    if (dr["IHNO"] != null)
                    {
                        body = body.Replace("{IHNO}", dr["IHNO"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{IHNO}", "");
                    }
                }
                if (body.Contains("BankAccno"))
                {
                    if (dr["bank_Acno"] != null)
                    {
                        body = body.Replace("{BankAccno}", MaskString(Convert.ToString(dr["bank_Acno"])));
                    }
                    else
                    {
                        body = body.Replace("{BankAccno}", "");
                    }
                }
                if (body.Contains("BankName"))
                {
                    if (dr["Bank_Name"] != null)
                    {
                        body = body.Replace("{BankName}", dr["Bank_Name"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{BankName}", "");
                    }
                }
                if (body.Contains("Distributor"))
                {
                    if (dr["Distributor"] != null)
                    {
                        body = body.Replace("{Distributor}", dr["Distributor"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{Distributor}", "");
                    }
                }
                if (body.Contains("RedAmtUnit"))
                {
                    if (dr["Amount"] != null)
                    {
                        body = body.Replace("{RedAmtUnit}", dr["Amount"].ToString());
                    }
                    else
                    {
                        if (dr["Units"] != null)
                        {
                            body = body.Replace("{RedAmtUnit}", dr["Units"].ToString());
                        }   
                    }
                }
                if (body.Contains("SIPStartDate"))
                {
                    if (dr["sip_startdate"] != null)
                    {
                        body = body.Replace("{SIPStartDate}", dr["sip_startdate"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{SIPStartDate}", "");
                    }
                }
                if (body.Contains("SIPEndDate"))
                {
                    if (dr["sip_enddate"] != null)
                    {
                        body = body.Replace("{SIPEndDate}", dr["sip_enddate"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{SIPEndDate}", "");
                    }
                }

                if (body.Contains("Scheme_Name"))
                {
                    if (dr["Scheme_Name"]!=null)
                    {
                        body=body.Replace("{Scheme_Name}",dr["Scheme_Name"].ToString());
                    }
                }

                if (body.Contains("To_Scheme_Name"))
                {
                    if (dr["To_Scheme_Name"] != null)
                    {
                        body = body.Replace("{To_Scheme_Name}", dr["To_Scheme_Name"].ToString());
                    }
                }

                if (body.Contains("Units"))
                {
                    if (dr["Units"] != null)
                    {
                        body = body.Replace("{Units}", dr["Units"].ToString());
                    }
                }

                //if (body.Contains("name"))
                //{
                //    if (dr["Name"] != null)
                //    {
                //        body = body.Replace("name", dr["Name"].ToString());
                //    }
                //    else
                //    {
                //        body = body.Replace("name", "");
                //    }
                //}
                //if (body.Contains("tract"))
                //{
                //    if (dr["Action"] != null)
                //    {
                //        body = body.Replace("tract", dr["Action"].ToString());
                //    }
                //    else
                //    {
                //        body = body.Replace("tract", "");
                //    }
                //}
                //if (body.Contains("folionumber"))
                //{
                //    if (dr["dd_acno"] != null)
                //    {
                //        body = body.Replace("folionumber", dr["dd_acno"].ToString());
                //    }
                //    else
                //    {
                //        body = body.Replace("folionumber", "");
                //    }
                //}
                //if (body.Contains("transactiondate"))
                //{
                //    if (dr["dd_trdate"] != null)
                //    {
                //        body = body.Replace("transactiondate", dr["dd_trdate"].ToString());
                //    }
                //    else
                //    {
                //        body = body.Replace("transactiondate", "");
                //    }
                //}

                //if (body.Contains("txnrefno"))
                //{
                //    if (dr["refno"] != null)
                //    {
                //        body = body.Replace("txnrefno", dr["refno"].ToString());
                //    }
                //    else
                //    {
                //        body = body.Replace("txnrefno", "");
                //    }
                //}
                //if (body.Contains("ntor"))
                //{
                //    if (dr["InvestorName"] != null)
                //    {
                //        body = body.Replace("ntor", dr["InvestorName"].ToString());
                //    }
                //    else
                //    {
                //        body = body.Replace("ntor", "");
                //    }
                //}
                //if (body.Contains("schemeplanoption"))
                //{
                //    string Schoption = dr["dd_schemedesc"].ToString() + " " + dr["dd_plandesc"].ToString() + " " + dr["dd_optiondesc"].ToString();

                //    if (Schoption != null)
                //    {
                //        body = body.Replace("schemeplanoption", Schoption);
                //    }
                //    else
                //    {
                //        body = body.Replace("schemeplanoption", "");
                //    }
                //}
                //if (body.Contains("maskedpan"))
                //{
                //    if (dr["pan"] != null)
                //    {
                //        body = body.Replace("maskedpan", dr["pan"].ToString());
                //    }
                //    else
                //    {
                //        body = body.Replace("maskedpan", "");
                //    }
                //}
                //if (body.Contains("txntype"))
                //{
                //    if (dr["dd_trtype"] != null)
                //    {
                //        body = body.Replace("txntype", dr["dd_trtype"].ToString());
                //    }
                //    else
                //    {
                //        body = body.Replace("txntype", "");
                //    }
                //}
                //if (body.Contains("arn"))
                //{
                //    if (dr["ARN/EUIN"] != null)
                //    {
                //        body = body.Replace("arn", dr["ARN/EUIN"].ToString());
                //    }
                //    else
                //    {
                //        body = body.Replace("arn", "");
                //    }
                //}
                //if (body.Contains("euin"))
                //{
                //    if (dr["ARN/EUIN"] != null)
                //    {
                //        body = body.Replace("euin", dr["ARN/EUIN"].ToString());
                //    }
                //    else
                //    {
                //        body = body.Replace("EUIN", "");
                //    }
                //}
                //if (body.Contains("amountunits"))
                //{
                //    if (dr["dd_amt"] != null)
                //    {
                //        body = body.Replace("amountunits", dr["dd_amt"].ToString());
                //    }
                //    else
                //    {
                //        body = body.Replace("amountunits", "");
                //    }
                //}
                //if (body.Contains("bank"))
                //{
                //    if (dr["Bankname"] != null)
                //    {
                //        body = body.Replace("bank", dr["Bankname"].ToString());
                //    }
                //    else
                //    {
                //        body = body.Replace("bank", "");
                //    }
                //}
                //if (body.Contains("initiationdate"))
                //{
                //    if (dr["commencementdate"] != null)
                //    {
                //        body = body.Replace("initiationdate", dr["commencementdate"].ToString());
                //    }
                //    else
                //    {

                //        body = body.Replace("initiationdate", "");
                //    }
                //}
                //if (body.Contains("validity"))
                //{
                //    string vald = "NA";
                //    if (vald != null)
                //    {
                //        body = body.Replace("validity", vald);
                //    }
                //    else
                //    {
                //        body = body.Replace("validity", "NA");
                //    }
                //}
                //if (body.Contains("status"))
                //{
                //    if (dr["Status"] != null)
                //    {
                //        body = body.Replace("status", dr["Status"].ToString());
                //    }
                //    else
                //    {

                //        body = body.Replace("status", "");
                //    }
                //}

            }
            catch (Exception ex)
            {
                WriteLog(ex.Message + " - Inner Exception : " + ex.InnerException, ex.StackTrace, "Purchaseapprovalbyapprovers", dr["refno"].ToString());
                body = string.Empty;
            }

            return body;
        }

        private static string MaskString(string accno)
        {
            try
            {
                if (String.IsNullOrEmpty(accno))
                    return "";

                string maskedstring = string.Empty;
                int len = (accno.Length)-5;
                for (int i = 0; i < accno.Length-1; i++)
                {
                    if (i < len)
                        maskedstring += "X";
                    else
                        maskedstring += accno[i];
                }
                return maskedstring;
            }
            catch (Exception)
            {
                return "";
            }
        }


        public static void emailtemplate()
        {
            DataSet ds = new DataSet();
            try
            {
                string trtype = "", invstr = "", flag = "", sipamt = "", paymode = "", installments = "", chqtype = "", stdt = "", umrno = "", enddt = "", freq = "", regno = "", entdt = "", appno = "", option = "", funddesc = "", fund = "", dcr = "", folio = "", ihno = "", contact_email = "", tollno = "", pendingstatus = "", mobile = "", trandate = "", Old_scheme = "", new_scheme = "", amount = "", units = "", urnno = "", bankname = "";
                string distributor = "", bank_acno = "";
                ds = Getdataforemail();
                foreach (DataRow dr in ds.Tables["Table"].Rows)
                {
                    if (Convert.ToString(dr["Fund"]).Replace(" ", "") == "102")
                    {
                        trtype = Convert.ToString(dr["Trantype"]).ToUpper().Replace(" ", "");
                        invstr = Convert.ToString(dr["Investor_name"]);
                        funddesc = Convert.ToString(dr["Fund_Desc"]);
                        fund = Convert.ToString(dr["Fund"]);
                        dcr = Convert.ToString(dr["dcr"]);
                        paymode = Convert.ToString(dr["paymode"]).ToUpper().Replace(" ", "");
                        folio = Convert.ToString(dr["Folio_No"]);
                        ihno = Convert.ToString(dr["IHNO"]);
                        // string tollno = "1-800-4254-034/035";
                        tollno = Convert.ToString(ConfigurationManager.AppSettings["toolno"]);
                        contact_email = Convert.ToString(ConfigurationManager.AppSettings["contact_email"]);
                        pendingstatus = Convert.ToString(dr["pending"]).Replace(" ", "").ToUpper();
                        mobile = Convert.ToString(dr["Mobile"]);
                        trandate = Convert.ToString(dr["TranDate"]);
                        Old_scheme = Convert.ToString(dr["Scheme_Name"]);
                        new_scheme = Convert.ToString(dr["To_Scheme_Name"]);
                       
                        string schdesc = "";
                        string plndesc = "";
                        string optdesc = "";
                        try
                        {
                            schdesc = Convert.ToString(dr["schdesc"]);
                            plndesc = Convert.ToString(dr["plndesc"]);
                            optdesc = Convert.ToString(dr["optdesc"]);
                            distributor = Convert.ToString(dr["Distributor"]);
                            bank_acno = Convert.ToString(dr["bank_Acno"]);
                        }
                        catch (Exception ex) { }

                        amount = Convert.ToString(dr["Amount"]);
                        var amt = "";
                        var unt = "";
                        if (amount != "" && amount != null && amount != "0.00")
                        {
                            amt = String.Format("{0:#,0.00}", Convert.ToDouble(amount));
                        }
                        else
                        {
                            amt = amount;
                        }
                        units = Convert.ToString(dr["Units"]);

                        if (units != "" && units != null && units != "0.000")
                        {
                            unt = String.Format("{0:#,0.000}", Convert.ToDouble(units));
                        }
                        else
                        {
                            unt = units;
                        }

                        flag = Convert.ToString(dr["partial_full_flag"]);
                        if (Convert.ToString(dr["URN_No"]) != "")
                            urnno = Convert.ToString(dr["URN_No"]);
                        else if (Convert.ToString(dr["UMRN_No"]) != "")
                            urnno = Convert.ToString(dr["UMRN_No"]);
                        else
                            urnno = "";
                        bankname = Convert.ToString(dr["Bank_Name"]);
                        entdt = Convert.ToString(dr["entdt"]);
                        chqtype = Convert.ToString(dr["chqtype"]).ToUpper().Replace(" ", "");
                        stdt = Convert.ToString(dr["sip_startdate"]);
                        enddt = Convert.ToString(dr["sip_enddate"]);
                        freq = Convert.ToString(dr["sip_frequency"]);
                        appno = Convert.ToString(dr["appno"]);
                        regno = Convert.ToString(dr["sip_regno"]);
                        installments = Convert.ToString(dr["no_of_insts"]);
                        sipamt = Convert.ToString(dr["sipamt"]);
                        option = Convert.ToString(dr["stp_option"]);


                        if ((ds.Tables["Table"].Columns.Contains("Email") == true && Convert.ToString(dr["Email"]) != ""))
                        {
                            string tomailid = Convert.ToString(dr["Email"]);
                            StringBuilder sbMailBody = new StringBuilder();
                            StringBuilder sbSMSBody = new StringBuilder();
                            
                            string maindir = AppDomain.CurrentDomain.BaseDirectory.Replace("\\bin\\Debug\\", "");

                            if (trtype == "NEWPURCHASE")
                            {

                                var XHeader = fund + "_" + ihno + "_" + "Newpurchase";
                                string relativePath1 = maindir + ConfigurationManager.AppSettings["NewPurchase"];
                                StreamReader reader1 = new StreamReader(relativePath1);
                                string body1 = LIC(reader1, dr);
                               

                                string Subject1 = "Purchase Acknowledgement from LIC Mutual Fund";
                                bool SentFlag1 = SendEmail(tomailid, body1, Subject1, "", "", XHeader);
                                
                                if (SentFlag1)
                                {
                                    string resultMsg = " Mail Sent to  MailId";
                                    GetdataforSentemail(fund.ToString(), ihno.ToString());
                                }
                                //nikita 30-11-23
                                //string sms = "'We confirm receipt of your request for New Purchase of Rs." + dr["dd_amt"].ToString() + " in Folio-(not yet allocated) on " + dr["TranDate"].ToString() + ". We shall confirm your transaction. Regards, Old Bridge Mutual Fund.'";

                                //string sms = "'Dear Investor, we acknowledge the receipt of Purchase request submitted on " + dr["TranDate"].ToString() + " in Folio not yet allocated for Rs." + dr["sipamt"].ToString() + " under scheme "+dr["schdesc"]+". The request will be processed subject to verification, post which, you will receive a confirmation. Units will be allotted basis credit realisation by Unit. Please call your distributor or 022 6295 5064 for more info. Regards, LIC Mutual Fund.'";   
                                
                                //added by Priyani 140524
                                string sms = "' Dear Investor, we acknowledge the receipt of your New Purchase request for Rs." + dr["Amount"].ToString() + " in " + dr["schdesc"].ToString() + "-" + dr["plndesc"].ToString() + "-" + dr["optdesc"].ToString() + " with Ref.No." + dr["IHNO"].ToString() + " on " + dr["TranDate"].ToString() + " . SOA shall be sent to your e-mail address after transaction is processed. -LICMF'";

                                //Priyani end

                                if (!string.IsNullOrEmpty(mobile))
                                {
                                    insertSmS(sms, mobile, "NEW");

                                   
                                    //string folio_sms = folio;
                                    //string fund_sms = fund;
                                    //string trtype_sms = trtype;
                                    //string ihno_sms = ihno;
                                    //string mobile_sms = mobile;
                                    //string msg_sms = sms;

                                    //SendSms(folio_sms, fund_sms, trtype_sms, ihno_sms, mobile_sms, msg_sms);
                                }
                            }
                            else if (trtype == "SWP")
                            {
                                var XHeader = fund + "_" + ihno + "_" + "SWP";
                                string relativePath1 = maindir + ConfigurationManager.AppSettings["SWP"];
                                StreamReader reader1 = new StreamReader(relativePath1);
                                //string body1 = oldBridge(reader1, dr);
                                string body1 = LIC(reader1, dr);
                                string Subject1 = "SWP Request Acknowledgement from LIC Mutual Fund";
                                bool SentFlag1 = SendEmail(tomailid, body1, Subject1, "", "", XHeader);
                                if (SentFlag1)
                                {
                                    string resultMsg = " Mail Sent to  MailId";
                                    GetdataforSentemail(fund.ToString(), ihno.ToString());
                                }
                                //nikita 30-11-23
                                //string sms = "'We are in receipt of your request for SWP registration in Folio" + dr["Folio_No"].ToString() + " on " + dr["TranDate"].ToString() + ". We shall confirm your transaction status shortly. Regards, LIC Mutual Fund.'";        

                                //added by Priyani 140524
                                string sms = "'Dear Investor, we acknowledge the receipt of your SWP request for Capital Appreciation in " + dr["schdesc"].ToString() + "-" + dr["plndesc"].ToString() + "-" + dr["optdesc"].ToString() + " with Ref.No." + dr["IHNO"].ToString() + "  on " + dr["TranDate"].ToString() + ". SOA shall be sent to your e-mail address after transaction is processed. -LICMF '";

                                //Priyani end
                                 if (!string.IsNullOrEmpty(mobile))
                                {
                                    insertSmS(sms, mobile, "SWP"); //
                                }
                            }
                            else if (trtype == "STP")
                            {
                                var XHeader = fund + "_" + ihno + "_" + "STP";
                                string relativePath1 = maindir + ConfigurationManager.AppSettings["STP"];
                                StreamReader reader1 = new StreamReader(relativePath1);
                                string body1 = LIC(reader1, dr);
                                string Subject1 = "STP Request Acknowledgement from LIC Mutual Fund";
                                bool SentFlag1 = SendEmail(tomailid, body1, Subject1, "", "", XHeader);
                                if (SentFlag1)
                                {
                                    string resultMsg = " Mail Sent to  MailId";
                                    GetdataforSentemail(fund.ToString(), ihno.ToString());
                                }
                                //nikita 30-11-23
                                //string sms = "'We are in receipt of your request for STP registration in Folio" + dr["Folio_No"].ToString() + " on " + dr["TranDate"].ToString() + ". We shall confirm your transaction status shortly. Regards, LIC Mutual Fund.'";     

                                //added by Priyani 140524
                                string sms = "'Dear Investor, we acknowledge the receipt of your STP request for Capital Appreciation in " + dr["schdesc"].ToString() + "-" + dr["plndesc"].ToString() + "-" + dr["optdesc"].ToString() + " with Ref.No. " + dr["IHNO"].ToString() + " on " + dr["TranDate"].ToString() + ". SOA shall be sent to your e-mail address after transaction is processed. -LICMF'";
                                  //Priyani end

                                if (!string.IsNullOrEmpty(mobile))
                                {
                                    insertSmS(sms, mobile, "STP"); //
                                }
                            }
                            else if (trtype == "SIP")
                            {
                                var XHeader = fund + "_" + ihno + "_" + "SIP";
                                string relativePath1 = maindir + ConfigurationManager.AppSettings["SIP"];
                                StreamReader reader1 = new StreamReader(relativePath1);
                                string body1 = LIC(reader1, dr);
                                string Subject1 = "SIP Request Acknowledgement from LIC Mutual Fund";
                                bool SentFlag1 = SendEmail(tomailid, body1, Subject1, "", "", XHeader);
                                if (SentFlag1)
                                {
                                    string resultMsg = " Mail Sent to  MailId";
                                    GetdataforSentemail(fund.ToString(), ihno.ToString());
                                }
                                //Dear Investor, we acknowledge the receipt of your SIP registration submitted on " + dr["TranDate"].ToString() + " under "+dr["schdesc"])+" in Folio"+dr["Folio_No"]+". We will send a confirmation and the statement of account once your transaction is processed. Units will be allotted basis credit realisation. Regards, quant Mutual Fund.

                                //string sms = "'Dear Investor, we acknowledge the receipt of your SIP registration submitted on " + dr["TranDate"].ToString() + " under " + dr["schdesc"].ToString() + " in Folio " + dr["Folio_No"].ToString() + ". We will send a confirmation and the statement of account once your transaction is processed. Units will be allotted basis credit realisation. Regards, LIC Mutual Fund.'";

                                //added by Priyani 150524
                                string sms = "' Dear Investor, we acknowledge the receipt of your SIP request for Rs." + dr["sipamt"].ToString() + " in " + dr["schdesc"].ToString() + "-" + dr["plndesc"].ToString() + "-" + dr["optdesc"].ToString() + " with Ref.No." + dr["IHNO"].ToString() + " on " + dr["TranDate"].ToString() + " . SOA shall be sent to your e-mail address after transaction is processed. -LICMF'";
                              
                                //Priyani end

                                if (String.IsNullOrEmpty(Convert.ToString(dr["Folio_No"])))
                                {
                                    sms = string.Format(sms, "(not yet allocated)");
                                }
                                else
                                {
                                    sms = string.Format(sms, Convert.ToString(dr["Folio_No"]));
                                }

                                if (!string.IsNullOrEmpty(mobile))
                                {
                                    insertSmS(sms, mobile, "SIP"); //
                                }
                            }
                            else if (trtype == "PURCHASE")
                            {
                                var XHeader = fund + "_" + ihno + "_" + "AddnPurchase";
                                string relativePath1 = maindir + ConfigurationManager.AppSettings["AddnPurchase"];
                                StreamReader reader1 = new StreamReader(relativePath1);
                                string body1 = LIC(reader1, dr);
                                string Subject1 = "Purchase Request Acknowledgement from LIC Mutual Fund";
                                bool SentFlag1 = SendEmail(tomailid, body1, Subject1, "", "", XHeader);
                                if (SentFlag1)
                                {
                                    string resultMsg = " Mail Sent to  MailId";
                                    GetdataforSentemail(fund.ToString(), ihno.ToString());
                                }

                                //nikita 30-11-23
                                //string sms = "'Dear Investor, we acknowledge the receipt of Purchase request submitted on " + dr["TranDate"].ToString() + " in Folio not yet allocated for Rs." + dr["sipamt"].ToString() + " under scheme "+dr["schdesc"]+". The request will be processed subject to verification, post which, you will receive a confirmation. Units will be allotted basis credit realisation by Unit. Please call your distributor or 022 6295 5064 for more info. Regards, LIC Mutual Fund.'";

                                //added by Priyani 140524

                                string sms = "' Dear Investor, we acknowledge the receipt of your " + dr["Trantype"].ToString() + " request for Rs." + dr["sipamt"].ToString() + " in " + dr["schdesc"].ToString() + "-" + dr["plndesc"].ToString() + "-" + dr["optdesc"].ToString() + " with Ref.No." + dr["IHNO"].ToString() + "  on " + dr["TranDate"].ToString() + ". SOA shall be sent to your e-mail address after transaction is processed. -LICMF'";

                                //Priyani end
                               
                                if (!string.IsNullOrEmpty(mobile))
                                {
                                    insertSmS(sms, mobile, "PUR"); //
                                }
                            }
                            else if (trtype == "REDEMPTION")
                            {
                                var XHeader = fund + "_" + ihno + "_" + "Redemption";
                                string relativePath1 = maindir + ConfigurationManager.AppSettings["Redemption"];
                                StreamReader reader1 = new StreamReader(relativePath1);
                                string body1 = LIC(reader1, dr);
                                string Subject1 = "Redemption Request Acknowledgement from LIC Mutual Fund";
                                bool SentFlag1 = SendEmail(tomailid, body1, Subject1, "", "", XHeader);
                                if (SentFlag1)
                                {
                                    string resultMsg = " Mail Sent to  MailId";
                                    GetdataforSentemail(fund.ToString(), ihno.ToString());
                                }
                                string sms = string.Empty;
                                int unit = 0;
                                int.TryParse(Convert.ToString(dr["Units"]), out unit);
                                //Dear Investor, we acknowledge the receipt of Redemption request submitted on " + dr["TranDate"].ToString() + " under scheme"+dr["schdesc"]+" in Folio{#var#} for{#var#}. The request will be processed subject to verification, post which, you will receive a confirmation. Please call your distributor or {#var#}{#var#} for more info. Regards, quant Mutual Fund.

                                //if (unit != 0)
                                //    sms = "'We are in receipt of your request for  Redemption for " + Convert.ToString(dr["Units"]) + " Units in Folio" + dr["Folio_No"].ToString() + ". We shall confirm your transaction status shortly. Regards,LIC Mutual Fund.'";
                                   
                                //else
                                //    sms = "'We have received your request for Redemption for Rs." + dr["Amount"].ToString() + " in Folio" + dr["Folio_No"].ToString() + ".The transaction will be processed subject to verification. Regards, LIC Mutual Fund.'";

                                //added by Priyani 140524
                                if (unit != 0)
                                    sms = "'Dear Investor, we acknowledge the receipt of your Redemption request for " + dr["Units"].ToString() + " in " + dr["schdesc"].ToString() + "-" + dr["plndesc"].ToString() + "-" + dr["optdesc"].ToString() + " with Ref.No." + dr["IHNO"].ToString() + "  on " + dr["TranDate"].ToString() + ". SOA shall be sent to your e-mail address after transaction is processed. -LICMF '";
                                else
                                    sms = "'Dear Investor, we acknowledge the receipt of your Redemption request for Rs." + dr["Amount"].ToString() + " in " + dr["schdesc"].ToString() + "-" + dr["plndesc"].ToString() + "-" + dr["optdesc"].ToString() + " with Ref.No." + dr["IHNO"].ToString() + "  on " + dr["TranDate"].ToString() + ". SOA shall be sent to your e-mail address after transaction is processed. -LICMF '";
                                //Priyani end

                                if (!string.IsNullOrEmpty(mobile))
                                {
                                    insertSmS(sms, mobile, "RED"); //
                                }
                            }
                            else if (trtype == "SWITCH")
                            {
                                var XHeader = fund + "_" + ihno + "_" + "Switch";
                                string relativePath1 = maindir + ConfigurationManager.AppSettings["Switch"];
                                StreamReader reader1 = new StreamReader(relativePath1);
                                string body1 = LIC(reader1, dr);
                                string Subject1 = "Switch Request Acknowledgement from LIC Mutual Fund";
                                bool SentFlag1 = SendEmail(tomailid, body1, Subject1, "", "", XHeader);
                                if (SentFlag1)
                                {
                                    string resultMsg = " Mail Sent to  MailId";
                                    GetdataforSentemail(fund.ToString(), ihno.ToString());
                                }
                                string sms = string.Empty;
                                int unit = 0;
                                int.TryParse(Convert.ToString(dr["Units"]), out unit);

                                //if (unit != 0)
                                //    sms = "'We are in receipt of your request for  Switch for " + Convert.ToString(dr["Units"]) + " Units in Folio" + dr["Folio_No"].ToString() + ". We shall confirm your transaction status shortly. Regards, LIC Mutual Fund.'";

                                //else
                                //    sms = "'We have received your request for Switch for Rs." + dr["Amount"].ToString() + " in Folio" + dr["Folio_No"].ToString() + ".The transaction will be processed subject to verification. Regards, LIC Mutual Fund.'";

                                //added by Priyani 140524
                                if(unit!=0)
                                sms = "' Dear Investor, we acknowledge the receipt of your Switch request for " + dr["Units"].ToString() + " in " + dr["To_Scheme_Name"].ToString() + " with Ref.No. " + dr["IHNO"].ToString() + " on " + dr["TranDate"].ToString() + ". SOA shall be sent to your e-mail address after transaction is processed. -LICMF '";

                                else
                                    sms = "' Dear Investor, we acknowledge the receipt of your Switch request for Rs." + dr["Amount"].ToString() + " in " + dr["To_Scheme_Name"].ToString() + " with Ref.No. " + dr["IHNO"].ToString() + " on " + dr["TranDate"].ToString() + ". SOA shall be sent to your e-mail address after transaction is processed. -LICMF '";

                                    //Priyani end 

                                if (!string.IsNullOrEmpty(mobile))
                                {
                                    insertSmS(sms, mobile, "SW"); //
                                }
                            }
                                //added by Priyani 140524
                            else if (trtype == "SIPCANCELLATION" || trtype == "SWPCANCELLATION" || trtype == "STPCANCELLATION")
                            {
                                var XHeader = fund + "_" + ihno + "_" + trtype ;
                                string relativePath1 = maindir + ConfigurationManager.AppSettings["Cancellation"];
                                StreamReader reader1 = new StreamReader(relativePath1);
                                string body1 = LIC(reader1, dr);
                                string Subject1="Cancellation Request Acknowledgement from LIC Mutual Fund";
                                 bool SentFlag1 = SendEmail(tomailid, body1, Subject1, "", "", XHeader);
                                if (SentFlag1)
                                {
                                    string resultMsg = " Mail Sent to  MailId";
                                    GetdataforSentemail(fund.ToString(), ihno.ToString());
                                }

                                string sms="'Dear Investor, we hereby confirm that your " + dr["Trantype"].ToString() + "request by your MFD " + dr["Distributor"].ToString() + " has processed successfully and details have been sent to your registered email address. - LIC MF'";

                                

                                 if (!string.IsNullOrEmpty(mobile))
                                {
                                    insertSmS(sms, mobile, "CANC"); 
                                }
                            }

                            //Priyani end



                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // WriteLog(ds + "\n" + "\n" + Convert.ToString(ex.GetBaseException()), "emailtempalte()");
            }
            finally
            {
                ds.Tables.Clear();
                ds.Dispose();
            }
        }


        #region  "Distributor email log"
        public static DataSet dist_emaillog(string trtype, string appno, string fund, string entrydt, string Arncode)
        {
            SqlConnection con;
            SqlCommand sqlcmd;
            DataSet dsgetdata;
            SqlDataAdapter dagetdata;
            dsgetdata = new DataSet();
            sqlcmd = new SqlCommand();
            try
            {
                string strsql = Convert.ToString(ConfigurationManager.ConnectionStrings["KBOLT"].ConnectionString);
                con = new SqlConnection(strsql);
                dagetdata = new SqlDataAdapter("Ktrack_Distributor_Mails", con);
                dagetdata.SelectCommand.CommandType = CommandType.StoredProcedure;
                dagetdata.SelectCommand.Parameters.Add("@fund", SqlDbType.VarChar).Value = fund;
                dagetdata.SelectCommand.Parameters.Add("@referencenumber", SqlDbType.VarChar).Value = appno;
                dagetdata.SelectCommand.Parameters.Add("@Transactiontype", SqlDbType.VarChar).Value = trtype;
                dagetdata.SelectCommand.Parameters.Add("@initiatedon", SqlDbType.VarChar).Value = entrydt;
                dagetdata.SelectCommand.Parameters.Add("@Arncode", SqlDbType.VarChar).Value = Arncode;
                dagetdata.SelectCommand.CommandTimeout = 1000;
                dagetdata.Fill(dsgetdata);
            }
            catch (Exception ex)
            {
                WriteLog(ex.Message, "dist_emaillog()", ex.StackTrace,"");
            }
            return dsgetdata;
        }
        #endregion

        public static DataSet Getdataforemail()
        {
            SqlConnection con;
            SqlCommand sqlcmd;
            DataSet dsgetdata;
            SqlDataAdapter dagetdata;
            dsgetdata = new DataSet();
            sqlcmd = new SqlCommand();
            try
            {
                string strsql = Convert.ToString(ConfigurationManager.ConnectionStrings["KBOLT"].ConnectionString);
                con = new SqlConnection(strsql);
                //dagetdata = new SqlDataAdapter("KTrack_sendemail_customfund_trust", con);
                dagetdata = new SqlDataAdapter("KTrack_sendemail_customfund_LIC", con);
                // dagetdata = new SqlDataAdapter("KTrack_sendemail_phani", con);
                dagetdata.SelectCommand.CommandType = CommandType.StoredProcedure;
                dagetdata.SelectCommand.CommandTimeout = 1000;
                dagetdata.Fill(dsgetdata);
            }
            catch (Exception ex)
            {
                //WriteLog(dsgetdata + "\n" + "\n" + Convert.ToString(ex.GetBaseException()), "Getdataforemail()");
            }
            // WriteLog(ex.Message & vbNewLine & vbNewLine & vbNewLine & ex.Source, Strings.Format(Now.Date, "ddMMyyyyHHmmss"), "Getdataforemail()")
            finally
            {
            }
            return dsgetdata;
        }

        // ended by saloni
        private static string AdditionalPurchaseapprovedbyFinalApprover(StreamReader reader, DataRow dr)
        {
            string body = string.Empty;

            string Ulink = ConfigurationManager.AppSettings["linkURL"];
            try
            {
                //string Link = Ulink + dr["Emailid"].ToString();
                var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(dr["Emailid"].ToString());
                string en_email = System.Convert.ToBase64String(plainTextBytes);

                string Link = Ulink + en_email;
                using (reader)
                {
                    body = reader.ReadToEnd();
                }
                if (body.Contains("{Link}"))
                {
                    body = body.Replace("{Link}", Link);
                }
                if (body.Contains("{FirstName}"))
                {
                    if (dr["Apprname"] != null)
                    {
                        body = body.Replace("{FirstName}", dr["Apprname"].ToString());
                    }
                    else
                    {

                        body = body.Replace("{FirstName}", "");
                    }
                }
                if (body.Contains("{folio}"))
                {
                    if (dr["dd_acno"] != null)
                    {
                        body = body.Replace("{folio}", dr["dd_acno"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{folio}", dr["dd_acno"].ToString());
                    }
                }
                if (body.Contains("{scheme}"))
                {
                    if (dr["dd_schemedesc"] != null)
                    {
                        body = body.Replace("{scheme}", dr["dd_schemedesc"].ToString());
                    }
                    else
                    {

                        body = body.Replace("{scheme}", "");
                    }
                }
                if (body.Contains("{plan}"))
                {
                    if (dr["dd_plandesc"] != null)
                    {
                        body = body.Replace("{plan}", dr["dd_plandesc"].ToString());
                    }
                    else
                    {

                        body = body.Replace("{plan}", "");
                    }
                }
                if (body.Contains("{option}"))
                {
                    if (dr["dd_optiondesc"] != null)
                    {
                        body = body.Replace("{option}", dr["dd_optiondesc"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{option}", "");
                    }
                }
                if (body.Contains("{amount}"))
                {
                    if (dr["dd_amt"] != null)
                    {
                        body = body.Replace("{amount}", dr["dd_amt"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{amount}", "");
                    }
                }
                if (body.Contains("{tdate}"))
                {
                    if (dr["dd_trdate"] != null)
                    {
                        body = body.Replace("{tdate}", dr["dd_trdate"].ToString());
                    }
                    else
                    {

                        body = body.Replace("{tdate}", "");
                    }
                }
                if (body.Contains("{distributor}"))
                {
                    if (dr["dd_distributor"] != null)
                    {
                        body = body.Replace("{distributor}", dr["dd_distributor"].ToString());
                    }
                    else
                    {

                        body = body.Replace("{distributor}", dr["dd_distributor"].ToString());
                    }

                }
                if (body.Contains("{ihno}"))
                {
                    if (dr["Ihno"] != null)
                    {
                        body = body.Replace("{ihno}", dr["Ihno"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{ihno}", "");
                    }
                }
                if (body.Contains("{Fundname}"))
                {
                    if (dr["fm_fundname"] != null)
                    {
                        body = body.Replace("{Fundname}", dr["fm_fundname"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{Fundname}", "");
                    }
                }
            }
            catch (Exception ex)
            {
                WriteLog(ex.Message + " - Inner Exception : " + ex.InnerException, ex.StackTrace, "AdditionalPurchaseapprovedbyFinalApprover", dr["Emailid"].ToString());
                body = string.Empty;
            }

            return body;
        }
       
        private static string NewPurchaseapprovedbyFinalApprover(StreamReader reader, DataRow dr)
        {
            string body = string.Empty;

            try
            {
                using (reader)
                {
                    body = reader.ReadToEnd();
                }
                if (body.Contains("{FirstName}"))
                {
                    if (dr["Apprname"] != null)
                    {
                        body = body.Replace("{FirstName}", dr["Apprname"].ToString());
                    }
                    else
                    {

                        body = body.Replace("{FirstName}", "");
                    }
                }
                if (body.Contains("{folio}"))
                {
                    if (dr["dd_acno"] != null)
                    {
                        body = body.Replace("{folio}", dr["dd_acno"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{folio}", "");
                    }
                }
                if (body.Contains("{scheme}"))
                {
                    if (dr["dd_schemedesc"] != null)
                    {
                        body = body.Replace("{scheme}", dr["dd_schemedesc"].ToString());
                    }
                    else
                    {

                        body = body.Replace("{scheme}", "");
                    }
                }
                if (body.Contains("{plan}"))
                {
                    if (dr["dd_plandesc"] != null)
                    {
                        body = body.Replace("{plan}", dr["dd_plandesc"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{plan}", "");
                    }
                }
                if (body.Contains("{option}"))
                {
                    if (dr["dd_optiondesc"] != null)
                    {
                        body = body.Replace("{option}", dr["dd_optiondesc"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{option}", "");
                    }
                }
                if (body.Contains("{amount}"))
                {
                    if (dr["dd_amt"] != null)
                    {
                        body = body.Replace("{amount}", dr["dd_amt"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{amount}", "");
                    }
                }
                if (body.Contains("{tdate}"))
                {
                    if (dr["dd_trdate"] != null)
                    {
                        body = body.Replace("{tdate}", dr["dd_trdate"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{tdate}", "");
                    }
                }
                if (body.Contains("{distributor}"))
                {
                    if (dr["dd_distributor"] != null)
                    {
                        body = body.Replace("{distributor}", dr["dd_distributor"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{distributor}", "");
                    }
                }
                if (body.Contains("{ihno}"))
                {
                    if (dr["Ihno"] != null)
                    {
                        body = body.Replace("{ihno}", dr["Ihno"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{ihno}", "");
                    }
                }
                if (body.Contains("{Investor}"))
                {
                    if (dr["InvestorName"] != null)
                    {
                        body = body.Replace("{Investor}", dr["InvestorName"].ToString());
                    }
                    else
                    {

                        body = body.Replace("{Investor}", "");
                    }
                }
                if (body.Contains("{PAN}"))
                {
                    if (dr["pan"] != null)
                    {
                        body = body.Replace("{PAN}", dr["pan"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{PAN}", "");
                    }
                }
            }
            catch (Exception ex)
            {
                WriteLog(ex.Message + " - Inner Exception : " + ex.InnerException, ex.StackTrace, "NewPurchaseapprovedbyFinalApprover", dr["Ihno"].ToString());
                body = string.Empty;
            }

            return body;
        }
        private static string Purchaseapprovalbyapprovers(StreamReader reader, DataRow dr)
        {
            string body = string.Empty;

            try
            {
                using (reader)
                {
                    body = reader.ReadToEnd();
                }
                if (body.Contains("{FirstName}"))
                {
                    if (dr["Apprname"] != null)
                    {
                        body = body.Replace("{FirstName}", dr["Apprname"].ToString());
                    }
                    else
                    {

                        body = body.Replace("{FirstName}", "");
                    }
                }
                if (body.Contains("{folio}"))
                {
                    if (dr["dd_acno"] != null)
                    {
                        body = body.Replace("{folio}", dr["dd_acno"].ToString());
                    }
                    else
                    {

                        body = body.Replace("{folio}", "");
                    }
                }
                if (body.Contains("{scheme}"))
                {
                    if (dr["dd_schemedesc"] != null)
                    {
                        body = body.Replace("{scheme}", dr["dd_schemedesc"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{scheme}", "");
                    }
                }
                if (body.Contains("{plan}"))
                {
                    if (dr["dd_plandesc"] != null)
                    {
                        body = body.Replace("{plan}", dr["dd_plandesc"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{plan}", "");
                    }
                }
                if (body.Contains("{option}"))
                {
                    if (dr["dd_optiondesc"] != null)
                    {
                        body = body.Replace("{option}", dr["dd_optiondesc"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{option}", "");
                    }
                }
                if (body.Contains("{amount}"))
                {
                    if (dr["dd_amt"] != null)
                    {
                        body = body.Replace("{amount}", dr["dd_amt"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{amount}", "");
                    }
                }
                if (body.Contains("{tdate}"))
                {
                    if (dr["dd_trdate"] != null)
                    {
                        body = body.Replace("{tdate}", dr["dd_trdate"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{tdate}", "");
                    }
                }
                if (body.Contains("{distributor}"))
                {
                    if (dr["dd_distributor"] != null)
                    {
                        body = body.Replace("{distributor}", dr["dd_distributor"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{distributor}", "");
                    }
                }
                if (body.Contains("{ARN/EUIN}"))
                {
                    if (dr["ARN/EUIN"] != null)
                    {
                        body = body.Replace("{ARN/EUIN}", dr["ARN/EUIN"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{ARN/EUIN}", "");
                    }
                }
                if (body.Contains("{refno}"))
                {
                    if (dr["refno"] != null)
                    {
                        body = body.Replace("{refno}", dr["refno"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{refno}", "");
                    }
                }
                if (body.Contains("{Apprname}"))
                {
                    if (dr["Apprname"] != null)
                    {
                        body = body.Replace("{Apprname}", dr["Apprname"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{Apprname}", "");
                    }
                }
                if (body.Contains("{Investor}"))
                {
                    if (dr["InvestorName"] != null)
                    {
                        body = body.Replace("{Investor}", dr["InvestorName"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{Investor}", "");
                    }
                }
                if (body.Contains("{PAN}"))
                {
                    if (dr["pan"] != null)
                    {
                        body = body.Replace("{PAN}", dr["pan"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{PAN}", "");
                    }
                }
                if (body.Contains("{paylink}"))
                {
                    if (string.IsNullOrEmpty(dr["PaymentLink"].ToString()))
                    {

                        body = body.Replace("<a href='{paylink}'>Click here to Pay</a>", "");
                    }
                    else
                    {
                        body = body.Replace("{paylink}", dr["PaymentLink"].ToString());
                    }
                }
                if (body.Contains("{HDFCrefno}"))
                {
                    if (dr["Corporate_Acno"] != null)
                    {

                        body = body.Replace("{HDFCrefno}", dr["Corporate_Acno"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{HDFCrefno}", "");
                    }
                }
            }
            catch (Exception ex)
            {
                WriteLog(ex.Message + " - Inner Exception : " + ex.InnerException, ex.StackTrace, "Purchaseapprovalbyapprovers", dr["InvestorName"].ToString());
                body = string.Empty;
            }

            return body;
        }
        private static string PurchasebyInitiator(StreamReader reader, DataRow dr)
        {
            string body = string.Empty;
            string Ulink = ConfigurationManager.AppSettings["linkURL"];
            try
            {
                var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(dr["Emailid"].ToString());
                string en_email = System.Convert.ToBase64String(plainTextBytes);

                string Link = Ulink + en_email;
                using (reader)
                {
                    body = reader.ReadToEnd();
                }
                if (body.Contains("{Link}"))
                {
                    body = body.Replace("{Link}", Link);
                }
                if (body.Contains("{FirstName}"))
                {
                    if (dr["Apprname"] != null)
                    {
                        body = body.Replace("{FirstName}", dr["Apprname"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{FirstName}", "");
                    }
                }
                if (body.Contains("{folio}"))
                {
                    if (dr["dd_acno"] != null)
                    {
                        body = body.Replace("{folio}", dr["dd_acno"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{folio}", "");
                    }
                }
                if (body.Contains("{scheme}"))
                {
                    if (dr["dd_schemedesc"] != null)
                    {
                        body = body.Replace("{scheme}", dr["dd_schemedesc"].ToString());
                    }
                    else
                    {

                        body = body.Replace("{scheme}", "");
                    }
                }
                if (body.Contains("{plan}"))
                {
                    if (dr["dd_plandesc"] != null)
                    {
                        body = body.Replace("{plan}", dr["dd_plandesc"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{plan}", "");
                    }
                }
                if (body.Contains("{option}"))
                {
                    if (dr["dd_optiondesc"] != null)
                    {
                        body = body.Replace("{option}", dr["dd_optiondesc"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{option}", "");
                    }
                }
                if (body.Contains("{amount}"))
                {
                    if (dr["dd_amt"] != null)
                    {
                        body = body.Replace("{amount}", dr["dd_amt"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{amount}", "");
                    }
                }
                if (body.Contains("{tdate}"))
                {
                    if (dr["dd_trdate"] != null)
                    {
                        body = body.Replace("{tdate}", dr["dd_trdate"].ToString());
                    }
                    else
                    {

                        body = body.Replace("{tdate}", "");
                    }
                }
                if (body.Contains("{distributor}"))
                {
                    if (dr["dd_distributor"] != null)
                    {
                        body = body.Replace("{distributor}", dr["dd_distributor"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{distributor}", "");
                    }
                }
                if (body.Contains("{ihno}"))
                {
                    if (dr["Ihno"] != null)
                    {
                        body = body.Replace("{ihno}", dr["Ihno"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{ihno}", "");
                    }
                }
                if (body.Contains("{refno}"))
                {
                    if (dr["refno"] != null)
                    {
                        body = body.Replace("{refno}", dr["refno"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{refno}", "");
                    }
                }
                if (body.Contains("{ARN/EUIN}"))
                {
                    if (dr["ARN/EUIN"] != null)
                    {
                        body = body.Replace("{ARN/EUIN}", dr["ARN/EUIN"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{ARN/EUIN}", "");
                    }
                }
                if (body.Contains("{Investor}"))
                {
                    if (dr["InvestorName"] != null)
                    {
                        body = body.Replace("{Investor}", dr["InvestorName"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{Investor}", "");
                    }
                }
                if (body.Contains("{PAN}"))
                {
                    if (dr["pan"] != null)
                    {
                        body = body.Replace("{PAN}", dr["pan"].ToString());
                    }
                    else
                    {

                        body = body.Replace("{PAN}", "");
                    }
                }
                if (body.Contains("{bank}"))
                {
                    if (dr["BankName"] != null)
                    {
                        body = body.Replace("{bank}", dr["BankName"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{bank}", "");
                    }
                }


            }
            catch (Exception ex)
            {
                WriteLog(ex.Message + " - Inner Exception : " + ex.InnerException, ex.StackTrace, "PurchasebyInitiator", dr["Ihno"].ToString());
                body = string.Empty;
            }

            return body;
        }
        //On 24/09/2019
        private static string PurchasebySingleUser(StreamReader reader, DataRow dr)
        {
            string body = string.Empty;
            string Ulink = ConfigurationManager.AppSettings["linkURL"];
            try
            {
                var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(dr["Emailid"].ToString());
                string en_email = System.Convert.ToBase64String(plainTextBytes);

                string Link = Ulink + en_email;
                using (reader)
                {
                    body = reader.ReadToEnd();
                }
                if (body.Contains("{Link}"))
                {
                    body = body.Replace("{Link}", Link);
                }
                if (body.Contains("{FirstName}"))
                {
                    if (dr["Apprname"] != null)
                    {
                        body = body.Replace("{FirstName}", dr["Apprname"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{FirstName}", "");
                    }
                }
                if (body.Contains("{folio}"))
                {
                    if (dr["dd_acno"] != null)
                    {
                        body = body.Replace("{folio}", dr["dd_acno"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{folio}", "");
                    }
                }
                if (body.Contains("{scheme}"))
                {
                    if (dr["dd_schemedesc"] != null)
                    {
                        body = body.Replace("{scheme}", dr["dd_schemedesc"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{scheme}", "");
                    }
                }
                if (body.Contains("{plan}"))
                {
                    if (dr["dd_plandesc"] != null)
                    {
                        body = body.Replace("{plan}", dr["dd_plandesc"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{plan}", "");
                    }
                }
                if (body.Contains("{option}"))
                {
                    if (dr["dd_optiondesc"] != null)
                    {
                        body = body.Replace("{option}", dr["dd_optiondesc"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{option}", "");
                    }
                }
                if (body.Contains("{amount}"))
                {
                    if (dr["dd_amt"] != null)
                    {
                        body = body.Replace("{amount}", dr["dd_amt"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{amount}", "");
                    }
                }
                if (body.Contains("{tdate}"))
                {
                    if (dr["dd_trdate"] != null)
                    {
                        body = body.Replace("{tdate}", dr["dd_trdate"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{tdate}", "");
                    }
                }
                if (body.Contains("{distributor}"))
                {
                    if (dr["dd_distributor"] != null)
                    {
                        body = body.Replace("{distributor}", dr["dd_distributor"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{distributor}", "");
                    }
                }
                if (body.Contains("{ihno}"))
                {
                    if (dr["Ihno"] != null)
                    {
                        body = body.Replace("{ihno}", dr["Ihno"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{ihno}", "");
                    }
                }
                if (body.Contains("{refno}"))
                {
                    if (dr["refno"] != null)
                    {
                        body = body.Replace("{refno}", dr["refno"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{refno}", "");
                    }
                }
                if (body.Contains("{ARN/EUIN}"))
                {
                    if (dr["ARN/EUIN"] != null)
                    {
                        body = body.Replace("{ARN/EUIN}", dr["ARN/EUIN"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{ARN/EUIN}", "");
                    }
                }
                if (body.Contains("{Investor}"))
                {
                    if (dr["InvestorName"] != null)
                    {
                        body = body.Replace("{Investor}", dr["InvestorName"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{Investor}", "");
                    }
                }
                if (body.Contains("{PAN}"))
                {
                    if (dr["pan"].ToString() != null)
                    {
                        body = body.Replace("{PAN}", dr["pan"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{PAN}", "");
                    }
                }
                if (body.Contains("{bank}"))
                {
                    if (dr["BankName"] != null)
                    {
                        body = body.Replace("{bank}", dr["BankName"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{bank}", "");
                    }
                }


            }
            catch (Exception ex)
            {
                WriteLog(ex.Message, ex.StackTrace, "PurchasebyInitiator", dr["Ihno"].ToString());
                body = string.Empty;
            }

            return body;
        }

        private static string LICTransactions(StreamReader reader, DataRow dr)
        {
            string body = string.Empty;

            try
            {
                using (reader)
                {
                    body = reader.ReadToEnd();
                }
                if (body.Contains("trtype"))
                {
                    if (dr["dd_trtype"] != null)
                    {
                        body = body.Replace("trtype", dr["dd_trtype"].ToString());
                    }
                    else
                    {
                        body = body.Replace("trtype", "");
                    }
                }
                if (body.Contains("name"))
                {
                    if (dr["Name"] != null)
                    {
                        body = body.Replace("name", dr["Name"].ToString());
                    }
                    else
                    {
                        body = body.Replace("name", "");
                    }
                }
                if (body.Contains("tract"))
                {
                    if (dr["Action"] != null)
                    {
                        body = body.Replace("tract", dr["Action"].ToString());
                    }
                    else
                    {
                        body = body.Replace("tract", "");
                    }
                }
                if (body.Contains("folionumber"))
                {
                    if (dr["dd_acno"] != null)
                    {
                        body = body.Replace("folionumber", dr["dd_acno"].ToString());
                    }
                    else
                    {
                        body = body.Replace("folionumber", "");
                    }
                }
                if (body.Contains("transactiondate"))
                {
                    if (dr["dd_trdate"] != null)
                    {
                        body = body.Replace("transactiondate", dr["dd_trdate"].ToString());
                    }
                    else
                    {
                        body = body.Replace("transactiondate", "");
                    }
                }

                if (body.Contains("txnrefno"))
                {
                    if (dr["refno"] != null)
                    {
                        body = body.Replace("txnrefno", dr["refno"].ToString());
                    }
                    else
                    {
                        body = body.Replace("txnrefno", "");
                    }
                }
                if (body.Contains("ntor"))
                {
                    if (dr["InvestorName"] != null)
                    {
                        body = body.Replace("ntor", dr["InvestorName"].ToString());
                    }
                    else
                    {
                        body = body.Replace("ntor", "");
                    }
                }
                if (body.Contains("schemeplanoption"))
                {
                    string Schoption = dr["dd_schemedesc"].ToString() + " " + dr["dd_plandesc"].ToString() + " " + dr["dd_optiondesc"].ToString();

                    if (Schoption != null)
                    {
                        body = body.Replace("schemeplanoption", Schoption);
                    }
                    else
                    {
                        body = body.Replace("schemeplanoption", "");
                    }
                }
                if (body.Contains("maskedpan"))
                {
                    if (dr["pan"] != null)
                    {
                        body = body.Replace("maskedpan", dr["pan"].ToString());
                    }
                    else
                    {
                        body = body.Replace("maskedpan", "");
                    }
                }
                if (body.Contains("txntype"))
                {
                    if (dr["dd_trtype"] != null)
                    {
                        body = body.Replace("txntype", dr["dd_trtype"].ToString());
                    }
                    else
                    {
                        body = body.Replace("txntype", "");
                    }
                }
                if (body.Contains("arn"))
                {
                    if (dr["ARN/EUIN"] != null)
                    {
                        body = body.Replace("arn", dr["ARN/EUIN"].ToString());
                    }
                    else
                    {
                        body = body.Replace("arn", "");
                    }
                }
                if (body.Contains("euin"))
                {
                    if (dr["ARN/EUIN"] != null)
                    {
                        body = body.Replace("euin", dr["ARN/EUIN"].ToString());
                    }
                    else
                    {
                        body = body.Replace("EUIN", "");
                    }
                }
                if (body.Contains("amountunits"))
                {
                    if (dr["dd_amt"] != null)
                    {
                        body = body.Replace("amountunits", dr["dd_amt"].ToString());
                    }
                    else
                    {
                        body = body.Replace("amountunits", "");
                    }
                }
                if (body.Contains("bank"))
                {
                    if (dr["Bankname"] != null)
                    {
                        body = body.Replace("bank", dr["Bankname"].ToString());
                    }
                    else
                    {
                        body = body.Replace("bank", "");
                    }
                }
                if (body.Contains("initiationdate"))
                {
                    if (dr["commencementdate"] != null)
                    {
                        body = body.Replace("initiationdate", dr["commencementdate"].ToString());
                    }
                    else
                    {

                        body = body.Replace("initiationdate", "");
                    }
                }
                if (body.Contains("validity"))
                {
                    string vald = "NA";
                    if (vald != null)
                    {
                        body = body.Replace("validity", vald);
                    }
                    else
                    {
                        body = body.Replace("validity", "NA");
                    }
                }
                if (body.Contains("status"))
                {
                    if (dr["Status"] != null)
                    {
                        body = body.Replace("status", dr["Status"].ToString());
                    }
                    else
                    {

                        body = body.Replace("status", "");
                    }
                }
                
            }
            catch (Exception ex)
            {
                WriteLog(ex.Message + " - Inner Exception : " + ex.InnerException, ex.StackTrace, "Purchaseapprovalbyapprovers", dr["refno"].ToString());
                body = string.Empty;
            }

            return body;
        }
 
        //On 24/09/2019 END

        private static string PurchasebyKartha(StreamReader reader, DataRow dr)
        {
            string body = string.Empty;

            try
            {
                using (reader)
                {
                    body = reader.ReadToEnd();
                }
                if (body.Contains("{Firstname}"))
                {
                    if (dr["Name"] != null)
                    {
                        body = body.Replace("{Firstname}", dr["Name"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{Firstname}", "");
                    }
                }
                if (body.Contains("{folio}"))
                {
                    if (dr["dd_acno"] != null)
                    {
                        body = body.Replace("{folio}", dr["dd_acno"].ToString());
                    }
                    else
                    {

                        body = body.Replace("{folio}", "");
                    }
                }
                if (body.Contains("{scheme}"))
                {
                    if (dr["dd_schemedesc"] != null)
                    {
                        body = body.Replace("{scheme}", dr["dd_schemedesc"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{scheme}", "");
                    }
                }
                if (body.Contains("{plan}"))
                {
                    if (dr["dd_plandesc"] != null)
                    {
                        body = body.Replace("{plan}", dr["dd_plandesc"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{plan}", "");
                    }
                }
                if (body.Contains("{option}"))
                {
                    if (dr["dd_optiondesc"] != null)
                    {
                        body = body.Replace("{option}", dr["dd_optiondesc"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{option}", "");
                    }
                }
                if (body.Contains("{amount}"))
                {
                    if (dr["dd_amt"] != null)
                    {
                        body = body.Replace("{amount}", dr["dd_amt"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{amount}", "");
                    }
                }
                if (body.Contains("{tdate}"))
                {
                    if (dr["dd_trdate"] != null)
                    {
                        body = body.Replace("{tdate}", dr["dd_trdate"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{tdate}", "");
                    }
                }
                if (body.Contains("{distributor}"))
                {
                    if (dr["dd_distributor"] != null)
                    {
                        body = body.Replace("{distributor}", dr["dd_distributor"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{distributor}", "");
                    }
                }
                if (body.Contains("{ihno}"))
                {
                    if (dr["Ihno"] != null)
                    {
                        body = body.Replace("{ihno}", dr["Ihno"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{ihno}", "");
                    }
                }
                if (body.Contains("{refno}"))
                {
                    if (dr["refno"] != null)
                    {
                        body = body.Replace("{refno}", dr["refno"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{refno}", "");
                    }
                }
                if (body.Contains("{ARN/EUIN}"))
                {
                    if (dr["ARN/EUIN"] != null)
                    {
                        body = body.Replace("{ARN/EUIN}", dr["ARN/EUIN"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{ARN/EUIN}", "");
                    }
                }
                if (body.Contains("{Investor}"))
                {
                    if (dr["InvestorName"] != null)
                    {
                        body = body.Replace("{Investor}", dr["InvestorName"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{Investor}", "");
                    }
                }
                if (body.Contains("{PAN}"))
                {
                    if (dr["pan"] != null)
                    {
                        body = body.Replace("{PAN}", dr["pan"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{PAN}", "");
                    }
                }
                if (body.Contains("{bank}"))
                {
                    if (dr["BankName"] != null)
                    {
                        body = body.Replace("{bank}", dr["BankName"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{bank}", "");
                    }
                }
            }
            catch (Exception ex)
            {
                WriteLog(ex.Message + " - Inner Exception : " + ex.InnerException, ex.StackTrace, "PurchasebyKartha", dr["Ihno"].ToString());
                body = string.Empty;
            }

            return body;
        }
        private static string PurchaseRejectedbyApprover(StreamReader reader, DataRow dr)
        {
            string body = string.Empty;

            try
            {
                using (reader)
                {
                    body = reader.ReadToEnd();
                }
                if (body.Contains("{FirstName}"))
                {
                    if (dr["Name"] != null)
                    {
                        body = body.Replace("{FirstName}", dr["Name"].ToString());
                    }
                    else
                    {

                        body = body.Replace("{FirstName}", "");
                    }
                }
                if (body.Contains("{folio}"))
                {
                    if (dr["dd_acno"] != null)
                    {
                        body = body.Replace("{folio}", dr["dd_acno"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{folio}", "");
                    }
                }
                if (body.Contains("{scheme}"))
                {
                    if (dr["dd_schemedesc"] != null)
                    {
                        body = body.Replace("{scheme}", dr["dd_schemedesc"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{scheme}", "");
                    }
                }
                if (body.Contains("{plan}"))
                {
                    if (dr["dd_plandesc"] != null)
                    {
                        body = body.Replace("{plan}", dr["dd_plandesc"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{plan}", "");
                    }
                }
                if (body.Contains("{option}"))
                {
                    if (dr["dd_optiondesc"] != null)
                    {
                        body = body.Replace("{option}", dr["dd_optiondesc"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{option}", "");
                    }
                }

                if (body.Contains("{amount}"))
                {
                    if (dr["dd_amt"] != null)
                    {
                        body = body.Replace("{amount}", dr["dd_amt"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{amount}", "");
                    }
                }
                if (body.Contains("{tdate}"))
                {
                    if (dr["dd_trdate"] != null)
                    {
                        body = body.Replace("{tdate}", dr["dd_trdate"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{tdate}", "");
                    }
                }

                if (body.Contains("{distributor}"))
                {
                    if (dr["dd_distributor"] != null)
                    {
                        body = body.Replace("{distributor}", dr["dd_distributor"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{distributor}", "");
                    }
                }
                if (body.Contains("{ARN/EUIN}"))
                {
                    if (dr["ARN/EUIN"] != null)
                    {
                        body = body.Replace("{ARN/EUIN}", dr["ARN/EUIN"].ToString());
                    }
                    else
                    {

                        body = body.Replace("{ARN/EUIN}", "");
                    }
                }
                if (body.Contains("{refno}"))
                {
                    if (dr["refno"] != null)
                    {
                        body = body.Replace("{refno}", dr["refno"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{refno}", "");
                    }
                }
                if (body.Contains("{Apprname}"))
                {
                    if (dr["Apprname"] != null)
                    {
                        body = body.Replace("{Apprname}", dr["Apprname"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{Apprname}", dr["Apprname"].ToString());
                    }
                }
                if (body.Contains("{Investor}"))
                {
                    if (dr["InvestorName"] != null)
                    {
                        body = body.Replace("{Investor}", dr["InvestorName"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{Investor}", "");
                    }
                }
                if (body.Contains("{PAN}"))
                {
                    if (dr["pan"] != null)
                    {
                        body = body.Replace("{PAN}", dr["pan"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{PAN}", "");
                    }
                }

            }
            catch (Exception ex)
            {
                WriteLog(ex.Message + " - Inner Exception : " + ex.InnerException, ex.StackTrace, "PurchaseRejectedbyApprover", dr["refno"].ToString());
                body = string.Empty;
            }

            return body;
        }
        private static string RedemptionapprovalbyFinalApproval(StreamReader reader, DataRow dr)
        {
            string body = string.Empty;

            try
            {
                using (reader)
                {
                    body = reader.ReadToEnd();
                }
                if (body.Contains("{FirstName}"))
                {
                    if (dr["Apprname"] != null)
                    {
                        body = body.Replace("{FirstName}", dr["Apprname"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{FirstName}", "");
                    }
                }
                if (body.Contains("{folio}"))
                {
                    if (dr["dd_acno"] != null)
                    {
                        body = body.Replace("{folio}", dr["dd_acno"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{folio}", "");
                    }
                }
                if (body.Contains("{scheme}"))
                {
                    if (dr["dd_schemedesc"] != null)
                    {
                        body = body.Replace("{scheme}", dr["dd_schemedesc"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{scheme}", "");
                    }
                }
                if (body.Contains("{plan}"))
                {
                    if (dr["dd_plandesc"] != null)
                    {
                        body = body.Replace("{plan}", dr["dd_plandesc"].ToString());
                    }
                    else
                    {

                        body = body.Replace("{plan}", "");
                    }
                }
                if (body.Contains("{option}"))
                {
                    if (dr["dd_optiondesc"] != null)
                    {
                        body = body.Replace("{option}", dr["dd_optiondesc"].ToString());
                    }
                    else
                    {

                        body = body.Replace("{option}", "");
                    }
                }
                if (body.Contains("{amount}"))
                {
                    if (dr["dd_amt"] != null)
                    {
                        body = body.Replace("{amount}", dr["dd_amt"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{amount}", "");
                    }
                }
                if (body.Contains("{tdate}"))
                {
                    if (dr["dd_trdate"] != null)
                    {
                        body = body.Replace("{tdate}", dr["dd_trdate"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{tdate}", "");
                    }
                }
                if (body.Contains("{distributor}"))
                {
                    if (dr["dd_distributor"] != null)
                    {
                        body = body.Replace("{distributor}", dr["dd_distributor"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{distributor}", "");
                    }
                }
                if (body.Contains("{ihno}"))
                {
                    if (dr["Ihno"] != null)
                    {
                        body = body.Replace("{ihno}", dr["Ihno"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{ihno}", "");
                    }
                }
            }
            catch (Exception ex)
            {
                WriteLog(ex.Message + " - Inner Exception : " + ex.InnerException, ex.StackTrace, "RedemptionapprovalbyFinalApproval", dr["Ihno"].ToString());
                body = string.Empty;
            }

            return body;
        }
        private static string Redemptionapprovedbyapprover(StreamReader reader, DataRow dr)
        {
            string body = string.Empty;

            try
            {
                using (reader)
                {
                    body = reader.ReadToEnd();
                }
                //if (body.Contains("{FirstName}"))
                //{
                //    body = body.Replace("{FirstName}", dr["Name"].ToString());
                //}
                if (body.Contains("{folio}"))
                {
                    if (dr["dd_acno"] != null)
                    {
                        body = body.Replace("{folio}", dr["dd_acno"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{folio}", "");
                    }
                }
                if (body.Contains("{scheme}"))
                {
                    if (dr["dd_schemedesc"] != null)
                    {
                        body = body.Replace("{scheme}", dr["dd_schemedesc"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{scheme}", "");
                    }
                }
                if (body.Contains("{plan}"))
                {
                    if (dr["dd_plandesc"] != null)
                    {
                        body = body.Replace("{plan}", dr["dd_plandesc"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{plan}", "");
                    }
                }
                if (body.Contains("{option}"))
                {
                    if (dr["dd_optiondesc"] != null)
                    {
                        body = body.Replace("{option}", dr["dd_optiondesc"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{option}", "");
                    }
                }
                if (body.Contains("{amount}"))
                {
                    if (dr["dd_amt"] != null)
                    {
                        body = body.Replace("{amount}", dr["dd_amt"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{amount}", "");
                    }
                }
                if (body.Contains("{tdate}"))
                {
                    if (dr["dd_trdate"] != null)
                    {
                        body = body.Replace("{tdate}", dr["dd_trdate"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{tdate}", "");
                    }
                }
                //if (body.Contains("{distributor}"))
                //{
                //    body = body.Replace("{distributor}", dr["dd_distributor"].ToString());
                //}
                //if (body.Contains("{ihno}"))
                //{
                //    body = body.Replace("{ihno}", dr["Ihno"].ToString());
                //}
                if (body.Contains("{ARN/EUIN}"))
                {
                    if (dr["ARN/EUIN"] != null)
                    {
                        body = body.Replace("{ARN/EUIN}", dr["ARN/EUIN"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{ARN/EUIN}", "");
                    }
                }
                if (body.Contains("{refno}"))
                {
                    if (dr["Ihno"] != null)
                    {
                        body = body.Replace("{refno}", dr["Ihno"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{refno}", "");
                    }
                }
                if (body.Contains("{Apprname}"))
                {
                    if (dr["Apprname"] != null)
                    {
                        body = body.Replace("{Apprname}", dr["Apprname"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{Apprname}", "");
                    }
                }
                if (body.Contains("{Investor}"))
                {
                    if (dr["InvestorName"] != null)
                    {
                        body = body.Replace("{Investor}", dr["InvestorName"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{Investor}", "");
                    }
                }
                if (body.Contains("{PAN}"))
                {
                    if (dr["pan"] != null)
                    {
                        body = body.Replace("{PAN}", dr["pan"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{PAN}", "");
                    }
                }
            }
            catch (Exception ex)
            {
                WriteLog(ex.Message + " - Inner Exception : " + ex.InnerException, ex.StackTrace, "Redemptionapprovedbyapprover", dr["Ihno"].ToString());
                body = string.Empty;
            }

            return body;
        }
        private static string RedemptionbyInitiator(StreamReader reader, DataRow dr)
        {
            string body = string.Empty;

            string Ulink = ConfigurationManager.AppSettings["linkURL"];
            try
            {
                //string Link = Ulink + dr["Emailid"].ToString();
                var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(dr["Emailid"].ToString());
                string en_email = System.Convert.ToBase64String(plainTextBytes);

                string Link = Ulink + en_email;
                using (reader)
                {
                    body = reader.ReadToEnd();
                }
                if (body.Contains("{Link}"))
                {
                    body = body.Replace("{Link}", Link);
                }
                if (body.Contains("{FirstName}"))
                {
                    if (dr["Apprname"] != null)
                    {
                        body = body.Replace("{FirstName}", dr["Apprname"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{FirstName}", "");
                    }
                }
                if (body.Contains("{folio}"))
                {
                    if (dr["dd_acno"] != null)
                    {
                        body = body.Replace("{folio}", dr["dd_acno"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{folio}", "");
                    }
                }
                if (body.Contains("{scheme}"))
                {
                    if (dr["dd_schemedesc"] != null)
                    {
                        body = body.Replace("{scheme}", dr["dd_schemedesc"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{scheme}", "");
                    }
                }
                if (body.Contains("{plan}"))
                {
                    if (dr["dd_plandesc"] != null)
                    {
                        body = body.Replace("{plan}", dr["dd_plandesc"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{plan}", "");
                    }
                }
                if (body.Contains("{option}"))
                {
                    if (dr["dd_optiondesc"] != null)
                    {
                        body = body.Replace("{option}", dr["dd_optiondesc"].ToString());
                    }
                    else
                    {

                        body = body.Replace("{option}", "");
                    }
                }
                if (body.Contains("{amount}"))
                {
                    if (dr["dd_amt"] != null)
                    {
                        body = body.Replace("{amount}", dr["dd_amt"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{amount}", "");
                    }
                }
                if (body.Contains("{tdate}"))
                {
                    if (dr["dd_trdate"] != null)
                    {
                        body = body.Replace("{tdate}", dr["dd_trdate"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{tdate}", "");
                    }
                }
                if (body.Contains("{distributor}"))
                {
                    if (dr["dd_distributor"] != null)
                    {
                        body = body.Replace("{distributor}", dr["dd_distributor"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{distributor}", "");
                    }
                }
                if (body.Contains("{ihno}"))
                {
                    if (dr["Ihno"] != null)
                    {
                        body = body.Replace("{ihno}", dr["Ihno"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{ihno}", "");
                    }
                }
                if (body.Contains("{ARN/EUIN}"))
                {
                    if (dr["ARN/EUIN"] != null)
                    {
                        body = body.Replace("{ARN/EUIN}", dr["ARN/EUIN"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{ARN/EUIN}", "");
                    }
                }
                if (body.Contains("{refno}"))
                {
                    if (dr["refno"] != null)
                    {
                        body = body.Replace("{refno}", dr["refno"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{refno}", "");
                    }
                }
                if (body.Contains("{Investor}"))
                {
                    if (dr["InvestorName"] != null)
                    {
                        body = body.Replace("{Investor}", dr["InvestorName"].ToString());
                    }
                    else
                    {

                        body = body.Replace("{Investor}", "");
                    }
                }
                if (body.Contains("{PAN}"))
                {
                    if (dr["pan"] != null)
                    {
                        body = body.Replace("{PAN}", dr["pan"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{PAN}", "");
                    }
                }
                if (body.Contains("{bank}"))
                {
                    if (dr["BankName"] != null)
                    {
                        body = body.Replace("{bank}", dr["BankName"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{bank}", "");
                    }
                }

            }
            catch (Exception ex)
            {
                WriteLog(ex.Message + " - Inner Exception : " + ex.InnerException, ex.StackTrace, "RedemptionbyInitiator", dr["Ihno"].ToString());
                body = string.Empty;
            }

            return body;
        }
        private static string RedemptionbyKartha(StreamReader reader, DataRow dr)
        {
            string body = string.Empty;

            try
            {
                using (reader)
                {
                    body = reader.ReadToEnd();
                }
                if (body.Contains("{Firstname}"))
                {
                    if (dr["Name"] != null)
                    {
                        body = body.Replace("{Firstname}", dr["Name"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{Firstname}", "");
                    }
                }
                if (body.Contains("{folio}"))
                {
                    if (dr["dd_acno"] != null)
                    {
                        body = body.Replace("{folio}", dr["dd_acno"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{folio}", "");
                    }
                }
                if (body.Contains("{scheme}"))
                {
                    if (dr["dd_schemedesc"] != null)
                    {
                        body = body.Replace("{scheme}", dr["dd_schemedesc"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{scheme}", "");
                    }
                }
                if (body.Contains("{plan}"))
                {
                    if (dr["dd_plandesc"] != null)
                    {
                        body = body.Replace("{plan}", dr["dd_plandesc"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{plan}", "");
                    }
                }
                if (body.Contains("{option}"))
                {
                    if (dr["dd_optiondesc"] != null)
                    {
                        body = body.Replace("{option}", dr["dd_optiondesc"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{option}", "");
                    }
                }
                if (body.Contains("{amount}"))
                {
                    if (dr["dd_amt"] != null)
                    {
                        body = body.Replace("{amount}", dr["dd_amt"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{amount}", "");
                    }
                }
                if (body.Contains("{tdate}"))
                {
                    if (dr["dd_trdate"] != null)
                    {
                        body = body.Replace("{tdate}", dr["dd_trdate"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{tdate}", "");
                    }
                }
                if (body.Contains("{distributor}"))
                {
                    if (dr["dd_distributor"] != null)
                    {
                        body = body.Replace("{distributor}", dr["dd_distributor"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{distributor}", "");
                    }
                }
                if (body.Contains("{ihno}"))
                {
                    if (dr["Ihno"] != null)
                    {
                        body = body.Replace("{ihno}", dr["Ihno"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{ihno}", "");
                    }
                }
                if (body.Contains("{ARN/EUIN}"))
                {
                    if (dr["ARN/EUIN"] != null)
                    {
                        body = body.Replace("{ARN/EUIN}", dr["ARN/EUIN"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{ARN/EUIN}", "");
                    }
                }
                if (body.Contains("{refno}"))
                {
                    if (dr["refno"] != null)
                    {
                        body = body.Replace("{refno}", dr["refno"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{refno}", "");
                    }
                }
                if (body.Contains("{Investor}"))
                {
                    if (dr["InvestorName"] != null)
                    {
                        body = body.Replace("{Investor}", dr["InvestorName"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{Investor}", "");
                    }
                }
                if (body.Contains("{PAN}"))
                {
                    if (dr["pan"] != null)
                    {
                        body = body.Replace("{PAN}", dr["pan"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{PAN}", "");
                    }
                }
                if (body.Contains("{bank}"))
                {
                    if (dr["BankName"] != null)
                    {
                        body = body.Replace("{bank}", dr["BankName"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{bank}", "");
                    }
                }

            }
            catch (Exception ex)
            {
                WriteLog(ex.Message + " - Inner Exception : " + ex.InnerException, ex.StackTrace, "RedemptionbyKartha", dr["Ihno"].ToString());
                body = string.Empty;
            }

            return body;
        }
        private static string RedemptionRejectedbyApprover(StreamReader reader, DataRow dr)
        {
            string body = string.Empty;

            try
            {
                using (reader)
                {
                    body = reader.ReadToEnd();
                }
                //if (body.Contains("{FirstName}"))
                //{
                //    body = body.Replace("{FirstName}", dr["Name"].ToString());
                //}
                if (body.Contains("{folio}"))
                {
                    if (dr["dd_acno"] != null)
                    {
                        body = body.Replace("{folio}", dr["dd_acno"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{folio}", "");
                    }
                }
                if (body.Contains("{scheme}"))
                {
                    if (dr["dd_schemedesc"] != null)
                    {
                        body = body.Replace("{scheme}", dr["dd_schemedesc"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{scheme}", "");
                    }
                }
                if (body.Contains("{plan}"))
                {
                    if (dr["dd_plandesc"] != null)
                    {
                        body = body.Replace("{plan}", dr["dd_plandesc"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{plan}", "");
                    }
                }
                if (body.Contains("{option}"))
                {
                    if (dr["dd_optiondesc"] != null)
                    {
                        body = body.Replace("{option}", dr["dd_optiondesc"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{option}", "");
                    }
                }
                if (body.Contains("{amount}"))
                {
                    if (dr["dd_amt"] != null)
                    {
                        body = body.Replace("{amount}", dr["dd_amt"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{amount}", "");
                    }
                }
                if (body.Contains("{tdate}"))
                {
                    if (dr["dd_trdate"] != null)
                    {
                        body = body.Replace("{tdate}", dr["dd_trdate"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{tdate}", "");
                    }
                }
                //if (body.Contains("{distributor}"))
                //{
                //    body = body.Replace("{distributor}", dr["dd_distributor"].ToString());
                //}
                //if (body.Contains("{ihno}"))
                //{
                //    body = body.Replace("{ihno}", dr["Ihno"].ToString());
                //}
                if (body.Contains("{ARN/EUIN}"))
                {
                    if (dr["ARN/EUIN"] != null)
                    {
                        body = body.Replace("{ARN/EUIN}", dr["ARN/EUIN"].ToString());
                    }
                    else
                    {

                        body = body.Replace("{ARN/EUIN}", "");
                    }
                }
                if (body.Contains("{refno}"))
                {
                    if (dr["Ihno"] != null)
                    {
                        body = body.Replace("{refno}", dr["Ihno"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{refno}", "");
                    }
                }
                if (body.Contains("{Apprname}"))
                {
                    if (dr["Apprname"] != null)
                    {
                        body = body.Replace("{Apprname}", dr["Apprname"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{Apprname}", "");
                    }
                }
                if (body.Contains("{Investor}"))
                {
                    if (dr["InvestorName"] != null)
                    {
                        body = body.Replace("{Investor}", dr["InvestorName"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{Investor}", "");
                    }
                }
                if (body.Contains("{PAN}"))
                {
                    if (dr["pan"] != null)
                    {
                        body = body.Replace("{PAN}", dr["pan"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{PAN}", "");
                    }
                }
            }
            catch (Exception ex)
            {
                WriteLog(ex.Message + " - Inner Exception : " + ex.InnerException, ex.StackTrace, "RedemptionRejectedbyApprover", dr["Ihno"].ToString());
                body = string.Empty;
            }

            return body;
        }
        private static string STPapprovalbyapprovers(StreamReader reader, DataRow dr)
        {
            string body = string.Empty;

            try
            {
                using (reader)
                {
                    body = reader.ReadToEnd();
                }
                if (body.Contains("{FirstName}"))
                {
                    if (dr["Name"] != null)
                    {
                        body = body.Replace("{FirstName}", dr["Name"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{FirstName}", "");
                    }
                }
                if (body.Contains("{folio}"))
                {
                    if (dr["dd_acno"] != null)
                    {
                        body = body.Replace("{folio}", dr["dd_acno"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{folio}", "");
                    }
                }
                if (body.Contains("{STPAmount}"))
                {
                    if (dr["dd_amt"] != null)
                    {
                        body = body.Replace("{STPAmount}", dr["dd_amt"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{STPAmount}", "");
                    }
                }
                if (body.Contains("{Fopted}"))
                {
                    if (dr["FrequencyOpted"] != null)
                    {
                        body = body.Replace("{Fopted}", dr["FrequencyOpted"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{Fopted}", "");
                    }
                }

                if (body.Contains("{NoOfSTP}"))
                {
                    if (dr["dd_trdate"] != null)
                    {
                        body = body.Replace("{NoOfSTP}", dr["dd_trdate"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{NoOfSTP}", "");
                    }
                }
                if (body.Contains("{STPcommencementDate}"))
                {
                    if (dr["commencementdate"] != null)
                    {
                        body = body.Replace("{STPcommencementDate}", dr["commencementdate"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{STPcommencementDate}", "");
                    }
                }
                if (body.Contains("{STPendDate}"))
                {
                    if (dr["enddate"] != null)
                    {
                        body = body.Replace("{STPendDate}", dr["enddate"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{STPendDate}", "");
                    }
                }
                if (body.Contains("{ihno}"))
                {
                    if (dr["Ihno"] != null)
                    {
                        body = body.Replace("{ihno}", dr["Ihno"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{ihno}", "");
                    }
                }
                if (body.Contains("{Fromscheme}"))
                {
                    if (dr["dd_schemedesc"] != null)
                    {
                        body = body.Replace("{Fromscheme}", dr["dd_schemedesc"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{Fromscheme}", "");
                    }
                }
                if (body.Contains("{Toscheme}"))
                {
                    if (dr["ToScheme"] != null)
                    {
                        body = body.Replace("{Toscheme}", dr["ToScheme"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{Toscheme}", "");
                    }
                }
            }
            catch (Exception ex)
            {
                WriteLog(ex.Message + " - Inner Exception : " + ex.InnerException, ex.StackTrace, "STPapprovalbyapprovers", dr["Ihno"].ToString());
                body = string.Empty;
            }

            return body;
        }
        private static string STPapprovalbyFinalApproval(StreamReader reader, DataRow dr)
        {
            string body = string.Empty;

            try
            {
                using (reader)
                {
                    body = reader.ReadToEnd();
                }
                if (body.Contains("{FirstName}"))
                {
                    if (dr["Name"] != null)
                    {
                        body = body.Replace("{FirstName}", dr["Name"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{FirstName}", "");
                    }
                }
                if (body.Contains("{folio}"))
                {
                    if (dr["dd_acno"] != null)
                    {
                        body = body.Replace("{folio}", dr["dd_acno"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{folio}", "");
                    }
                }

                if (body.Contains("{STPAmount}"))
                {
                    if (dr["dd_amt"] != null)
                    {
                        body = body.Replace("{STPAmount}", dr["dd_amt"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{STPAmount}", "");
                    }
                }
                if (body.Contains("{Fopted}"))
                {
                    if (dr["FrequencyOpted"] != null)
                    {
                        body = body.Replace("{Fopted}", dr["FrequencyOpted"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{Fopted}", "");
                    }
                }

                if (body.Contains("{NoOfSTP}"))
                {
                    if (dr["dd_trdate"] != null)
                    {
                        body = body.Replace("{NoOfSTP}", dr["dd_trdate"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{NoOfSTP}", "");
                    }
                }
                if (body.Contains("{STPcommencementDate}"))
                {
                    if (dr["commencementdate"] != null)
                    {
                        body = body.Replace("{STPcommencementDate}", dr["commencementdate"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{STPcommencementDate}", "");
                    }
                }
                if (body.Contains("{STPendDate}"))
                {
                    if (dr["enddate"] != null)
                    {
                        body = body.Replace("{STPendDate}", dr["enddate"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{STPendDate}", "");
                    }
                }
                if (body.Contains("{ihno}"))
                {
                    if (dr["Ihno"] != null)
                    {
                        body = body.Replace("{ihno}", dr["Ihno"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{ihno}", "");
                    }
                }
                if (body.Contains("{Fromscheme}"))
                {
                    if (dr["dd_schemedesc"] != null)
                    {
                        body = body.Replace("{Fromscheme}", dr["dd_schemedesc"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{Fromscheme}", "");
                    }
                }
                if (body.Contains("{Toscheme}"))
                {
                    if (dr["ToScheme"] != null)
                    {
                        body = body.Replace("{Toscheme}", dr["ToScheme"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{Toscheme}", "");
                    }
                }
            }
            catch (Exception ex)
            {
                WriteLog(ex.Message + " - Inner Exception : " + ex.InnerException, ex.StackTrace, "STPapprovalbyFinalApproval", dr["Ihno"].ToString());
                body = string.Empty;
            }

            return body;
        }
        private static string STPbyInitiator(StreamReader reader, DataRow dr)
        {
            string body = string.Empty;

            try
            {
                using (reader)
                {
                    body = reader.ReadToEnd();
                }
                if (body.Contains("{FirstName}"))
                {
                    if (dr["Name"] != null)
                    {
                        body = body.Replace("{FirstName}", dr["Name"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{FirstName}", "");
                    }
                }
                if (body.Contains("{folio}"))
                {
                    if (dr["dd_acno"] != null)
                    {
                        body = body.Replace("{folio}", dr["dd_acno"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{folio}", "");
                    }
                }
                if (body.Contains("{STPAmount}"))
                {
                    if (dr["dd_amt"] != null)
                    {
                        body = body.Replace("{STPAmount}", dr["dd_amt"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{STPAmount}", "");
                    }
                }
                if (body.Contains("{Fopted}"))
                {
                    if (dr["FrequencyOpted"] != null)
                    {
                        body = body.Replace("{Fopted}", dr["FrequencyOpted"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{Fopted}", "");
                    }
                }

                if (body.Contains("{NoOfSTP}"))
                {
                    if (dr["dd_trdate"] != null)
                    {
                        body = body.Replace("{NoOfSTP}", dr["dd_trdate"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{NoOfSTP}", "");
                    }
                }
                if (body.Contains("{STPcommencementDate}"))
                {
                    if (dr["commencementdate"] != null)
                    {
                        body = body.Replace("{STPcommencementDate}", dr["commencementdate"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{STPcommencementDate}", "");
                    }
                }
                if (body.Contains("{STPendDate}"))
                {
                    if (dr["enddate"] != null)
                    {
                        body = body.Replace("{STPendDate}", dr["enddate"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{STPendDate}", "");
                    }
                }
                if (body.Contains("{ihno}"))
                {
                    if (dr["Ihno"] != null)
                    {
                        body = body.Replace("{ihno}", dr["Ihno"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{ihno}", "");
                    }
                }
                if (body.Contains("{Fromscheme}"))
                {
                    if (dr["dd_schemedesc"] != null)
                    {
                        body = body.Replace("{Fromscheme}", dr["dd_schemedesc"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{Fromscheme}", "");
                    }
                }
                if (body.Contains("{Toscheme}"))
                {
                    if (dr["ToScheme"] != null)
                    {
                        body = body.Replace("{Toscheme}", dr["ToScheme"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{Toscheme}", "");
                    }
                }
            }
            catch (Exception ex)
            {
                WriteLog(ex.Message + " - Inner Exception : " + ex.InnerException, ex.StackTrace, "STPbyInitiator", dr["Ihno"].ToString());
                body = string.Empty;
            }

            return body;
        }
        private static string STPRejectedbyApprover(StreamReader reader, DataRow dr)
        {
            string body = string.Empty;

            try
            {
                using (reader)
                {
                    body = reader.ReadToEnd();
                }
                if (body.Contains("{FirstName}"))
                {
                    if (dr["Name"] != null)
                    {
                        body = body.Replace("{FirstName}", dr["Name"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{FirstName}", "");
                    }
                }
                if (body.Contains("{folio}"))
                {
                    if (dr["dd_acno"] != null)
                    {
                        body = body.Replace("{folio}", dr["dd_acno"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{folio}", "");
                    }
                }
                if (body.Contains("{STPAmount}"))
                {
                    if (dr["dd_amt"] != null)
                    {
                        body = body.Replace("{STPAmount}", dr["dd_amt"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{STPAmount}", "");
                    }
                }
                if (body.Contains("{Fopted}"))
                {
                    if (dr["FrequencyOpted"] != null)
                    {
                        body = body.Replace("{Fopted}", dr["FrequencyOpted"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{Fopted}", "");
                    }
                }

                if (body.Contains("{NoOfSTP}"))
                {
                    if (dr["dd_trdate"] != null)
                    {
                        body = body.Replace("{NoOfSTP}", dr["dd_trdate"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{NoOfSTP}", "");
                    }
                }
                if (body.Contains("{STPcommencementDate}"))
                {
                    if (dr["commencementdate"] != null)
                    {
                        body = body.Replace("{STPcommencementDate}", dr["commencementdate"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{STPcommencementDate}", "");
                    }
                }
                if (body.Contains("{STPendDate}"))
                {
                    if (dr["enddate"] != null)
                    {
                        body = body.Replace("{STPendDate}", dr["enddate"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{STPendDate}", "");
                    }
                }
                if (body.Contains("{ihno}"))
                {
                    if (dr["Ihno"] != null)
                    {
                        body = body.Replace("{ihno}", dr["Ihno"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{ihno}", "");
                    }
                }
                if (body.Contains("{Fromscheme}"))
                {
                    if (dr["dd_schemedesc"] != null)
                    {
                        body = body.Replace("{Fromscheme}", dr["dd_schemedesc"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{Fromscheme}", "");
                    }
                }
                if (body.Contains("{Toscheme}"))
                {
                    if (dr["ToScheme"] != null)
                    {
                        body = body.Replace("{Toscheme}", dr["ToScheme"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{Toscheme}", "");
                    }
                }
            }
            catch (Exception ex)
            {
                WriteLog(ex.Message + " - Inner Exception : " + ex.InnerException, ex.StackTrace, "STPRejectedbyApprover", dr["Ihno"].ToString());
                body = string.Empty;
            }

            return body;
        }
        private static string Switchapprovalbyapprovers(StreamReader reader, DataRow dr)
        {
            string body = string.Empty;

            try
            {
                using (reader)
                {
                    body = reader.ReadToEnd();
                }
                if (body.Contains("{FirstName}"))
                {
                    if (dr["Apprname"] != null)
                    {
                        body = body.Replace("{FirstName}", dr["Apprname"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{FirstName}", "");
                    }
                }
                if (body.Contains("{folio}"))
                {
                    if (dr["dd_acno"] != null)
                    {
                        body = body.Replace("{folio}", dr["dd_acno"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{folio}", "");
                    }
                }

                if (body.Contains("{amount}"))
                {
                    if (dr["dd_amt"] != null)
                    {
                        body = body.Replace("{amount}", dr["dd_amt"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{amount}", "");
                    }
                }
                if (body.Contains("{tdate}"))
                {
                    if (dr["dd_trdate"] != null)
                    {
                        body = body.Replace("{tdate}", dr["dd_trdate"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{tdate}", "");
                    }
                }
                if (body.Contains("{switchindate}"))
                {
                    if (dr["commencementdate"] != null)
                    {
                        body = body.Replace("{switchindate}", dr["commencementdate"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{switchindate}", "");
                    }
                }

                if (body.Contains("{switchoutdate}"))
                {
                    if (dr["enddate"] != null)
                    {
                        body = body.Replace("{switchoutdate}", dr["enddate"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{switchoutdate}", "");
                    }
                }
                if (body.Contains("{distributor}"))
                {
                    if (dr["dd_distributor"] != null)
                    {
                        body = body.Replace("{distributor}", dr["dd_distributor"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{distributor}", "");
                    }
                }
                if (body.Contains("{ihno}"))
                {
                    if (dr["Ihno"] != null)
                    {
                        body = body.Replace("{ihno}", dr["Ihno"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{ihno}", "");
                    }
                }
                if (body.Contains("{Fromscheme}"))
                {
                    if (dr["dd_schemedesc"] != null)
                    {
                        body = body.Replace("{Fromscheme}", dr["dd_schemedesc"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{Fromscheme}", "");
                    }
                }
                if (body.Contains("{Toscheme}"))
                {
                    if (dr["fm_tschdesc"] != null)
                    {
                        body = body.Replace("{Toscheme}", dr["fm_tschdesc"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{Toscheme}", "");
                    }
                }

                if (body.Contains("{FromPlan}"))
                {
                    if (dr["dd_plandesc"] != null)
                    {
                        body = body.Replace("{FromPlan}", dr["dd_plandesc"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{FromPlan}", "");
                    }
                }
                if (body.Contains("{ToPlan}"))
                {
                    if (dr["fm_tplandesc"] != null)
                    {
                        body = body.Replace("{ToPlan}", dr["fm_tplandesc"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{ToPlan}", "");
                    }
                }

                if (body.Contains("{FromOption}"))
                {
                    if (dr["dd_optiondesc"] != null)
                    {
                        body = body.Replace("{FromOption}", dr["dd_optiondesc"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{FromOption}", "");
                    }
                }
                if (body.Contains("{ToOption}"))
                {
                    if (dr["fm_toptiondesc"] != null)
                    {
                        body = body.Replace("{ToOption}", dr["fm_toptiondesc"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{ToOption}", "");
                    }
                }

                if (body.Contains("{ARN/EUIN}"))
                {
                    if (dr["ARN/EUIN"] != null)
                    {
                        body = body.Replace("{ARN/EUIN}", dr["ARN/EUIN"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{ARN/EUIN}", "");
                    }
                }
                if (body.Contains("{refno}"))
                {
                    if (dr["refno"] != null)
                    {
                        body = body.Replace("{refno}", dr["refno"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{refno}", "");
                    }
                }
                if (body.Contains("{Apprname}"))
                {
                    if (dr["Apprname"] != null)
                    {
                        body = body.Replace("{Apprname}", dr["Apprname"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{Apprname}", "");
                    }
                }
                if (body.Contains("{Investor}"))
                {
                    if (dr["InvestorName"] != null)
                    {
                        body = body.Replace("{Investor}", dr["InvestorName"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{Investor}", "");
                    }
                }
                if (body.Contains("{PAN}"))
                {
                    if (dr["pan"] != null)
                    {
                        body = body.Replace("{PAN}", dr["pan"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{PAN}", "");
                    }
                }
            }
            catch (Exception ex)
            {
                WriteLog(ex.Message + " - Inner Exception : " + ex.InnerException, ex.StackTrace, "Switchapprovalbyapprovers", dr["Ihno"].ToString());
                body = string.Empty;
            }

            return body;
        }
        private static string SwitchapprovalbyFinalApproval(StreamReader reader, DataRow dr)
        {
            string body = string.Empty;

            try
            {
                using (reader)
                {
                    body = reader.ReadToEnd();
                }
                if (body.Contains("{FirstName}"))
                {
                    if (dr["Apprname"] != null)
                    {
                        body = body.Replace("{FirstName}", dr["Apprname"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{FirstName}", "");
                    }
                }
                if (body.Contains("{folio}"))
                {
                    if (dr["dd_acno"] != null)
                    {
                        body = body.Replace("{folio}", dr["dd_acno"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{folio}", "");
                    }
                }

                if (body.Contains("{amount}"))
                {
                    if (dr["dd_amt"] != null)
                    {
                        body = body.Replace("{amount}", dr["dd_amt"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{amount}", "");
                    }
                }
                if (body.Contains("{tdate}"))
                {
                    if (dr["dd_trdate"] != null)
                    {
                        body = body.Replace("{tdate}", dr["dd_trdate"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{tdate}", "");
                    }
                }
                if (body.Contains("{switchindate}"))
                {
                    if (dr["commencementdate"] != null)
                    {
                        body = body.Replace("{switchindate}", dr["commencementdate"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{switchindate}", "");
                    }
                }

                if (body.Contains("{switchoutdate}"))
                {
                    if (dr["enddate"] != null)
                    {
                        body = body.Replace("{switchoutdate}", dr["enddate"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{switchoutdate}", "");
                    }
                }
                if (body.Contains("{distributor}"))
                {
                    if (dr["dd_distributor"] != null)
                    {
                        body = body.Replace("{distributor}", dr["dd_distributor"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{distributor}", "");
                    }
                }
                if (body.Contains("{ihno}"))
                {
                    if (dr["Ihno"] != null)
                    {
                        body = body.Replace("{ihno}", dr["Ihno"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{ihno}", "");
                    }
                }
                if (body.Contains("{Fromscheme}"))
                {
                    if (dr["dd_schemedesc"] != null)
                    {
                        body = body.Replace("{Fromscheme}", dr["dd_schemedesc"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{Fromscheme}", "");
                    }
                }
                if (body.Contains("{Toscheme}"))
                {
                    if (dr["fm_tschdesc"] != null)
                    {
                        body = body.Replace("{Toscheme}", dr["fm_tschdesc"].ToString());
                    }
                    else
                    {

                        body = body.Replace("{Toscheme}", "");
                    }
                }

                if (body.Contains("{FromPlan}"))
                {
                    if (dr["dd_plandesc"] != null)
                    {
                        body = body.Replace("{FromPlan}", dr["dd_plandesc"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{FromPlan}", "");
                    }
                }
                if (body.Contains("{ToPlan}"))
                {
                    if (dr["fm_tplandesc"] != null)
                    {
                        body = body.Replace("{ToPlan}", dr["fm_tplandesc"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{ToPlan}", "");
                    }
                }

                if (body.Contains("{FromOption}"))
                {
                    if (dr["dd_optiondesc"] != null)
                    {
                        body = body.Replace("{FromOption}", dr["dd_optiondesc"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{FromOption}", "");
                    }
                }
                if (body.Contains("{ToOption}"))
                {
                    if (dr["fm_toptiondesc"] != null)
                    {
                        body = body.Replace("{ToOption}", dr["fm_toptiondesc"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{ToOption}", "");
                    }
                }

                if (body.Contains("{fundname}"))
                {
                    if (dr["fm_fundname"] != null)
                    {
                        body = body.Replace("{fundname}", dr["fm_fundname"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{fundname}", "");
                    }
                }
                if (body.Contains("{Investor}"))
                {
                    if (dr["InvestorName"] != null)
                    {
                        body = body.Replace("{Investor}", dr["InvestorName"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{Investor}", "");
                    }
                }
                if (body.Contains("{PAN}"))
                {
                    if (dr["pan"] != null)
                    {
                        body = body.Replace("{PAN}", dr["pan"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{PAN}", "");
                    }
                }
            }
            catch (Exception ex)
            {
                WriteLog(ex.Message + " - Inner Exception : " + ex.InnerException, ex.StackTrace, "SwitchapprovalbyFinalApproval", dr["Ihno"].ToString());
                body = string.Empty;
            }

            return body;
        }
        private static string SwitchbyInitiator(StreamReader reader, DataRow dr)
        {
            string body = string.Empty;

            string Ulink = ConfigurationManager.AppSettings["linkURL"];
            try
            {
                //string Link = Ulink + dr["Emailid"].ToString();
                var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(dr["Emailid"].ToString());
                string en_email = System.Convert.ToBase64String(plainTextBytes);

                string Link = Ulink + en_email;
                using (reader)
                {
                    body = reader.ReadToEnd();
                }
                if (body.Contains("{Link}"))
                {
                    body = body.Replace("{Link}", Link);
                }
                if (body.Contains("{FirstName}"))
                {
                    if (dr["Apprname"] != null)
                    {
                        body = body.Replace("{FirstName}", dr["Apprname"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{FirstName}", "");
                    }
                }
                if (body.Contains("{folio}"))
                {
                    if (dr["dd_acno"] != null)
                    {
                        body = body.Replace("{folio}", dr["dd_acno"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{folio}", "");
                    }
                }

                if (body.Contains("{amount}"))
                {
                    if (dr["dd_amt"] != null)
                    {
                        body = body.Replace("{amount}", dr["dd_amt"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{amount}", "");
                    }
                }
                if (body.Contains("{tdate}"))
                {
                    if (dr["dd_trdate"] != null)
                    {
                        body = body.Replace("{tdate}", dr["dd_trdate"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{tdate}", "");
                    }
                }
                //if (body.Contains("{switchindate}"))
                //{
                //    body = body.Replace("{switchindate}", dr["commencementdate"].ToString());
                //}

                //if (body.Contains("{switchoutdate}"))
                //{
                //    body = body.Replace("{switchoutdate}", dr["enddate"].ToString());
                //}
                //if (body.Contains("{distributor}"))
                //{
                //    body = body.Replace("{distributor}", dr["dd_distributor"].ToString());
                //}
                //if (body.Contains("{ihno}"))
                //{
                //    body = body.Replace("{ihno}", dr["Ihno"].ToString());
                //}
                if (body.Contains("{Fromscheme}"))
                {
                    if (dr["dd_schemedesc"] != null)
                    {
                        body = body.Replace("{Fromscheme}", dr["dd_schemedesc"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{Fromscheme}", "");
                    }
                }
                if (body.Contains("{Toscheme}"))
                {
                    if (dr["fm_tschdesc"] != null)
                    {
                        body = body.Replace("{Toscheme}", dr["fm_tschdesc"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{Toscheme}", "");
                    }
                }
                if (body.Contains("{Plan}"))
                {
                    if (dr["dd_plandesc"] != null)
                    {
                        body = body.Replace("{Plan}", dr["dd_plandesc"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{Plan}", "");
                    }
                }
                if (body.Contains("{ToPlan}"))
                {
                    if (dr["fm_tplandesc"] != null)
                    {
                        body = body.Replace("{ToPlan}", dr["fm_tplandesc"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{ToPlan}", "");
                    }
                }
                if (body.Contains("{Option}"))
                {
                    if (dr["dd_optiondesc"] != null)
                    {
                        body = body.Replace("{Option}", dr["dd_optiondesc"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{Option}", "");
                    }
                }
                if (body.Contains("{ToOption}"))
                {
                    if (dr["fm_toptiondesc"] != null)
                    {
                        body = body.Replace("{ToOption}", dr["fm_toptiondesc"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{ToOption}", "");
                    }
                }
                if (body.Contains("{ARN/EUIN}"))
                {
                    if (dr["ARN/EUIN"] != null)
                    {
                        body = body.Replace("{ARN/EUIN}", dr["ARN/EUIN"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{ARN/EUIN}", "");
                    }
                }
                if (body.Contains("{refno}"))
                {
                    if (dr["refno"] != null)
                    {
                        body = body.Replace("{refno}", dr["refno"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{refno}", "");
                    }
                }
                if (body.Contains("{Apprname}"))
                {
                    if (dr["Apprname"] != null)
                    {
                        body = body.Replace("{Apprname}", dr["Apprname"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{Apprname}", "");
                    }
                }
                if (body.Contains("{Investor}"))
                {
                    if (dr["InvestorName"] != null)
                    {
                        body = body.Replace("{Investor}", dr["InvestorName"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{Investor}", "");
                    }
                }
                if (body.Contains("{PAN}"))
                {
                    if (dr["pan"] != null)
                    {
                        body = body.Replace("{PAN}", dr["pan"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{PAN}", "");
                    }
                }

            }
            catch (Exception ex)
            {
                WriteLog(ex.Message + " - Inner Exception : " + ex.InnerException, ex.StackTrace, "SwitchbyInitiator", dr["Emailid"].ToString());
                body = string.Empty;
            }

            return body;
        }
        private static string SwitchbyKartha(StreamReader reader, DataRow dr)
        {
            string body = string.Empty;

            try
            {
                using (reader)
                {
                    body = reader.ReadToEnd();
                }
                if (body.Contains("{Firstname}"))
                {
                    if (dr["Name"] != null)
                    {
                        body = body.Replace("{Firstname}", dr["Name"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{Firstname}", "");
                    }
                }
                if (body.Contains("{folio}"))
                {
                    if (dr["dd_acno"] != null)
                    {
                        body = body.Replace("{folio}", dr["dd_acno"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{folio}", "");
                    }
                }

                if (body.Contains("{amount}"))
                {
                    if (dr["dd_amt"] != null)
                    {
                        body = body.Replace("{amount}", dr["dd_amt"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{amount}", "");
                    }
                }
                if (body.Contains("{tdate}"))
                {
                    if (dr["dd_trdate"] != null)
                    {
                        body = body.Replace("{tdate}", dr["dd_trdate"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{tdate}", "");
                    }
                }
                if (body.Contains("{switchindate}"))
                {
                    if (dr["commencementdate"] != null)
                    {
                        body = body.Replace("{switchindate}", dr["commencementdate"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{switchindate}", "");
                    }
                }

                if (body.Contains("{switchoutdate}"))
                {
                    if (dr["enddate"] != null)
                    {
                        body = body.Replace("{switchoutdate}", dr["enddate"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{switchoutdate}", "");
                    }
                }
                if (body.Contains("{distributor}"))
                {
                    if (dr["dd_distributor"] != null)
                    {
                        body = body.Replace("{distributor}", dr["dd_distributor"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{distributor}", "");
                    }
                }
                if (body.Contains("{ihno}"))
                {
                    if (dr["Ihno"] != null)
                    {
                        body = body.Replace("{ihno}", dr["Ihno"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{ihno}", "");
                    }
                }
                if (body.Contains("{Fromscheme}"))
                {
                    if (dr["dd_schemedesc"] != null)
                    {
                        body = body.Replace("{Fromscheme}", dr["dd_schemedesc"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{Fromscheme}", "");
                    }
                }
                if (body.Contains("{Toscheme}"))
                {
                    if (dr["fm_tschdesc"] != null)
                    {
                        body = body.Replace("{Toscheme}", dr["fm_tschdesc"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{Toscheme}", "");
                    }
                }

                if (body.Contains("{FromPlan}"))
                {
                    if (dr["dd_plandesc"] != null)
                    {
                        body = body.Replace("{FromPlan}", dr["dd_plandesc"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{FromPlan}", "");
                    }
                }
                if (body.Contains("{ToPlan}"))
                {
                    if (dr["fm_tplandesc"] != null)
                    {
                        body = body.Replace("{ToPlan}", dr["fm_tplandesc"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{ToPlan}", "");
                    }
                }

                if (body.Contains("{FromOption}"))
                {
                    if (dr["dd_optiondesc"] != null)
                    {
                        body = body.Replace("{FromOption}", dr["dd_optiondesc"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{FromOption}", "");
                    }
                }
                if (body.Contains("{ToOption}"))
                {
                    if (dr["fm_toptiondesc"] != null)
                    {
                        body = body.Replace("{ToOption}", dr["fm_toptiondesc"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{ToOption}", "");
                    }
                }
                if (body.Contains("{ARN/EUIN}"))
                {
                    if (dr["ARN/EUIN"] != null)
                    {
                        body = body.Replace("{ARN/EUIN}", dr["ARN/EUIN"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{ARN/EUIN}", "");
                    }
                }
                if (body.Contains("{refno}"))
                {
                    if (dr["refno"] != null)
                    {
                        body = body.Replace("{refno}", dr["refno"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{refno}", "");
                    }
                }
                if (body.Contains("{Apprname}"))
                {
                    if (dr["Apprname"] != null)
                    {
                        body = body.Replace("{Apprname}", dr["Apprname"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{Apprname}", "");
                    }
                }

                if (body.Contains("{Apprname}"))
                {
                    if (dr["Apprname"] != null)
                    {
                        body = body.Replace("{Apprname}", dr["Apprname"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{Apprname}", "");
                    }
                }

                if (body.Contains("{Apprname}"))
                {
                    if (dr["Apprname"] != null)
                    {
                        body = body.Replace("{Apprname}", dr["Apprname"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{Apprname}", "");
                    }
                }
                if (body.Contains("{Investor}"))
                {
                    if (dr["InvestorName"] != null)
                    {
                        body = body.Replace("{Investor}", dr["InvestorName"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{Investor}", "");
                    }
                }
                if (body.Contains("{PAN}"))
                {
                    if (dr["pan"] != null)
                    {
                        body = body.Replace("{PAN}", dr["pan"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{PAN}", "");
                    }
                }
            }
            catch (Exception ex)
            {
                WriteLog(ex.Message + " - Inner Exception : " + ex.InnerException, ex.StackTrace, "SwitchbyKartha", dr["refno"].ToString());
                body = string.Empty;
            }

            return body;
        }
        private static string SwitchRejectedbyApprover(StreamReader reader, DataRow dr)
        {
            string body = string.Empty;

            try
            {
                using (reader)
                {
                    body = reader.ReadToEnd();
                }
                if (body.Contains("{FirstName}"))
                {
                    if (dr["Apprname"] != null)
                    {
                        body = body.Replace("{FirstName}", dr["Apprname"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{FirstName}", "");
                    }
                }
                if (body.Contains("{folio}"))
                {
                    if (dr["dd_acno"] != null)
                    {
                        body = body.Replace("{folio}", dr["dd_acno"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{folio}", "");
                    }
                }

                if (body.Contains("{amount}"))
                {
                    if (dr["dd_amt"] != null)
                    {
                        body = body.Replace("{amount}", dr["dd_amt"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{amount}", "");
                    }
                }
                if (body.Contains("{tdate}"))
                {
                    if (dr["dd_trdate"] != null)
                    {
                        body = body.Replace("{tdate}", dr["dd_trdate"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{tdate}", "");
                    }
                }
                if (body.Contains("{switchindate}"))
                {
                    if (dr["commencementdate"] != null)
                    {
                        body = body.Replace("{switchindate}", dr["commencementdate"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{switchindate}", "");
                    }
                }

                if (body.Contains("{switchoutdate}"))
                {
                    if (dr["enddate"] != null)
                    {
                        body = body.Replace("{switchoutdate}", dr["enddate"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{switchoutdate}", "");
                    }
                }
                if (body.Contains("{distributor}"))
                {
                    if (dr["dd_distributor"] != null)
                    {
                        body = body.Replace("{distributor}", dr["dd_distributor"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{distributor}", "");
                    }
                }
                if (body.Contains("{ihno}"))
                {
                    if (dr["Ihno"] != null)
                    {
                        body = body.Replace("{ihno}", dr["Ihno"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{ihno}", "");
                    }
                }
                if (body.Contains("{Fromscheme}"))
                {
                    if (dr["dd_schemedesc"] != null)
                    {
                        body = body.Replace("{Fromscheme}", dr["dd_schemedesc"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{Fromscheme}", "");
                    }
                }
                if (body.Contains("{Toscheme}"))
                {
                    if (dr["fm_tschdesc"] != null)
                    {
                        body = body.Replace("{Toscheme}", dr["fm_tschdesc"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{Toscheme}", "");
                    }
                }

                if (body.Contains("{FromPlan}"))
                {
                    if (dr["dd_plandesc"] != null)
                    {
                        body = body.Replace("{FromPlan}", dr["dd_plandesc"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{FromPlan}", "");
                    }
                }
                if (body.Contains("{ToPlan}"))
                {
                    if (dr["fm_tplandesc"] != null)
                    {
                        body = body.Replace("{ToPlan}", dr["fm_tplandesc"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{ToPlan}", "");
                    }
                }

                if (body.Contains("{FromOption}"))
                {
                    if (dr["dd_optiondesc"] != null)
                    {
                        body = body.Replace("{FromOption}", dr["dd_optiondesc"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{FromOption}", "");
                    }
                }
                if (body.Contains("{ToOption}"))
                {
                    if (dr["fm_toptiondesc"] != null)
                    {
                        body = body.Replace("{ToOption}", dr["fm_toptiondesc"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{ToOption}", "");
                    }
                }

                if (body.Contains("{ARN/EUIN}"))
                {
                    if (dr["ARN/EUIN"] != null)
                    {
                        body = body.Replace("{ARN/EUIN}", dr["ARN/EUIN"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{ARN/EUIN}", "");
                    }
                }
                if (body.Contains("{refno}"))
                {
                    if (dr["refno"] != null)
                    {
                        body = body.Replace("{refno}", dr["refno"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{refno}", "");
                    }
                }
                if (body.Contains("{Apprname}"))
                {
                    if (dr["Apprname"] != null)
                    {
                        body = body.Replace("{Apprname}", dr["Apprname"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{Apprname}", "");
                    }
                }
                if (body.Contains("{Investor}"))
                {
                    if (dr["InvestorName"] != null)
                    {
                        body = body.Replace("{Investor}", dr["InvestorName"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{Investor}", "");
                    }
                }
                if (body.Contains("{PAN}"))
                {
                    if (dr["pan"] != null)
                    {
                        body = body.Replace("{PAN}", dr["pan"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{PAN}", "");
                    }
                }

            }
            catch (Exception ex)
            {
                WriteLog(ex.Message + " - Inner Exception : " + ex.InnerException, ex.StackTrace, "SwitchRejectedbyApprover", dr["Ihno"].ToString());
                body = string.Empty;
            }

            return body;
        }
        private static string SWPapprovalbyapprovers(StreamReader reader, DataRow dr)
        {
            string body = string.Empty;

            try
            {
                using (reader)
                {
                    body = reader.ReadToEnd();
                }
                if (body.Contains("{FirstName}"))
                {
                    if (dr["Name"] != null)
                    {
                        body = body.Replace("{FirstName}", dr["Name"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{FirstName}", "");
                    }
                }
                if (body.Contains("{folio}"))
                {
                    if (dr["dd_acno"] != null)
                    {
                        body = body.Replace("{folio}", dr["dd_acno"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{folio}", "");
                    }
                }
                if (body.Contains("{amount}"))
                {
                    if (dr["dd_amt"] != null)
                    {
                        body = body.Replace("{amount}", dr["dd_amt"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{amount}", "");
                    }
                }
                if (body.Contains("{Fopted}"))
                {
                    if (dr["FrequencyOpted"] != null)
                    {
                        body = body.Replace("{Fopted}", dr["FrequencyOpted"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{Fopted}", "");
                    }
                }
                if (body.Contains("{{swpenddate}}"))
                {
                    if (dr["enddate"] != null)
                    {
                        body = body.Replace("{{swpenddate}}", dr["enddate"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{{swpenddate}}", "");
                    }
                }

                if (body.Contains("{noofwithdrawls}"))
                {
                    if (dr["dd_trdate"] != null)
                    {
                        body = body.Replace("{noofwithdrawls}", dr["dd_trdate"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{noofwithdrawls}", "");
                    }
                }
                if (body.Contains("{swpcomemtdate}"))
                {
                    if (dr["commencementdate"] != null)
                    {
                        body = body.Replace("{swpcomemtdate}", dr["commencementdate"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{swpcomemtdate}", "");
                    }
                }
                if (body.Contains("{distributor}"))
                {
                    if (dr["dd_distributor"] != null)
                    {
                        body = body.Replace("{distributor}", dr["dd_distributor"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{distributor}", "");
                    }
                }
                if (body.Contains("{ihno}"))
                {
                    if (dr["Ihno"] != null)
                    {
                        body = body.Replace("{ihno}", dr["Ihno"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{ihno}", "");
                    }
                }
                if (body.Contains("{Fromscheme}"))
                {
                    if (dr["dd_schemedesc"] != null)
                    {
                        body = body.Replace("{Fromscheme}", dr["dd_schemedesc"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{Fromscheme}", "");
                    }
                }

                if (body.Contains("{Toscheme}"))
                {
                    if (dr["fm_tschdesc"] != null)
                    {
                        body = body.Replace("{Toscheme}", dr["fm_tschdesc"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{Toscheme}", "");
                    }
                }

                if (body.Contains("{FromPlan}"))
                {
                    if (dr["dd_plandesc"] != null)
                    {
                        body = body.Replace("{FromPlan}", dr["dd_plandesc"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{FromPlan}", "");
                    }
                }
                if (body.Contains("{ToPlan}"))
                {
                    if (dr["fm_tplandesc"] != null)
                    {
                        body = body.Replace("{ToPlan}", dr["fm_tplandesc"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{ToPlan}", "");
                    }
                }

                if (body.Contains("{FromOption}"))
                {
                    if (dr["dd_optiondesc"] != null)
                    {
                        body = body.Replace("{FromOption}", dr["dd_optiondesc"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{FromOption}", "");
                    }
                }
                if (body.Contains("{ToOption}"))
                {
                    if (dr["fm_toptiondesc"] != null)
                    {
                        body = body.Replace("{ToOption}", dr["fm_toptiondesc"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{ToOption}", "");
                    }
                }

                if (body.Contains("{ARN/EUIN}"))
                {
                    if (dr["ARN/EUIN"] != null)
                    {
                        body = body.Replace("{ARN/EUIN}", dr["ARN/EUIN"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{ARN/EUIN}", "");
                    }
                }
                if (body.Contains("{refno}"))
                {
                    if (dr["refno"] != null)
                    {
                        body = body.Replace("{refno}", dr["refno"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{refno}", "");
                    }
                }
                if (body.Contains("{Apprname}"))
                {
                    if (dr["Apprname"] != null)
                    {
                        body = body.Replace("{Apprname}", dr["Apprname"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{Apprname}", "");
                    }
                }

            }
            catch (Exception ex)
            {
                WriteLog(ex.Message + " - Inner Exception : " + ex.InnerException, ex.StackTrace, "SWPapprovalbyapprovers", dr["Ihno"].ToString());
                body = string.Empty;
            }

            return body;
        }
        private static string SWPapprovalbyFinalApproval(StreamReader reader, DataRow dr)
        {
            string body = string.Empty;

            try
            {
                using (reader)
                {
                    body = reader.ReadToEnd();
                }
                if (body.Contains("{FirstName}"))
                {
                    if (dr["Name"] != null)
                    {
                        body = body.Replace("{FirstName}", dr["Name"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{FirstName}", "");
                    }
                }
                if (body.Contains("{folio}"))
                {
                    if (dr["dd_acno"] != null)
                    {
                        body = body.Replace("{folio}", dr["dd_acno"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{folio}", "");
                    }
                }
                if (body.Contains("{amount}"))
                {
                    if (dr["dd_amt"].ToString() != null)
                    {
                        body = body.Replace("{amount}", dr["dd_amt"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{amount}", "");
                    }
                }
                if (body.Contains("{Fopted}"))
                {
                    if (dr["FrequencyOpted"] != null)
                    {
                        body = body.Replace("{Fopted}", dr["FrequencyOpted"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{Fopted}", "");
                    }
                }
                if (body.Contains("{{swpenddate}}"))
                {
                    if (dr["enddate"] != null)
                    {
                        body = body.Replace("{{swpenddate}}", dr["enddate"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{{swpenddate}}", "");
                    }
                }

                if (body.Contains("{noofwithdrawls}"))
                {
                    if (dr["dd_trdate"] != null)
                    {
                        body = body.Replace("{noofwithdrawls}", dr["dd_trdate"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{noofwithdrawls}", "");
                    }
                }
                if (body.Contains("{swpcomemtdate}"))
                {
                    if (dr["commencementdate"] != null)
                    {
                        body = body.Replace("{swpcomemtdate}", dr["commencementdate"].ToString());
                    }
                    else
                    {

                        body = body.Replace("{swpcomemtdate}", "");
                    }
                }
                if (body.Contains("{distributor}"))
                {
                    if (dr["dd_distributor"] != null)
                    {
                        body = body.Replace("{distributor}", dr["dd_distributor"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{distributor}", "");
                    }
                }
                if (body.Contains("{ihno}"))
                {
                    if (dr["Ihno"] != null)
                    {
                        body = body.Replace("{ihno}", dr["Ihno"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{ihno}", "");
                    }
                }
                if (body.Contains("{Fromscheme}"))
                {
                    if (dr["dd_schemedesc"] != null)
                    {
                        body = body.Replace("{Fromscheme}", dr["dd_schemedesc"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{Fromscheme}", "");
                    }
                }


                if (body.Contains("{Toscheme}"))
                {
                    if (dr["fm_tschdesc"] != null)
                    {
                        body = body.Replace("{Toscheme}", dr["fm_tschdesc"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{Toscheme}", "");
                    }
                }

                if (body.Contains("{FromPlan}"))
                {
                    if (dr["dd_plandesc"] != null)
                    {
                        body = body.Replace("{FromPlan}", dr["dd_plandesc"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{FromPlan}", "");
                    }
                }
                if (body.Contains("{ToPlan}"))
                {
                    if (dr["fm_tplandesc"] != null)
                    {
                        body = body.Replace("{ToPlan}", dr["fm_tplandesc"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{ToPlan}", "");
                    }
                }

                if (body.Contains("{FromOption}"))
                {
                    if (dr["dd_optiondesc"] != null)
                    {
                        body = body.Replace("{FromOption}", dr["dd_optiondesc"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{FromOption}", "");
                    }
                }
                if (body.Contains("{ToOption}"))
                {
                    if (dr["fm_toptiondesc"] != null)
                    {
                        body = body.Replace("{ToOption}", dr["fm_toptiondesc"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{ToOption}", "");
                    }
                }


            }
            catch (Exception ex)
            {
                WriteLog(ex.Message + " - Inner Exception : " + ex.InnerException, ex.StackTrace, "SWPapprovalbyFinalApproval", dr["Ihno"].ToString());
                body = string.Empty;
            }

            return body;
        }
        private static string SWPbyInitiator(StreamReader reader, DataRow dr)
        {
            string body = string.Empty;

            try
            {
                using (reader)
                {
                    body = reader.ReadToEnd();
                }
                if (body.Contains("{FirstName}"))
                {
                    if (dr["Name"] != null)
                    {
                        body = body.Replace("{FirstName}", dr["Name"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{FirstName}", "");
                    }
                }
                if (body.Contains("{folio}"))
                {
                    if (dr["dd_acno"] != null)
                    {
                        body = body.Replace("{folio}", dr["dd_acno"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{folio}", "");
                    }
                }
                if (body.Contains("{amount}"))
                {
                    if (dr["dd_amt"] != null)
                    {
                        body = body.Replace("{amount}", dr["dd_amt"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{amount}", "");
                    }
                }
                if (body.Contains("{Fopted}"))
                {
                    if (dr["FrequencyOpted"] != null)
                    {
                        body = body.Replace("{Fopted}", dr["FrequencyOpted"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{Fopted}", "");
                    }
                }
                if (body.Contains("{{swpenddate}}"))
                {
                    if (dr["enddate"] != null)
                    {
                        body = body.Replace("{{swpenddate}}", dr["enddate"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{{swpenddate}}", "");
                    }
                }

                if (body.Contains("{noofwithdrawls}"))
                {
                    if (dr["dd_trdate"] != null)
                    {
                        body = body.Replace("{noofwithdrawls}", dr["dd_trdate"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{noofwithdrawls}", "");
                    }
                }
                if (body.Contains("{swpcomemtdate}"))
                {
                    if (dr["commencementdate"] != null)
                    {
                        body = body.Replace("{swpcomemtdate}", dr["commencementdate"].ToString());
                    }
                    else
                    {

                        body = body.Replace("{swpcomemtdate}", "");
                    }
                }
                if (body.Contains("{distributor}"))
                {
                    if (dr["dd_distributor"] != null)
                    {
                        body = body.Replace("{distributor}", dr["dd_distributor"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{distributor}", "");
                    }
                }
                if (body.Contains("{ihno}"))
                {
                    if (dr["Ihno"] != null)
                    {
                        body = body.Replace("{ihno}", dr["Ihno"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{ihno}", "");
                    }
                }
                if (body.Contains("{Fromscheme}"))
                {
                    if (dr["dd_schemedesc"] != null)
                    {
                        body = body.Replace("{Fromscheme}", dr["dd_schemedesc"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{Fromscheme}", "");
                    }
                }


                if (body.Contains("{Toscheme}"))
                {
                    if (dr["fm_tschdesc"] != null)
                    {
                        body = body.Replace("{Toscheme}", dr["fm_tschdesc"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{Toscheme}", "");
                    }
                }

                if (body.Contains("{FromPlan}"))
                {
                    if (dr["dd_plandesc"] != null)
                    {
                        body = body.Replace("{FromPlan}", dr["dd_plandesc"].ToString());
                    }
                    else
                    {

                        body = body.Replace("{FromPlan}", "");
                    }
                }
                if (body.Contains("{ToPlan}"))
                {
                    if (dr["fm_tplandesc"] != null)
                    {
                        body = body.Replace("{ToPlan}", dr["fm_tplandesc"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{ToPlan}", "");
                    }
                }

                if (body.Contains("{FromOption}"))
                {
                    if (dr["dd_optiondesc"] != null)
                    {
                        body = body.Replace("{FromOption}", dr["dd_optiondesc"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{FromOption}", "");
                    }
                }
                if (body.Contains("{ToOption}"))
                {
                    if (dr["fm_toptiondesc"] != null)
                    {
                        body = body.Replace("{ToOption}", dr["fm_toptiondesc"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{ToOption}", "");
                    }
                }


                if (body.Contains("{ARN/EUIN}"))
                {
                    if (dr["ARN/EUIN"] != null)
                    {
                        body = body.Replace("{ARN/EUIN}", dr["ARN/EUIN"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{ARN/EUIN}", "");
                    }
                }
                if (body.Contains("{refno}"))
                {
                    if (dr["refno"] != null)
                    {
                        body = body.Replace("{refno}", dr["refno"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{refno}", "");
                    }
                }
                if (body.Contains("{Apprname}"))
                {
                    if (dr["Apprname"] != null)
                    {
                        body = body.Replace("{Apprname}", dr["Apprname"].ToString());
                    }
                    else
                    {

                        body = body.Replace("{Apprname}", "");
                    }
                }

            }
            catch (Exception ex)
            {
                WriteLog(ex.Message + " - Inner Exception : " + ex.InnerException, ex.StackTrace, "SWPbyInitiator", dr["Ihno"].ToString());
                body = string.Empty;
            }

            return body;
        }
        private static string SWPRejectedbyApprover(StreamReader reader, DataRow dr)
        {
            string body = string.Empty;

            try
            {
                using (reader)
                {
                    body = reader.ReadToEnd();
                }
                if (body.Contains("{FirstName}"))
                {
                    if (dr["Name"] != null)
                    {
                        body = body.Replace("{FirstName}", dr["Name"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{FirstName}", "");
                    }
                }
                if (body.Contains("{folio}"))
                {
                    if (dr["dd_acno"] != null)
                    {
                        body = body.Replace("{folio}", dr["dd_acno"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{folio}", "");
                    }
                }
                if (body.Contains("{amount}"))
                {
                    if (dr["dd_amt"] != null)
                    {
                        body = body.Replace("{amount}", dr["dd_amt"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{amount}", "");
                    }
                }
                if (body.Contains("{Fopted}"))
                {
                    if (dr["FrequencyOpted"] != null)
                    {
                        body = body.Replace("{Fopted}", dr["FrequencyOpted"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{Fopted}", "");
                    }
                }
                if (body.Contains("{{swpenddate}}"))
                {
                    if (dr["enddate"] != null)
                    {
                        body = body.Replace("{{swpenddate}}", dr["enddate"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{{swpenddate}}", "");
                    }
                }

                if (body.Contains("{noofwithdrawls}"))
                {
                    if (dr["dd_trdate"] != null)
                    {
                        body = body.Replace("{noofwithdrawls}", dr["dd_trdate"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{noofwithdrawls}", "");
                    }
                }
                if (body.Contains("{swpcomemtdate}"))
                {
                    if (dr["commencementdate"] != null)
                    {
                        body = body.Replace("{swpcomemtdate}", dr["commencementdate"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{swpcomemtdate}", "");
                    }
                }
                if (body.Contains("{distributor}"))
                {
                    if (dr["dd_distributor"] != null)
                    {
                        body = body.Replace("{distributor}", dr["dd_distributor"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{distributor}", "");
                    }
                }
                if (body.Contains("{ihno}"))
                {
                    if (dr["Ihno"] != null)
                    {
                        body = body.Replace("{ihno}", dr["Ihno"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{ihno}", "");
                    }
                }
                if (body.Contains("{Fromscheme}"))
                {
                    if (dr["dd_schemedesc"] != null)
                    {
                        body = body.Replace("{Fromscheme}", dr["dd_schemedesc"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{Fromscheme}", "");
                    }
                }


                if (body.Contains("{Toscheme}"))
                {
                    if (dr["fm_tschdesc"] != null)
                    {
                        body = body.Replace("{Toscheme}", dr["fm_tschdesc"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{Toscheme}", "");
                    }
                }

                if (body.Contains("{FromPlan}"))
                {
                    if (dr["dd_plandesc"] != null)
                    {
                        body = body.Replace("{FromPlan}", dr["dd_plandesc"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{FromPlan}", "");
                    }
                }
                if (body.Contains("{ToPlan}"))
                {
                    if (dr["fm_tplandesc"] != null)
                    {
                        body = body.Replace("{ToPlan}", dr["fm_tplandesc"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{ToPlan}", "");
                    }
                }

                if (body.Contains("{FromOption}"))
                {
                    if (dr["dd_optiondesc"] != null)
                    {
                        body = body.Replace("{FromOption}", dr["dd_optiondesc"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{FromOption}", "");
                    }
                }
                if (body.Contains("{ToOption}"))
                {
                    if (dr["fm_toptiondesc"] != null)
                    {
                        body = body.Replace("{ToOption}", dr["fm_toptiondesc"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{ToOption}", "");
                    }
                }



            }
            catch (Exception ex)
            {
                WriteLog(ex.Message + " - Inner Exception : " + ex.InnerException, ex.StackTrace, "SWPRejectedbyApprover", dr["Ihno"].ToString());
                body = string.Empty;
            }

            return body;
        }

        #region Distributor Email Templates
        private static string PurchasebyDistributor(StreamReader reader, DataRow dr)
        {
            string body = string.Empty;
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(dr["Emailid"].ToString());
            string en_email = System.Convert.ToBase64String(plainTextBytes);

            string Link = ConfigurationManager.AppSettings["linkURL"].ToString() + en_email + "&type=dit";

            //string Link = ConfigurationManager.AppSettings["linkURL"].ToString() + dr["Emailid"].ToString() + "&type=dit";
            try
            {
                using (reader)
                {
                    body = reader.ReadToEnd();
                }
                if (body.Contains("{Investor}"))
                {
                    if (dr["InvestorName"] != null)
                    {
                        body = body.Replace("{Investor}", dr["InvestorName"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{Investor}", "");
                    }
                }
                if (body.Contains("{PAN}"))
                {
                    if (dr["pan"] != null)
                    {
                        body = body.Replace("{PAN}", dr["pan"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{PAN}", "");

                    }
                }
                if (body.Contains("{FolioNo}"))
                {
                    if (dr["dd_acno"] != null)
                    {
                        body = body.Replace("{FolioNo}", dr["dd_acno"].ToString());
                    }
                    else
                    {

                        body = body.Replace("{FolioNo}", "");
                    }
                }
                if (body.Contains("{Scheme}"))
                {
                    if (dr["dd_schemedesc"] != null)
                    {
                        body = body.Replace("{Scheme}", dr["dd_schemedesc"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{Scheme}", "");
                    }
                }
                if (body.Contains("{plan}"))
                {
                    if (dr["dd_plandesc"] != null)
                    {
                        body = body.Replace("{plan}", dr["dd_plandesc"].ToString());
                    }
                    else
                    {

                        body = body.Replace("{plan}", "");
                    }
                }
                if (body.Contains("{option}"))
                {
                    if (dr["dd_optiondesc"] != null)
                    {
                        body = body.Replace("{option}", dr["dd_optiondesc"].ToString());
                    }
                    else
                    {

                        body = body.Replace("{option}", "");
                    }
                }
                if (body.Contains("{amount}"))
                {
                    if (dr["dd_amt"] != null)
                    {
                        body = body.Replace("{amount}", dr["dd_amt"].ToString());
                    }
                    else
                    {

                        body = body.Replace("{amount}", "");
                    }
                }
                if (body.Contains("{TransactionDate}"))
                {
                    if (dr["dd_trdate"] != null)
                    {
                        body = body.Replace("{TransactionDate}", dr["dd_trdate"].ToString());
                    }
                    else
                    {

                        body = body.Replace("{TransactionDate}", "");
                    }
                }
                if (body.Contains("{DistributorName}"))
                {
                    if (dr["Distributor_name"] != null)
                    {
                        body = body.Replace("{DistributorName}", dr["Distributor_name"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{DistributorName}", "");

                    }
                }
                if (body.Contains("{ReferenceNumber}"))
                {
                    if (dr["refno"] != null)
                    {
                        body = body.Replace("{ReferenceNumber}", dr["refno"].ToString());
                    }
                    else
                    {

                        body = body.Replace("{ReferenceNumber}", "");
                    }
                }
                if (body.Contains("{ARN/EUIN}"))
                {
                    if (dr["ARN/EUIN"] != null)
                    {
                        body = body.Replace("{ARN/EUIN}", dr["ARN/EUIN"].ToString());
                    }
                    else
                    {

                        body = body.Replace("{ARN/EUIN}", "");
                    }
                }
                if (body.Contains("{Link}"))
                {
                    body = body.Replace("{Link}", Link);
                }
                if (body.Contains("{Regular/Direct}"))
                {
                    if (dr["Direct/Regular"] != null)
                    {
                        body = body.Replace("{Regular/Direct}", dr["Direct/Regular"].ToString());
                    }
                    else
                    {

                        body = body.Replace("{Regular/Direct}", "");
                    }
                }
            }
            catch (Exception ex)
            {
                WriteLog(ex.Message + " - Inner Exception : " + ex.InnerException, ex.StackTrace, "PurchasebyDistributor", dr["Emailid"].ToString());
                body = string.Empty;
            }

            return body;
        }
        private static string RedemptionbyDistributor(StreamReader reader, DataRow dr)
        {
            string body = string.Empty;
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(dr["Emailid"].ToString());
            string en_email = System.Convert.ToBase64String(plainTextBytes);

            string Link = ConfigurationManager.AppSettings["linkURL"].ToString() + en_email + "&type=dit";

            //string Link = ConfigurationManager.AppSettings["linkURL"].ToString() + dr["Emailid"].ToString() + "&type=dit";
            try
            {
                using (reader)
                {
                    body = reader.ReadToEnd();
                }
                if (body.Contains("{Investor}"))
                {
                    if (dr["InvestorName"] != null)
                    {
                        body = body.Replace("{Investor}", dr["InvestorName"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{Investor}", "");
                    }
                }
                if (body.Contains("{PAN}"))
                {
                    if (dr["pan"] != null)
                    {
                        body = body.Replace("{PAN}", dr["pan"].ToString());
                    }
                    else
                    {

                        body = body.Replace("{PAN}", "");
                    }
                }
                if (body.Contains("{FolioNo}"))
                {
                    if (dr["dd_acno"] != null)
                    {
                        body = body.Replace("{FolioNo}", dr["dd_acno"].ToString());
                    }
                    else
                    {

                        body = body.Replace("{FolioNo}", "");
                    }
                }
                if (body.Contains("{Scheme}"))
                {
                    if (dr["dd_schemedesc"] != null)
                    {
                        body = body.Replace("{Scheme}", dr["dd_schemedesc"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{Scheme}", "");
                    }
                }
                if (body.Contains("{plan}"))
                {
                    if (dr["dd_plandesc"] != null)
                    {
                        body = body.Replace("{plan}", dr["dd_plandesc"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{plan}", "");
                    }
                }
                if (body.Contains("{option}"))
                {
                    if (dr["dd_optiondesc"] != null)
                    {
                        body = body.Replace("{option}", dr["dd_optiondesc"].ToString());
                    }
                    else
                    {

                        body = body.Replace("{option}", "");
                    }
                }
                if (body.Contains("{amount}"))
                {
                    if (dr["dd_amt"] != null)
                    {
                        body = body.Replace("{amount}", dr["dd_amt"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{amount}", "");
                    }
                }
                if (body.Contains("{TransactionDate}"))
                {
                    if (dr["dd_trdate"] != null)
                    {
                        body = body.Replace("{TransactionDate}", dr["dd_trdate"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{TransactionDate}", "");
                    }
                }
                if (body.Contains("{DistributorName}"))
                {
                    if (dr["Distributor_name"] != null)
                    {
                        body = body.Replace("{DistributorName}", dr["Distributor_name"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{DistributorName}", "");
                    }
                }
                if (body.Contains("{ReferenceNumber}"))
                {
                    if (dr["refno"] != null)
                    {
                        body = body.Replace("{ReferenceNumber}", dr["refno"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{ReferenceNumber}", "");
                    }
                }
                if (body.Contains("{ARN/EUIN}"))
                {
                    if (dr["ARN/EUIN"] != null)
                    {
                        body = body.Replace("{ARN/EUIN}", dr["ARN/EUIN"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{ARN/EUIN}", "");
                    }
                }
                if (body.Contains("{Link}"))
                {
                    body = body.Replace("{Link}", Link);
                }
                if (body.Contains("{Regular/Direct}"))
                {
                    if (dr["Direct/Regular"] != null)
                    {
                        body = body.Replace("{Regular/Direct}", dr["Direct/Regular"].ToString());
                    }
                    else
                    {

                        body = body.Replace("{Regular/Direct}", "");
                    }
                }
            }
            catch (Exception ex)
            {
                WriteLog(ex.Message + " - Inner Exception : " + ex.InnerException, ex.StackTrace, "RedemptionbyDistributor", dr["Emailid"].ToString());
                body = string.Empty;
            }

            return body;
        }
        private static string SwitchbyDistributor(StreamReader reader, DataRow dr)
        {
            string body = string.Empty;
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(dr["Emailid"].ToString());
            string en_email = System.Convert.ToBase64String(plainTextBytes);

            string Link = ConfigurationManager.AppSettings["linkURL"].ToString() + en_email + "&type=dit";

            //string Link = ConfigurationManager.AppSettings["linkURL"].ToString() + dr["Emailid"].ToString() + "&type=dit";
            try
            {
                using (reader)
                {
                    body = reader.ReadToEnd();
                }
                if (body.Contains("{FROMSCHEME}"))
                {
                    if (dr["dd_schemedesc"] != null)
                    {
                        body = body.Replace("{FROMSCHEME}", dr["dd_schemedesc"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{FROMSCHEME}", "");
                    }
                }
                if (body.Contains("{Toscheme}"))
                {
                    if (dr["ToScheme"] != null)
                    {
                        body = body.Replace("{Toscheme}", dr["ToScheme"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{Toscheme}", "");
                    }
                }
                if (body.Contains("{ARN/EUIN}"))
                {
                    if (dr["ARN/EUIN"] != null)
                    {
                        body = body.Replace("{ARN/EUIN}", dr["ARN/EUIN"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{ARN/EUIN}", "");
                    }
                }
                if (body.Contains("{Link}"))
                {
                    body = body.Replace("{Link}", Link);
                }
                if (body.Contains("{Investor}"))
                {
                    if (dr["InvestorName"] != null)
                    {
                        body = body.Replace("{Investor}", dr["InvestorName"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{Investor}", "");
                    }
                }
                if (body.Contains("{PAN}"))
                {
                    if (dr["pan"] != null)
                    {
                        body = body.Replace("{PAN}", dr["pan"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{PAN}", "");
                    }
                }
                if (body.Contains("{FolioNo}"))
                {
                    if (dr["dd_acno"] != null)
                    {
                        body = body.Replace("{FolioNo}", dr["dd_acno"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{FolioNo}", "");
                    }
                }
                if (body.Contains("{amount}"))
                {
                    if (dr["dd_amt"] != null)
                    {
                        body = body.Replace("{amount}", dr["dd_amt"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{amount}", "");
                    }
                }
                if (body.Contains("{TransactionDate}"))
                {
                    if (dr["dd_trdate"] != null)
                    {
                        body = body.Replace("{TransactionDate}", dr["dd_trdate"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{TransactionDate}", "");
                    }
                }
                if (body.Contains("{SwitchInDate}"))
                {
                    if (dr["commencementdate"] != null)
                    {
                        body = body.Replace("{SwitchInDate}", dr["commencementdate"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{SwitchInDate}", "");
                    }
                }
                if (body.Contains("{SwitchOutDate}"))
                {
                    if (dr["enddate"] != null)
                    {
                        body = body.Replace("{SwitchOutDate}", dr["enddate"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{SwitchOutDate}", "");
                    }
                }
                if (body.Contains("{DistributorName}"))
                {
                    if (dr["Distributor_name"] != null)
                    {
                        body = body.Replace("{DistributorName}", dr["Distributor_name"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{DistributorName}", "");
                    }
                }
                if (body.Contains("{ReferenceNumber}"))
                {
                    if (dr["refno"] != null)
                    {
                        body = body.Replace("{ReferenceNumber}", dr["refno"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{ReferenceNumber}", "");
                    }
                }
                if (body.Contains("{Regular/Direct}"))
                {
                    if (dr["Direct/Regular"] != null)
                    {
                        body = body.Replace("{Regular/Direct}", dr["Direct/Regular"].ToString());
                    }
                    else
                    {
                        body = body.Replace("{Regular/Direct}", "");
                    }
                }
            }
            catch (Exception ex)
            {
                WriteLog(ex.Message + " - Inner Exception : " + ex.InnerException, ex.StackTrace, "SwitchbyDistributor", dr["Emailid"].ToString());
                body = string.Empty;
            }

            return body;
        }
        #endregion

        public static DataSet GetdataforSentemail(string fund, string ihno)
        {
            SqlConnection con;
            SqlCommand sqlcmd;
            DataSet dsgetdata;
            SqlDataAdapter dagetdata;
            dsgetdata = new DataSet();
            sqlcmd = new SqlCommand();
            try
            {
                string strsql = Convert.ToString(ConfigurationManager.ConnectionStrings["KBOLT"].ConnectionString);
                con = new SqlConnection(strsql);
                dagetdata = new SqlDataAdapter("KTrack_emailsent", con);
                dagetdata.SelectCommand.CommandType = CommandType.StoredProcedure;
                dagetdata.SelectCommand.Parameters.Add("@fund", SqlDbType.VarChar, 150).Value = fund;
                dagetdata.SelectCommand.Parameters.Add("@ihno", SqlDbType.VarChar, 150).Value = ihno;
                dagetdata.SelectCommand.Parameters.Add("@customfund", SqlDbType.VarChar, 150).Value = fund;

                dagetdata.SelectCommand.CommandTimeout = 1000;
                dagetdata.Fill(dsgetdata);
            }
            catch (Exception ex)
            {
                WriteLog(ex.Message + " - Inner Exception : " + ex.InnerException, ex.StackTrace, "SendEmail", "error");
            }

            finally
            {
            }
            return dsgetdata;
        }

      
        private static bool SendEmail(string CorporateEmail, string Body, string Subject, string Bcc, string Cc, string XHeader)
        {
            bool EmailSentFlag = false;
            if (!string.IsNullOrEmpty(CorporateEmail))
            {
                string[] BccArry = null;
                string[] CcArry = null;
                //Cc = "madhushree.sudhomay@kfintech.com,nikitababurao.nikam@kfintech.com,madhushreec9@gmail.com";
                  //Bcc = "sindhu.popuri@kfintech.com";//"";
                  Bcc = "sindhu.popuri@kfintech.com,priyani.bhandari@kfintech.com";
                try
                {
                    if (!string.IsNullOrEmpty(Bcc))
                    {
                        BccArry = Bcc.Split(',');
                    }
                    if (!string.IsNullOrEmpty(Cc))
                    {
                        CcArry = Cc.Split(',');
                    }

                    var message = new MailMessage();
                    ArrayList list_emails = new ArrayList();
                    if (!string.IsNullOrEmpty(CorporateEmail))
                    {
                        foreach (string item in CorporateEmail.Split(','))
                        {
                            list_emails.Add(item);
                        }
                    }
                    //list_emails.Add("bhimashankar.kathare@kfintech.com");
                    foreach (var item in list_emails)
                    {
                        message.To.Add(new MailAddress(item.ToString()));
                    }
                   // message.Bcc.Add("raja.garikipati@kfintech.com");
                   // message.Bcc.Add("chaitanyasharma.adimurthy@kfintech.com");
                    //message.Bcc.Add("chandan.khatua@kfintech.com");
                    //message.Bcc.Add("anasuya.das@kfintech.com");
                    //message.Bcc.Add("muzafarali.khan@kfintech.com");
                    if (!string.IsNullOrEmpty(Bcc))
                    {

                        foreach (var item in BccArry)
                        {
                            message.Bcc.Add(new MailAddress(item.ToString()));

                        }
                    }
                    if (!string.IsNullOrEmpty(Cc))
                    {
                        foreach (var item in CcArry)
                        {
                            message.CC.Add(new MailAddress(item.ToString()));
                        }
                    }
                    // message.To.Add(new MailAddress("v-hcl.ibrahim@kfintech.com"));  // replace with valid value 
                    message.From = new MailAddress("service_licmf@kfintech.com", "LIC Mutual Fund");  // replace with valid value
                    // message.Subject = "Registration Confirmation email";
                    message.Subject = Subject;
                    message.Body = Body.ToString();// string.Format(Body.ToString(), "Registration Confirmation email", "korpconnect.care@kfintech.com", "Please click below link to continue with registration.");
                    message.IsBodyHtml = true;
                    message.Headers.Add("XHeader", XHeader);
                    using (var smtp = new SmtpClient())
                    {
                        var credential = new NetworkCredential
                        {
                            UserName = ConfigurationManager.AppSettings["smtpUserName"].ToString(), //"samfd@kfintech.com",  // replace with valid value
                            Password = ConfigurationManager.AppSettings["smtpPassword"].ToString() //"Karvy234$"  // replace with valid value
                        };
                    
                        smtp.Credentials = credential;
                        smtp.UseDefaultCredentials = true;
                        smtp.Host = ConfigurationManager.AppSettings["SMTPServer"].ToString(); //"192.168.14.23";
                        smtp.Port = int.Parse(ConfigurationManager.AppSettings["smtpserverport"].ToString());
                        smtp.EnableSsl = false;
                        smtp.Send(message);
                        EmailSentFlag = true;
                    }
                }
                catch (Exception ex)
                {
                    WriteLog(ex.Message + " - Inner Exception : " + ex.InnerException, ex.StackTrace, "SendEmail", CorporateEmail + "-" + XHeader);
                    DataTable dt = new DataTable();
                    var connection = ConfigurationManager.ConnectionStrings["conStr"].ConnectionString;
                    try
                    {
                        using (var connections = new SqlConnection(connection))
                        {
                            connections.Open();
                            SqlDataAdapter adp = new SqlDataAdapter("Set_Corporate_mails_exception", connections);
                            adp.SelectCommand.CommandType = CommandType.StoredProcedure;
                            adp.SelectCommand.Parameters.AddWithValue("@foliono", drExceptionRow["dd_acno"].ToString());
                            adp.SelectCommand.Parameters.AddWithValue("@CorporateRegID", drExceptionRow["CorporateRegID"].ToString());
                            adp.SelectCommand.Parameters.AddWithValue("@Name", drExceptionRow["Name"].ToString());
                            adp.SelectCommand.Parameters.AddWithValue("@MobileNumber", drExceptionRow["MobileNumber"].ToString());
                            adp.SelectCommand.Parameters.AddWithValue("@EmailId", drExceptionRow["EmailId"].ToString());
                            adp.SelectCommand.Parameters.AddWithValue("@ActorType", drExceptionRow["ActorType"].ToString());
                            adp.SelectCommand.Parameters.AddWithValue("@dd_trtype", drExceptionRow["dd_trtype"].ToString());
                            adp.SelectCommand.Parameters.AddWithValue("@dd_trdate", drExceptionRow["dd_trdate"].ToString());
                            adp.SelectCommand.Parameters.AddWithValue("@dd_fund", drExceptionRow["dd_fund"].ToString());
                            adp.SelectCommand.Parameters.AddWithValue("@dd_scheme", drExceptionRow["dd_scheme"].ToString());
                            adp.SelectCommand.Parameters.AddWithValue("@dd_plan", drExceptionRow["dd_plan"].ToString());
                            adp.SelectCommand.Parameters.AddWithValue("@dd_option", drExceptionRow["dd_option"].ToString());
                            adp.SelectCommand.Parameters.AddWithValue("@dd_amt", drExceptionRow["dd_amt"].ToString());
                            adp.SelectCommand.Parameters.AddWithValue("@units", drExceptionRow["units"].ToString());
                            adp.SelectCommand.Parameters.AddWithValue("@dd_distributor", drExceptionRow["dd_distributor"].ToString());
                            adp.SelectCommand.Parameters.AddWithValue("@fm_fundname", drExceptionRow["fm_fundname"].ToString());
                            adp.SelectCommand.Parameters.AddWithValue("@dd_schemedesc", drExceptionRow["dd_schemedesc"].ToString());
                            adp.SelectCommand.Parameters.AddWithValue("@dd_plandesc", drExceptionRow["dd_plandesc"].ToString());
                            adp.SelectCommand.Parameters.AddWithValue("@dd_optiondesc", drExceptionRow["dd_optiondesc"].ToString());
                            adp.SelectCommand.Parameters.AddWithValue("@Subject", drExceptionRow["Subject"].ToString());
                            adp.SelectCommand.Parameters.AddWithValue("@Ihno", drExceptionRow["Ihno"].ToString());
                            adp.SelectCommand.Parameters.AddWithValue("@totalNoofApprovers", drExceptionRow["totalNoofApprovers"].ToString());
                            adp.SelectCommand.Parameters.AddWithValue("@NoofApproversAprvd", drExceptionRow["NoofApproversAprvd"].ToString());
                            adp.SelectCommand.Parameters.AddWithValue("@Status", "Pending");
                            adp.SelectCommand.Parameters.AddWithValue("@NoofApproversRejected", drExceptionRow["NoofApproversRejected"].ToString());
                            adp.SelectCommand.Parameters.AddWithValue("@roworder", drExceptionRow["roworder"].ToString());
                            adp.SelectCommand.Parameters.AddWithValue("@Action", drExceptionRow["Action"].ToString());
                            adp.SelectCommand.Parameters.AddWithValue("@FrequencyOpted", drExceptionRow["FrequencyOpted"].ToString());
                            adp.SelectCommand.Parameters.AddWithValue("@commencementdate", drExceptionRow["commencementdate"].ToString());
                            adp.SelectCommand.Parameters.AddWithValue("@enddate", drExceptionRow["enddate"].ToString());
                            adp.SelectCommand.Parameters.AddWithValue("@ToScheme", drExceptionRow["ToScheme"].ToString());
                            adp.SelectCommand.Parameters.AddWithValue("@dd_bnkacno", drExceptionRow["dd_bnkacno"].ToString());
                            adp.SelectCommand.Parameters.AddWithValue("@dd_bnkacname", drExceptionRow["dd_bnkacname"].ToString());
                            adp.SelectCommand.Parameters.AddWithValue("@Comments", drExceptionRow["Comments"].ToString());
                            adp.SelectCommand.Parameters.AddWithValue("@ARN_EUIN", drExceptionRow["ARN/EUIN"].ToString());
                            adp.SelectCommand.Parameters.AddWithValue("@refno", drExceptionRow["refno"].ToString());
                            adp.SelectCommand.Parameters.AddWithValue("@Apprname", drExceptionRow["Apprname"].ToString());
                            adp.SelectCommand.Parameters.AddWithValue("@Error_number", drExceptionRow["Error_number"].ToString());
                            adp.SelectCommand.Parameters.AddWithValue("@Appemailid", drExceptionRow["Appemailid"].ToString());
                            adp.SelectCommand.Parameters.AddWithValue("@sendmail", "");
                            adp.SelectCommand.Parameters.AddWithValue("@dd_tplan", drExceptionRow["dd_tplan"].ToString());
                            adp.SelectCommand.Parameters.AddWithValue("@dd_toption", drExceptionRow["dd_toption"].ToString());
                            adp.SelectCommand.Parameters.AddWithValue("@fm_tschdesc", drExceptionRow["fm_tschdesc"].ToString());
                            adp.SelectCommand.Parameters.AddWithValue("@fm_tplandesc", drExceptionRow["fm_tplandesc"].ToString());
                            adp.SelectCommand.Parameters.AddWithValue("@fm_toptiondesc", drExceptionRow["fm_toptiondesc"].ToString());
                            adp.SelectCommand.Parameters.AddWithValue("@AllowSMS", drExceptionRow["AllowSMS"].ToString());
                            adp.SelectCommand.Parameters.AddWithValue("@Allowmail", drExceptionRow["Allowmail"].ToString());
                            adp.SelectCommand.Parameters.AddWithValue("@pan", drExceptionRow["pan"].ToString());
                            adp.SelectCommand.Parameters.AddWithValue("@InvestorName", drExceptionRow["InvestorName"].ToString());
                            adp.SelectCommand.Parameters.AddWithValue("@Distributor_id", drExceptionRow["Distributor_id"].ToString());
                            adp.SelectCommand.Parameters.AddWithValue("@Distributor_name", drExceptionRow["Distributor_name"].ToString());
                            adp.SelectCommand.Parameters.AddWithValue("@UserType", drExceptionRow["UserType"].ToString());
                            adp.SelectCommand.Parameters.AddWithValue("@Direct_Regular", drExceptionRow["Direct/Regular"].ToString());
                            adp.SelectCommand.Parameters.AddWithValue("@ISAdminalert", drExceptionRow["ISAdminalert"].ToString());
                            adp.SelectCommand.Parameters.AddWithValue("@Bankname", drExceptionRow["Bankname"].ToString());
                            adp.SelectCommand.Parameters.AddWithValue("@PaymentLink", drExceptionRow["PaymentLink"].ToString());
                            adp.SelectCommand.Parameters.AddWithValue("@BankFlag", drExceptionRow["BankFlag"].ToString());
                            adp.SelectCommand.Parameters.AddWithValue("@Corporate_Acno", drExceptionRow["Corporate_Acno"].ToString());
                            adp.SelectCommand.Parameters.AddWithValue("@Inputtedon", DateTime.Now);
                            adp.SelectCommand.Parameters.AddWithValue("@Exception", (ex.Message + " - Inner Exception : " + ex.InnerException + " Stack Trace: " + ex.StackTrace).ToString());
                            adp.SelectCommand.Parameters.AddWithValue("@XHeader", XHeader.ToString());


                            adp.Fill(dt);

                        }
                    }
                    catch (Exception ex1) { }
                    EmailSentFlag = false;

                }
            }
            return EmailSentFlag;
        }
   
        private static DataTable GetAdminAlertConfigEmailAndMobileNo(string Fund, decimal TransactionAmount, long Ihno, string TranAmountType)
        {
            var connection = ConfigurationManager.ConnectionStrings["conStr"].ConnectionString;
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            try
            {
                using (var connections = new SqlConnection(connection))
                {
                    var command = new SqlCommand("GetCorporateAdminAlertdetails_console", connections);
                    command.Parameters.AddWithValue("@fund", Fund);
                    command.Parameters.AddWithValue("@TransactionAmount", TransactionAmount);
                    command.Parameters.AddWithValue("@Ihno", Ihno);
                    command.Parameters.AddWithValue("@TranAmountType", TranAmountType);
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandTimeout = 600000;
                    connections.Open();
                    SqlDataAdapter da = new SqlDataAdapter(command);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                WriteLog(ex.Message, ex.StackTrace, "GetAdminAlertConfigEmailAndMobileNo", Ihno.ToString());
            }
            return dt;
        }



        #region "Distributor Confirmation mail Templates"
        public static void PrepareMail_Purchase_SendToDistributor()
        {
            try
            {
                DataSet dsinvdetails = new DataSet();
                dsinvdetails = getdistconfirmdtls();
                StringBuilder sbMailBody = new StringBuilder("");

                string refno = "", initiatedon = "", agentname = "", fund = "", dcr = "", arncode = "", euincode = "", invname = "", acno = "", trtype = "", scheme = "", amount = "", email = "", phone = "";
                string pan = "", Tscheme = "", sipstdt = "", sipenddt = "", URN = "", units = "", ihno = "", installmnts = "", SIPamount = "", agentemail = "", subtrtype = "", freqency = "",paymode="";

                if (dsinvdetails != null)
                {
                    if (dsinvdetails.Tables.Count > 1)
                    {
                        if (dsinvdetails.Tables[1].Rows.Count > 0)
                        {
                            foreach (DataRow distdr in dsinvdetails.Tables[1].Rows)
                            {
                                sbMailBody.Remove(0, sbMailBody.Length);
                                trtype = Convert.ToString(distdr["Transaction Type"]).Replace(" ", "").ToUpper();
                                dcr = Convert.ToString(distdr["dcr"]);
                                refno = Convert.ToString(distdr["Reference Number"]);
                                scheme = Convert.ToString(distdr["Schemedesc"]);
                                initiatedon = Convert.ToString(distdr["Initiated On"]);
                                arncode = Convert.ToString(distdr["ARN Code"]);
                                euincode = Convert.ToString(distdr["EUIN Code"]);
                                fund = Convert.ToString(distdr["Fund"]);
                                ihno = Convert.ToString(distdr["ihno"]);
                                
                                if (trtype == "SIN" || trtype == "NEWPURCHASE" || trtype == "SINI")
                                {
                                    string ACNO = Convert.ToString(distdr["Acno"]);
                                    if (ACNO != "0" || ACNO != "")
                                    {
                                        acno = Convert.ToString(distdr["Acno"]);

                                    }
                                    else
                                    {
                                        acno = "Yet To be Allotted";
                                    }
                                }

                                else
                                {
                                    acno = Convert.ToString(distdr["Acno"]);
                                }
                                pan = Convert.ToString(distdr["Pan"]);
                                subtrtype = Convert.ToString(distdr["Trtype"]).ToUpper();
                                freqency = Convert.ToString(distdr["frequency"]);
                                invname = Convert.ToString(distdr["Investor Name"]);
                                Tscheme = Convert.ToString(distdr["Tschemedesc"]);
                                SIPamount = Convert.ToString(distdr["sipamount"]);
                                sipstdt = Convert.ToString(distdr["Sipstartdate"]);
                                sipenddt = Convert.ToString(distdr["sipenddate"]);
                                URN = Convert.ToString(distdr["Urnno"]);
                                agentemail = Convert.ToString(distdr["AgentEmail"]).Replace(" ", "");
                                agentemail = agentemail.Replace(Environment.NewLine, ""); //add a line terminating ;
                                if (agentemail.Contains(";"))
                                    agentemail = agentemail.Replace(";", ",");
                                agentemail = agentemail.Replace("..", ".");


                                try
                                {
                                    WriteLog("URN Number:" + URN + " IHNo: " + ihno + " Transaction: " + trtype, "", "","");
                                }
                                catch (Exception ex) { }
                                if (agentemail.Trim() != "" && agentemail.Trim() != null)
                                {
                                    if (agentemail.Substring(agentemail.Length - 1, 1) == "," || agentemail.Substring(agentemail.Length - 1, 1) == ";")
                                        agentemail = agentemail.Remove(agentemail.Length - 1, 1);
                                }
                                else
                                    agentemail = Convert.ToString(ConfigurationManager.AppSettings["DistBcc"]);

                                email = Convert.ToString(distdr["email"]);
                                phone = Convert.ToString(distdr["Mobile"]);
                                agentname = Convert.ToString(distdr["Agent Name"]);
                                installmnts = Convert.ToString(distdr["no_of_insts"]);
                                if (Convert.ToString(distdr["Units"]) != "0.000")
                                {
                                    units = Convert.ToString(distdr["Units"]);
                                }
                                else
                                {
                                    units = "0";
                                }

                                if (Convert.ToString(distdr["Amount"]) != "0.00")
                                {
                                    amount = Convert.ToString(distdr["Amount"]);
                                }
                                else
                                {
                                    amount = "0";
                                }


                                if (trtype == "SIN")
                                    trtype = "New Purchase with SIP";
                                if (trtype == "SINI")
                                    trtype = "New folio - Sip Registration";
                                if (trtype == "SIPCANCELLATION" || trtype == "STPCANCELLATION" || trtype == "SWPCANCELLATION")
                                    trtype = trtype.Substring(0, 3) + " Cancellation";
                                if (trtype == "SIPPAUSE")
                                    trtype = "Pause SIP";
                               

                                string maindir = AppDomain.CurrentDomain.BaseDirectory.Replace("\\bin\\Debug\\", "");
                                var XHeader = fund + "_" + ihno + "_" + "Distributor";
                                string relativePath1 = maindir + ConfigurationManager.AppSettings["Distributor"];
                                StreamReader reader = new StreamReader(relativePath1);
                                string Subject1 = "Distributor Initiated " + trtype + " Transaction – Successfully Authorized - " + refno;
                               
                                string body = string.Empty;


                                using (reader)
                                {
                                    body = reader.ReadToEnd();

                                    if (body.Contains("AgentName"))
                                    {
                                        if (!string.IsNullOrEmpty(agentname))
                                        {
                                            body = body.Replace("{AgentName}", agentname);
                                        }
                                        else
                                        {
                                            body = body.Replace("{AgentName}", "Distributor");
                                        }
                                    }
                                    if (body.Contains("refno"))
                                    {
                                        if (!string.IsNullOrEmpty(refno))
                                        {
                                            body = body.Replace("{refno}", refno);
                                        }
                                        else
                                        {
                                            body = body.Replace("{refno}", "");
                                        }
                                    }
                                    if (body.Contains("initiatedon"))
                                    {
                                        if (!string.IsNullOrEmpty(initiatedon))
                                        {
                                            body = body.Replace("{initiatedon}", initiatedon);
                                        }
                                        else
                                        {
                                            body = body.Replace("{initiatedon}", "");
                                        }
                                    }
                                    if (body.Contains("agentname"))
                                    {
                                        if (!string.IsNullOrEmpty(agentname))
                                        {
                                            body = body.Replace("{agentname}", agentname);
                                        }
                                        else
                                        {
                                            body = body.Replace("{agentname}", "");
                                        }
                                    }
                                    if (body.Contains("arncode"))
                                    {
                                        if (!string.IsNullOrEmpty(arncode))
                                        {
                                            body = body.Replace("{arncode}", arncode);
                                        }
                                        else
                                        {
                                            body = body.Replace("{arncode}", "");
                                        }
                                    }

                                    if ((trtype != "SWP"))
                                    {
                                        if (body.Contains("euincode"))
                                        {
                                            if (!string.IsNullOrEmpty(euincode))
                                            {
                                                body = body.Replace("{euincode}", euincode);
                                            }
                                            else
                                            {
                                                body = body.Replace("<tr id='trEUIN'>", "<tr id='trEUIN' style='display:none'>");
                                            }
                                        }
                                    }
                                    else
                                    {
                                        body = body.Replace("<tr id='trEUIN'>", "<tr id='trEUIN' style='display:none'>");
                                    }


                                   
                                    if (body.Contains("invname"))
                                    {
                                        if (!string.IsNullOrEmpty(invname))
                                        {
                                            body = body.Replace("{invname}", invname);
                                        }
                                        else
                                        {
                                            body = body.Replace("{invname}", "");
                                        }
                                    }

                                    if (body.Contains("acno"))
                                    {
                                        if (!string.IsNullOrEmpty(acno))
                                        {
                                            body = body.Replace("{acno}", acno);
                                        }
                                        else
                                        {
                                            body = body.Replace("{acno}", "");
                                        }
                                    }

                                    if (body.Contains("trantype"))
                                    {
                                        switch (trtype)
                                        {
                                            case "REDEMPTION":
                                                
                                            case "SWITCH":
                                                body = body.Replace("{trantype}", trtype + " (" + subtrtype + ") ");
                                                break;
                                            case "SIN":
                                                body = body.Replace("{trantype}", "New Purchase with SIP");
                                                break;
                                            case "SINI":
                                                body = body.Replace("{trantype}", "New folio - Sip Registration");
                                                break;
                                            case "NEWPURCHASE":
                                                body = body.Replace("{trantype}", "New Purchase");
                                                break;
                                            case "SIPCANCELLATION":
                                            case "STPCANCELLATION":
                                            case "SWPCANCELLATION":
                                                body = body.Replace("{trantype}", trtype.Substring(0, 3) + " Cancellation");
                                                break;
                                            case "SIPPAUSE":
                                                body = body.Replace("{trantype}", "SIP Pause");
                                                break;
                                            default:
                                                body = body.Replace("{trantype}", trtype);
                                                break;
                                        }
                                    }


                                    if (body.Contains("scheme"))
                                    {
                                        if (!string.IsNullOrEmpty(scheme))
                                        {
                                            body = body.Replace("{scheme}", scheme);
                                        }
                                        else
                                        {
                                            body = body.Replace("{scheme}", "");
                                        }

                                        switch (trtype)
                                        {
                                            
                                            case "STP":
                                                 body = body.Replace("<td id='FrmScheme' class='auto-style4'>From Scheme/Plan/Option</td>", "<td id='FrmScheme' class='auto-style4'>Transfer From</td>");
                                                break;
                                            case "SWITCH":
                                                body = body.Replace("<td id='FrmScheme' class='auto-style4'>From Scheme/Plan/Option</td>", "<td id='FrmScheme' class='auto-style4'>Switch Out</td>");
                                                break;
                                            case "STPCANCELLATION":
                                            case "SWPCANCELLATION":
                                            
                                            case "REDEMPTION":
                                            case "SIN":
                                            case "SINI":
                                            case "NEWPURCHASE":
                                            case "SIPPAUSE":
                                            default:
                                                body = body.Replace("<td id='FrmScheme' class='auto-style4'>From Scheme/Plan/Option</td>", "<td id='FrmScheme' class='auto-style4'>Scheme/Plan/Option</td>");
                                                break;
                                        }
                                    }

                                    if (body.Contains("{Amount/Units}"))
                                    {
                                        switch (trtype)
                                        {
                                            case "SWITCH":
                                            case "REDEMPTION":
                                            case "STP":
                                            case "SWP":
                                            case "STPCANCELLATION":
                                            case "SWPCANCELLATION":
                                                body = body.Replace("{Amount/Units}", "Amount/Units");
                                                body = body.Replace("{amount}", "Rs." + String.Format("{0:#,0.00}", amount) + "/" + String.Format("{0:#,0:000}", units) + " Units");
                                                break;
                                            case "SIN":
                                                body = body.Replace("{Amount/Units}", "Lumpsum Amount");
                                                body = body.Replace("{amount}", "Rs." + String.Format("{0:#,0.00}", amount) + " /-");
                                                break;
                                            case "SINI":
                                                body = body.Replace("{Amount/Units}", "Lumpsum Amount");
                                                body = body.Replace("{amount}", "NA");
                                                break;
                                            default:
                                                if (trtype != "SIPCANCELLATION" || trtype != "SIPPAUSE")
                                                {
                                                    body = body.Replace("{Amount/Units}", "Amount");
                                                    body = body.Replace("{amount}", "Rs." + String.Format("{0:#,0.00}", amount) + " /-");
                                                }
                                                else
                                                {
                                                    body = body.Replace("<tr id='amountunits'>", "<tr id='amountunits' style='display:none;'>");
                                                }
                                                break;
                                        }
                                    }

                                    if (body.Contains("{SIPAmount}"))
                                    {
                                        switch (trtype)
                                        {
                                            case "SIN":
                                            case "SINI":
                                            case "SIP":
                                            case "SWP":
                                            case "SIPCANCELLATION":
                                            case "SIPPAUSE":
                                                body = body.Replace("{SIPAmount}", "Rs." + String.Format("{0:#,0.00}", SIPamount) + " /-");
                                                break;
                                            default:
                                                body = body.Replace("<tr id='SIPAmount'>", "<tr id='SIPAmount' style='display:none;'>");
                                                break;
                                        }
                                    }

                                    if (body.Contains("Tscheme"))
                                    {
                                        if (!string.IsNullOrEmpty(Tscheme))
                                        {
                                            body = body.Replace("{Tscheme}", Tscheme);
                                        }
                                                                     
                                        switch (trtype)
                                        {
                                            
                                            case "SWITCH":
                                                body = body.Replace("<td class='auto-style4'>To Scheme/Plan/Option</td>", "<td class='auto-style4'>Switch In</td>");
                                              break;
   
                                            case "STP":
                                               body = body.Replace("<td class='auto-style4'>To Scheme/Plan/Option</td>", "<td class='auto-style4'>Transfer To</td>");
                                                 break;
                                        
                                            case "REDEMPTION":
                                            case "SIPPAUSE":
                                            case "SIN":
                                            case "SINI":
                                            case "SIPCANCELLATION":
                                            case "NEWPURCHASE":
                                                 body = body.Replace("<tr id='Toscheme'>", "<tr id='Toscheme' style='display:none;'>");
                                                 break;
                                             default:
                                             body = body.Replace("<tr id='Toscheme'>","<tr id='Toscheme' style='display:none;'>");
        
                                                break;
                                        }
                                    }


                                    if (body.Contains("{sipstdt}"))
                                    {
                                        switch (trtype)
                                        {
                                            case "SIN":
                                            case "SINI":
                                                body = body.Replace("{sipstdt}", sipstdt);
                                                body = body.Replace("{sipenddt}", sipenddt);
                                                body = body.Replace("{SIP Start Date}", "SIP Start Date");
                                                body = body.Replace("{SIP End Date}", "SIP End Date");
                                                break;
                                            case "SIPPAUSE":
                                                body = body.Replace("{sipstdt}", sipstdt);
                                                body = body.Replace("{sipenddt}", sipenddt);
                                                body = body.Replace("{SIP Start Date}", "Pause SIP Start Date");
                                                body = body.Replace("{SIP End Date}", "Pause SIP End Date");
                                                break;
                                            default:
                                                body = body.Replace(" <tr id='StartDt'>", "<tr id='StartDt' style='display:none;'>");
                                                body = body.Replace(" <tr id='EndDt'>", "<tr id='EndDt' style='display:none;'>");
                                                break;
                                        }
                                    }


                                    if (body.Contains("{freqency}"))
                                    {
                                        switch (trtype)
                                        {
                                            case "SIN":
                                            case "SINI":
                                            case "SIP":
                                            case "SIPCANCELLATION":
                                            case "SIPPAUSE":
                                            case "STP":
                                            case "STPCANCELLATION":
                                                body = body.Replace("{freqency}", freqency);
                                                break;
                                             default:
                                                body = body.Replace(" <tr id='freqency'>", "<tr id='freqency' style='display:none;'>");
                                                break;
                                        }
                                    }

                                    if (body.Contains("{URN}"))
                                    {
                                        switch (trtype)
                                        {
                                            case "SIN":
                                            case "SINI":
                                            case "SIP":
                                                
                                                body = body.Replace("{URN}", URN);
                                                break;
                                            default:
                                              
                                                body = body.Replace("<tr id='URN'>", "<tr id='URN' style='display:none;'>");
                                                break;
                                        }
                                    }

                                    if (body.Contains("{installmnts}"))
                                    {
                                        if (!string.IsNullOrEmpty(installmnts))
                                        {
                                            body = body.Replace("{installmnts}", installmnts);
                                        }
                                        else 
                                        {
                                            body = body.Replace("<tr id='instno'>", "<tr id='instno' style='display:none;'>");
                                        }
                                    }

                                    if (body.Contains("{email}"))
                                    {
                                        if (!string.IsNullOrEmpty(email))
                                        {
                                            body = body.Replace("{email}", email);
                                        }
                                        else
                                        {
                                            body = body.Replace("{email}", "");
                                        }
                                    }

                                    //{fromEmailid_distributor}
                                    if (body.Contains("{fromEmailid_distributor}"))
                                    {
                                        body = body.Replace("{fromEmailid_distributor}", "");
                                        
                                    }
                                }

                               bool SentFlag1 = SendEmail(agentemail, body, Subject1, "", "", XHeader);
                               if (SentFlag1) 
                               {
                                   dist_emaillog(trtype, ihno, fund, initiatedon, arncode);
                               }

                                #region old code
                                //sbMailBody.Append("<html><body><table width='85%' ><tr><td><table style='width:100%'><tr>");
                                //if (dcr.ToUpper() == "KTRKD" || dcr.ToUpper() == "KTRWD")
                                //{
                                //    if (agentname.Trim() != "" && agentname.Trim() != null)
                                //    {
                                //        sbMailBody.Append("<td> Dear <b> " + agentname + " ,</b> </td></tr>");
                                //    }
                                //    else
                                //    {
                                //        sbMailBody.Append("<td> Dear  Distributor </td></tr>");
                                //    }
                                //}
                                //sbMailBody.Append("<tr><td style='margin-top:10px'><span> Thank you for Initiating the transaction, Investor approved the transaction initiated by you</span>.<br/><span>Please find the transaction summary below.</span> </td></tr>");
                                //sbMailBody.Append("<tr><td>&nbsp;</td></tr>");
                                // sbMailBody.Append("<tr><td><table border='2'><tr><td>Transaction Reference Number</td><td> " + refno + "</td></tr>");
                                //sbMailBody.Append("<tr><td> Transaction Initiated On</td><td> " + initiatedon + "</td></tr>");
                                //sbMailBody.Append("<tr><td>Distributor Name</td><td> " + agentname + "</td></tr>");
                                //sbMailBody.Append("<tr><td>ARN Code</td><td> " + arncode + "</td></tr>");
                                //if ((trtype != "SWP"))
                                //{
                                //    if (euincode != "" && euincode != null)
                                //    {
                                //        sbMailBody.Append("<tr><td>EUIN Code</td><td> " + euincode + "</td></tr>");
                                //    }
                                //}

                                //sbMailBody.Append("<tr><td>Investor Name</td><td> " + invname + "</td></tr>");

                                //sbMailBody.Append("<tr><td>Folio Number</td><td> " + acno + "</td></tr>");

                                //if ((trtype == "REDEMPTION" | trtype == "SWITCH"))
                                //    sbMailBody.Append("<tr><td>Transaction Type</td><td> " + trtype + " (" + subtrtype + ") " + " </td></tr>");

                                //else if (trtype == "SIN")
                                //    sbMailBody.Append("<tr><td>Transaction Type</td><td>New Purchase with SIP </td></tr>");
                                //else if (trtype == "SINI")
                                //    sbMailBody.Append("<tr><td>Transaction Type</td><td>New folio - Sip Registration </td></tr>");
                                //else if (trtype == "NEWPURCHASE")
                                //    sbMailBody.Append("<tr><td>Transaction Type</td><td>New Purchase </td></tr>");

                                //else if (trtype == "SIPCANCELLATION" || trtype == "STPCANCELLATION" || trtype == "SWPCANCELLATION")
                                //    sbMailBody.Append("<tr><td>Transaction Type</td><td>" + trtype.Substring(0, 3) + " Cancellation</td></tr>");

                                //else if (trtype == "SIPPAUSE")
                                //    sbMailBody.Append("<tr><td>Transaction Type</td><td>Pause SIP</td></tr>");
                                //else
                                //    sbMailBody.Append("<tr><td>Transaction Type</td><td> " + trtype + " </td></tr>");
                                //if ((trtype == "SWITCH" || trtype == "STP" || trtype == "STPCANCELLATION"))
                                //    sbMailBody.Append("<tr><td>From Scheme/Plan/Option</td><td> " + scheme + " </td></tr>");
                                //else
                                //    sbMailBody.Append("<tr><td>Scheme/Plan/Option</td><td> " + scheme + "</td></tr>");

                                //if (trtype == "SWITCH" | trtype == "REDEMPTION" | trtype == "STP" | trtype == "SWP" || trtype == "STPCANCELLATION" || trtype == "SWPCANCELLATION")
                                //    sbMailBody.Append("<tr><td>Amount/Units</td><td> " + " Rs." + String.Format("{0:#,0.00}", amount) + "/" + String.Format("{0:#,0:000}", units) + " Units</td></tr>");
                                //else if (trtype == "SIN")
                                //    sbMailBody.Append("<tr><td>Lumpsum Amount</td><td> " + " Rs." + String.Format("{0:#,0.00}", amount) + " /- </td></tr>");
                                //else if (trtype == "SINI")
                                //    sbMailBody.Append("<tr><td>Lumpsum Amount</td><td>NA</td></tr>");
                                //else if (trtype != "SIPCANCELLATION" || trtype != "SIPPAUSE")
                                //    sbMailBody.Append("<tr><td>Amount</td><td> " + " Rs." + String.Format("{0:#,0.00}", amount) + " /- </td></tr>");
                                //if (trtype == "SIN" || trtype == "SINI" || trtype == "SIP" || trtype == "SIPCANCELLATION" || trtype == "SIPPAUSE")
                                //    sbMailBody.Append("<tr><td>SIP Amount</td><td> " + " Rs." + String.Format("{0:#,0.00}", SIPamount) + " /-  </td></tr>");



                                //if (trtype == "SWITCH" || trtype == "STP" || trtype == "STPCANCELLATION")
                                //    sbMailBody.Append("<tr><td>To Scheme/Plan/Option</td><td> " + Tscheme + " </td></tr>");



                                //if (trtype == "SIP" || trtype == "SIN" || trtype == "SINI" || trtype == "SIPCANCELLATION" || trtype == "SIPPAUSE" || trtype == "STP" || trtype == "STPCANCELLATION")
                                //{
                              
                                //    if (trtype == "SIN" || trtype == "SINI")
                                //    {
                                //        sbMailBody.Append("<tr><td>SIP Start Date</td><td> " + sipstdt + " </td></tr>");
                                //        sbMailBody.Append("<tr><td>SIP End Date</td><td> " + sipenddt + " </td></tr>");

                                //    }
                                //    else if (trtype == "SIPPAUSE")
                                //    {

                                //        sbMailBody.Append("<tr><td>Pause SIP Start Date</td><td> " + sipstdt + " </td></tr>");
                                //        sbMailBody.Append("<tr><td>Pause SIP End Date</td><td> " + sipenddt + " </td></tr>");
                                //    }
                                  
                                //    sbMailBody.Append("<tr><td> Frequency</td><td> " + freqency + " </td></tr>");
                                //}


                                //if (trtype == "SIP" || trtype == "SIN" || trtype == "SINI")
                                //{
                                //    sbMailBody.Append("<tr><td>URN Number</td><td> " + URN + " </td></tr>");
                                //}
                                //if (installmnts != "" && installmnts != null)
                                //{
                                //    sbMailBody.Append("<tr><td>No Of Installments</td><td> " + installmnts + " </td></tr>");
                                //}

                                //sbMailBody.Append("<tr><td>Investor Contact Details</td><td> " + email + "</td></tr>");

                                //sbMailBody.Append("</table></td></tr><tr><td><br><br><br> <span style='font-family:trebuchet ms;font-size: 12px'>You are requested to save this e-mail for future reference. If you have any further queries, please mail us at " + fromEmailid_distributor + " for further assistance. </span></td></tr></table></td></tr>");


                                //sbMailBody.Append("<tr><td><br><br>Regards,</td></tr>");

                                //sbMailBody.Append("<tr><td><br>KFinKart Distributor Team  </td></tr>");

                                //sbMailBody.Append("<tr><td><br>Do Encourage us, by rating us on <a href='https://play.google.com/store/apps/details?id=com.karvydistributor' target='_blank'>Google Play Store</a>  </td></tr>");

                                //sbMailBody.Append("</table></body></html>");


                                //sendmail(fund, acno, ihno, agentemail, subject, Convert.ToString(sbMailBody), dcr, trtype, initiatedon, "", arncode, "Y");
                                #endregion
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                WriteLog(ex.Message, "PrepareMail_Purchase_SendToDistributor()", ex.StackTrace,"");

            }
        }
        #endregion

        public static DataSet getdistconfirmdtls()
        {
            SqlConnection con;
            SqlCommand sqlcmd;
            DataSet dsgetdata;
            SqlDataAdapter dagetdata;
            dsgetdata = new DataSet();
            sqlcmd = new SqlCommand();
            try
            {
                //nikita 04-12-23
                string strsql = ConfigurationManager.ConnectionStrings["KBOLT"].ConnectionString;
                con = new SqlConnection(strsql);
                //dagetdata = new SqlDataAdapter("Ktrack_Distributor_Transactions", con);
                dagetdata = new SqlDataAdapter("Ktrack_Distributor_Transactions_LIC_Dit", con);
                dagetdata.SelectCommand.CommandType = CommandType.StoredProcedure;
                dagetdata.SelectCommand.CommandTimeout = 1000;
                dagetdata.Fill(dsgetdata);
            }
            catch (Exception ex)
            {
                WriteLog(ex.Message, " getdistconfirmdtls()", ex.StackTrace,"");
            }
            return dsgetdata;
        }

        static void Main(string[] args)
        {
            //System.Threading.ThreadStart ts = new System.Threading.ThreadStart(emailtemplate); //GetTransactiondata
            //System.Threading.Thread th = new System.Threading.Thread(ts);
            //th.Start();
            Parallel.Invoke(emailtemplate,PrepareMail_Purchase_SendToDistributor);//
        }

        #region  GetWriteLog
        public static void WriteLog(string ErrDesc, string ErrSrc, string ErrInfo, string CorporateEmail)
        {
            string strFileName = "Error" + DateTime.Now.ToString("MMMyyyy") + ".log";
            string strDirName = System.AppDomain.CurrentDomain.BaseDirectory + "\\LogFile";
            string path = strDirName + "\\" + strFileName;
            if (!Directory.Exists(strDirName))
            {
                Directory.CreateDirectory(strDirName);
            }
            if (!File.Exists(path))
            {
                File.Create(path).Dispose();
            }
            StreamWriter errFile = File.AppendText(path);

            StringBuilder sbError = new StringBuilder("");

            sbError.Append("Form Name:" + "	" + ErrSrc + Environment.NewLine);

            sbError.Append("Err Date:" + "	" + DateTime.Now + Environment.NewLine);

            sbError.Append("Err Desc:" + "	" + ErrDesc + Environment.NewLine);
            sbError.Append("User Det:" + "	" + CorporateEmail + Environment.NewLine);

            sbError.Append("User Info:" + "	" + ErrSrc + "	" + "" + "	" + "" + Environment.NewLine);

            sbError.Append("*********************************************************************" + "	" + Environment.NewLine + Environment.NewLine);
            errFile.WriteLine(sbError.ToString());

            if ((errFile != null))
                errFile.Close();
        }
        #endregion

    }
}


