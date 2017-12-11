<%@ Page Language="c#" MasterPageFile="MsterPageTabs.master" AutoEventWireup="false"
    Inherits="Votations.NSurvey.WebAdmin.ResultsCrossTabulation" CodeBehind="ResultsCrossTabulation.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

        <div id="Panel" class="Panel">

                <script type="text/javascript">
                     <!--
                    function printPreviewDiv(elementId) {
                        var printContent = document.getElementById(elementId);
                        var windowUrl = 'about:blank';
                        var uniqueName = new Date();
                        var windowName = 'Print' + uniqueName.getTime();
                        var printWindow = window.open(windowUrl, windowName, 'left=200,top=50,width=750,height=800,menubar=yes,toolbar=yes,resizable=yes,scrollbars=yes');


                        var printPreviewObject = '<object id="printPreviewElement" width="0" height="0" classid="CLSID:8856F961-340A-11D0-A96B-00C04FD705A2"></object>';

                        printWindow.document.write('<link rel="stylesheet" type="text/css" href="../nsurveyadmin/css/voterreport.css" /> <link href="../Content/bootstrap.min.css" rel="stylesheet" />' + printContent.innerHTML);

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


            <div style="position: absolute; width: 650px; text-align: center; margin-left: 57px; top: 15px;">
 <asp:Label ID="MessageLabel" runat="server"  CssClass="errorMessage" Visible="False"></asp:Label>
                </div>
                                                                <div class="helpDiv">
                                            <a onmouseover='this.style.cursor="help" ' onfocus='this.blur();' href="Help/new/Cross%20Tabulation.aspx"
                                                title="Click for more Information">
                                                <img alt="help" border="0" src="<%= Page.ResolveUrl("~")%>Images/small_help.gif" />
                                            </a>
                                        </div>

            <fieldset>
                 <legend class="titleFont titleLegend">
                    <asp:Literal ID="SurveyCrossTabTitle" runat="server" EnableViewState="False" Text="Survey cross tabulation results"></asp:Literal>
                </legend>
                <br />
                <ol>
                    <li>

                        <asp:RadioButtonList runat="server" ID="rblReports" RepeatDirection="Vertical"
                            CellPadding="10" OnSelectedIndexChanged="rbListSelectedIndexChanged" AutoPostBack="true"
                            Width="700px">
                            <asp:ListItem Text="GraphicalReports" Value="GR"></asp:ListItem>
                            <asp:ListItem Text="CrossTabulationReports" Value="CTR" Selected="True"></asp:ListItem>
                        </asp:RadioButtonList>


                    </li>
                </ol>
                <br />
                </fieldset>
                                     <div class="helpDiv">
                <input type="image" class="PrintImage" alt="print" src="../images/Print_32X32_Standard.png"
                    title="Print" onclick="JavaScript: printPreviewDiv('DivPrint');" />
                            </div>
                     <fieldset>
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
            <div id="fillerDiv" class="fillerDiv">&nbsp;</div>
</div>

</asp:Content>
