<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ResultsReport.aspx.cs" Inherits="Votations.NSurvey.WebAdmin.ResultsReport" %>
<%@ Import Namespace = "Votations.NSurvey.Helpers" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Survey Project Results Report</title>

    <meta id="MetaDescription" name="DESCRIPTION" content="Survey Project is a free and open source web based survey and (data entry) forms toolkit for processing & gathering data online." />
    <meta id="MetaKeywords" name="KEYWORDS" content="Survey, Project, Nsurvey, c#, open source, websurvey, surveyform, formbuilder, FWS, Fryslan Webservices, codeplex " />
    <meta id="MetaCopyright" name="COPYRIGHT" content=" 2011 &lt;href='http://www.fryslanwebservices.com'>Fryslan Webservices&lt;/a>" />
    <meta id="MetaGenerator" name="GENERATOR" content="Survey Project" />
    <meta id="MetaAuthor" name="AUTHOR" content="Fryslan Webservices" />
    <meta name="RESOURCE-TYPE" content="DOCUMENT" />
    <meta name="DISTRIBUTION" content="GLOBAL" />
    <meta name="ROBOTS" content="INDEX, FOLLOW" />
    <meta name="REVISIT-AFTER" content="1 DAYS" />
    <meta name="RATING" content="GENERAL" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="PAGE-ENTER" content="RevealTrans(Duration=0,Transition=1)" />

    <meta name="viewport" content="width=device-width, initial-scale=1.0" />

    <!-- Bootstrap -->
    <link href="content/bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="content/bootstrap-theme.min.css" />
    <link href="nsurveyadmin/css/surveymobile.css" type="text/css" rel="stylesheet" />

    <!-- Resultsreport CSS -->
    <link href="~/nsurveyadmin/css/resultsreport.css" type="text/css" rel="stylesheet" />

    <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
      <script src="Scripts/Javascript/HTML5shiv/html5shiv.js"></script>
      <script src="Scripts/respond.min.js"></script>
    <![endif]-->    

    <link rel="SHORTCUT ICON" href="favicon.ico" type="image/x-icon" />
</head>
<body>
        <div class="container">
    <form id="form1" class="form-inline" runat="server">
                    <div class="container panel panel-default" style="margin-top:15px;">

                <div id="DivPrint">

 <script type="text/javascript">
 <!--
    function printPreviewDiv(elementId) {
        var printContent = document.getElementById(elementId);
        var windowUrl = 'about:blank';
        var uniqueName = new Date();
        var windowName = 'Print' + uniqueName.getTime();
        var printWindow = window.open(windowUrl, windowName, 'left=200,top=50,width=750,height=800,menubar=yes,toolbar=yes,resizable=yes,scrollbars=yes');


        var printPreviewObject = '<object id="printPreviewElement" width="0" height="0" classid="CLSID:8856F961-340A-11D0-A96B-00C04FD705A2"></object>';

        printWindow.document.write('<link rel="stylesheet" type="text/css" href="nsurveyadmin/css/voterreport.css" />' + printContent.innerHTML);

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


                   <div style="position: relative; text-align:right; right:85px; top: 40px;">
                   <input type="image" class="PrintImage" alt="print" src="nsurveyadmin/images/Print_32X32_Standard.png" title="Print" onclick="JavaScript: printPreviewDiv('DivPrint');" />
                        </div>

             <fieldset style="width:100%; margin-left:12px; margin-top:15px;" title="">
        <legend class="titleFont" style="margin: 0px 15px 0 15px; text-align:left;">
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

            <!-- jQuery (necessary for Bootstrap's JavaScript plugins) -->
    <script type="text/javascript" src="Scripts/bootstrap.min.js"></script>
</body>
</html>
