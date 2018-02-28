using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Votations.NSurvey.UserProvider;

namespace Votations.NSurvey.WebAdmin
{
    public partial class ADLogon : System.Web.UI.Page
    {
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent()
        {
            this.btnLogin.Click += new System.EventHandler(this.Login_Click);
            this.Load += new System.EventHandler(this.Page_Load);

        }

        private void Page_Load(object sender, System.EventArgs e)
        {

        }

        void Login_Click(Object sender, EventArgs e)
        {
            //Fully-qualified Domain Name + distinguished name
            //String adPath = "LDAP://www.openldap.com/dc=OpenLDAP,dc=org"; 
            //String adPath = "LDAP://ldap.forumsys.com:389/cn=read-only-admin,dc=example,dc=com"; 

            String adPath = "LDAP://db.debian.org:389/dc=debian,dc=org"; 

            ADUserProvider adAuth = new ADUserProvider(adPath);
            try
            {
                if (true == adAuth.IsAuthenticated(txtDomain.Text, txtUsername.Text, txtPassword.Text))
                {
                    String groups = adAuth.GetGroups();

                    //Create the ticket, and add the groups.
                    bool isCookiePersistent = chkPersist.Checked;
                    FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(1, txtUsername.Text,
              DateTime.Now, DateTime.Now.AddMinutes(60), isCookiePersistent, groups);

                    //Encrypt the ticket.
                    String encryptedTicket = FormsAuthentication.Encrypt(authTicket);

                    //Create a cookie, and then add the encrypted ticket to the cookie as data.
                    HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);

                    if (true == isCookiePersistent)
                        authCookie.Expires = authTicket.Expiration;

                    //Add the cookie to the outgoing cookies collection.
                    Response.Cookies.Add(authCookie);

                    //You can redirect now.
                    Response.Redirect(FormsAuthentication.GetRedirectUrl(txtUsername.Text, false));
                }
                else
                {
                    errorLabel.Text = "Authentication did not succeed. Check user name and password.";
                }
            }
            catch (Exception ex)
            {
                errorLabel.Text = "Error authenticating. " + ex.Message + "<br /><br />Inner:" + ex.InnerException + "<br /><br />Stack:" + ex.StackTrace;
            }
        }



    }
}