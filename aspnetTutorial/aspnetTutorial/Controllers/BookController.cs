using aspnetTutorial.Data;
using aspnetTutorial.Models;
using aspnetTutorial.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace aspnetTutorial.Controllers
{
    public class BookController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IBookRepository _bookRepository;
       private readonly IAccountRepository _accountRepository;

        public BookController(UserManager<User> userManager, SignInManager<User> signInManager,IBookRepository bookRepository,
            IAccountRepository accountRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _bookRepository = bookRepository;
            _accountRepository = accountRepository;
        }

        [Route("Book/MakeReservation")]
        public async Task<IActionResult> MakeReservation()
        {
            ViewBag.toggleError = 0;

            string userID = _signInManager.UserManager.GetUserId(User);
            User user = await _userManager.FindByIdAsync(userID);

            if (user.isNew)
            {
                ViewBag.ShowOnboarding = true;
                user.isNew = false;
                var result = await _accountRepository.updateUser(user);
            }
            else
            {
                ViewBag.ShowOnboarding = false;
            }
    
          
            return View();
        }
        [HttpPost]
        [Route("Book/MakeReservation")]
        public async Task<IActionResult> MakeReservation(BookModel Book)
        {
            if (ModelState.IsValid)
            {
                string userID = _signInManager.UserManager.GetUserId(User);
                User user = await _userManager.FindByIdAsync(userID);
                Book.userID = userID;
                Book.Date = Convert.ToDateTime(Book.DateDay+ " " + Book.DateHour +" "+ "PM");

               int result=  await _bookRepository.addBook(Book);
                ViewBag.toggleError = result;
            }
        
            return View();
        }
    }
}
