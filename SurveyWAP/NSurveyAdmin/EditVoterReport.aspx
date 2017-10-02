<%@ Page language="c#" MasterPageFile="MsterPageTabs.master"   validaterequest="false" AutoEventWireup="false" Inherits="Votations.NSurvey.WebAdmin.EditVoterReport" Codebehind="EditVoterReport.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

        <div id="Panel" class="Panel">


              <div class="errorDiv">
 <asp:Label ID="MessageLabel" runat="server"  CssClass="errorMessage" Visible="False"></asp:Label>
                </div>

             <fieldset>
        <legend class="titleFont titleLegend">
                                <asp:Literal ID="VoterInformationTitle" runat="server" EnableViewState="False">Voter information</asp:Literal></font>
                </legend><br />

                            <table class="innerText TableLayoutContainer">
                                <tr>
                                    <td style="width:160px;">
                                        <strong>
                                            <asp:Literal ID="VoterDBIDLabel" runat="server" EnableViewState="False">Voter DB ID :</asp:Literal></strong>
                                    </td>
                                    <td>
                                        <asp:Label ID="VoterUIDLabel" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width:160px;">
                                        <strong>
                                            <asp:Literal ID="VoterUserNameLabel" runat="server" EnableViewState="False">Voter user name :</asp:Literal></strong>
                                    </td>
                                    <td>
                                        <asp:Label ID="VoterUserName" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width:160px;">
                                        <strong>
                                            <asp:Literal ID="VoterEmailLabel" runat="server" EnableViewState="False">Voter Email :</asp:Literal></strong>
                                    </td>
                                    <td>
                                        <asp:Label ID="VoterEmail" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width:160px;">
                                        <strong>
                                            <asp:Literal ID="VoterIPAddressLabel" runat="server" EnableViewState="False">IP address :</asp:Literal></strong>
                                    </td>
                                    <td>
                                        <asp:Label ID="IPAddressLabel" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width:160px;">
                                        <strong>
                                            <asp:Literal ID="VoteRecordedLabel" runat="server" EnableViewState="False">Vote recorded on :</asp:Literal></strong>
                                    </td>
                                    <td>
                                        <asp:Label ID="VoteDateLabel" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width:160px; vertical-align:top;">
                                        <strong>
                                            <asp:Literal ID="TimeToTakeLabel" runat="server" EnableViewState="False">Time to take the survey :</asp:Literal></strong>
                                    </td>
                                    <td>
                                        <asp:Label ID="TimeToTakeSurveyLabel" runat="server"></asp:Label>&nbsp;<br />
                                        <br />
                                    </td>
                                </tr>
                            </table>
                               </fieldset>

                <asp:PlaceHolder ID="AddInVoterDataPlaceHolder" runat="server"></asp:PlaceHolder>

                            <div style="width:470px;">
                            <asp:Button ID="ReadOnlyAnswersLinkButton" CssClass="btn btn-primary btn-xs bw" runat="server" OnClick="ReadOnlyAnswersLinkButton_Click" />
                                </div>

                <br /> <br />

                 <fieldset>
        <legend class="titleFont titleLegend">
                                <asp:Literal ID="EditSurveyAnswersTitle" runat="server" EnableViewState="False" Text="Edit survey answers"></asp:Literal>
                                         </legend>

                            <table class="innerText TableLayoutContainer">
                                <tr>
                                    <td>
                                        <asp:PlaceHolder ID="EditAnswersPlaceHolder" runat="server"></asp:PlaceHolder>
                                        <asp:Button ID="UpdateVoterAnswersButton" CssClass="btn btn-primary btn-xs bw" runat="server" Text=""></asp:Button>
                                   <br /> <br />
                                    </td>
                                </tr>
                            </table>
                                 </fieldset>
             <br />
             <div id="fillerDiv" class="fillerDiv">&nbsp;</div>
</div>

</asp:Content>
