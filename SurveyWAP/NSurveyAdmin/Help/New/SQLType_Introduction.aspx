<%@ Page Language="c#" MasterPageFile="~/nsurveyadmin/MsterPageTabs.master" AutoEventWireup="false"
    Inherits="Votations.NSurvey.WebAdmin.HelpFiles" CodeBehind="../default.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><div id="helpPanel"><div>
                <asp:ImageButton ID="btnBack" ImageUrl="~/Images/index-icon.png" runat="server" CssClass="buttonIndex"
                    PostBackUrl="~/NSurveyAdmin/Help/default.aspx#SqlTypes" Visible="True" ToolTip="Back to Helpfiles Index" />
            </div><div>
                <h2 style="color:#5720C6;">
                    SQL Answertype Introduction</h2><hr style="color:#e2e2e2;" />
            
SQL queries based answertypes are another powerful feature of SP&trade;.<br />
<br />
Using a Sql based answertype existing data directly from the SP&trade; database can be used in
a survey.<br />
The data will be presented on the surveyform in a dropdown list, which can be set to mandatory or not. There are no 'out
of the box' (default) Sql based answer types available as these will depend on the database used and data to connect to.<br />
<br />
Instruction to create a Sql Answertype :<br />
<div style="margin-left:10px;">
<ul>
<li>Go to the menu Designer / Answer type editor.</li>
<li>Click on the button 'create new type</li>
<li>Add a new name eg : Users list</li>
<li>Select 'Sql Query' as a datasource</li>
<li>Enter the Sql query eg: <code>select userid, username from vts_tbuser</code></li>
<li> Only Sql &quot;select&quot; based queries are allowed.</li> 
<li>If it requires a selection check the &quot;selection required&quot; box.</li>
<li>Click the 'Create Type' button.</li>
</ul>
    <br />
</div>
This new answer type can now be used in any new or existing question.<br />
<br />
<u>Answer Piping</u><br />
Answers from previous pages can be 'piped' using the standard
[[pipealias]]] tags directly in the Sql query. In the previous
example a survey could be created that asks the user on the first
page for his/her country and it could then retrieve (through piping into a select query) on the
other pages a list of users of the selected country.<br />
<br />
E.g. <code>select userid, username from vts_tbuser where country =
[[countryalias]]</code><br />
<br />
Its also possible to use other piping tags in the SQL query like :<br />
                <code>
##yourquerystringvariablename##<br />
@@yoursessionvariablename@@<br />
&amp;&amp;yourcookievariablename&amp;&amp;<br />
%%servervariablename%%<br /></code>
<br />
<u>Security considerations</u><br />
In some scenarios allowing 'select' queries against a database can be a
security threat. By default all administrator accounts can create / change sql based answertypes.<br />
<br />
If a new user is created (role)rights will have to be given explicitly to create sql based answer types. 
It is advised that if no sql based answer type roleright is given to consider to also remove (survey and question) Xml import rights
for the user could theoretically through changing the Xml import file inject Sql code to run against the database<br />
<br />
To disable the SQL features for all administrators go to menu Surveys/ Settings/ System Settings/ and find Miscellaneous Settings:
                SqlBasedAnswerTypesAllowed and set it to false.
                <hr style="color:#e2e2e2;" /><h3>More Information</h3>
                <br />
<a href="AT_Introduction.aspx" title=" Answer Types Introduction " >Answer Types Introduction </a>	<br />
<a href="Answer Type Creator.aspx" title=" Answer Type Creator " >Answer Type Creator </a>	<br />
<a href="Answer Type Editor.aspx" title=" Answer Type Editor " >Answer Type Editor </a>	<br />                <br />
            </div>
<div id="fillerDiv" class="fillerDiv">&nbsp;</div></div></asp:Content>

