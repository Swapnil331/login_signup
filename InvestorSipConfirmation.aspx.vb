Imports System
Imports System.Data
Imports System.Text
Imports System.IO
Imports KBDBDataTier
Imports System.Net
Imports System.Xml
Imports System.Security.Cryptography
Imports System.Net.Mail
Partial Class OnlinePurchase_InvestorSipConfirmation
    Inherits System.Web.UI.Page

#Region "Objects Declaration"
    Dim booError As Boolean = False
    Dim objdbRequest As New KBDBRequest
    Dim objdbFactory As KBAbstractFactory
    Dim objdsInfo, dsInfo As New DataSet
    Public TransactionType As String
    Dim PostUrl, PaymentType, ReturnUrl, FormatedNAVDt, strPDF, Category, MailID, cfund, strfund As String
    Dim TrsSavedDate As Date
    Dim Cheksumkey As String
    Dim additional_info7 As String
    Dim objpayconfirm As New MFIPaymentConfirmation()
    Dim objChecksum As New SHASample
    Dim ChecksumValue As String
    Dim dbRequest As New KBDBRequest
    Dim dSub As New KBSQLFactory
    Dim param As KBDBRequest.Parameter
    Dim dsSub As New DataSet
    Dim params As New ArrayList
    Dim add_info7 As String
    Dim kboltconstr As String = Convert.ToString(ConfigurationManager.AppSettings("KBOLT"))
    Dim mfdwebconstr As String = Convert.ToString(ConfigurationManager.AppSettings("MFDWEB"))
    Dim strsql_mfs As String = ConfigurationManager.AppSettings("KARVYMFS").ToString()
