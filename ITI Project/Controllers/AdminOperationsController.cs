using ITI_Project.Constants;
using ITI_Project.DTO;
using ITI_Project.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

namespace ITI_Project.Controllers
{
    [Authorize(Roles = nameof(Roles.Admin))]
    public class AdminOperationsController : Controller
    {
        private readonly IUserOrderRepository userOrderRepository;
        private readonly IAdminRepository adminRepository;

        public AdminOperationsController(IUserOrderRepository userOrderRepository,
            IAdminRepository adminRepository)
        {
            this.userOrderRepository = userOrderRepository;
            this.adminRepository = adminRepository;
        }
        public async Task<IActionResult> AllOrders()
        {
            var orders = await userOrderRepository.UserOrders(true);
            return View(orders);
        }

        public async Task<IActionResult> TooglePaymentStatus(int orderId)
        {
            try
            {
                await userOrderRepository.TogglePaymentStatus(orderId);
            }
            catch (Exception ex)
            {
                //log exception
            }
            return RedirectToAction(nameof(AllOrders));
        }


        public async Task<IActionResult> UpdateOrderStatus(int orderId)
        {
            var order = await userOrderRepository.GetOrderById(orderId);
            if (order == null)
            {
                throw new InvalidOperationException($"Order with id:{orderId} does not found.");
            }
            var orderStatusList = (await userOrderRepository.GetOrderStatuses()).Select(orderStatus =>
            {
                return new SelectListItem
                {
                    Value = orderStatus.Id.ToString(),
                    Text = orderStatus.StatusName,
                    Selected = order.OrderStatusId == orderStatus.Id
                };
            });
            var data = new UpdateOrderStatusModel
            {
                OrderId = orderId,
                OrderStatusId = order.OrderStatusId,
                OrderStatusList = orderStatusList
            };
            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateOrderStatus(UpdateOrderStatusModel data)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    data.OrderStatusList = (await userOrderRepository.GetOrderStatuses()).Select(orderStatus =>
                    {
                        return new SelectListItem
                        {
                            Value = orderStatus.Id.ToString(),
                            Text = orderStatus.StatusName,
                            Selected = orderStatus.Id == data.OrderStatusId
                        };
                    });

                    return View(data);
                }
                await userOrderRepository.ChangeOrderStatus(data);
                TempData["msg"] = "Updated successfully";
            }
            catch (Exception ex)
            {
                // catch exception here
                TempData["msg"] = "Something went wrong";
            }
            return RedirectToAction(nameof(UpdateOrderStatus), new { orderId = data.OrderId });
        }

        public IActionResult Dashboard()
        {
            return View();
        }

        [HttpGet]
        public IActionResult CreateAdmin()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAdmin(string email, string password)
        {
            if (ModelState.IsValid)
            {
                var result = await adminRepository.CreateAdmin(email, password);

                if (result.Succeeded)
                {
                    TempData["Success"] = "Admin created successfully!";
                    return RedirectToAction("CreateAdmin");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View();
        }


        public async Task<IActionResult> AllUsers()
        {
            var users = await adminRepository.AllUsers();
            return View(users);
        }

    }
}
