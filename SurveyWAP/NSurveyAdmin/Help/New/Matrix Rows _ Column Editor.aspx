<%@ Page Language="c#" MasterPageFile="~/nsurveyadmin/MsterPageTabs.master" AutoEventWireup="false"
    Inherits="Votations.NSurvey.WebAdmin.HelpFiles" CodeBehind="../default.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><div id="helpPanel"><div>
                <asp:ImageButton ID="btnBack" ImageUrl="~/Images/index-icon.png" runat="server" CssClass="buttonIndex"
                    PostBackUrl="~/NSurveyAdmin/Help/default.aspx#SurveyDesigner" Visible="True" ToolTip="Back to Helpfiles Index" />
            </div><div>
                <h2 style="color:#5720C6;">
                    Matrix Rows & Columns Editor</h2><hr style="color:#e2e2e2;" />          

The matrix rows and columns editor is used for adding and editing rows and columns that are part of the the matrix question.<br />
<br />

                * add either a new column or a new row to the matrix.<br />
* edit either a column or a row inside the matrix.<br />
<br />

                <u>Insert a Row or Column</u>
 <br />
<br />



<i>* Row (Question) </i><br />- is the 'question' text of the row.<br />
<br />

<i>* Column (Header)  </i><br />-  the text that will appear in the matrix's column's
  header<br />
<br />


<u>Current Rows and Columns</u><br />
<br />

<b>Rows</b><br /><br />

              <i>  * Edit  </i><br />-  click to edit/ change the text of the row
                <br /><br />
<i>* Delete  </i><br />- deletes a row. Respondent answers related to the row
  columns will also be deleted and cannot be recovered afterward.<br />
<br />
<b>Columns</b><br /><br />
<i>* Type </i><br />- option to set the current answertype used for a column. Respondent answers already collected will not be deleted if the answertype is changed.<br />
<br />

<i>* Rating </i><br />- option to determine if this selection is to be part of the
  rating calculation in the reports. To learn more about rating
  read the <a href="Rating_Introduction.aspx" title=" Rating Introduction " > Rating Introduction </a>.<br />
<br />
  This feature is only available if rating is activated on the matrix question of the row / columns set.<br /><br />
                
<i>* Mandatory</i><br />-  option to make answering mandatory or not               
                
            
              <i>  * Edit  </i><br />-  click to edit/ change the text and settings of the column
                <br /><br />
<i>* Delete  </i><br />- deletes a column. Respondent answers related to the row
  columns will also be deleted and cannot be recovered afterward.<br />
<br />                
                
                <hr style="color:#e2e2e2;" />
        
                <h3>
                    More Information</h3>
                <br />
<a href="AT_Introduction.aspx" title=" Answer Types Introduction " > Answer Types Introduction </a>	<br />
<a href="Matrix Question Editor.aspx" title=" Matrix Question Editor " > Matrix Question Editor </a><br />
                <a href="Matrix Question.aspx" title=" Matrix Question " > Matrix Question </a>	<br />
<a href="Rating_Introduction.aspx" title=" Rating Introduction " > Rating Introduction </a><br />
                <br />
            </div>
<div id="fillerDiv" class="fillerDiv">&nbsp;</div></div></asp:Content>

