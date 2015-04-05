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
                    Field - Constant Sum</h2>
                <br />
                <br />
                <hr style="color:#e2e2e2;"/>
                <br />

Constant Sum is a method that will allow us to setup and group multiple
constant sum fields together to ask the respondent to enter a values that
will match the total value we've specified.<br />
<br />
In the example above we have a question with three constant sum fields
linked using the connections of the type settings. The respondent will
need to answer each field with a number and the total of all fields must
reach 100.<br />
<br />
In order to make constant sum work we always need to have a &quot;master&quot;
ranking type that will manage the values of the group of constant sum
fields.<br />
<br />
In this case the &quot;master&quot; type is &quot;Quality&quot;. &quot;Quality&quot; has subscribed to
the other constant sum types value using the connections of the type
settings. We have also set the total of the sum to reach from the
extended settings of the &quot;Quality&quot; type. To learn more about ranking we
suggest watching the Constant%20Sum%20Tutorial.html.<br />
<br />
The field is not mandatory by default, if you want to make it mandatory
you make check the mandatory option in the type settings.<br />
<br />
Type Settings<br />
<br />
* Answer Text is the text that will be shown next to the field. <br />
<br />
* Image URL we can give an image URL
  (<a href="http://www.mydomain.com/myimage.gif" target="_blank">http://www.mydomain.com/myimage.gif</a>) in order to show it instead of
  showing the answer text. <br />
<br />
* Type allows us to change the current type of the answer we want in
  our survey. Respondent answers already collected will not be deleted if
  we change the type.<br />
<br />
* RegEx Server Side Validation allows us to validate the content of the
  textbox field using a regular expression. Several regular expression
  validation are provided out of the box and it is very easy to create
  new one using the Regular%20Expression%20Editor.html. This check is
  done server-side.<br />
<br />
* Mandatory allows us to set if the textbox field requires to be
  filled. This check is done server-side.<br />
<br />
* Default Text Value allows us to set a default value inside the
  textbox field. It is also possible to fill the textbox with a default
  value coming from an external source using following tags :<br />
<br />
  ##yourquerystringvariablename## will set the default text with a query
  string variable's value.<br />
  @@yoursessionvariablename@@ will set the default text with a session
  variable's value.<br />
  &amp;&amp;yourcookievariablename&amp;&amp; will set the default text with a cookie
  variable's value.<br />
  %%servervariablename%% will set the default text with a server side
  variable's value.<br />
<br />
* Pipe Alias allows us to specify an alias that we can use in other
  questions to pipe the textbox field value. To learn about piping and
  pipe alias we suggest reading the Piping_Introduction.html.<br />
<br />
* Reporting Alias is the text that can be shown instead of the answer
  text inside our reports.<br />
<br />
* Extended Report Filter we will be able to use directly the text
  answers of the respondent as a filter in the
  Report%20General%20Settings.html.To learn more about piping and
  extended report filters we suggest reading the EF_Introduction.html.<br />
<br />
* Connections will let us choose with which child constant sum type
  items this type will be linked to. To learn more about constant sums we
  suggest watching the Constant%20Sum%20Tutorial.html.<br />
<br />
Extended Type Settings<br />
Extended type settings are extra specific properties of a type that are
only availble once a type has been created.<br />
<br />
To edit the extended settings of a type we will have to click on the edit
button of the answer overview once we've added our type to our question
or changed an existing type to a new one by clicking the update button.<br />
<br />
* Constant Sum To Reach is the total that need to be reach across all
  constant sum types that are linked to the master constant sum type.
<br />
<br />
                <hr style="color:#e2e2e2;"/>
                <br />
                <br />
                <h3>
                    More Information</h3>
                <br />
FBT_Introduction.html<br />
Constant%20Sum%20Tutorial.html<br />
AT_Introduction.html<br />
Answer%20Type%20Creator.html<br />
EF_Introduction.html<br />
Answers%20Editor.html<br />
                <br />
            </td>
        </tr>
    </table>
</div></div></asp:Content>

