<%@ Page Language="c#" MasterPageFile="MsterPageTabs.master" AutoEventWireup="false"
    Inherits="Votations.NSurvey.WebAdmin.ResultsReporting" CodeBehind="ResultsReporting.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="HeaderControl" Src="UserControls/HeaderControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FooterControl" Src="UserControls/FooterControl.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="mainBody" class="mainBody contentHolder ps-container">
        <div id="Panel" class="Panel content">

    <script type="text/javascript">
        $(function () {

            $("#<%=StartDateTextBox.ClientID%>").datepicker();
            $("#<%=EndDateTextBox.ClientID%>").datepicker();
        });
    </script>

            
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
                    title="Print" onclick="JavaScript: printPreviewDiv('DivPrint');" />
                </div>

               <fieldset style="width:750px; margin-left:12px; margin-top:15px;" title="">
        <legend class="titleFont" style="margin: 0px 15px 0 15px; text-align:left;"><asp:Literal ID="SurveyResultsTitle" runat="server" EnableViewState="False">Survey results</asp:Literal>
        </legend><br />
   <ol>
     <li>
                
                <asp:RadioButtonList runat="server" ID="rblReports" RepeatDirection="Vertical"
                    OnSelectedIndexChanged="rbListSelectedIndexChanged" Width="700" AutoPostBack="true" CellPadding="10">
                    <asp:ListItem Text="GraphicalReports" Value="GR" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="VoterReports" Value="TR"></asp:ListItem>
                    <asp:ListItem Text="CrossTabulationReports" Value="CTR"></asp:ListItem>
                </asp:RadioButtonList>


                          </li>
  </ol>
                    <br />
                    </fieldset>

                     <fieldset style="width:750px; margin-left:12px; margin-top:15px;" title="">
                           <br /> <ol>
     <li>
                                            <asp:Label ID="QuestionsResultsDisplaylabel" runat="server" AssociatedControlID="QuestionsDropDownList">Question's results to display</asp:Label>

                                        <asp:DropDownList ID="QuestionsDropDownList" runat="server" AutoPostBack="True">
                                        </asp:DropDownList>
        </li><li>
                                            <asp:Label ID="ResultsLayoutLabel" runat="server" AssociatedControlID="LayoutDropDownList" >Results layout :</asp:Label>

                                        <asp:DropDownList ID="LayoutDropDownList" runat="server" AutoPostBack="True">
                                        </asp:DropDownList>
              </li><li>
                                            <asp:Label ID="ResultsOrderLabel" runat="server" AssociatedControlID="ResultsOrderDropDownList" >Results order :</asp:Label>

                                        <asp:DropDownList ID="ResultsOrderDropDownList" runat="server" AutoPostBack="True">
                                        </asp:DropDownList>
                    </li><li>
                                            <asp:Label ID="LanguageFilterLabel" runat="server" AssociatedControlID="LanguagesDropdownlist" ></asp:Label>

                                        <asp:DropDownList ID="LanguagesDropdownlist"  runat="server" AutoPostBack="True" Enabled="False">
                                        </asp:DropDownList>
  </li><li>
                                            <asp:Label ID="DateRangeLabel" runat="server" AssociatedControlID="StartDateTextBox" ></asp:Label>

                                        <asp:TextBox ID="StartDateTextBox" runat="server" Width="100px" Columns="12"></asp:TextBox>&nbsp;

                                        <asp:Literal ID="DateToRangeLabel" runat="server" EnableViewState="false" >To</asp:Literal>&nbsp;
                                        <asp:TextBox ID="EndDateTextBox" runat="server" Width="100px" Columns="12"></asp:TextBox>&nbsp;

                                        <asp:Button ID="ApplyRangeButton" CssClass="btn btn-primary btn-xs bw" runat="server" Text="Apply range" Enabled="False">
                                        </asp:Button><br />
        </li><li>
                                            <asp:Label ID="ApplyFilterLabel" runat="server" AssociatedControlID="FilterDropDownList" >Apply a filter :</asp:Label>

                                        <asp:DropDownList ID="FilterDropDownList" runat="server" AutoPostBack="True" OnTextChanged="OnParentFilterChange">
                                        </asp:DropDownList>

                                        <asp:PlaceHolder ID="pnlDropDownList" runat="server"/>
                          </li><li>
                                        <asp:HyperLink ID="FilterEditorHyperLink" runat="server">Click here to edit / create new filters</asp:HyperLink>
              </li>
                                                               
                               
                               <li>
   <div id="DivPrint">
                                        <asp:PlaceHolder ID="ResultsPlaceHolder" runat="server"></asp:PlaceHolder>
                                        <br />
                                        <asp:Repeater ID="ChartRepeater" runat="server">
                                            <ItemTemplate>
                                                <img alt="chartrepeater" class="rounded_corners" Width="100%" src='<%#GetFileName()%>?questionId=<%#DataBinder.Eval(Container.DataItem, "QuestionID")%>&FilterId=<%=GetFilterId().ToString()%>&SortOrder=<%=ResultsOrderDropDownList.SelectedValue%>&LanguageCode=<%=LanguagesDropdownlist.SelectedValue%>&StartDate=<%=StartDateTextBox.Text%>&EndDate=<%=EndDateTextBox.Text%>'>
                                                <br />
                                                <br />
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <asp:Image ID="SingleChartImage" CssClass="rounded_corners" Width="100%" runat="server" Visible="False"></asp:Image>
                                        </div>

                          </li>
  </ol>
                    <br />
                    </fieldset>
</div></div></asp:Content>
