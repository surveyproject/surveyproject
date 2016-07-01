using System;
using System.Configuration;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.SqlClient;


using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Data.Configuration;
using Votations.NSurvey.WebAdmin.NSurveyAdmin;
using Votations.NSurvey.Data;

namespace Votations.NSurvey.WebAdmin
{
    public partial class Settings : PageBase
    {
        
        //Configuration configuration;
        //CustomErrorsSection section;

        string connDev = ConfigurationManager.ConnectionStrings["SurveyProjectDevConnectionString"].ConnectionString;
        string connTest = ConfigurationManager.ConnectionStrings["SurveyProjectTestConnectionString"].ConnectionString;
        string connProd = ConfigurationManager.ConnectionStrings["SurveyProjectProdConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            UITabList.SetSurveyInfoTabs((MsterPageTabs)Page.Master, UITabList.SurveyInfoTabs.SurveySettings);
            SetupSecurity();

            LocalizePage();
          

            if (!Page.IsPostBack)
            {
                txtCulture.Text = Culture;

                txtConnectionStringDev.Text = connDev;
                txtConnectionStringTest.Text = connTest;
                txtConnectionStringProd.Text = connProd;

                txtDefaultDbConnection.Text = DefaultDatabase;

                txtSmtpServer.Text = SmtpServerName;
                txtSmtpUser.Text = SmtpServerUsername ;
                txtSmtpPassword.Text = SmtpServerPassword;
                txtSmtpPort.Text = SmtpServerPort;

                txtFileDeleteTime.Text = FileDeleteTime;
                txtSessionFileDeleteTime.Text = SessionFileDeleteTime;
                txtSingleMode.Text = SingleMode;
                txtSqlAnswer.Text = SqlAnswer;

                ShowHideDecryptConnectionButton();
                ShowHideDecryptCultureButton();
                ShowHideDecryptSmtpButton();
            }

        }

        private void SetupSecurity()
        {
            if (NSurveyUser.Identity.IsAdmin == false)
            {
                UINavigator.NavigateToAccessDenied(getSurveyId(), MenuIndex);
            }
        }

        private void LocalizePage()
        {
            lblMainTitle.Text = Resources.ResourceManager.GetString("GeneralSettingsTitle");
            lblCulture.Text = Culture;

            lblConnectionStringDev.Text = connDev;
            lblConnectionStringTest.Text = connTest;
            lblConnectionStringProd.Text = connProd;

            lblDefaultDbConnection.Text = DefaultDatabase;

            lblSmtpMailserver.Text = SmtpServerName;
            lblSmtpServerUsername.Text = SmtpServerUsername;
            lblSmtpServerPassword.Text = SmtpServerPassword;
            lblSmtpServerPort.Text = SmtpServerPort;

            lblFileDeleteTime.Text = FileDeleteTime ;
            lblSessionFileDeleteTime.Text = SessionFileDeleteTime;
            lblSingleMode.Text = SingleMode;
            lblSqlAnswer.Text = SqlAnswer;

        }


        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnCreateSqlDb.Click += new System.EventHandler(this.btnCreateSqlDb_Click);

            this.btnCulture.Click += new System.EventHandler(this.btnCulture_Click);
            this.btnCultureDecript.Click += new System.EventHandler(this.btnCultureDecript_Click);

            this.btnConnectionStrings.Click += new System.EventHandler(this.btnConnectionStrings_Click);
            this.btnDecriptConnections.Click += new System.EventHandler(this.btnDecriptConnections_Click);

            this.btnSmtp.Click += new System.EventHandler(this.btnSmtp_Click);
            this.btnDecryptSmtp.Click += new System.EventHandler(this.btnDecryptSmtp_Click);


            this.btnDefaultDbConnection.Click += new System.EventHandler(this.btnDefaultDbConnection_Click);
            this.btnMiscSettings.Click += new System.EventHandler(this.btnMiscSettings_Click);

            this.Load += new System.EventHandler(this.Page_Load);

        }

