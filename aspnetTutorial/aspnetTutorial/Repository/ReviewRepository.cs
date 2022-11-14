using aspnetTutorial.Data;
using aspnetTutorial.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace aspnetTutorial.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly WebSiteContext _context = null;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public ReviewRepository(WebSiteContext context, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        //[Authorize]
        public async void addReview(string review, int rating, string idUser, int idDish)
        {
            //User user = await _userManager.FindByIdAsync(_userManager.GetUserId());
            var newReview = new Review()
            {

                date = DateTime.Now,
                rating = rating,
                review = review,
                userID = idUser,
                dishID = idDish
            };

            var result = _context.Reviews.Add(newReview);

            _context.SaveChanges();
        }

        public async Task<List<ReviewModel>> GetDishReviews(int ID)
        {
            var reviewsModel = new List<ReviewModel>();
            var reviews = await _context.Reviews.Where(x => x.dish.ID == ID).Select(review => new ReviewModel()
            {
                date = review.date,
                shortDate = review.date.Value.ToString("dd/MM/yyyy"),
                rating = review.rating,
                review = review.review,
                nombreUsuario = review.user.Name + " " + review.user.LastName,
                dishName = review.dish.Name,
                imageBinary = review.dish.imageBinary,
                dias = ((DateTime.Now - review.date).Value.Days),
                horas = ((DateTime.Now - review.date).Value.Hours),
                
            }).ToListAsync();

            List<ReviewModel> model = new List<ReviewModel>();
            foreach(var dato in reviews)
            {
         
                
                if (dato.dias >= 30)
                {
                    if ((dato.dias / 30) > 1)
                    {
                        dato.finalResult = $"Hace {dato.dias / 30} meses ";
                    }
                    else if((dato.dias / 30) == 1)
                    {
                        dato.finalResult = $"Hace 1 mes";
                    }

                }
                else if(dato.dias>1 && dato.dias < 30)
                {
                    dato.finalResult = $"Hace {dato.dias} dias ";
                }else if (dato.dias == 1)
                {
                    dato.finalResult = $"Hace 1 dia";
                }

                if (dato.dias < 1 && dato.horas>=2)
                {
                    dato.finalResult = $"Hace {dato.horas} horas";
                }

                if(dato.dias<1 && dato.horas < 2)
                {
                    dato.finalResult = $"Hace un momento";
                }

                model.Add(dato);
            }
            //if (reviews?.Any() == true)
            //{
            //    foreach(var review in reviews)
            //    {
            //        reviewsModel.Add(new ReviewModel()
            //        {
            //        shortDate=review.date.Value.ToString("dd/MM/yyyy"),
            //        rating=review.rating,
            //        review=review.review

            //        });
            //    }
            //}
            return model;
        }

        //Ejemplo porque aun no tenemos la base de datos
        private List<ReviewModel> DataSource()
        {
            return new List<ReviewModel>()
            {
                   new ReviewModel(){ID=1, rating=2,review="Esta super padre este lugar",shortDate=DateTime.Now.ToString("dd/MM/yyyy")},
                  new ReviewModel(){ID=1,rating=4,review="Me encantan los pescados de nikkei",shortDate=DateTime.Now.ToString("dd/MM/yyyy")},
                   new ReviewModel(){ID=2,rating=1,review="Esta super padre este lugar",shortDate=DateTime.Now.ToString("dd/MM/yyyy")},
                   new ReviewModel(){ID=2,rating=3,review="Vengan a visitarlo cuando puedan de verdad",shortDate=DateTime.Now.ToString("dd/MM/yyyy")},
                  new ReviewModel(){ID=3,rating=2,review="Esta super padre este lugar",shortDate=DateTime.Now.ToString("dd/MM/yyyy")},
            };
        }
    }
}
