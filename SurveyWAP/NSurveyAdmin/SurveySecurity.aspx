<%@ Page language="c#" MasterPageFile="MsterPageTabs.master"   validaterequest="false" AutoEventWireup="false" Inherits="Votations.NSurvey.WebAdmin.SurveySecurity" Codebehind="SurveySecurity.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

        <div id="Panel" class="Panel">

            <div class="errorDiv">
 <asp:Label ID="MessageLabel" runat="server"  CssClass="errorMessage" Visible="False"></asp:Label>
                </div>

                       <div class="helpDiv">
                                            <a onmouseover='this.style.cursor="help" ' onfocus='this.blur();' href="Help/Sec_Introduction.aspx"
                                                title="Click for more Information">
                                                <img alt="help" border="0" src="<%= Page.ResolveUrl("~")%>Images/small_help.gif" />
                                            </a>
                                        </div>
  
     <fieldset>
                
         <legend class="titleFont titleLegend">
                                            <asp:Literal ID="SurveySecurityTitle" runat="server" Text="Survey security" EnableViewState="False"></asp:Literal>
          </legend>
          <br />

             <asp:PlaceHolder ID="SecurityOptionsPlaceHolder" runat="server" Visible="False">
                <ol>
                    <li>
                     <asp:Label ID="UnAuthentifiedUserActionLabel" AssociatedControlID="ActionsDropDownList" runat="server">If user has not been automatically authenticated by all security addins :</asp:Label>

                     <asp:DropDownList ID="ActionsDropDownList" runat="server" AutoPostBack="True"></asp:DropDownList>

                 </li> 
                    <li>
                        <asp:Literal ID="SecurityActionInfoLiteral" runat="server"></asp:Literal>
                    </li>
                </ol>
             </asp:PlaceHolder> 
                  
            <br />
                            <asp:PlaceHolder ID="AddInListPlaceHolder" runat="server"></asp:PlaceHolder>

                  <br />
                    </fieldset>     
    <div id="fillerDiv" class="fillerDiv">&nbsp;</div>
</div>

    <script>
        $(".reveal").on('click', function () {
            var $pwd = $(".pwd");
            if ($pwd.attr('type') === 'password') {
                $pwd.attr('type', 'text');
            } else {
                $pwd.attr('type', 'password');
            }
        });

    </script>

</asp:Content>
