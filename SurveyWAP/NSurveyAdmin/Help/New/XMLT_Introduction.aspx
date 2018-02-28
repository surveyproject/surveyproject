<%@ Page Language="c#" MasterPageFile="~/nsurveyadmin/MsterPageTabs.master" AutoEventWireup="false"
    Inherits="Votations.NSurvey.WebAdmin.HelpFiles" CodeBehind="../default.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><div id="helpPanel"><div>
                <asp:ImageButton ID="btnBack" ImageUrl="~/Images/index-icon.png" runat="server" CssClass="buttonIndex"
                    PostBackUrl="~/NSurveyAdmin/Help/default.aspx#XmlBoundTypes" Visible="True" ToolTip="Back to Helpfiles Index" />
            </div><div>
                <h2 style="color:#5720C6;">
                    XML Bound Answertypes Introduction</h2><hr style="color:#e2e2e2;" />
       
XML bound answertypes are linked to an XML file on the webserver.<br />
<br />
An Xml bound answertype will read the content of the Xml file to which it is bound and show
the choices inside a dropdown list. It is possible to create and bind
new Xml bound answertypes to a custom Xml file using the Answer Type Creator.<br />
<br />
The following default xml bound answertypes are available :<br />
<br />
* XML Countries list<br />
* XML US States list<br />
<br />
The XML file names and extentions are in part based on the option to create different language versions of each file. SP&trade; will 
                select a particular Xml file from the server based on the language it is running in (browser based or other) before reading
its content. 
                
                <br /><br />
                E.g.: If an Xml answertype is bound to a file called
&quot;country.xml&quot; this will be the default file to read if no other language versions of the xml file are available.
                If switching to French for
example another Xml file called &quot;country.fr.xml&quot; if available will
be automatically read when the survey is in French.<br /><br /> This way it is
possible to provide a translated version of a single Xml bound type to
different audiences. <br /> <br />
                
                <hr style="color:#e2e2e2;" /><h3>More Information</h3><br />
                
<a href="AT_Introduction.aspx" title=" Answer Types Introduction " >Answer Types Introduction </a>	<br />
                <a href="Answer Type Creator.aspx" title=" Answer Type Creator " >Answer Type Creator </a>	<br />
<a href="Xml - Country.aspx" title=" Xml - Country " > Xml - Country </a>	<br />
<a href="Xml - US States.aspx" title=" Xml - US States " > Xml - US States </a>	<br />
                <a href="Subscriber - Xml List.aspx" title=" Subscriber - Xml List " > Subscriber - Xml List </a>	<br /><br />
            </div>
<div id="fillerDiv" class="fillerDiv">&nbsp;</div></div></asp:Content>

