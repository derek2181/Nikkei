using aspnetTutorial.Models;
using System.Threading.Tasks;

namespace aspnetTutorial.Repository
{
    public interface IBookRepository
    {
     Task<int> addBook(BookModel model);
    }
}