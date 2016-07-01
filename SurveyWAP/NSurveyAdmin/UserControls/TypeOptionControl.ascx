<%@ Control Language="c#" AutoEventWireup="false" Inherits="Votations.NSurvey.WebAdmin.UserControls.TypeOptionControl"
    TargetSchema="http://schemas.microsoft.com/intellisense/ie5" CodeBehind="TypeOptionControl.ascx.cs" %>


            <div style="position: absolute; width: 650px; text-align: center; margin-left: 57px; top: 15px;">
 <asp:Label ID="MessageLabel" runat="server"  CssClass="errorMessage" Visible="False"></asp:Label>
                </div>
<br />
                        <fieldset style="width:750px; margin-left:12px; margin-top:15px;">
                                            <legend class="titleFont" style="margin: 0px 15px 0 15px; text-align:left;">
                <asp:Label ID="fieldTypeOptionTitleLabel" runat="server">Label</asp:Label>
                </legend><br />
<ol>
     <li>

                        <asp:Label ID="FieldNameLabel" AssociatedControlID="TitleTextBox" runat="server" >Field name:</asp:Label>

                        <asp:TextBox ID="TitleTextBox" Columns="50" MaxLength="48" runat="server"></asp:TextBox>
         </li><li>  
                            <asp:Label ID="DataSourceLabel" AssociatedControlID="DataSourceDropDownList" runat="server"></asp:Label>

                        <asp:DropDownList ID="DataSourceDropDownList" runat="server" AutoPostBack="True">
                        </asp:DropDownList>
</li><li>  
                        <asp:Label ID="XmlFileNameLabel" AssociatedControlID="XmlFileNameTextbox" runat="server" Font-Bold="True" Visible="False">Xml filename :</asp:Label>

                        <asp:TextBox ID="XmlFileNameTextbox" runat="server" Visible="False"></asp:TextBox>
    </li><li>  
                        <asp:Label ID="SqlQueryLabel" AssociatedControlID="SqlQueryTextbox" runat="server" Font-Bold="True" Visible="False">Sql query *:</asp:Label>

                        <asp:TextBox ID="SqlQueryTextbox" runat="server" Visible="False" Columns="50" TextMode="MultiLine"
                            Rows="5"></asp:TextBox>

                        <asp:Label ID="SqlQueryInfoLabel" Visible="False" runat="server"><br />*First two column will be used as value and text</asp:Label>

        </li><li>  
                <asp:PlaceHolder ID="FullOptionPlaceholder" runat="server" Visible="true">

                                <asp:Label ID="AllowSelectionLabel" AssociatedControlID="SelectionTypeCheckBox" runat="server">Allow selection:</asp:Label>

                            <asp:CheckBox ID="SelectionTypeCheckBox" runat="server" AutoPostBack="True"></asp:CheckBox>
</li><li>  
                                <asp:Label ID="FieldEntryLabel" AssociatedControlID="FieldTypeCheckBox" runat="server">Field entry  :</asp:Label>
                            <asp:CheckBox ID="FieldTypeCheckBox" runat="server" AutoPostBack="True"></asp:CheckBox>
</li><li>  
                    <asp:PlaceHolder ID="FieldOptionsPlaceHolder" runat="server" Visible="false">

                                    <asp:Label ID="RichFieldLabel" AssociatedControlID="RichFieldCheckBox" runat="server" >Rich field:</asp:Label>

                                <asp:CheckBox ID="RichFieldCheckBox" runat="server"></asp:CheckBox>
</li><li> 
                                    <asp:Label ID="FieldWidthLabel" AssociatedControlID="FieldWidthTextBox" runat="server">Field width :</asp:Label>
                                <asp:TextBox ID="FieldWidthTextBox" runat="server" size="3" MaxLength="3">20</asp:TextBox>
</li><li> 
                                    <asp:Label ID="FieldHeightLabel" AssociatedControlID="FieldHeightTextBox" runat="server">Field height :</asp:Label>
                                <asp:TextBox ID="FieldHeightTextBox" runat="server" size="3" MaxLength="3">0</asp:TextBox>
    </li><li> 
                                    <asp:Label ID="FieldLengthLabel" AssociatedControlID="FieldMaxLengthTextBox" runat="server">Field max. length:</asp:Label>

                                <asp:TextBox ID="FieldMaxLengthTextBox" runat="server" size="3" MaxLength="3">255</asp:TextBox>
        </li><li> 
                                    <asp:Label ID="ShownInResultsLabel" AssociatedControlID="FieldShownInResultsCheckBox" runat="server" >Field shown in results :</asp:Label>

                                <asp:CheckBox ID="FieldShownInResultsCheckBox" runat="server"></asp:CheckBox>
            </li><li> 
                                    <asp:Label ID="JavascriptFunctionLabel" AssociatedControlID="JavascriptFunctionNameTextBox" runat="server">Javascript function name :</asp:Label>

                                <asp:TextBox ID="JavascriptFunctionNameTextBox" Columns="50" MaxLength="45" runat="server"></asp:TextBox>
                </li><li> 
                                    <asp:Label ID="JavascriptErrorMessageLabel" AssociatedControlID="JavascriptErrorMessageTextBox" runat="server">Error message :</asp:Label>

                                <asp:TextBox ID="JavascriptErrorMessageTextBox" Columns="84" MaxLength="80" runat="server"></asp:TextBox>
                    </li><li> 
                                    <asp:Label ID="JavascriptCodeLabel" AssociatedControlID="JavascriptTextBox" runat="server">Javascript code :</asp:Label>
                                <br /><br />
                                <asp:TextBox ID="JavascriptTextBox" BorderStyle="solid" BorderWidth="20" BorderColor="White" runat="server" MaxLength="999" Width="100%" Rows="8" TextMode="MultiLine"
                                    Columns="60"></asp:TextBox>
                                

                    </asp:PlaceHolder>

                </asp:PlaceHolder>

                        </li><li> 
<asp:Button ID="CreateTypeButton" CssClass="btn btn-primary btn-xs bw" runat="server" Text="Create type"></asp:Button>
<asp:Button ID="ApplyChangesButton" CssClass="btn btn-primary btn-xs bw" runat="server" Text="Apply changes"></asp:Button>
<asp:Button ID="DeleteTypeButton" CssClass="btn btn-primary btn-xs bw" runat="server" Text="Delete type"></asp:Button>
<asp:Button ID="MakeBuiltInButton" CssClass="btn btn-primary btn-xs bw" Visible="False" runat="server" Text="Make Built In"></asp:Button>
<br />

                                        </li>
  </ol>
                    <br />
                    </fieldset>