using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;

public class CustomAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly ILocalStorageService _localStorage;
    private ClaimsPrincipal _anonymous = new ClaimsPrincipal(new ClaimsIdentity());

    public CustomAuthenticationStateProvider(ILocalStorageService localStorage)
    {
        _localStorage = localStorage;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var savedToken = await _localStorage.GetItemAsync<string>("authToken");

        if (string.IsNullOrWhiteSpace(savedToken))
        {
            return new AuthenticationState(_anonymous);
        }

        var claims = ParseClaimsFromJwt(savedToken);
        var identity = new ClaimsIdentity(claims, "jwt");
        var user = new ClaimsPrincipal(identity);

        return new AuthenticationState(user);
    }

    public void NotifyUserAuthentication(ClaimsPrincipal user)
    {
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
    }

    public void NotifyUserLogout()
    {
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_anonymous)));
    }

    private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
    {
        var claims = new List<Claim>();

        var payload = jwt.Split('.')[1];
        var jsonBytes = ParseBase64WithoutPadding(payload);
        var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

        foreach (var kvp in keyValuePairs)
        {
            if (kvp.Key == "unique_name" || kvp.Key == "sub")
            {
                claims.Add(new Claim(ClaimTypes.Name, kvp.Value.ToString()));
            }
            else if (kvp.Key.Contains("role", StringComparison.OrdinalIgnoreCase))
            {
                if (kvp.Value is JsonElement roleElement)
                {
                    if (roleElement.ValueKind == JsonValueKind.Array)
                    {
                        foreach (var role in roleElement.EnumerateArray())
                        {
                            claims.Add(new Claim(ClaimTypes.Role, role.GetString()));
                        }
                    }
                    else
                    {
                        claims.Add(new Claim(ClaimTypes.Role, roleElement.GetString()));
                    }
                }
                else
                {
                    claims.Add(new Claim(ClaimTypes.Role, kvp.Value.ToString()));
                }
            }
            else
            {
                claims.Add(new Claim(kvp.Key, kvp.Value.ToString()));
            }
        }

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
