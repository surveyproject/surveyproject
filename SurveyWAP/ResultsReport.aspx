<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ResultsReport.aspx.cs" Inherits="Votations.NSurvey.WebAdmin.ResultsReport" %>
<%@ Import Namespace = "Votations.NSurvey.Helpers" %>

<!DOCTYPE html>
<html>
<head id="Head1" runat="server">
    <title>SurveyProject&trade; ResultsReport</title>
    <meta charset="UTF-8" />
    <meta name="application-name" content="Survey&trade; Project Webapplication ResultsReport" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />

    <meta name="DESCRIPTION" content="SurveyProject&trade;  is a free and open source survey and (data entry) forms webapplication for processing & gathering data online." />
    <meta name="KEYWORDS" content="surveyproject, survey, webform, questionnaire, nsurvey, w3devpro" />
    <meta name="COPYRIGHT" content=" 2017 &lt;href='http://www.w3devpro.com'>W3DevPro&lt;/a>" />
    <meta name="GENERATOR" content="SurveyProject&trade; " />
    <meta name="AUTHOR" content="W3DevPro" />

    <meta name="RESOURCE-TYPE" content="DOCUMENT" />
    <meta name="DISTRIBUTION" content="GLOBAL" />
    <meta name="ROBOTS" content="INDEX, FOLLOW" />
    <meta name="REVISIT-AFTER" content="1 DAYS" />
    <meta name="RATING" content="GENERAL" />

    <!-- IE only -->
    <meta http-equiv="PAGE-ENTER" content="RevealTrans(Duration=0,Transition=1)" />

        <!-- Bootstrap v. 3.3.7 - Package -->
    <link href="Content/bootstrap.min.css" rel="stylesheet" />

    <!-- Resultsreport CSS -->
    <link href="~/nsurveyadmin/css/resultsreport.css" type="text/css" rel="stylesheet" />

    <!-- Part of Bootstrap Installation -->
    <!-- HTML5 shim and Respond.js for IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
        <script src="Scripts/html5shiv.min.js"></script>
        <script src="Scripts/respond.min.js"></script>
    <![endif]--> 

    <link rel="SHORTCUT ICON" href="favicon.ico" type="image/x-icon" />
</head>
<body>
        <div class="container">
    <form id="form1" class="form-inline" runat="server">
                    <div class="container panel panel-default" style="margin-top:15px;">
                       
                   <div style="position: relative; text-align:right; right:85px; top: 40px;">
                   <input type="image" class="PrintImage" alt="print" src="nsurveyadmin/images/Print_32X32_Standard.png" title="Print" onclick="JavaScript: printPreviewDiv('DivPrint');" />
                        </div>
                 

                <div id="DivPrint">

             <fieldset style="width:100%; margin-left:12px; margin-top:15px;" title="">
        <legend class="titleFont titleLegend">
                            <asp:Literal ID="SurveyAnswersTitle" runat="server" EnableViewState="False">Survey Results Report</asp:Literal>
            </legend>

                                    <asp:DataGrid BorderWidth="15" BorderColor="white" ID="QuestionsDataGrid" runat="server" GridLines="None"
                                        ShowHeader="False" AutoGenerateColumns="False" Width="100%">
                                        <Columns>
                                            <asp:TemplateColumn>
                                                <ItemTemplate>
                                                    <br />
                                                    <div class="rrquestion">

                                                        <%#Server.HtmlEncode(System.Text.RegularExpressions.Regex.Replace(new PipeManager().PipeValuesInText(int.Parse(DataBinder.Eval(Container.DataItem,"QuestionId").ToString()),DataBinder.Eval(Container.DataItem,"questionText").ToString(), _voterAnswers.VotersAnswers, null), "<[^>]*>", " "))%>

                                                    </div>
                                                    <br />
                                                    <asp:PlaceHolder ID="matrixplaceholder" runat="server" />
                                                    <asp:PlaceHolder ID="questionanswerplaceholder" runat="server" />
                                                    <b><asp:Label ID="QuestionScoreLabel" runat="server" /></b>
                                                    <br />
                                                
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                        </Columns>
                                    </asp:DataGrid>
                                    <b><asp:Label ID="VoterScoreTotalLabel" BorderWidth="15" BorderColor="white" runat="server"></asp:Label></b>    
         <br />  <br />
            </fieldset>
    </div>    

                    </div>

    </form>
  </div>

    <!-- Bootstrap JS 3.3.7 - package -->
    <script src="Scripts/bootstrap.min.js"></script>
    
 <script>
 <!--
    function printPreviewDiv(elementId) {
        var printContent = document.getElementById(elementId);
        var windowUrl = 'about:blank';
        var uniqueName = new Date();
        var windowName = 'Print' + uniqueName.getTime();
        var printWindow = window.open(windowUrl, windowName, 'left=200,top=50,width=750,height=800,menubar=yes,toolbar=yes,resizable=yes,scrollbars=yes');

        var printPreviewObject = '<object id="printPreviewElement" width="0" height="0" classid="CLSID:8856F961-340A-11D0-A96B-00C04FD705A2"></object>';


            printWindow.document.write('<link rel="stylesheet" type="text/css" href="nsurveyadmin/css/resultsreport.css" />  <link href="Content/bootstrap.min.css" rel="stylesheet" />' + printContent.innerHTML);

            printWindow.document.write(printPreviewObject);

            printWindow.document.write('<script language=JavaScript>');
            printWindow.document.write('var OLECMDID = 6;');
            printWindow.document.write('var PROMPT = 1;');
            printWindow.document.write('printPreviewElement.ExecWB(OLECMDID, PROMPT);');
            printWindow.document.write('printPreviewElement.outerHTML = "";');
            printWindow.document.write('\<\/script>');
            printWindow.document.close();

            printWindow.focus();
            printWindow.print();
            //printWindow.close();


    }
    // -->
 </script>

</body>
</html>
