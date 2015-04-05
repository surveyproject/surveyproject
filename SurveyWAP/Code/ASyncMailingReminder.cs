/**************************************************************************************************

	NSurvey - The web surveys and forms engine
	Copyright (c) 2004, Thomas Zumbrunn. (http://www.nsurvey.org)

	This program is free software; you can redistribute it and/or
	modify it under the terms of the GNU General Public License
	as published by the Free Software Foundation; either version 2
	of the License, or (at your option) any later version.

	This program is distributed in the hope that it will be useful,
	but WITHOUT ANY WARRANTY; without even the implied warranty of
	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
	GNU General Public License for more details.

	You should have received a copy of the GNU General Public License
	along with this program; if not, write to the Free Software
	Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA  02111-1307, USA.

************************************************************************************************/
using System;
using Votations.NSurvey.Emailing;
using Votations.NSurvey.BusinessRules;

namespace Votations.NSurvey.WebAdmin
{

	/// <summary>
	/// Handles asyncronous mailing
	/// </summary>
	public class ASyncMailingReminder
	{
		private delegate void RemindRecipientsDelegate(int surveyId);
		private RemindRecipientsDelegate remindRecipientsDelegate;

		public ASyncMailingReminder()
		{
			remindRecipientsDelegate = new RemindRecipientsDelegate(this.RemindRecipients);
		}

		public void RemindRecipients(int surveyId)
		{
			while (true)
			{
				IEmailing mailService = EmailingFactory.Create();
				EmailingMessage message = new EmailingMessage();
				message.FromEmail = "mail@surveyproject.org";
				message.Subject = DateTime.Now.ToString();
				message.Body = "ok";
				message.ToEmail = "mail@surveyproject.org";
				mailService.SendEmail(message);
				System.Threading.Thread.Sleep(10000); // 10 seconds wait
			}


		}

		// Begins an asynchronous call
		public System.IAsyncResult RemindRecipients(int surveyId, System.AsyncCallback callback, object state)
		{
			return this.remindRecipientsDelegate.BeginInvoke(surveyId, callback, state);
		}

		// Waits for the pending asynchronous request to complete.
		public void EndremindRecipients(System.IAsyncResult result)
		{
			this.remindRecipientsDelegate.EndInvoke(result);
		}
	}

}
