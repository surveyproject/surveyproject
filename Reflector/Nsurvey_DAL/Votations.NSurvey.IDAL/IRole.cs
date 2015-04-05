namespace Votations.NSurvey.IDAL
{
    using System;
    using Votations.NSurvey.Data;

    /// <summary>
    /// interface for the role DAL.
    /// </summary>
    public interface IRole
    {
        void AddRightToRole(int roleId, int securityRightId);
        void AddRole(RoleData newRole);
        void AddRoleToUser(int roleId, int userId);
        void DeleteRoleById(int roleId);
        void DeleteRoleRights(int roleId);
        void DeleteUserRole(int roleId, int userId);
        RoleData GetAllRolesList();
        RoleData GetRoleById(int roleId);
        RoleData GetRolesOfUser(int userId);
        SecurityRightData GetSecurityRightList();
        RoleData GetUnassignedRolesToUser(int userId);
        void UpdateRole(RoleData updatedRole);
    }
}

