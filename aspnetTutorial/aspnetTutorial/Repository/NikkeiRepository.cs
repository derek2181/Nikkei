using aspnetTutorial.Data;
using aspnetTutorial.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aspnetTutorial.Repository
{
    public class NikkeiRepository : INikkeiRepository
    {
        private readonly WebSiteContext _context = null;


        public NikkeiRepository(WebSiteContext context)
        {
            _context = context;
        }

        //Añadir usuario forma viejita
        //public async Task<int> addUser(UserModel model)
        //{
        //    var newUser = new User()
        //    {
        //        ConfirmPassword = model.ConfirmPassword,
        //        Email = model.Email,
        //        LastName = model.LastName,
        //        Name = model.Name,
        //        Password = model.Password
        //    };

        //  await  _context.Users.AddAsync(newUser);
        //  await  _context.SaveChangesAsync();
        //    return 1;
        //}
        public int addNewReview(ReviewModel model)
        {
            var newReview = new Review()
            {
                rating = model.rating,
                review = model.review
            };

            _context.Reviews.Add(newReview);
            _context.SaveChanges();
            return newReview.ID;
        }

        public async Task<List<DishModel>> GetAllPlatillos()
        {
            //byte[] byteArray=_context.Image.Find()
            var dishesModel = new List<DishModel>();
            var allDishes = await _context.Dishes.ToListAsync();

            foreach (var dish in allDishes)
            {
                dishesModel.Add(new DishModel
                {
                    ID = dish.ID,
                    Description = dish.Description,
                    Name = dish.Name,
                    Price = dish.Price,
                    imageBinary = dish.imageBinary

                }
                );
            }

            return dishesModel;
        }

        public async Task<List<DishModel>> GetMenuSelection(string selection)
        {
            //byte[] byteArray=_context.Image.Find()

            return await _context.Dishes.Where(d=>d.category==selection).Select(dish => new DishModel()
            {
                //rating=dish.Reviews.Sum(r=>r.rating)/dish.Reviews.Count(),
                ID = dish.ID,
                Description = dish.Description,
                Name = dish.Name,
                Price = dish.Price,
                imageBinary = dish.imageBinary

            }).OrderByDescending(d=>d.Price).ToListAsync();
            return DataSource();
        }

        public async Task<List<DishModel>> GetMenuSelectionByPrice(string selection,int price)
        {
            //byte[] byteArray=_context.Image.Find()

            return await _context.Dishes.Where(d => d.category == selection && d.Price<price).Select(dish => new DishModel()
            {
                //rating=dish.Reviews.Sum(r=>r.rating)/dish.Reviews.Count(),
                ID = dish.ID,
                Description = dish.Description,
                Name = dish.Name,
                Price = dish.Price,
                imageBinary = dish.imageBinary

            }).OrderByDescending(d => d.Price).ToListAsync();
        }


        public async Task<List<DishModel>> GetTopDishes(int count)
        {
            //byte[] byteArray=_context.Image.Find()

            //var result = from dishes in _context.Dishes join review in _context.Reviews ;

            return await _context.Dishes.Where(d => d.Reviews.Count() > 0 && d.category=="Nigiri").Select(dish => new DishModel()
            {
                rating = dish.Reviews.Sum(r => r.rating) / dish.Reviews.Count(),
                ID = dish.ID,
                Description = dish.Description,
                Name = dish.Name,
                Price = dish.Price,
                imageBinary = dish.imageBinary

            }).OrderByDescending(d => d.rating).Take(count).ToListAsync();
        }

        public async Task<List<DishModel>> GetTopRolls(int count)
        {
            //byte[] byteArray=_context.Image.Find()

            return await _context.Dishes.Where(d => d.Reviews.Count() > 0 && d.category == "Roll").Select(dish => new DishModel()
            {
                rating = dish.Reviews.Sum(r => r.rating) / dish.Reviews.Count(),
                ID = dish.ID,
                Description = dish.Description,
                Name = dish.Name,
                Price = dish.Price,
                imageBinary = dish.imageBinary

            }).OrderByDescending(d => d.rating).Take(count).ToListAsync();
        }

        public async Task<DishModel> GetDishById(int id)
        {

            return await _context.Dishes.Select(dish => new DishModel()
            {
                ID = dish.ID,
                Description = dish.Description,
                Name = dish.Name,
                Price = dish.Price,

                imageBinary = dish.imageBinary
            }).Where(dish => dish.ID == id).FirstOrDefaultAsync();



        }

        //public List<Platillo> SearchBook(string Author, string Name)
        //{
        //    return DataSource().Where(x => x.Author == Author && x.Title == Name).ToList();
        //}

        //Ejemplo porque aun no tenemos la base de datos
        private List<DishModel> DataSource()
        {
            return new List<DishModel>()
            {
                new DishModel(){ID=1,Name="Sakana",Price=180 ,Description="Contiene salsa valentina y pescado",Stars=3 },
                new DishModel(){ID=2,Name="Sushi",Price=15 ,Description="Tipicos rollos con arroz y tsurimi",Stars=2 },
                new DishModel(){ID=3,Name="Teriyaki",Price=15 ,Description="Pescado empanizado con verduras",Stars=4 },
                new DishModel(){ID=4,Name="Arroz",Price=100 ,Description="El clasico arroz japones con soya",Stars=2 },
                new DishModel(){ID=5,Name="Cangrejo",Price=15 ,Description="Trozos de cangrejo con aderezos y arroz",Stars=5 },
            };
        }
    }
}
