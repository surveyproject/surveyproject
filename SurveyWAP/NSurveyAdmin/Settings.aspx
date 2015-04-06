<%@ Page Language="C#" MasterPageFile="~/Wap.Master" AutoEventWireup="true" CodeBehind="Settings.aspx.cs" Inherits="Votations.NSurvey.WebAdmin.Settings" %>

<asp:content id="Content1" contentplaceholderid="ContentPlaceHolder1" runat="Server">
        <div id="mainBody" class="contentHolder ps-container" style="width: 775px; top: 0px;
                background-color: #ffffff; 
                height:750px; 
                vertical-align:top;
                text-align:left;
                border: 1px #aaaaaa solid ;              
               -webkit-border-radius: 7px;
               -moz-border-radius: 7px;
                border-radius: 7px;
                
                ">
        <div id="Panel" class="Panel content" style="margin: 25px 0px 25px 10px; width: 725px;">

                        <div style="position: absolute; width: 650px; text-align: center; margin-left: 57px; top: 15px;">
 <asp:Label ID="MessageLabel" runat="server"  CssClass="ErrorMessage" Visible="False"></asp:Label>
                </div>


            <h2 style="margin-left:25px;">General Settings</h2><br />
<!--
            <fieldset style="width: 720px; margin-top: 15px; margin-left: 15px; text-align: left;">
                <legend class="titleFont" style="margin: 0px 15px 0 15px;">CREATE SP DATABASE
                </legend>
                <br />
                <ol>
                    <li>Create the SP database:


                <asp:Button ID="btnCreateSqlDb" CssClass="btn btn-primary btn-xs bw" runat="server" Text="Create DB" />

                    </li>
                </ol>

            </fieldset>
            -->

                            <fieldset style="width:720px; margin-top:15px; margin-left:15px; text-align: left;">
        <legend class="titleFont" style="margin: 0px 15px 0 15px;">DATABASE SETTINGS
                                </legend><br />
        <ol>
            <li>
            
            <b>Connection Strings:</b>
        <br /><br />
                1. Developement [SurveyProjectDevConnectionString]:<br /><asp:Label CssClass="SettingsValueLabel" ID="lblConnectionStringDev" runat="server"></asp:Label><br />
            <asp:TextBox Width="625" Rows="4" Height="50" Wrap="true" TextMode="MultiLine" colums="50"  ID="txtConnectionStringDev" runat="server"></asp:TextBox><br /><br />
                        2. Test [SurveyProjectTestConnectionString]:<br /><asp:Label ID="lblConnectionStringTest" CssClass="SettingsValueLabel" runat="server"></asp:Label><br />
            <asp:TextBox Width="625" Rows="4" Height="50" Wrap="true" TextMode="MultiLine" colums="50" ID="txtConnectionStringTest" runat="server"></asp:TextBox><br /><br />
                       3. Production [SurveyProjectProdConnectionString]:<br /><asp:Label ID="lblConnectionStringProd" CssClass="SettingsValueLabel" runat="server"></asp:Label><br />
            <asp:TextBox Width="625" Rows="4" Height="50" Wrap="true" TextMode="MultiLine" colums="50"  ID="txtConnectionStringProd" runat="server"></asp:TextBox><br /><br />
            <asp:Button ID="btnConnectionStrings" CssClass="btn btn-primary btn-xs bw" runat="server" Text="Submit" />
            <asp:Button ID="btnDecriptConnections" CssClass="btn btn-primary btn-xs bw" runat="server" Text="Decrypt" /><br /><br />
                                            </li>
            <li>

           <b>Default connection:</b>          <br />
            <asp:Label ID="lblDefaultDbConnection" CssClass="SettingsValueLabel" runat="server" ></asp:Label><br />
            <asp:TextBox Width="350" ID="txtDefaultDbConnection" runat="server"></asp:TextBox>
            <asp:Button ID="btnDefaultDbConnection" CssClass="btn btn-primary btn-xs bw" runat="server" Text="Submit"/>
            <br /><br />
                                                            </li>
        </ol>
   
    </fieldset>
        <br /><br />

                <fieldset style="width:720px; margin-top:15px; margin-left:15px; text-align: left;">
        <legend class="titleFont" style="margin: 0px 15px 0 15px;">Globalisation Culture
                                </legend><br />
        <ol>
            <li>

                        <b>Globalisation Culture:</b><br />
        <asp:Label ID="lblCulture" CssClass="SettingsValueLabel" runat="server"></asp:Label><br />
        <asp:TextBox ID="txtCulture" runat="server"></asp:TextBox>
        <asp:Button ID="btnCulture" CssClass="btn btn-primary btn-xs bw" runat="server" Text="Rename" />
        <asp:Button ID="btnCultureDecript" CssClass="btn btn-primary btn-xs bw" runat="server" Text="Decrypt" />
            <br /><br />
                                                            </li>
        </ol>
   
    </fieldset>

                <fieldset style="width:720px; margin-top:15px; margin-left:15px; text-align: left;">
        <legend class="titleFont" style="margin: 0px 15px 0 15px;">NSURVEYSETTINGS
                                </legend><br />
        <ol>
            <li>

        <b>Mailserver Settings:</b>
        <br /><br />
        SMTP Mailserver: <asp:Label ID="lblSmtpMailserver" CssClass="SettingsValueLabel" runat="server"></asp:Label><br />
            <asp:TextBox Width="350" ID="txtSmtpServer"  runat="server"></asp:TextBox><br /><br />
        SMTP Username: <asp:Label ID="lblSmtpServerUsername" CssClass="SettingsValueLabel" runat="server"></asp:Label><br />
            <asp:TextBox Width="350" ID="txtSmtpUser" runat="server"></asp:TextBox><br /><br />
        SMTP Password: <asp:Label ID="lblSmtpServerPassword" CssClass="SettingsValueLabel" runat="server"></asp:Label><br />
            <asp:TextBox Width="350" ID="txtSmtpPassword" runat="server"></asp:TextBox><br /><br />
        SMTP Server Port: <asp:Label ID="lblSmtpServerPort" CssClass="SettingsValueLabel" runat="server"></asp:Label><br />
            <asp:TextBox Width="350" ID="txtSmtpPort" runat="server"></asp:TextBox><br /><br />
                        <asp:Button ID="btnSmtp" CssClass="btn btn-primary btn-xs bw" runat="server" Text="Submit" /><br />
                              
                            </li>
            <li>
        <b>Miscellaneous Settings:</b>
        <br /><br />
            FormUserProviderSingleMode: <asp:Label ID="lblSingleMode" CssClass="SettingsValueLabel" runat="server"></asp:Label><br />
            <asp:TextBox Width="350" ID="txtSingleMode" runat="server"></asp:TextBox><br /><br />
            SQL Based Answer Types Allowed: <asp:Label ID="lblSqlAnswer" CssClass="SettingsValueLabel" runat="server"></asp:Label><br />
            <asp:TextBox Width="350" ID="txtSqlAnswer" runat="server"></asp:TextBox><br /><br />
            UploadedFileDeleteTimeOut: <asp:Label ID="lblFileDeleteTime" CssClass="SettingsValueLabel" runat="server"></asp:Label><br />
            <asp:TextBox Width="350" ID="txtFileDeleteTime" runat="server"></asp:TextBox><br /><br />
            SessionUploadedFileDeleteTimeOut: <asp:Label ID="lblSessionFileDeleteTime" CssClass="SettingsValueLabel" runat="server"></asp:Label><br />
            <asp:TextBox Width="350" ID="txtSessionFileDeleteTime" runat="server"></asp:TextBox><br /><br />
            <asp:Button ID="btnMiscSettings" CssClass="btn btn-primary btn-xs bw" runat="server" Text="Submit" /><br /><br />
                                            </li>
            <li>
            <b>Decrypt the NSurveySettings (smtp & miscellaneous):</b>
            <asp:Button ID="btnDecryptSmtp" CssClass="btn btn-primary btn-xs bw" runat="server" Text="Decrypt" />   <br /><br />

                                            </li>
        </ol>
   
    </fieldset>
            <br /><br />
            </div></div>
</asp:content>