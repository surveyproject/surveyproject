<%@ Register TagPrefix="uc1" TagName="SurveyListControl" Src="UserControls/SurveyListControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FooterControl" Src="UserControls/FooterControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="HeaderControl" Src="UserControls/HeaderControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="SurveyOptionControl" Src="UserControls/SurveyOptionControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="SurveyMessageConditonsControl" Src="UserControls/SurveyMessageConditonsControl.ascx" %>

<%@ Page language="c#" MasterPageFile="MsterPageTabs.master"   ValidateRequest="false" AutoEventWireup="false" Inherits="Votations.NSurvey.WebAdmin.SurveyPrivacy" Codebehind="SurveyPrivacy.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="mainBody" class="mainBody contentHolder ps-container">
        <div id="Panel" class="Panel content">

            <div style="position: absolute; width: 650px; text-align: center; margin-left: 57px; top: 4px;">
 <asp:Label ID="MessageLabel" runat="server"  CssClass="errorMessage" Visible="False"></asp:Label>
                </div>

                                    <div style="position: relative; left: 720px; width: 10px;  top: 13px; clear:none;">
                                            <a onmouseover='this.style.cursor="help" ' onfocus='this.blur();' href="Help/Completion Actions.aspx"
                                                title="Click for more Information">
                                                <img alt="help" border="0" src="<%= Page.ResolveUrl("~")%>Images/small_help.gif" />
                                            </a>
                                        </div>
             
                <fieldset style="width:750px; margin-top:15px; margin-left:12px;"><legend class="titleFont titleLegend">
                                            <asp:Literal ID="PrivacySettingsTitle" runat="server" Text="Privacy settings" EnableViewState="False"></asp:Literal>
                    </legend>

                    <br />

<ol>
     <li>

                                        <strong>
                                            <asp:Label ID="EditionLanguageLabel" runat="server"  AssociatedControlID="LanguagesDropdownlist"></asp:Label></strong>

                                        <asp:DropDownList ID="LanguagesDropdownlist" width="225px" runat="server" AutoPostBack="True">
                                        </asp:DropDownList>
           </li><li>
                                        <strong>
                                            <asp:Label ID="RedirectionURLLabel" runat="server"  AssociatedControlID="RedirectionURLTextBox" EnableViewState="False"></asp:Label></strong>
                 <div id="tooltip">
                                        <asp:TextBox ID="RedirectionURLTextBox" width="225px" runat="server"></asp:TextBox>
                     </div>
  </li><li>
                                        
                                            <asp:Label ID="ThanksMessageLabel" AssociatedControlID="ThankYouCKEditor" runat="server" EnableViewState="False"></asp:Label>
      <br />
  </li><li>
                                   <CKEditor:CKEditorControl ID="ThankYouCKEditor" BasePath="~/Scripts/ckeditor" runat="server">
                                        </CKEditor:CKEditorControl>

                            <br />
                            <asp:Button ID="ApplyPrivacyButton" CssClass="btn btn-primary btn-xs bw" runat="server" Text="Apply changes"></asp:Button>
                            <br />


                 </li>
  </ol>
                    <br />


                    </fieldset>
                       <div style="position: relative; left: 720px; width: 10px;  top: 3px; clear:none;">
                                <a onmouseover='this.style.cursor="help" ' onfocus='this.blur();' href="Help/Thanks Message Conditions.aspx"
                                    title="Click for more Information">
                                    <img alt="help" border="0" src="<%= Page.ResolveUrl("~")%>Images/small_help.gif" />
                                </a>
                            </div>

                <fieldset style="width: 750px; margin-top:15px; margin-left: 12px; margin-top: 10px;"><legend class="titleFont titleLegend">
                                <asp:Literal ID="ThanksMessageConditionTitle" runat="server" Text="Thanks messages display conditions*"
                                    EnableViewState="False"></asp:Literal> </legend>
                    <br />

    <ol>
                <li>
                            <asp:Button ID="AddNewConditionHyperLink" CssClass="btn btn-primary btn-xs bw" runat="server"></asp:Button>
                    <br /><br />

                                        <uc1:SurveyMessageConditonsControl ID="SurveyMessageConditons" runat="server"></uc1:SurveyMessageConditonsControl>
                                        <asp:Label ID="EvaluationMessageConditionInfo" runat="server">Label</asp:Label>
                 </li>
  </ol>
                    <br />
                    </fieldset>



</div></div></asp:Content>
