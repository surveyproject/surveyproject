<%@ Page Language="c#" MasterPageFile="~/nsurveyadmin/MsterPageTabs.master" AutoEventWireup="false" Inherits="Votations.NSurvey.WebAdmin.HelpOptions" Codebehind="index.aspx.cs" %>



 <asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <div id="mainBody" class="mainBody contentHolder ps-container">
         <div id="Panel" class="Panel content">


                        <!--      <asp:Label ID="HelpMessageLabel" runat="server" CssClass=""></asp:Label> -->

                 <fieldset style="width:700px; margin-top:15px; margin-left:37px; text-align: left;">
        <legend class="titleFont" id="hftitle" runat="server">
        </legend><br />
        <ol>
            <li style="background-color:white;"><div class="rounded_corners" style="padding:7px; border-color:transparent;">
   
                             <asp:Literal ID="HelpFilesText" runat="server" EnableViewState="False">Helpfiles Text</asp:Literal>
                                           <br /> <br />
               </div></li>
        </ol>
   
    </fieldset>

                              <fieldset style="width:700px; margin-top:15px; margin-left:37px; text-align: left;">
        <legend class="titleFont" id="sstitle" runat="server">                        
             </legend><br />
        <ol>
            <li style="background-color:white;"><div class="rounded_corners" style="padding:7px; border-color:transparent;">
                             <asp:Literal ID="StartupSettings" runat="server" EnableViewState="False">Startup & Settings</asp:Literal>
                                                       <br /><br /> </div></li>
        </ol>
   
    </fieldset><br /><br />
 </div></div></asp:Content>

