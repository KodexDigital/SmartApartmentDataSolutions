using Microsoft.AspNetCore.Mvc;
using SmartApp.DotNetCore.Services.Interfaces;
using SmartApp.DotNetCore.Services.Requests;
using SmartApp.DotNetCore.Services.Responses;
using SmartApp.DotNetCore.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartApp.DotNetCore.Controllerss
{
    public class AccountController : Controller
    {
        private readonly ISmartAppartmentService smartService;

        public AccountController(ISmartAppartmentService smartService)
        {
            this.smartService = smartService;
        }
        public IActionResult UserRegistration() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UserRegistration(RegisterUserViewModel request)
        {
            if (!ModelState.IsValid)
                ModelState.AddModelError("Request", "Error found on request.");

            var result = await smartService.RegisterUser(new RegisterUserRequest
            {
                Email = request.Email,
                Password = request.Password,
                PhoneNumber = request.PhoneNumber,
                ConfirmPassword = request.ConfirmPassword
            });
            if (result.Status.Equals(true))
            {
                //TempData["User-Email"] = result.Data.Email;
                //TempData["User-Token"] = result.Data.Token;
                ModelState.Clear();
                //ViewBag.Message = result.Message;
                return RedirectToAction(nameof(Login), this);
            }                
            else
                ViewBag.Message = result.Message;

            return View(request);
        }
        
        public IActionResult Login() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel request)
        {
            if (!ModelState.IsValid)
                ModelState.AddModelError("Request", "Error found on request.");

            var result = await smartService.LoginUser(new LoginUserRequest 
            { 
                Email = request.Email, 
                Password = request.Password 
            });
            if (result.Status.Equals(true))
            {
                //TempData["email"] = model.Email;
                ModelState.Clear();
                return RedirectToAction(nameof(AllUsers), this);
            }
            else
                ViewBag.Message = result.Message;

            return View(request);
        }
        public async Task<IActionResult> AllUsers()
        {
            var result = await smartService.GetAllUsers();

            if (result != null)
                return View(result);
            else
                ViewBag.Message = result.Message;

            return View();
        }
    }
}
