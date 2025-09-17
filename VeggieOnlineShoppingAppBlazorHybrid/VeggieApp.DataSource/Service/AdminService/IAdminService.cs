using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeggieApp.Model.Model.Authentication;

namespace VeggieApp.DataSource.Service.AdminService
{
    public interface IAdminService
    {
        Task<List<UserDto>> GetUsersAsync();
        Task<List<string>> GetRolesAsync();
        Task<List<UserWithRoles>> GetUsersWithRolesAsync();
        Task<List<string>> GetUserRolesAsync(string userId);
        Task AssignRoleAsync(string userId, string roleName);
        Task RemoveRoleAsync(string userId, string roleName);
        Task CreateRoleAsync(string roleName);
    }
}
