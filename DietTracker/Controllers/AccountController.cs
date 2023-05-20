using BusinessLogic.Contracts;
using BusinessLogic.Implementations;
using DataAccessLayer.Entities;
using DietTracker.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace DietTracker.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        private readonly IWeightService _weightService;

        public AccountController(IUserService userService, IRoleService roleService, IWeightService weightService)
        {
            _userService = userService;
            _roleService = roleService;
            this._weightService = weightService;
        }
        [HttpGet]
        public IActionResult NewRegister()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                //Check if user already exists
                User user = await _userService.GetByLoginAsync(model.Login);
                if (user == null)
                {
                    user = new User 
                    { 
                         Login = model.Login,
                         Password = model.Password,
                         Gender = model.Gender,
                         LifeStyle = model.LifeStyle,
                         BirthDate =  model.BirthDate,
                         Height = model.Height,
                         Expectation = DataAccessLayer.Enums.Expectation.Medium //TODO add field yo UI registration
                    };
                    Role userRole = await _roleService.GetAll().FirstOrDefaultAsync(r => r.RoleName == "user");
                    if (userRole != null)
                        user.Role = userRole;
                    var createdUserId = await _userService.CreateAsync(user);
                    var weightHistory = new UserWeightHistory()
                    {
                        UpdatedDate = DateTime.Now,
                        Weight = model.Weight,
                        UserId = createdUserId,
                    };
                    await _weightService.AddUserWeightHistoryAsync(weightHistory);
                    await Authenticate(user);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Wrond login or(and) password");
                }
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await _userService.GetAll()
                    .FirstOrDefaultAsync(u => u.Login == model.Login && u.Password == model.Password);
                if (user != null)
                {
                    await Authenticate(user);

                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Wrong login or password");
            }
            return View(model);
        }
        private async Task Authenticate(User user)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role?.RoleName)
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
        /// <summary>
        /// Check in model does user with such login exist
        /// </summary>
        [AcceptVerbs("Get", "Post")]
        public async Task<IActionResult> CheckLogin(string login)
        {
            var user = await _userService.GetByLoginAsync(login);
            if (user is not null)
                return Json(false);
            return Json(true);
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
    }
}
