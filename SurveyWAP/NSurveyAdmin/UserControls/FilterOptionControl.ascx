<%@ Control Language="c#" AutoEventWireup="false" Inherits="Votations.NSurvey.WebAdmin.UserControls.FilterOptionControl"
    TargetSchema="http://schemas.microsoft.com/intellisense/ie5" CodeBehind="FilterOptionControl.ascx.cs" %>
<%@ Import Namespace="Votations.NSurvey.WebAdmin" %>

<br />
            <div style="position: absolute; width: 650px; text-align: center; margin-left: 57px; top: 15px;">
 <asp:Label ID="MessageLabel" runat="server"  CssClass="errorMessage" Visible="False"></asp:Label>
                </div>
<br />
            <fieldset style="width: 730px; margin-left: 12px; margin-top: 15px;">
                <legend class="titleFont" style="margin: 0px 15px 0 15px; text-align:left;">
            <asp:Label ID="filtertitle" runat="server"></asp:Label>
                                    </legend>
                <br />
                <ol>
                    <li>

                            <asp:Label ID="FilterNameLabel" AssociatedControlID="FilterNameTextBox" runat="server" EnableViewState="False">Filter name :</asp:Label>

                        <asp:TextBox ID="FilterNameTextBox" runat="server"></asp:TextBox>
                                            </li>
                    <li>
                         <asp:Label ID="ParentFilterNameLabel" AssociatedControlID="ParentFilterNameDropDownList" runat="server" EnableViewState="False">Parent filter:</asp:Label>

                        <asp:DropDownList ID="ParentFilterNameDropDownList" runat="server"></asp:DropDownList>
                    </li>
                    <li>
                            <asp:Label ID="RuleOperatorLabel" AssociatedControlID="LogicalOperatorDropDownList" runat="server" EnableViewState="False">Conditional rule operator :</asp:Label>

                        <asp:DropDownList ID="LogicalOperatorDropDownList" runat="server">
                        </asp:DropDownList>
                                            </li>
                    <li>
<asp:Button ID="UpdateFilterButton" CssClass="btn btn-primary btn-xs bw" runat="server" Text="Update filter"></asp:Button>
<asp:Button ID="DeleteFilterButton" CssClass="btn btn-primary btn-xs bw" runat="server" Text="Delete filter"></asp:Button>
<asp:Button ID="CreatefilterButton" CssClass="btn btn-primary btn-xs bw" runat="server" Text="Create filter"></asp:Button>

    <br />
                                          </li>
                </ol>
            </fieldset>
                  
<asp:PlaceHolder ID="EditplaceHolder" runat="server">

            <fieldset style="width: 730px; margin-left: 12px; margin-top: 15px;">
                <legend class="titleFont" style="margin: 0px 15px 0 15px; text-align:left;">
                    <asp:Literal ID="NewRuleTitle" runat="server" EnableViewState="False"> Add new rule to filter</asp:Literal>
                                                        </legend>
                <br />
                <ol>
                    <li>
                                <asp:Label ID="QuestionLabel" AssociatedControlID="QuestionFilterDropdownlist" runat="server" EnableViewState="False">Question:</asp:Label>

                            <asp:DropDownList ID="QuestionFilterDropdownlist" runat="server" AutoPostBack="True">
                            </asp:DropDownList>
                          </li>
                    <li>
                                <asp:Label ID="AnswerLabel" AssociatedControlID="AnswerFilterDropdownlist" runat="server">Answer :</asp:Label>

                            <asp:DropDownList ID="AnswerFilterDropdownlist" runat="server" AutoPostBack="True">
                            </asp:DropDownList>
                                            </li>
                    <li>
                                <asp:Label ID="FilterText" AssociatedControlID="TextFilterTextbox" runat="server">Text contains :</asp:Label>

                            <asp:TextBox ID="TextFilterTextbox" runat="server"></asp:TextBox>
                                            </li>
                    <li>
    <asp:Button ID="AddRuleButton" CssClass="btn btn-primary btn-xs bw" runat="server" Text="Add new rule"></asp:Button>
                        <br />
                    </li>
                    <li>
                    <asp:Label ID="FilterRulesTitle" AssociatedControlID="RulesRepeater" runat="server" EnableViewState="False">Filter rules</asp:Label>

                            <asp:Repeater ID="RulesRepeater" runat="server">
                                <HeaderTemplate>
                                    <table border="0" cellpadding="4" cellspacing="3" class="InnerText">
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <%#FormatRule(DataBinder.Eval(Container.DataItem,"QuestionText").ToString(), DataBinder.Eval(Container.DataItem,"AnswerText").ToString(), DataBinder.Eval(Container.DataItem,"TextFilter").ToString())%>
                                            [ <asp:LinkButton Font-Bold="true" ForeColor="Red" ID="DeleteRuleLinkButton" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"FilterRuleID")%>'
                                                Text='<%#((PageBase)Page).GetPageResource("DeleteRuleInfo")%>' runat="server"></asp:LinkButton> ]
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <AlternatingItemTemplate>
                                    <tr bgcolor="#F4F9FA">
                                        <td>
                                            <%#FormatRule(DataBinder.Eval(Container.DataItem,"QuestionText").ToString(), DataBinder.Eval(Container.DataItem,"AnswerText").ToString(), DataBinder.Eval(Container.DataItem,"TextFilter").ToString())%>
                                            (<asp:LinkButton ID="Linkbutton1" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"FilterRuleID")%>'
                                                Text='<%#((PageBase)Page).GetPageResource("DeleteRuleInfo")%>' runat="server"></asp:LinkButton>)
                                        </td>
                                    </tr>
                                </AlternatingItemTemplate>
                                <FooterTemplate>
                                    </table>
                                </FooterTemplate>
                            </asp:Repeater>
                        <br />
                                          </li>
                </ol>
            </fieldset>
