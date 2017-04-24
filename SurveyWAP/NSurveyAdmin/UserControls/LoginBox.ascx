<%@ Control Language="C#" AutoEventWireup="true" ClassName="LoginBox" Inherits="Votations.NSurvey.WebAdmin.NSurveyAdmin.UserControls.LoginBox" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" Codebehind="LoginBox.ascx.cs" %>

                    <div style="position:absolute; padding-left: 3px; padding-top:5px; min-width:210px;">
                    <asp:Label ID="MessageLabel" runat="server" CssClass="errorMessage"  Visible="False"></asp:Label>
                    </div>

                    <div style="margin-left:10px; margin-top:40px;
                    background-color:rgba(221,221,221,0.3); 
                    border: 1px solid white ;
                    -moz-border-radius: 0.45em; 
                    border-radius: 0.45em; 
                    padding: 10px; 
                    text-align:left;" 
                    >
                                <table title="Login Form" class="normal">
                                    <tr>
                                        <td>
                                            <div class="FPtitleFont">
                                                 <span class="fa fa-sign-in" style="font-size:1.2vw;"></span>&nbsp;&nbsp;&nbsp;
                                                <asp:Literal ID="NSurveyAuthenticationTitle" runat="server" EnableViewState="False">Survey Administation Authentication</asp:Literal>

                                            </div> 

                                         </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <br />

                                            <div class="form-4">
                                            <p class="field"> 
                                                 
                                                <asp:TextBox ID="LoginTextBox" ToolTip="Enter Username" Placeholder="UserName" TextMode="SingleLine" runat="server"></asp:TextBox>
                                                <i class="icon-user"></i>                                               
                                            </p>
                                            </div>
                                        </td>

                                    </tr>
                                    <tr>
                                        <td>

                                            <div class="form-4">
                                            <p class="field">
                                                
                                                <asp:TextBox ID="PasswordTextBox" ToolTip="Enter Password" runat="server" Placeholder="Password" TextMode="Password"></asp:TextBox>
                                                <i class="icon-lock"></i>
                                            </p>
                                            </div>
                                        </td>

                                    </tr>
                                    <tr>
                                        <td>
                                            <div style="margin: 10px -10px 0 0;">
                                                <asp:Button ID="ValidateCredentialsButton" ToolTip="Validate credentials" CssClass="btn btn-primary bw" runat="server" Text="Validate credentials"></asp:Button>
                                            </div>
                                        </td>
                                    </tr>
                                </table>

                               

                </div>


