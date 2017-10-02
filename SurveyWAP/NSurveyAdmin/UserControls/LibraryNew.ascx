<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LibraryNew.ascx.cs"
    Inherits="Votations.NSurvey.WebAdmin.NSurveyAdmin.UserControls.LibraryNew" %>
<%@ Import Namespace="Votations.NSurvey.WebAdmin" %>

            <div class="errorDiv">
 <asp:Label ID="MessageLabel" runat="server"  CssClass="errorMessage" Visible="False"></asp:Label>
                </div>

            <fieldset>
                 <legend class="titleFont titleLegend">
            <asp:Label runat="server" ID="lblLibraryTitle"></asp:Label>
        </legend>
          <br />
                <ol>
                    <li>

            <asp:ValidationSummary ID="valsum" runat="server" />
</li><li>
            <asp:Label runat="server" ID="lblTableName" AssociatedControlID="txtLibName" Text=''></asp:Label>

            <asp:TextBox Width="300" runat="server" ID="txtLibName" />
            <asp:RequiredFieldValidator ID="rvLibName" ControlToValidate="txtLibName" ValidationGroup="LibUpdateValidationGroup"
                ErrorMessage="!" runat="Server">
            </asp:RequiredFieldValidator>
    </li><li>
            <asp:Label runat="server" ID="lblNewDescription" AssociatedControlID="txtLibDescr" Text=''></asp:Label>

            <asp:TextBox Width="300" runat="server" ID="txtLibDescr" />

            </li>
    <asp:Panel ID="pnllanguageSelection" runat="server" Visible="true">
<li>
                <asp:Label AssociatedControlID="lbLangSrc" runat="server" ID="lblLanguageSrc" Text=''></asp:Label>

        <table>
            <tr>
                <td style="width: 200px" align="left" valign="top">
                    <asp:ListBox runat="server" ID="lbLangSrc" OnSelectedIndexChanged="OnAddLang" AutoPostBack="true" Width="200" Height="200" />
                </td>
                <td style="width: 80px" align="center">&gt;&gt;<br />  &lt;&lt;     &nbsp;&nbsp;
                </td>
                <td style="width: 200px" align="right" valign="top">
                    <asp:ListBox runat="server" ID="lbLangEnabled" OnSelectedIndexChanged="OnRemoveLang" AutoPostBack="true" Width="200" Height="200" />
                </td>
            </tr>
        </table>

        </li><li>
                <asp:Label AssociatedControlID="ddlDefaultLang" runat="server" ID="lblDdlLanguage" Text=''></asp:Label>

                <asp:DropDownList runat="server" ID="ddlDefaultLang" Width="200" />
</li>
    </asp:Panel>

    <li>

            <asp:Button CssClass="btn btn-primary btn-xs bw" runat="server" ID="btnAddLib" OnClick="OnAddLibrary" CausesValidation="true" />
            <asp:Button CssClass="btn btn-primary btn-xs bw" runat="server" ID="btnDeleteLib" OnClientClick="return ConfirmLibraryDelete();"
                OnClick="OnDeleteLibrary" Visible="false" />
        <br />
    </li></ol>
   </fieldset>


<script type="text/javascript">
    function ConfirmLibraryDelete() {
        return confirm('<%= ((PageBase)Page).GetPageResource("LibraryTabDeleteConfirm") %>');
    }
</script>
