<%@ Page Language="c#" MasterPageFile="~/nsurveyadmin/MsterPageTabs.master" AutoEventWireup="false"
    Inherits="Votations.NSurvey.WebAdmin.HelpFiles" CodeBehind="../default.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><div id="mainBody" class="mainBody contentHolder ps-container"><div id="Panel" class="Panel content">
    <table class="TableLayoutContainer">
        <tr>
            <td>
                <asp:ImageButton ID="btnBack" ImageUrl="~/Images/index-icon.png" runat="server" CssClass="buttonIndex"
                    PostBackUrl="~/NSurveyAdmin/Help/default.aspx#SqlTypes" Visible="True" ToolTip="Back to Helpfiles Index" />
            </td>
        </tr>
        <tr>
            <td class="contentCell" valign="top">
                <br />
                <h2 style="color:#5720C6;">
                    Sql Types Introduction</h2>
                <br />
                <br />
                <hr style="color:#e2e2e2;"/>
                <br />
Sql based query answer types are another powerful feature of Survey Project.<br />
<br />
Using a Sql based type you can expose and use existing data from the database directly in
your survey.<br />
The data will be shown as a dropdown list, which can be set to mandatory or not. By default there aren't any out
of the box Sql based answer types as these will depend on the database to connect to.<br />
<br />
If you whish to create your own Sql type you will need to :<br />
<div style="margin-left:10px;">
<ul>
<li>Go to the menu Designer / Answer type editor section.</li>
<li>Click on create new type</li>
<li>Give it a new name eg : Customer list</li>
<li>Select Sql query as a datasource</li>
<li>Enter the Sql query eg: select customerid, customername from tbcustomers</li>
<li> Only Sql &quot;select&quot; based queries are allowed.</li> 
<li>If it requires a selection check the &quot;selection required&quot; box.</li>
<li>Create type.</li>
</ul>
    <br />
</div>
This new answer type can now be used in any new or existing question.<br />
<br />
<u>Answer Piping</u><br />
Answers from previous pages can be piped using the standard
[[pipealias]]] tags directly in the Sql query, so in the previous
example for example a form could be used that asks the user on the first
page for his/her country and it could then retrieve through piping on the
other pages the list of customers of the selected country.<br />
<br />
E.g. select customerid, customername from tbcustomer where country =
[[countryalias]]<br />
<br />
Its also possible to use other piping tags like :<br />
##yourquerystringvariablename##<br />
@@yoursessionvariablename@@<br />
&amp;&amp;yourcookievariablename&amp;&amp;<br />
%%servervariablename%%<br />
<br />
<u>Security considerations</u><br />
In some scenarios allowing select queries against a database can be a
security threat. By default all administrator accounts can create / change
sql based type.<br />
<br />
If a new user is created rights will have to be given explicitly to create sql based answer types. 
It is advised that if no sql based answer type right is given to consider to also remove Xml import rights
for the user as theoretically through changing the Xml file Sql code could be injected and run against the database<br />
<br />
To disable the SQL features for all administrators through the web.config go to setting SqlBasedAnswerTypesAllowed and set it to false.<br />
<br />

<hr style="color:#e2e2e2;" /> <br /><h3>More Information</h3>
                <br />
FBT_Introduction.html<br />
AT_Introduction.html<br />
Answers%20Editor.html<br />
                <br />
            </td>
        </tr>
    </table>
</div></div></asp:Content>

