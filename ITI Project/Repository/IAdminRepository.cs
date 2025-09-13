using Microsoft.AspNetCore.Identity;

namespace ITI_Project.Repository
{
    public interface IAdminRepository
    {
        Task<IdentityResult> CreateAdmin(string email, string password);
        Task<IEnumerable<IdentityUser>> AllUsers();
    }
}
