using AutoMapper;
using IdentityByExamples.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityByExamples.Controllers
{
    public class AccountController : Controller
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public AccountController(IMapper mapper,UserManager<User> userManager)
        {
            _mapper = mapper;
            _userManager = userManager;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View("Register");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(UserRegistrationModel registrationModel)
        {
            if(!ModelState.IsValid)
            {
                return View(registrationModel);
            }

            var user = _mapper.Map<User>(registrationModel);
            var result = await _userManager.CreateAsync(user,registrationModel.Password);

            if(!result.Succeeded)
            {
                foreach(var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }
                return View(registrationModel);
            }

            await _userManager.AddToRoleAsync(user, "visitor");

            return RedirectToAction(nameof(HomeController.Index),"Home");
        }
    }
}
