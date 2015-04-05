<%@ Register TagPrefix="uc1" TagName="FilterOptionControl" Src="UserControls/FilterOptionControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="SkipLogicRulesControl" Src="UserControls/SkipLogicRulesControl.ascx" %>
<%@ Page language="c#" MasterPageFile="MsterPageTabs.master"   AutoEventWireup="false" Inherits="Votations.NSurvey.WebAdmin.EditSkipLogicRules" Codebehind="EditSkipLogicRules.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="PageBranchingRulesControl" Src="UserControls/PageBranchingRulesControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="HeaderControl" Src="UserControls/HeaderControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FooterControl" Src="UserControls/FooterControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="SurveyListControl" Src="UserControls/SurveyListControl.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="mainBody" class="mainBody contentHolder ps-container">
        <div id="Panel" class="Panel content">

            <div style="position: absolute; width: 650px; text-align: center; margin-left: 57px; top: 15px;">
 <asp:Label ID="MessageLabel" runat="server"  CssClass="ErrorMessage" Visible="False"></asp:Label>
                </div>
                            <div style="position: absolute; width: 50px; text-align: center; margin-left: 700px; top: 15px;">
<asp:ImageButton ID="ImageButton1" ImageUrl="~/Images/back_button.gif" runat="server"
                    CssClass="buttonBack" ToolTip="Click to go Back" OnCommand="OnBackButton" />
                                </div>
            <br />
                
                                 <fieldset style="width:750px; margin-left:12px; margin-top:15px;" title="Survey Title">
        <legend class="titleFont" style="text-align:left; margin: 0px 15px 0 15px;">
            <asp:literal id="AddNewSkipLogicTitle" runat="server" EnableViewState="False" Text="Add new skip logic to this question"></asp:literal>
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
            <asp:label id=FilterTextLabel AssociatedControlID="TextFilterTextbox" runat="server">Text :</asp:label>
            <asp:textbox id="TextFilterTextbox" runat="server"></asp:textbox>
            
            <asp:label id="ScoreLabel" AssociatedControlID="ScoreTextbox" runat="server">Score :</asp:label>
            <asp:textbox id="ScoreTextbox" runat="server" Columns="2"></asp:textbox> 
                      </li><li> 
                <asp:label id="ScoreRangeLabel" AssociatedControlID="ScoreMaxTextbox" runat="server">between</asp:label> 
                <asp:textbox id="ScoreMaxTextbox" runat="server" Columns="2"></asp:textbox>

                             </li><li> 
	<asp:button id="AddRuleButton" CssClass="btn btn-primary btn-xs bw" runat="server" Enabled="False" Text="Add new rule"></asp:button>
                                          <br /> </li>
  </ol>
   </fieldset>


                                                       <fieldset style="width:750px; margin-left:12px; margin-top:15px;" title="Survey Title">
        <legend class="titleFont" style="text-align:left; margin: 0px 15px 0 15px;">
<asp:literal id="SkipLogicRulesTitle" runat="server" EnableViewState="False" Text="Skip logic rules"></asp:literal>
                                </legend>
                         <br />

  <ol>
     <li>  

             <div class="scm" style="width:60%;"> <asp:literal id="SkipLogicEvaluationConditionInfo" runat="server" EnableViewState="False">Conditions are evaluated from top to down</asp:literal>
                </div>  <br />

            <uc1:SkipLogicRulesControl id="SkipLogicRules" runat="server"></uc1:SkipLogicRulesControl>
				                                           </li>
  </ol>
   </fieldset>
</div></div></asp:Content>
