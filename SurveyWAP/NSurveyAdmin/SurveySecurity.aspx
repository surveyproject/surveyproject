<%@ Register TagPrefix="uc1" TagName="SurveyListControl" Src="UserControls/SurveyListControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FooterControl" Src="UserControls/FooterControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="HeaderControl" Src="UserControls/HeaderControl.ascx" %>
<%@ Page language="c#" MasterPageFile="MsterPageTabs.master"   validaterequest="false" AutoEventWireup="false" Inherits="Votations.NSurvey.WebAdmin.SurveySecurity" Codebehind="SurveySecurity.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="mainBody" class="mainBody contentHolder ps-container">
        <div id="Panel" class="Panel content">

            <div style="position: absolute; width: 650px; text-align: center; margin-left: 57px; top: 15px;">
 <asp:Label ID="MessageLabel" runat="server"  CssClass="ErrorMessage" Visible="False"></asp:Label>
                </div>

                       <div style="position: relative; left: 720px; width: 10px;  top: 13px; clear:none;">
                                            <a onmouseover='this.style.cursor="help" ' onfocus='this.blur();' href="Help/Sec_Introduction.aspx"
                                                title="Click for more Information">
                                                <img alt="help" border="0" src="<%= Page.ResolveUrl("~")%>Images/small_help.gif" />
                                            </a>
                                        </div>
  
     <fieldset style="width: 750px; margin-left: 12px; margin-top: 15px;">
         
         
         <legend class="titleFont" style="margin: 0px 15px 0 15px; text-align:left;">
                                            <asp:Literal ID="SurveySecurityTitle" runat="server" Text="Survey security" EnableViewState="False"></asp:Literal>
          </legend>
          <br />


        


             <asp:PlaceHolder ID="SecurityOptionsPlaceHolder" runat="server" Visible="False">
                <ol>  <li>
                     <asp:Label ID="UnAuthentifiedUserActionLabel" AssociatedControlID="ActionsDropDownList" runat="server">If user has not been automatically authentified by all security addins :</asp:Label>

                     <asp:DropDownList ID="ActionsDropDownList" runat="server" AutoPostBack="True"></asp:DropDownList>

                 </li> 

                </ol>
             </asp:PlaceHolder>

 
                  
            <br />

                            <asp:PlaceHolder ID="AddInListPlaceHolder" runat="server"></asp:PlaceHolder>

                  <br />
                    </fieldset>     


</div></div></asp:Content>
