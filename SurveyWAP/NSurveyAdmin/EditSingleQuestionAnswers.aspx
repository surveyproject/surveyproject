<%@ Register TagPrefix="uc1" TagName="SurveyListControl" Src="UserControls/SurveyListControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FooterControl" Src="UserControls/FooterControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="HeaderControl" Src="UserControls/HeaderControl.ascx" %>
<%@ Page language="c#" MasterPageFile="MsterPageTabs.master" ValidateRequest="false" AutoEventWireup="false" Inherits="Votations.NSurvey.WebAdmin.EditSingleQuestionAnswers" Codebehind="EditSingleQuestionAnswers.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="AnswerOptionControl" Src="UserControls/AnswerOptionControl.ascx" %>

<%@ Register TagPrefix="mbrsc" Namespace="MetaBuilders.WebControls" Assembly="Votations.NSurvey.WebAdmin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="mainBody" class="mainBody contentHolder ps-container">
        <div id="Panel" class="Panel content">

                 <div style="position: absolute; width: 50px; text-align: center; margin-left: 700px; top: 15px;">
            <asp:ImageButton ID="ImageButton1" ImageUrl="~/Images/back_button.gif" runat="server" CssClass="buttonBack" OnCommand="OnBackButton" />
                            </div>

            <div style="position: absolute; width: 650px; text-align: center; margin-left: 57px; top: 15px;">
 <asp:Label ID="MessageLabel" runat="server"  CssClass="ErrorMessage" Visible="False"></asp:Label>
                </div>
            <br />
                        <fieldset style="width: 750px; margin-left: 12px; margin-right: 0px; margin-top: 15px;">
                <legend class="titleFont" style="margin: 0px 15px 0 15px;">
                                            <asp:Literal ID="SingleQuestionAnswerEditorTitle" runat="server" EnableViewState="False"
                                                Text="Question's answers editor"></asp:Literal>
                                    </legend>
                <br />
                <ol>
                    <li>

                                            <asp:Label ID="DisplayAnswersOfLabel" runat="server" AssociatedControlID="QuestionsDropDownList" EnableViewState="False" Text="Display answers of :"></asp:Label>

                                        <asp:DropDownList ID="QuestionsDropDownList"  class="QuestionsDropDownClass" runat="server" AutoPostBack="True">
                                        </asp:DropDownList>

                                        <asp:Button ID="EditQuestionButton" CssClass="btn btn-primary btn-xs bw" runat="server" Text="Edit question"></asp:Button>
                        <br />
                                                    </li>
                        <li>
                                            <asp:Label ID="EditionLanguageLabel" AssociatedControlID="LanguagesDropdownlist" runat="server" Text="Edition language :"></asp:Label>

                                        <asp:DropDownList ID="LanguagesDropdownlist" runat="server" AutoPostBack="True">
                                        </asp:DropDownList>

                                            </li>
                </ol>
            </fieldset>

                <asp:PlaceHolder ID="AnswerOverviewPlaceHolder" runat="server">

                                            <fieldset style="width: 750px; margin-left: 12px; margin-right: 0px; margin-top: 15px;">
                <legend class="titleFont" style="margin: 0px 15px 0 15px;">
                                    <asp:Literal ID="AnswersOverviewTitle" runat="server" Text="Answers overview" EnableViewState="False"></asp:Literal>
                                                        </legend>
                <br />
                <ol>
                    <li>
                        
            <div class="rounded_corners">
                                <asp:DataGrid ID="AnswersDataGrid" runat="server" Border="0" CellPadding="3"  AutoGenerateColumns="False" Width="100%">
                                    <AlternatingItemStyle BackColor="#FFF6BB"></AlternatingItemStyle>
                                    <ItemStyle Font-Size="Small" BackColor="White"></ItemStyle>
                                    <HeaderStyle Font-Size="Small" Font-Bold="True" BackColor="#e2e2e2" BorderColor="#e2e2e2" ForeColor="#5720C6"></HeaderStyle>
                                    <Columns>
                                        <asp:TemplateColumn HeaderText="Order">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="UpImageButton" Width="12" ImageUrl="~/NSurveyAdmin/images/questionupbutton.gif"
                                                    CommandName="up" runat="server"></asp:ImageButton>
                                                <asp:ImageButton ID="DownImageButton" Width="12" ImageUrl="~/NSurveyAdmin/images/questiondownbutton.gif"
                                                    CommandName="down" runat="server"></asp:ImageButton>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Selected">
                                            <ItemTemplate>
                                                <mbrsc:GlobalRadioButton GroupName="GlobalSelected" ID="DefaultRadio" Enabled="False"
                                                    Visible="false" runat="server" />
                                                <asp:CheckBox ID="DefaultCheckBox" Enabled="False" Visible="false" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                         <asp:TemplateColumn HeaderText="Text" >
                                            <ItemTemplate >
                                               <asp:Label runat="server" Text='<%#Eval("AnswerText") %>' ID="lblText" ></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                    
                                        <asp:TemplateColumn HeaderText="Type">
                                            <ItemTemplate>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Rating">
                                            <ItemTemplate>
                                                <asp:Label ID="RatingLabel" runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Score">
                                            <ItemTemplate>
                                                <asp:Label ID="ScorePoint" runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Delete">
                                            <ItemTemplate>
                                                 <asp:ImageButton ImageUrl="~/images/delete.gif" Width="16" runat="server" CommandName="Delete"></asp:ImageButton>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>

                                        <asp:EditCommandColumn HeaderText="Edit" UpdateText="Update" CancelText="Cancel" EditText="<img src='../Images/edit.gif'>" ></asp:EditCommandColumn>

                                    </Columns>
                                </asp:DataGrid>
                </div>
                                                                                 </li>
                        <li>
                                <asp:Button ID="AddNewAnswerButton" CssClass="btn btn-primary btn-xs bw" runat="server" Text="Add new answer"></asp:Button>
                                                                 <br />   </li>
                </ol>
            </fieldset>
                </asp:PlaceHolder>

                <uc1:AnswerOptionControl ID="AnswerOption" runat="server" Visible="False"></uc1:AnswerOptionControl>

</div></div></asp:Content>
