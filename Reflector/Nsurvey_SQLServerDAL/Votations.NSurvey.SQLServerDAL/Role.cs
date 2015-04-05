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
    public class Role : IRole
    {
        /// <summary>
        /// Adds a new right to a role
        /// </summary>
        public void AddRightToRole(int roleId, int securityRightId)
        {
            //SqlParameter[] commandParameters = new SqlParameter[] 
            //{ new SqlParameter("@RoleID", roleId), 
            //    new SqlParameter("@SecurityRightId", securityRightId) };

            ArrayList commandParameters = new ArrayList();
            {
                commandParameters.Add(new SqlParameter("@RoleID", roleId).SqlValue);
                commandParameters.Add(new SqlParameter("@SecurityRightId", securityRightId).SqlValue);
            }

            DbConnection.db.ExecuteNonQuery("vts_spRoleSecurityRightAddNew", commandParameters.ToArray());
        }

        /// <summary>
        /// Adds a new role to the database
        /// </summary>
        public void AddRole(RoleData newRole)
        {
            SqlConnection connection = new SqlConnection(DbConnection.NewDbConnectionString);
            SqlCommand insertCommand = new SqlCommand("vts_spRoleAddNew", connection);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Parameters.Add(new SqlParameter("@RoleName", SqlDbType.VarChar, 0xff, "RoleName"));
            insertCommand.Parameters.Add(new SqlParameter("@RoleId", SqlDbType.Int, 4, "RoleId"));
            insertCommand.Parameters["@RoleId"].Direction = ParameterDirection.Output;
            DbConnection.db.UpdateDataSet(newRole, "Roles", insertCommand, new SqlCommand(), new SqlCommand(), UpdateBehavior.Transactional);
        }

        /// <summary>
        /// Add a new role to the user
        /// </summary>
        public void AddRoleToUser(int roleId, int userId)
        {
            //SqlParameter[] commandParameters = new SqlParameter[] 
            //{ new SqlParameter("@RoleID", roleId), 
            //    new SqlParameter("@UserId", userId) };

            ArrayList commandParameters = new ArrayList();
            {
                commandParameters.Add(new SqlParameter("@RoleID", roleId).SqlValue);
                commandParameters.Add(new SqlParameter("@UserId", userId).SqlValue);
            }

            DbConnection.db.ExecuteNonQuery("vts_spUserRoleAssignUser", commandParameters.ToArray());
        }

        /// <summary>
        /// Remove the role 
        /// </summary>
        public void DeleteRoleById(int roleId)
        {
            ArrayList commandParameters = new ArrayList();
            {
                commandParameters.Add(new SqlParameter("@RoleID", roleId).SqlValue);
            }

            DbConnection.db.ExecuteNonQuery("vts_spRoleDelete", commandParameters.ToArray());
        }

        /// <summary>
        /// delete a role's security rights
        /// </summary>
        public void DeleteRoleRights(int roleId)
        {
            ArrayList commandParameters = new ArrayList();
            {
                commandParameters.Add(new SqlParameter("@RoleID", roleId).SqlValue);
            }

            DbConnection.db.ExecuteNonQuery("vts_spRoleSecurityRightDeleteAll", commandParameters.ToArray());
        }

        /// <summary>
        /// Deletes a user's role
        /// </summary>
        public void DeleteUserRole(int roleId, int userId)
        {
            //SqlParameter[] commandParameters = new SqlParameter[] 
            //{ new SqlParameter("@RoleID", roleId), 
            //    new SqlParameter("@UserId", userId) };

            ArrayList commandParameters = new ArrayList();
            {
                commandParameters.Add(new SqlParameter("@RoleID", roleId).SqlValue);
                commandParameters.Add(new SqlParameter("@UserId", userId).SqlValue);
            }

            DbConnection.db.ExecuteNonQuery("vts_spUserRoleUnAssignUser", commandParameters.ToArray());
        }

        /// <summary>
        /// Retrieves all roles from the database
        /// </summary>
        public RoleData GetAllRolesList()
        {
            RoleData dataSet = new RoleData();
            DbConnection.db.LoadDataSet("vts_spRoleGetList", dataSet, new string[] { "Roles" });
            return dataSet;
        }

        /// <summary>
        /// Retrieves role details from the database
        /// </summary>
        public RoleData GetRoleById(int roleId)
        {
            RoleData dataSet = new RoleData();

            ArrayList commandParameters = new ArrayList();
            {
                commandParameters.Add(new SqlParameter("@RoleID", roleId).SqlValue);
            }

            DbConnection.db.LoadDataSet("vts_spRoleGetDetails", dataSet, new string[] { "Roles", "SecurityRights" }, commandParameters.ToArray());
            return dataSet;
        }

        /// <summary>
        /// Retrieves all roles from the database assigned to a user
        /// </summary>
        public RoleData GetRolesOfUser(int userId)
        {
            RoleData dataSet = new RoleData();

            ArrayList commandParameters = new ArrayList();
            {
                commandParameters.Add(new SqlParameter("@UserId", userId).SqlValue);
            }

            DbConnection.db.LoadDataSet("vts_spUserRoleGetAssignedList", dataSet, new string[] { "Roles" }, commandParameters.ToArray());
            return dataSet;
        }

        /// <summary>
        /// Retrieves role & securityrights (access) details from the database
        /// </summary>
        public SecurityRightData GetSecurityRightList()
        {
            SecurityRightData dataSet = new SecurityRightData();
            
            DbConnection.db.LoadDataSet("vts_spSecurityRightGetList", dataSet, new string[] { "SecurityRights" });
            return dataSet;
        }

        /// <summary>
        /// Retrieves all roles from the database not assigned to a user
        /// </summary>
        public RoleData GetUnassignedRolesToUser(int userId)
        {
            RoleData dataSet = new RoleData();

            ArrayList commandParameters = new ArrayList();
            {
                commandParameters.Add(new SqlParameter("@UserId", userId).SqlValue);
            }

            DbConnection.db.LoadDataSet("vts_spUserRoleGetUnAssignedList", dataSet, new string[] { "Roles" }, commandParameters.ToArray());
            return dataSet;
        }

        /// <summary>
        /// Updates roles data
        /// </summary>
        public void UpdateRole(RoleData updatedRole)
        {
            SqlConnection connection = new SqlConnection(DbConnection.NewDbConnectionString);
            SqlCommand insertCommand = new SqlCommand("vts_spRoleUpdate", connection);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Parameters.Add(new SqlParameter("@RoleId", SqlDbType.Int, 4, "RoleId"));
            insertCommand.Parameters.Add(new SqlParameter("@RoleName", SqlDbType.VarChar, 0xff, "RoleName"));
            DbConnection.db.UpdateDataSet(updatedRole, "Roles", insertCommand, new SqlCommand(), new SqlCommand(), UpdateBehavior.Transactional);
        }
    }
}

