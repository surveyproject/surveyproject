<%@ Page Language="c#" MasterPageFile="~/nsurveyadmin/MsterPageTabs.master" AutoEventWireup="false"
    Inherits="Votations.NSurvey.WebAdmin.HelpFiles" CodeBehind="../default.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><div id="helpPanel"><div>
                <asp:ImageButton ID="btnBack" ImageUrl="~/Images/index-icon.png" runat="server" CssClass="buttonIndex"
                    PostBackUrl="~/NSurveyAdmin/Help/default.aspx#ConditionalLogic" Visible="True" ToolTip="Back to Helpfiles Index" />
            </div><div>
                <h2 style="color:#5720C6;">
                    Branching Conditions</h2><hr style="color:#e2e2e2;" />
             
A Branching condition is a set of one or more  logical rules based on a surveys answers to 'branch' to a specific page in a
survey or even terminate the survey when a particular answers meets any of the rules.<br />
<br />
As branching conditions are a feature related to 'skipping' pages at
least two pages have to be created in a survey to be able to use the branching features. Its
not possible to use branching on the last page of the survey.<br />
<br />
Each condition is based on a set of rules that can be defined. The first
condition that will be met will cause the survey to 'branch' off to the page that is defined in the
branching editor. An unlimited number of conditions is available which can be ordered
or re-ordered at any time.<br />
<br />
To learn more about conditions and how they work and how to use them read the Conditional Logic Introduction.
<br /><br />

                <hr style="color:#e2e2e2;"/>
                <h3>
                    More Information</h3>
                <br />
                <a href="Condition_Introduction.aspx" title=" Conditional Logic Introduction " > Conditional Logic Introduction </a>	<br />
<a href="Skip Logic Conditions.aspx" title=" Skip Logic Conditions " > Skip Logic Conditions </a>	<br />
                <a href="../Score_Introduction.aspx" title=" Score Introduction " > Score Introduction </a>	<br />
                <a href="../Completion Actions.aspx" title=" Completion Actions " > Completion Actions </a>	<br />
                <br />
            </div>
<div id="fillerDiv" class="fillerDiv">&nbsp;</div></div></asp:Content>

