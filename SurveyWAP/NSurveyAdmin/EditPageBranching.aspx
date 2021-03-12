<%@ Page language="c#" MasterPageFile="MsterPageTabs.master"   AutoEventWireup="false" Inherits="Votations.NSurvey.WebAdmin.EditPageBranching" Codebehind="EditPageBranching.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="PageBranchingRulesControl" Src="UserControls/PageBranchingRulesControl.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div id="Panel" class="Panel">

        <fieldset id="liML" runat="server">
            <asp:ImageButton ID="ImageButton1" ImageUrl="~/Images/back_button.gif" runat="server"
                CssClass="buttonBack" ToolTip="Click to go back" OnCommand="OnBackButton" />

            <div class="helpDiv">
                <a onmouseover='this.style.cursor="help" ' onfocus='this.blur();' href="Help/New/Branching conditions.aspx"
                    title="Click for more Information">
                    <img alt="help" border="0" src="<%= Page.ResolveUrl("~")%>Images/small_help.gif" />
                </a>
            </div>

        </fieldset>
        <div class="errorDiv">
            <asp:Label ID="MessageLabel" runat="server" CssClass="errorMessage" Visible="False"></asp:Label>
        </div>

        <fieldset>
            <legend class="titleFont titleLegend">
                <asp:Literal ID="AddNewBranchingTitle" runat="server" EnableViewState="False">Add new branching rule to page</asp:Literal>
            </legend>
            <br />

            <ol>
                <li>
                    <asp:Label ID="QuestionLabel" AssociatedControlID="QuestionFilterDropdownlist" runat="server" EnableViewState="False">Question :</asp:Label>
                    <asp:DropDownList ID="QuestionFilterDropdownlist" runat="server" AutoPostBack="True"></asp:DropDownList>
                </li>
                <li>
                    <asp:Label ID="ConditionalLabel" AssociatedControlID="LogicDropDownList" runat="server">Conditional operator :</asp:Label>
                    <asp:DropDownList ID="LogicDropDownList" runat="server" AutoPostBack="True"></asp:DropDownList>
                </li>
                <li>
                    <asp:Label ID="AnswerLabel" AssociatedControlID="AnswerFilterDropdownlist" runat="server">Answer :</asp:Label>
                    <asp:DropDownList ID="AnswerFilterDropdownlist" runat="server" AutoPostBack="True"></asp:DropDownList>
                </li>
                <li>
                    <asp:Label ID="TextEvaluationConditionLabel" AssociatedControlID="ExpressionLogicDropdownlist" runat="server">Text evaluation condition :</asp:Label>
                    <asp:DropDownList ID="ExpressionLogicDropdownlist" runat="server"></asp:DropDownList>
                </li>
                <li>
                    <asp:Label ID="FilterTextLabel" AssociatedControlID="TextFilterTextbox" runat="server">Text :</asp:Label>
                    <asp:TextBox ID="TextFilterTextbox" runat="server"></asp:TextBox>
                </li>
                <li>
                    <asp:Label ID="ScoreLabel" AssociatedControlID="ScoreTextbox" runat="server">Score :</asp:Label>
                    <asp:TextBox ID="ScoreTextbox" runat="server" Columns="2"></asp:TextBox>
                </li>
                <li>
                    <asp:Label ID="ScoreRangeLabel" AssociatedControlID="ScoreMaxTextbox" runat="server">between</asp:Label>
                    <asp:TextBox ID="ScoreMaxTextbox" runat="server" Columns="2"></asp:TextBox>
                </li>
                <li>
                    <asp:Label ID="PageTargetLabel" AssociatedControlID="PageTargetDropdownlist" runat="server">Go to page :</asp:Label>
                    <asp:DropDownList ID="PageTargetDropdownlist" runat="server"></asp:DropDownList>

                </li>
                <li>
                    <asp:Button ID="AddRuleButton" CssClass="btn btn-primary btn-xs bw" runat="server" Text="Add new rule" Enabled="False"></asp:Button><br />
                </li>
            </ol>
        </fieldset>


        <fieldset>
            <legend class="titleFont titleLegend">
                <asp:Literal ID="BranchingRulesTitle" runat="server" EnableViewState="False">Branching rules</asp:Literal>
            </legend>
            <br />

            <ol>
                <li>
                    <div class="scm" style="width: 60%;">
                        <asp:Literal ID="EvaluationConditionInfo" runat="server" EnableViewState="False">Conditions are evaluated from top to down with a "first condition met branch" rule</asp:Literal>
                    </div>
                    <br />

                    <uc1:PageBranchingRulesControl ID="PageBranchingRules" runat="server"></uc1:PageBranchingRulesControl>
                </li>
            </ol>
        </fieldset>
        <div id="fillerDiv" class="fillerDiv">&nbsp;</div>
    </div>
</asp:Content>