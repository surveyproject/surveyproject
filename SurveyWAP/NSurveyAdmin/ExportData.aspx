<%@ Page Language="c#" MasterPageFile="MsterPageTabs.master" AutoEventWireup="false"
    Inherits="Votations.NSurvey.WebAdmin.ExportData" CodeBehind="ExportData.aspx.cs" %>

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


            <div class="errorDiv">
 <asp:Label ID="MessageLabel" runat="server"  CssClass="errorMessage" Visible="False"></asp:Label>
                </div>
                                                              <div class="helpDiv">
                                            <a onmouseover='this.style.cursor="help" ' onfocus='this.blur();' href="Help/new/Data Export.aspx"
                                                title="Click for more Information">
                                                <img alt="help" border="0" src="<%= Page.ResolveUrl("~")%>Images/small_help.gif" />
                                            </a>
                                        </div>
            
    <fieldset>
        <legend class="titleFont titleLegend">

                                            <asp:Literal ID="SurveyExportTitle" runat="server" EnableViewState="False">Survey CSV export</asp:Literal>
                                </legend><br />
        <ol>
            <li>
                     <asp:Label ID="ExportTypeLabel" AssociatedControlID="ExportDropDownList" runat="server" EnableViewState="False" Text="Export type" />

                                        <asp:DropDownList ID="ExportDropDownList" runat="server" AutoPostBack="True">
                                        </asp:DropDownList>
                                            </li>
            <li>
                                        <asp:Label ID="info1Label" runat="server">: Note: XML format must be chosen to import the data to another Survey installation.</asp:Label>
                                            </li>
        </ol>
   
    </fieldset>


            <asp:PlaceHolder ID="plhCSVStyles" runat="server">
                <fieldset>
                    <legend class="titleFont titleLegend">
                        <asp:Label ID="layoutLabel" runat="server">Export Layout</asp:Label>
                    </legend>
                    <br />
                    <ol>
                        <li>
                            <asp:RadioButton ID="rdStyle2" AutoPostBack="true" GroupName="StylesGroup" runat="server"
                                Text="Export each response as a Row with one Column per possible answer to each Question" />
                             <br /> <br /> <br /> <br />
                            <asp:RadioButton ID="rdStyle1" AutoPostBack="true" GroupName="StylesGroup" Checked="true"
                                runat="server" Text="Export each response as a Row with one Column per Question" />
                             <br /> <br />
                                                    </li>
                        
                            <asp:PlaceHolder ID="plhDdls" runat="server" Visible="true">
