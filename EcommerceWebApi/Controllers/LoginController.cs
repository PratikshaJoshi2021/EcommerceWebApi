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
        public LoginController(EcommerceContext _db)
        {
            db = _db;
        }

        [HttpPost]
        [Route("login")]
        public bool Login(LoginViewModel loginViewModel)
        {
            if (db.TableLogins.Any(x => x.Username == loginViewModel.Username && x.Passwordd == loginViewModel.Passwordd))
            {
                return true;
            }
            return false;
        }
        [HttpPost]
        [Route("register")]
        public void Register(RegisterViewModel registerViewModel)
        {
            TableLogin tblLogin = new TableLogin();
            tblLogin.Username = registerViewModel.Username;
            tblLogin.Passwordd = registerViewModel.Passwordd;
            db.TableLogins.Add(tblLogin);
            db.SaveChanges();
        }
    }
}
