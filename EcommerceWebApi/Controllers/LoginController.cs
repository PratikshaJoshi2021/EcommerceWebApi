using EcommerceWebApi.Interfaces;
using EcommerceWebApi.Models;
using EcommerceWebApi.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        EcommerceContext db;
        IJWTManagerRepository iJWTManagerRepository;
        public LoginController(EcommerceContext _db, IJWTManagerRepository _iJWTManagerRepository)
        {
            db = _db;
            iJWTManagerRepository = _iJWTManagerRepository;
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login(LoginViewModel loginViewModel)
        {
            var token = iJWTManagerRepository.Authenicate(loginViewModel, false);
            if (token == null)
            {
                return Unauthorized();
            }
            return Ok(token);
        }
        [HttpPost]
        [Route("register")]
        public IActionResult Register(RegisterViewModel registerViewModel)
        {
            LoginViewModel login = new LoginViewModel();
            login.Username = registerViewModel.Username;
            login.Passwordd = registerViewModel.Passwordd;
            var token = iJWTManagerRepository.Authenicate(login, true);
            if (token.IsUserExits)
            {
                return Ok("User already exists");
            }
            if (token == null)
            {
                return Unauthorized();
            }
            return Ok(token);
        }
    }
}