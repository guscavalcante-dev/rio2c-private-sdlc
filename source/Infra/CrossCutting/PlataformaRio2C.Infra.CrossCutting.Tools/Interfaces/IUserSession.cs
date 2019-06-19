using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlataformaRio2C.Infra.CrossCutting.Tools.Interfaces
{
    public interface IUserSession
    {
        string LastAccess { get; set; }
        string Name { get; set; }
        string UserName { get; set; }
        string Ip { get; set; }
        string[] Roles { get; set; }
        void Reset();
    }

    public abstract class BaseUserSession : IUserSession
    {
        public const string UserLastAccess = "UserSession.LastAccess";
        public const string NameSessionKey = "UserSession.Name";
        public const string UserNameSessionKey = "UserSession.UserName";
        public const string RolesSessionKey = "UserSession.Roles";
        public const string IpSessionKey = "UserSession.Ip";
        public const string UserPermissionsKeys = "UserSession.Permissions";

        public int[] Permissions
        {
            set
            {
                HttpContext.Current.Session[UserPermissionsKeys] = value;
            }
        }

        public bool HasPermission(IEnumerable<int> requiredPermissions)
        {
            var userPermissions = HttpContext.Current.Session[UserPermissionsKeys] as int[];
            if (userPermissions == null) return false;

            return requiredPermissions.All(e => userPermissions.Contains(e));
        }

        public string Name
        {
            get
            {
                var contexto = HttpContext.Current.Session[NameSessionKey];
                return contexto as string;
            }
            set
            {
                HttpContext.Current.Session[NameSessionKey] = value;
            }
        }

        public string LastAccess
        {
            get
            {
                var contexto = HttpContext.Current.Session[UserLastAccess];
                return contexto as string;
            }
            set
            {
                HttpContext.Current.Session[UserLastAccess] = value;
            }
        }

        public string UserName
        {
            get
            {
                var contexto = HttpContext.Current.Session[UserNameSessionKey];
                return contexto as string;
            }
            set
            {
                HttpContext.Current.Session[UserNameSessionKey] = value;
            }
        }

        public string Ip
        {
            get
            {
                var contexto = HttpContext.Current.Session[IpSessionKey];
                return contexto as string;
            }
            set
            {
                HttpContext.Current.Session[IpSessionKey] = value;
            }
        }

        public string[] Roles
        {
            get
            {
                var contexto = HttpContext.Current.Session[RolesSessionKey];
                return contexto as string[];
            }
            set
            {
                HttpContext.Current.Session[RolesSessionKey] = value;
            }
        }

        public virtual void Reset()
        {
            HttpContext.Current.Session[NameSessionKey] = null;
            HttpContext.Current.Session[UserNameSessionKey] = null;
            HttpContext.Current.Session[RolesSessionKey] = null;
            HttpContext.Current.Session[IpSessionKey] = null;
        }
    }
}
