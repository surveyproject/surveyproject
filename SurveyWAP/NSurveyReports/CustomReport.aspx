<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CustomReport.aspx.cs" Inherits="Votations.NSurvey.WebAdmin.CustomReport" %>

<%@ Import Namespace = "Votations.NSurvey.Helpers" %>
<%@ Import Namespace = "Votations.NSurvey.WebControls" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Survey&trade; Project Custom Results Report</title>

            <!-- Bootstrap Package -->
    <link href="../Content/bootstrap.min.css" rel="stylesheet" />

    <!-- Resultsreport CSS -->
    <link href='../Content/surveyform/resultsreport.css' type="text/css" rel="stylesheet" />

    <!-- Part of Bootstrap Installation -->
    <!-- HTML5 shim and Respond.js for IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
        <script src="Scripts/html5shiv.min.js"></script>
        <script src="Scripts/respond.min.js"></script>
    <![endif]--> 
</head>
<body>

        <div class="container">
    <form id="form1" class="form-inline" runat="server">
                    <div class="container panel panel-default" style="margin-top:15px;">
                       
                   <div style="position: relative; text-align:right; right:85px; top: 40px;">
                   <input type="image" class="PrintImage" alt="print" src="../Images/Print_32X32_Standard.png" title="Print" onclick="JavaScript: printPreviewDiv('DivPrint');" />
                        </div>
                 

                <div id="DivPrint">

             <fieldset style="width:100%; margin-left:12px; margin-top:15px;" title="">
        <legend class="titleFont titleLegend">
                            <asp:Literal ID="SurveyAnswersTitle" runat="server" EnableViewState="False">Survey Results Report</asp:Literal>
            </legend>
                 <br /><br />

                                            <asp:DataGrid ID="CustomReportDataGrid" runat="server" class="" AllowCustomPaging="True" GridLines="Vertical"
                                                ForeColor="Black" Width="100%"  AlternatingRowStyle-BackColor="#FFF6BB" ShowHeader="true" ShowFooter="False" AllowPaging="False" PageSize="10" FooterStyle-BackColor="#FFDF12" FooterStyle-BorderStyle="None" FooterStyle-BorderColor="#E2E2E2">
                                                <SelectedItemStyle Font-Bold="True" Wrap="True" ForeColor="White" BackColor="#000099">
                                                </SelectedItemStyle>
                                                <EditItemStyle Wrap="False"></EditItemStyle>
                                                <AlternatingItemStyle Wrap="True" ForeColor="Black" BackColor="#F4F9FA"></AlternatingItemStyle>
                                                <ItemStyle Wrap="True" HorizontalAlign="Center" BackColor="white" ForeColor="Black" Font-Size="1.2em">
                                                </ItemStyle>
                                                <HeaderStyle BackColor="#e2e2e2" ForeColor="#5720C6" Font-Size="1.4em" Font-Bold="true"  HorizontalAlign="Center" Wrap="true" Width="25%" BorderWidth="1px" BorderStyle="Solid" BorderColor="#E2E2E2"></HeaderStyle>
                                                <FooterStyle Wrap="True" BackColor="#CCCCCC"></FooterStyle>
                                            </asp:DataGrid>

         <br />  <br />
            </fieldset>
             </div>    </div> 

         </form>    

        </div>



   <!-- Bootstrap JS package -->
    <script src="../Scripts/bootstrap.min.js"></script>

    <!-- Print report -->
     <script type="text/javascript">
 <!--
    function printPreviewDiv(elementId) {
        var printContent = document.getElementById(elementId);
        var windowUrl = 'about:blank';
        var uniqueName = new Date();
        var windowName = 'Print' + uniqueName.getTime();
        var printWindow = window.open(windowUrl, windowName, 'left=200,top=50,width=750,height=800,menubar=yes,toolbar=yes,resizable=yes,scrollbars=yes');


        var printPreviewObject = '<object id="printPreviewElement" width="0" height="0" classid="CLSID:8856F961-340A-11D0-A96B-00C04FD705A2"></object>';

        printWindow.document.write('<link rel="stylesheet" type="text/css" href="../Content/surveyform/resultsreport.css" />' + printContent.innerHTML);

        printWindow.document.write(printPreviewObject);

        printWindow.document.write('<script language=JavaScript>');
        printWindow.document.write('var OLECMDID = 6;');
        printWindow.document.write('var PROMPT = 1;');
        printWindow.document.write('printPreviewElement.ExecWB(OLECMDID, PROMPT);');
        printWindow.document.write('printPreviewElement.outerHTML = "";');
        printWindow.document.write('\<\/script>');

        printWindow.document.close();
        //printWindow.focus();
        printWindow.print();
        //printWindow.close();
    }
    // -->
 </script>

</body>
</html>
