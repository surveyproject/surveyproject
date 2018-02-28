<%@ Page Language="c#" MasterPageFile="~/nsurveyadmin/MsterPageTabs.master" AutoEventWireup="false"
    Inherits="Votations.NSurvey.WebAdmin.HelpFiles" CodeBehind="../default.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><div id="helpPanel"><div>
                <asp:ImageButton ID="btnBack" ImageUrl="~/Images/index-icon.png" runat="server" CssClass="buttonIndex"
                    PostBackUrl="~/NSurveyAdmin/Help/default.aspx#SurveyDesigner" Visible="True" ToolTip="Back to Helpfiles Index" />
            </div><div>
                <h2 style="color:#5720C6;">
                    Question Groups</h2><hr style="color:#e2e2e2;" />

                Question Groups and Subgroups can be used to label or categorize a set (group) of questions in the database (QuestionGroupID). 
                Group- and subgroup names do not appear on any screen in surveys and are not actively used in the code of the webapplication.
                They are there for reporting, analyzing and querying purposes.
                <br /><br />
                Before adding ('labeling') a question to a group or subgroup the Groups and Subgroups have to be created through the Designer/ Question 
                Groups menu. After creating one of more Groups and/or Subgroups the names will become availabe and visible 
                in the <a href="Question Editor.aspx" title=" Question Editor " > Question Editor </a> by clicking the Rating option. 
<br /><br />

<u>Creating Groups & Subgroups</u><br /><br />

<i>* Language </i><br /> - available if the Multilanguage feature is enabled. Option to choose and switch the languages to create 
                Groups/ Subgroups for different survey language versions.  
              <br /><br />
<i>* Groups - Add New </i><br /> - 
                By default (on opening the Question Groups page) the first Group Name entry field is shown. It can be used to create the first groupname. To create more Groupnames clickt the Add New link.

                <br /><br />
<i>* Name </i><br /> - entry field to write/ edit the the Group name


  <br /><br />
                <i>* Edit </i><br /> - button to change/ edit an existing Group name  <br /><br />
                <i>* Delete </i><br /> - button to delete the Group name from the list of names. Any subgroups linked to the Group will be removed as well.
                  <br /><br />
 <i>* Order - radio button </i><br /> - if multiple Groupnames have been created and a Subgroup has to be added to one of the Groups click the radio button in front of the Group name. 
                This will show the Group name next to the 'Subgroup for' label.

                  <br /><br />
                <i>* Sub Groups for - Add New </i><br /> - click to add a new Subgroup name to a Group. 
<br />
<br />
                <i>* Edit </i><br /> - button to change/ edit an existing SubGroup name  <br /><br />
                <i>* Delete </i><br /> - button to delete the SubGroup name from the list of names. 
                  <br /><br />
                 <i>* Order - Up/ Down Arrows</i><br /> - used to (re)order the list of Groups names. The order is also used in the drop down list on the
                question editor to select Groups and Subgroups.<br />
<br />

                <hr style="color:#e2e2e2;"/>
                <h3>
                    More Information</h3>
                <br />
                 <a href="Question Editor.aspx" title=" Question Editor " > Question Editor </a><br />
                <a href="Rating_Introduction.aspx" title=" Rating Introduction " >Rating Introduction</a><br />
                <br />
            </div>
<div id="fillerDiv" class="fillerDiv">&nbsp;</div></div></asp:Content>

