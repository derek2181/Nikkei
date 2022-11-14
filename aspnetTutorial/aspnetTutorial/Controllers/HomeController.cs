using aspnetTutorial.Data;
using aspnetTutorial.Models;
using aspnetTutorial.Repository;
using aspnetTutorial.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace aspnetTutorial.Controllers
{
    public class HomeController : Controller
    {
        //Si el nombre del metodo es igual al nombre de la vista there is no need
        //to do anything else con acento hindi
        private INikkeiRepository _nikkeiRepository = null;
        private IReviewRepository _reviewRepository = null;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IAccountRepository _accountRepository;
        private readonly IEmailService _emailService;

        public HomeController(INikkeiRepository nikkeiRepository,IReviewRepository reviewRepository,
            UserManager<User> userManager,SignInManager<User> signInManager,
            IAccountRepository accountRepository,IEmailService emailService )
        {
            
            _nikkeiRepository = nikkeiRepository;
            _reviewRepository = reviewRepository;
            _userManager = userManager;
            _signInManager = signInManager;
            _accountRepository = accountRepository;
            _emailService = emailService;
        }
    //public async Task<IActionResult> ExceptionPage()
    //{

    //        return View();
    //}
        public async Task<IActionResult> AboutUs()
        {

            return View();
        }

        [HttpPost("Account")]
        public async Task<IActionResult> Account(ChangePasswordModel model)
        {
            // await _accountRepository.Logout();
            if (ModelState.IsValid)
            {
                string userID = _signInManager.UserManager.GetUserId(User);
                User user = await _userManager.FindByIdAsync(userID);

                user.Name = model.Name;
                user.LastName = model.LastName;
                var result= await _accountRepository.updateUser(user);


            }
             return View();
        }
        [Route("Account")]
        public async Task<IActionResult> Account()
        {

            return View();
        }
        //[HttpPost]
       
        [Route("/Home/Review")]
     
        public async Task<ActionResult> addReview(  string review, int rating,int ID)
        {
            //TODO: Arreglar el ajax
         
           _reviewRepository.addReview(review ==null ? "" : review, rating, _signInManager.UserManager.GetUserId(User), ID);
            List<ReviewModel> data = new List<ReviewModel>();
            data = await _reviewRepository.GetDishReviews(ID);
            int stars = 0;
            float starsRealValue = 0;
            foreach (var rev in data)
            {
                stars += rev.rating;
                starsRealValue+=rev.rating;
            }
            if (data.Count != 0)
            {
                stars /= data.Count;
                starsRealValue /= data.Count;
            }

            decimal decimalRating = Math.Round((decimal)starsRealValue, 2);


            if (starsRealValue > 4.5)
            {
                stars = 5;
            }


          
            var dish = await _nikkeiRepository.GetDishById(ID);

            
                return Json(new
                {
                    reviews = data.ToArray(),
                    imageData = dish.imageBinary,
                    name = dish.Name,
                    rate = stars,
                    realRate = decimalRating,
                    description = dish.Description,
                    numberReviews = data.Count
                    //Items = new[] { "Abdullah Khamir", "Mark Schrenberg", "Katy Sullivan", "Erico Gantomaro", }
                });
            
           
        }
        public async Task<ViewResult> Index()
        {
            //UserEmailOptions options = new UserEmailOptions
            //{
            //    ToEmails = new List<string>() { "felix.larasz@uanl.edu.mx" },
            //    PlaceHolders = new List<KeyValuePair<string, string>>()
            //   {
            //       new KeyValuePair<string,string>("{{UserName}}","Melics")
            //   }
            //};
            //await _emailService.SendTestEmail(options);
            try
            {
                var dishes = await _nikkeiRepository.GetAllPlatillos();
                return View(dishes);
            }
            catch(Exception e)
            {
              ViewBag.Exception=  e.StackTrace;
               ViewBag.Message= e.Message;
                ViewBag.Source = e.Source;
                ViewBag.Type = e.GetType().ToString();

                ViewBag.Target = e.TargetSite;
            }
            return View("ExceptionPage");
        
        }
        [Route("/Home/Menu")]
        public async Task<ViewResult> Menu(string selection="Roll")
        {

            
            ViewBag.Selection = selection;
            var dishes = await _nikkeiRepository.GetMenuSelection(selection);

            return View(dishes);
        }

        [Route("/Home/GetSelection")]
        [HttpGet]
        public async Task<ActionResult> GetSelection(string selection)
        {
            
            ViewBag.Selection = selection;
            var dishes = await _nikkeiRepository.GetMenuSelection(selection);

            var result= _signInManager.IsSignedIn(User);
            return Json(new
            {
                data = dishes.ToArray(),
                sesion=result
                //Items = new[] { "Abdullah Khamir", "Mark Schrenberg", "Katy Sullivan", "Erico Gantomaro", }
            });

       
        }
        [Route("/Home/GetSelectionByPrice")]
        [HttpGet]
        public async Task<ActionResult> GetSelectionByPrice(string selection,int price)
        {

            ViewBag.Selection = selection;
            var dishes = await _nikkeiRepository.GetMenuSelectionByPrice(selection,price);

            var result = _signInManager.IsSignedIn(User);
            return Json(new
            {
                data = dishes.ToArray(),
                sesion = result
                //Items = new[] { "Abdullah Khamir", "Mark Schrenberg", "Katy Sullivan", "Erico Gantomaro", }
            });


        }


        [Route("/Home/Modal")]
        [Authorize]
        public async Task<ActionResult> getDish(int ID)
        {
                 List<ReviewModel> data = new List<ReviewModel>();
            data = await _reviewRepository.GetDishReviews(ID);
            int stars = 0;
            float starsRealValue = 0;
            foreach(var review in data)
            {
                stars += review.rating;
                starsRealValue += review.rating;

                if (review.dias > 30)
                {
                    
                }
            }
            if(data.Count != 0)
            {
                stars /= data.Count;
                starsRealValue /= data.Count;
            }

            decimal decimalRating = Math.Round((decimal)starsRealValue, 2);


            if (starsRealValue > 4.5)
            {
                stars = 5;
            }
            var dish = await _nikkeiRepository.GetDishById(ID);
   
       
            return Json(new
            {
               reviews=data.ToArray(),
               imageData=dish.imageBinary,
               name=dish.Name,
               rate=stars,
               realRate= decimalRating,
               description=dish.Description,
               numberReviews = data.Count
                //Items = new[] { "Abdullah Khamir", "Mark Schrenberg", "Katy Sullivan", "Erico Gantomaro", }
            }); 
        }
    }
}
