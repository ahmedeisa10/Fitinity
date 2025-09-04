using Humanizer.Localisation;
using ITI_Project.Models;

namespace ITI_Project.Repository
{
    public interface IHomeRepository
    {
        Task<IEnumerable<Product>> DisplayProducts(string sTerm = "", int CategoryId = 0);
        Task<IEnumerable<Category>> GetCategory();
    }
}
