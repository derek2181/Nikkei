using aspnetTutorial.Data;
using aspnetTutorial.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aspnetTutorial.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly WebSiteContext _context = null;
        public BookRepository(UserManager<User> userManager, SignInManager<User> signInManager,
            WebSiteContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        public async Task<int> addBook(BookModel model)
        {
            var data = new Book
            {
                date=model.Date,
                people=model.People,
                 userID=model.userID
            };
            var validate = _context.Book.Where(b => b.userID == model.userID).FirstOrDefault();
            if (validate == null)
            {
               _context.Book.Add(data);
                _context.SaveChanges();
                return 1;
            }
            else
            {
                return 2;
            }
           
        }
    }
}