</asp:PlaceHolder>

<br />
<br />
<asp:PlaceHolder ID="AutoPlaceHolder" runat="server">
                <fieldset style="width: 730px; margin-left: 12px; margin-top: 15px;">
                <legend class="titleFont" style="margin: 0px 15px 0 15px; text-align:left;">
                    <asp:Literal ID="autoFilterLabel" runat="server" EnableViewState="False"> Auto-Create Filter</asp:Literal>
                                                                            </legend>
                <br />
                <ol>
                    <li>
                <asp:Literal ID="autoFilterMessageLabel" runat="server">
        Note: Auto-create creates one filter for every answer a question can have.<br />
         It is available only for closed questions (no free text fields).
                </asp:Literal>
                                                                    </li>
                    <li>
                    <asp:Label ID="autoFilterQuestionLabel" AssociatedControlID="ddlAutoQuestions" runat="server">Auto-create filter based on question:</asp:Label>

                <asp:DropDownList ID="ddlAutoQuestions" Width="370" runat="server">
                </asp:DropDownList>
                        <br /><br />
                                                                    </li>
                     <li> 
                <asp:Label ID="autoFilterParentLabel" AssociatedControlID="ddlAutoFilterParent" runat="server">Parent filter:</asp:Label>

                <asp:DropDownList ID="ddlAutoFilterParent" Width="370" runat="server">
                </asp:DropDownList>
                         <br /><br />

                     </li>
                    <li>
                <asp:Label ID="autoFilterMsg2title" CssClass="subtitleFont" runat="server">Filter naming scheme:</asp:Label>
                                            </li>
                    <li>
                    <asp:Label ID="autoQuestionNamingLabel" AssociatedControlID="ddlAutoQuestionNaming" runat="server">Question Naming:</asp:Label>

                <asp:DropDownList Width="225" ID="ddlAutoQuestionNaming" runat="server">
                    <asp:ListItem Value="Question">Question</asp:ListItem>
                    <asp:ListItem Value="QuestionDisplayOrderNumber">Question Display Order Number</asp:ListItem>
                    <asp:ListItem Value="QuestionID">Question ID</asp:ListItem>
                    <asp:ListItem Value="QuestionAlias">Question Alias</asp:ListItem>
                </asp:DropDownList>
                                            </li>
                    <li>
                    <asp:Label ID="autoanswerNaminglabel" AssociatedControlID="ddlAutoAnswerNaming" runat="server">Auto-create filter based on question:</asp:Label>

                <asp:DropDownList Width="225" ID="ddlAutoAnswerNaming" runat="server">
                    <asp:ListItem Value="Answer">Answer</asp:ListItem>
                    <asp:ListItem Value="AnswerDisplayOrderNumber">Answer Display Order Number</asp:ListItem>
                    <asp:ListItem Value="AnswerID">Answer ID</asp:ListItem>
                    <asp:ListItem Value="AnswerAlias">Answer Alias</asp:ListItem>
                </asp:DropDownList>
                                                                    </li>
                    <li>
                <asp:Literal ID="autofilterInfo2Label" runat="server">
    Note: The filter is named following the scheme [Question naming] | [Answer naming].<br />
Only the first 32 characters are taken each for Question naming and for Answer naming.
                </asp:Literal>
                                                                    </li>
                    <li>
                <asp:Button ID="btnAutoFilter" CssClass="btn btn-primary btn-xs bw" runat="server" Text="Generate Filters" />
                        <br />
                                                                  </li>
                </ol>
            </fieldset>
</asp:PlaceHolder>
