<%@ Import Namespace="Votations.NSurvey.WebAdmin" %>
<%@ Control Language="c#" AutoEventWireup="false" Inherits="Votations.NSurvey.WebAdmin.UserControls.SurveyMessageConditionsControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" Codebehind="SurveyMessageConditonsControl.ascx.cs" %>


<asp:repeater id="RulesRepeater" runat="server">
			<HeaderTemplate>
<table border="0" bgcolor="#B5B7BA" cellpadding="5" cellspacing="1" width="100%" class="smallText" >
			</HeaderTemplate>
			<ItemTemplate>
			
<tr bgcolor="#EEEEEE">
<td>

	<table class="smallText" width="100%">
	<tr>
	<td><%#
	FormatRule(DataBinder.Eval(Container.DataItem,"MessageConditionalOperator"),
	DataBinder.Eval(Container.DataItem,"ConditionalOperator"),
	DataBinder.Eval(Container.DataItem,"QuestionText").ToString(),
	DataBinder.Eval(Container.DataItem,"AnswerText").ToString(),
	DataBinder.Eval(Container.DataItem,"TextFilter").ToString(),
	DataBinder.Eval(Container.DataItem,"ThankYouMessage").ToString(),
	DataBinder.Eval(Container.DataItem,"Score").ToString(),
	DataBinder.Eval(Container.DataItem,"Scoremax").ToString(),
	DataBinder.Eval(Container.DataItem,"ExpressionOperator"))%>
	</td></tr>
	
	<tr>
	<td align="right"><strong><asp:HyperLink id="EditHyperLink" runat="server">Edit condition</asp:HyperLink> |
	<asp:LinkButton id="Linkbutton2" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"MessageConditionID")%>' text='<%#((PageBase)Page).GetPageResource("DeleteConditionInfo")%>' runat="server">Delete condition</asp:LinkButton>&nbsp;</strong>
	</td>
	</tr>
	</table>
	
</td>
</tr>

			</ItemTemplate>
			<AlternatingItemTemplate>
<tr bgcolor="#E5EDF3"><td>
				
<table class="smallText" width="100%">
<tr>
<td>
<%#
FormatRule(DataBinder.Eval(Container.DataItem,"MessageConditionalOperator"), 
DataBinder.Eval(Container.DataItem,"ConditionalOperator"), 
DataBinder.Eval(Container.DataItem,"QuestionText").ToString(), 
DataBinder.Eval(Container.DataItem,"AnswerText").ToString(), 
DataBinder.Eval(Container.DataItem,"TextFilter").ToString(), 
DataBinder.Eval(Container.DataItem,"ThankYouMessage").ToString(),
DataBinder.Eval(Container.DataItem,"Score").ToString(),
DataBinder.Eval(Container.DataItem,"Scoremax").ToString(),
DataBinder.Eval(Container.DataItem,"ExpressionOperator"))%></td>
</tr>
<tr><td align="right"><strong><asp:HyperLink id="EditHyperlink" runat="server">Edit condition</asp:HyperLink> | 
<asp:LinkButton id="Linkbutton1" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"MessageConditionID")%>' text='<%#((PageBase)Page).GetPageResource("DeleteConditionInfo")%>' runat="server">Delete condition</asp:LinkButton>&nbsp;</strong>
</td>
</tr>
</table>

</td></tr>
			</AlternatingItemTemplate>
			<FooterTemplate>
				</table>
			</FooterTemplate>
			</asp:repeater>
