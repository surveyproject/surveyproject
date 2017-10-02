<%@ Register TagPrefix="uc1" TagName="FilterOptionControl" Src="UserControls/FilterOptionControl.ascx" %>
<%@ Page language="c#" MasterPageFile="MsterPageTabs.master"   AutoEventWireup="false" Inherits="Votations.NSurvey.WebAdmin.EditPageBranching" Codebehind="EditPageBranching.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="PageBranchingRulesControl" Src="UserControls/PageBranchingRulesControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="SurveyListControl" Src="UserControls/SurveyListControl.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

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
 <asp:Label ID="MessageLabel" runat="server"  CssClass="errorMessage" Visible="False"></asp:Label>
                </div>

                                 <fieldset>
        <legend class="titleFont titleLegend">
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


                                           <fieldset>
        <legend class="titleFont titleLegend">
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
                                     <div id="fillerDiv" class="fillerDiv">&nbsp;</div>
</div></asp:Content>
