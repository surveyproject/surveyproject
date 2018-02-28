<%@ Page Language="c#" MasterPageFile="MsterPageTabs.master" AutoEventWireup="false"
    Inherits="Votations.NSurvey.WebAdmin.ResultsReporting" CodeBehind="ResultsReporting.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
        <div id="Panel" class="Panel">

    <script type="text/javascript">
        $(function () {

            var lang2 = '<%=Request.UserLanguages[0].ToString().ToLower()%>';
            var lang = lang2.substring(0,2)

            $("#<%=StartDateTextBox.ClientID%>").datepicker( $.datepicker.regional[lang] );
            $("#<%=EndDateTextBox.ClientID%>").datepicker($.datepicker.regional[lang]);

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

                    printWindow.document.write('<link rel="stylesheet" type="text/css" href="../Content/surveyadmin/voterreport.css" />' + printContent.innerHTML);

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


                                                    <div class="helpDiv">
                                            <a onmouseover='this.style.cursor="help" ' onfocus='this.blur();' href="Help/new/Graphic%20Reports.aspx"
                                                title="Click for more Information">
                                                <img alt="help" border="0" src="<%= Page.ResolveUrl("~")%>Images/small_help.gif" />
                                            </a>
                                        </div>

               <fieldset>
        <legend class="titleFont titleLegend"><asp:Literal ID="SurveyResultsTitle" runat="server" EnableViewState="False">Survey results</asp:Literal>
        </legend><br />
   <ol>
     <li>
                
                <asp:RadioButtonList runat="server" ID="rblReports" RepeatDirection="Vertical"
                    OnSelectedIndexChanged="rbListSelectedIndexChanged" Width="700" AutoPostBack="true" CellPadding="10">
                    <asp:ListItem Text="GraphicalReports" Value="GR" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="CrossTabulationReports" Value="CTR"></asp:ListItem>
                    <asp:ListItem Text="SSRSReports" Value="SSRS"></asp:ListItem>
                </asp:RadioButtonList>
       </li>
  </ol>
                    <br />
                    </fieldset>
                        <div class="helpDiv">
                <input type="image" class="PrintImage" alt="print" src="<%=Page.ResolveUrl("~/images/Print_32X32_Standard.png")%>"
                    title="Print" onclick="JavaScript: printPreviewDiv('DivPrint');" />
                </div>
                     <fieldset>
                           <br /> <ol>
     <li>
                                            <asp:Label ID="QuestionsResultsDisplaylabel" runat="server" AssociatedControlID="QuestionsDropDownList">Question's results to display</asp:Label>

                                        <asp:DropDownList ID="QuestionsDropDownList" runat="server" AutoPostBack="True">
                                        </asp:DropDownList>
        </li><li>
                                            <asp:Label ID="ResultsLayoutLabel" runat="server" AssociatedControlID="LayoutDropDownList" >Results layout :</asp:Label>

                                        <asp:DropDownList ID="LayoutDropDownList" runat="server" AutoPostBack="True">
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="ThreeDeeDropDownList" Width="45" runat="server" Visible="false" AutoPostBack="True">
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
                                                <img alt="chartrepeater" class="rounded_corners" Width="100%" src='<%#GetFileName()%>?questionId=<%#DataBinder.Eval(Container.DataItem, "QuestionID")%>&FilterId=<%=GetFilterId().ToString()%>&SortOrder=<%=ResultsOrderDropDownList.SelectedValue%>&LanguageCode=<%=LanguagesDropdownlist.SelectedValue%>&StartDate=<%=StartDateTextBox.Text%>&EndDate=<%=EndDateTextBox.Text%>&ChartType=<%=LayoutDropDownList.SelectedValue%>&Enable3D=<%=ThreeDeeDropDownList.SelectedValue%>'>
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
                            <div id="fillerDiv" class="fillerDiv">&nbsp;</div>
</div></asp:Content>
