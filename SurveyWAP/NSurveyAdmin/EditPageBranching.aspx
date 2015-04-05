<%@ Register TagPrefix="uc1" TagName="FilterOptionControl" Src="UserControls/FilterOptionControl.ascx" %>
<%@ Page language="c#" MasterPageFile="MsterPageTabs.master"   AutoEventWireup="false" Inherits="Votations.NSurvey.WebAdmin.EditPageBranching" Codebehind="EditPageBranching.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="PageBranchingRulesControl" Src="UserControls/PageBranchingRulesControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="HeaderControl" Src="UserControls/HeaderControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FooterControl" Src="UserControls/FooterControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="SurveyListControl" Src="UserControls/SurveyListControl.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><div id="mainBody" class="mainBody contentHolder ps-container"><div id="Panel" class="Panel content">
 
            <div style="position: absolute; width: 650px; text-align: center; margin-left: 57px; top: 15px;">
 <asp:Label ID="MessageLabel" runat="server"  CssClass="ErrorMessage" Visible="False"></asp:Label>
                </div>

                <div style="position: absolute; width: 50px; text-align: center; margin-left: 700px; top: 15px;">
<asp:ImageButton ID="ImageButton1" ImageUrl="~/Images/back_button.gif" runat="server"
                    CssClass="buttonBack" ToolTip="Click to go back" OnCommand="OnBackButton" />
                    </div>
    <br />
    
                                 <fieldset style="width:750px; margin-left:12px; margin-top:15px;" title="Survey Title">
        <legend class="titleFont" style="text-align:left; margin: 0px 15px 0 15px;">
    <asp:literal id="AddNewBranchingTitle" runat="server" EnableViewState="False">Add new branching rule to page</asp:literal>
                    </legend>
                         <br />

  <ol>
     <li>        

          
           <asp:label id="QuestionLabel" AssociatedControlID="QuestionFilterDropdownlist" runat="server" EnableViewState="False">Question :</asp:label>
    <asp:dropdownlist id="QuestionFilterDropdownlist" runat="server" AutoPostBack="True"></asp:dropdownlist>
     </li><li> 
    <asp:label id="ConditionalLabel" AssociatedControlID="LogicDropDownList" runat="server">Conditional operator :</asp:label>
    <asp:dropdownlist id="LogicDropDownList" runat="server" AutoPostBack="True"></asp:dropdownlist>
     </li><li> 
    <asp:label id="AnswerLabel" AssociatedControlID="AnswerFilterDropdownlist" runat="server">Answer :</asp:label>
    <asp:dropdownlist id="AnswerFilterDropdownlist" runat="server" AutoPostBack="True"></asp:dropdownlist>
     </li><li> 
    <asp:label id="TextEvaluationConditionLabel" AssociatedControlID="ExpressionLogicDropdownlist" runat="server">Text evaluation condition :</asp:label>
    <asp:dropdownlist id="ExpressionLogicDropdownlist" runat="server" ></asp:dropdownlist>
     </li><li> 
    <asp:label id="FilterTextLabel" AssociatedControlID="TextFilterTextbox" runat="server">Text :</asp:label>
    <asp:textbox id="TextFilterTextbox" runat="server"></asp:textbox>
     </li><li> 
    <asp:label id="ScoreLabel" AssociatedControlID="ScoreTextbox" runat="server">Score :</asp:label>
    <asp:textbox id="ScoreTextbox" runat="server" Columns="2"></asp:textbox>
          </li><li> 
                <asp:label id="ScoreRangeLabel" AssociatedControlID="ScoreMaxTextbox" runat="server">between</asp:label>
                <asp:textbox id="ScoreMaxTextbox" runat="server" Columns="2"></asp:textbox>
               </li><li> 
                <asp:label id="PageTargetLabel" AssociatedControlID="PageTargetDropdownlist" runat="server">Go to page :</asp:label>
                <asp:dropdownlist id="PageTargetDropdownlist" runat="server" ></asp:dropdownlist>

           </li><li> 
          <asp:button id="AddRuleButton" CssClass="btn btn-primary btn-xs bw" runat="server" Text="Add new rule" Enabled="False"></asp:button><br />
                                           </li>
  </ol>
   </fieldset>


                                           <fieldset style="width:750px; margin-left:12px; margin-top:15px;" title="Survey Title">
        <legend class="titleFont" style="text-align:left; margin: 0px 15px 0 15px;">
    <asp:literal id="BranchingRulesTitle" runat="server" EnableViewState="False">Branching rules</asp:literal>
                    </legend>
                         <br />

  <ol>
     <li>  
          <div class="scm" style="width:60%;"><asp:literal id="EvaluationConditionInfo" runat="server" EnableViewState="False">Conditions are evaluated from top to down with a "first condition met branch" rule</asp:literal>
              </div>  <br />
    
    <uc1:PageBranchingRulesControl id="PageBranchingRules" runat="server"></uc1:PageBranchingRulesControl>
                                           </li>
  </ol>
   </fieldset>

</div></div></asp:Content>
