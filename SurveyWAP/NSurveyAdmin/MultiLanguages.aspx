<%@ Page language="c#" MasterPageFile="MsterPageTabs.master"   AutoEventWireup="false" Inherits="Votations.NSurvey.WebAdmin.MultiLanguagesPage" Codebehind="MultiLanguages.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

        <div id="Panel" class="Panel">

            <div class="errorDiv">
 <asp:Label ID="MessageLabel" runat="server"  CssClass="errorMessage" Visible="False"></asp:Label>
                </div>

                                        <div class="helpDiv">
                                            <a onmouseover='this.style.cursor="help" ' onfocus='this.blur();' href="Help/Multi-Languages Settings.aspx"
                                                title="Click for more Information">
                                                <img alt="help" border="0" src="<%= Page.ResolveUrl("~")%>Images/small_help.gif" />
                                            </a>
                                        </div>
             
                <fieldset>
                    <legend class="titleFont titleLegend">
                        <asp:Literal ID="MultiLanguagesTitle" runat="server" EnableViewState="False" Text="Multi-Languages settings"></asp:Literal>
                    </legend>
                    <br />
<ol>
     <li>


                                        <strong>
                                            <asp:Label AssociatedControlID="MultiLanguagesCheckBox" ID="EnableMultiLanguagesLabel" runat="server" EnableViewState="False"></asp:Label></strong>

                                        <asp:CheckBox ID="MultiLanguagesCheckBox" runat="server" AutoPostBack="True"></asp:CheckBox>
 </li>


                                <asp:PlaceHolder ID="MultiLanguagesPlaceHolder" Visible="False" runat="server">
<li>
                                            <strong>
                                                <asp:Label ID="MultiLanguagesModeLabel" AssociatedControlID="MultiLanguagesModeDropDownList" runat="server" EnableViewState="False"></asp:Label></strong>

                                            <asp:DropDownList ID="MultiLanguagesModeDropDownList" runat="server" AutoPostBack="True">
                                            </asp:DropDownList>

   </li>
                                    <asp:PlaceHolder ID="VariablePlaceHolder" Visible="false" runat="server">
                                    <li>
                                        <strong>
                                            <asp:Label ID="VariableNameLabel" AssociatedControlID="VariableNameTextBox" runat="server" Visible="False"></asp:Label>
                                        </strong>
                                            <asp:TextBox ID="VariableNameTextBox" runat="server" Visible="False"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                                            <asp:Button ID="VariableNameUpdateButton" CssClass="btn btn-primary btn-xs bw" runat="server" Visible="False" Text="Update"></asp:Button>
                                        <br />
                                    </li>
                                    <li>
                                        <asp:Label ID="VariableNameInfoLabel" runat="server"></asp:Label>
                                    </li>
                                    </asp:PlaceHolder>

                                    <li>
                                            <strong>
                                                <asp:Label ID="EnabledLanguagesLabel" AssociatedControlID="DisabledLanguagesListBox" runat="server" EnableViewState="False" ></asp:Label></strong>

                                            <table >
                                                <tr>
                                                    <td style="width:200px" align="left" valign="top">
                                                        <asp:ListBox  Width="200" Rows="10" ID="DisabledLanguagesListBox" runat="server" AutoPostBack="True"></asp:ListBox>
                                                    </td>
                                                    <td style="width:80px" align="center">
                                                        &gt;&gt;<br />
                                                        &lt;&lt;
                                                        &nbsp;&nbsp;
                                                    </td>
                                                    <td style="width:200px" align="right" valign="top">
                                                        <asp:ListBox Width="200" Rows="10" ID="EnabledLanguagesListBox" runat="server" AutoPostBack="True"></asp:ListBox>
                                                    </td>
                                                </tr>
                                            </table>
           </li><li>
                                            <strong>
                                                <asp:Label ID="DefaultLanguageLabel" AssociatedControlID="DefaultLanguageDropdownlist" runat="server" EnableViewState="False"></asp:Label></strong>

                                            <asp:DropDownList Width="200px" ID="DefaultLanguageDropdownlist" runat="server" AutoPostBack="True">
                                            </asp:DropDownList>
 </li>
           <li><strong><asp:Label ID="MlCodesLabel" AssociatedControlID="MlCodesLiteral" runat="server">Language Codes</asp:Label></strong>
                   <asp:Literal ID="MlCodesLiteral" runat="server" EnableViewState="False"></asp:Literal>
               
           </li>



                                </asp:PlaceHolder>

            
  </ol>
                    <br />
                    </fieldset>

</div></asp:Content>
