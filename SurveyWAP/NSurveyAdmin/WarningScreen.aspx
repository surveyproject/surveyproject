<%@ Page Title="" Language="C#" MasterPageFile="~/Wap.Master" AutoEventWireup="true"
    CodeBehind="WarningScreen.aspx.cs" Inherits="Votations.NSurvey.WebAdmin.NSurveyAdmin.WarningScreen" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div style="width: 775px; 
                background-color: #ffffff; 
                height:750px; 
                vertical-align:top;
                border: 1px #aaaaaa solid ;              
               -webkit-border-radius: 7px;
               -moz-border-radius: 7px;
                border-radius: 7px;
                ">
                <br />
                <br />
                <br />
                <asp:Label CssClass="ErrorMessage icon-warning-sign" runat="server" ID="lblWarning"></asp:Label>
                <br />
</div>
</asp:Content>
