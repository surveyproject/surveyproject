<%@ Page Language="c#" MasterPageFile="~/nsurveyadmin/MsterPageTabs.master" AutoEventWireup="false"
    Inherits="Votations.NSurvey.WebAdmin.HelpFiles" CodeBehind="../default.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><div id="helpPanel"><div>
                <asp:ImageButton ID="btnBack" ImageUrl="~/Images/index-icon.png" runat="server" CssClass="buttonIndex"
                    PostBackUrl="~/NSurveyAdmin/Help/default.aspx#Completion" Visible="True" ToolTip="Back to Helpfiles Index" />
            </div><div>
                <h2 style="color:#5720C6;">
                    Create Custom Results Reports</h2><hr style="color:#e2e2e2;" />             
<i>
Reporting Option</i><br />
- Show custom results report webpage after submitting a survey to present (up to) 5 different (calculated) scores/ results
                <br /><br />
<i>
General Working</i><br />
- scores/ results are based on calculations in stored procedure that is triggered on submitting the survey and on opening the customreport<br />
<br />
<b>
Steps to Implement</b><br />
                <br /><u>
1. Edit the '[dbo].[vts_spReportGetScores]' Stored Procedure</u><br /><br />

- Open MSsqlserver Management<br />
- connect to the SP database<br />
- go to Programmability - Stored procedures - [dbo].[vts_spReportGetScores]<br />
- right click sproc and select 'Modify' to open and edit<br />
- add calculations/ values etc - SEE TECHNICAL DETAILS BELOW<br />
- run the Modify query (!Execute button or Ctrl + e) to apply the changes
                <br /><br />
<u>
2. Add the report to a survey</u><br />
                <br />
- Login to SP and select the survey to work with<br />
- go to menu Campaigns/ Web and copy the '?surveyid=xxxxx' from the 'Deployment URL' (note: report does not work with friendly url)<br />
- go to Menu Surveys/ Settings/ Completion<br />
- add the 'Return URL' : nsurveyreports/customreport.aspx?surveyid=xxxxxxxx (paste surveyid) and save changes<br />
                <br />
    <u>
3. Submit survey and show custom report</u><br />
                <br />
- Go to menu Campaigns/ Web and click the Deployment URL to open the survey in a new Tab<br />
- Answer the questions and click submit button<br />
- The Custom Report webpage will open and present the results of the calculations of the sproc<br />
                <br /><br />
        <b>
TECHNICAL DETAILS</b><br /><br />

<u>              
I. Webpages</u> <br />
- the customreport pages (.aspx, .cs and .designer.cs) are stored in the 'NsurveyReports' directory<br />
- the directorypath 'NsurveyReports/' has to be included in the Completion 'Return URL'<br />

<br /><i>
a. customreport.aspx webpage</i><br />
- .aspx webpage contains a datagrid (generated automatically) presenting the (calculated) results<br />
- number of columns/ fields shown is determined by the .cs codebehind and sproc<br />
- layout/ look and feel are determined by the .aspx page (and css)<br />
                <br />
<i>
b. codebehind files (.cs/ .designer.cs)</i><br />
- the .cs page includes C# code instructions to add/present the values received from the sproc into the datagrid on the .aspx webpage<br />
- by default 5 different values + 1 voterid (mandatory) are shown on the datagrid<br />
- the number of values has to match the number of values generated in the stored procedure e.v.v.<br />
                <br />
- the method 'BindItemData(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)' is set to show 5 values + 1 voterid<br />
- the Voterid is used as a key to the .aspx datagrid and therefore mandatory in .cs code and sproc but can be kept 'hidden' (cell[0].visible = false)<br />
- the other cells (e.Item.Cells[1] - e.Item.Cells[5]) will containt the calculated values<br />
                <br />
- more values can be added by adding more codeblocks e.g.:<br /><br />
<code>
            //e.Item.Cells[6].Visible = true;<br />
            if (e.Item.Cells[6].Text.Equals("blank6") || e.Item.Cells[5].Text.Equals("cell6"))<br />
            {<br />
                if (e.Item.ItemType == ListItemType.Header)<br />
                    e.Item.Cells[6].Visible = false;<br />
    <br />
                if (e.Item.ItemType == ListItemType.Item)<br />
                    e.Item.Cells[6].Visible = false;<br />
            }
</code><br /><br />
- if < 5 values are used the number of codeblocks can be adjusted also (delete codeblock) <br />
                <br />
NOTE: ! always match the number of values calculated in the sproc (see below)!<br />

                <br /><u>
II. Stored Procedure </u><br />
- there is only one (hardcoded) storedprocedure '[dbo].[vts_spReportGetScores]' that can be used to show values on the report page<br />
- the number of (output) values calculated by the storedprocedure must match the number of cells[x] coded in the .cs file<br />
- by default both sproc and .cs file allow for 5 values to show on the datagrid (ex. voterid)<br />

- to 'fill' all 5 cells[1-5] even if < 5 are needed the sproc can make use of the following values/ fieldnames:<br /><br />
                <code>
	'blank1' as cell1,<br />
	'blank2' as cell2,<br />
	'blank3' as cell3,<br />
	'blank4' as cell4,<br />
	'blank5' as cell5,
</code><br /><br />
	If any of these values ('blankx') and/ or fieldsnames (cellx) are used in the right order in the sproc
	they will not show on the datagrid (visilble = false) without having to 
	adjust the number of cells[x] in the .cs codebehind file.<br /><br />

As soon as any other value or field name is used in the Sproc it will show on the datagrid.<br />
<br /><i>
Example sproc values:</i><br /><br />
                <code>
	'Test' as test1,<br />
	'blank2' as cell2,<br />
	'blank3' as cell3,<br />
	'blank4' as cell4,<br />
	'blank5' as cell5,<br />
</code><br />
Result on datagrid: 1 column showing Header test1 and value Test, all other items invisible. No need to adjust .cs codebehind.

                <br /><br />
                A separate manual will be published at http://www.surveyproject.org - menu Support/ Helpfiles/ 
                <a href="http://www.surveyproject.org/Support/Helpfiles/GuidesManuals/tabid/300/Default.aspx" target="_blank">Guides & Manuals</a>
                <br />        
               
<hr style="color:#e2e2e2;" /><h3>More Information</h3><br />
                <a href="../Completion Actions.aspx" title="Completion Actions">Completion Actions</a><br />

                                <a href="..\Thanks Message Conditions.aspx" title="">Thanks Message Conditions</a><br />
                                <a href="Advanced Completion.aspx" title="Advanced Completion">Advanced Completion</a><br />
<a href="Voter%20Report%20Edit.aspx">Edit Individual Responses Report</a><br />
                <br />
            </div>
<div id="fillerDiv" class="fillerDiv">&nbsp;</div></div></asp:Content>

