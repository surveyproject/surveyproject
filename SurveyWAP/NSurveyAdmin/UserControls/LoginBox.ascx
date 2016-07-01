<%@ Control Language="C#" AutoEventWireup="true" ClassName="LoginBox" Inherits="Votations.NSurvey.WebAdmin.NSurveyAdmin.UserControls.LoginBox" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" Codebehind="LoginBox.ascx.cs" %>

                    <div style="position:absolute; padding-left: 3px; padding-top:5px; min-width:210px;">
                    <asp:Label ID="MessageLabel" runat="server" CssClass="errorMessage"  Visible="False"></asp:Label>
                    </div>

                    <div style="margin-left:5px; margin-top:40px;
                    background-color:rgba(221,221,221,0.3); 
                    border: 1px outset white ;
                    -moz-border-radius: 0.45em; 
                    border-radius: 0.45em; 
                    padding: 10px; 
                    width:205px; 
                    text-align:left;" 
                    >
                                <table title="Survey&#8482; Project Loginbox" class="normal">
                                    <tr>
                                        <td>
                                            <div class="FPtitleFont">
                                                
                                                <asp:Literal ID="NSurveyAuthenticationTitle" runat="server" EnableViewState="False">Survey Administation Authentication</asp:Literal>
                                                <span class="fa-sign-in"></span>
                                            </div> 
                                            <!--
                                       <div style=" position:relative; left:155px; top:115px; ">

                                           <a onmouseover='this.style.cursor="help" ' onfocus='this.blur();' onclick="document.getElementById('PopUp').style.display = 'block' ">
                                               <img title='<asp:Literal ID="HelpGifTitle" runat="server" />' alt="help" src="Images/help.gif" />
                                               </a>
                                           <div id='PopUp' style='display: none; position: absolute; left: -200px; top: -50px;
                                               border: solid #CCCCCC 1px; padding: 10px; background-color: rgba(255,255,225,0.9); text-align: justify; overflow:auto; 
                                               font-size: 12px; width: 175px; -webkit-border-radius: 7px;  -moz-border-radius: 7px;   border-radius: 7px;'>
                                               <asp:Literal ID="Introduction" runat="server" EnableViewState="False">Introduction</asp:Literal>
                                               <br />
                                               <div style='text-align: right;'>
                                                   <a onmouseover='this.style.cursor="pointer" ' style='font-size: 12px;' onfocus='this.blur();'
                                                       onclick="document.getElementById('PopUp').style.display = 'none' "><img alt="Close" title="Close" src="<%= Page.ResolveUrl("~")%>Images/close-icn.png" />  </a>
                                                       </div>
                                           </div>

                                         </div>
                                            -->
                                         </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <br />
                                            <!--<strong>
                                             <asp:Literal ID="LoginLabel" runat="server" EnableViewState="False">Login :</asp:Literal></strong><br /> -->
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
                                           <!-- <strong>
                                                <asp:Literal ID="PasswordLabel" runat="server" EnableViewState="False">Password :</asp:Literal></strong><br /> -->
                                            <div class="form-4">
                                            <p class="field">
                                                
                                                <asp:TextBox ID="PasswordTextBox" ToolTip="Enter Password" runat="server" Placeholder="Password" TextMode="Password"></asp:TextBox>
                                                <i class="icon-lock"></i>
                                            </p>
                                            </div>
                                        </td>

                                    </tr>
                                    <tr><td><div style="width:160px; margin-top:10px;"><asp:Button ID="ValidateCredentialsButton" CssClass="btn btn-primary btn-xs bw" runat="server" Text="Validate credentials">
                                </asp:Button></div></td></tr>
                                </table>

                               

                </div>


