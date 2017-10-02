<%@ Page Language="c#" MasterPageFile="~/nsurveyadmin/MsterPageTabs.master" AutoEventWireup="false"
    Inherits="Votations.NSurvey.WebAdmin.HelpFiles" CodeBehind="../default.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><div id="helpPanel"><div>
                <asp:ImageButton ID="btnBack" ImageUrl="~/Images/index-icon.png" runat="server" CssClass="buttonIndex"
                    PostBackUrl="~/NSurveyAdmin/Help/default.aspx#QuestionLibrary" Visible="True" ToolTip="Back to Helpfiles Index" />
            </div><div>
                <h2 style="color:#5720C6;">
                   Questions Library List & New Library</h2><hr style="color:#e2e2e2;" />
              
The Question Library pages enables the creation of new question libraries where questions can be archived and sorted by theme or topic for later
                 use in new or existing surveys.<br />
<br />
<u>New Library Tab</u> <br />- option to create a new library directory. Only one level directories are supported, it is not possible to
  create sub-directories.<br />
<br />
<i>* Libray Name </i><br />- the name of the name of library as shown to the users. <br />
<br />
<i>* Description </i><br />-- optional general description of content and use of library

                <br />
<br />
<u>Library List Tab</u>
                                                <br />
<br />
<i>* Libray Name </i><br />- click name to add/ edit questions 
                                <br />
<br />
<i>* Edit </i><br /> - option to change the library name, description, set multilanguage options and delete the library.<br />
<br />

                <u>Library Edit page</u>
                <br /><br />
                <i>* Libray Name </i><br />- option to edit the name of the name of library as shown to the users. <br />
<br />
<i>* Description </i><br />- option to edit the description of content and use of library
                <br /><br />
<i>* Enabled Languages </i><br />- option to create multilanguages Library

<br />
<br />
<i>* Default Language</i><br />- Primary language for the Question Library in case of Multilanguage questions.
<br />
<br />

<i>* Delete Library button</i> <br /> will delete the directory including all its question  templates.<br /><hr style="color:#e2e2e2;" />
   
                <h3>
                    More Information</h3>
                <br />
<a href="QL_Introduction.aspx" title=" Question Library Introduction " >Question Library Introduction </a>	<br />
<a href="Library Templates.aspx" title=" Library Templates " > Library Templates </a>	<br />
                <br />
            </div>
<div id="fillerDiv" class="fillerDiv">&nbsp;</div></div></asp:Content>

