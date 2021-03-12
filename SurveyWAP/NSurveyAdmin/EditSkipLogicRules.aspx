<%@ Page language="c#" MasterPageFile="MsterPageTabs.master"   AutoEventWireup="false" Inherits="Votations.NSurvey.WebAdmin.EditSkipLogicRules" Codebehind="EditSkipLogicRules.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="SkipLogicRulesControl" Src="UserControls/SkipLogicRulesControl.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

        <div id="Panel" class="Panel">
            
 <fieldset id="liML" runat="server">    
<asp:ImageButton ID="ImageButton1" ImageUrl="~/Images/back_button.gif" runat="server"
                    CssClass="buttonBack" ToolTip="Click to go Back" OnCommand="OnBackButton" />

                                                                             <div class="helpDiv">
                                            <a onmouseover='this.style.cursor="help" ' onfocus='this.blur();' href="Help/New/Skip%20Logic%20Conditions.aspx"
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


   <fieldset>
        <legend class="titleFont titleLegend">
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
                                                 <div id="fillerDiv" class="fillerDiv">&nbsp;</div>
</div></asp:Content>