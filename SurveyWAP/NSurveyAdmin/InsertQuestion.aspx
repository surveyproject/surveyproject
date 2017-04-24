<%@ Page Language="c#" MasterPageFile="MsterPageTabs.master" ValidateRequest="false"
    AutoEventWireup="false" Inherits="Votations.NSurvey.WebAdmin.InsertQuestion"
    CodeBehind="InsertQuestion.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="HeaderControl" Src="UserControls/HeaderControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FooterControl" Src="UserControls/FooterControl.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="mainBody" class="mainBody contentHolder ps-container">
        <div id="Panel" class="Panel content">

            <div style="position: absolute; width: 50px; text-align: center; margin-left: 700px; top: 15px;">
                <asp:ImageButton ID="ImageButton1" ImageUrl="~/Images/back_button.gif" ToolTip="Click to go Back" runat="server" CssClass="buttonBack" OnCommand="OnBackButton" />
         </div>

            <div style="position: absolute; width: 650px; text-align: center; margin-left: 57px; top: 15px;">
 <asp:Label ID="MessageLabel" runat="server"  CssClass="errorMessage" Visible="False"></asp:Label>
                </div>
            <br />

          <fieldset style="width: 750px; margin-left: 12px; margin-right: 0px; margin-top: 15px;">
                <legend class="titleFont titleLegend">
                                <asp:Literal ID="NewQuestionTitle" runat="server" EnableViewState="False">New question</asp:Literal>
                                                        </legend>
                <br />
                <ol>
                    <li>
                                     <asp:Label runat="server" AssociatedControlID="txtQuestionID" Text="ID"></asp:Label>

                                                    <asp:TextBox runat="server" ID="txtQuestionID"></asp:TextBox>
                                                                    </li>
                    <li>
                                    <asp:Label ID="QuestionLabel" AssociatedControlID="QuestionFreeTextBox" runat="server" EnableViewState="False">Question</asp:Label>
                                                                                           <br /> </li>
                    <li>
                                        <CKEditor:CKEditorControl ID="QuestionFreeTextBox" BasePath="~/Scripts/ckeditor" runat="server">
                                        </CKEditor:CKEditorControl>

                                                                    </li>
                    <li>
                                            <asp:Label ID="TypeLabel" AssociatedControlID="QuestionDropDownList" runat="server" EnableViewState="False">Type</asp:Label>

                                        <asp:DropDownList ID="QuestionDropDownList" runat="server">
                                        </asp:DropDownList>
                                                                                            </li>
                    <li>
                                        <asp:Button ID="AddQuestionButton" CssClass="btn btn-primary btn-xs bw" runat="server" Text="Add new question"></asp:Button>
                        <br />
                                                                                            </li>
                    <li>

                                <asp:Label ID="CopyExistingQuestionTitle" AssociatedControlID="SourceDropDownList" runat="server" EnableViewState="False">Copy existing question</asp:Label>

                                        <asp:DropDownList Width="225" ID="SourceDropDownList" runat="server" AutoPostBack="True">
                                        </asp:DropDownList>
                                                                                                                    </li>
                    <li><div style="text-align:right; margin-right:120px;">
                                        <asp:DropDownList Width="225"  ID="SurveyListDropdownlist" runat="server" AutoPostBack="True" Visible="False">
                                        </asp:DropDownList></div>
                                                                                                                    </li>
                    <li><div style="text-align:right; margin-right:120px;">
                                        <asp:DropDownList Width="225" ID="LibraryDropDownList" runat="server" Visible="False" AutoPostBack="True">
                                        </asp:DropDownList></div>
                                                                                                                    </li>
                    <li><div style="text-align:right; margin-right:120px;">
                                        <asp:DropDownList Width="225" ID="SurveyQuestionListDropdownlist" runat="server" Visible="False">
                                        </asp:DropDownList></div>
                                                                                                                    </li>
                    <li><div style="text-align:right; margin-right:120px;">
                                        <asp:DropDownList Width="225" ID="LibraryQuestionsDropDownList" runat="server" Visible="False">
                                        </asp:DropDownList></div>
                                                                                                                    </li>
                    <li>
                    <asp:Button ID="CopyExistingQuestionButton" CssClass="btn btn-primary btn-xs bw" runat="server" Text="Copy question to this survey" Enabled="False" onclick="CopyExistingQuestionButton_Click1"></asp:Button><br />
                                                                                                                                            </li>
                   

                <asp:PlaceHolder ID="ImportXmlQuestionPlaceHolder" runat="server">
 <li>
                                    <asp:Label ID="ImportQuestionTitle" AssociatedControlID="ImportFile" runat="server" EnableViewState="False" Text="Import XML question"></asp:Label>

                                            <input id="ImportFile" type="file" runat="server"/>

               <asp:Button ID="ImportXMLButton" CssClass="btn btn-primary btn-xs bw" runat="server" Text="Import XML"></asp:Button>
                        <br />
       </li>
                                    </asp:PlaceHolder>
                                                             
                                                        </ol>
            </fieldset>
</div></div>

</asp:Content>