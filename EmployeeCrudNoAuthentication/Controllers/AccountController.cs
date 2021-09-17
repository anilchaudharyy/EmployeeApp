using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeCrudNoAuthentication.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        public AccountController(UserManager<IdentityUser> userManager, 
            SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Register()
        {
            string username = "anil@xyz.com";
            string email = "anil@xyz.com";
            string pwd = "Hello_123#";
            var user = new IdentityUser { UserName = username, Email = email };
            var result =  await  _userManager.CreateAsync(user,pwd);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction("Index");
            }
            return View();
        }
        public async Task<IActionResult> LogInn()
        {
            string username = "anil@xyz.com";
            string pwd = "Hello_123#";
            var result = await _signInManager.PasswordSignInAsync(username, pwd, false, false);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("InvalidUser");
            }
          
        }
        public async Task<IActionResult> InvalidUser()
        {
            return View();
        }
        public async Task<IActionResult> AdminView()
        {
            return View();
        }
    }
}
