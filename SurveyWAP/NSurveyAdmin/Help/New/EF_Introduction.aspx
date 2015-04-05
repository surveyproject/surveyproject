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
                <h2 style="color:#5720C6;">Extended Filters</h2>
                <br />
                <br />
                <hr style="color:#e2e2e2;"/>
                <br />

Extended filters are available when you have marked at least one
AT_Introduction.html to act as an extended filter.<br />
<br />
Once you have marked an answer item you will be able to select, from a
dropdown list that contains all respondent text answers, the one that you
want to use as a filter for your results.<br />
<br />
As an example we could use the extended filters features along with the
Xml%20-%20Country.htmltype to create geographic / demographic filters
that will allows us to filter our report based on countries selected by
the respondents.<br />
<br />
Here is how the extended filters would look like with the example above.<br />
<br />
As you can see we can choose to set a filter based on &quot;live&quot; respondent's
answers. Here we have set country and region to act as an extended
filter. If we would set these filters the report would show all
respondents answers from whom answered CH to country and Geneva to region.<br />
<br />
We could have as many extended filters as we want. Each answer filter
will be populated according to the other selections, the group of all
filters must represent a valid combination of at least one respondent.<br />
<br />
As extended filters are based on respondents answers it is highly
recommended to create and apply the filters once you have finished the
survey as this will give us the widest choice of filters available.<br />
<br />

                <hr style="color:#e2e2e2;"/>
                <br />
                <br />
                <h3>
                    More Information</h3>
                <br />
AT_Introduction.html<br />
Answers%20Editor.html<br />
                <br />
            </td>
        </tr>
    </table>
</div></div></asp:Content>

