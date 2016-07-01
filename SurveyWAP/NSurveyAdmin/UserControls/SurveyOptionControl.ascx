
<%@ Register TagPrefix="uc1" TagName="SurveyListControl" Src="SurveyListControl.ascx" %>
<%@ Control Language="c#" AutoEventWireup="false" Inherits="Votations.NSurvey.WebAdmin.UserControls.SurveyOptionControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" Codebehind="SurveyOptionControl.ascx.cs" %>


    <script type="text/javascript">
        $(function () {

            $("#<%=OpeningDateTextBox.ClientID%>").datepicker();
            $("#<%=CloseDateTextbox.ClientID%>").datepicker();
        });
    </script>


            <div style="position: absolute; width: 650px; text-align: center; margin-left: 57px; top: 15px;">
 <asp:Label ID="MessageLabel" runat="server"  CssClass="errorMessage" Visible="False"></asp:Label>
                </div>


<!-- New Survey -->
                                        <div style="position: relative; left: 720px; width: 10px;  top: 10px; clear:none;">
                                            <a onmouseover='this.style.cursor="help" ' onfocus='this.blur();' href="Help/Survey General Settings.aspx"
                                                title="Click for more Information">
                                                <img alt="help" border="0" src="<%= Page.ResolveUrl("~")%>Images/small_help.gif" />
                                            </a>
                                        </div>

            <fieldset style="width: 750px; margin-left: 12px; margin-top: 15px;" title="Survey Title">
                <legend class="titleFont" style="text-align: left; margin: 0px 15px 0 15px;">
                    <asp:Literal ID="SurveyInformationTitle2" runat="server" Text=""
                        EnableViewState="False"></asp:Literal></legend>

                <br />

                <ol>
                    <li>
                        <strong>
                            <asp:Label ID="SurveyTitleLabel" AssociatedControlID="TitleTextBox" runat="server" EnableViewState="False">Title:</asp:Label></strong>
                        <div id="tooltip">

                            <asp:TextBox
                                ID="TitleTextBox"
                                runat="server"
                                Columns="40" MaxLength="90">
                            </asp:TextBox>

                            <asp:Button ID="CreateSurveyButton" CssClass="btn btn-primary btn-xs bw" runat="server" Text="Create survey!"></asp:Button>
                        </div>

                    </li>
                </ol>
                <br />
            </fieldset>



     <asp:PlaceHolder ID="EditUi" runat="server">
         <br />
         <fieldset style="width: 750px; margin-left: 12px; margin-top: 15px;" title="Survey Settings">
             <legend class="titleFont" style="text-align: left; margin: 0px 15px 0 15px;">
                 <asp:Literal ID="SurveyInformationTitle" runat="server" Text="Survey information"
                     EnableViewState="False"></asp:Literal></legend>
             <br />
             <ol>
                 <li>
                     <strong>
                         <asp:Label ID="OpeningDateLabel" runat="server" AssociatedControlID="OpeningDateTextBox" EnableViewState="False">Opening date :</asp:Label></strong>
                     <div style="width: 100px; float: right;">
                         <asp:Literal ID="DateForExampleInfo" runat="server" EnableViewState="False">e.g.:</asp:Literal>
                         <asp:Label ID="DateFormatLabel" runat="server"></asp:Label>
                     </div>

                     <div id="tooltip">
                         <asp:TextBox ID="OpeningDateTextBox" ToolTip="<b>Opening Date:</b> This is the date as of which the survey will accept new entries. No entries will be accepted before that date. Leave blank if the survey has no particular opening date. "
                             runat="server" Columns="30"></asp:TextBox>
                     </div>

                 </li>
                 <li>
                     <strong>
                         <asp:Label ID="CloseDateLabel" AssociatedControlID="CloseDateTextBox" runat="server" EnableViewState="False">Close date :</asp:Label></strong>

                     <div id="tooltip">
                         <asp:TextBox ID="CloseDateTextbox"
                             runat="server" Columns="30"></asp:TextBox>
                     </div>
                 </li>
                 <li>
                     <strong>
                         <asp:Label ID="ProgressDisplayLabel" AssociatedControlID="ProgressDisplayDropDownList" runat="server" EnableViewState="False">Progress display :</asp:Label></strong>
                     <div id="tooltip">
                         <asp:DropDownList ID="ProgressDisplayDropDownList"
                             Width="177"
                             runat="server">
                         </asp:DropDownList>
                     </div>
                 </li>
                 <li>
                     <strong>
                         <asp:Label ID="DisableQuestionNumbering" AssociatedControlID="QuestionNumberingCheckbox" runat="server" EnableViewState="False">Disable question numbering:</asp:Label></strong>
                     <div id="tooltip">
                         <asp:CheckBox ID="QuestionNumberingCheckbox" runat="server"></asp:CheckBox>
                     </div>
                 </li>
                 <li>
                     <strong>
                         <asp:Label ID="ActiveSurveyLabel" AssociatedControlID="ActiveCheckBox" runat="server" EnableViewState="False">Active:</asp:Label></strong>
                     <div id="tooltip">
                         <asp:CheckBox ID="ActiveCheckBox" runat="server"></asp:CheckBox>
                     </div>
                 </li>
                 <li>
                     <strong>
                         <asp:Label ID="DefaultSurveyLabel" AssociatedControlID="DefaultSurveyCheckBox" runat="server" EnableViewState="False">Default Survey:</asp:Label></strong>
                     <div id="tooltip">
                         <asp:CheckBox ID="DefaultSurveyCheckBox" runat="server"></asp:CheckBox>
                     </div>
                 </li>
                 <!-- archive not used?? 
              <li>    
                            <strong>
                                <asp:Label ID="ArchivedLabel" AssociatedControlId="ArchiveCheckBox" runat="server" EnableViewState="False"></asp:Label></strong>
                    <div id="tooltip">
                            <asp:CheckBox ID="ArchiveCheckBox" runat="server"></asp:CheckBox>
                            </div>
              </li> 
                  -->

                 <li>
                     <strong>
                         <asp:Label ID="ScoredLabel" AssociatedControlID="ScoredCheckbox" runat="server" EnableViewState="False">Scored ::</asp:Label></strong>
                     <div id="tooltip">
                         <asp:CheckBox ID="ScoredCheckbox" runat="server"></asp:CheckBox>
                     </div>
                 </li>
                 <li>
                     <strong>
                         <asp:Label ID="EnableNavigationLabel" AssociatedControlID="EnableNavigationCheckbox" runat="server" EnableViewState="False">Enable navigation :</asp:Label></strong>
                     <div id="tooltip">
                         <asp:CheckBox ID="EnableNavigationCheckbox" runat="server"></asp:CheckBox>
                     </div>
                 </li>
                 <li>
                     <strong>
                         <asp:Label ID="EnableResumeLabel" AssociatedControlID="ResumeModeDropdownlist" runat="server" EnableViewState="False">resume of progress :</asp:Label></strong>
                     <div id="tooltip">
                         <asp:DropDownList ID="ResumeModeDropdownlist" Width="325" runat="server">
                         </asp:DropDownList>
                     </div>


                 </li>
             </ol>
             <br />
         </fieldset>
