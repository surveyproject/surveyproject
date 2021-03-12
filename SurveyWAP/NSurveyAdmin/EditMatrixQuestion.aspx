<%@ Page Language="c#" MasterPageFile="~/NSurveyAdmin/MsterPageTabs.master" AutoEventWireup="false"
    ValidateRequest="false" Inherits="Votations.NSurvey.WebAdmin.EditMatrixQuestion"
    CodeBehind="EditMatrixQuestion.aspx.cs" %>

<%@ Register src="UserControls/QuestionExtraLinks.ascx" tagname="QuestionExtraLinks" tagprefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <script type="text/javascript">
        $(document).ready(function () {

            var scroll = document.getElementById("DivRDGrid");
            scroll.scrollIntoView();
        });
    </script>


        <div id="Panel" class="Panel">

            
            <fieldset>

                <asp:ImageButton ID="ImageButton1" Width="16px" ImageUrl="~/Images/back_button.gif" runat="server"
                    CssClass="buttonBack" OnCommand="OnBackButton" />
                <div style="left: 100px; text-align:center; width:450px; position: relative; top: 5px;">
                    <uc2:QuestionExtraLinks ID="QuestionExtraLinks1" runat="server" /></div>
                                                           <div class="helpDiv">
                                            <a onmouseover='this.style.cursor="help" ' onfocus='this.blur();' href="Help/new/Matrix Question Editor.aspx"
                                                title="Click for more Information">
                                                <img alt="help" border="0" src="<%= Page.ResolveUrl("~")%>Images/small_help.gif" />
                                            </a>
                                        </div>
                
            </fieldset>

           <div class="errorDiv">
 <asp:Label ID="MessageLabel" runat="server"  CssClass="errorMessage" Visible="False"></asp:Label>
                </div>
              
                            <fieldset>
                                <legend class="titleFont titleLegend">
                                <asp:Literal ID="EditMatrixQuestionTitle" runat="server" EnableViewState="False">Edit matrix question</asp:Literal>
                                                    </legend>
                    <br />
