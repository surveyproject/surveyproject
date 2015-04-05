namespace Votations.NSurvey.Web
{
    using System;
    using System.Web;
    using Votations.NSurvey.Web.Security;
    using Votations.NSurvey.UserProvider;

    /// <summary>
    /// Provides a global context of NSurvey settings 
    /// during an HTTP request
    /// </summary>
    public class NSurveyContext
    {
        private INSurveyPrincipal _user = UserFactory.Create().CreatePrincipal(null);

        public static NSurveyContext Current
        {
            get
            {
                if (HttpContext.Current == null)
                {
                    return new NSurveyContext();
                }
                if (HttpContext.Current.Items["NSurveyContext"] == null)
                {
                    HttpContext.Current.Items.Add("NSurveyContext", new NSurveyContext());
                }
                return (NSurveyContext) HttpContext.Current.Items["NSurveyContext"];
            }
        }

        /// <summary>
        /// Currently logged user with its
        /// nsurvey
        /// </summary>
        public INSurveyPrincipal User
        {
            get
            {
                return this._user;
            }
            set
            {
                this._user = value;
            }
        }
    }
}

