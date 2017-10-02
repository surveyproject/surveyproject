<%@ Page Language="c#" MasterPageFile="~/nsurveyadmin/MsterPageTabs.master" AutoEventWireup="false"
    Inherits="Votations.NSurvey.WebAdmin.HelpFiles" CodeBehind="default.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><div id="helpPanel"><div>
                <asp:ImageButton ID="btnBack" ImageUrl="~/Images/index-icon.png" runat="server" CssClass="buttonIndex"
                    PostBackUrl="~/NSurveyAdmin/Help/default.aspx#MLSurveys" Visible="True" ToolTip="Back to Helpfiles Index" />
            </div><div>
                <h2 style="color:#5720C6;">
                    Multi Language Settings</h2><hr style="color:#e2e2e2;" />
        
                By using the Multi-language Settings a survey can be set up to create and support different language versions.
                Go to menuoption Surveys/Settings/ Multi Languages to activate these features:<br />
                <br />

                <b>Enable Multi-Languages</b><br />
                Checkbox to enabe support for multiple languages for a survey.
                <br />
                <br />

                <b>Multi-Languages Mode</b><br />
                These are the options to determine the language of the survey. It can be selected by the respondent
                or is determined by the survey tool. Survey provides several ways of choosing/ determining
                the language of a survey at runtime.<br />
                <br />

                o <i>User List Selection</i><br />
                The respondent will be able to choose the language in which the survey is shown
                from a dropdown list that is presented at the start of the survey.<br />
                <br />

                o <i>Detect From Browser</i><br />
                Survey will determine the default language of the repondents browser and will switch to it
                if its language is available in the survey.<br />
                <br />

                o <i>Get From QueryString</i><br />
                Survey will get the language of the query-string variable from the survey URL and will switch to it
                if its language is available in the collection of survey xml language files.<br />
                <br />
                Steps:<br />
- go to the multilanguage menu page<br />
- select ML mode: "get from querystring"<br />
- you can now enter a variable name (e.g. lang) that must be added to the survey url and which will hold the preferred language (eg. nl-NL)<br />
- next save the changes<br /><br />

To make use of the variable the survey url should look something like:<br />
http://yourwebsite/surveymobile.aspx?surveyid=836aae1e-f2bb-451f-89d6-63b55e7f9318&lang=nl-NL


                <br />
                <br />
                o <i>Get From Cookie</i><br />
                Survey will get the language of the cookie variable and will switch to it if its
                language is available in the survey.<br />
                <br />
            
                o <i>Get From Session</i><br />
                Survey will get the language of the session variable and will switch to it if its
                language is available in the survey.<br />
                <br />
            
                <b><i>Enabled Languages</i></b><br />
                These are the languages that will be available to the respondent and to the survey
                administrator at edit time.<br />
                <br />
           
                <b>Default Language</b><br />
                This will set the default language. It is the default language in which a survey has been
                created and that is used if no other language is selected.<br />
                <br />
                <hr style="color:#e2e2e2;"/>
                <h3>
                    More Information</h3>
                <br />
                <a href="New\ML_Introduction.aspx" target="_self">Multilanguage Introduction</a><br />
                <a href="System%20Messages%20Manager.aspx" target="_self">System Messages & Multiple Languages</a><br />
                <br />
                <br />
            </div>
<div id="fillerDiv" class="fillerDiv">&nbsp;</div></div></asp:Content>
