using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AspCoreMicroservice.Core.Security.Authorization
{
    public abstract class BasePolicyService : IPolicyService
    {
        public abstract Task<Policy> EvaluateAsync(ClaimsPrincipal user);

        public async Task<bool> HasPermissionAsync(ClaimsPrincipal user, Permission permission)
        {
            var policy = await EvaluateAsync(user);
            if (policy == null)
                return false;

            return policy.Permissions.Any(p => p.Action == permission.Action && p.Resource == permission.Resource && p.Scope == permission.Scope);
        }

        public async Task<bool> HasPermissionAsync(ClaimsPrincipal user, params string[] permissions)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user), "Claims principal is missing.");

            if (permissions == null)
                return false;

            foreach (var permission in permissions)
            {
                if (await HasPermissionAsync(user, Permission.FromString(permission)))
                    return true;
            }

            return false;
        }    
    }
}
