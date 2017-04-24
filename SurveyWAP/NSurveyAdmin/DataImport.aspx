<%@ Page Title="" Language="C#" MasterPageFile="~/NSurveyAdmin/MsterPageTabs.master"
    AutoEventWireup="true" CodeBehind="DataImport.aspx.cs" Inherits="Votations.NSurvey.WebAdmin.NSurveyAdmin.DataImport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="mainBody" class="mainBody contentHolder ps-container">
        <div id="Panel" class="Panel content">

    <script type="text/javascript">
        $(function () {

            var lang2 = '<%=Request.UserLanguages[0].ToString().ToLower()%>';
            var lang = lang2.substring(0,2)

            $("#<%=txtFrom.ClientID%>").datepicker( $.datepicker.regional[lang] );
            $("#<%=txtTo.ClientID%>").datepicker($.datepicker.regional[lang]);

        });
    </script>


                        <div style="position: absolute; width: 650px; text-align: center; margin-left: 57px; top: 6px;">
 <asp:Label ID="MessageLabel" runat="server"  CssClass="errorMessage" Visible="False"></asp:Label>
                </div>
            <br />



    <fieldset style="width:750px; margin-top:15px; margin-left:12px; text-align: left;">
        <legend class="titleFont titleLegend">

                    <asp:Literal ID="DataImportTitle" runat="server" Text="Data Import" EnableViewState="False"></asp:Literal>


                    </legend><br />
        <ol>
            <li>
            <asp:Label ID="importFilelabel" AssociatedControlID="fupDataFile" runat="server" >Import file: </asp:Label>

                <asp:FileUpload runat="server" ID="fupDataFile" />
                            </li>
            <li>

     <asp:Label ID="dataSelectionLabel" AssociatedControlID="rbAll"  runat="server">  Data selection</asp:Label><br />
                                            </li>
            <li>
                <asp:RadioButton ID="rbAll" runat="server"  Checked="true" GroupName="DataSelection" Text="All data." />
                 <br /> <br />
              <asp:RadioButton ID="rbSelRange" Width="100%" runat="server" GroupName="DataSelection" Text="Only data in the selected date range." />
             
                <br />
              </li>
            <li>

          <asp:Label ID="importFromLabel" AssociatedControlID="txtFrom" runat="server">  From:</asp:Label>  

                <asp:TextBox ID="txtFrom" Columns="8"  runat="server"></asp:TextBox>
                           </li>
            <li>

 <asp:Label AssociatedControlID="txtTo" ID="importTolabel" runat="server">  To:</asp:Label>  

                <asp:TextBox ID="txtTo" Columns="8" runat="server"></asp:TextBox>
               
                            </li>
            <li>

                <asp:Button runat="server" CssClass="btn btn-primary btn-xs bw" ID="btnImport" Text="Import" OnClick="btnImport_Click" />     <br />
                            </li>
        </ol>
   
    </fieldset>
</div></div></asp:Content>
