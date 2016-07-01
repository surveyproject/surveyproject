
<%@ Register TagPrefix="uc1" TagName="SurveyListControl" Src="SurveyListControl.ascx" %>
<%@ Control Language="c#" AutoEventWireup="false" Inherits="Votations.NSurvey.WebAdmin.UserControls.AnswerOptionControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" Codebehind="AnswerOptionControl.ascx.cs" %>


            <div style="position: absolute; width: 650px; text-align: center; margin-left: 57px; top: 15px;">
 <asp:Label ID="MessageLabel" runat="server"  CssClass="errorMessage" Visible="False"></asp:Label>
                </div>

            <fieldset style="width: 750px; margin-left: 12px; margin-right: 0px; margin-top: 15px;">
                <legend class="titleFont" style="margin: 0px 15px 0 15px;">
                <asp:Literal ID="EditAnswerTitle" runat="server" Text="Edit answer settings"></asp:Literal>
                                    </legend>
                <br />
                <ol>                                    <li id="atddlLI" runat="server">
                            <asp:Label ID="AnswerTypeLabel" AssociatedControlID="AnswerTypeDropDownList" runat="server" EnableViewState="False" Text="Type :"></asp:Label>

                        <asp:DropDownList ID="AnswerTypeDropDownList" runat="server" AutoPostBack="True">
                        </asp:DropDownList>
                                            </li>    
                    <li id="aidLI" runat="server">

                    <asp:Label ID="lblAnswerID" AssociatedControlID="txtAnswerID" runat="server" Text="ID : ">ID : </asp:Label>
                       <div id="tooltip"> <asp:TextBox runat="server" ID="txtAnswerID"></asp:TextBox></div>
                                            </li>                    
                    <li id="aaLI" runat="server">
                   <asp:Label runat="server" ID="lblAnswerAlias" AssociatedControlID="txtAnswerAlias" Text="Alias : "></asp:Label> 
                        <div id="tooltip"><asp:TextBox runat="server" ID="txtAnswerAlias"></asp:TextBox></div>
                                            </li>
                    <li id="atLI" runat="server">
                            <asp:Label ID="AnswerTextLabel" AssociatedControlID="AnswerTextTextBox" runat="server" EnableViewState="False" Text="Answer text :"></asp:Label>
                        <div id="tooltip">
                        <asp:TextBox ID="AnswerTextTextBox" runat="server"></asp:TextBox></div>
                                            </li>

                    <li id="aiuLI" runat="server">
                            <asp:Label ID="AnswerURLLabel" AssociatedControlID="AnswerImageURLTextBox" runat="server" EnableViewState="False" Text="Image URL :"></asp:Label>

                        <asp:TextBox ID="AnswerImageURLTextBox" runat="server"></asp:TextBox>

                                                                    </li>   
                                        <li id="accLI" runat="server">
                        <asp:Label ID="AnswerCssClassLabel" AssociatedControlID="AnswerCssClassTextBox" runat="server" EnableViewState="False" Text="Radiobutton Css:"></asp:Label>
                        <div id="tooltip"><asp:TextBox ID="AnswerCssClassTextBox" runat="server"></asp:TextBox></div>
                    </li>
                                    <li id="dtLI" runat="server">
                            <asp:Label ID="DefaultTextLabel" AssociatedControlID="DefaultTextTextBox" Visible="False" runat="server" EnableViewState="False"
                                Text="Default text value* :"></asp:Label>
                                        <div id="tooltip">
                        <asp:TextBox ID="DefaultTextTextBox" Visible="False" runat="server"></asp:TextBox></div>

                                            </li>                 
                     <li id="revLI" runat="server">
                            <asp:Label ID="RegExValidationLabel" AssociatedControlID="RegExDropDownList" Visible="False" runat="server" EnableViewState="False"
                                Text="RegEx validation :"></asp:Label>

                        <asp:DropDownList ID="RegExDropDownList" runat="server" Visible="False">
                        </asp:DropDownList>
                                            </li>
                    <li id="mLI" runat="server">
                            <asp:Label ID="MandatoryLabel" AssociatedControlID="MandatoryCheckBox" Visible="False" runat="server" EnableViewState="False"
                                Text="Mandatory :"></asp:Label>
                        <div id="tooltip">
                        <asp:CheckBox ID="MandatoryCheckBox" runat="server"></asp:CheckBox>
                        </div>
                                            </li>

                    <li id="srLI" runat="server">
                        <asp:Label ID="SliderRangeLabel" AssociatedControlID="SliderRangeDDL" runat="server" Visible="False" EnableViewState="False" Text="Min or Max"></asp:Label>

            <div id="tooltip">
                         <asp:DropDownList ID="SliderRangeDDL" Visible="False" runat="server">
                         <asp:ListItem Selected="True" Text="No Value" Value=""></asp:ListItem>
                         <asp:ListItem Text="Minimum Value" Value="min"></asp:ListItem>
                         <asp:ListItem Text="Maximum Value" Value="max"></asp:ListItem>                         
                         </asp:DropDownList></div>
                       
                                            </li>

                    <li id="svLI" runat="server">
                        <asp:Label ID="SliderValueLabel" AssociatedControlID="SliderValueTextBox" runat="server" Visible="False" EnableViewState="False" Text="Starting Value"></asp:Label>

