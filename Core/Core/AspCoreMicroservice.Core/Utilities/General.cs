using static AspCoreMicroservice.Core.Enums;

namespace AspCoreMicroservice.Core.Utilities
{
    public class General
    {
        public static string GetUserRoleNameByRoleId(int roleId)
        {
            string roleType = "";
            if (roleId == (int)UserRoles.User) { roleType = UserRoles.User.ToString(); }
            else if (roleId == (int)UserRoles.Guide) { roleType = UserRoles.Guide.ToString(); }
            return roleType;

        }
    }
}
