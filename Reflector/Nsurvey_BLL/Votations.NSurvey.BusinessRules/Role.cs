namespace Votations.NSurvey.BusinessRules
{
    using System;
    using Votations.NSurvey.DALFactory;
    using Votations.NSurvey.Data;

    /// <summary>
    /// Contains the business rules that are used for a role
    /// </summary>
    public class Role
    {
        /// <summary>
        /// Adds a new right to a role
        /// </summary>
        public void AddRightToRole(int roleId, int securityRightId)
        {
            RoleFactory.Create().AddRightToRole(roleId, securityRightId);
        }

        /// <summary>
        /// Adds a new role to the database
        /// </summary>
        public void AddRole(RoleData newRole)
        {
            RoleFactory.Create().AddRole(newRole);
        }

        /// <summary>
        /// Add a new role to the user
        /// </summary>
        public void AddRoleToUser(int roleId, int userId)
        {
            RoleFactory.Create().AddRoleToUser(roleId, userId);
        }

        /// <summary>
        /// Remove the role 
        /// </summary>
        public void DeleteRoleById(int roleId)
        {
            RoleFactory.Create().DeleteRoleById(roleId);
        }

        /// <summary>
        /// Adds a new right to a role
        /// </summary>
        public void DeleteRoleRights(int roleId)
        {
            RoleFactory.Create().DeleteRoleRights(roleId);
        }

        /// <summary>
        /// Deletes a user's role
        /// </summary>
        public void DeleteUserRole(int roleId, int userId)
        {
            RoleFactory.Create().DeleteUserRole(roleId, userId);
        }

        /// <summary>
        /// Updates roles data
        /// </summary>
        public void UpdateRole(RoleData updatedRole)
        {
            RoleFactory.Create().UpdateRole(updatedRole);
        }
    }
}