<div id="tooltip">
                         <asp:TextBox ID="SliderValueTextBox" Visible="False" runat="server" Columns="15"></asp:TextBox>
                         <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="SliderValueTextBox"
        ErrorMessage="No Valid Number" ValidationExpression="^-{0,1}\d+$"></asp:RegularExpressionValidator></div>

                                            </li>   
                    <li id="smiLI" runat="server">
                        <asp:Label ID="SliderMinLabel" AssociatedControlID="SliderMinTextBox" runat="server" Visible="False" EnableViewState="False" Text="Min Value"></asp:Label>
                <div id="tooltip">
                        <asp:TextBox ID="SliderMinTextBox" Visible="False" runat="server" Columns="15"></asp:TextBox>
                      <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="SliderMinTextBox"
        ErrorMessage="No Valid Number" ValidationExpression="^-{0,1}\d+$"></asp:RegularExpressionValidator></div>

                                            </li>

                    <li id="smaLI" runat="server">
                        <asp:Label ID="SliderMaxLabel" AssociatedControlID="SliderMaxTextBox" runat="server" Visible="False" EnableViewState="False" Text="Max Value"></asp:Label>
                    <div id="tooltip">
                        <asp:TextBox ID="SliderMaxTextBox" Visible="False" runat="server" Columns="15"></asp:TextBox>
                      <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="SliderMaxTextBox"
        ErrorMessage="No Valid Number" ValidationExpression="^-{0,1}\d+$"></asp:RegularExpressionValidator></div>

                                            </li>                    
                    <li id="saLI" runat="server">
                        <asp:Label ID="SliderAnimateLabel" AssociatedControlID="SliderAnimateCheckbox" runat="server" Visible="False" EnableViewState="False" Text="Animate Y/N"></asp:Label>
                <div id="tooltip">
                        <asp:CheckBox ID="SliderAnimateCheckbox" Visible="False" runat="server"></asp:CheckBox></div>

                                            </li>                  
                      <li id="ssLI" runat="server">
                        <asp:Label ID="SliderStepLabel" AssociatedControlID="SliderStepTextBox" runat="server" Visible="False" EnableViewState="False" Text="Step value"></asp:Label>
                    
                    <div id="tooltip">
                        <asp:TextBox ID="SliderStepTextBox" Visible="False" runat="server" Columns="15"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="SliderStepTextBox"
        ErrorMessage="No Valid Number" ValidationExpression="^-{0,1}\d+$"></asp:RegularExpressionValidator></div>

                                            </li>


                    <li id="apaLI" runat="server">
                            <asp:Label ID="AnswerPipeAliasLabel" AssociatedControlID="AnswerPipeAliasTextBox" runat="server" EnableViewState="False" Text="Pipe alias :"></asp:Label>
                        <div id="tooltip">
                        <asp:TextBox ID="AnswerPipeAliasTextBox" runat="server"></asp:TextBox>
                            </div>
                                            </li>

                    <li id="seaLI" runat="server">
                            <asp:Label ID="SelectedAnswersLabel" AssociatedControlID="SelectionCheckBox" Visible="False" runat="server" EnableViewState="False"
                                Text="Selected by default :"></asp:Label>

                        <asp:CheckBox ID="SelectionCheckBox" Visible="False" runat="server"></asp:CheckBox>

                                            </li>

                    <li id="arLI" runat="server">
                            <asp:Label ID="AnswerRatingLabel" AssociatedControlID="RatingPartCheckbox" Visible="False" runat="server" EnableViewState="False"
                                Text="Rating part :"></asp:Label>

                        <asp:CheckBox ID="RatingPartCheckbox" Visible="False" runat="server"></asp:CheckBox>

                                            </li>
                    <li id="sLI" runat="server">
                            <asp:Label ID="ScoreLabel" AssociatedControlID="ScoreTextBox" Visible="False" runat="server" EnableViewState="False"
                                Text="Score :"></asp:Label>

                        <asp:TextBox ID="ScoreTextBox" Visible="False" runat="server" Columns="20"></asp:TextBox>

                                            </li>
                    

                <asp:PlaceHolder ID="ConnectionsPlaceHolder" Visible="False" runat="server">
