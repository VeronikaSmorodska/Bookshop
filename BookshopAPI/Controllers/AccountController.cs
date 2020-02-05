using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using BookshopAPI.Models;
using BookshopBLL.DTO;
using BookshopBLL.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookshopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMapper mapper;
        public AccountController(IUserService serv, IMapper _mapper)
        {
            _userService = serv;
            mapper = _mapper;
        }
        [HttpGet("[action]")]
        public IActionResult Index()
        {
            return Ok();
            //return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            return Ok();
            //return View();
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> Login(LoginViewModel loginviewmodel)
        {
            if (ModelState.IsValid)
            {
                bool isARealUser = _userService.TestUser(loginviewmodel.Login, loginviewmodel.Password);
                if (isARealUser == false)
                {
                    return BadRequest("User with such login and password does not exist.");
                }
                if (isARealUser == true)
                {
                    UserDTO userDto = _userService.GetByLogin(loginviewmodel.Login);
                    UserViewModel userviewmodel = mapper.Map<UserViewModel>(userDto);
                    await Authenticate(userviewmodel);
                    return Ok(loginviewmodel);
                }
                ModelState.AddModelError("", "Invalid login or password");
            }
            return Ok(loginviewmodel);

        }
       
        [HttpGet]
        public IActionResult Register()
        {
            return Ok();
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> Register(RegisterViewModel registerviewmodel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool isLoginTaken = _userService.TestLogin(registerviewmodel.Login);
                    if (isLoginTaken == true)
                    {
                        return BadRequest("Login must be unique.");
                    }
                    UserViewModel userviewmodel = new UserViewModel
                    {
                        Name = registerviewmodel.Name,
                        Surname = registerviewmodel.Surname,
                        Login = registerviewmodel.Login,
                        Password = registerviewmodel.Password,
                        Role = RoleViewModel.Member
                    };
                    _userService.Create(mapper.Map<UserDTO>(userviewmodel));
                    await Authenticate(userviewmodel);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return Ok(registerviewmodel);
        }
        private async Task Authenticate(UserViewModel userviewmodel)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userviewmodel.Name),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, userviewmodel.Role.ToString())
            };
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id), new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTime.UtcNow.AddMinutes(20)
            });
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
        [HttpGet("[action]")]
        public IActionResult AccessDenied()
        {
            return Unauthorized();
        }
    }
}