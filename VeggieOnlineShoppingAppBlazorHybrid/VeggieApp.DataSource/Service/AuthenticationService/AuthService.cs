using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using VeggieApp.Model.Model.Authentication;
using VeggieApp.DataSource.Utility;

namespace VeggieApp.DataSource.Service.AuthenticationService
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly ILocalStorageService _localStorage;

        public AuthService(HttpClient httpClient, AuthenticationStateProvider authenticationStateProvider, ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _authenticationStateProvider = authenticationStateProvider;
            _localStorage = localStorage;
        }

        public async Task<RegisterResult> Register(RegisterModel registerModel)
        {
            try
            {

            var result = await _httpClient.PostAsJsonAsync("api/Account", registerModel);
            if (!result.IsSuccessStatusCode)
                return new RegisterResult { Successful = false, Errors = new List<string> { "Error occured" } };
            return new RegisterResult { Successful = true, Errors = new List<string> { "Account Created succesfully" } };
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<LogInResult> Login(LogInModel loginModel)
        {
            try
            {

                //var loginJson = JsonSerializer.Serialize(loginModel);
                var response = await _httpClient.PostAsJsonAsync<LogInModel>("api/Login", loginModel);
                //new StringContent(loginJson, Encoding.UTF8, "application/json");

                /*var response = await _httpClient.PostAsync("api/LogIn/Login", new StringContent(JsonSerializer.Serialize(loginModel), Encoding.UTF8, "application/json"));*/
                var loginResult = JsonSerializer.Deserialize<LogInResult>(await response.Content.ReadAsStringAsync(),
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (!response.IsSuccessStatusCode)
                {
                    return loginResult!;
                }

                await _localStorage.SetItemAsync("authToken", loginResult!.Token);
                ((CustomAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsAuthenticated(loginModel.Email!);
                return loginResult;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync("authToken");
            ((CustomAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsLoggedOut();
            _httpClient.DefaultRequestHeaders.Authorization = null;
        }
    }
}
