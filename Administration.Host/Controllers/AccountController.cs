using Administration.Host.Models.Account;
using BookService.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Administration.Host.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;

        public AccountController(RoleManager<IdentityRole> roleManager, SignInManager<User> signInManager, UserManager<User> userManager)
        {
            _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            var login = new LoginViewModel()
            {
                ReturnUrl = returnUrl
            };

            return View(login);
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    await _signInManager.SignOutAsync();
                    var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
                    if (result.Succeeded)
                    {
                        return Redirect(model.ReturnUrl ?? "/");
                    }
                }
                ModelState.AddModelError(nameof(LoginViewModel.Email), "Неверный логин или пароль");
            }
            return View(model);
        }
        [Authorize]
        public async Task<IActionResult> Logout(string returnUrl)
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            var cookie = HttpContext.Request.Cookies;

            foreach (var item in cookie)
            {
                HttpContext.Response.Cookies.Delete(item.Key);
            }

             return RedirectToAction(returnUrl);
        }



        //private async Task AuthenticateAsync(User user)
        //{
        //    // создаем один claim
        //    var claims = new List<Claim>
        //    {
        //        new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email)
        //    };
        //    // создаем объект ClaimsIdentity
        //    ClaimsIdentity id = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme,
        //        ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
        //    // установка аутентификационных куки
        //    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        //}
        public IActionResult Registration()
        {
            var result = new RegistrationUserViewModel();
            return View(result);

        }
        [HttpPost]
        public async Task<IActionResult> Registration(RegistrationUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var existUser = await _userManager.FindByEmailAsync(model.Email);
            if (existUser != null)
            {
                ModelState.AddModelError(nameof(model.Email), "Email занят другим пользователем");
                return View(model);
            }
            var user = new User();
            user.UserName = model.Email;
            user.Name = model.Name;
            user.Surname = model.Surname;
            user.Email = model.Email;
            user.BirthDate = model.Birthday;

            var customer = await _userManager.CreateAsync(user, model.Password);
            if (!customer.Succeeded)
            {
                ModelState.AddModelError(nameof(customer), "Не удалось зарегестрировать пользователя");
                return View(model);
            }

            var customerRole = await _roleManager.FindByNameAsync("user");
            if (customerRole != null)
            {
                await _userManager.AddToRoleAsync(user, customerRole.Name);
            }
            else
            {
                ModelState.AddModelError(nameof(customerRole), "Не удалось зарегестрировать пользователя");
                return View(model);
            }
            return RedirectToAction("Login");
                
                 
        }

    }
}
