using System.Security.Claims;
using System.Threading.Tasks;

namespace AspCoreMicroservice.Core.Security.Authorization
{
    public interface IPolicyService
    {
        Task<Policy> EvaluateAsync(ClaimsPrincipal user);

        /// <summary>
        /// Determines whether the user has a permission.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="permission">The permission.</param>
        /// <returns>True if user has permission; otherwise false</returns>
        Task<bool> HasPermissionAsync(ClaimsPrincipal user, Permission permission);

        /// <summary>
        /// Determines whether the user has one of the specified permissions
        /// </summary>
        /// <param name="user">The user</param>
        /// <param name="permissions">The permission</param>
        /// <returns>True if user has permission; otherwise false</returns>
        Task<bool> HasPermissionAsync(ClaimsPrincipal user, params string[] permissions);
    }
}
