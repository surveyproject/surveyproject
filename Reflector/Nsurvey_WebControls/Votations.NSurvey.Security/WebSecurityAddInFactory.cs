namespace Votations.NSurvey.Security
{
    using System;
    using System.Reflection;
    using System.Web.UI;
    using Votations.NSurvey.Data;
    using Votations.NSurvey.Resources;

    /// <summary>
    /// Creates a new answer item instance from the db answer data
    /// </summary>
    public class WebSecurityAddInFactory
    {
        private WebSecurityAddInFactory()
        {
        }

        public static IWebSecurityAddIn Create(WebSecurityAddInData.WebSecurityAddInsRow addIn, StateBag viewStateContext, string languageCode)
        {
            IWebSecurityAddIn @in = null;
            try
            {
                if (addIn.TypeAssembly == null)
                {
                    throw new InvalidCastException();
                }
                @in = (IWebSecurityAddIn) Assembly.Load(addIn.TypeAssembly).CreateInstance(addIn.TypeNameSpace);
                @in.AddInDbId = addIn.WebSecurityAddInId;
                @in.Disabled = addIn.Disabled;
                string str = ResourceManager.GetString(addIn.Description);
                @in.Description = (str == null) ? addIn.Description : str;
                @in.SurveyId = addIn.SurveyID;
                @in.ViewState = viewStateContext;
                @in.Order = addIn.AddInOrder;
                @in.LanguageCode = languageCode;
///             return @in;
            }
            catch (NullReferenceException)
            {
                throw new InvalidCastException("specfied type " + addIn.TypeNameSpace + " could not be found in the specifed assembly " + addIn.TypeAssembly);
            }
            catch (InvalidCastException)
            {
                throw new InvalidCastException("specfied type " + addIn.TypeNameSpace + " must implement the IWebSecurityAddIn interface");
            }
            return @in;
        }

        public static WebSecurityAddInCollection CreateWebSecurityAddInCollection(WebSecurityAddInData addIns, StateBag viewStateContext, string languageCode)
        {
            WebSecurityAddInCollection ins = new WebSecurityAddInCollection();
            foreach (WebSecurityAddInData.WebSecurityAddInsRow row in addIns.WebSecurityAddIns)
            {
                ins.Add(Create(row, viewStateContext, languageCode));
            }
            return ins;
        }
    }
}

