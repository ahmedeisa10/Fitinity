using System.Threading.Tasks;
using ITI_Project.Data;
using ITI_Project.DTO;
using ITI_Project.Models;
using ITI_Project.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Stripe.Checkout;
using Stripe.Climate;

namespace ITI_Project.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly ICartRepository cartRepository;
        private readonly IUserOrderRepository userOrderRepository;

        public CartController(ICartRepository cartRepository,IUserOrderRepository userOrderRepository)
        {
            this.cartRepository = cartRepository;
            this.userOrderRepository = userOrderRepository;
        }

        public async Task<IActionResult> IncreaseItem(int productId, int Qty = 1, int redirect = 0)
        {
            try
            {
                var cartCount = await cartRepository.IncreaseItem(productId, Qty);

                if (redirect == 0)
                {
                    var userid = cartRepository.GetUserId();
                    var total = await cartRepository.GetCartTotal(userid);

                    return Json(new
                    {
                        success = true,
                        count = cartCount,
                        total = total,
                        message = "Item added successfully"
                    });
                }

                return RedirectToAction("GetUserCart");
            }
            catch (UnauthorizedAccessException)
            {
                return Json(new
                {
                    success = false,
                    count = 0,
                    total = 0,
                    message = "You must be logged in"
                });
            }
            catch (Exception)
            {
                return Json(new
                {
                    success = false,
                    count = 0,
                    total = 0,
                    message = "Something went wrong!"
                });
            }
        }



        public async Task<IActionResult> DecreaseItem(int productId)
        {
            var cartCount = await cartRepository.DecreaseItem(productId);
            return RedirectToAction("GetUserCart");
        }
        public async Task<IActionResult> DeleteItem(int productId)
        {
            var cartCount = await cartRepository.DeleteItem(productId);
            return RedirectToAction("GetUserCart");
        }



        public async Task<IActionResult> GetUserCart()
        {
            var cart = await cartRepository.GetUserCart();
            return View(cart);
        }

        public async Task<IActionResult> GetCartItemCount(string UserId)
        {
            var CartItemCount = await cartRepository.GetCartItemCount(UserId);
            return Ok(CartItemCount);
        }

        public IActionResult DoCheckout()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DoCheckout(CheckoutModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            bool isCheckedOut = await cartRepository.DoCheckout(model);
            if (!isCheckedOut)
                return RedirectToAction(nameof(OrderFailure));
            return RedirectToAction(nameof(OrderSuccess));
        }

        public async Task<IActionResult> OrderSuccess(int id)
        {
            return View();
            
        }


        public IActionResult OrderFailure()
        {
            return View();
        }
    }
}
