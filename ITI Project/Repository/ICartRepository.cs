using ITI_Project.DTO;
using ITI_Project.Models;

namespace ITI_Project.Repository
{
    public interface ICartRepository
    {
        Task<int> IncreaseItem(int productId, int Qty);
        Task<int> DecreaseItem(int productId);
        Task<int> DeleteItem(int productId);
        Task<ShoppingCart> GetUserCart();
        Task<int> GetCartItemCount(string userId);
        Task<ShoppingCart> GetCart(string userId);
        Task<bool> DoCheckout(CheckoutModel model);
        Task<double> GetCartTotal(string userId);
        string GetUserId();


    }
}

