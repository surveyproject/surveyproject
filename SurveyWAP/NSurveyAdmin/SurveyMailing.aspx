<%@ Register TagPrefix="uc1" TagName="SurveyListControl" Src="UserControls/SurveyListControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FooterControl" Src="UserControls/FooterControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="HeaderControl" Src="UserControls/HeaderControl.ascx" %>

<%@ Page language="c#" MasterPageFile="~/NSurveyAdmin/MsterPageTabs.master"   ValidateRequest="false" AutoEventWireup="false" Inherits="Votations.NSurvey.WebAdmin.SurveyMailing" Codebehind="SurveyMailing.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="mainBody" class="mainBody contentHolder ps-container">
        <div id="Panel" class="Panel content">

                        <div style="position: absolute; width: 650px; text-align: center; margin-left: 57px; top: 15px;">
 <asp:Label ID="MessageLabel" runat="server"  CssClass="ErrorMessage" Visible="False"></asp:Label>
                </div>

         <br />
                <fieldset style="width:750px; margin-top:15px; margin-left:12px;"><legend class="titleFont" style="margin: 0px 15px 0 15px; text-align:left;">
                                            <asp:Literal ID="InvitationMailingTitle" runat="server" EnableViewState="False">Invitation mailing</asp:Literal>
                    </legend>
                    <br />
<ol>
     <li>
                                            <asp:Label ID="FromLabel" AssociatedControlID="FromTextBox" runat="server" EnableViewState="False">From :</asp:Label>

                                        <asp:TextBox ID="FromTextBox" runat="server" Width="280px">youremail@yourdomain.com</asp:TextBox>
     </li><li>
                                            <asp:Label ID="FromNameLabel" AssociatedControlID="FromNameTextbox" runat="server" EnableViewState="False">From name:</asp:Label>

                                        <asp:TextBox ID="FromNameTextbox" runat="server" Width="280px">LastName FirstName</asp:TextBox>
           </li><li>
                                            <asp:Label ID="SubjectLabel" AssociatedControlID="SubjectTextBox" runat="server" EnableViewState="False">Subject :</asp:Label>

                                        <asp:TextBox ID="SubjectTextBox" runat="server" Width="280px">Please take part in my survey</asp:TextBox>
  </li><li>
                                       <div style="text-align:left; ">  <asp:Label ID="InvitationMessageLabel" CssClass="scm" runat="server" >Invitation message :</asp:Label></div>   
        </li><li>
                                        <CKEditor:CKEditorControl ID="MailingCKEditor" BasePath="~/Scripts/ckeditor" runat="server">
                                        </CKEditor:CKEditorControl>
 
      </li><li>
                                       <div style="text-align:left; clear:right; width:100%;">
                                            <asp:Label ID="EmailInvitationLabel" CssClass="scm" runat="server" EnableViewState="False">Email invitation list (must be separated by a comma eg: email@dom.com, email2@dom.com etc..) :</asp:Label>
                                           </div>
          </li><li>
                                        <asp:TextBox ID="MailingListTextBox" Width="100%" runat="server" TextMode="MultiLine" Rows="2" Columns="80"></asp:TextBox>

      </li><li>                      
                            <asp:Label ID="AnonymousLabel" AssociatedControlID="AnonymousEntriesCheckBox" runat="server" EnableViewState="False">Anonymous entries:</asp:Label>

                                        <asp:CheckBox ID="AnonymousEntriesCheckBox" runat="server"></asp:CheckBox>
          </li><li>
                            <asp:Button ID="SendInvitationButton" CssClass="btn btn-primary btn-xs bw" runat="server" Text="Send invitation"></asp:Button> 
              <br />
                 </li>
  </ol>
                    <br />
                    </fieldset>
</div></div></asp:Content>