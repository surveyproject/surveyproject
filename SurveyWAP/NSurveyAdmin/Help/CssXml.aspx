<%@ Page Language="c#" MasterPageFile="~/nsurveyadmin/MsterPageTabs.master" AutoEventWireup="false" Inherits="Votations.NSurvey.WebAdmin.HelpFiles" Codebehind="default.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><div id="helpPanel"><div>
                <asp:ImageButton ID="btnBack" ImageUrl="~/Images/index-icon.png" runat="server" CssClass="buttonIndex"
                    PostBackUrl="~/NSurveyAdmin/Help/default.aspx#SurveyLayout" Visible="True" ToolTip="Back to Helpfiles Index" />
            </div><div>
            <h2 style="color:#5720C6;">Css Xml Layout Options</h2><hr style="color:#e2e2e2;" />
          
            As part of the different features to determine the look and feel of the surveyform as it is presented to respondents 
            a list of CSS/ XML entries has been created.
            <br /> <br />
            Through this list the "CSS Selectors" column can be edited. The CSS Selector or CSS Class names refer to the selectors and classes 
            as defined in the CSS files used with a particular survey (default or custom). CSS files can be found in the Content directories.
            <br /><br />
            If a CSS Selectors is added (multiple selectors per item are possible) it's CSS rules will be applied to the part of the surveypage that can be recognised by the ID that is mentioned in the ObjectID column. 
             <br /><br />
            Different parts of the survey webpage and surveycontrol have been given ID's that can be used in client-side scripting (e.g. Javascript or Jquery) or to apply specific CSS rules to a page element or object. 
            The Name Column briefly describes the particular element and its feature/ function.
            <br /><br />
            When rendered on the webpage an ID is proceeded by "SurveyControl_". ID's can be found in the browsers HTML code of the webpage (click F12 button).
            <br /><br />
            <u>Radiobuttion Labeltext Alignment</u>
            <br /><br />
            One special entry in the list called 'GlobalRadioButtonTextAlign' is used to determine the left or right position of the radiobutton labeltext. Instead of referring to a CSS class or selector
            the entry is picked at runtime to present the label left or right of the radiobutton. 
            <br /><br />
            Options to set the alignment are: 'Left' or 'Right' (case sensitive). 

 <br />
                <br /><hr style="color:#e2e2e2;" />
                <h3>
                    More Information</h3>
                <br />
                <a href="Web%20Control%20Style.aspx" target="_self">Web Control CSS Style</a><br />
                <a href="Style_Introduction.aspx" target="_self">Layout & Style Introduction</a><br />
                <br />
                    </div>
<div id="fillerDiv" class="fillerDiv">&nbsp;</div></div></asp:Content>
