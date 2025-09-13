using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.General;

namespace ITI_Project.Repository
{
    public class AdminRepository : IAdminRepository
    {
        private readonly UserManager<IdentityUser> userManager;

        public AdminRepository(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<IdentityResult> CreateAdmin(string email, string password)
        {
            var user = new IdentityUser
            {
                UserName = email,
                Email = email
            };

            var result = await userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, "Admin");
            }

            return result;
        }

        public async Task<IEnumerable<IdentityUser>> AllUsers()
        {
            return  await userManager.Users.ToListAsync();
        }
    }
}
