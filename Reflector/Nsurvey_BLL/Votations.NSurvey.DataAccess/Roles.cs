namespace Votations.NSurvey.DataAccess
{
    using System;
    using Votations.NSurvey.DALFactory;
    using Votations.NSurvey.Data;

    /// <summary>
    /// Provides the method to access the role's data.
    /// </summary>
    public class Roles
    {
        /// <summary>
        /// Retrieves all role from the database
        /// </summary>
        public RoleData GetAllRolesList()
        {
            return RoleFactory.Create().GetAllRolesList();
        }

        /// <summary>
        /// Retrieves role details from the database
        /// </summary>
        public RoleData GetRoleById(int roleId)
        {
            return RoleFactory.Create().GetRoleById(roleId);
        }

        /// <summary>
        /// Retrieves all role from the database assigned to a user
        /// </summary>
        public RoleData GetRolesOfUser(int userId)
        {
            return RoleFactory.Create().GetRolesOfUser(userId);
        }

        /// <summary>
        /// retrieve the list of available security rights
        /// </summary>
        public SecurityRightData GetSecurityRightList()
        {
            return RoleFactory.Create().GetSecurityRightList();
        }

        /// <summary>
        /// Retrieves all role from the database not assigned to a user
        /// </summary>
        public RoleData GetUnassignedRolesToUser(int userId)
        {
            return RoleFactory.Create().GetUnassignedRolesToUser(userId);
        }
    }
}