<!-- Notification Settings -->


            <div style="position: relative; left: 720px; width: 10px;  top: 15px; clear:none;">
                <a onmouseover='this.style.cursor="help" ' onfocus='this.blur();' onclick="document.getElementById('PopUp2').style.display = 'block' ">
                    <img title="Click for more Information" alt="help" src="<%= Page.ResolveUrl("~")%>Images/small_help.gif" />
                </a>
                <div id='PopUp2' style='display: none; position: absolute; left: -420px; top: -315px;
                    border: solid #CCCCCC 1px; padding: 10px; background-color: rgb(255,255,225); line-height:15px; text-align: justify;
                    font-size: 11px; width: 425px; -webkit-border-radius: 5px;  -moz-border-radius: 5px; border-radius: 5px;'>
                    <asp:Literal ID="NotificationsSettingsHelp" runat="server" EnableViewState="False">Notification Settings Help</asp:Literal>
                    <br />
                    <div style='text-align: right;'>
                        <a onmouseover='this.style.cursor="pointer" ' style='font-size: 12px;' onfocus='this.blur();'
                            onclick="document.getElementById('PopUp2').style.display = 'none' "><img alt="Close" title="Close" src="<%= Page.ResolveUrl("~")%>Images/close-icn.png" /></a></div>
                </div>
            </div>

         <fieldset style="width: 750px; margin-top:15px; margin-left: 12px;" title="Notification Settings">
             <legend class="titleFont" style="text-align: left; margin: 0px 15px 0 15px;">
                 <asp:Literal ID="NotificationSettingsTitle" runat="server" EnableViewState="False" Text="Notification settings"></asp:Literal>
             </legend>
             <br />

             <ol>
                 <li>
                     <strong>
                         <asp:Label ID="EntryNotificationLabel" AssociatedControlID="EntryNotificationDropdownlist" runat="server" EnableViewState="False">Entry notification :</asp:Label></strong>
                     <asp:DropDownList ID="EntryNotificationDropdownlist" runat="server" AutoPostBack="true">
                     </asp:DropDownList>

                 </li>
                 <asp:PlaceHolder ID="NotificationPlaceHolder" runat="server">
                     <li>
                         <strong>
                             <asp:Label ID="FromEmailNotificationLabel" AssociatedControlID="EmailFromTextbox" runat="server" EnableViewState="False">Email from :</asp:Label></strong>

                         <asp:TextBox Width="350px" ID="EmailFromTextbox" runat="server"></asp:TextBox>
                     </li>
                     <li>
                         <strong>
                             <asp:Label ID="ToEmailNotificationLabel" AssociatedControlID="EmailToTextBox" runat="server" EnableViewState="False">Email to:</asp:Label></strong>

                         <asp:TextBox Width="350px" ID="EmailToTextBox" runat="server"></asp:TextBox>
                     </li>
                     <li>
                         <strong>
                             <asp:Label ID="NotificationSubjectLabel" AssociatedControlID="EmailSubjectTextbox" runat="server" EnableViewState="False"
                                 Text="">Email subject :</asp:Label></strong>

                         <asp:TextBox Width="350px" ID="EmailSubjectTextbox" runat="server"></asp:TextBox>
                     </li>
                 </asp:PlaceHolder>


             </ol>
             <br />
         </fieldset>


     </asp:PlaceHolder>

