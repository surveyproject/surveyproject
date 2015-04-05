/**************************************************************************************************

	NSurvey - The web survey and form engine
	Copyright (c) 2004, 2005 Thomas Zumbrunn. (http://www.nsurvey.org)

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
using System.Text;
using System.Net.Mail;

namespace Votations.NSurvey.Emailing
{
	/// <summary>
	/// Represents a message that can be 
	/// send using the emailing service
	/// </summary>
	public class EmailingMessage
	{
		public string FromName
		{
			get { return _fromName; }
			set { _fromName = value; }
		}

		public string FromEmail
		{
			get { return _fromEmail; }
			set { _fromEmail = value; }
		}

		public string ToEmail
		{
			get { return _toEmail; }
			set { _toEmail = value; }
		}

		public string toName
		{
			get { return _toName; }
			set { _toName = value; }
		}

		public string Subject
		{
			get { return _subject; }
			set { _subject = value; }
		}

		public string Body
		{
			get { return _body; }
			set { _body = value; }
		}

		public EmailFormat Format
		{
			get { return _format; }
			set { _format = value; }
		}

	
		string	_fromName,
				_fromEmail,
				_toName,
				_toEmail,
				_subject,
				_body;
		EmailFormat _format;

	}
}
