using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Tiradentes.CobrancaAtiva.Api.Extensions
{
    public class AutorizacaoAttribute : TypeFilterAttribute
    {
        private const string RealmRole = "app-multiplas-empresas-cobranca";
        private const string DefaultCLientRole = "financeiro_holding";

        public AutorizacaoAttribute()
            : base(typeof(AutorizacaoActionFilter))
        {
            Arguments = new object[] {RealmRole, new[] {DefaultCLientRole}};
        }

        public AutorizacaoAttribute(string clientRole)
            : base(typeof(AutorizacaoActionFilter))
        {
            Arguments = new object[] {RealmRole, new[] {DefaultCLientRole, clientRole}};
        }
    }

    public class AutorizacaoActionFilter : IAuthorizationFilter
    {
        private readonly string _realmRole;
        private readonly string[] _clientRole;

        public AutorizacaoActionFilter(string realmRole, string[] clientRole)
        {
            _realmRole = realmRole;
            _clientRole = clientRole;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!IsAuthorized(context.HttpContext))
            {
                context.Result = new ForbidResult();
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
            return realmRoles.roles.Contains(_realmRole) && resourceRoles != null &&
                   resourceRoles.roles.Any(r => _clientRole.Contains(r));
        }
    }

    public class RealmRole
    {
        public RealmRole()
        {
            roles = new List<string>();
        }

        public List<string> roles { get; set; }
    }
}