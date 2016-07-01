<%@ Page language="c#" MasterPageFile="MsterPageTabs.master"   AutoEventWireup="false" ValidateRequest="false" Inherits="Votations.NSurvey.WebAdmin.EditRegEx" Codebehind="EditRegEx.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="RolesOptionsControl" Src="UserControls/RolesOptionsControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="HeaderControl" Src="UserControls/HeaderControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FooterControl" Src="UserControls/FooterControl.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="mainBody" class="mainBody contentHolder ps-container">
        <div id="Panel" class="Panel content">
          
            <div style="position: absolute; width: 650px; text-align: center; margin-left: 57px; top: 11px;">
 <asp:Label ID="MessageLabel" runat="server"  CssClass="errorMessage" Visible="False"></asp:Label>
                </div>
  <br />
                                 <fieldset style="width:750px; margin-left:12px; margin-top:19px;" title="">
        <legend class="titleFont" style="text-align:left; margin: 0px 15px 0 15px;"><asp:Literal ID="RegExLibraryTitle" runat="server" Text="RegEx library" EnableViewState="False"></asp:Literal>

        </legend>
                         <br />

  <ol>
     <li>                                

                                            <asp:Label ID="RegExToEditLabel" AssociatedControlID="RegExDropDownList" runat="server"></asp:Label>
                                            <asp:DropDownList ID="RegExDropDownList" runat="server" AutoPostBack="True">
                                            </asp:DropDownList>
                   </li><li>  
                                                <asp:LinkButton ID="CreateRegExHyperLink" runat="server">Click here to create a new regex</asp:LinkButton><br />
                                                [ <asp:HyperLink ID="RegExLibComHyperLink" runat="server" Target="_blank" NavigateUrl="http://www.regexlib.com">Go to http://www.regexlib.com for more regular expressions</asp:HyperLink> ]
                  </li>                                    
                <asp:PlaceHolder ID="RegExOptionsPlaceHolder" Visible="False" runat="server">
<li> 
                                    <asp:Label ID="RegExOptionTitleLabel" runat="server">Label</asp:Label>
    </li><li> 
                                                <asp:Label ID="RegExNameLabel" AssociatedControlID="RegExDescriptionTextbox" runat="server" ></asp:Label>

                                            <asp:TextBox ID="RegExDescriptionTextbox" Width="300" runat="server"></asp:TextBox>
    </li><li> 
                                                <asp:Label ID="RegularExpressionLabel" AssociatedControlID="RegularExpressionTextbox" runat="server" ></asp:Label>

                                            <asp:TextBox ID="RegularExpressionTextbox" runat="server" Width="400" Columns="50" Rows="5" TextMode="MultiLine"></asp:TextBox>
    </li><li> 
                                                <asp:Label ID="RegExErrorMessageLabel" AssociatedControlID="ErrorMessageTextbox" runat="server" ></asp:Label>

                                            <asp:TextBox ID="ErrorMessageTextbox" runat="server" Width="400" Columns="50" Rows="5" TextMode="MultiLine"></asp:TextBox>
    </li><li> 
                    <asp:Button ID="CreateNewRegExButton" CssClass="btn btn-primary btn-xs bw" runat="server" Text="Create regex"></asp:Button>
                    <asp:Button ID="ApplyChangesButton" CssClass="btn btn-primary btn-xs bw" runat="server" Text="Apply changes"></asp:Button>
                    <asp:Button ID="DeleteRegExButton" CssClass="btn btn-primary btn-xs bw" runat="server" Text="Delete regex"></asp:Button>
                    <asp:Button ID="MakeBuiltInRegExButton" CssClass="btn btn-primary btn-xs bw" runat="server" Text="Make built in"></asp:Button>
                    <asp:Button ID="CancelRegExButton" CssClass="btn btn-primary btn-xs bw" runat="server" Text="Cancel"></asp:Button>
        <br />
              </li> 
    </asp:PlaceHolder>
                <li> 
                                <b><asp:Label ID="TestRegExTitle" runat="server">Test a regular expression</asp:Label></b>

                              </li><li>  
                                            <asp:Label ID="RegularExpressionTestLabel" AssociatedControlID="TestExpressionTextbox" runat="server"></asp:Label>

                                        <asp:TextBox ID="TestExpressionTextbox" runat="server" Columns="50" Rows="5" TextMode="MultiLine"></asp:TextBox>
                                            </li><li>  

                                      <asp:Label ID="TestExpressionValueLabel" AssociatedControlID="TestValueTextBox" runat="server"></asp:Label>

                                        <asp:TextBox ID="TestValueTextBox" runat="server" Columns="50" Rows="5" TextMode="MultiLine"></asp:TextBox>
          </li><li>  
                <asp:Button ID="TextRegExButton" CssClass="btn btn-primary btn-xs bw" runat="server" Text="Test expression"></asp:Button><br />
                            </li>
  </ol>
   </fieldset>
</div></div></asp:Content>
