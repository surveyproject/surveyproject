<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DwsReport.aspx.cs" Inherits="Votations.NSurvey.WebAdmin.DwsReport" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<%@ Import Namespace = "Votations.NSurvey.Helpers" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Survey Project Results Report</title>
        <link href="~/nsurveyadmin/css/nsurveyadmin.css" type="text/css" rel="stylesheet" />
</head>
<body>


    <form id="form1" runat="server">
       <div id="mainBody" class="mainBody contentHolder ps-container">
            <div id="Panel" class="Panel content">
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

             <fieldset style="width:100%; margin-left:12px; margin-top:15px; padding-right:12px;" title="">
        <legend class="titleFont" style="margin: 0px 15px 0 15px; text-align:left;">
                            <asp:Literal ID="SurveyAnswersTitle" runat="server" EnableViewState="False">Dierenwetenschap.com - Enqu&#234;te Scores</asp:Literal>
            </legend>

               <div style="position: relative; text-align:right; top: 20px;">
                   <input type="image" class="PrintImage" alt="print" src="nsurveyadmin/images/Print_32X32_Standard.png" title="Print" onclick="JavaScript: printPreviewDiv('DivPrint');" />
                        </div>

                     <table class="voterReport TableLayoutContainer" style="margin-top:-10px;">

                         <tr>
                             <td style="text-align: left; font-size:1.2em; padding:5px;">
                                 <br /><b>Hartelijk dank voor uw deelname en bijdrage aan deze enqu&#234;te.</b><br /><br />De impulsiviteitscores van uzelf en uw kat staan hier onder weergegeven. 
                                 Het diagram toont de gemiddelde impulsiveitscores en de waarde schalen waarmee u uw scores kunt vergelijken.
                                 <br /><br /><br />

                             </td>
                         </tr>

                            <tr>
                                <td> 
                                    <div class="rounded_corners">
                                            <asp:DataGrid ID="DwsReportDataGrid" runat="server" class="voterReport" AllowCustomPaging="True" GridLines="Vertical"
                                                ForeColor="Black" Width="100%"  AlternatingRowStyle-BackColor="#FFF6BB" ShowFooter="True" AllowPaging="False" PageSize="10" FooterStyle-BackColor="#FFDF12" FooterStyle-BorderStyle="None" FooterStyle-BorderColor="#E2E2E2">
                                                <SelectedItemStyle Font-Bold="True" Wrap="True" ForeColor="White" BackColor="#000099">
                                                </SelectedItemStyle>
                                                <EditItemStyle Wrap="False"></EditItemStyle>
                                                <AlternatingItemStyle Wrap="True" ForeColor="Black" BackColor="#F4F9FA"></AlternatingItemStyle>
                                                <ItemStyle Wrap="True" HorizontalAlign="Center" BackColor="white" ForeColor="Black" Font-Size="1.2em">
                                                </ItemStyle>
                                                <HeaderStyle BackColor="#e2e2e2" ForeColor="#5720C6" Font-Size="1.4em" Font-Bold="true"  HorizontalAlign="Center" Wrap="true" Width="25%" BorderWidth="1px" BorderStyle="Solid" BorderColor="#E2E2E2"></HeaderStyle>
                                                <FooterStyle Wrap="True" BackColor="#CCCCCC"></FooterStyle>
                                            </asp:DataGrid>
                                    </div>
                                                                    </td>
                            </tr>
                         <tr>
                             <td style="text-align:center;"><br /><br />

                                 <img src="Images/ImpulseScores1.jpg" style="width:100%;" alt="Impulsiviteits Scores" />

                                 <!--

                                 <asp:Chart ID="Chart1" runat="server" ImageType="Png" ImageLocation="~/Images/mscharts_temp/ChartPic_#SEQ(300,3)"  BackColor="#D3DFF0" Width="512px" Height="396px" BorderColor="26, 59, 105" Palette="BrightPastel" BorderlineDashStyle="Solid" BackGradientStyle="TopBottom" BorderWidth="2">
                                     <Titles>
                                         <asp:Title ShadowColor="32, 0, 0, 0" Font="Trebuchet MS, 14.25pt, style=Bold" ShadowOffset="3" Text="Nonsense Title" Alignment="MiddleLeft" ForeColor="26, 59, 105"></asp:Title>
                                     </Titles>
                                     <Legends>
                                         <asp:Legend Enabled="True" IsTextAutoFit="False" Name="Default" BackColor="Transparent" Font="Trebuchet MS, 8.25pt, style=Bold"></asp:Legend>
                                     </Legends>
                                     <BorderSkin SkinStyle="Emboss"></BorderSkin>

                                     <Series>
                                         <asp:Series Name="Series1" ChartType="Radar"  BorderColor="180, 26, 59, 105" Color="220, 65, 140, 240" XValueType="Double" YValueType="Double" font="Trebuchet MS, 8.25pt, style=Bold"></asp:Series>
                                     </Series>

							        <chartareas>
								        <asp:chartarea Name="ChartArea1" BorderColor="64, 64, 64, 64" BackSecondaryColor="Transparent" BackColor="64, 165, 191, 228" ShadowColor="Transparent" BackGradientStyle="TopBottom">
									        <area3dstyle Rotation="-21" perspective="10" enable3d="True" Inclination="48" IsRightAngleAxes="False" wallwidth="0" IsClustered="False"></area3dstyle>
									        <axisy linecolor="64, 64, 64, 64">
										        <labelstyle font="Trebuchet MS, 8.25pt, style=Bold" />
										        <majorgrid linecolor="64, 64, 64, 64" />
									        </axisy>
									        <axisx linecolor="64, 64, 64, 64">
										        <labelstyle font="Trebuchet MS, 8.25pt, style=Bold" />
										        <majorgrid linecolor="64, 64, 64, 64" />
									        </axisx>
								        </asp:chartarea>
							        </chartareas>
                                 </asp:Chart>

                                     -->


                             </td>
                         </tr>

                        </table>
    
         <br />
            </fieldset> <br /> <br />
    </div></div></div>
    </form>


</body>
</html>
