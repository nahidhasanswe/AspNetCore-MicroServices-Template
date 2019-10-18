using System;
using System.Text.Json.Serialization;

namespace AspCoreMicroservice.Core.Security.Authorization
{
    public class Permission : IEquatable<Permission>
    {
        private string resource;
        private string action;

        public Permission()
        {
        }

        public Permission(string resource, string action, string scope = null)
        {
            Resource = resource;
            Action = action;
            Scope = scope;
        }

        public Permission(string permission)
        {
            var value = FromString(permission);
            Resource = value.Resource;
            Action = value.Action;
            Scope = value.Scope;
        }

        /// <summary>
        /// Resource permission applies to
        /// </summary>
        [JsonPropertyName("resource")]
        public string Resource
        {
            get { return resource; }
            set
            {
                Check.NotNullOrEmpty(value, nameof(value));
                resource = value;
            }
        }
        /// <summary>
        /// Action permission allows, e.g. Create, Update, Delete, etc.
        /// </summary>
        [JsonPropertyName("action")]
        public string Action
        {
            get { return action; }
            set
            {
                Check.NotNullOrEmpty(value, nameof(value));
                action = value;
            }
        }
        /// <summary>
        /// Scope of permission, i.e. any user or user only
        /// </summary>
        [JsonPropertyName("scope")]
        public string Scope { get; set; }

        public bool Equals(Permission other)
        {
            if (other == null)
                return false;

            return string.Equals(Resource, other.Resource) &&
                string.Equals(Action, other.Action) &&
                string.Equals(Scope, other.Scope);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;

            return Equals(obj as Permission);
        }

        public override int GetHashCode()
        {
            return (Resource, Action, Scope).GetHashCode();
        }

        public override string ToString()
        {
            var permission = $"{Resource}:{Action}";
            if (!Scope.IsNullOrEmpty())
                permission += $".{Scope}";

            return permission;
        }

        public static Permission FromString(string permission)
        {
            if (string.IsNullOrEmpty(permission))
                return null;

            var source = permission.Split(':', '.');

            var resource = (source.Length >= 1) ? source[0] : null;
            var action = (source.Length >= 2) ? source[1] : null;
            var scope = (source.Length >= 3) ? source[2] : null;

            return new Permission(resource, action, scope);
        }
    }
}