<br />
<asp:Button ID="ApplyChangesButton" CssClass="btn btn-primary btn-xs" runat="server" Text="Apply changes" onclick="ApplyChangesButton_Click1"></asp:Button>
<asp:Button ID="DeleteButton" CssClass="btn btn-primary btn-xs" runat="server" Text="Delete"></asp:Button>
<asp:Button ID="CloneButton" CssClass="btn btn-primary btn-xs" runat="server" Text="Clone"></asp:Button>
<asp:Button ID="ExportSurveyButton" CssClass="btn btn-primary btn-xs" runat="server" Text="Export XML"></asp:Button>
<br /><br />

<!--Menu New Survey -  Import Survey -->

<asp:PlaceHolder runat="server" ID="SurveyImportPlaceHolder">
<div style=" display:inline-block; float:left; clear:both; width:750px; left:30px; position:absolute; top:195px;">     
    <fieldset title="Import Survey">
        <legend class="titleFont" style="text-align: left; margin: 15px;">
           <asp:Literal ID="ImportSurveyLiteral" runat="server" EnableViewState="false">Import</asp:Literal>
        </legend>

        <ol>
            <li>

                <asp:Label ID="ImportSurveyTitle" AssociatedControlID="ImportFile" runat="server" EnableViewState="False">Ïmport Survey from XML</asp:Label>

                <input id="ImportFile" type="file" size="40" style="margin-left: 15px;" name="ImportFile" runat="server" />

                <asp:Button ID="ImportXMLButton" CssClass="btn btn-primary btn-xs bw" runat="server" Text="Import XML"></asp:Button>


            </li>
        </ol>
        <br />

    </fieldset>
       </div>
</asp:PlaceHolder>

