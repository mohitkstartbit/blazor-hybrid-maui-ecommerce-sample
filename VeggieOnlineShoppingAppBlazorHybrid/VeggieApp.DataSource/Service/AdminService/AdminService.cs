using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using VeggieApp.Model.Model.Authentication;

namespace VeggieApp.DataSource.Service.AdminService
{
    public class AdminService : IAdminService
    {
        private readonly HttpClient _httpClient;
        public AdminService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<UserDto>> GetUsersAsync()
        {
            var user = await _httpClient.GetFromJsonAsync<List<UserDto>>("api/admin/users");
            if(user != null)
            {
                return user;
            }
            else
            {
                return new List<UserDto>();
            }
        }

        public async Task<List<string>> GetRolesAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<string>>("api/admin/Roles");
        }
        public async Task<List<UserWithRoles>> GetUsersWithRolesAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<UserWithRoles>>("api/admin/users-with-roles");
        }
        public async Task<List<string>> GetUserRolesAsync(string userId) =>
            await _httpClient.GetFromJsonAsync<List<string>>($"api/admin/userroles/{userId}");

        public async Task AssignRoleAsync(string userId, string roleName)
        {
            var model = new AssignRoleModel { UserId = userId, RoleName = roleName };
            var response = await _httpClient.PostAsJsonAsync("api/admin/assignrole", model);
            response.EnsureSuccessStatusCode();
        }

        public async Task RemoveRoleAsync(string userId, string roleName)
        {
            var model = new AssignRoleModel { UserId = userId, RoleName = roleName };
            var response = await _httpClient.PostAsJsonAsync("api/admin/removerole", model);
            response.EnsureSuccessStatusCode();
        }

        public async Task CreateRoleAsync(string roleName)
        {
            var response = await _httpClient.PostAsJsonAsync("api/admin/createrole", roleName);
            response.EnsureSuccessStatusCode();
        }
    }
  

    public class AssignRoleModel
    {
        public string? UserId { get; set; }
        public string? RoleName { get; set; }
    }
}
