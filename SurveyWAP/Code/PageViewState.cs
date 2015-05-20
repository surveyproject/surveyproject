//-----------------------------------------------------------------------
// <copyright file="PageBase.cs" company="Fryslan Webservices">
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
    using System.Web.UI;


    /// <summary>
    /// PageViewState class description
    /// </summary>
    public class PageViewState : Page
    {
        /// <summary>
        /// Creates new nsurvey context from the current user name
        /// </summary>
        public PageViewState()
        {
              
        }

        protected override void OnInit(EventArgs e) 
        {
           base.OnInit(e); 
           ViewStateUserKey = Session.SessionID;
        }
    }

}
