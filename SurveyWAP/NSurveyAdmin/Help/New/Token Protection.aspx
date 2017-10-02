<%@ Page Language="c#" MasterPageFile="~/nsurveyadmin/MsterPageTabs.master" AutoEventWireup="false"
    Inherits="Votations.NSurvey.WebAdmin.HelpFiles" CodeBehind="../default.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><div id="helpPanel"><div>
                <asp:ImageButton ID="btnBack" ImageUrl="~/Images/index-icon.png" runat="server" CssClass="buttonIndex"
                    PostBackUrl="~/NSurveyAdmin/Help/default.aspx#Token" Visible="True" ToolTip="Back to Helpfiles Index" />
            </div><div>
                <h2 style="color:#5720C6;">
                    Token Protection</h2><hr style="color:#e2e2e2;" />
              
This security addin will protect a survey using a unique token generated
using the <a href="Token_Introduction.aspx"> token generator</a>. To enable the Token Security Addin go to Menu Security/ Form Security and select Insert Security Addin/ Token Protection<br />
<br />
<u>Token Generator Settings</u>
    <br /><br />
* <i>Generate</i> - enter the number of tokens to be generated and click the generate button. Additional/ new tokens can be added any time.
                <br />
* <i>Created Tokens</i> - number of tokens that have been created by the generator
<br />
* <i>Available Tokens</i> - is the number of token available for use. <br />

* <i>Used Tokens</i> - is the number of token that have been already used. A
  token can only be used once. <br />
<br />
                <u>Tokenlist Filter Options</u>
<br /><br />
* <i>By Token</i> - option to filter the list of tokens based on an individual token<br />
* <i>By TokenType</i> - option to filter the list tokens by Used Only, Only Not Used or All<br />
* <i>By Issued Date</i> - option to filter the list of tokens based on issue date (enter dateformat: m/dd/yyyy)
                <br /><br />
Note: Filter by tokentype and issued date can be combined.<br /><br />

<u>List of Generated Tokens</u>
<br /><br />
* <i>Used/ Not Used</i> - color marker indicating used (red) unused (green) status of token<br />
* <i>Select Checkbox</i> -  option to select tokens from the tokenlist; selected tokens will be affected by the Delete Selected button<br />
* <i>Export Tokens</i> -   will generate a .csv file containing all tokens (selection will have no effect)<br />
* <i>Delete Selected</i> - will delete tokens marked for selection by selecting the checkbox<br />
* <i>Delete All</i> -  will delete all tokens irrespective of the selected status<hr style="color:#e2e2e2;" />

                <h3>
                    More Information</h3>
                <br />
                <a href="Token_Introduction.aspx">Token Introduction</a><br />
<a href="../Survey%20Security.aspx" title="Survey Security">Security</a><br />
<a href="../Sec_Introduction.aspx" title="Security Introduction">Security Introduction</a><br />
                <br />
            </div>
<div id="fillerDiv" class="fillerDiv">&nbsp;</div></div></asp:Content>

