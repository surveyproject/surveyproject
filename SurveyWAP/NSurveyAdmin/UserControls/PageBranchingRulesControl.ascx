<%@ Import Namespace="Votations.NSurvey.WebAdmin" %>
<%@ Control Language="c#" AutoEventWireup="false" Inherits="Votations.NSurvey.WebAdmin.UserControls.PageBranchingRulesControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" Codebehind="PageBranchingRulesControl.ascx.cs" %>

  
			<asp:repeater id="RulesRepeater" runat="server">
          
			<HeaderTemplate>
                 <div class="rounded_corners" style="margin-top:5px; background-color: #e2e2e2; border: none;">
                                   
			<table style="border:none; display:table; width:100%; text-align:left; padding:2px;" class="smallText" >
			</HeaderTemplate>

			<ItemTemplate>
				<tr style="background-color:#e2e2e2; border:none;"><td style="border:none;">
<%#FormatRule(DataBinder.Eval(Container.DataItem,"ConditionalOperator"), 
DataBinder.Eval(Container.DataItem,"QuestionText").ToString(), 
DataBinder.Eval(Container.DataItem,"AnswerText").ToString(), 
DataBinder.Eval(Container.DataItem,"TextFilter").ToString(), 
DataBinder.Eval(Container.DataItem,"TargetPageNumber").ToString(),
DataBinder.Eval(Container.DataItem,"Score").ToString(),
DataBinder.Eval(Container.DataItem,"Scoremax").ToString(),
DataBinder.Eval(Container.DataItem,"ExpressionOperator"))
%>
- [<asp:LinkButton id="DeleteRuleLinkButton" ForeColor="Red" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"BranchingRuleID")%>' text='<%#((PageBase)Page).GetPageResource("DeleteRuleInfo")%>' runat="server">Delete rule</asp:LinkButton>]

				    </td></tr>
			</ItemTemplate>

			<AlternatingItemTemplate>
				<tr style="background-color:#F4F9FA; border:none;"><td style="border:none;">
<%#FormatRule(DataBinder.Eval(Container.DataItem,"ConditionalOperator"), 
DataBinder.Eval(Container.DataItem,"QuestionText").ToString(), 
DataBinder.Eval(Container.DataItem,"AnswerText").ToString(), 
DataBinder.Eval(Container.DataItem,"TextFilter").ToString(), 
DataBinder.Eval(Container.DataItem,"TargetPageNumber").ToString(),
DataBinder.Eval(Container.DataItem,"Score").ToString(),
DataBinder.Eval(Container.DataItem,"Scoremax").ToString(),
DataBinder.Eval(Container.DataItem,"ExpressionOperator")
)%>
- [<asp:LinkButton id="Linkbutton1" ForeColor="Red" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"BranchingRuleID")%>' text='<%#((PageBase)Page).GetPageResource("DeleteRuleInfo")%>' runat="server">Delete rule</asp:LinkButton>]
</td></tr>
			</AlternatingItemTemplate>

			<FooterTemplate>
				</table></div>
			</FooterTemplate>

			</asp:repeater>