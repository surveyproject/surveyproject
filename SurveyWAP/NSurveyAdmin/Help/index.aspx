<%@ Page Language="c#" MasterPageFile="~/nsurveyadmin/MsterPageTabs.master" AutoEventWireup="false" Inherits="Votations.NSurvey.WebAdmin.HelpOptions" Codebehind="index.aspx.cs" %>



 <asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     
         <div id="helpPanel">


                        <!--      <asp:Label ID="HelpMessageLabel" runat="server" CssClass=""></asp:Label> -->

                 <fieldset style="width:700px; margin-top:15px; margin-left:17px; text-align: left;">
        <legend class="titleFont" id="hftitle" runat="server">
        </legend>
        <ol>
            <li style="background-color:white;"><div class="rounded_corners" style="border-color:transparent;">
   
                             <asp:Literal ID="HelpFilesText" runat="server" EnableViewState="False">Helpfiles Text</asp:Literal>
                                           <br />
               </div></li>
        </ol>
   
    </fieldset>

                              <fieldset style="width:700px; margin-top:15px; margin-left:17px; text-align: left;">
        <legend class="titleFont" id="sstitle" runat="server">                        
             </legend>
        <ol>
            <li style="background-color:white;"><div class="rounded_corners" style="border-color:transparent;">
                             <asp:Literal ID="StartupSettings" runat="server" EnableViewState="False">Startup & Settings</asp:Literal>
                                                       <br /><br /> </div></li>
        </ol>
   
    </fieldset><br /><br />
 <div id="fillerDiv" class="fillerDiv">&nbsp;</div></div></asp:Content>

