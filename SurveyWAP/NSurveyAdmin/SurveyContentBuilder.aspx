<%@ Register TagPrefix="uc1" TagName="FooterControl" Src="UserControls/FooterControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="HeaderControl" Src="UserControls/HeaderControl.ascx" %>

<%@ Page Language="c#" MasterPageFile="MsterPageTabs.master" ValidateRequest="false"
    AutoEventWireup="false" Inherits="Votations.NSurvey.WebAdmin.SurveyContentBuilder"
    CodeBehind="SurveyContentBuilder.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            var scroll = '<%= GetScrollQuestionId() %>';
            if (scroll != null && scroll.length > 0) {
                var scrollStr = "questionid=" + scroll + "&";
                $("table a").each(function () {
                    if ($(this).attr("href")) {

                        if ($(this).attr("href").indexOf(scrollStr) > -1) {

                            this.scrollIntoView();
                        }
                    }

                });
            }
        });
    </script>

    <style type="text/css" id="scb_answers">
        span.AnswerTextRender > label{color: black;background: white;font-weight: normal; margin-left: 5px;}
        span.AnswerTextRender > div > input {margin: -5px 5px 0 0}
    </style>

    <div id="mainBody" class="mainBody contentHolder ps-container">
    <div id="Panel" class="Panel content">
                        <fieldset style="width:750px; margin-top:15px; margin-left:12px;" id="liML" runat="server">
                            
                      <!--      <legend class="titleFont" style="margin: 0px 15px 0 15px;">
                                            <asp:Literal ID="SurveyBuilderTitle" runat="server" EnableViewState="False">Survey builder</asp:Literal>
                                                </legend> -->
                    
<ol>
     <li>
                                            <asp:label ID="PreviewSurveyLanguageLabel" AssociatedControlID="LanguagesDropdownlist" runat="server" Text="Language preview :"></asp:label>
                                        <asp:DropDownList ID="LanguagesDropdownlist" runat="server" AutoPostBack="True">
                                        </asp:DropDownList>

                          </li>
  </ol>
                   
                    </fieldset><br />

                                        <asp:PlaceHolder ID="QuestionListPlaceHolder" runat="server"></asp:PlaceHolder>

        </div> 
    </div>
</asp:Content>