#End Region
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                Dim ds As New DataSet()
                If (Convert.ToString(Session("qparam")) <> Nothing And Convert.ToString(Session("dsconfdata")) <> Nothing) Then
                    ds = Session("qparam")
                    Dim dsDtrWeb As New DataSet()
                    Dim dsurl As New DataSet()
                    dsDtrWeb = Session("dsconfdata")
                    'dsurl = Session("dsbankurl")
                    If ds.Tables(0).Columns.Contains("schdesc") And ds.Tables(0).Columns.Contains("plndesc") And ds.Tables(0).Columns.Contains("optdesc") Then
                        lblScheme.Text = ds.Tables(0).Rows(0)("schdesc") + " - " + ds.Tables(0).Rows(0)("plndesc") + " - " + ds.Tables(0).Rows(0)("optdesc")
                    End If
                    If dsDtrWeb.Tables(0).Columns.Contains("dd_urnno") Then
                        lblurnno.Text = dsDtrWeb.Tables(0).Rows(0)("dd_urnno")
                    End If
                    If ds.Tables(0).Columns.Contains("schdesc") And ds.Tables(0).Columns.Contains("plndesc") And ds.Tables(0).Columns.Contains("optdesc") Then
                        Label1.Text = ds.Tables(0).Rows(0)("schdesc") + " - " + ds.Tables(0).Rows(0)("plndesc") + " - " + ds.Tables(0).Rows(0)("optdesc")
                    End If
                    'If dsurl.Tables(0).Columns.Contains("Bank_URL") Then
                    '    hdnurl.Value = dsurl.Tables(0).Rows(0)("Bank_URL")
                    'End If
                    imglogo.Attributes.Add("src", "https://mfs.kfintech.com/mfs/redesign/mfs-assets/images/names/lic.png")
                    'If ds.Tables(0).Columns.Contains("customfund") Then
                    '    If Not ds.Tables(0).Rows(0)("customfund") Is Nothing And Convert.ToString(ds.Tables(0).Rows(0)("customfund")) <> "" Then
                    '        If Convert.ToString(ds.Tables(0).Rows(0)("customfund")).Trim() = "117" Then
                    '            imglogo.Attributes.Add("src", "../ktrackimages/" + Convert.ToString(ds.Tables(0).Rows(0)("customfund")).Trim() + ".png")
                    '            'venkatesh added
                    '            'btnConfirmpay.Attributes.Add("style", "background-color:#f58220;border-color:#f58220;font-size: 1em; padding: 7px 23px")
                    '            'thrutag.InnerText = "Mirae DIT Portal."
                    '            Dim NfoFlag As String
                    '            NfoFlag = ""
                    '            If ds.Tables(0).Columns.Contains("etf_flag") Then
                    '                If Not ds.Tables(0).Rows(0)("etf_flag") Is DBNull.Value Then
                    '                    NfoFlag = Convert.ToString(ds.Tables(0).Rows(0)("etf_flag"))
                    '                    If NfoFlag.ToUpper() = "Y" Then
                    '                        'divnfo.Attributes.Add("style", "display:none")
                    '                    End If
                    '                End If
                    '            End If
                    '        End If
                    '    Else

                    '        imglogo.Attributes.Add("src", "../css/images/KFintechlogo.png")

                    '    End If
                    'End If

                    'If dsDtrWeb.Tables(0).Columns.Contains("dd_trdate") Then
                    '    'lblNavDate.Text = Format(dsDtrWeb.Tables(0).Rows(0)("dd_trdate"), "dd/MM/yyyy")
                    'End If
                    'If dsDtrWeb.Tables(0).Columns.Contains("dd_dcr") Then
                    '    If Convert.ToString(dsDtrWeb.Tables(0).Rows(0)("dd_dcr")).ToUpper() = "KMFS" Then
                    '        lblapptype.Text = "KFintech Investor Website"
                    '    ElseIf Convert.ToString(dsDtrWeb.Tables(0).Rows(0)("dd_dcr")).ToUpper() = "KTRKD" Then
                    '        lblapptype.Text = "KFinKart Distributor Mobile Application"
                    '    ElseIf Convert.ToString(dsDtrWeb.Tables(0).Rows(0)("dd_dcr")).ToUpper() = "KTRWD" Then
                    '        lblapptype.Text = "KFintech DIT WebSite"
                    '    ElseIf Convert.ToString(dsDtrWeb.Tables(0).Rows(0)("dd_dcr")).ToUpper() = "KG99" Then
                    '        lblapptype.Text = "Kbolt Go mobile Application"
                    '    ElseIf hdncustfund.Value <> "" Then
                    '        lblapptype.Text = getfunddesc(hdncustfund.Value) + " DIT WebSite"
                    '    End If
                    'End If
                    'If dsDtrWeb.Tables(0).Columns.Contains("dd_appno") <> "" Then
                    lblInetRefNo.Text = Convert.ToString(dsDtrWeb.Tables(0).Rows(0)("dd_appno")).Replace(" ", "")
                    Label3.Text = Convert.ToString(dsDtrWeb.Tables(0).Rows(0)("dd_appno")).Replace(" ", "")
                    hdnappno.Value = Convert.ToString(dsDtrWeb.Tables(0).Rows(0)("dd_appno")).Replace(" ", "")
                    'Else
                    '    lblInetRefNo.Text = Convert.ToString(ds.Tables(0).Rows(0)("dd_appno")).Replace(" ", "")
                    '    hdnappno.Value = Convert.ToString(ds.Tables(0).Rows(0)("dd_appno")).Replace(" ", "")
                    'End If
                    'lblInetRefNo.Text = Convert.ToString(dsDtrWeb.Tables(0).Rows(0)("dd_appno")).Replace(" ", "")

                    hdnTrtype.Value = Convert.ToString(ds.Tables(0).Rows(0)("trtype")).Replace(" ", "")
                    'If (hdnTrtype.Value.ToString = "R") Then
                    '    '    divtrtype.InnerText = "Redemption"
                    'ElseIf (hdnTrtype.Value.ToString = "S") Then
                    '    '   divtrtype.InnerText = "Switch"
                    'ElseIf (hdnTrtype.Value.ToString = "P") Then
                    '    '  divtrtype.InnerText = "Purchase" 
                    'End If
                    hdnId.Value = ""
                    hdnId.Value = Convert.ToString(ds.Tables(0).Rows(0)("ID"))
                    hdnihno.Value = Convert.ToString(ds.Tables(0).Rows(0)("dd_ihno"))
                    'If (ds.Tables(0).Columns.Contains("EuinValid")) Then
                    '    If (Convert.ToString(ds.Tables(0).Rows(0)("EuinValid")) = "N") Then
                    '        '     diveuindec.InnerText = "No"
                    '    Else
                    '        '    diveuindec.InnerText = "Yes"
                    '    End If
                    'End If
                    'If (hdnTrtype.Value.ToUpper() = "P") Then
                    '    Dim qparam As String = hdnQparam.Value
                    '    'btnConfirmpay.Attributes.Add("OnClick", "javascript:sebi();")
                    '    Dim qparam1 As String() = hdnQparam.Value.Split("|")

                    '    Dim fund = Convert.ToString(ds.Tables(0).Rows(0)("i_Fund"))
                    '    'venkatesh added
                    '    If ds.Tables(0).Columns.Contains("customfund") Then
                    '        If Convert.ToString(ds.Tables(0).Rows(0)("customfund")).Trim() = "117" Then
                    '            'btnConfirmpay.Attributes.Add("style", "background-color:#f58220;border-color:#f58220;font-size: 1em; padding: 7px 23px")
                    '        End If
                    '        hdncustfund.Value = Convert.ToString(ds.Tables(0).Rows(0)("customfund"))
                    '    End If


                    '    Session("fundcode") = fund
                    '    Dim panno = Convert.ToString(ds.Tables(0).Rows(0)("PanNo"))
                    '    'Dim folio = qparam1(3).ToString()
                    '    ' Dim folio = "9021186901"
                    '    Dim folio = Convert.ToString(ds.Tables(0).Rows(0)("i_Acno"))
                    '    Session("acno") = folio
                    '    '        Dim arncode = "000000-0"
                    '    Dim arncode = Convert.ToString(ds.Tables(0).Rows(0)("ARNCode"))
                    '    Dim subarn = Convert.ToString(ds.Tables(0).Rows(0)("ARNCode"))
                    '    Dim subbroker = Convert.ToString(ds.Tables(0).Rows(0)("Subbroker"))
                    '    Dim euin = Convert.ToString(ds.Tables(0).Rows(0)("EuinCode"))
                    '    Dim euindeclaration = Convert.ToString(ds.Tables(0).Rows(0)("EuinValid"))
                    '    'Dim amount = qparam1(9).ToString()
                    '    'Dim entby = qparam1(10).ToString()
                    '    ' Dim amount = "200"
                    '    Dim amount As String
                    '    If Session("NFOamt") <> Nothing And Session("NFOamt") <> "" Then
                    '        amount = Session("NFOamt")
                    '    ElseIf Convert.ToString(ds.Tables(0).Rows(0)("i_UntAmtValue")) <> "" And Convert.ToString(ds.Tables(0).Rows(0)("i_UntAmtValue")) <> "0" And Convert.ToString(ds.Tables(0).Rows(0)("i_UntAmtValue")) <> Nothing Then
                    '        amount = Convert.ToString(ds.Tables(0).Rows(0)("i_UntAmtValue"))

                    '    End If
                    '    Dim entby As String = Convert.ToString(ds.Tables(0).Rows(0)("EuinValid"))
                    '    Try

                    '        'btnConfirmpay.Attributes.Add("OnClick", "javascript:sebi();")
                    '        TrsSavedDate = CType(Now.Date, Date)
                    '        If Not Session("chktype") Is Nothing And Convert.ToString(Session("chktype")) <> "" Then
                    '            pgiaction.Value = Session("chktype")
                    '        Else
                    '            pgiaction.Value = Convert.ToString(ds.Tables(0).Rows(0)("Paymenttype")).Replace(" ", "")
                    '        End If


                    '        '------------------------------ Confirmation display------------------
                    '        cfund = Session("fundcode")

                    '        PaymentType = ds.Tables(0).Rows(0)("Paymenttype").ToString()
                    '        TransactionType = ds.Tables(0).Rows(0)("trtype").ToString()
                    '        Dim dsinfo As New DataSet
                    '        Dim Bankingtype As String = Convert.ToString(ds.Tables(0).Rows(0)("Paymenttype")).Replace(" ", "") 'net banking type creditcard/netbanking 
                    '        If Session("chktype") <> Nothing Then
                    '            'If (Convert.ToString(Session("chktype")).ToUpper().Replace(" ", "") = "DCB" Or Convert.ToString(Session("chktype")).ToUpper().Replace(" ", "") = "DC") Then
                    '            'Added new upi condition 
                    '            If (Convert.ToString(Session("chktype")).ToUpper().Replace(" ", "") = "DCB" Or Convert.ToString(Session("chktype")).ToUpper().Replace(" ", "") = "DC" Or Convert.ToString(Session("chktype")).ToUpper().Replace(" ", "") = "UPI") Then

                    '                Dim sourceid As String = "NA" 'arrListrefInfo.Item(0).ToString()
                    '                ' Biller ID
                    '                Dim billerid As String = "KARVYMFS" 'arrListrefInfo.Item(1).ToString() 
                    '                Dim txn_amount As String = Replace(FormatNumber(CDbl(amount), 2), ",", "")

                    '                Dim RU As String

                    '                RU = ConfigurationManager.AppSettings("BillDesk_returnurl").ToString()

                    '                '23122020
                    '                'If ConfigurationManager.AppSettings("EnvironmentFlag") = "LIVE" Then

                    '                '    'RU = "https://mfs.kfintech.com/InvestorServices/OnlinePurchase/PaymentConfirmation.aspx" ' Live Site
                    '                '    RU = "https://www.karvymfs.com/karvy/InvestorServices/OnlinePurchase/mobilePaymentConfirmation.aspx"
                    '                'Else
                    '                '    'RU = "https://www.karvymfs.com/karvyonlinetest/InvestorServices/OnlinePurchase/PaymentConfirmation.aspx"
                    '                '    RU = "https://www.karvymfs.com/karvyonlinetest/InvestorServices/OnlinePurchase/mobilePaymentConfirmation.aspx"

                    '                '    ' Test Site
                    '                'End If
                    '                Dim txtBankID As String
                    '                Dim distds As New DataSet()
                    '                Dim txtMerchantUserRefNo As String
                    '                If ds.Tables(0).Columns.Contains("i_InvDistFlag") And ds.Tables(0).Columns.Contains("i_acno") Then
                    '                    'Venkatesh Added
                    '                    'If (Convert.ToString(ds.Tables(0).Rows(0)("i_InvDistFlag")).ToUpper() = "A" Or Convert.ToString(ds.Tables(0).Rows(0)("i_InvDistFlag")).ToUpper() = "G" Or Convert.ToString(ds.Tables(0).Rows(0)("i_InvDistFlag")).ToUpper() = "W") And Convert.ToString(ds.Tables(0).Rows(0)("i_acno")) = "0" Then
                    '                    If (Convert.ToString(ds.Tables(0).Rows(0)("i_InvDistFlag")).ToUpper() = "M" Or Convert.ToString(ds.Tables(0).Rows(0)("i_InvDistFlag")).ToUpper() = "A" Or Convert.ToString(ds.Tables(0).Rows(0)("i_InvDistFlag")).ToUpper() = "G" Or Convert.ToString(ds.Tables(0).Rows(0)("i_InvDistFlag")).ToUpper() = "W") And Convert.ToString(ds.Tables(0).Rows(0)("i_acno")) = "0" Then
                    '                        distds = Session("distpur")
                    '                        txtBankID = distds.Tables(1).Rows(0)("BANKID").ToString()
                    '                        txtMerchantUserRefNo = distds.Tables(1).Rows(0)("bank_acno").ToString()
                    '                    Else
                    '                        txtBankID = ds.Tables(0).Rows(0)("BANKID").ToString() 'BankID
                    '                        txtMerchantUserRefNo = txtBankID.Split("~")(2)
                    '                        txtBankID = ds.Tables(0).Rows(0)("BANKID").ToString()
                    '                    End If
                    '                Else
                    '                    txtBankID = ds.Tables(0).Rows(0)("BANKID").ToString() 'BankID
                    '                    txtMerchantUserRefNo = txtBankID.Split("~")(2)
                    '                    txtBankID = ds.Tables(0).Rows(0)("BANKID").ToString()
                    '                End If
                    '                Dim BankID = txtBankID.Split("~")(0)
                    '                Dim pinfo1 As String = ds.Tables(0).Rows(0)("i_Scheme").ToString() + ds.Tables(0).Rows(0)("i_Plan").ToString() + ds.Tables(0).Rows(0)("i_Option").ToString()
                    '                Dim additional_info1 As String = pinfo1 + ds.Tables(0).Rows(0)("ID").ToString() 'Reference Number/Application Number
                    '                Dim additional_info2 As String = ds.Tables(0).Rows(0)("i_Acno").ToString()

                    '                Dim additional_info3 As String = ""
                    '                If (Not ds.Tables(0).Rows(0)("USERID") Is Nothing And ds.Tables(0).Rows(0)("USERID") <> "") Then
                    '                    additional_info3 = ds.Tables(0).Rows(0)("USERID").ToString()   'UserID
                    '                ElseIf dsDtrWeb.Tables(0).Columns.Contains("dd_lname") Then
                    '                    If dsDtrWeb.Tables(0).Rows(0)("dd_lname") <> "" And Not dsDtrWeb.Tables(0).Rows(0)("dd_lname") Is Nothing Then
                    '                        additional_info3 = dsDtrWeb.Tables(0).Rows(0)("dd_lname")
                    '                    End If
                    '                End If

                    '                Dim additional_info4 As String = arncode
                    '                Dim additional_info5 As String = fund
                    '                Dim additional_info6 As String = "Liquid"  '

                    '                Category = "11"

                    '                If Trim(Category) = "11" Then

                    '                    additional_info7 = "RESIDENT"   'Investor Type eg:resident / NRI-NRO /NRI - NRE

                    '                ElseIf Trim(Category) = "21" Then

                    '                    additional_info7 = "NRI-NRE"   'Investor Type eg:resident / NRI-NRO /NRI - NRE

                    '                Else

                    '                    additional_info7 = "NRI-NRO"   'Investor Type eg:resident / NRI-NRO /NRI - NRE

                    '                End If

                    '                Dim additional_info8 As String = Format(TrsSavedDate, "yyyyMMddhhmmss") 'txnDate.ToString("yyyyMMddhhmmss")  'Txn time

                    '                Dim additional_info9 As String = ds.Tables(0).Rows(0)("i_Scheme").ToString()



                    '                Dim additional_info10 As String = "NA" 'arrListrefInfo.Item(26).ToString() 'N 
                    '                Dim additional_info11 As String = "L" 'arrListrefInfo.Item(27).ToString() 'N
                    '                ' AMC Code
                    '                Dim additional_info12 As String = "NA" 'arrListrefInfo.Item(28).ToString() 'N
                    '                add_info7 = additional_info7 & "-" & ds.Tables(0).Rows(0)("i_Scheme").ToString() + ds.Tables(0).Rows(0)("i_Plan").ToString() & "-NA-" & ds.Tables(0).Rows(0)("i_Option").ToString() & "-" & additional_info4 & "-NA"
                    '                hdnTrtype.Value = ds.Tables(0).Rows(0)("trtype").ToString()

                    '                hdnId.Value = ds.Tables(0).Rows(0)("ID").ToString()

                    '                Dim dsmerchantlist As DataSet = Get_merchantlist(Session("fundcode").ToString())
                    '                If (dsmerchantlist.Tables.Count > 0 And dsmerchantlist.Tables(0).Rows.Count > 0) Then
                    '                    billerid = dsmerchantlist.Tables(0).Rows(0)("Mid").ToString()
                    '                    Cheksumkey = dsmerchantlist.Tables(0).Rows(0)("Mchksumid").ToString()
                    '                End If
                    '                'If Session("fundcode").ToString() = "187" Then
                    '                '    billerid = "KRVYMIRMF"
                    '                '    Cheksumkey = "qwFDysRn06GM"
                    '                'End If
                    '                Dim data As [String]

                    '                additional_info6 = "LIQUID"
                    '                Using billdskappcon As New SqlConnection(kboltconstr)
                    '                    Using billdskappda As New SqlDataAdapter("ktrack_change_appno_ihno", billdskappcon)
                    '                        billdskappda.SelectCommand.Parameters.AddWithValue("@fund", Convert.ToString(dsDtrWeb.Tables(0).Rows(0)("dd_fund")))
                    '                        billdskappda.SelectCommand.Parameters.AddWithValue("@ihno", dsDtrWeb.Tables(0).Rows(0)("dd_ihno"))
                    '                        billdskappda.SelectCommand.Parameters.AddWithValue("@branch", dsDtrWeb.Tables(0).Rows(0)("dd_branch"))

                    '                        Dim billdeskds As New DataSet()
                    '                        billdskappda.SelectCommand.CommandType = CommandType.StoredProcedure
                    '                        billdskappda.Fill(billdeskds)
                    '                        If Convert.ToString(billdeskds) <> Nothing Then
                    '                            If billdeskds.Tables.Count > 0 And billdeskds.Tables(0).Rows.Count > 0 Then
                    '                                additional_info1 = Convert.ToString(billdeskds.Tables(0).Rows(0)("appno"))

                    '                            End If
                    '                        End If
                    '                    End Using

                    '                End Using

                    '                If (BankID.ToUpper().Trim() = "CMP") Then

                    '                    data = billerid & "|" & additional_info1 & "|" & txtMerchantUserRefNo & "|" & txn_amount & "|" & BankID & "|NA|NA|INR|DIRECTD|R" & "|" & billerid.ToLower() & "|" & "NA|NA|F" & "|" & additional_info2 & "|" & additional_info3 & "|" & additional_info4 & "|" & additional_info5 & "|" & additional_info6 & "|" & add_info7 & "|" & additional_info8 & "|" & RU
                    '                Else
                    '                    data = billerid & "|" & additional_info1 & "|" & txtMerchantUserRefNo & "|" & txn_amount & "|" & BankID & "|NA|NA|INR|DIRECT|R" & "|" & billerid.ToLower() & "|" & "NA|NA|F" & "|" & additional_info2 & "|" & additional_info3 & "|" & additional_info4 & "|" & additional_info5 & "|" & additional_info6 & "|" & add_info7 & "|" & additional_info8 & "|" & RU

                    '                End If

                    '                Dim additional_info1temp As String
                    '                additional_info1temp = "TSGPG" + ds.Tables(0).Rows(0)("ID").ToString()


                    '                Dim datatemp As String
                    '                ChecksumValue = objChecksum.GenerateCheckSum(data, Cheksumkey)

                    '                msg.Value = data & "|" & ChecksumValue
                    '                'Response.Write("<script>alert('" & msg.Value & "')</script>") 

                    '                'If (Convert.ToString(Session("chktype")).ToUpper().Replace(" ", "") = "UPI" And fund = "117") Then
                    '                '    BillDeskXML1(ds)
                    '                'End If

                    '                Dim sch As String = ds.Tables(0).Rows(0)("i_Scheme").ToString()
                    '                Dim pln As String = ds.Tables(0).Rows(0)("i_Plan").ToString()
                    '                Dim opt As String = ds.Tables(0).Rows(0)("i_Option").ToString()
                    '                Dim uid As String = additional_info3
                    '                Dim chqno As String = "213123"
                    '                Dim Chqdt As String = Now.Date.Date.ToString()
                    '                Dim Chqtype As String = ""
                    '                Dim uerid As String = ds.Tables(0).Rows(0)("USERID").ToString()
                    '                Dim p_BranchName, p_Pangno, p_Mstatus, p_Fname, p_Mname, p_Lname, p_Mailid, p_Mapinid, p_entby, p_trdt, p_entdt, p_RTGSAcctype, p_EUINno As String
                    '                Dim p_EUINflag, p_EUINopt, p_EUINsubarncode, p_dpid, p_clientid, p_allotmentmode, p_proxybranch, o_ErrNo, o_ErrMsg As String
                    '                p_BranchName = ""
                    '                p_Pangno = panno
                    '                p_Mstatus = ""
                    '                p_Fname = ""
                    '                p_Mname = ""
                    '                p_Lname = ""
                    '                p_Mailid = ""
                    '                p_Mapinid = ""
                    '                p_entby = uerid
                    '                p_trdt = "05/12/2015"
                    '                p_entdt = "05/12/2015"
                    '                p_RTGSAcctype = ""
                    '                p_EUINno = euin
                    '                p_EUINflag = ""
                    '                p_EUINflag = ""
                    '                p_EUINsubarncode = ""
                    '                p_dpid = ""
                    '                p_clientid = ""
                    '                p_allotmentmode = ""
                    '                p_proxybranch = ""
                    '                o_ErrNo = ""
                    '                o_ErrMsg = ""


                    '                Dim s As String = fund & " ~ " & sch & " ~ " & pln & " ~ " & opt & " ~ " & folio & " ~ " & amount & " ~ " & "i_Agent" & " ~ " & subbroker & " ~ " & chqno & " ~ " & Chqdt & " ~ " & "" & " ~ " & "" & " ~ " & "i_Bankacno" & " ~ " & uerid & " ~ " & p_BranchName & " ~ " & p_Pangno & " ~ " & p_Mstatus & " ~ " & p_Fname & " ~ " & p_Mname & " ~ " & p_Lname & " ~ " & p_Mailid & " ~ " & p_Mapinid & " ~ " & p_entby & " ~ " & p_trdt & " ~ " & p_entdt & " ~ " & p_RTGSAcctype & " ~ " & p_EUINno & " ~ " & p_EUINflag & " ~ " & p_EUINopt & " ~ " & p_EUINsubarncode & " ~ " & p_dpid & " ~ " & p_clientid & " ~ " & p_allotmentmode & " ~ " & p_proxybranch & " ~ " & o_ErrNo & " ~ " & o_ErrMsg

                    '                Dim ipaddr As String = Request.ServerVariables("HTTP_X_FORWARDED_FOR")
                    '                If ipaddr = "" Or ipaddr Is Nothing Then
                    '                    ipaddr = Request.ServerVariables("REMOTE_ADDR")
                    '                    If ipaddr = "127.0.0.1" Or ipaddr = Nothing Then
                    '                        ipaddr = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName()).AddressList(1).ToString()
                    '                    End If
                    '                End If
                    '                Dim sourceapp As String = ""


                    '                If ds.Tables(0).Columns.Contains("i_InvDistFlag") Then
                    '                    If Convert.ToString(ds.Tables(0).Rows(0)("i_InvDistFlag")).ToUpper() = "A" Then
                    '                        sourceapp = "KFinKart Distributor APP"
                    '                    ElseIf Convert.ToString(ds.Tables(0).Rows(0)("i_InvDistFlag")).ToUpper() = "G" Then
                    '                        sourceapp = "Kbolt Go mobile Application"
                    '                    ElseIf Convert.ToString(ds.Tables(0).Rows(0)("i_InvDistFlag")).ToUpper() = "W" Then
                    '                        sourceapp = "WebileApps Website"
                    '                    Else
                    '                        sourceapp = getfunddesc(hdncustfund.Value) + "Distributor WebSite"
                    '                    End If
                    '                End If
                    '                Dim browser As String = ""
                    '                browser = Request.ServerVariables("HTTP_USER_AGENT")
                    '                TrackbillDeskTransactions("Insert", fund, additional_info1, ds.Tables(0).Rows(0)("ihno"), msg.Value, ipaddr, sourceapp, browser)

                    '            End If

                    '        End If

                    '    Catch ex As Exception

                    '        If Not ex.Message = "Thread was being aborted." Then

                    '            booError = True
                    '            Session("Error") = ex

                    '        End If
                    '        If booError Then Response.Redirect("../General/ErrorPage.aspx")
                    '    End Try
                    '    booError = False
                    '    'End If
                    'End If
                End If
            End If
        Catch ex As Exception

            If Not ex.Message = "Thread was being aborted." Then
                booError = True
                Session("Error") = ex
            End If
            If booError Then Response.Redirect("../General/ErrorPage.aspx")
        End Try
        booError = False
    End Sub
    Function Get_merchantlist(ByVal fund As String) As DataSet
        dbRequest = New KBDBRequest
        dSub = New KBSQLFactory
        params = New ArrayList
        Try

            'dbRequest.ConnectionString = ConfigurationManager.AppSettings(cfund & "test").ToString
            dbRequest.ConnectionString = kboltconstr
            'dbRequest.Command = "WB99_GetSchDetails1_v2"
            dbRequest.Command = "Ktrack_getMerchantDet"
            dbRequest.CommandType = CommandType.StoredProcedure
            subPrepareParam(1, "@Fund", 50, fund, KBDBRequest.Common_DataType.TypeVarChar)
            dbRequest.Parameters = params
            dsSub = dSub.ExecuteDataSet(dbRequest).returnedDataSet
        Catch ex As Exception
            If Not ex.Message = "Thread was being aborted." Then
                booError = True
                Session("Error") = ex
            End If
            dbRequest = Nothing
            params = Nothing
        Finally
            If booError Then Response.Redirect("../General/ErrorPage.aspx")
        End Try
        booError = False
        Return dsSub
    End Function
    Private Sub subPrepareParam(ByVal intDirection As Integer, ByVal strParamName As String, ByVal intSize As Integer, ByVal strParamValue As String, ByVal intDataType As String)
        param = New KBDBRequest.Parameter
        param.Direction = intDirection
        param.ParamName = strParamName
        param.Size = intSize
        param.ParamValue = strParamValue
        param.ProviderType = ConfigurationManager.AppSettings("provider" & cfund).ToString
        param.DataType = intDataType
        params.Add(param)
    End Sub
    Public Sub TrackbillDeskTransactions(ByVal flag As String, ByVal fund As String, ByVal appno As String, ByVal ihno As String, ByVal billdeskstr As String, ByVal ip As String, ByVal remarks As String, ByVal browsername As String)
        Try
            Dim con As SqlConnection
            Dim sqlcmd As SqlCommand
            Dim dsgetdata As DataSet
            Dim dagetdata As SqlDataAdapter
            ' Dim ds As String
            ' Dim obj1 As New EncodingDecoding
            dsgetdata = New DataSet()
            sqlcmd = New SqlCommand()
            Dim strsql As String = kboltconstr
            con = New SqlConnection(strsql)

            dagetdata = New SqlDataAdapter("Ktrack_billDesk_Transactions_Track_V17", con)
            dagetdata.SelectCommand.CommandType = CommandType.StoredProcedure
            dagetdata.SelectCommand.Parameters.Add("@i_Flag", SqlDbType.VarChar, 10).Value = flag
            dagetdata.SelectCommand.Parameters.Add("@i_Fund", SqlDbType.VarChar, 5).Value = fund
            dagetdata.SelectCommand.Parameters.Add("@i_AppRefNo", SqlDbType.VarChar, 25).Value = appno
            dagetdata.SelectCommand.Parameters.Add("@i_IHNo", SqlDbType.VarChar, 25).Value = ihno
            'dagetdata.SelectCommand.Parameters.Add("@i_Branch", SqlDbType.VarChar, 10).Value = i_Acno
            dagetdata.SelectCommand.Parameters.Add("@i_BillDeskString", SqlDbType.VarChar, 650).Value = billdeskstr
            dagetdata.SelectCommand.Parameters.Add("@i_IPAddress", SqlDbType.VarChar, 50).Value = ip
            dagetdata.SelectCommand.Parameters.Add("@i_Remarks", SqlDbType.VarChar, 50).Value = remarks
            dagetdata.SelectCommand.Parameters.Add("@i_BrowserName", SqlDbType.VarChar, 550).Value = browsername


            dagetdata.SelectCommand.CommandTimeout = 1000
            dagetdata.Fill(dsgetdata)

            If dsgetdata.Tables.Count > 1 Then
                If dsgetdata.Tables(0).Rows.Count > 0 Then
                    dsgetdata.Tables(0).TableName = "Dtinformation"
                    dsgetdata.Tables(1).TableName = "DtData"
                Else
                    dsgetdata.Tables(0).TableName = "Dtinformation"
                End If
            End If
        Catch ex As Exception
            dbRequest = Nothing
            params = Nothing
        Finally
            dsSub = Nothing
        End Try
    End Sub

    Public Function getfunddesc(ByVal fundcode As String) As String
        Dim fnds As New DataSet()
        Dim strfunddesc As String = ""
        Try
            Using con As New SqlConnection(kboltconstr)
                Using da As New SqlDataAdapter("ktrack_getfundslist", con)
                    da.SelectCommand.Parameters.AddWithValue("@fund", fundcode)
                    da.SelectCommand.CommandType = CommandType.StoredProcedure
                    da.Fill(fnds)
                    If Not fnds.Tables(0) Is Nothing Then
                        If fnds.Tables(0).Rows.Count > 0 Then
                            strfunddesc = Convert.ToString(fnds.Tables(0).Rows(0)("funddesc"))
                        End If
                    End If
                End Using
            End Using
        Catch ex As Exception
            objpayconfirm.WriteLog("", ex.Message, ex.StackTrace, "getfunddesc()")
        Finally
            dsSub = Nothing
        End Try
        Return strfunddesc
    End Function
    Private Sub BillDeskXML1(ds As DataSet)
        'ds = Session("qparam")
        Dim dsDtr As DataSet
        dsDtr = Session("dsconfdata")
        If Not ds Is Nothing Then
            If ds.Tables.Count > 0 Then
                If ds.Tables(0).Rows.Count > 0 Then

                    Dim sAdditionalInfo1 As String = ds.Tables(0).Rows(0)("i_Acno").ToString 'folio'
                    Dim sAdditionalInfo2 As String = dsDtr.Tables(0).Rows(0)("dd_appno").ToString 'Appno
                    Dim sAdditionalInfo3 As String = ds.Tables(0).Rows(0)("EuinValid").ToString
                    Dim sAdditionalInfo4 As String = "MIRE DIRECT"
                    Dim sAdditionalInfo5 As String = "NA"
                    Dim sAdditionalInfo6 As String = "NA"
                    Dim sAdditionalInfo7 As String = "NA"
                    '
                    'Dim sAdditionalInfo6 As String = ds.Tables(0).Rows(0)("Category").ToString
                    'Dim sAdditionalInfo7 As String = ds.Tables(0).Rows(0)("InvestorType").ToString

                    Dim cntRows As Integer = ds.Tables(0).Rows.Count
                    Dim BillDeskXML As String = ""
                    BillDeskXML = "<REQUEST><TXNDATA><TXNSUMMARY>"
                    BillDeskXML = BillDeskXML & "<REQID>" & "PGECOM201" & "</REQID>"
                    BillDeskXML = BillDeskXML & "<PGMERCID>" & "MIRAEMFEM" & "</PGMERCID>"
                    BillDeskXML = BillDeskXML & "<RECORDS>" & cntRows & "</RECORDS>"
                    BillDeskXML = BillDeskXML & "<PGCUSTOMERID>" & dsDtr.Tables(0).Rows(0)("dd_appno").ToString & "</PGCUSTOMERID>"
                    BillDeskXML = BillDeskXML & "<AMOUNT>" & Convert.ToDouble(ds.Tables(0).Rows(0)("i_UntAmtValue")) & ".00" & "</AMOUNT>"
                    ''BillDeskXML = BillDeskXML & "<AMOUNT>" & "5.00" & "</AMOUNT>"
                    BillDeskXML = BillDeskXML & "<TXNDATE>" & Convert.ToInt64(DateTime.Now.ToString("yyyyMMddhhmmss")) & "</TXNDATE></TXNSUMMARY>"
                    Dim cnt As Integer = 1
                    For Each dr As DataRow In ds.Tables(0).Rows
                        BillDeskXML = BillDeskXML & "<RECORD ID=""" & cnt & """>"
                        BillDeskXML = BillDeskXML & "<MERCID>" & "MIRAEMFSCH" & "</MERCID>"
                        BillDeskXML = BillDeskXML & "<AMOUNT>" & Convert.ToDouble(dr("i_UntAmtValue")) & ".00" & "</AMOUNT>"
                        'BillDeskXML = BillDeskXML & "<AMOUNT>" & "1.00" & "</AMOUNT>"
                        BillDeskXML = BillDeskXML & "<CUSTOMERID>" & dr("ihno") & "</CUSTOMERID>"
                        BillDeskXML = BillDeskXML & "<ADDITIONALINFO1>" & IIf(IsDBNull(dr("mobile")), "NA", dr("mobile")) & "</ADDITIONALINFO1>"
                        '' BillDeskXML = BillDeskXML & "<ADDITIONALINFO1>" & "9666000505" & "</ADDITIONALINFO1>"
                        BillDeskXML = BillDeskXML & "<ADDITIONALINFO2>" & IIf(IsDBNull(dr("email")), "NA", dr("email")) & "</ADDITIONALINFO2>"
                        ''BillDeskXML = BillDeskXML & "<ADDITIONALINFO2>" & "girishdhanani@gmail.com" & "</ADDITIONALINFO2>"
                        BillDeskXML = BillDeskXML & "<ADDITIONALINFO3>" & IIf(IsDBNull(dr("trtype")), "NA", dr("trtype")) & "</ADDITIONALINFO3>"
                        ''BillDeskXML = BillDeskXML & "<ADDITIONALINFO3>" & "NA" & "</ADDITIONALINFO3>"
                        ''BillDeskXML = BillDeskXML & "<ADDITIONALINFO4>" & IIf(IsDBNull(dr("dd_mobileno")), "NA", dr("dd_mobileno")) & "</ADDITIONALINFO4>"
                        BillDeskXML = BillDeskXML & "<ADDITIONALINFO4>" & "NA" & "</ADDITIONALINFO4>"
                        ''BillDeskXML = BillDeskXML & "<ADDITIONALINFO5>" & IIf(IsDBNull(dr("dd_email")), "NA", dr("dd_email")) & "</ADDITIONALINFO5>"
                        BillDeskXML = BillDeskXML & "<ADDITIONALINFO5>" & "NA" & "</ADDITIONALINFO5>"
                        BillDeskXML = BillDeskXML & "<ADDITIONALINFO6>" & "NA" & "</ADDITIONALINFO6>"
                        BillDeskXML = BillDeskXML & "<ADDITIONALINFO7>" & "NA" & "</ADDITIONALINFO7>"
                        BillDeskXML = BillDeskXML & "<FILLER1>" & "NA" & "</FILLER1>"
                        BillDeskXML = BillDeskXML & "<FILLER2>" & "NA" & "</FILLER2>"
                        BillDeskXML = BillDeskXML & "<FILLER3>" & "NA" & "</FILLER3>"
                        BillDeskXML = BillDeskXML & "</RECORD>"
                        cnt = cnt + 1
                    Next
                    BillDeskXML = BillDeskXML & "</TXNDATA>"
                    Dim CheckSumgenXML = BillDeskXML.Replace("<REQUEST>", "")
                    Dim sa As SHASample
                    sa = New SHASample
                    Dim result As String
                    result = ""
                    Dim checksum = Trim(sa.GetHMACSHA256(CheckSumgenXML, "hHlY0zzv4yRT"))
                    checksum = checksum.ToUpper()
                    BillDeskXML = "<?xml version=""1.0"" encoding=""UTF-8""?>" & BillDeskXML & "<CHECKSUM>" & checksum & "</CHECKSUM></REQUEST>"
                    'Try

                    '    Dim parms = "Adminusername=c21hcnRzZXJ2aWNl" + "&" + "Adminpassword=a2FydnkxMjM0JTI0%2520" + "&" + "IMEI=ODY2NzQxMDMzNDQwMjQx%0A" + "&" + "OS=QW5kcm9pZA%3D%3D%0A" + "&" + "APKVer=NC4yLjgx%0A" + "&" + "XmlData=" + BillDeskXML

                    '    Dim httpWebRequest = DirectCast(WebRequest.Create("https://clientwebsitesuat3.kfintech.com/25UAT/Smartservice.svc/UPIDetails?" & parms), HttpWebRequest)
                    '    ' httpWebRequest.ContentType = "application/json"
                    '    httpWebRequest.Method = "GET"

                    '    Dim httpResponse = DirectCast(httpWebRequest.GetResponse(), HttpWebResponse)
                    '    Using streamReader = New StreamReader(httpResponse.GetResponseStream())
                    '        result = streamReader.ReadToEnd()
                    '    End Using
                    'Catch ex As Exception
                    '    If Not ex.Message = "Thread was being aborted." Then
                    '        MFIPaymentConfirmation.WriteLog("", ex.Message, ex.StackTrace, "BillDeskXML1")
                    '    End If
                    'End Try
                    Dim objResponse As HttpWebResponse
                    ServicePointManager.Expect100Continue = True
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                    Dim origRequest As HttpWebRequest = DirectCast(HttpWebRequest.Create("https://payments.billdesk.com/ecom/ECOM2ReqHandler?msg='" + BillDeskXML + "'"), HttpWebRequest)
                    origRequest.AllowAutoRedirect = False
                    origRequest.Method = "POST"
                    objResponse = DirectCast(origRequest.GetResponse(), HttpWebResponse)
                    Dim Stream As Stream = objResponse.GetResponseStream()
                    Dim sr As New StreamReader(Stream, Encoding.GetEncoding("utf-8"))
                    Dim str As String = sr.ReadToEnd()
                    ConvertXMLToDataSet(str)

                End If
            End If
        End If

    End Sub
    Public Function ConvertXMLToDataSet(ByVal xmlData As String) As DataSet
        Dim stream As StringReader = Nothing
        Dim reader As XmlTextReader = Nothing

        Try
            Dim xmlDS As DataSet = New DataSet()
            stream = New StringReader(xmlData)
            reader = New XmlTextReader(stream)
            xmlDS.ReadXml(reader)
            PreparePaymentString(xmlDS)
            Return xmlDS
        Catch
            Return Nothing
        Finally
            If reader IsNot Nothing Then reader.Close()
        End Try
    End Function
    Public Function PreparePaymentString(ByVal xmlData As DataSet)
        Dim MerchantID, PGCUSTOMERID, InvestorBankAccntNo, TxnAmount, BANKID, CurrencyType, PRODUCTID, TypeField1, SecurityID, TypeField2, AdditionalInfo1, AdditionalInfo2, AdditionalInfo3, AdditionalInfo4, AdditionalInfo5, AdditionalInfo6, AdditionalInfo7, RU As String
        If xmlData.Tables.Count > 0 Then
            If xmlData.Tables("TXNSUMMARY").Rows.Count > 0 Then
                If xmlData.Tables("TXNSUMMARY").Rows.Count > 0 Then
                    PGCUSTOMERID = xmlData.Tables("TXNSUMMARY").Rows(0)("PGCUSTOMERID")
                End If
                If xmlData.Tables("TXNSUMMARY").Rows.Count > 0 Then
                    TxnAmount = xmlData.Tables("TXNSUMMARY").Rows(0)("AMOUNT")
                End If
            End If
            Dim ds As DataSet = Session("qparam")
            Dim dsDtr As DataSet = Session("dsconfdata")
            MerchantID = "MIRAEMFEM"
            CurrencyType = "INR"
            TypeField1 = "R"
            SecurityID = "MIRAEMFEM".ToLower()
            TypeField2 = "F"
            Dim sAdditionalInfo1 As String = ds.Tables(0).Rows(0)("i_Acno").ToString 'folio'
            Dim sAdditionalInfo2 As String = dsDtr.Tables(0).Rows(0)("dd_appno").ToString 'Appno
            Dim sAdditionalInfo3 As String = ds.Tables(0).Rows(0)("EuinValid").ToString
            Dim sAdditionalInfo4 As String = "MIRE DIRECT"
            Dim sAdditionalInfo5 As String = "NA"
            Dim sAdditionalInfo6 As String = "11"
            Dim sAdditionalInfo7 As String = "NRI"
            'Dim sAdditionalInfo6 As String = ds.Tables(0).Rows(0)("Category").ToString
            'Dim sAdditionalInfo7 As String = ds.Tables(0).Rows(0)("InvestorType").ToString
            Dim IFSC As String = String.Empty
            Dim txtMerchantUserRefNo As String = String.Empty
            Dim sbankID = ds.Tables(0).Rows(0)("BANKID").ToString() 'BankID
            txtMerchantUserRefNo = sbankID.Split("~")(2)
            AdditionalInfo1 = sAdditionalInfo1
            AdditionalInfo2 = sAdditionalInfo2
            AdditionalInfo3 = sAdditionalInfo3
            AdditionalInfo4 = sAdditionalInfo4
            AdditionalInfo5 = sAdditionalInfo5
            AdditionalInfo6 = sAdditionalInfo6
            AdditionalInfo7 = sAdditionalInfo7
            InvestorBankAccntNo = txtMerchantUserRefNo
            BANKID = sbankID
            PRODUCTID = "DIRECT"

            RU = "http://localhost:63010/InvestorServices/OnlinePurchase/mobilePaymentConfirmation_new.aspx"
            'If ConfigurationManager.AppSettings("EnvironmentFlag") = "LIVE" Then
            '    RU = "https://transact.miraeassetmf.co.in/investor/Transactions/AddPaymentConfirmation.aspx" ' Live Site
            'Else
            '    RU = "https://clientwebsitesuat2.kfintech.com/miraeonline/Transactions/AddPaymentConfirmation.aspx" ' Live Site
            'End If
            If (Convert.ToString(Session("chktype")).ToUpper().Replace(" ", "") = "UPI") Then
                BANKID = "IC4"
                'IFSC = ds.Tables(0).Rows(0)("ifsc_code").ToString()
                IFSC = "ICIC0000008"
                If IFSC <> Nothing Then
                    txtMerchantUserRefNo = sbankID.Split("~")(2) + "~" + IFSC
                End If
                InvestorBankAccntNo = txtMerchantUserRefNo
                'InvestorBankAccntNo = InvestorBankAccntNo & "~" & IFSC
                'InvestorBankAccntNo = "247501500661" & "~" & "ICIC0002475"
            End If
            'MerchantID|CustomerID|InvestorBankAccntNo|TxnAmount|BANKID|NA|NA|CurrencyType|PRODUCTID|TypeField1|SecurityID|NA|NA|TypeField2|AdditionalInfo1|AdditionalInfo2|AdditionalInfo3|AdditionalInfo4|AdditionalInfo5|AdditionalInfo6|AdditionalInfo7|RU|Checksum
            Dim paystring As String = MerchantID & "|" & PGCUSTOMERID & "|" & InvestorBankAccntNo & "|" & TxnAmount & "|" & BANKID & "|" & "NA" & "|" & "NA" & "|" & CurrencyType & "|" & PRODUCTID & "|" & TypeField1 & "|" & SecurityID & "|" & "NA" & "|" & "NA" & "|" & TypeField2 & "|" & AdditionalInfo1 & "|" & AdditionalInfo2 & "|" & AdditionalInfo3 & "|" & AdditionalInfo4 & "|" & AdditionalInfo5 & "|" & AdditionalInfo6 & "|" & AdditionalInfo7 & "|" & RU
            ''txtBilldesk2.Value = paystring
            Dim sa1 As SHASample
            sa1 = New SHASample
            Dim checksumString As String = Trim(sa1.GetHMACSHA256(paystring, "hHlY0zzv4yRT"))
            Dim chk As String = checksumString.ToUpper()
            Dim Cheksumpaystring As String = MerchantID & "|" & PGCUSTOMERID & "|" & InvestorBankAccntNo & "|" & TxnAmount & "|" & BANKID & "|" & "NA" & "|" & "NA" & "|" & CurrencyType & "|" & PRODUCTID & "|" & TypeField1 & "|" & SecurityID & "|" & "NA" & "|" & "NA" & "|" & TypeField2 & "|" & AdditionalInfo1 & "|" & AdditionalInfo2 & "|" & AdditionalInfo3 & "|" & AdditionalInfo4 & "|" & AdditionalInfo5 & "|" & AdditionalInfo6 & "|" & AdditionalInfo7 & "|" & RU & "|" & chk
            msg.Value = Cheksumpaystring
            'txtPayCategory.Value = "NETBANKING"
        End If
        Return Nothing
    End Function

    'Kathe add
    Protected Sub resendgenerateotp_Click(sender As Object, e As EventArgs) Handles resendbtn.Click
        Dim dssip As New DataSet()
        Dim TranNo, MobileNo, TranType, PAn, acno
        Dim lname As String
        dssip = Session("qparam")
        Dim rnd As New Random()
        Dim Otp = rnd.Next(1000, 99999)
        Session("OTP") = Otp
        'Dim acno = Session("Acno")



        If dssip.Tables(0).Columns.Contains("dd_lname") Then
            If Convert.ToString(dssip.Tables(0).Rows(0)("dd_lname")) <> "" Then
                lname = Convert.ToString(dssip.Tables(0).Rows(0)("dd_lname"))

            End If

        End If
        If dssip.Tables(0).Columns.Contains("dd_fname") Then
            If Convert.ToString(dssip.Tables(0).Rows(0)("dd_fname")) <> "" Then
                lname = Convert.ToString(dssip.Tables(0).Rows(0)("dd_fname"))

            End If

        End If


        If dssip.Tables(0).Columns.Contains("dd_appno") Then
            TranNo = dssip.Tables(0).Rows(0)("dd_appno")
        End If
        If dssip.Tables(0).Columns.Contains("dd_appno") Then
            TranNo = dssip.Tables(0).Rows(0)("dd_appno")
        End If

        If dssip.Tables(0).Columns.Contains("dd_mobileno") Then
            MobileNo = dssip.Tables(0).Rows(0)("dd_mobileno")
        End If
        If dssip.Tables(0).Columns.Contains("dd_pangno") Then
            PAn = dssip.Tables(0).Rows(0)("dd_pangno")
        End If

        If dssip.Tables(0).Columns.Contains("dd_acno") Then
            acno = dssip.Tables(0).Rows(0)("dd_acno")
        End If
        Dim dsemail As DataSet = fn_getEmailbyFolio("102", acno, PAn)
        'If (dsemail.Tables.Count > 0 And dsemail.Tables(0).Rows.Count > 0) Then
        '    If (dsemail.Tables(0).Rows(0)("Email") <> "") Then
        '        fnEmailOTP_byfolio(dsemail.Tables(0).Rows(0)("Email").ToString(), OtpPin, TranType, transType_OTP, "", Distflag, lname)

        '    End If

        'End If

        'KTrack_GenerateOTP_URL_1("166", acno, Otp, MobileNo, TranType, TranNo, "", hdnTrtype.Value, PAn, "W", lname)

        Otp = DateTime.Now.ToString("HHms")

        'KTrack_Generate_Diff_OTPs_EmailOrMob_trans(166, hdnTrtype.Value, Convert.ToString(MobileNo), Convert.ToString(dsemail.Tables(0).Rows(0)("Email").ToString()), Otp, hdnId.Value)

        KTrack_Generate_Diff_OTPs_EmailOrMob_trans("102", hdnTrtype.Value, Convert.ToString(MobileNo), Convert.ToString(dsemail.Tables(0).Rows(0)("Email").ToString()), Otp, hdnId.Value)
        dsemail.Tables(0).Rows(0)("Email").ToString()

        divotpconfirm.Style("display") = "block"
        divgenerate.Style("display") = "none"


        'otpvalidation()

    End Sub

    Protected Sub btngenerateotp_Click(sender As Object, e As EventArgs) Handles btngenerateotp.Click
        Dim dssip As New DataSet()
        Dim TranNo, MobileNo, TranType, PAn, acno
        Dim lname As String
        dssip = Session("qparam")
        Dim rnd As New Random()
        Dim Otp = rnd.Next(1000, 99999)
        Session("OTP") = Otp
        'Dim acno = Session("Acno")



        If dssip.Tables(0).Columns.Contains("dd_lname") Then
            If Convert.ToString(dssip.Tables(0).Rows(0)("dd_lname")) <> "" Then
                lname = Convert.ToString(dssip.Tables(0).Rows(0)("dd_lname"))

            End If

        End If
        If dssip.Tables(0).Columns.Contains("dd_fname") Then
            If Convert.ToString(dssip.Tables(0).Rows(0)("dd_fname")) <> "" Then
                lname = Convert.ToString(dssip.Tables(0).Rows(0)("dd_fname"))

            End If

        End If


        If dssip.Tables(0).Columns.Contains("dd_appno") Then
            TranNo = dssip.Tables(0).Rows(0)("dd_appno")
        End If
        If dssip.Tables(0).Columns.Contains("dd_appno") Then
            TranNo = dssip.Tables(0).Rows(0)("dd_appno")
        End If

        If dssip.Tables(0).Columns.Contains("dd_mobileno") Then
            MobileNo = dssip.Tables(0).Rows(0)("dd_mobileno")
        End If
        If dssip.Tables(0).Columns.Contains("dd_pangno") Then
            PAn = dssip.Tables(0).Rows(0)("dd_pangno")
        End If

        If dssip.Tables(0).Columns.Contains("dd_acno") Then
            acno = dssip.Tables(0).Rows(0)("dd_acno")
        End If
        Dim dsemail As DataSet = fn_getEmailbyFolio("102", acno, PAn)
        'If (dsemail.Tables.Count > 0 And dsemail.Tables(0).Rows.Count > 0) Then
        '    If (dsemail.Tables(0).Rows(0)("Email") <> "") Then
        '        fnEmailOTP_byfolio(dsemail.Tables(0).Rows(0)("Email").ToString(), OtpPin, TranType, transType_OTP, "", Distflag, lname)

        '    End If

        'End If

        'KTrack_GenerateOTP_URL_1("166", acno, Otp, MobileNo, TranType, TranNo, "", hdnTrtype.Value, PAn, "W", lname)

        Otp = DateTime.Now.ToString("HHms")
      
        'KTrack_Generate_Diff_OTPs_EmailOrMob_trans(166, hdnTrtype.Value, Convert.ToString(MobileNo), Convert.ToString(dsemail.Tables(0).Rows(0)("Email").ToString()), Otp, hdnId.Value)

        KTrack_Generate_Diff_OTPs_EmailOrMob_trans("102", hdnTrtype.Value, Convert.ToString(MobileNo), Convert.ToString(dsemail.Tables(0).Rows(0)("Email").ToString()), Otp, hdnId.Value)
        dsemail.Tables(0).Rows(0)("Email").ToString()

        divotpconfirm.Style("display") = "block"
        divgenerate.Style("display") = "none"


        'otpvalidation()

    End Sub
    Protected Sub btnotpvalidate_Click(sender As Object, e As EventArgs) Handles btnotpvalidate.Click
        'Dim OTP = Session("OTP")
        Dim dsotp As New DataSet()
        If (txtOtp.Text = "") Then
            ScriptManager.RegisterStartupScript(Page, Me.GetType(), "OTP Validation", "alert('Please enter OTP')", True)
            'btnConfirmpay.Attributes.Add("onclick", "disabled=''")
            'ElseIf OTP Is Nothing Then
            '    ScriptManager.RegisterStartupScript(Page, Me.GetType(), "OTP Validation", "alert('Session Expired. Please generate OTP')", True)
        Else
            'If OTP = txtOtp.Text Then
            '    divconfirm.Style("display") = "block"
            '    divgenerate.Style("display") = "none"
            '    divotpconfirm.Style("display") = "none"
            '    Dim dsres As DataSet = savePurchaseData(Session("qparam"))
            'Else
            dsotp = otpvalidation()

            If dsotp.Tables(0).Rows(0)("Error_Code") = "1" Then

                ScriptManager.RegisterStartupScript(Page, Me.GetType(), "OTP Validation", "alert('Please enter Valid OTP')", True)
                Exit Sub
            End If
            Dim dsqparam As New DataSet
            Dim paymode As String
            dsqparam = Session("qparam")
            If dsqparam.Tables(0).Columns.Contains("dd_paymode") Then
                paymode = dsqparam.Tables(0).Rows(0)("dd_paymode").ToString
            End If
            If paymode.ToUpper = "KOTM" Then
                dvconfirmKOTM.Style("display") = "block"
                divconfirm.Style("display") = "none"
            Else
                dvconfirmKOTM.Style("display") = "none"
                divconfirm.Style("display") = "block"
            End If
            'divconfirm.Style("display") = "block"
            divgenerate.Style("display") = "none"
            divotpconfirm.Style("display") = "none"
            Dim dsres As DataSet = savePurchaseData(Session("qparam"))
        End If
        'ScriptManager.RegisterStartupScript(Page, Me.GetType(), "OTP Validation", "alert('Please enter Valid OTP')", True)
        'End If


        'End If


    End Sub

    Public Function KTrack_GenerateOTP_URL_1(ByVal Fund As String, ByVal Folio As String, ByVal OtpPin As String, ByVal MobileNo As String, ByVal TranType As String, ByVal TranNo As String, ByVal URL As String, ByVal transType_OTP As String, ByVal pan As String, Optional ByVal Distflag As String = "", Optional ByVal lname As String = "") As DataSet
        Dim dsmob As New DataSet()
        Try

            Dim dsemail As DataSet = fn_getEmailbyFolio(Fund, Folio, pan)
            If (dsemail.Tables.Count > 0 And dsemail.Tables(0).Rows.Count > 0) Then
                If (dsemail.Tables(0).Rows(0)("Email") <> "") Then
                    fnEmailOTP_byfolio(dsemail.Tables(0).Rows(0)("Email").ToString(), OtpPin, TranType, transType_OTP, "", Distflag, lname)

                End If

            End If

            Using con As New SqlConnection(Convert.ToString(ConfigurationManager.AppSettings("MFDWEB")))
                Using da As New SqlDataAdapter("KTrack_Mob_InsertSMSLog_V17", con)
                    da.SelectCommand.CommandType = CommandType.StoredProcedure

                    da.SelectCommand.Parameters.Add("@i_Folio", SqlDbType.VarChar, 150).Value = Folio
                    da.SelectCommand.Parameters.Add("@i_Fund", SqlDbType.VarChar, 150).Value = Fund
                    da.SelectCommand.Parameters.Add("@i_TrNo", SqlDbType.VarChar, 150).Value = TranNo
                    da.SelectCommand.Parameters.Add("@i_TrType", SqlDbType.VarChar, 150).Value = "ISIP"
                    da.SelectCommand.Parameters.Add("@i_IHNo", SqlDbType.VarChar, 150).Value = "0"
                    da.SelectCommand.Parameters.Add("@i_MobileNo", SqlDbType.VarChar, 150).Value = MobileNo
                    da.SelectCommand.Parameters.Add("@i_OtpNo", SqlDbType.VarChar, 150).Value = OtpPin
                    da.SelectCommand.Parameters.Add("@i_Msg", SqlDbType.VarChar, 150).Value = "Dear Investor, Your One Time Password (OTP) is " + OtpPin + ". - KFintech"
                    da.SelectCommand.CommandTimeout = 1000
                    da.Fill(dsmob)
                End Using
            End Using

            Return dsmob



        Catch ex As Exception
            If Not ex.Message = "Thread was being aborted." Then
                booError = True
                Session("Error") = ex
            End If
        Finally
        End Try
        Return dsmob
    End Function

    Function fn_getEmailbyFolio(ByVal Fund As String, ByVal Folio As String, ByVal pan As String) As DataSet
        Dim dsemail As New DataSet()
        Try
            Using con As New SqlConnection(Convert.ToString(ConfigurationManager.AppSettings("KARVYMFS")))
                Using da As New SqlDataAdapter("KTrack_get_email_v17", con)
                    da.SelectCommand.CommandType = CommandType.StoredProcedure
                    da.SelectCommand.Parameters.Add("@Fund", SqlDbType.VarChar, 5).Value = Fund
                    da.SelectCommand.Parameters.Add("@Folio", SqlDbType.VarChar, 12).Value = Folio
                    da.SelectCommand.Parameters.Add("@pan", SqlDbType.VarChar, 12).Value = pan
                    da.SelectCommand.CommandTimeout = 1000
                    da.Fill(dsemail)
                End Using
            End Using
            Return dsemail
        Catch ex As Exception
            'WriteLog(ex.Message, ex.Source, DateTime.Now.ToString(), "KTrack_get_email_v17", Fund & Folio)
            'Return GetErrorcode(100, "Error occured while processing the Request")
        Finally
        End Try
        Return dsemail
    End Function

    Private Function fnEmailOTP_byfolio(ByVal email As String, ByVal otp As String, Optional ByVal trtype As String = "", Optional transType_OTP As String = "", Optional ByVal modulename As String = "", Optional ByVal customfund As String = "", Optional ByVal Distflag As String = "", Optional ByVal folio As String = "", Optional ByVal lname As String = "") As DataSet
        Dim dsgetdata As New DataSet()
        Try
            Dim mailFlg = "Y"
            'Dim dsgetdata As New DataSet()
            'Dim obj1 As New EncodingDecoding
            '16122020 added for Mirae
            '121*
            Dim fromEmailid_Miraedistributor As String = ConfigurationManager.AppSettings("fromEmailid_MiraeditSMS").ToString
            Using mail As New MailMessage()
                Dim emailstatus As String = Nothing
                '29052017
                email = HttpUtility.UrlDecode(email)
                'email = "popurisindhu86@gmail.com"
                'email = "sreedhar.bobbala@kfintech.com"

                'Dim cfDesc As String = GetCustomFundDesc(customfund)

                If transType_OTP.ToUpper = "S" Then
                    dsgetdata = fnCheckEmail(email)
                    mailFlg = dsgetdata.Tables(0).Rows(0)("EncryptionFlag")
                End If
                'If trtype.ToUpper = "Enach" Then
                '    trtype = "Enach SIP"
                'End If

                If mailFlg = "Y" Then
                    Dim smtp As New SmtpClient()
                    'smtp.Host = ConfigurationManager.AppSettings("Server").ToString()
                    smtp.Host = ConfigurationManager.AppSettings("smtpServer").ToString()
                    smtp.Credentials = New NetworkCredential(ConfigurationManager.AppSettings("smtpUserName").ToString(), ConfigurationManager.AppSettings("smtpPassword").ToString())
                    smtp.Port = Convert.ToInt32(ConfigurationManager.AppSettings("smtpserverport").ToString())
                    smtp.EnableSsl = False

                    mail.From = New System.Net.Mail.MailAddress(ConfigurationManager.AppSettings("fromEmailid_MiraeditSMS").ToString())
                    mail.Subject = "Bajaj DIT OTP"

                    mail.To.Add(email)
                    mail.Bcc.Add("bhimashankar.kathare@kfintech.com")
                    Dim mailbody As New StringBuilder()

                    mailbody.Append("<html class='no-js' lang='en'>")
                    mailbody.Append("<head><title>Bajaj Asset Manangement</title>")
                    mailbody.Append("<meta http-equiv='Content-Type' content='text/html; charset=windows-1252'>")
                    mailbody.Append("<meta name='viewport' content='width=device-width, initial-scale=1.0'>")
                    mailbody.Append("<meta name='description' content='height=device-height' /></head>")

                    mailbody.Append("<style>.collapsetable {border-collapse: collapse;border: 1px solid black;}</style>")
                    mailbody.Append("<body bgcolor='#fff' leftmargin='0' topmargin='0' marginwidth='0' marginheight='0'>")
                    mailbody.Append("<table style='border: 1px solid black;' id='Table_01' width='900' cellpadding='0' cellspacing='0' align='center'><tr><td><img src='https://abcexperiential.com/emailer/asset_management/images/index_01.jpg' style='vertical-align: top' alt='' /></td></tr><tr><td>")
                    mailbody.Append("<table style='font-size: 10.125pt; font-family: Arial; padding: 0px 10px 0px 10px;' id='Table_01' width='900' border='0' cellpadding='0' cellspacing='0' align='center'><tr><td>")
                    If lname <> "" Then
                        mailbody.Append("<tr><td><p></p><p>Dear " + lname + ",</p><p>Thank you For choosing Bajaj Finserv Mutual Fund.</p><p><strong>" + otp + "</strong> Is your One Time Password (OTP) for authentication with Bajaj Finserv Mutual Fund.</p>")
                    Else
                        mailbody.Append("<tr><td><p></p><p>Dear Investor,</p><p>Thank you For choosing Bajaj Finserv Mutual Fund.</p><p><strong>" + otp + "</strong> Is your One Time Password (OTP) for authentication with Bajaj Finserv Mutual Fund.</p>")

                    End If

                    mailbody.Append("<p>Do Not disclose the OTP To anyone.</p></td></tr>")

                    mailbody.Append("<tr><td><p style='text-align: justify;'><br />In case, if you have any queries, please feel free to reach us at 1800 3093 900 (Toll free) between 9.00 am and 6.00 pm from Monday to Friday. Alternatively, you may also write to us at <a href='mailto:service@bajajamc.com' style='text-decoration: none'>service@bajajamc.com</a> . </p>")
                    mailbody.Append("<p>Yours Sincerely,<br>Bajaj Finserv Mutual Fund.  </p></td></tr><tr><td><br/>Mutual Fund investments are subject to market risks, read all scheme related documents carefully.</td></tr></table></td></tr>")
                    mailbody.Append("<tr><td><table id='Table_01' width='900' border='0' cellpadding='0' cellspacing='0' align='center'>")
                    mailbody.Append("<tr><td><img src='https://abcexperiential.com/emailer/asset_management/images/index_03.jpg' style='vertical-align: top' alt='' /></td></tr></table>")



                    mailbody.Append("<table id='Table_01' width='900' border='0' cellpadding='0' cellspacing='0' align='center'><tr><td valgin='top'><a href='https://www.bajajamc.com/' target='_blank'>")
                    mailbody.Append("<img src='https://abcexperiential.com/emailer/asset_management/images/index_04.jpg' alt='' style='vertical-align: top' /></a></td>")

                    'sbMailBody.Append("<td valgin='top'><a href='tel:18000000000'>")
                    mailbody.Append("<td valgin='top'><a href='tel:18000000000'><img src='https://abcexperiential.com/emailer/asset_management/images/index_05.jpg' style='vertical-align: top' alt='' /></a></td>")
                    mailbody.Append("<td valgin='top'><a href='mailto:service@bajajamc.com'><img src='https://abcexperiential.com/emailer/asset_management/images/index_06.jpg' style='vertical-align: top' alt='' /></a></td></tr></table>")
                    mailbody.Append("<table id='Table_01' width='900' border='0' cellpadding='0' cellspacing='0' align='center'>")
                    mailbody.Append("<tr><td><img src='https://abcexperiential.com/emailer/asset_management/images/index_07.jpg' style='vertical-align: top' alt='' /></td>")
                    mailbody.Append("<td><a href='https://www.facebook.com/company/bajaj-finserv-asset-management-ltd/' target='_blank'>")
                    mailbody.Append("<img src='https://abcexperiential.com/emailer/asset_management/images/index_08.jpg' style='vertical-align: top' alt='' /></a></td>")
                    mailbody.Append("<td><a href='https://www.instagram.com/company/bajaj-finserv-asset-management-ltd/' target='_blank'>")
                    mailbody.Append("<img src='https://abcexperiential.com/emailer/asset_management/images/index_09.jpg' style='vertical-align: top' alt='' /></a></td>")
                    mailbody.Append("<td><a href='https://www.linkedin.com/company/bajaj-finserv-asset-management-ltd/' target='_blank'>")
                    mailbody.Append("<img src='https://abcexperiential.com/emailer/asset_management/images/index_10.jpg' style='vertical-align: top' alt='' /></a></td>")

                    mailbody.Append("<td><img src='https://abcexperiential.com/emailer/asset_management/images/index_11.jpg' style='vertical-align: top' alt='' /></td></tr></table></td></tr></table></body></html>")

                    mail.IsBodyHtml = True
                    mail.Body = mailbody.ToString()

                    '31122019
                    mail.Headers.Add("X-APIHEADER", "KFinkart_" + otp + "_All_123456_" + otp)
                    smtp.Send(mail)
                    emailstatus = mail.DeliveryNotificationOptions
                    If emailstatus = "0" Then
                        'added 22042019
                        Dim Dtinformation As New System.Data.DataTable
                        Dtinformation.Columns.Add("Error_Code")
                        Dtinformation.Columns.Add("Error_Message")
                        Dtinformation.Columns.Add("EncryptionFlag")
                        Dim dr As DataRow
                        dr = Dtinformation.NewRow
                        dr.Item("Error_Code") = "0"
                        dr.Item("Error_Message") = "Success"
                        dr.Item("EncryptionFlag") = "Y"
                        Dtinformation.Rows.Add(dr)
                        dsgetdata.Tables.Add(Dtinformation)

                        If dsgetdata.Tables.Count > 1 Then
                            dsgetdata.Tables(0).TableName = "Dtinformation"
                            dsgetdata.Tables(1).TableName = "DtData"
                        Else
                            dsgetdata.Tables(0).TableName = "Dtinformation"
                        End If
                        Return dsgetdata
                    End If
                End If
                Return dsgetdata
            End Using
        Catch ex As Exception

        Finally
        End Try
        Return dsgetdata
    End Function
    Public Function fnCheckEmail(ByVal email As String) As DataSet
        Dim dsgetdata As New DataSet()
        Try
            Using trxncon As New SqlConnection(kboltconstr)
                Using trxnda As New SqlDataAdapter("ktrack_checkemail_v17", trxncon)
                    trxnda.SelectCommand.CommandType = CommandType.StoredProcedure
                    trxnda.SelectCommand.CommandTimeout = 15000
                    trxnda.SelectCommand.Parameters.AddWithValue("@email", email)
                    trxnda.Fill(dsgetdata)
                End Using
            End Using

            If dsgetdata.Tables.Count > 1 Then
                dsgetdata.Tables(0).TableName = "Dtinformation"
                'dsgetdata.Tables(0).Columns.Add("EncryptionFlag", GetType(String))
                ' dsgetdata.Tables(0).Rows(0).Item("EncryptionFlag") = "Y"
                dsgetdata.Tables(1).TableName = "DtData"

                'dsgetdata.Tables(1).Rows(0).Item("Login_MailID") = obj1.Encode(dsgetdata.Tables(1).Rows(0).Item("Login_MailID").ToString())
            Else
                dsgetdata.Tables(0).TableName = "Dtinformation"
            End If
            Return dsgetdata
        Catch ex As Exception
            'WriteLog(ex.Message, ex.Source, DateTime.Now.ToString(), "", "'" & email & "'")
            'Return GetErrorcode(100, "Error occured while processing the Request")
        Finally
        End Try
        Return dsgetdata
    End Function

    


    Public Function savePurchaseData(ByVal ds As DataSet) As DataSet
        Dim dsgetdata As New DataSet()
        Dim ihno As String = ""
        Try
            Using con As New SqlConnection(kboltconstr)
                ihno = ds.Tables(0).Rows(0)("dd_ihno").ToString()
                'Using dagetdata As New SqlDataAdapter("KTrack_AddPurSave_sri", con)
                Using dagetdata As New SqlDataAdapter("ktrack_Kbolt_NewTrnsxOverMobile_Confirm", con) 'KTrack_AddPurSave_Distadd

                    dagetdata.SelectCommand.CommandType = CommandType.StoredProcedure
                    dagetdata.SelectCommand.Parameters.Add("@Fund", SqlDbType.VarChar, 15).Value = "102"
                    dagetdata.SelectCommand.Parameters.Add("@i_Ihno", SqlDbType.VarChar, 150).Value = ihno
                    dagetdata.SelectCommand.Parameters.Add("@i_TxnReferenceNo", SqlDbType.VarChar, 150).Value = ihno
                    dagetdata.SelectCommand.Parameters.Add("@i_BankID", SqlDbType.VarChar, 150).Value = Convert.ToString(ds.Tables(0).Rows(0)("BANKID"))
                    dagetdata.SelectCommand.Parameters.Add("@i_BankAccno", SqlDbType.VarChar, 150).Value = Convert.ToString(ds.Tables(0).Rows(0)("dd_bnkacno"))
                    'dagetdata.SelectCommand.Parameters.Add("@i_BankID", SqlDbType.VarChar, 150).Value = ""
                    'dagetdata.SelectCommand.Parameters.Add("@i_BankAccno", SqlDbType.VarChar, 150).Value = ""
                    dagetdata.SelectCommand.Parameters.Add("@i_GoalAmount", SqlDbType.VarChar, 150).Value = "0"
                    dagetdata.SelectCommand.Parameters.Add("@i_Goalsip", SqlDbType.VarChar, 150).Value = ""

                    dagetdata.SelectCommand.CommandTimeout = 1000
                    dagetdata.Fill(dsgetdata)
                    'If dsgetdata.Tables.Count > 1 Then
                    '    dsgetdata.Tables(0).TableName = "Dtinformation"
                    '    dsgetdata.Tables(1).TableName = "DtData"
                    'Else
                    '    dsgetdata.Tables(0).TableName = "Dtinformation"
                    'End If
                    'Session("dsconfdata") = dsgetdata
                End Using
            End Using
        Catch ex As Exception
            If Not ex.Message = "Thread was being aborted." Then
                objpayconfirm.WriteLog("", ex.Message, ex.StackTrace, "savepurchaseData()")
            End If
        Finally
        End Try
        If booError Then Response.Redirect("../General/ErrorPage.aspx")
        booError = False
        Return dsgetdata
    End Function


    Public Function KTrack_Generate_Diff_OTPs_EmailOrMob_trans(ByVal fund As String, ByVal trtype As String, MobileNo As String, ByVal Email As String, ByVal Otp As String, ByVal trno As String)

        Try

            'OTP sent to Email

            Dim dsgetdata As New DataSet

            Dim mfdweb As String = Convert.ToString(ConfigurationManager.AppSettings("KARVYMFS"))

            Using trxncon As New SqlConnection(mfdweb)
                Using trxnda As New SqlDataAdapter("ktrack_generate_login_OTP", trxncon)
                    trxnda.SelectCommand.CommandType = CommandType.StoredProcedure
                    trxnda.SelectCommand.CommandTimeout = 15000
                    trxnda.SelectCommand.CommandType = CommandType.StoredProcedure
                    trxnda.SelectCommand.Parameters.Add("@i_UserID", SqlDbType.VarChar, 150).Value = ""
                    trxnda.SelectCommand.Parameters.Add("@i_Password", SqlDbType.VarChar, 150).Value = ""
                    trxnda.SelectCommand.Parameters.Add("@i_ReqBy", SqlDbType.VarChar, 150).Value = trno
                    trxnda.SelectCommand.Parameters.Add("@i_customfund", SqlDbType.VarChar, 150).Value = "102"
                    trxnda.SelectCommand.Parameters.Add("@mobile", SqlDbType.VarChar, 150).Value = MobileNo
                    trxnda.SelectCommand.Parameters.Add("@email", SqlDbType.VarChar, 150).Value = Email
                    'trxnda.SelectCommand.Parameters.Add("@i_OtpNo", SqlDbType.VarChar, 150).Value = Otp
                    'trxnda.SelectCommand.Parameters.Add("@i_Msg", SqlDbType.VarChar, 150).Value = "Dear Investor, Your One Time Password (OTP) for New purchase is " + Otp + " - KFIN MFS"
                    trxnda.Fill(dsgetdata)
                End Using
            End Using
            Dim msg As String = ""
            If dsgetdata.Tables(1).Columns.Contains("msg") Then
                If Convert.ToString(dsgetdata.Tables(1).Rows(0)("msg")) <> "" Then
                    msg = Convert.ToString(dsgetdata.Tables(1).Rows(0)("msg"))
                End If
            End If
            If dsgetdata.Tables(1).Columns.Contains("otp_id") Then
                If Convert.ToString(dsgetdata.Tables(1).Rows(0)("otp_id")) <> "" Then
                    hdntempid.Value = Convert.ToString(dsgetdata.Tables(1).Rows(0)("otp_id"))
                End If
            End If
            Dim relativePath As String
            relativePath = ConfigurationManager.AppSettings("OTPAuthentication").ToString

            Dim reader As StreamReader = New StreamReader(relativePath)

            Dim body As String = String.Empty
            Using reader
                body = reader.ReadToEnd()
            End Using

            If body.Contains("{[msg.]}") Then
                If msg IsNot Nothing AndAlso msg <> "" Then
                    body = body.Replace("{[msg.]}", msg)
                Else
                    body = body.Replace("{[msg.]}", "")
                End If
            End If
            Dim Subject As String = "LIC Mutual Fund One Time Password Authentication"

            sendmailHtml(Email, Subject, Convert.ToString(body), "102", "")

            'dsgetdata = fnEmailOTP_New_CustomFund_trans(Email, msg)
            'If dsgetdata.Tables.Count > 1 Then
            '    dsgetdata.Tables(0).TableName = "Dtinformation"
            '    dsgetdata.Tables(1).TableName = "DtData"
            'Else
            '    dsgetdata.Tables(0).TableName = "Dtinformation"
            'End If
            'Return dsgetdata

        Catch ex As Exception

        End Try

    End Function

    Public Function otpvalidation() As DataSet
        Dim dsSub As New DataSet()
        Try
            Dim trtype As String = hdnTrtype.Value
            Dim Trnno As String = hdntempid.Value
            Dim otp As String = txtotp.Text

            Using otpvalcon As New SqlConnection(strsql_mfs)
                Using otpvalda As New SqlDataAdapter("ktrack_generate_login_OTP_validate", otpvalcon)
                    otpvalda.SelectCommand.CommandType = CommandType.StoredProcedure


                    otpvalda.SelectCommand.Parameters.Add("@otp_id", SqlDbType.VarChar).Value = Trnno


                    otpvalda.SelectCommand.Parameters.Add("@otp", SqlDbType.VarChar).Value = otp
                    otpvalda.SelectCommand.Parameters.Add("@fund", SqlDbType.VarChar).Value = "102"
                    otpvalda.Fill(dsSub)

                    If dsSub.Tables(0).Rows(0)("Error_Code") = "0" Then
                        Return dsSub
                        '    If dsSub.Tables(1).Rows(0)("Msg").ToString <> "OTP matched" Then
                        '        ScriptManager.RegisterStartupScript(Page, Me.GetType(), "OTP Validation", "alert('Please enter Valid OTP')", True)
                        '        btnConfirmpay.Attributes.Add("onclick", "disabled=''")
                        '        Exit Function
                        '    End If
                        'ElseIf dsSub.Tables(0).Rows(0)("Error_Code") = "1" Then

                        '    ScriptManager.RegisterStartupScript(Page, Me.GetType(), "OTP Validation", "alert('Please enter Valid OTP')", True)

                        '    Exit Function
                    End If
                    'End If
                End Using
            End Using
            Return dsSub
        Catch ex As Exception
            If Not ex.Message = "Thread was being aborted." Then
                booError = True
                Session("Error") = ex
            End If
        End Try
        If booError Then Response.Redirect("../General/ErrorPage.aspx")
        booError = False
        Return dsSub
    End Function

#Region "Send mail through HTML"
    Public Sub sendmailHtml(ByVal tomail As String, ByVal subject As String, ByVal Body As String, ByVal fund As String, ByVal ihno As String)
        Try
            Dim strMailServer As String = ""
            Dim strMailUser As String = ""
            Dim strMailPassword As String = ""
            Dim strMailSmtpSvrPort As String = ""
            Dim strMailSendUsing As String = ""
            Dim strMailSmtpAuthne As String = ""
            Dim emailstatus As String = Nothing = ""
            Using ms As MailMessage = New MailMessage()
                tomail = tomail.Replace(" ", "")

                If tomail IsNot Nothing AndAlso tomail <> "" Then
                    ms.[To].Add(tomail)
                    ms.Body = Convert.ToString(Body)
                    ms.IsBodyHtml = True
                    ms.Subject = subject
                End If
                'WriteLog_quant("", "before From", Now)
                ms.From = New MailAddress((Convert.ToString(ConfigurationManager.AppSettings("fromEmailQuant"))), "LIC")
                ms.Bcc.Add(Convert.ToString(ConfigurationManager.AppSettings("Bccaddr")))
                ' ms.Bcc.Add("sravankumar.kota@kfintech.com")
                'ms.Bcc.Add("sindhu.popuri@kfintech.com")
                'WriteLog_quant("", "before mail hdr", Now)
                'ms.Headers.Add(Convert.ToString(ConfigurationManager.AppSettings("mailhdr")), "Fund_" & fund & "Reference NO_" & ihno)
                'WriteLog_quant("", "after mail hdr", Now)
                Dim smtp = New SmtpClient()
                'Dim credential = New NetworkCredential With {
                '    .UserName = Convert.ToString(ConfigurationManager.AppSettings("smtpUserName")),
                '    .Password = Convert.ToString(ConfigurationManager.AppSettings("smtpPassword"))
                '}
                smtp.Host = ConfigurationManager.AppSettings("smtpServer").ToString()
                smtp.Credentials = New NetworkCredential(ConfigurationManager.AppSettings("smtpUserName").ToString(), ConfigurationManager.AppSettings("smtpPassword").ToString())
                smtp.Port = Convert.ToInt32(ConfigurationManager.AppSettings("smtpserverport").ToString())
                smtp.EnableSsl = False
                'WriteLog_quant("", "after smtp", Now)
                'smtp.Credentials = credential
                smtp.Host = ConfigurationManager.AppSettings("Server")
                smtp.Port = Convert.ToInt32(ConfigurationManager.AppSettings("smtpserverport"))
                smtp.EnableSsl = False
                'WriteLog_quant("", "before send", Now)
                smtp.Send(ms)
                'WriteLog_quant("", "after send", Now)
            End Using

        Catch ex As Exception
            'WriteLog_quant("sendmail exception", ex.Message, Now)
            'WriteLog(tomail & vbLf & subject & vbLf & Body & vbLf & Convert.ToString(ex.GetBaseException()), "sendmail()")
        End Try
    End Sub

#End Region
End Class


