<%@ Page Title="Error Logfile" Language="C#" MasterPageFile="~/NSurveyAdmin/MsterPageTabs.master" AutoEventWireup="true" CodeBehind="ErrorLog.aspx.cs" Inherits="Votations.NSurvey.WebAdmin.NSurveyAdmin.ErrorLog" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div id="Panel" class="Panel">

        <div class="errorDiv">
            <asp:Label ID="MessageLabel" runat="server" CssClass="errorMessage" Visible="False"></asp:Label>
        </div>

        <div class="helpDiv">
            <a onmouseover='this.style.cursor="help" ' onfocus='this.blur();' href="Help/ErrorLog.aspx"
                title="Click for more Information">
                <img alt="help" border="0" src="<%= Page.ResolveUrl("~")%>Images/small_help.gif" />
            </a>
        </div>

        <style>
            .test{
                height:50vh;
                overflow: auto;
            }
        </style>

        <fieldset>
            <legend class="titleFont titleLegend">Error Log</legend>
            <br />
            <ol>
                    <asp:PlaceHolder ID="EditLogPlaceHolder" runat="server" Visible="false">
                        <li>
                            <asp:Label ID="EditLogTitle" AssociatedControlID="EditLogTextBox" runat="server">Edit Logfile</asp:Label>

                            <asp:TextBox Width="700px" CssClass="test" BorderWidth="10" BorderColor="white" ID="EditLogTextBox" runat="server" TextMode="MultiLine"></asp:TextBox>
                        </li>
                        <li>
                            <asp:Button CssClass="btn btn-primary btn-xs bw" ID="EditLogSaveButton" runat="server" Text="Save" OnClick="EditLogSaveButton_Click" />
                        <!--    <asp:Button CssClass="btn btn-primary btn-xs bw" ID="EditLogCancelButton" runat="server" Text="Cancel" OnClick="EditLogCancelButton_Click" /> -->
                            <br />
                        </li>
                    </asp:PlaceHolder>
            </ol>

        </fieldset>
        <div id="fillerDiv" class="fillerDiv">&nbsp;</div>
        </div>
</asp:Content>
