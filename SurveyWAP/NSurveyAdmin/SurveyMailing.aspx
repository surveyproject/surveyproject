<%@ Register TagPrefix="uc1" TagName="SurveyListControl" Src="UserControls/SurveyListControl.ascx" %>

<%@ Page language="c#" MasterPageFile="~/NSurveyAdmin/MsterPageTabs.master"   ValidateRequest="false" AutoEventWireup="false" Inherits="Votations.NSurvey.WebAdmin.SurveyMailing" Codebehind="SurveyMailing.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

        <div id="Panel" class="Panel">

                        <div class="errorDiv">
 <asp:Label ID="MessageLabel" runat="server"  CssClass="errorMessage" Visible="False"></asp:Label>
                </div>

                                        <div class="helpDiv">
                                            <a onmouseover='this.style.cursor="help" ' onfocus='this.blur();' href="Help/New/Survey Mailing.aspx"
                                                title="Click for more Information">
                                                <img alt="help" border="0" src="<%= Page.ResolveUrl("~")%>Images/small_help.gif" />
                                            </a>
                                        </div>
                <fieldset><legend class="titleFont titleLegend">
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
                                        <div id="fillerDiv" class="fillerDiv">&nbsp;</div>
</div></asp:Content>