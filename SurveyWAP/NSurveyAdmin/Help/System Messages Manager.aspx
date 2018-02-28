<%@ Page Language="c#" MasterPageFile="~/nsurveyadmin/MsterPageTabs.master" AutoEventWireup="false"
    Inherits="Votations.NSurvey.WebAdmin.HelpFiles" CodeBehind="default.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><div id="helpPanel"><div>
                <asp:ImageButton ID="btnBack" ImageUrl="~/Images/index-icon.png" runat="server" CssClass="buttonIndex"
                    PostBackUrl="~/NSurveyAdmin/Help/default.aspx#MLSurveys" Visible="True" ToolTip="Back to Helpfiles Index" />
            </div><div>
                <h2 style="color:#5720C6;">
                    System Messages & Multiple Languages</h2><hr style="color:#e2e2e2;" />
           
                All system messages and screen texts (for both the administration tool and survey controls) can
                be adjusted or translated as these messages are stored in an external
                XML file located by default in the &quot;XmlData/Languages/&quot; directory of the
                webapplication. 
                <br /><br />
                Each language has its own XML language files with the messages and texts
                in that language. By default Survey&trade; Project comes with a complete set of English and Dutch
                XML files.<br />
                <br />
                A new language XML file can be created manually by copying the default en-US.xml
                file and renaming the copy to a local language code and translate the values inside
                it.<br />
                <br />
                As the XML file also contains the survey web control's system messages like error
                messages or submit button texts (".. is required", "next page" etc...) these messages
                can be changed by changing the current XML language file used. However it is important to
                note that these changes will affect all SP&trade; users.
                <br />
                <br />
                By default the language setting of the webbrowser determine the XML languagefiles used to present
                the webpage texts. This effect can be changed in the web.config file section: culture/
                uiculture. If set to "auto" SP™ will pick up the culture/ language of the browser
                settings. To explicitely pick a specific culture/ language, replace "auto" with
                "en-US" or "nl-NL" for example.
                                <br />
                <br />Culture settings can be changes throught the Surveys/Settings/System Settings menu.

                <br />
                <br />
                Some languages can have regional codes like &quot;en-US&quot; for US English. If
                SP&trade; cannot find a matching 2 letter code for a given language it will try to
                match the single letter code like &quot;en&quot; for English codes.<br />
                <hr style="color:#e2e2e2;" />
                <h3>
                    More Information</h3>
                <br />
                <a href="New\ML_Introduction.aspx" target="_self">Multilanguage Introduction</a><br />
                <a href="Multi-Languages%20Settings.aspx" target="_self">Multilanguage Settings</a>
                <br />

            </div>
<div id="fillerDiv" class="fillerDiv">&nbsp;</div></div></asp:Content>
