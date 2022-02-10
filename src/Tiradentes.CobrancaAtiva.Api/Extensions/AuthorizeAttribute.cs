using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Tiradentes.CobrancaAtiva.Api.Extensions
{
    public class AuthorizeAttribute : TypeFilterAttribute
    {
        public AuthorizeAttribute(string realmRole, string clientRole)
            : base(typeof(AuthorizeActionFilter))
        {
            Arguments = new object[] {realmRole, clientRole};
        }
    }
    
    public class AuthorizeActionFilter : IAuthorizationFilter
    {

        private readonly string _realmRole;
        private readonly string _clientRole;

        public AuthorizeActionFilter(string realmRole, string clientRole)
        {
            _realmRole = realmRole;
            _clientRole = clientRole;
        }

        public void OnAuthorization(AuthorizationFilterContext context) {
            if (!IsAuthorized(context.HttpContext)) {
                context.Result = new UnauthorizedResult();
            }
        }

        private bool IsAuthorized(HttpContext context)
        {
            var realmAccess = context.User.Claims.FirstOrDefault(c => c.Type.Equals("realm_access"))?.Value;
            var resourceAccess = context.User.Claims.FirstOrDefault(c => c.Type.Equals("resource_access"))?.Value;
            var audience = context.User.Claims.FirstOrDefault(c => c.Type.Equals("azp"))?.Value;
            
            if (realmAccess == null) return false;
            
            var realmRoles = JsonSerializer.Deserialize<RealmRole>(realmAccess);
            var resourceAccessRoles = JsonSerializer.Deserialize<IDictionary<string, RealmRole>>(resourceAccess);
            resourceAccessRoles.TryGetValue(audience, out var resourceRoles);
            return realmRoles.Roles.Contains(_realmRole) && resourceRoles != null && resourceRoles.Roles.Contains(_clientRole);
        }
        
        public class RealmRole {

            public RealmRole() {
                Roles = new List<string>();
            }
        
            public List<string> Roles { get; set; }
        }

    }

}