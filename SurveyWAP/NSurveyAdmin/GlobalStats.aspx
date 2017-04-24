<%@ Page Language="c#" MasterPageFile="MsterPageTabs.master" AutoEventWireup="false" Inherits="Votations.NSurvey.WebAdmin.GlobalStats" Codebehind="GlobalStats.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="HeaderControl" Src="UserControls/HeaderControl.ascx" %>

 <asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

             <div id="mainBody" class="mainBody contentHolder ps-container">
                 <div id="Panel" class="Panel content">
 
            <div style="position: absolute; width: 650px; text-align: center; margin-left: 57px; top: 15px;">
 <asp:Label ID="MessageLabel" runat="server"  CssClass="errorMessage" Visible="False"></asp:Label>
                </div>

                                        <div style="position: relative; left: 720px; width: 10px;  top: 13px; clear:none;">
                                            <a onmouseover='this.style.cursor="help" ' onfocus='this.blur();' href="Help/Statistics.aspx"
                                                title="Click for more Information">
                                                <img alt="help" border="0" src="<%= Page.ResolveUrl("~")%>Images/small_help.gif" />
                                            </a>
                                        </div>
        
            <fieldset style="width: 750px; margin-left: 12px; margin-top: 15px;">
                 <legend class="titleFont titleLegend">
                    <asp:Literal id="SurveyStatisticsTitle" runat="server" EnableViewState="False">Survey statistics</asp:Literal>
                </legend> <br />
             
   <ol>
     <li>

                <div class="scm"><asp:Literal id="SurveyCreationDateLabel" runat="server" EnableViewState="False">Creation date :</asp:Literal></div>
                        <asp:label id="CreationDateLabel" CssClass="statlabel" runat="server"></asp:label>
  </li><li>
                <div class="scm"><asp:Literal id="LastSurveyEntryLabel" runat="server" EnableViewState="False">Last entry on :</asp:Literal></div>
                <asp:label id="LastEntryDateLabel" CssClass="statlabel" runat="server"></asp:label>
      </li><li>     
                <div class="scm"><asp:Literal id="SurveyDisplayTimesLabel" runat="server" EnableViewState="False">Display times :</asp:Literal></div>
                <asp:label id="DisplayTimesLabel"  CssClass="statlabel" runat="server"></asp:label>
  </li><li>

                <div class="scm"><asp:Literal id="SurveyNumberOfVotesLabel" runat="server" EnableViewState="False">Number of voters :</asp:Literal></div>
                <asp:label id="NumberOfVotersLabel" CssClass="statlabel" runat="server"></asp:label>
  </li><li>
                <div class="scm"><asp:Literal id="SaveProgressEntriesLabel" runat="server" EnableViewState="False" Text="Unvalidated progress entries :"></asp:Literal></div>
                <asp:label id="NumberSaveProgressEntriesLabel" CssClass="statlabel" runat="server"></asp:label>
 </li><li><br />
     <div style="float: right; position:relative; top:-16px; margin-right: 165px;">
                <asp:button id="DeleteSaveProgressLinkButton" CssClass="btn btn-primary btn-xs bw" Text="Delete Unvalidated" runat="server"></asp:button>
         </div>
  </li><li>
                <strong><asp:Literal id="SurveyMonthlyStatsLabel" runat="server" EnableViewState="False">Monthly stats :</asp:Literal></strong>
            <div style="margin-left:85px;">
                <asp:Calendar id="StatsCalendar" runat="server" ForeColor="Black" Font-Names="Verdana" Font-Size="9pt" NextPrevFormat="ShortMonth" ShowGridLines="false" BackColor="White" Width="525px" Height="336px" BorderColor="#e2e2e2" CellSpacing="1" BorderStyle="Solid" SelectionMode="None">
                <TODAYDAYSTYLE ForeColor="White"
                  BackColor="#999999" 
                  Font-Size="XX-Small"></TODAYDAYSTYLE>
                  <DAYSTYLE
                  BackColor="#e2e2e2" Font-Size="XX-Small" 
                  Font-Names="arial" ></DAYSTYLE>
                  <NEXTPREVSTYLE ForeColor="White" CssClass="calendar"
                  Font-Size="Small" 
                  Font-Bold="True"></NEXTPREVSTYLE>
                  <DAYHEADERSTYLE
                  ForeColor="#333333" Height="8pt" Font-Size="XX-Small" 
                  Font-Names="arial" 
                  Font-Bold="True"></DAYHEADERSTYLE>
                  <SELECTEDDAYSTYLE
                  ForeColor="White" 
                  BackColor="#333399" Font-Bold="True"></SELECTEDDAYSTYLE>
                  <TITLESTYLE
                  ForeColor="White" Height="16pt" BackColor="#7C8FE3" 
                  Font-Size="Small" 
                  Font-Bold="True"></TITLESTYLE>
                  <OTHERMONTHDAYSTYLE
                  ForeColor="red"
                  Font-Size="XX-Small" Font-Bold="True"></OTHERMONTHDAYSTYLE>
                  </asp:Calendar>
                </div>
            
            <br /><asp:button id="ResetVotesButton" CssClass="btn btn-primary btn-xs bw" runat="server" Text="Reset votes!"></asp:button>
      <br /><br />
     </li>
  </ol>
                    <br />
                    </fieldset>

                </div> 
    </div>
</asp:Content>
