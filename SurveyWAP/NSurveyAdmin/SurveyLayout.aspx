<%@ Page Title="" Language="C#" MasterPageFile="~/NSurveyAdmin/MsterPageTabs.master"
    AutoEventWireup="true" CodeBehind="SurveyLayout.aspx.cs" Inherits="Votations.NSurvey.WebAdmin.NSurveyAdmin.SurveyLayout" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<%--    <script type="text/javascript">

        $(function () {
            $('#<%=fuCss.ClientID %>').fileinput();
            $('.fileinput-button').removeClass('ui-state-default ui-widget-header ui-corner-right').append($('<input type="button" value="Upload"/>'));
            $('.fileinput-wrapper').removeClass('ui-widget').unbind('mouseenter mouseleave');
            $('.fileinput-input').removeClass('ui-state-default ui-widget-content ui-corner-left');
            $('.fileinput-button-text').remove();

            $('#<%=fuCss.ClientID %>').change(function () {
                __doPostBack('<%=btnCssUpload.UniqueID %>', '');
            });

        });
        
    </script>--%>


        <div id="mainBody" class="mainBody contentHolder ps-container">
        <div id="Panel" class="Panel content">

            <div style="position: absolute; width: 650px; text-align: center; margin-left: 57px; top: 15px;">
 <asp:Label ID="MessageLabel" runat="server"  CssClass="ErrorMessage" Visible="False"></asp:Label>
                </div>
            
                <div style="position: relative; left: 720px; width:10px; top: 13px;">
                    <a onmouseover='this.style.cursor="help" ' onfocus='this.blur();' href="Help/Style_Introduction.aspx"
                        title="Click for more Information">
                        <img alt="help" border="0" src="<%= Page.ResolveUrl("~")%>Images/small_help.gif" />
                    </a>
                </div>
            
       <fieldset style="width:750px; margin-left:12px; margin-top:15px;">
        <legend class="titleFont" style="margin: 0px 15px 0 15px; text-align:left;">
          <asp:Literal ID="SurveyLayoutLegend" runat="server" EnableViewState="false">Survey Layout</asp:Literal>  
        </legend><br />

 <ol>
     <li>
                    <asp:Label ID="CssLabel" AssociatedControlID="ddlTemplate" runat="server"></asp:Label>
                <asp:DropDownList runat="server" ID="ddlTemplate" Width="400px" AppendDataBoundItems="True">
                </asp:DropDownList>

</li><li>   
        
                            <asp:FileUpload Width="225"  runat="server" ID="fuCss"/>
                            <asp:Button Width="100" Height="23" CssClass="btn btn-primary btn-xs bw" runat="server" Text="Upload" ID="btnCssUpload" OnCommand="OnCssFileUpload" />
    </li><li> 
                            <asp:Button CssClass="btn btn-primary btn-xs bw" runat="server" ID="btnCssDownload" Text="Download" OnClick="btnCssDownload_Click" />

                            <asp:Button CssClass="btn btn-primary btn-xs bw" runat="server" ID="btnCssDelete" Text="Delete" OnClick="btnCssDelete_Click" />

                            <asp:Button CssClass="btn btn-primary btn-xs bw" runat="server" ID="btnCssEdit" Text="Edit" OnClick="btnCssEdit_Click" />
        <br />
        </li><li>
                                <asp:Label AssociatedControlID="LanguagesDropdownlist" ID="EditionLanguageLabel" runat="server"></asp:Label>

                            <asp:DropDownList ID="LanguagesDropdownlist" runat="server" 
                                AutoPostBack="True" 
                                onselectedindexchanged="LanguagesDropdownlist_SelectedIndexChanged1">
                            </asp:DropDownList>

            </li>
            <!--
                    <asp:TextBox runat="server" ID="tctEditCxSS" Wrap="true" TextMode="MultiLine" Rows="10" />
            -->

    <asp:Panel ID="pnlHdrFooter" runat="server">
<li>

                        <asp:Label ID="HeaderLabel" AssociatedControlID="txtPageHeader" runat="server"></asp:Label>
    <br />
     </li><li>

                   <CKEditor:CKEditorControl ID="txtPageHeader" BasePath="~/Scripts/ckeditor" runat="server"></CKEditor:CKEditorControl>
    </li><li>

                        <asp:Label ID="FooterLabel" AssociatedControlID="txtPageFooter" runat="server"></asp:Label>
         <br />

          </li><li>
            <CKEditor:CKEditorControl ID="txtPageFooter" BasePath="~/Scripts/ckeditor" runat="server">
                                        </CKEditor:CKEditorControl>

        </li><li>
                    <asp:Button CssClass="btn btn-primary btn-xs bw" ID="LayoutSaveButton" runat="server" Text="" OnCommand="OnSave" />
            <br />
            </li>
    </asp:Panel>

    <asp:PlaceHolder ID="EditCssPlaceHolder" runat="server" Visible="false">
<li>
                        <asp:Label ID="EditCssTitle" AssociatedControlID="EditCssTextBox" runat="server">Edit CSS</asp:Label>

                    <asp:TextBox Width="650px" Height="300px" BorderWidth="10" BorderColor="white" ID="EditCssTextBox" runat="server" TextMode="MultiLine"></asp:TextBox>
    </li>
        <li>
                    <asp:Button CssClass="btn btn-primary btn-xs bw"  ID="EditCssSaveButton" runat="server" Text="Save" OnClick="EditCssSaveButton_Click" />
                    <asp:Button CssClass="btn btn-primary btn-xs bw" ID="EditCssCancelButton" runat="server" Text="Cancel" OnClick="EditCssCancelButton_Click" />
            <br />
        </li>
    </asp:PlaceHolder>
  
  </ol>

            </fieldset>

            </div>
         </div>
</asp:Content>
