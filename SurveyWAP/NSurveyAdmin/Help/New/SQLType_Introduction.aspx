<%@ Page Language="c#" MasterPageFile="~/nsurveyadmin/MsterPageTabs.master" AutoEventWireup="false"
    Inherits="Votations.NSurvey.WebAdmin.HelpFiles" CodeBehind="../default.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><div id="mainBody" class="mainBody contentHolder ps-container"><div id="Panel" class="Panel content">
    <table class="TableLayoutContainer">
        <tr>
            <td>
                <asp:ImageButton ID="btnBack" ImageUrl="~/Images/index-icon.png" runat="server" CssClass="buttonIndex"
                    PostBackUrl="~/NSurveyAdmin/Help/default.aspx" Visible="True" ToolTip="Back to Helpfiles Index" />
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
Sql based query answer types are another powerful feature of Survey.<br />
<br />
Using Sql based type you can expose and use existing data directly in
your survey.<br />
At this time the data will be exposed as a dropdown list, you can choose
if you want to have it mandatory or not. By default there aren't any out
of the box Sql based answer types as these are related to your own
database.<br />
<br />
If you whish to create your own Sql type you will need to :<br />

<ol>
<li>Go to the form builder / answer type editor section.</li>
<li>Click on create new type</li>
<li>Give it a new eg : Customer list</li>
<li>Select Sql query as a datasource</li>
<li>Enter your Sql query eg: select customerid, customername from</li>
</ol>
   tbcustomers<br />
   Only Sql &quot;select&quot; based queries are allowed.<br />
6. If it requires a selection check the &quot;selection required&quot; box.<br />
7. Create type.<br />
<br />
You can now use this new answer type in any new or existing question.<br />
<br />
Answer Piping<br />
You can pipe answers from previous pages using the standard
[[pipealias]]] tags directly in your Sql query, so in our previous
example we could have for example a form that ask the user on the first
page for his country and we could then retrieve through piping on the
other pages the list of customers of the selected country.<br />
<br />
eg : select customerid, customername from tbcustomer where country =
[[countryalias]]<br />
<br />
Its also possible to use other piping tags like :<br />
##yourquerystringvariablename##<br />
@@yoursessionvariablename@@<br />
&amp;&amp;yourcookievariablename&amp;&amp;<br />
%%servervariablename%%<br />
<br />
Security considerations<br />
In some scenarios allowing select queries against a database can be a
security threat. By default all Survey administrator can create / change
sql based type.<br />
<br />
If you create a new user you will need to give him explicitly the rights
to create sql based answer type, its strongly recommended that if you
don't give sql based answer type right to also remove Xml import rights
for the user as a user could change the Xml file and inject his own Sql
code.<br />
<br />
You can also disable the feature for all admin from the web.config by
setting the SqlBasedAnswerTypesAllowed to false.<br />
<br />

                <hr style="color:#e2e2e2;"/>
                <br />
                <br />
                <h3>
                    More Information</h3>
                <br />
FBT_Introduction.html<br />
AT_Introduction.html<br />
Answers%20Editor.html<br />
                <br />
            </td>
        </tr>
    </table>
</div></div></asp:Content>

