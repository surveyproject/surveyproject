/**************************************************************************************************
	Survey TM Project changes: copyright (c) 2013, Fryslan Webservices TM (http://survey.codeplex.com)	

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


LoginSecurity.cs added for SP 2.1.1

************************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Web.Security;

namespace Votations.NSurvey.WebAdmin.Code
{
    public class LoginSecurity
    {
        public virtual string CreateSaltKey(int size) 
        {
            // Generate a cryptographic random number
            var rng = new RNGCryptoServiceProvider();
            var buff = new byte[size];
            rng.GetBytes(buff);

            // Return a Base64 string representation of the random number
            return Convert.ToBase64String(buff);
        }

        public virtual string CreatePasswordHash(string password, 
            string saltkey, string passwordFormat = "SHA1")
        {
            if (String.IsNullOrEmpty(passwordFormat))
                passwordFormat = "SHA1";
            string saltAndPassword = String.Concat(password, saltkey);
            string hashedPassword =
                FormsAuthentication.HashPasswordForStoringInConfigFile(
                    saltAndPassword, passwordFormat);
            return hashedPassword;
        }
}
}