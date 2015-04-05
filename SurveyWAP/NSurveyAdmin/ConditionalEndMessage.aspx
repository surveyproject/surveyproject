<%@ Register TagPrefix="uc1" TagName="SurveyListControl" Src="UserControls/SurveyListControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FooterControl" Src="UserControls/FooterControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="HeaderControl" Src="UserControls/HeaderControl.ascx" %>

<%@ Register TagPrefix="uc1" TagName="PageBranchingRulesControl" Src="UserControls/PageBranchingRulesControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="SurveyMessageConditonsControl" Src="UserControls/SurveyMessageConditonsControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FilterOptionControl" Src="UserControls/FilterOptionControl.ascx" %>

<%@ Page language="c#" MasterPageFile="MsterPageTabs.master"   ValidateRequest="false"	AutoEventWireup="false" Inherits="Votations.NSurvey.WebAdmin.ConditionalEndMessage" Codebehind="ConditionalEndMessage.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="mainBody" class="mainBody contentHolder ps-container">
        <div id="Panel" class="Panel content">

                                    <div style="position: absolute; width: 650px; text-align: center; margin-left: 57px; top: 10px;">
 <asp:Label ID="MessageLabel" runat="server"  CssClass="ErrorMessage" Visible="False"></asp:Label>
                </div>
            <br />
    <fieldset style="width:750px; margin-top:15px; margin-left:12px;"><legend class="titleFont" style="margin: 0px 15px 0 15px; text-align:left;">
                    <asp:Literal ID="AddNewConditionTitle" runat="server" EnableViewState="False"></asp:Literal></legend>
        <br />
<ol>
     <li>
                        <asp:Label ID="MessageConditionLabel"
                            runat="server" AssociatedControlID="MessageConditionDropdownlist"></asp:Label>
                        <asp:DropDownList ID="MessageConditionDropdownlist" runat="server" AutoPostBack="True"></asp:DropDownList>
 </li><li>
                        <asp:Label ID="QuestionLabel"
                            runat="server" AssociatedControlID="QuestionFilterDropdownlist">Question :</asp:Label>
                        <asp:DropDownList ID="QuestionFilterDropdownlist" runat="server" AutoPostBack="True"></asp:DropDownList>
 </li><li>
                        <asp:Label ID="ConditionalLabel" AssociatedControlID="LogicDropDownList" runat="server">Conditional operator :</asp:Label>
                        <asp:DropDownList ID="LogicDropDownList" runat="server" AutoPostBack="True"></asp:DropDownList>
 </li><li>
                        <asp:Label ID="AnswerLabel" AssociatedControlID="AnswerFilterDropdownlist" runat="server">Answer :</asp:Label>
                        <asp:DropDownList ID="AnswerFilterDropdownlist" runat="server" AutoPostBack="True"></asp:DropDownList>
 </li><li>
                        <asp:Label ID="TextEvaluationConditionLabel" AssociatedControlID="ExpressionLogicDropdownlist" runat="server">Text evaluation condition :</asp:Label>
                        <asp:DropDownList ID="ExpressionLogicDropdownlist" runat="server"></asp:DropDownList>
 </li><li>
                        <asp:Label ID="FilterTextLabel" AssociatedControlID="TextFilterTextbox" runat="server">Text :</asp:Label>
                         <asp:TextBox ID="TextFilterTextbox" runat="server"></asp:TextBox>

 </li><li>
                        <asp:Label ID="ScoreLabel" AssociatedControlID="ScoreTextbox" runat="server">Score :&nbsp;</asp:Label>

                        <asp:TextBox ID="ScoreTextbox" runat="server" Columns="2"></asp:TextBox>

                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="ScoreTextbox"
                            ErrorMessage="No Valid Number" ValidationExpression="^-{0,1}\d+$"></asp:RegularExpressionValidator><br />
 </li><li>
                        <asp:Label ID="ScoreRangeLabel" AssociatedControlID="ScoreMaxTextbox" runat="server">and</asp:Label>&nbsp;
                   <asp:TextBox ID="ScoreMaxTextbox" runat="server" Columns="2"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="ScoreMaxTextbox"
                            ErrorMessage="No Valid Number" ValidationExpression="^-{0,1}\d+$"></asp:RegularExpressionValidator>

    </li><li> 
                        <asp:Label ID="ConditionalMessageLabel" AssociatedControlID="" runat="server">Message shown on submit :</asp:Label>
   </li><li> 
                        <CKEditor:CKEditorControl ID="ConditionCKeditor" BasePath="~/Scripts/ckeditor" runat="server">
                        </CKEditor:CKEditorControl>
 </li><li>
                        <br />
                        <asp:Label ID="ScoreTagLabel" runat="server">Score total can be shown using the ::score:: tag inside your message</asp:Label>


  </li><li>
               
               <asp:button id="AddMessageConditionButton" CssClass="btn btn-primary btn-xs bw" runat="server" Text="Add new message condition" Enabled="False"></asp:button>
               <asp:button id="UpdateMessageConditionButton" CssClass="btn btn-primary btn-xs bw" runat="server" Text="Update message condition"></asp:button>
               <asp:Button id="ConditionCancelButton"  CssClass="btn btn-primary btn-xs bw" runat="server" Text="Cancel / Back"></asp:Button>

       <br />
                          </li>
  </ol>
                    <br />
                    </fieldset>

 </div></div></asp:Content>