<li>
                                <asp:Label ID="HeaderFieldLabel" AssociatedControlID="ddlHeader" runat="server">Header field:</asp:Label>

                                <asp:DropDownList ID="ddlHeader" runat="server">
                                    <asp:ListItem Value="Question">Question</asp:ListItem>
                                    <asp:ListItem Value="QuestionDisplayOrderNumber">Question Display Order Number</asp:ListItem>
                                    <asp:ListItem Value="QuestionID">Question ID</asp:ListItem>
                                    <asp:ListItem Value="QuestionAlias">Question Alias</asp:ListItem>
                                    <asp:ListItem Value="QuestionIDAlias">Question ID Alias</asp:ListItem>
                                </asp:DropDownList>
                           </li>
                        <li>
                                <asp:Label ID="answerFieldLabel" AssociatedControlID="ddlAnswer" runat="server">Answer field:</asp:Label>

                                <asp:DropDownList ID="ddlAnswer" runat="server">
                                    <asp:ListItem Value="Answer">Answer</asp:ListItem>
                                    <asp:ListItem Value="AnswerDisplayOrderNumber">Answer Display Order Number</asp:ListItem>
                                    <asp:ListItem Value="AnswerID">Answer ID</asp:ListItem>
                                    <asp:ListItem Value="AnswerAlias">Answer Alias</asp:ListItem>
                                    <asp:ListItem Value="AnswerIdAlias">Answer Id Alias</asp:ListItem>
                                </asp:DropDownList>
                            <br /> </li>
                            </asp:PlaceHolder>
                           
                       
                    </ol>

                </fieldset>
            </asp:PlaceHolder>

            <asp:PlaceHolder ID="CSVOptionPlaceHolder" runat="server">
                <fieldset>
                    <legend class="titleFont titleLegend">
                        <asp:Literal ID="formatlabel" runat="server" EnableViewState="False" Text="Format"></asp:Literal>
                    </legend>
                    <br />
                    <ol>
                        <li>

                            <asp:Label ID="FieldDelimiterLabel" AssociatedControlID="FieldDelimiterTextBox" runat="server" EnableViewState="False" Text="Field delimiter"></asp:Label>

                            <asp:TextBox ID="FieldDelimiterTextBox" runat="server" Columns="1" MaxLength="1">,</asp:TextBox>
                        </li>
                        <li>
                            <asp:Label ID="TextDelimiterLabel" AssociatedControlID="TextDelimiterTextBox" runat="server" EnableViewState="False">Text delimiter:</asp:Label>

                            <asp:TextBox ID="TextDelimiterTextBox" runat="server" Columns="1" MaxLength="1">&quot;</asp:TextBox>
                        </li>
                        <li>
                            <asp:Label ID="MultiSeperatorLabel" AssociatedControlID="MultiSeparatorTextBox" runat="server" EnableViewState="False">Multiple Choice Separator</asp:Label>

                            <asp:TextBox ID="MultiSeparatorTextBox" Text="#" runat="server" Columns="1" MaxLength="1"></asp:TextBox>

                        </li>
                        <li>
                            <asp:Label ID="ReplaceCarriageLabel" AssociatedControlID="CarriageReturnDropDownList" runat="server" EnableViewState="False">Carriage return:</asp:Label>

                            <asp:DropDownList ID="CarriageReturnDropDownList" runat="server" AutoPostBack="True">
                            </asp:DropDownList>

                            &nbsp;<asp:TextBox ID="CRCharTextbox" runat="server" Visible="False" Columns="1"
                                MaxLength="255"></asp:TextBox>
                            <br />
                        </li>
                    </ol>

                </fieldset>
            </asp:PlaceHolder>

                  <fieldset>
                    <legend class="titleFont titleLegend">
                     <asp:Label runat="server" ID="dataSelectionLabel" Text="Data Selection"></asp:Label>
                    </legend>
                    <br />
                    <ol>
                        <li>
                                        <asp:RadioButton Checked="true" ID="rdAllDates" runat="server" GroupName="DateOption"
                                            Text="all data" /> <br /> <br />

                                        <asp:RadioButton ID="rdSelectedDates" runat="server" GroupName="DateOption" Text="only data in the selected date range" />
                            <br />
                                        </li>
                        <li>

                                            <asp:Label ID="ExportFromDateLabel" AssociatedControlID="StartDateTextBox" runat="server" EnableViewState="False" Text="Export from date :"></asp:Label>

                                        <asp:TextBox ID="StartDateTextBox" runat="server" Columns="8"></asp:TextBox>
                                                    </li>
                        <li>
                                            <asp:Label ID="ExportToDateLabel" AssociatedControlID="EndDateTextBox" runat="server" EnableViewState="False" Text="To date :"></asp:Label>

                                        <asp:TextBox ID="EndDateTextBox" runat="server" Columns="8"></asp:TextBox>
                                                    </li>
                        <li>
                <asp:Button ID="ExportDataButton" CssClass="btn btn-primary btn-xs bw" runat="server" Text="Export CSV"></asp:Button>
                <asp:Button ID="VoterExportXMLButton" CssClass="btn btn-primary btn-xs bw" runat="server" Text="Export XML" Visible="False"></asp:Button>
                <asp:Button ID="ExportDataPdfButton" CssClass="btn btn-primary btn-xs bw" runat="server" Text="Export PDF" />
            <br />
                                                    </li>
                    </ol>

                </fieldset>
                                                 <div id="fillerDiv" class="fillerDiv">&nbsp;</div>
</div></asp:Content>