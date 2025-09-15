using ITI_Project.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ITI_Project.Controllers
{
    [Authorize]
    public class UserOrderController : Controller
    {
        private readonly IUserOrderRepository userOrderRepository;
        private readonly UserManager<IdentityUser> userManager;

        public UserOrderController(IUserOrderRepository userOrderRepository,
            UserManager<IdentityUser> userManager)
        {
            this.userOrderRepository = userOrderRepository;
            this.userManager = userManager;
        }
        public async Task<IActionResult> UserOrders()
        {
            var orders = await userOrderRepository.UserOrders();
            return View(orders);
        }

    }
}
