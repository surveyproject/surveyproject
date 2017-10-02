<%@ Control Language="C#" AutoEventWireup="true" ClassName="LoginBox" Inherits="Votations.NSurvey.WebAdmin.NSurveyAdmin.UserControls.LoginBox" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" Codebehind="LoginBox.ascx.cs" %>

                    <div class="loginErrorDiv">
                    <asp:Label ID="MessageLabel" runat="server" CssClass="errorMessage"  Visible="False"></asp:Label>
                    </div>
                    <div class="loginBoxDiv">
                                <div title="Login Form" class="normal">

                                            <div class="FPtitleFont">
                                                 <span class="fa fa-sign-in" style="font-size:1.2vw;"></span>&nbsp;&nbsp;&nbsp;
                                                <asp:Literal ID="NSurveyAuthenticationTitle" runat="server" EnableViewState="False">Survey Administation Authentication</asp:Literal>

                                            </div> 
  
                                            <br />

                                            <div class="form-4">
                                            <p class="field"> 
                                                 
                                                <asp:TextBox ID="LoginTextBox" ToolTip="Enter Username" Placeholder="UserName" TextMode="SingleLine" runat="server"></asp:TextBox>
                                                <i class="icon-user"></i>                                               
                                            </p>
                                            </div>


                                            <div class="form-4">
                                            <p class="field">
                                                
                                                <asp:TextBox ID="PasswordTextBox" ToolTip="Enter Password" runat="server" Placeholder="Password" TextMode="Password"></asp:TextBox>
                                                <i class="icon-lock"></i>
                                            </p>
                                            </div>

                                            <div class="validateButton">
                                                <asp:Button ID="ValidateCredentialsButton" ToolTip="Validate credentials" CssClass="btn btn-primary bw" runat="server" Text="Validate credentials"></asp:Button>
                                            </div>
                                </div>                              
                </div>


