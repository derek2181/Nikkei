using aspnetTutorial.Data;
using aspnetTutorial.Models;
using aspnetTutorial.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace aspnetTutorial.Controllers
{
    public class LoginController : Controller
    {
  
        private IAccountRepository _accountRepository = null;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        Dictionary<string, string> errores = new Dictionary<string, string>();

        public LoginController(IAccountRepository accountRepository, UserManager<User> userManager,
            SignInManager<User> signInManager)
        {
            _accountRepository = accountRepository;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public async Task<ViewResult> SignIn(string returnUrl="")
        {
            if(returnUrl!= "")
            {
                ViewBag.returnUrl = returnUrl;
            }
            else
            {
                ViewBag.returnUrl = Request.Headers["Referer"].ToString();
            }
            UserSignInModel user = new UserSignInModel
            {
                ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList()
            };
            return View(user);
        }
     
        public async Task<IActionResult> Logout()
        {
            await _accountRepository.Logout();
            return RedirectToAction("Index", "Home");
        }
        public IActionResult SignUp(bool isSuccess=false)
        {
            ViewBag.IsSuccess = isSuccess;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(UserSignInModel user, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var result=await _accountRepository.PasswordSignIn(user);

                if (result.Succeeded)
                {
                    var userName = this.User.FindFirstValue(ClaimTypes.Name);
                    ViewBag.userName = userName;
                    return Redirect(returnUrl==null ? "/Nikkei" : returnUrl);
    
                }
              
                if(result.IsNotAllowed)
                {
                    ViewBag.Error = "Correo no verificado";
                }
                else
                {
                    ViewBag.Error = "Credenciales invalidas";
                }
              

                
            }
            return View(user);
        }


        [HttpPost]

        public async Task<IActionResult> SignUp(UserModel user)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var userCreation = await _userManager.FindByEmailAsync(user.Email);

                    if (userCreation != null)
                    {
                        ViewBag.Error = $"El usuario {user.Email} ya se encuentra en uso";
                        return View();
                    }
                    else
                    {
                        var result = await _accountRepository.CreateUser(user);
                        //if (!result.Succeeded)
                        //{
                        //    foreach(var errorMessage in result.Errors){
                        //        ModelState.AddModelError("", errorMessage.Description);
                        //    }
                        //}
                        return RedirectToAction("ConfirmationEmail", new { email = user.Email });
                    }



                }
                errores.Add("Name", ModelState.GetFieldValidationState("Name").ToString());
                errores.Add("LastName", ModelState.GetFieldValidationState("LastName").ToString());
                errores.Add("Email", ModelState.GetFieldValidationState("Email").ToString());
                errores.Add("Password", ModelState.GetFieldValidationState("Password").ToString());
                errores.Add("ConfirmPassword", ModelState.GetFieldValidationState("ConfirmPassword").ToString());
                ViewBag.ListOfErrors = errores;

                ViewBag.IsSuccess = false;

                return View();
            }
            catch (Exception e)
            {
                ViewBag.Exception = e.StackTrace;
                ViewBag.Message = e.Message;
                ViewBag.Source = e.Source;
                ViewBag.Type = e.GetType().ToString();

                ViewBag.Target = e.TargetSite;
            }
            return View("ExceptionPage");
        }


        [HttpPost]
        public IActionResult ExternalLogin(string provider,string returnUrl)
        {
            var redirectUrl = Url.Action("ExternalLoginCallback", "Login",
                new { ReturnUrl = returnUrl });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return new ChallengeResult(provider, properties);
        }
        public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null,string remoteError=null)
        {
           

            UserSignInModel model = new UserSignInModel
            {
                ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList()
            };

            if (remoteError != null)
            {
                ModelState.AddModelError(string.Empty, $"Error desde el proveedor externo:{remoteError}");
                return View("SignIn");
            }

            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                ModelState.AddModelError(string.Empty, "Error al cargar el login externo.");

                return View("SignIn");
            }

            var signInResult = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider,
                info.ProviderKey,isPersistent:false,bypassTwoFactor:true);

            if (signInResult.Succeeded)
            {
                return Redirect(returnUrl == null ? "/Nikkei" : returnUrl);
            }
            else
            {
                var email = info.Principal.FindFirstValue(ClaimTypes.Email);

                if (email != null)
                {
                    var user = await _userManager.FindByEmailAsync(email);
                    if (user == null)
                    {
                        user = new User
                        {
                            UserName = info.Principal.FindFirstValue(ClaimTypes.Email),
                            Name = info.Principal.FindFirstValue(ClaimTypes.GivenName),
                            LastName = info.Principal.FindFirstValue(ClaimTypes.Surname),
                            Email=info.Principal.FindFirstValue(ClaimTypes.Email),
                            EmailConfirmed=true,
                            isNew = true,
                        };
                      var resultado=  await _userManager.CreateAsync(user);
                    }
                  var resultado2=  await _userManager.AddLoginAsync(user, info);
                  await _signInManager.SignInAsync(user, isPersistent: false);

                    return Redirect(returnUrl == null ? "/Nikkei" : returnUrl);
                }

            }

            return View("SignIn");
        }

        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmationEmail(string uid, string token, string email)
        {
            try
            {

         
            EmailConfirmModel model = new EmailConfirmModel
            {
                Email = email
            };
            if (!string.IsNullOrEmpty(uid) && !string.IsNullOrEmpty(token))
            {
                token = token.Replace(' ', '+');
                var result = await _accountRepository.ConfirmEmail(uid, token);
                if (result.Succeeded)
                {
                    model.EmailVerified = true;
                }
            }
            return View("ConfirmationEmail", model);
            }
            catch (Exception e)
            {
                ViewBag.Exception = e.StackTrace;
                ViewBag.Message = e.Message;
                ViewBag.Source = e.Source;
                ViewBag.Type = e.GetType().ToString();

                ViewBag.Target = e.TargetSite;
            }
            return View("ExceptionPage");
        }

        [HttpPost("confirm-email")]
        public async Task<IActionResult> ConfirmationEmail(EmailConfirmModel model)
        {
            try
            {
                var user = await _accountRepository.GetUserByEmail(model.Email);
                if (user != null)
                {
                    if (user.EmailConfirmed)
                    {
                        model.EmailVerified = true;
                        return View(model);
                    }
                    await _accountRepository.GenerateEmailConfirmationTokenAsync(user);
                    model.EmailSent = true;

                    ModelState.Clear();
                }
                else
                {

                }
                return View("ConfirmationEmail", model);
            }
            catch (Exception e)
            {
                ViewBag.Exception = e.StackTrace;
                ViewBag.Message = e.Message;
                ViewBag.Source = e.Source;
                ViewBag.Type = e.GetType().ToString();

                ViewBag.Target = e.TargetSite;
            }
            return View("ExceptionPage");
        }
        //public Platillo GetBook(int id)
        //{
        //    return _bookRepository.GetBookById(id);
        //}

        //public List<Platillo> searchBook(string bookName, string AuthorName)
        //{
        //    return _bookRepository.SearchBook(AuthorName,bookName);
        //} 
    }
}
