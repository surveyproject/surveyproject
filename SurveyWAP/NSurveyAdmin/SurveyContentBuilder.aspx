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


    <div id="Panel" class="Panel">
                        <fieldset id="liML" runat="server">                    
                            <ol>
                                <li>
                                    <asp:Label ID="PreviewSurveyLanguageLabel" AssociatedControlID="LanguagesDropdownlist" runat="server" Text="Language preview :"></asp:Label>
                                    <asp:DropDownList ID="LanguagesDropdownlist" runat="server" AutoPostBack="True">
                                    </asp:DropDownList>

                                </li>
                            </ol>

                        </fieldset>

                                                <div class="helpDiv">
                                            <a onmouseover='this.style.cursor="help" ' onfocus='this.blur();' href="Help/new/Survey%20Form%20Builder.aspx"
                                                title="Click for more Information">
                                                <img alt="help" border="0" src="<%= Page.ResolveUrl("~")%>Images/small_help.gif" />
                                            </a></div>

                                        <asp:PlaceHolder ID="QuestionListPlaceHolder" runat="server"></asp:PlaceHolder>
                <div id="fillerDiv" class="fillerDiv">&nbsp;</div>
        </div> 

</asp:Content>
