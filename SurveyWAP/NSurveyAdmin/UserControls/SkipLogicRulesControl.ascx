<%@ Control Language="c#" AutoEventWireup="false" Inherits="Votations.NSurvey.WebAdmin.UserControls.SkipLogigRulesControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" CodeBehind="SkipLogicRulesControl.ascx.cs" %>
<%@ Import Namespace="Votations.NSurvey.WebAdmin" %>

<asp:Repeater ID="RulesRepeater" runat="server">
    <HeaderTemplate>
    </HeaderTemplate>
    <ItemTemplate>
        <div style="border: 1px solid white; display: inline-block; background-color: #B5B7BA; padding: 5px; width: 100%;" class="smallText rounded-corners">
            <div class="smallText" style="width: 60%; float: left; height: 50%;">
                <%#FormatRule(DataBinder.Eval(Container.DataItem,"ConditionalOperator"), 
DataBinder.Eval(Container.DataItem,"QuestionText").ToString(), 
DataBinder.Eval(Container.DataItem,"AnswerText").ToString(), 
DataBinder.Eval(Container.DataItem,"TextFilter").ToString(), 
DataBinder.Eval(Container.DataItem,"Score").ToString(),
DataBinder.Eval(Container.DataItem,"Scoremax").ToString(),
DataBinder.Eval(Container.DataItem,"ExpressionOperator"))%>
            </div>

            <div style="float: right;">
                [<asp:LinkButton ID="DeleteRuleLinkButton" ForeColor="Red" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"SkipLogicRuleID")%>' Text='<%#((PageBase)Page).GetPageResource("DeleteRuleInfo")%>' runat="server">Delete rule</asp:LinkButton>]
            </div>
        </div>
    </ItemTemplate>
    <AlternatingItemTemplate>
        <div style="border: 1px solid white; display: inline-block; background-color: #B5B7BA; padding: 5px; width: 100%;" class="rounded-corners smallText">
            <div class="smallText" style="width: 60%; float: left;">
                <%#FormatRule(DataBinder.Eval(Container.DataItem,"ConditionalOperator"), 
DataBinder.Eval(Container.DataItem,"QuestionText").ToString(), 
DataBinder.Eval(Container.DataItem,"AnswerText").ToString(), 
DataBinder.Eval(Container.DataItem,"TextFilter").ToString(), 
DataBinder.Eval(Container.DataItem,"Score").ToString(),
DataBinder.Eval(Container.DataItem,"Scoremax").ToString(),
DataBinder.Eval(Container.DataItem,"ExpressionOperator")
)%>
            </div>

            <div style="float: right;">
                [<asp:LinkButton ID="Linkbutton1" ForeColor="Red" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"SkipLogicRuleID")%>' Text='<%#((PageBase)Page).GetPageResource("DeleteRuleInfo")%>' runat="server">Delete rule</asp:LinkButton>]
            </div>
        </div>
    </AlternatingItemTemplate>
    <FooterTemplate>
    </FooterTemplate>
</asp:Repeater>
