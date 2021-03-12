<%@ Page Language="c#" MasterPageFile="MsterPageTabs.master" ValidateRequest="false"
    AutoEventWireup="false" Inherits="Votations.NSurvey.WebAdmin.EditSingleQuestion"
    CodeBehind="EditSingleQuestion.aspx.cs" %>

<%@ Register TagPrefix="mbrsc" Namespace="MetaBuilders.WebControls" Assembly="SurveyProject.WebApplication" %>
<%@ Register Src="UserControls/QuestionExtraLinks.ascx" TagName="QuestionExtraLinks" TagPrefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div id="Panel" class="Panel">
                        <fieldset id="liML" runat="server">    

                <asp:ImageButton ID="ImageButton1" Width="16px" ImageUrl="~/Images/back_button.gif" runat="server"
                    CssClass="buttonBack" OnCommand="OnBackButton" />
                <div class="elDiv">
                    <uc2:QuestionExtraLinks ID="QuestionExtraLinks1" runat="server" /></div>
                                           <div class="helpDiv">
                                            <a onmouseover='this.style.cursor="help" ' onfocus='this.blur();' href="Help/new/Question Editor.aspx"
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
                    <asp:Literal ID="EditQuestionTitle" runat="server" EnableViewState="False">Edit question</asp:Literal>
                </legend>
                <br />
                <ol>
                    <li>

                        <CKEditor:CKEditorControl ID="QuestionFreeTextBox" BasePath="~/Scripts/ckeditor" runat="server">
                        </CKEditor:CKEditorControl>
                    </li>
                    <li>

                        <asp:Label ID="EditionLanguageLabel" AssociatedControlID="LanguagesDropdownlist" runat="server" Text="Edition language :"></asp:Label>

                        <asp:DropDownList ID="LanguagesDropdownlist" runat="server" AutoPostBack="True">
                        </asp:DropDownList>
                    </li>
                    <li>
                        <asp:Label ID="IDLabel" AssociatedControlID="txtQuestionID" runat="server" EnableViewState="False">ID:</asp:Label>

                        <asp:TextBox runat="server" ID="txtQuestionID"></asp:TextBox>
                    </li>
                    <li>
                        <asp:Label ID="AliasLabel" AssociatedControlID="txtAlias" runat="server" EnableViewState="False">Alias:</asp:Label>

                        <asp:TextBox runat="server" ID="txtAlias"></asp:TextBox>
                    </li>

                    <asp:PlaceHolder ID="HelpTextPlaceholder" runat="server">
                    <li>
                        <asp:Label ID="HelpTextLabel" AssociatedControlID="txtHelpText" runat="server" EnableViewState="False">Help Text:</asp:Label>

                        <asp:TextBox runat="server" ID="txtHelpText" TextMode="MultiLine" Columns="65" Rows="3"></asp:TextBox>
                    </li>
                    <li>
                        <asp:Label ID="ShowHelpTextLabel" AssociatedControlID="chbShowHelpText" runat="server" EnableViewState="False">Show Help Text:</asp:Label>

                        <asp:CheckBox runat="server" ID="chbShowHelpText"></asp:CheckBox>
                    </li>
                    </asp:PlaceHolder>

                    <li>
                        <asp:Label ID="SelectionModeLabel" AssociatedControlID="SelectionModeDropDownList" runat="server" EnableViewState="False">Selection mode:</asp:Label>

                        <asp:DropDownList ID="SelectionModeDropDownList" runat="server"  AutoPostBack="True" OnSelectedIndexChanged="SelectionModeDropDownList_SelectedIndexChanged">
                        </asp:DropDownList>
                    </li>



                    <asp:PlaceHolder ID="AnswerOptionsPlaceholder" runat="server">
                        <li>
                            <asp:Label ID="DisplayModeLabel" AssociatedControlID="DisplayModeDropDownList" runat="server" EnableViewState="False">Display mode:</asp:Label>

                            <asp:DropDownList ID="DisplayModeDropDownList" runat="server">
                            </asp:DropDownList>
                        </li>
                        <li>
                            <asp:Label ID="ColumnNumberLabel" AssociatedControlID="ColumnDropdownlist" runat="server" EnableViewState="False">Number of columns:</asp:Label>

                            <asp:DropDownList ID="ColumnDropdownlist" runat="server">
                                <asp:ListItem Value="1">1</asp:ListItem>
                                <asp:ListItem Value="2">2</asp:ListItem>
                                <asp:ListItem Value="3">3</asp:ListItem>
                                <asp:ListItem Value="4">4</asp:ListItem>
                                <asp:ListItem Value="5">5</asp:ListItem>
                                <asp:ListItem Value="6">6</asp:ListItem>
                                <asp:ListItem Value="7">7</asp:ListItem>
                                <asp:ListItem Value="8">8</asp:ListItem>
                                <asp:ListItem Value="9">9</asp:ListItem>
                                <asp:ListItem Value="10">10</asp:ListItem>
                                <asp:ListItem Value="11">11</asp:ListItem>
                                <asp:ListItem Value="12">12</asp:ListItem>
                                <asp:ListItem Value="13">13</asp:ListItem>
                                <asp:ListItem Value="14">14</asp:ListItem>
                                <asp:ListItem Value="15">15</asp:ListItem>
                                <asp:ListItem Value="16">16</asp:ListItem>
                                <asp:ListItem Value="17">17</asp:ListItem>
                                <asp:ListItem Value="18">18</asp:ListItem>
                                <asp:ListItem Value="19">19</asp:ListItem>
                                <asp:ListItem Value="20">20</asp:ListItem>
                            </asp:DropDownList>
                        </li>
                        <li>
                            <asp:Label ID="RandomizeAnswersLabel" AssociatedControlID="RandomizeCheckBox" runat="server" EnableViewState="False">Randomize answers order:</asp:Label>

                            <asp:CheckBox ID="RandomizeCheckBox" runat="server"></asp:CheckBox>
                        </li>
                        <li>
                            <asp:Label ID="AnswerRatingLabel" AssociatedControlID="RatingCheckbox" runat="server" EnableViewState="False">Enable answer rating:</asp:Label>

                            <asp:CheckBox ID="RatingCheckbox" runat="server" AutoPostBack="True" OnCheckedChanged="RatingCheckbox_CheckedChanged"></asp:CheckBox>
                        </li>

                        <li>
                            <asp:Label ID="GroupLabel" AssociatedControlID="ddlGroup" runat="server" Visible="false" EnableViewState="False">Group:</asp:Label>

                            <asp:DropDownList runat="server" ID="ddlGroup" AutoPostBack="true" AppendDataBoundItems="true" OnSelectedIndexChanged="OnddlGroup_SelectedIndexChanged">
                            </asp:DropDownList>
                        </li>
                        <li>
                            <asp:Label ID="SubGroupLabel" AssociatedControlID="ddlSubGroup" Visible="false" runat="server" EnableViewState="False">Sub Group:</asp:Label>

                            <asp:DropDownList runat="server" AppendDataBoundItems="true" ID="ddlSubGroup">
                            </asp:DropDownList>
                        </li>


                        <li>
                            <asp:Label ID="MinSelectionLabel" AssociatedControlID="MinSelectionDropDownList" runat="server" EnableViewState="False">Min. selections required :</asp:Label>

                            <div id="tooltip">
                            <asp:DropDownList ID="MinSelectionDropDownList" runat="server">
                                <asp:ListItem Value="1">1</asp:ListItem>
                                <asp:ListItem Value="2">2</asp:ListItem>
                                <asp:ListItem Value="3">3</asp:ListItem>
                                <asp:ListItem Value="4">4</asp:ListItem>
                                <asp:ListItem Value="5">5</asp:ListItem>
                                <asp:ListItem Value="6">6</asp:ListItem>
                                <asp:ListItem Value="7">7</asp:ListItem>
                                <asp:ListItem Value="8">8</asp:ListItem>
                                <asp:ListItem Value="9">9</asp:ListItem>
                                <asp:ListItem Value="10">10</asp:ListItem>
                                <asp:ListItem Value="11">11</asp:ListItem>
                                <asp:ListItem Value="12">12</asp:ListItem>
                                <asp:ListItem Value="13">13</asp:ListItem>
                                <asp:ListItem Value="14">14</asp:ListItem>
                                <asp:ListItem Value="15">15</asp:ListItem>
                                <asp:ListItem Value="16">16</asp:ListItem>
                                <asp:ListItem Value="17">17</asp:ListItem>
                                <asp:ListItem Value="18">18</asp:ListItem>
                                <asp:ListItem Value="19">19</asp:ListItem>
                                <asp:ListItem Value="20">20</asp:ListItem>
                            </asp:DropDownList>
                                </div>

                        </li>
                        <li>

                            <asp:Label ID="MaxSelectionAllowed" AssociatedControlID="MaxAllowedDropDownList" runat="server" EnableViewState="False">Max. selections allowed :</asp:Label>
                             <div id="tooltip">
                            <asp:DropDownList ID="MaxAllowedDropDownList" runat="server">
                                <asp:ListItem Value="1">1</asp:ListItem>
                                <asp:ListItem Value="2">2</asp:ListItem>
                                <asp:ListItem Value="3">3</asp:ListItem>
                                <asp:ListItem Value="4">4</asp:ListItem>
                                <asp:ListItem Value="5">5</asp:ListItem>
                                <asp:ListItem Value="6">6</asp:ListItem>
                                <asp:ListItem Value="7">7</asp:ListItem>
                                <asp:ListItem Value="8">8</asp:ListItem>
                                <asp:ListItem Value="9">9</asp:ListItem>
                                <asp:ListItem Value="10">10</asp:ListItem>
                                <asp:ListItem Value="11">11</asp:ListItem>
                                <asp:ListItem Value="12">12</asp:ListItem>
                                <asp:ListItem Value="13">13</asp:ListItem>
                                <asp:ListItem Value="14">14</asp:ListItem>
                                <asp:ListItem Value="15">15</asp:ListItem>
                                <asp:ListItem Value="16">16</asp:ListItem>
                                <asp:ListItem Value="17">17</asp:ListItem>
                                <asp:ListItem Value="18">18</asp:ListItem>
                                <asp:ListItem Value="19">19</asp:ListItem>
                                <asp:ListItem Value="20">20</asp:ListItem>
                            </asp:DropDownList>
                                 </div>
                        </li>
                        <li>
                            <asp:Label ID="QuestionPipeAliasLabel" AssociatedControlID="PipeAliasTextBox" runat="server" EnableViewState="False">Pipe alias :</asp:Label>

                            <asp:TextBox ID="PipeAliasTextBox" runat="server" MaxLength="255"></asp:TextBox>
                        </li>
                    </asp:PlaceHolder>
                    <li>
                        <asp:Button ID="UpdateQuestionButton" CssClass="btn btn-primary btn-xs bw" runat="server" Text="Update"></asp:Button>
                        <asp:Button ID="EditAnswersButton" CssClass="btn btn-primary btn-xs bw" runat="server" Text="Add / Edit answers"></asp:Button>
                        <asp:Button ID="ExportXMLButton" CssClass="btn btn-primary btn-xs bw" runat="server" Text="Export XML"></asp:Button>
                        <br />
                    </li>
                </ol>
            </fieldset>

            <asp:PlaceHolder ID="RepeatSectionOptionPlaceHolder" runat="server">
                                                                        <div class="helpDiv">
                                            <a onmouseover='this.style.cursor="help" ' onfocus='this.blur();' href="Help/New/Repeatable_Introduction.aspx"
                                                title="Click for more Information">
                                                <img alt="help" border="0" src="<%= Page.ResolveUrl("~")%>Images/small_help.gif" />
                                            </a>
                                        </div>
                <fieldset>
                    <legend class="titleFont titleLegend">
                        <asp:Literal ID="RepeatableSectionsLabel" runat="server" EnableViewState="False">Repeatable sections</asp:Literal>
                    </legend>
                    <br />
                    <ol>
                        <li>
                            <asp:Label ID="RepeatModeLabel" AssociatedControlID="RepeatModeDropdownlist" runat="server" EnableViewState="False">Repeat mode:</asp:Label>

                            <asp:DropDownList ID="RepeatModeDropdownlist" runat="server">
                            </asp:DropDownList>
                        </li>
                        <li>
                            <asp:Label ID="MaxSectionAllowedLabel" AssociatedControlID="MaxSectionAllowedDropdownlist" runat="server">Max. sections allowed :</asp:Label>


                            <asp:DropDownList ID="MaxSectionAllowedDropdownlist" runat="server">
                                <asp:ListItem Value="1">1</asp:ListItem>
                                <asp:ListItem Value="2">2</asp:ListItem>
                                <asp:ListItem Value="3">3</asp:ListItem>
                                <asp:ListItem Value="4">4</asp:ListItem>
                                <asp:ListItem Value="5">5</asp:ListItem>
                                <asp:ListItem Value="6">6</asp:ListItem>
                                <asp:ListItem Value="7">7</asp:ListItem>
                                <asp:ListItem Value="8">8</asp:ListItem>
                                <asp:ListItem Value="9">9</asp:ListItem>
                                <asp:ListItem Value="10">10</asp:ListItem>
                                <asp:ListItem Value="11">11</asp:ListItem>
                                <asp:ListItem Value="12">12</asp:ListItem>
                                <asp:ListItem Value="13">13</asp:ListItem>
                                <asp:ListItem Value="14">14</asp:ListItem>
                                <asp:ListItem Value="15">15</asp:ListItem>
                                <asp:ListItem Value="16">16</asp:ListItem>
                                <asp:ListItem Value="17">17</asp:ListItem>
                                <asp:ListItem Value="18">18</asp:ListItem>
                                <asp:ListItem Value="19">19</asp:ListItem>
                                <asp:ListItem Value="20">20</asp:ListItem>
                            </asp:DropDownList>
                        </li>
                        <li>
                            <asp:Label ID="AddRepeatSectionLabel" AssociatedControlID="AddSectionLinkTextBox" runat="server">Add section link text:</asp:Label>

                            <asp:TextBox ID="AddSectionLinkTextBox" runat="server"></asp:TextBox>
                        </li>
                        <li>
                            <asp:Label ID="DeleteRepeatSectionLabel" AssociatedControlID="DeleteSectionLinkTextBox" runat="server">Delete section link text :</asp:Label>

                            <asp:TextBox ID="DeleteSectionLinkTextBox" runat="server"></asp:TextBox>
                        </li>
                        <li>
                            <asp:Label ID="EditRepeatSectionLabel" AssociatedControlID="EditSectionLinkTextBox" runat="server">Edit section link text :</asp:Label>

                            <asp:TextBox ID="EditSectionLinkTextBox" runat="server"></asp:TextBox>
                        </li>
                        <li>
                            <asp:Label ID="UpdateRepeatSectionLabel" AssociatedControlID="UpdateSectionLinkTextBox" runat="server">Update section link text :</asp:Label>

                            <asp:TextBox ID="UpdateSectionLinkTextBox" runat="server"></asp:TextBox>
                        </li>
                        <li>
                            <asp:Label ID="GridAnswersLabel" AssociatedControlID="AnswersListBox" runat="server" Text="Answers shown in grid:"></asp:Label>
                            <br />
                        </li>
                        <li>
                            <asp:ListBox ID="AnswersListBox" runat="server" AutoPostBack="True"></asp:ListBox>

                            &nbsp; &nbsp;<asp:Label ID="GridAnswersChoiceLabel" runat="server"><==></asp:Label>&nbsp;&nbsp;
                        
                                            <asp:ListBox ID="GridAnswersListBox" runat="server" AutoPostBack="True"></asp:ListBox>
                        </li>
                        <li>
                            <asp:Button ID="UpdateSectionsButton" CssClass="btn btn-primary btn-xs bw" runat="server" Text="Update repeatable options"></asp:Button>
                            <br />
                        </li>
                    </ol>
                </fieldset>
            </asp:PlaceHolder>
                <div id="fillerDiv" class="fillerDiv">&nbsp;</div>
        </div>

</asp:Content>