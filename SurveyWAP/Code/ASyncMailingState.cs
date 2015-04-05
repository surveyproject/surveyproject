//-----------------------------------------------------------------------
// <copyright file="ASyncMailingState.cs" company="Fryslan Webservices">
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

  /// <summary>
  /// ASyncMailingState class
  /// </summary>
  /// <remarks>Keep track of the state to syncronize with the callback when calling the End method</remarks>

  public class ASyncMailingState
  {
    
    /// <summary>
    /// SessionId
    /// </summary>
    /// <remarks>The current session ID</remarks>
    public string SessionId
    {
      get { return _sessionId;}
      set { _sessionId = value; }
    }

    /// <summary>
    /// AsyncMailing
    /// </summary>
    /// <remarks>Original object that handle the begin call</remarks>
    public ASyncMailing AsyncMailing
    {
      get { return _asyncMailing; }
      set { _asyncMailing = value; }
    }

    /// <summary>
    /// AsynchMailing
    /// </summary>
    /// <param name="asyncMailing"></param>
    /// <param name="sessionId"></param>
    public ASyncMailingState(ASyncMailing asyncMailing, string sessionId)
    {
      _asyncMailing = asyncMailing;
      _sessionId = sessionId;
    }

    /// <summary>
    /// _sessionId
    /// </summary>
    /// <remarks></remarks>
    private string _sessionId;
    
    /// <summary>
    /// _asyncMailing
    /// </summary>
    /// <remarks></remarks>
    private ASyncMailing _asyncMailing;

  }


}