<ol>
     <li>
                                        <CKEditor:CKEditorControl ID="QuestionFreeTextBox" BasePath="~/Scripts/ckeditor" runat="server">
                                        </CKEditor:CKEditorControl>
               </li><li>   

                                            <asp:Label ID="EditionLanguageLabel" AssociatedControlID="LanguagesDropdownlist" runat="server" Text="Edition language :"></asp:Label>
                                        <asp:DropDownList ID="LanguagesDropdownlist" runat="server" AutoPostBack="True">
                                        </asp:DropDownList>
                   <br /><br />
                                            <asp:Label ID="IDLabel" AssociatedControlID="txtQuestionID" runat="server" EnableViewState="False">ID:</asp:Label>
                                        <asp:TextBox runat="server" ID="txtQuestionID"></asp:TextBox>
                   <br /><br />
                                            <asp:Label ID="AliasLabel" AssociatedControlID="txtAlias" runat="server" EnableViewState="False">Alias:</asp:Label>
                                        <asp:TextBox runat="server" ID="txtAlias"></asp:TextBox>
                   <br /><br />
                                            <asp:Label ID="HelpTextLabel" AssociatedControlID="txtHelpText" runat="server" EnableViewState="False">Help Text:</asp:Label>
                                        <asp:TextBox runat="server" ID="txtHelpText" TextMode="MultiLine" Width="315"></asp:TextBox>
                   <br /><br />
                                            <asp:Label ID="ShowHelpTextLabel" AssociatedControlID="chbShowHelpText" runat="server" EnableViewState="False">Show Help Text:</asp:Label>
                                        <asp:CheckBox runat="server" ID="chbShowHelpText"></asp:CheckBox>

                   <br /><br />
                                            <asp:Label ID="MultipleChoicesMatrixLabel" AssociatedControlID="MultipleChoiceCheckbox" runat="server" EnableViewState="False">Multiple choices matrix:</asp:Label>
                                        <asp:CheckBox ID="MultipleChoiceCheckbox" runat="server"></asp:CheckBox>
                     <br /><br />
                                            <asp:Label ID="AnswerRatingLabel" AssociatedControlID="RatingCheckbox" runat="server" EnableViewState="False">Enable answer rating:</asp:Label>
                                        <asp:CheckBox ID="RatingCheckbox" AutoPostBack="true" runat="server"></asp:CheckBox>
                     <br /><br />
                                       <asp:Label ID="GroupLabel" AssociatedControlID="ddlGroup" runat="server" EnableViewState="False">Group:</asp:Label>
                                            <asp:DropDownList runat="server" ID="ddlGroup" AutoPostBack="true" AppendDataBoundItems="true" OnSelectedIndexChanged="OnddlGroup_SelectedIndexChanged">
                                            </asp:DropDownList>
                     <br /><br />
                                    <asp:Label ID="SubGroupLabel" AssociatedControlID="ddlSubGroup" runat="server"  EnableViewState="False">Sub Group:</asp:Label>
                                        <asp:DropDownList runat="server" AppendDataBoundItems="true" ID="ddlSubGroup">
                                        </asp:DropDownList>
                     <br /><br />

                                            <asp:Label ID="AllowMultipleMatrixSectionsLabel" AssociatedControlID="RepeatSectionCheckbox" runat="server" EnableViewState="False"
                                                Text="Repeatable matrix section :"></asp:Label>
                                        <asp:CheckBox ID="RepeatSectionCheckbox" runat="server" AutoPostBack="True"></asp:CheckBox>

                     <br /><br />

                                            <asp:Label ID="MinSelectionMatrixLabel" AssociatedControlID="MinSelectionDropDownList" runat="server" EnableViewState="False"></asp:Label>
                                        <asp:DropDownList ID="MinSelectionDropDownList" runat="server">
                                            <asp:ListItem Value="1">1</asp:ListItem>
                                            <asp:ListItem Value="2">2</asp:ListItem>
                                            <asp:ListItem Value="3">3</asp:ListItem>
                                            <asp:ListItem Value="4">4</asp:ListItem>
                                            <asp:ListItem Value="5">5</asp:ListItem>
                                            <asp:ListItem Value="6">6</asp:ListItem>
                                            <asp:ListItem Value="7">7</asp:ListItem>
                                            <asp:ListItem Value="8">8</asp:ListItem>
                                            <asp:ListItem Value="9">9</asp:ListItem>
                                            <asp:ListItem Value="10">10</asp:ListItem>
                                            <asp:ListItem Value="11">11</asp:ListItem>
                                            <asp:ListItem Value="12">12</asp:ListItem>
                                            <asp:ListItem Value="13">13</asp:ListItem>
                                            <asp:ListItem Value="14">14</asp:ListItem>
                                            <asp:ListItem Value="15">15</asp:ListItem>
                                            <asp:ListItem Value="16">16</asp:ListItem>
                                            <asp:ListItem Value="17">17</asp:ListItem>
                                            <asp:ListItem Value="18">18</asp:ListItem>
                                            <asp:ListItem Value="19">19</asp:ListItem>
                                            <asp:ListItem Value="20">20</asp:ListItem>
                                        </asp:DropDownList>

                     <br /><br />

                                            <asp:Label ID="MaxSelectionMatrixAllowedLabel" AssociatedControlID="MaxAllowedDropDownList" runat="server" EnableViewState="False"></asp:Label>
                                        <asp:DropDownList ID="MaxAllowedDropDownList" runat="server">
                                            <asp:ListItem Value="1">1</asp:ListItem>
                                            <asp:ListItem Value="2">2</asp:ListItem>
                                            <asp:ListItem Value="3">3</asp:ListItem>
                                            <asp:ListItem Value="4">4</asp:ListItem>
                                            <asp:ListItem Value="5">5</asp:ListItem>
                                            <asp:ListItem Value="6">6</asp:ListItem>
                                            <asp:ListItem Value="7">7</asp:ListItem>
                                            <asp:ListItem Value="8">8</asp:ListItem>
                                            <asp:ListItem Value="9">9</asp:ListItem>
                                            <asp:ListItem Value="10">10</asp:ListItem>
                                            <asp:ListItem Value="11">11</asp:ListItem>
                                            <asp:ListItem Value="12">12</asp:ListItem>
                                            <asp:ListItem Value="13">13</asp:ListItem>
                                            <asp:ListItem Value="14">14</asp:ListItem>
                                            <asp:ListItem Value="15">15</asp:ListItem>
                                            <asp:ListItem Value="16">16</asp:ListItem>
                                            <asp:ListItem Value="17">17</asp:ListItem>
                                            <asp:ListItem Value="18">18</asp:ListItem>
                                            <asp:ListItem Value="19">19</asp:ListItem>
                                            <asp:ListItem Value="20">20</asp:ListItem>
                                        </asp:DropDownList>


                                <asp:PlaceHolder ID="RepeatSectionOptionPlaceholder" runat="server">
                                      <br /><br /> <br /><br />
                                                <asp:Label ID="AddRepeatSectionLabel" AssociatedControlID="AddSectionLinkTextBox" runat="server">Add section link text:</asp:Label>
                                            <asp:TextBox ID="AddSectionLinkTextBox" runat="server"></asp:TextBox>
                                      <br /><br />
                                                <asp:Label ID="DeleteRepeatSectionLabel" AssociatedControlID="DeleteSectionLinkTextBox" runat="server">Delete section link text :</asp:Label>
                                            <asp:TextBox ID="DeleteSectionLinkTextBox" runat="server"></asp:TextBox>
                                      
                                </asp:PlaceHolder>
                    <br /><br />
                         </li><li>   

    <asp:Button ID="UpdateQuestionButton" CssClass="btn btn-primary btn-xs bw" runat="server" Text="Update"></asp:Button>
    <asp:Button ID="ExportXMLButton" CssClass="btn btn-primary btn-xs bw" runat="server" Text="Export XML"></asp:Button>
    <br />

                 </li>
  </ol>
                    <br />
                    </fieldset>

            <div class="helpDiv">
                <a onmouseover='this.style.cursor="help" ' onfocus='this.blur();' href="Help/New/Matrix Rows _ Column Editor.aspx"
                    title="Click for more Information">
                    <img alt="help" border="0" src="<%= Page.ResolveUrl("~")%>Images/small_help.gif" />
                </a>
            </div>
            <fieldset>
                <legend class="titleFont titleLegend">
                    <asp:Literal ID="InsertNewTitle" runat="server" EnableViewState="False">Insert a new</asp:Literal>
                </legend>
                <br />
                <ol>
                    <li>

                        <asp:Label ID="RowLabel" AssociatedControlID="NewRowTextBox" runat="server" EnableViewState="False">Row </asp:Label>

                        <asp:TextBox ID="NewRowTextBox" Width="350" runat="server"></asp:TextBox>

                        <asp:Button ID="AddRowButton" CssClass="btn btn-primary btn-xs bw" runat="server" Text="Add row"></asp:Button>
                    </li>
                    <li>
                        <asp:Label ID="ColumnLabel" AssociatedControlID="NewColumnTextBox" runat="server" EnableViewState="False">Column </asp:Label>

                        <asp:TextBox ID="NewColumnTextBox" Width="150" runat="server"></asp:TextBox>
                        <asp:DropDownList ID="AnswerTypeDropDownList" Width="25%" runat="server">
                        </asp:DropDownList>

                        <asp:Button ID="AddColumnButton" CssClass="btn btn-primary btn-xs bw" runat="server" Text="Add column"></asp:Button>
                    </li>
                </ol>
                <br />
            </fieldset>

            <fieldset>
                <legend class="titleFont titleLegend">
                    <asp:Literal ID="CurrentRowsColumnsTitle" runat="server" EnableViewState="False">Current rows and columns</asp:Literal>
                </legend>
                <br />
                <ol>
                    <li>

                        <asp:Label ID="ChildsEditionLanguageLabel" AssociatedControlID="ChildsLanguageDropdownlist" runat="server" Text="Edition language :"></asp:Label>
                        <asp:DropDownList ID="ChildsLanguageDropdownlist" runat="server" AutoPostBack="True">
                        </asp:DropDownList>
                    </li>
                    <li>
                        <div id="DivRDGrid" class="rounded_corners">
                            <asp:DataGrid ID="RowsDataGrid" runat="server" AutoGenerateColumns="False" Width="100%"
                                CellPadding="3" border="0">
                                <AlternatingItemStyle HorizontalAlign="Left" BackColor="#F4F9FA"></AlternatingItemStyle>
                                <ItemStyle HorizontalAlign="Left" BackColor="white" Font-Size="small"></ItemStyle>
                                <HeaderStyle BackColor="#e2e2e2" BorderColor="#e2e2e2" ForeColor="#5720C6" Font-Size="xx-small" Font-Bold="True"></HeaderStyle>
                                <Columns>
                                    <asp:BoundColumn DataField="QuestionText" HeaderText="Rows"></asp:BoundColumn>
                                    <asp:EditCommandColumn ButtonType="LinkButton" HeaderText="Edit" UpdateText="Update" CancelText="Cancel" EditText="<img src='../Images/edit.gif'>"></asp:EditCommandColumn>
                                    <asp:ButtonColumn HeaderText="Delete" Text="<img src='../Images/delete.gif'>" CommandName="Delete"></asp:ButtonColumn>
                                </Columns>
                            </asp:DataGrid>
                        </div>
                    </li>
                    <li>
                        <div class="rounded_corners">
                            <asp:DataGrid ID="ColsDataGrid" runat="server" border="0" CellPadding="3" Width="100%"
                                AutoGenerateColumns="False">
                                <AlternatingItemStyle BackColor="#F4F9FA"></AlternatingItemStyle>
                                <ItemStyle BackColor="white" Font-Size="small"></ItemStyle>
                                <HeaderStyle BackColor="#e2e2e2" BorderColor="#e2e2e2" ForeColor="#5720C6" Font-Size="xx-small" Font-Bold="True"></HeaderStyle>
                                <Columns>
                                    <asp:BoundColumn DataField="AnswerText" HeaderText="Columns"></asp:BoundColumn>
                                    <asp:TemplateColumn HeaderText="Type">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="TypesDropDownList" runat="server" Enabled="False">
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:DropDownList ID="TypesEditDropDownList" runat="server">
                                            </asp:DropDownList>
                                        </EditItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Rating">
                                        <ItemTemplate>
                                            <asp:Label ID="RatingLabel" runat="server"></asp:Label><asp:CheckBox ID="RatingPartCheckBox"
                                                runat="server" Enabled="False"></asp:CheckBox>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Mandatory">
                                        <ItemTemplate>
                                            <asp:Label ID="MandatoryLabel" runat="server"></asp:Label><asp:CheckBox ID="MandatoryCheckbox"
                                                runat="server" Enabled="False"></asp:CheckBox>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:EditCommandColumn HeaderText="Edit" ButtonType="LinkButton" UpdateText="Update" CancelText="Cancel"
                                        EditText="<img src='../Images/edit.gif'>"></asp:EditCommandColumn>
                                    <asp:ButtonColumn HeaderText="Delete" Text="<img src='../Images/delete.gif'>" CommandName="Delete"></asp:ButtonColumn>
                                </Columns>
                            </asp:DataGrid>
                        </div>
                    </li>
                </ol>
                <br />
            </fieldset>
                                                 <div id="fillerDiv" class="fillerDiv">&nbsp;</div>
</div></asp:Content>