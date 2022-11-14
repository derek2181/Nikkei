using aspnetTutorial.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace aspnetTutorial.Repository
{
    public interface INikkeiRepository
    {
        int addNewReview(ReviewModel model);
        Task<List<DishModel>> GetAllPlatillos();
        Task<DishModel> GetDishById(int id);
        Task<List<DishModel>> GetMenuSelection(string selection);
        Task<List<DishModel>> GetTopDishes(int count);
        Task<List<DishModel>> GetTopRolls(int count);
        Task<List<DishModel>> GetMenuSelectionByPrice(string selection, int price);
    }
}