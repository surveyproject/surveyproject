namespace Votations.NSurvey.BusinessRules
{
    using System;
    using System.Web.Security;
    using System.Security.Cryptography;
    using System.Text;
    using System.Linq;
    using Votations.NSurvey.DALFactory;
    using Votations.NSurvey.Data;

    /// <summary>
    /// Contains the business rules that are used for a user
    /// </summary>
    public class User
    {
        public void AddUser(NSurveyUserData newUser)
        {
            UserFactory.Create().AddUser(newUser);
        }

        /// <summary>
        /// Add users settings
        /// </summary>
        public void AddUserSettings(UserSettingData newUserSettings)
        {
            UserFactory.Create().AddUserSettings(newUserSettings);
        }

        /// <summary>
        /// Deletes the user from the db
        /// </summary>
        public void DeleteUserById(int userId)
        {
            UserFactory.Create().DeleteUserById(userId);
        }


        /// <summary>
        /// Generate Password Salt Int to add to password
        /// </summary>
        /// <returns>Salt Int</returns>

        internal string GenerateSalt()
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buff = new byte[32];
            rng.GetBytes(buff);
            return Convert.ToBase64String(buff);
        }

        /// <summary>
        /// Encrypts the user's password
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public string EncryptUserPassword(string password)
        {
            //return FormsAuthentication.HashPasswordForStoringInConfigFile(password, "md5");

            string salt = GenerateSalt();

            //start hash creation:
            System.Security.Cryptography.SHA256 sha256 = new System.Security.Cryptography.SHA256Managed();

            // password string to bytes
            byte[] sha256Bytes = System.Text.Encoding.UTF8.GetBytes(password);

            //password bytes to hash
            byte[] cryString = sha256.ComputeHash(sha256Bytes);

            // start final encrypted password
            string sha256Str = string.Empty;

            // create final encrypted password: bytes to hex string
            for (int i = 0; i < cryString.Length; i++)
            {
                sha256Str += cryString[i].ToString("X");
            }

            // concatenate hashed password + salt
            // sha256Str = sha256Str + salt;

            return sha256Str;
        }

        /// <summary>
        /// check user password
        /// </summary>
        /// <param name="password">submitted password</param>
        /// <returns>hashed password</returns>
        public string CheckUserPassword(string password)
        {
            //return FormsAuthentication.HashPasswordForStoringInConfigFile(password, "md5");

            //string salt = GenerateSalt();

            //start hash creation:
            System.Security.Cryptography.SHA256 sha256 = new System.Security.Cryptography.SHA256Managed();

            // password string to bytes
            byte[] sha256Bytes = System.Text.Encoding.UTF8.GetBytes(password);

            //password bytes to hash
            byte[] cryString = sha256.ComputeHash(sha256Bytes);

            // start final encrypted password
            string sha256Str = string.Empty;

            // create final encrypted password: bytes to hex string
            for (int i = 0; i < cryString.Length; i++)
            {
                sha256Str += cryString[i].ToString("X");
            }

            // concatenate hashed password + salt
            // sha256Str = sha256Str + salt;

            return sha256Str;
        }










        /// <summary>
        /// UPdate user
        /// </summary>
        /// <param name="updatedUser">updateuser</param>

                
        public void UpdateUser(NSurveyUserData updatedUser)
        {
            UserFactory.Create().UpdateUser(updatedUser);
        }

        /// <summary>
        /// Updates users settings
        /// </summary>
        public void UpdateUserSettings(UserSettingData updatedUserSettings)
        {
            UserFactory.Create().UpdateUserSettings(updatedUserSettings);
        }
    }
}