<li>
                             <div style="text-align:left;"> <asp:Label ID="ConnectionLabel" CssClass="scm" runat="server" Text="Connections :" EnableViewState="False"></asp:Label></div>  
                                                </li>
                    <li>
                                        <asp:Label ID="AnswerPublishersLabel" AssociatedControlID="AvailablePublishersListBox" runat="server" Text="Answer publishers" EnableViewState="False"></asp:Label>
                                            <asp:ListBox ID="AvailablePublishersListBox" runat="server" AutoPostBack="True"></asp:ListBox>
                        <br /><br />
                                        <asp:Label ID="AnswerSubscribedLabel" AssociatedControlID="SubscribedListbox"  runat="server" Text="Subscribed to :" EnableViewState="False"></asp:Label>
                                        <asp:ListBox ID="SubscribedListbox" runat="server" AutoPostBack="True"></asp:ListBox>
                        </li>
                    
                </asp:PlaceHolder>
<li>
            <asp:Button ID="UpdateAnswerButton" CssClass="btn btn-primary btn-xs bw" runat="server" Text="Update"></asp:Button>
            <asp:Button ID="AddAnswerButton" CssClass="btn btn-primary btn-xs bw" runat="server" Text="Add answer"></asp:Button>
            <asp:Button ID="DeleteAnswerButton" CssClass="btn btn-primary btn-xs bw" runat="server" Text="Delete"></asp:Button>
            <asp:Button ID="CancelAnswerButton" CssClass="btn btn-primary btn-xs bw" runat="server" Text="Cancel"></asp:Button>
    <br />
                    </li>
                   
           <asp:PlaceHolder ID="ExtendedPlaceholder" Visible="False" runat="server">
 <li>

                            <asp:Label ID="EditExtendedSettingsTitle" AssociatedControlID ="ExtendedPropertiesPlaceholder" runat="server" Text="Edit extended settings"
                                EnableViewState="False"></asp:Label>
                            <br /><br />

                        <asp:PlaceHolder ID="ExtendedPropertiesPlaceholder" Visible="False" runat="server"> </asp:PlaceHolder>
                                   </li>
                    
            </asp:PlaceHolder>
<li>

            <asp:Label ID="PipeHelpLabel" runat="server">
*you can pipe special values that will be replaced at runtime into the text value :<br />
##yourquerystringvariablename##<br />
@@yoursessionvariablename@@<br />
&&yourcookievariablename&&<br />
%%servervariablename%%
            </asp:Label>

                            </li>
                    </ol>
                </fieldset>
