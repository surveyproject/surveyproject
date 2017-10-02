<%@ Page language="c#" MasterPageFile="~/Wap.master"   AutoEventWireup="false" Inherits="Votations.NSurvey.WebAdmin.EditMultiLanguages" Codebehind="EditMultiLanguages.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <!-- note: file not used anywhere !? -->

        <div id="Panel" class="Panel content">

<table class="TableLayoutContainer">
  <tr>
    <td></td></tr>
  <tr>
    <td class="contentCell" valign="top"> 
        
                  <div class="errorDiv">
 <asp:Label ID="MessageLabel" runat="server"  CssClass="errorMessage" Visible="False"></asp:Label>
                </div>
    
      <table class="innerText">
        <tr>
          <td colspan="2">
          
          <table id="Table2">
          <tr>
          <td width="99%">
          <font class="titleFont"><asp:Literal id="MessagesTranslatorLabel" runat="server" EnableViewState="False" Text="Messages translator"></asp:Literal></font></td>
          <td align="right"></td>
          </tr>
          </table>
          
          <br /><br />

        </td>
        </tr>
        <tr>
          <td>&nbsp;&nbsp; </td>
          <td>
          
            <table class="innerText">
              <tr>
                <td nowrap="nowrap" width="180" valign="top">
                <strong><asp:Literal id="SurveyCreationDateLabel" runat="server" EnableViewState="False" Text="Source Text :"></asp:Literal></strong> </td>
                <td width="480"><asp:ListBox id="LanguageTextListBox" runat="server"></asp:ListBox></td>
              </tr>
              <tr>
                <td width="180"><strong></strong>&nbsp; </td>
                <td></td>
              </tr>
              <tr>
                <td width="180"><strong></strong>&nbsp; </td>
                <td></td>
              </tr>
              <tr>
                <td width="180"><strong></strong>&nbsp; </td>
                <td></td>
              </tr>
              <tr>
                <td width="280"><strong></strong> </td>
                <td>&nbsp;</td>
              </tr>
              <tr>
                <td width="180" valign="top"><strong></strong>&nbsp; </td>
                <td></td>
              </tr>
            </table>
            
            <br />
            <asp:PlaceHolder id="LanguageTextPlaceHolder" runat="server"></asp:PlaceHolder>
            <br /><br />
            <asp:button id="ResetVotesButton" runat="server" Text="Reset votes!"></asp:button>
          </td>
         </tr>
        </table>
        
       </td>
      </tr>
      
    </table>
                                     <div id="fillerDiv" class="fillerDiv">&nbsp;</div>
 </div></asp:Content>
