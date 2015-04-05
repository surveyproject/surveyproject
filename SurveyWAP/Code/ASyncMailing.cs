//-----------------------------------------------------------------------
// <copyright file="ASyncMailing.cs" company="Fryslan Webservices">
//
// Survey changes: copyright (c) 2010, Fryslan Webservices TM (http://survey.codeplex.com) 
//
// NSurvey - The web survey and form engine
// Copyright (c) 2004, 2005 Thomas Zumbrunn. (http://www.nsurvey.org)
//
// This program is free software; you can redistribute it and/or
// modify it under the terms of the GNU General Public License
// as published by the Free Software Foundation; either version 2
// of the License, or (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program; if not, write to the Free Software
// Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA  02111-1307, USA.
//
// </copyright>
//-----------------------------------------------------------------------

namespace Votations.NSurvey.WebAdmin
{
using System;
using Votations.NSurvey.BusinessRules;
using Votations.NSurvey.Data;
using Votations.NSurvey.Emailing;
 
 /// <summary>
 /// Handles asyncronous mailing
 /// </summary>
 public class ASyncMailing
 {
 /// <summary>
 /// description: mailItDelegate
 /// </summary>
  private MailItDelegate mailItDelegate;

  /// <summary>
  /// Constructor: Initializes a new instance of the ASyncMailing class. 
  /// </summary>
  public ASyncMailing()
  {
   this.mailItDelegate = new MailItDelegate(this.MailIt);
  }
  
  /// <summary>
  /// description: .....
  /// </summary>
  /// <param name="sessionId">description .....</param>
  /// <param name="surveyId">description ....</param>
  /// <param name="anonymousEntries">description ..</param>
  /// <param name="mailingEmails">description .......</param>
  /// <param name="message">description ......</param>
  private delegate void MailItDelegate(string sessionId, int surveyId, bool anonymousEntries, string mailingEmails, EmailingMessage message); 

  /// <summary>
  /// description: ......
  /// </summary>
  /// <param name="sessionId">description .....</param>
  /// <param name="surveyId">description ....</param>
  /// <param name="anonymousEntries">description ..</param>
  /// <param name="mailingEmails">description .......</param>
  /// <param name="message">description ......</param>
  public void MailIt(string sessionId, int surveyId, bool anonymousEntries, string mailingEmails, EmailingMessage message)
  {
   IEmailing mailService = EmailingFactory.Create();
   string guid = null,
     taggedMessage = message.Body,
     email;
   string[] mailingList = mailingEmails.Split(',');

   SyncDataStore.SetRecords(sessionId + ":" + "Progress", 0);
   SyncDataStore.SetRecords(sessionId + ":" + "FailedEmails", 0);

   int failedEmails = 0;
    
   // Send the invitation email to the list
   for (int i = 0; i < mailingList.Length; i++)
   {
    email = mailingList[i].Trim();
    guid = null;
    try
    {
     guid = new Voter().GenerateVoterInvitationUId(surveyId, email, anonymousEntries);
     message.Body = message.Body.Replace("[--invitationid-]", guid);
     message.ToEmail = email;
     mailService.SendEmail(message);
     message.Body = taggedMessage;
    }
    catch (Exception ex)
    {
     try
     {
      if (guid != null)
      {
       // rollback db state if an email has been saved
       new Voter().DeleteVoterInvitation(surveyId, email);
      }

      InvitationLogData invitationLogs = new InvitationLogData();
      InvitationLogData.InvitationLogsRow invitationLog = invitationLogs.InvitationLogs.NewInvitationLogsRow();
      invitationLog.ExceptionMessage = (ex.InnerException != null && ex.InnerException.InnerException != null) ? 
       ex.InnerException.InnerException.Message : ex.Message.ToString();
      invitationLog.ExceptionType = (ex.InnerException != null && ex.InnerException.InnerException != null) ? 
       ex.InnerException.InnerException.GetType().ToString() : ex.GetType().ToString();
      invitationLog.SurveyId = surveyId;
      invitationLog.Email = email;
      invitationLog.ErrorDate = DateTime.Now;
      invitationLogs.InvitationLogs.AddInvitationLogsRow(invitationLog);
     
      // Log error in the database
      new Voter().LogInvitationError(invitationLogs);
      failedEmails++;
     }
     catch (Exception e)
     { 
      SyncDataStore.SetRecords(sessionId + ":" + "Error", e);
     }
    }

    SyncDataStore.UpdateRecords(sessionId + ":" + "Progress", (i * 100) / mailingList.Length);
    SyncDataStore.UpdateRecords(sessionId + ":" + "FailedEmails", failedEmails);
   }
  }

  /// <summary>
  /// description: Begins an asynchronous call
  /// </summary>
  /// <param name="sessionId">description .....</param>
  /// <param name="surveyId">description ....</param>
  /// <param name="anonymousEntries">description ..</param>
  /// <param name="mailingEmails">description .... ...</param>
  /// <param name="message">description ......</param>
  /// <param name="callback">description .. .....</param>
  /// <param name="state">description ..... .</param>
  /// <returns>Description returned value.</returns>
  public System.IAsyncResult BeginMailIt(string sessionId, int surveyId, bool anonymousEntries, string mailingEmails, EmailingMessage message, System.AsyncCallback callback, object state)
  {
   return this.mailItDelegate.BeginInvoke(sessionId, surveyId, anonymousEntries, mailingEmails, message, callback, state);
  }

  /// <summary>
  /// Description: Waits for the pending asynchronous request to complete.
  /// </summary>
  /// <param name="result">description .....</param>
  public void EndMailIt(System.IAsyncResult result)
  {
   this.mailItDelegate.EndInvoke(result);
  }
 }
}