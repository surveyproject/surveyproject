<%@ Page Language="c#" MasterPageFile="~/nsurveyadmin/MsterPageTabs.master" AutoEventWireup="false"
    Inherits="Votations.NSurvey.WebAdmin.HelpFiles" CodeBehind="../default.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><div id="helpPanel"><div>
                <asp:ImageButton ID="btnBack" ImageUrl="~/Images/index-icon.png" runat="server" CssClass="buttonIndex"
                    PostBackUrl="~/NSurveyAdmin/Help/default.aspx#SelectionTypes" Visible="True" ToolTip="Back to Helpfiles Index" />
            </div><div>
                <h2 style="color:#5720C6;">
                    Selection Answer Types Introduction</h2><hr style="color:#e2e2e2;" />
       
           
The way Selection Answer types will render on the survey form depends on the selection mode that was
chosen in the question editor. The Question level selection mode determines if  
the answerstype 'Selection - Text' and 'Selection - Other' will be rendered as a radio button, a checkbox or grouped in a dropdown list.<br />
<br />
The following Selection Answer types are available :<br />
<br />
* Selection - Text (radiobutton, checkbox or DDL)<br />
* Selection - Other (additional textbox field)<br />
                <hr style="color:#e2e2e2;" />        
                <h3>
                    More Information</h3>
                <br />
<a href="Selection - Text.aspx" title=" Selection - Text " > Selection - Text </a>	<br />
<a href="Selection - Other.aspx" title=" Selection - Other " > Selection - Other </a>	<br />                <br />
            </div>
<div id="fillerDiv" class="fillerDiv">&nbsp;</div></div></asp:Content>