        protected void btnCreateSqlDb_Click(object sender, EventArgs e)
        {
            String str;
            SqlConnection myConn = new SqlConnection("Server=localhost;Integrated security=SSPI;database=master");

            str = "CREATE DATABASE MyDatabase ON PRIMARY " +
                "(NAME = MyDatabase_Data, " +
                "FILENAME = 'C:\\MyDatabaseData.mdf', " +
                "SIZE = 2MB, MAXSIZE = 10MB, FILEGROWTH = 10%) " +
                "LOG ON (NAME = MyDatabase_Log, " +
                "FILENAME = 'C:\\MyDatabaseLog.ldf', " +
                "SIZE = 1MB, " +
                "MAXSIZE = 5MB, " +
                "FILEGROWTH = 10%)";

            SqlCommand myCommand = new SqlCommand(str, myConn);
            try
            {
                myConn.Open();
                myCommand.ExecuteNonQuery();
                //MessageBox.Show("DataBase is Created Successfully", "MyProgram", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (System.Exception ex)
            {
                //MessageBox.Show(ex.ToString(), "MyProgram", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                if (myConn.State == ConnectionState.Open)
                {
                    myConn.Close();
                }
            }
	
        }

        protected void btnMiscSettings_Click(object sender, EventArgs e)
        {
            var config = WebConfigurationManager.OpenWebConfiguration("~");
            var nsurveySection = (AppSettingsSection)config.GetSection("nSurveySettings");

            var singleMode = nsurveySection.Settings["FormUserProviderSingleMode"];
            singleMode.Value = txtSingleMode.Text;

            var sqlAllowed = nsurveySection.Settings["SqlBasedAnswerTypesAllowed"];
            sqlAllowed.Value = txtSqlAnswer.Text;

            var uploadFile = nsurveySection.Settings["UploadedFileDeleteTimeOut"];
            uploadFile.Value = txtFileDeleteTime.Text;

            var sessionFile = nsurveySection.Settings["SessionUploadedFileDeleteTimeOut"];
            sessionFile.Value = txtSessionFileDeleteTime.Text;

            config.Save();

            // display a message informing the user that value has changed
            ((PageBase)Page).ShowNormalMessage(MessageLabel, "Updated Misc. Settings");
            MessageLabel.Visible = true;
        }
        


        protected void btnDefaultDbConnection_Click(object sender, EventArgs e)
        {
            var config = WebConfigurationManager.OpenWebConfiguration("~");
            var dbSettings = (DatabaseSettings)config.GetSection("dataConfiguration");

            dbSettings.DefaultDatabase = txtDefaultDbConnection.Text;

            config.Save();

            // display a message informing the user that value has changed
            ((PageBase)Page).ShowNormalMessage(MessageLabel, "Updated Default DB Connection");
            MessageLabel.Visible = true;
        }


        protected void btnSmtp_Click(object sender, EventArgs e)
        {
            var config = WebConfigurationManager.OpenWebConfiguration("~");
            var nsurveySection = (AppSettingsSection)config.GetSection("nSurveySettings");

            var smtpServer = nsurveySection.Settings["NSurveySMTPServer"];
            smtpServer.Value = txtSmtpServer.Text;

            var smtpPort = nsurveySection.Settings["NSurveySMTPServerPort"];
            smtpPort.Value = txtSmtpPort.Text;

            var smtpUser = nsurveySection.Settings["NSurveySMTPServerAuthUserName"];
            smtpUser.Value = txtSmtpUser.Text;

            var smtpPasswrd = nsurveySection.Settings["NSurveySMTPServerAuthPassword"];
            smtpPasswrd.Value = txtSmtpPassword.Text;

            //encrypt the nsurveysection
            nsurveySection.SectionInformation.ProtectSection("RsaProtectedConfigurationProvider");
            nsurveySection.SectionInformation.ForceSave = true;

            config.Save();

            //show decrypt button to unencrypt the connectionstrings in web.config
            btnDecryptSmtp.Visible = true;

            // display a message informing the user that value has changed
            ((PageBase)Page).ShowNormalMessage(MessageLabel, "Updated SMTP");
            MessageLabel.Visible = true;
        }


        // show/ hide Decrypt Misc Settings button depending on state of encryption of web.config file

        private void ShowHideDecryptSmtpButton()
        {
            var config = WebConfigurationManager.OpenWebConfiguration("~");
            var nsurveySection = (AppSettingsSection)config.GetSection("nSurveySettings");

            if (!nsurveySection.SectionInformation.IsProtected)
                btnDecryptSmtp.Visible = false;
            else
                btnDecryptSmtp.Visible = true;
        }


        protected void btnDecryptSmtp_Click(object sender, EventArgs e)
        {
            var config = WebConfigurationManager.OpenWebConfiguration("~");
            var nsurveySection = (AppSettingsSection)config.GetSection("nSurveySettings");

            // Unprotect (decrypt)the section.
            nsurveySection.SectionInformation.UnprotectSection();

            // Force the section information to be written to 
            // the configuration file.
            nsurveySection.SectionInformation.ForceDeclaration(true);

            // Save the decrypted section.
            nsurveySection.SectionInformation.ForceSave = true;

            config.Save(ConfigurationSaveMode.Full);

            //show decrypt button to unencrypt the connectionstrings in web.config
            btnDecryptSmtp.Visible = false;

            // display a message informing the user that value has changed
            ((PageBase)Page).ShowNormalMessage(MessageLabel, "Decrypted SMTP");
            MessageLabel.Visible = true;
        }


        protected void btnConnectionStrings_Click(object sender, EventArgs e)
        {

            var config = WebConfigurationManager.OpenWebConfiguration("~");
            var connectionStringsSection = (ConnectionStringsSection)config.GetSection("connectionStrings");

            connectionStringsSection.ConnectionStrings["SurveyProjectDevConnectionString"].ConnectionString = txtConnectionStringDev.Text;
            connectionStringsSection.ConnectionStrings["SurveyProjectTestConnectionString"].ConnectionString = txtConnectionStringTest.Text;
            connectionStringsSection.ConnectionStrings["SurveyProjectProdConnectionString"].ConnectionString = txtConnectionStringProd.Text;

            connectionStringsSection.SectionInformation.ProtectSection("RsaProtectedConfigurationProvider");
            connectionStringsSection.SectionInformation.ForceSave = true;

            config.Save();

            //show decrypt button to unencrypt the connectionstrings in web.config
            btnDecriptConnections.Visible = true;

            // display a message informing the user that value has changed
            ((PageBase)Page).ShowNormalMessage(MessageLabel, "Updated Connection Test Strings");
            MessageLabel.Visible = true;
        }


        // show/ hide Decrypt Connection button depending on state of encryption of web.config file

        private void ShowHideDecryptConnectionButton()
        {
            Configuration config = WebConfigurationManager.OpenWebConfiguration(Request.ApplicationPath);
            ConfigurationSection connectionStringsSection = config.GetSection("connectionStrings");

            if (!connectionStringsSection.SectionInformation.IsProtected)
                btnDecriptConnections.Visible = false;
            else
                btnDecriptConnections.Visible = true;
        }


        protected void btnDecriptConnections_Click(object sender, EventArgs e)
        {
            Configuration config = WebConfigurationManager.OpenWebConfiguration(Request.ApplicationPath);
            ConfigurationSection connectionStringsSection = config.GetSection("connectionStrings");

                // Unprotect (decrypt)the section.
                connectionStringsSection.SectionInformation.UnprotectSection();

                // Force the section information to be written to 
                // the configuration file.
                connectionStringsSection.SectionInformation.ForceDeclaration(true);

                // Save the decrypted section.
                connectionStringsSection.SectionInformation.ForceSave = true;

                config.Save(ConfigurationSaveMode.Full);

            //hide decrypt button after unencrypting the connections
            btnDecriptConnections.Visible = false;

            // display a message informing the user that value has changed
            ((PageBase)Page).ShowNormalMessage(MessageLabel, "Decripted ConnStrings");
                MessageLabel.Visible = true;
            
        }




        protected void btnCulture_Click(object sender, EventArgs e)
        {

            Configuration configuration = WebConfigurationManager.OpenWebConfiguration("~");
            var section = (GlobalizationSection)configuration.GetSection("system.web/globalization");

            if (section != null)
            {
                section.UICulture = txtCulture.Text;

                section.SectionInformation.ProtectSection("RsaProtectedConfigurationProvider");
                section.SectionInformation.ForceSave = true;

                configuration.Save();

                //show decrypt button to unencrypt the Culture in web.config
                btnCultureDecript.Visible = true;

                // display a message informing the user that value has changed
                ((PageBase)Page).ShowNormalMessage(MessageLabel, "Updated:" + section.UICulture.ToString());
                MessageLabel.Visible = true;
            }
        }

        // show/ hide Decrypt Culture button depending on state of encryption of web.config file

        private void ShowHideDecryptCultureButton()
        {
            Configuration configuration = WebConfigurationManager.OpenWebConfiguration("~");
            var section = (GlobalizationSection)configuration.GetSection("system.web/globalization");

            if (!section.SectionInformation.IsProtected)
                btnCultureDecript.Visible = false;
            else
                btnCultureDecript.Visible = true;
        }



        protected void btnCultureDecript_Click(object sender, EventArgs e)
        {

            Configuration configuration = WebConfigurationManager.OpenWebConfiguration("~");
            var section = (GlobalizationSection)configuration.GetSection("system.web/globalization");

            if (section != null)
            {

                // Unprotect (decrypt)the section.
                section.SectionInformation.UnprotectSection();

                // Force the section information to be written to 
                // the configuration file.
                section.SectionInformation.ForceDeclaration(true);

                // Save the decrypted section.
                section.SectionInformation.ForceSave = true;

                configuration.Save(ConfigurationSaveMode.Full);

                //hide decrypt button to unencrypt the Culture in web.config
                btnCultureDecript.Visible = false;

                // display a message informing the user that value has changed
                ((PageBase)Page).ShowNormalMessage(MessageLabel, "Updated:" + section.UICulture.ToString());
                MessageLabel.Visible = true;
            }
        }

        

        public static string DefaultDatabase
        {
            get
            {
                string str = null;

                Configuration configuration;
                configuration = WebConfigurationManager.OpenWebConfiguration("~");

                DatabaseSettings dbSettings = (DatabaseSettings)ConfigurationManager.GetSection("dataConfiguration");

                str = dbSettings.DefaultDatabase;

                if (str == null)
                {
                    return "empty";
                }
                return str;
            }
        }

        public static string SmtpServerName
        {
            get
            {
                string str = null;

                NameValueCollection config = (NameValueCollection)ConfigurationManager.GetSection("nSurveySettings");
                if (config == null)
                {
                    config = ConfigurationManager.AppSettings;
                }

                str = config["NSurveySMTPServer"];

                if (str == null)
                {
                    return "empty";
                }
                return str;
            }
        }

        public static string SmtpServerUsername
        {
            get
            {
                string str = null;

                NameValueCollection config = (NameValueCollection)ConfigurationManager.GetSection("nSurveySettings");
                if (config == null)
                {
                    config = ConfigurationManager.AppSettings;
                }

                str = config["NSurveySMTPServerAuthUserName"];

                if (str == null)
                {
                    return "empty";
                }
                return str;
            }
        }

        public static string SmtpServerPassword
        {
            get
            {
                string str = null;

                NameValueCollection config = (NameValueCollection)ConfigurationManager.GetSection("nSurveySettings");
                if (config == null)
                {
                    config = ConfigurationManager.AppSettings;
                }

                str = config["NSurveySMTPServerAuthPassword"];

                if (str == null)
                {
                    return "empty";
                }
                return str;
            }
        }

        public static string SmtpServerPort
        {
            get
            {
                string str = null;

                NameValueCollection config = (NameValueCollection)ConfigurationManager.GetSection("nSurveySettings");
                if (config == null)
                {
                    config = ConfigurationManager.AppSettings;
                }

                str = config["NSurveySMTPServerPort"];

                if (str == null)
                {
                    return "empty";
                }
                return str;
            }
        }


            public static string  FileDeleteTime
            {
                get
                {
                    string str = null;

                    NameValueCollection config = (NameValueCollection)ConfigurationManager.GetSection("nSurveySettings");
                    if (config == null)
                    {
                        config = ConfigurationManager.AppSettings;
                    }

                    str = config["UploadedFileDeleteTimeOut"];

                    if (str == null)
                    {
                        return "empty";
                    }
                    return str;
                }
            }

            public static string  SessionFileDeleteTime
            {
                get
                {
                    string str = null;

                    NameValueCollection config = (NameValueCollection)ConfigurationManager.GetSection("nSurveySettings");
                    if (config == null)
                    {
                        config = ConfigurationManager.AppSettings;
                    }

                    str = config["SessionUploadedFileDeleteTimeOut"];

                    if (str == null)
                    {
                        return "empty";
                    }
                    return str;
                }
            }


            public static string SingleMode
            {
                get
                {
                    string str = null;

                    NameValueCollection config = (NameValueCollection)ConfigurationManager.GetSection("nSurveySettings");
                    if (config == null)
                    {
                        config = ConfigurationManager.AppSettings;
                    }

                    str = config["FormUserProviderSingleMode"];

                    if (str == null)
                    {
                        return "empty";
                    }
                    return str;
                }
            }

            public static string SqlAnswer
            {
                get
                {
                    string str = null;

                    NameValueCollection config = (NameValueCollection)ConfigurationManager.GetSection("nSurveySettings");
                    if (config == null)
                    {
                        config = ConfigurationManager.AppSettings;
                    }

                    str = config["SqlBasedAnswerTypesAllowed"];

                    if (str == null)
                    {
                        return "empty";
                    }
                    return str;
                }
            }

            public static string Culture
            {
                get
                {
                    string str = null;

                    Configuration configuration = WebConfigurationManager.OpenWebConfiguration("~");
                    var config = (GlobalizationSection)configuration.GetSection("system.web/globalization");

                    str = Convert.ToString(config.UICulture);

                    if (str == null)
                    {
                        return "empty";
                    }
                    return str;
                }
            }



    }
}