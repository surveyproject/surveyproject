<%@ Register TagPrefix="vts" Namespace="Votations.NSurvey.WebControls" Assembly="SurveyProject.WebControls" %>

<%@ Page Language="c#" MasterPageFile="~/NSurveyAdmin/MsterPageTabs.master" ValidateRequest="false"
    AutoEventWireup="false" Inherits="Votations.NSurvey.WebAdmin.ControlCode" CodeBehind="ControlCode.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

        <div id="Panel" class="Panel">
            <div class="errorDiv">
 <asp:Label ID="MessageLabel" runat="server"  CssClass="errorMessage" Visible="False"></asp:Label>
                </div>

                                                <div class="helpDiv">
                                            <a onmouseover='this.style.cursor="help" ' onfocus='this.blur();' href="Help/new/Survey%20Deployement.aspx"
                                                title="Click for more Information">
                                                <img alt="help" border="0" src="<%= Page.ResolveUrl("~")%>Images/small_help.gif" />
                                            </a>
                                        </div>

  
    <fieldset>
        <legend class="titleFont titleLegend">
            <asp:Literal ID="SurveyUrl" runat="server" EnableViewState="False">Survey Hyperlinks</asp:Literal>
        </legend><br />
        <ol>
            <li>
                <div class="scm">
                    <asp:Literal ID="QuickLinkInfo" runat="server" EnableViewState="False">
                Deployment URL:</asp:Literal>
                </div>
                <br />
                
                <asp:HyperLink ID="CodeMobileHyperLink" Target="_blank" runat="server">MobileHyperLink</asp:HyperLink>
                <br />
                &nbsp;
            </li>
            <li>
                <div class="scm">
                    <asp:Literal ID="friendlyLabel" runat="server">Create Friendly Name:</asp:Literal></div>

                <div style="float: left; margin-left: 225px; position: relative; top: -19px; width: 440px;">
                    <asp:TextBox ID="txtFriendly" runat="server"></asp:TextBox></div>
                <div style="float: right; position: relative; top: -37px; margin-right: 65px;">
                    <asp:Button ID="btnFriendly" CssClass="btn btn-primary btn-xs bw" runat="server" />
                </div>
                <asp:Literal ID="FriendlyUrl" runat="server" EnableViewState="False">Note: Friendly Url format = http:// [YourWebSiteUrl] / surveymobile.aspx / [<b>YourFriendlyName</b>]</asp:Literal>
            </li>
            <li id="fuID" runat="server">
                            
                <div class="scm">
                    <asp:Literal ID="friendlyUrlLabel" runat="server" EnableViewState="False">
                Friendly URL:</asp:Literal>
                </div>
                <br />
                <asp:HyperLink ID="friendlyMobileUrlLink" Target="_blank" runat="server">MobileHyperLink</asp:HyperLink><br />
                <asp:Button ID="btnDeleteFriendly" CssClass="btn btn-primary btn-xs bw" runat="server" />

                <br />

            </li>
        </ol>
        <br />
    </fieldset>

            <div class="helpDiv">
                <a onmouseover='this.style.cursor="help" ' onfocus='this.blur();' href="Help/new/Web%20Control%20Deployment.aspx"
                    title="Click for more Information">
                    <img alt="help" border="0" src="<%= Page.ResolveUrl("~")%>Images/small_help.gif" />
                </a>
            </div>

    <fieldset>
        <legend class="titleFont titleLegend">
            <asp:Literal ID="ControlCodeTitle" runat="server" EnableViewState="False">Webcontrol asp.net starting code</asp:Literal></legend>
        <br />
        <ol>
            <li>
                <asp:Literal ID="WebControlIntro" runat="server" EnableViewState="false">Introdution to using the custom webcontrol</asp:Literal>
            </li>
                        <li>
                <asp:Literal ID="PageDirectiveInfo" runat="server" EnableViewState="False">Include the following directive in the aspx pages that will display the survey :</asp:Literal>
                <br />
                <span class="RegisterLiteral">
                    <asp:Literal ID="TagPrefixInfo" runat="server" EnableViewState="False">
                        &lt;%@Register TagPrefix="vts" Namespace="Votations.NSurvey.WebControls" Assembly="SurveyProject.WebControls" %&gt;
                    </asp:Literal>
                </span>
                &nbsp;
            </li>
            <li>
        <textarea id="taCode" runat="server" style="width: 685px; font-size:0.9em; border: 10px solid white;" rows="45" cols="85">
        &lt;!-- Add a CSS file to the survey by creating a stylesheet link in the webpage header. E.g. --&gt;
        &lt;head runat="server"&gt;
        &lt;link href="{0}" type="text/css" rel="stylesheet" /&gt;
        &lt;/head&gt;

        &lt;!-- Minimum code added to webpage body. Set surveyid to id of survey that needs to be shown on the page. --&gt;
        &lt;!-- SurveyID below is from the currently selected survey. --&gt;
        &lt;body&gt;
        &lt;form id="form1" runat=""server""&gt;

        &lt;vts:SurveyBox ID="SurveyControl" Surveyid="{1}" CssClass="surveybox" EnableValidation="true" runat="server" &gt;

        &lt;QuestionStyle  CssClass="questionStyle"&gt;&lt;/QuestionStyle&gt;

        &lt;QuestionValidationMessageStyle CssClass="qvmStyle" &gt;&lt;/QuestionValidationMessageStyle&gt;
        &lt;QuestionValidationMarkStyle CssClass="icon-warning-sign" &gt;&lt;/QuestionValidationMarkStyle&gt;

        &lt;ConfirmationMessageStyle CssClass="cmStyle" &gt;&lt;/ConfirmationMessageStyle&gt;

        &lt;SectionOptionStyle CssClass="soStyle"&gt;&lt;/SectionOptionStyle&gt;

        &lt;ButtonStyle CssClass="btn btn-primary btn-xs bw"&gt;&lt;/ButtonStyle&gt;

        &lt;AnswerStyle CssClass="answerStyle"&gt;&lt;/AnswerStyle&gt;

        &lt;MatrixStyle CssClass="matrixStyle" &gt;&lt;/MatrixStyle&gt;
        &lt;MatrixHeaderStyle CssClass="mhStyle"&gt;&lt;/MatrixHeaderStyle&gt;
        &lt;MatrixItemStyle CssClass="miStyle" &gt;&lt;/MatrixItemStyle&gt;
        &lt;MatrixAlternatingItemStyle CssClass="maiStyle"&gt;&lt;/MatrixAlternatingItemStyle&gt;

        &lt;SectionGridAnswersItemStyle CssClass="sgiStyle" &gt;&lt;/SectionGridAnswersItemStyle&gt;
        &lt;SectionGridAnswersAlternatingItemStyle CssClass="sgaaisStyle" &gt;&lt;/SectionGridAnswersAlternatingItemStyle&gt;
        &lt;SectionGridAnswersStyle CssClass="sgaStyle" &gt;&lt;/SectionGridAnswersStyle&gt;
        &lt;SectionGridAnswersHeaderStyle CssClass="sgahStyle"&gt;&lt;/SectionGridAnswersHeaderStyle&gt;

        &lt;FootStyle CssClass="footStyle" &gt;&lt;/FootStyle&gt;

        &lt;/vts:SurveyBox&gt;
        &lt;/form&gt;

        &lt;!-- Add the following script at the bottom of the page: --&gt;
       &lt;script src="&lt;%=Page.ResolveUrl("~/Scripts/bootstrap.min.js")%&gt;"&gt;&lt;/script&gt;
        &lt;/body&gt;
        </textarea>
            </li>
        </ol>
        <br />
    </fieldset>
                            <div id="fillerDiv" class="fillerDiv">&nbsp;</div>
</div>
</asp:Content>