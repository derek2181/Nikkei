using aspnetTutorial.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace aspnetTutorial.Repository
{
    public interface IReviewRepository
    {
        void addReview(string review, int rating, string idUser, int idDish);
        Task<List<ReviewModel>> GetDishReviews(int ID);
    }
}