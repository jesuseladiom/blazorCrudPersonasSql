using BlazorCrudPersonasSql.Client.Helpers;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Security.Claims;
using System.Text.Json;
using BlazorCRUD.Client.Auth;

namespace BlazorCrudPersonasSql.Client.Auth
{
    public class JWTAuthenticationProvider : AuthenticationStateProvider, ILoginService
    {
        private readonly IJSRuntime js;
        private readonly HttpClient httpClient;
        public static readonly string TOKENKEY = "TOKENKEY";

        private AuthenticationState Anonimo => new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

        public JWTAuthenticationProvider(IJSRuntime js, HttpClient httpClient)
        {
            this.js = js;
            this.httpClient = httpClient;
        }

        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            //await Logout();
            var token = await js.GetFromLocalStorage(TOKENKEY);
            if (string.IsNullOrEmpty(token))
            {
                return Anonimo;
            }

            return ConstruirAuthenticationState(token);
        }

        private AuthenticationState ConstruirAuthenticationState(string token)
        {
            //cada vez que el usuario se autentique sea ese token
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt")));
        }

        public async Task Login(string token)
        {
            await js.SetInLocalStorage(TOKENKEY, token);
            var authState = ConstruirAuthenticationState(token);
            //se notifica a la app que el usuario se acaba e autenticar
            NotifyAuthenticationStateChanged(Task.FromResult(authState));
        }

        public async Task Logout()
        {
            httpClient.DefaultRequestHeaders.Authorization = null;
            await js.RemoveItem(TOKENKEY);
            //se notifica a la app que el usuario se acaba e autenticar
            NotifyAuthenticationStateChanged(Task.FromResult(Anonimo));
        }

        private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var claims = new List<Claim>();
            var payload = jwt.Split('.')[1];
            var jsonBytes = ParseBase64WithoutPadding(payload);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

            keyValuePairs.TryGetValue(ClaimTypes.Role, out object roles);

            if (roles != null)
            {
                if (roles.ToString().Trim().StartsWith("["))
                {
                    var parsedRoles = JsonSerializer.Deserialize<string[]>(roles.ToString());

                    foreach (var parsedRole in parsedRoles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, parsedRole));
                    }
                }
                else
                {
                    claims.Add(new Claim(ClaimTypes.Role, roles.ToString()));
                }

                keyValuePairs.Remove(ClaimTypes.Role);
            }

            claims.AddRange(keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString())));

            return claims;
        }

        private byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }

    }
}
