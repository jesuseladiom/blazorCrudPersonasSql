using Microsoft.AspNetCore.Components.Authorization;
using System.Reflection;
using System.Security.Claims;

namespace BlazorCrudPersonasSql.Client.Auth
{
    
    public class AuthStateProviderFalso : AuthenticationStateProvider
    {
        // este metodo determina si el usuario esta autenticado
        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            //var identity = new ClaimsIdentity(new List<Claim>
            //{
            //    new Claim(ClaimTypes.Name, "Felipe")
            //}, "prueba");
            var identity = new ClaimsIdentity();
            return await Task.FromResult(new AuthenticationState(new ClaimsPrincipal(identity)));


        }
    }
}
