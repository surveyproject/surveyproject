<%@ Register TagPrefix="uc1" TagName="SurveyListControl" Src="UserControls/SurveyListControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FooterControl" Src="UserControls/FooterControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="HeaderControl" Src="UserControls/HeaderControl.ascx" %>

<%@ Page Language="c#" MasterPageFile="MsterPageTabs.master" AutoEventWireup="false"
    Inherits="Votations.NSurvey.WebAdmin.ResultsCrossTabulation" CodeBehind="ResultsCrossTabulation.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="mainBody" class="mainBody contentHolder ps-container">
        <div id="Panel" class="Panel content">

                <script type="text/javascript">
                     <!--
                    function printPreviewDiv(elementId) {
                        var printContent = document.getElementById(elementId);
                        var windowUrl = 'about:blank';
                        var uniqueName = new Date();
                        var windowName = 'Print' + uniqueName.getTime();
                        var printWindow = window.open(windowUrl, windowName, 'left=200,top=50,width=750,height=800,menubar=yes,toolbar=yes,resizable=yes,scrollbars=yes');


                        var printPreviewObject = '<object id="printPreviewElement" width="0" height="0" classid="CLSID:8856F961-340A-11D0-A96B-00C04FD705A2"></object>';

                        printWindow.document.write('<link rel="stylesheet" type="text/css" href="../nsurveyadmin/css/voterreport.css" />' + printContent.innerHTML);

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
                        <div style="position: relative; left: 700px; width: 10px;  top: 15px; clear:none;">
                <input type="image" class="PrintImage" alt="print" src="../images/Print_32X32_Standard.png"
                    title="Print" onclick="JavaScript:printPreviewDiv('DivPrint');" />
                            </div>

            <div style="position: absolute; width: 650px; text-align: center; margin-left: 57px; top: 15px;">
 <asp:Label ID="MessageLabel" runat="server"  CssClass="ErrorMessage" Visible="False"></asp:Label>
                </div>


            <fieldset style="width: 750px; margin-left: 12px; margin-top: 15px;" title="">
                <legend class="titleFont" style="margin: 0px 15px 0 15px; text-align: left;">
                    <asp:Literal ID="SurveyCrossTabTitle" runat="server" EnableViewState="False" Text="Survey cross tabulation results"></asp:Literal>
                </legend>
                <br />
                <ol>
                    <li>

                        <asp:RadioButtonList runat="server" ID="rblReports" RepeatDirection="Vertical"
                            CellPadding="10" OnSelectedIndexChanged="rbListSelectedIndexChanged" AutoPostBack="true"
                            Width="700px">
                            <asp:ListItem Text="GraphicalReports" Value="GR"></asp:ListItem>
                            <asp:ListItem Text="VoterReports" Value="TR"></asp:ListItem>
                            <asp:ListItem Text="CrossTabulationReports" Value="CTR" Selected="True"></asp:ListItem>
                        </asp:RadioButtonList>


                    </li>
                </ol>
                <br />
                </fieldset>

                     <fieldset style="width:750px; margin-left:12px; margin-top:15px;" title="">
                           <br /> <ol>
     <li>
                                            <asp:Label ID="BaseQuestionLabel" runat="server" AssociatedControlID="BaseQuestionDropDownList" ></asp:Label>

                                        <asp:DropDownList ID="BaseQuestionDropDownList" runat="server" AutoPostBack="True">
                                        </asp:DropDownList>
                          </li><li>
                                            <asp:Label ID="CompareQuestionLabel" runat="server" AssociatedControlID="CompareQuestionDropDownList" ></asp:Label>

                                        <asp:DropDownList ID="CompareQuestionDropDownList" runat="server" AutoPostBack="True">
                                        </asp:DropDownList>

                                 </li><li><div class="rounded_corners"><br />
                                                    <asp:Label ID="BaseQuestionChoiceLabel" CssClass="crossTabBaseCell"  runat="server">Choose a base question ...</asp:Label>
                                     <br /><br />
                                                    <asp:Label ID="CompareQuestionChoiceLabel" CssClass="crossTabCompareCell"  runat="server">Choose a compare question ...</asp:Label>
                                     <br /><br /></div>
   </li><li>

                                        <div id="DivPrint">
                                            <div class="rounded_corners">
                                                <asp:PlaceHolder ID="CrossTabResultsPlaceHolder" runat="server"></asp:PlaceHolder>
                                           </div>
                                        </div>
                                            </li>
  </ol>
                    <br />
                    </fieldset>
</div></div>

</asp:Content>
