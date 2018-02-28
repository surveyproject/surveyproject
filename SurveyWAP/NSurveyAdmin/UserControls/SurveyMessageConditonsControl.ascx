<%@ Import Namespace="Votations.NSurvey.WebAdmin" %>
<%@ Control Language="c#" AutoEventWireup="false" Inherits="Votations.NSurvey.WebAdmin.UserControls.SurveyMessageConditionsControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" Codebehind="SurveyMessageConditonsControl.ascx.cs" %>

<asp:repeater id="RulesRepeater" runat="server">
			<HeaderTemplate>

			</HeaderTemplate>
			<ItemTemplate>
<div style="border:1px solid white; display:inline-block; background-color:#B5B7BA; padding:5px; width:100%;" class="smallText" >			
	<div class="smallText" style="width:60%; float:left; height:50%;">
	<%#
	FormatRule(DataBinder.Eval(Container.DataItem,"MessageConditionalOperator"),
	DataBinder.Eval(Container.DataItem,"ConditionalOperator"),
	DataBinder.Eval(Container.DataItem,"QuestionText").ToString(),
	DataBinder.Eval(Container.DataItem,"AnswerText").ToString(),
	DataBinder.Eval(Container.DataItem,"TextFilter").ToString(),
	DataBinder.Eval(Container.DataItem,"ThankYouMessage").ToString(),
	DataBinder.Eval(Container.DataItem,"Score").ToString(),
	DataBinder.Eval(Container.DataItem,"Scoremax").ToString(),
	DataBinder.Eval(Container.DataItem,"ExpressionOperator"))%>
	</div>
	
	<div style="float: right;"><strong>
        <asp:HyperLink id="EditHyperLink" runat="server">Edit condition</asp:HyperLink> |
	<asp:LinkButton id="Linkbutton2" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"MessageConditionID")%>' text='<%#((PageBase)Page).GetPageResource("DeleteConditionInfo")%>' runat="server">Delete condition</asp:LinkButton>
        &nbsp;</strong>
	</div>
    </div>
			</ItemTemplate>
			<AlternatingItemTemplate>
	<div style="border:1px solid white; display:inline-block; background-color:#B5B7BA; padding:5px; width:100%;" class="smallText" >					
<div class="smallText" style="width:60%; float: left;">

<%#
FormatRule(DataBinder.Eval(Container.DataItem,"MessageConditionalOperator"), 
DataBinder.Eval(Container.DataItem,"ConditionalOperator"), 
DataBinder.Eval(Container.DataItem,"QuestionText").ToString(), 
DataBinder.Eval(Container.DataItem,"AnswerText").ToString(), 
DataBinder.Eval(Container.DataItem,"TextFilter").ToString(), 
DataBinder.Eval(Container.DataItem,"ThankYouMessage").ToString(),
DataBinder.Eval(Container.DataItem,"Score").ToString(),
DataBinder.Eval(Container.DataItem,"Scoremax").ToString(),
DataBinder.Eval(Container.DataItem,"ExpressionOperator"))%>
    </div>

<div style="float:right;"><strong>
    <asp:HyperLink id="EditHyperlink" runat="server">Edit condition</asp:HyperLink> | 
<asp:LinkButton id="Linkbutton1" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"MessageConditionID")%>' text='<%#((PageBase)Page).GetPageResource("DeleteConditionInfo")%>' runat="server">Delete condition</asp:LinkButton>&nbsp;

                         </strong>
</div></div>
			</AlternatingItemTemplate>
			<FooterTemplate>

			</FooterTemplate>
			</asp:repeater>		


