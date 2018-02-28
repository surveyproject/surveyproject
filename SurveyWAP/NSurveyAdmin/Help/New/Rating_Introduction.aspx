<%@ Page Language="c#" MasterPageFile="~/nsurveyadmin/MsterPageTabs.master" AutoEventWireup="false"
    Inherits="Votations.NSurvey.WebAdmin.HelpFiles" CodeBehind="../default.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><div id="helpPanel"><div>
                <asp:ImageButton ID="btnBack" ImageUrl="~/Images/index-icon.png" runat="server" CssClass="buttonIndex"
                    PostBackUrl="~/NSurveyAdmin/Help/default.aspx#Rating" Visible="True" ToolTip="Back to Helpfiles Index" />
            </div><div>
                <h2 style="color:#5720C6;">
                    Rating Introduction</h2><hr style="color:#e2e2e2;" />
        
This introduction will explain how to use and setup rating
features on a survey and how rating is presented in the survey's
reports.<br />
<br />
Rating makes it possible to add metrics to a question by
activating a 'value' (rating) for each answer from which a 'mean value' will be calculated in
the result reports. During a campaign this provides the option to have a quick view on how a question is
'rated' by the respondents.<br />
<br />
<i>An example 'rating' question :</i><br />
<br />
<mark>How do you rate SP&trade;'s rating feature ?<br />
- very bad<br />
- bad<br />
- good<br />
- very good<br />
- n / a<br /></mark>
<br />
By activating rating on the question using the 'Question Editor' it is possible to
have a rating number set to each answer that is added to the question. Each answer for which the rating option is 
checked on the Answer Editor will have a value
assigned by the survey's webcontrol based on its position (!) in the question and
a 'mean rating' will be calculated based on these values and the respondent
answers.<br />
<br />
In the example above if activating the rateting for each answer the values assigned would be:<br />
<br /><mark>
How do you rate SP&trade;'s rating feature ?<br />
- very bad (rating value 1)<br />
- bad (rating value 2)<br />
- good (rating value 3)<br />
- very good (rating value 4)<br />
- n / a (rating value 0)<br /></mark>
<br />
1 being the worsed / lowest value in the rating calculation and 4 being
the best / highest value in the calculating, n/a has a rating value 0
because the 'rating' option was not checked as it should not be part
of the calculation.<br />
<br />
It is not possible to 'revert' the rating values defined by the survey for each
answer eg: very good, good, bad, very bad would results in very good
(rating 1), good (rating 2), bad (rating 3), very bad (rating 4). The number of answers and the order and position of the answers determine the rating values.
<br />
<br />
Once respondents have submitted their answers the reporting engine
will be able to calculate the mean rating of the question based on the rating weight of each answer.<br />
<br />

                <hr style="color:#e2e2e2;"/>
                <h3>
                    More Information</h3>
                <br />
     <a href="Question Editor.aspx" title=" Question Editor " > Question Editor </a><br />
                   	<a href="../Score_Introduction.aspx" title=" Score Introduction " > Score Introduction </a>	<br />
                <br />
            </div>
<div id="fillerDiv" class="fillerDiv">&nbsp;</div></div></asp:Content>

