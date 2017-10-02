<%@ Page Language="c#" MasterPageFile="~/nsurveyadmin/MsterPageTabs.master" AutoEventWireup="false"
    Inherits="Votations.NSurvey.WebAdmin.HelpFiles" CodeBehind="../default.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><div id="helpPanel"><div>
                <asp:ImageButton ID="btnBack" ImageUrl="~/Images/index-icon.png" runat="server" CssClass="buttonIndex"
                    PostBackUrl="~/NSurveyAdmin/Help/default.aspx#ConditionalLogic" Visible="True" ToolTip="Back to Helpfiles Index" />
            </div><div>
                <h2 style="color:#5720C6;">
                    Skip Logic Conditions</h2><hr style="color:#e2e2e2;" />
               
A Skip logic condition is a set of one or more logical rules based on a surveys answers that will 'skip' (hide) a question to the respondent.<br />
<br />
To use skip logic features at least two pages in a survey are required as a question can only be
hidden based on answers of previous pages. It cannot be based on answers on the current page
of the question. Skip logic can not be used on the first page of a survey.<br />
<br />
Each condition is based on a set of rules that can be defined. The first
condition that is met will hide the question to which the skip logic
applies. An unlimited number of conditions are possible and can be ordered or
re-ordered at any time.<br />
<br />
To learn more about conditions and how they work and how to use them read the Conditional Logic Introduction.<br />
<br />

                <hr style="color:#e2e2e2;"/>

                <h3>
                    More Information</h3>
                <br />
                <a href="Condition_Introduction.aspx" title=" Conditional Logic Introduction " > Conditional Logic Introduction </a>	<br />
<a href="Branching conditions.aspx" title=" Branching Conditions " > Branching Conditions </a>	<br />
                <a href="../Score_Introduction.aspx" title=" Score Introduction " > Score Introduction </a>	<br />
                <a href="../Completion Actions.aspx" title=" Completion Actions " > Completion Actions </a>	<br />
                <br />
            </div>
<div id="fillerDiv" class="fillerDiv">&nbsp;</div></div></asp:Content>

