namespace Votations.NSurvey.SQLServerDAL
{
    using Microsoft.Practices.EnterpriseLibrary.Common;
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using Microsoft.Practices.EnterpriseLibrary.Data.Sql;

    //using Microsoft.ApplicationBlocks.Data;

    using System;
    using System.Collections;
    using System.Collections.Generic;

    using System.Data;
    using System.Data.SqlClient;
    using Votations.NSurvey.Data;
    using Votations.NSurvey.IDAL;

    /// <summary>
    /// SQL Server DAL implementation.
    /// </summary>
    public class User : IUser
    {
        /// <summary>
        /// Adds a new users to the database
        /// </summary>
        public void AddUser(NSurveyUserData newUser)
        {
            SqlConnection connection = new SqlConnection(DbConnection.NewDbConnectionString);
            SqlCommand insertCommand = new SqlCommand("vts_spUserAddNew", connection);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Parameters.Add(new SqlParameter("@UserName", SqlDbType.VarChar, 0xff, "UserName"));
            insertCommand.Parameters.Add(new SqlParameter("@Password", SqlDbType.VarChar, 0xff, "Password"));
            insertCommand.Parameters.Add(new SqlParameter("@PasswordSalt", SqlDbType.VarChar, 0xff, "PasswordSalt"));
            insertCommand.Parameters.Add(new SqlParameter("@LastName", SqlDbType.VarChar, 0xff, "LastName"));
            insertCommand.Parameters.Add(new SqlParameter("@FirstName", SqlDbType.VarChar, 0xff, "FirstName"));
            insertCommand.Parameters.Add(new SqlParameter("@Email", SqlDbType.VarChar, 0xff, "Email"));
            insertCommand.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int, 4, "UserId"));
            insertCommand.Parameters["@UserId"].Direction = ParameterDirection.Output;
            DbConnection.db.UpdateDataSet(newUser, "Users", insertCommand, new SqlCommand(), new SqlCommand(), UpdateBehavior.Transactional);
        }

        /// <summary>
        /// Adds a new users settings to the database
        /// </summary>
        public void AddUserSettings(UserSettingData newUserSettings)
        {
            SqlConnection connection = new SqlConnection(DbConnection.NewDbConnectionString);
            SqlCommand insertCommand = new SqlCommand("vts_spUserSettingAddNew", connection);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int, 4, "UserId"));
            insertCommand.Parameters.Add(new SqlParameter("@IsAdmin", SqlDbType.Bit, 1, "IsAdmin"));
            insertCommand.Parameters.Add(new SqlParameter("@GlobalSurveyAccess", SqlDbType.Bit, 1, "GlobalSurveyAccess"));

            DbConnection.db.UpdateDataSet(newUserSettings, "UserSettings", insertCommand, new SqlCommand(), new SqlCommand(), UpdateBehavior.Transactional);
        }

        /// <summary>
        /// Remove the user 
        /// </summary>
        public void DeleteUserById(int userId)
        {
            ArrayList commandParameters = new ArrayList();
            {
                commandParameters.Add(new SqlParameter("@UserID", userId).SqlValue);
            }

            DbConnection.db.ExecuteNonQuery("vts_spUserDelete", commandParameters.ToArray());
        }

        /// <summary>
        /// Retrieves the number of admins in the database
        /// </summary>
        public int GetAdminCount()
        {
            object obj2 = DbConnection.db.ExecuteScalar("vts_spUserGetAdminCount");
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                return int.Parse(obj2.ToString());
            }
            return 0;
        }

        /// <summary>
        /// Retrieves all users from the database
        /// </summary>
        public UserData GetAllUsersList()
        {
            UserData dataSet = new UserData();
            DbConnection.db.LoadDataSet("vts_spUserGetList", dataSet, new string[] { "Users" });
            return dataSet;
        }

         /// <summary>
        /// Retrieves all users from the database by filter
        /// </summary>
        public UserData GetAllUsersListByFilter(string UserName = null, string FirstName = null, string LastName = null, string Email = null, int ?Admin = null)
        {
            UserData dataSet = new UserData();

            //SqlParameter[] commandParameters = new SqlParameter[] { 
            //    new SqlParameter("@UserName", UserName), 
            //    new SqlParameter("@FirstName", FirstName),
            //    new SqlParameter("@LastName", LastName),
            //    new SqlParameter("@Email", Email),
            //    new SqlParameter("@Administrator", Admin),
            //};

            ArrayList commandParameters = new ArrayList();
            {
                commandParameters.Add(new SqlParameter("@UserName", UserName).SqlValue);
                commandParameters.Add(new SqlParameter("@FirstName", FirstName).SqlValue);
                commandParameters.Add(new SqlParameter("@LastName", LastName).SqlValue);
                commandParameters.Add(new SqlParameter("@Email", Email).SqlValue);
                commandParameters.Add(new SqlParameter("@Administrator", Admin).SqlValue);
            }

            DbConnection.db.LoadDataSet("vts_spUserGetListByFilter", dataSet, new string[] { "Users" }, commandParameters.ToArray());
            return dataSet;
        }

       /// <summary>
        /// Retrieves the user if any available
        /// </summary>
        public NSurveyUserData GetNSurveyUserData(string userName, string password)
        {
            //SqlParameter[] commandParameters = new SqlParameter[] 
            //{ new SqlParameter("@UserName", userName), 
            //    new SqlParameter("@Password", password) };

            ArrayList commandParameters = new ArrayList();
            {
                commandParameters.Add(new SqlParameter("@UserName", userName).SqlValue);
                commandParameters.Add(new SqlParameter("@Password", password).SqlValue);
            }
            
            NSurveyUserData dataSet = new NSurveyUserData();
            DbConnection.db.LoadDataSet("vts_spUserGetData", dataSet, new string[] { "Users", "UserSecurityRights" }, commandParameters.ToArray());
            return dataSet;
        }

        /// <summary>
        /// Retrieves users details from the database
        /// </summary>
        public NSurveyUserData GetUserById(int userId)
        {
            NSurveyUserData dataSet = new NSurveyUserData();

            ArrayList commandParameters = new ArrayList();
            {
                commandParameters.Add(new SqlParameter("@UserId", userId).SqlValue);
            }

            DbConnection.db.LoadDataSet("vts_spUserGetDetails", dataSet, new string[] { "Users" }, commandParameters.ToArray());
            return dataSet;
        }

        /// <summary>
        /// Retrieves users id from its username
        /// </summary>
        public int GetUserByIdFromUserName(string userName)
        {

            ArrayList commandParameters = new ArrayList();
            {
                commandParameters.Add(new SqlParameter("@UserName", userName).SqlValue);
            }

            object obj2 = DbConnection.db.ExecuteScalar("vts_spUserGetUserIdFromUserName", commandParameters.ToArray());
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                return int.Parse(obj2.ToString());
            }
            return -1;
        }

        /// <summary>
        /// Retrieves an array of the security rights of the user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public int[] GetUserSecurityRights(int userId)
        {
            ArrayList commandParameters = new ArrayList();
            {
                commandParameters.Add(new SqlParameter("@UserId", userId).SqlValue);
            }

            DataSet set = DbConnection.db.ExecuteDataSet("vts_spUserSecurityRightGet", commandParameters.ToArray());
            int[] numArray = new int[set.Tables[0].Rows.Count];
            for (int i = 0; i < set.Tables[0].Rows.Count; i++)
            {
                numArray[i] = int.Parse(set.Tables[0].Rows[i].ItemArray[0].ToString());
            }
            return numArray;
        }

        /// <summary>
        /// Get user settings 
        /// </summary>
        public UserSettingData GetUserSettings(int userId)
        {
            UserSettingData dataSet = new UserSettingData();

            ArrayList commandParameters = new ArrayList();
            {
                commandParameters.Add(new SqlParameter("@UserId", userId).SqlValue);
            }

            DbConnection.db.LoadDataSet("vts_spUserSettingGet", dataSet, new string[] { "UserSettings" }, commandParameters.ToArray());
            return dataSet;
        }

        /// <summary>
        /// Checks if the given user is an administrator
        /// </summary>
        public bool IsAdministrator(int userId)
        {
            ArrayList commandParameters = new ArrayList();
            {
                commandParameters.Add(new SqlParameter("@UserId", userId).SqlValue);
            }

            object obj2 = DbConnection.db.ExecuteScalar("vts_spUserIsAdmin", commandParameters.ToArray());
            return ((obj2 != null) && (obj2 != DBNull.Value));
        }

        /// <summary>
        /// Updates users data
        /// </summary>
        public void UpdateUser(NSurveyUserData updatedUser)
        {
            SqlConnection connection = new SqlConnection(DbConnection.NewDbConnectionString);
            SqlCommand insertCommand = new SqlCommand("vts_spUserUpdate", connection);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int, 4, "UserId"));
            insertCommand.Parameters.Add(new SqlParameter("@UserName", SqlDbType.VarChar, 0xff, "UserName"));
            insertCommand.Parameters.Add(new SqlParameter("@Password", SqlDbType.VarChar, 0xff, "Password"));
            insertCommand.Parameters.Add(new SqlParameter("@PasswordSalt", SqlDbType.VarChar, 0xff, "PasswordSalt"));
            insertCommand.Parameters.Add(new SqlParameter("@LastName", SqlDbType.VarChar, 0xff, "LastName"));
            insertCommand.Parameters.Add(new SqlParameter("@FirstName", SqlDbType.VarChar, 0xff, "FirstName"));
            insertCommand.Parameters.Add(new SqlParameter("@Email", SqlDbType.VarChar, 0xff, "Email"));
            DbConnection.db.UpdateDataSet(updatedUser, "Users", insertCommand, new SqlCommand(), insertCommand, UpdateBehavior.Transactional);
        }

        /// <summary>
        /// Updates users settings
        /// </summary>
        public void UpdateUserSettings(UserSettingData updatedUserSettings)
        {
            SqlConnection connection = new SqlConnection(DbConnection.NewDbConnectionString);
            SqlCommand updateCommand = new SqlCommand("vts_spUserSettingUpdate", connection);
            updateCommand.CommandType = CommandType.StoredProcedure;
            updateCommand.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int, 4, "UserId"));
            updateCommand.Parameters.Add(new SqlParameter("@IsAdmin", SqlDbType.Bit, 1, "IsAdmin"));
            updateCommand.Parameters.Add(new SqlParameter("@GlobalSurveyAccess", SqlDbType.Bit, 1, "GlobalSurveyAccess"));

            DbConnection.db.UpdateDataSet(updatedUserSettings, "UserSettings", updateCommand, new SqlCommand(), updateCommand, UpdateBehavior.Transactional);
        }
    }
}

