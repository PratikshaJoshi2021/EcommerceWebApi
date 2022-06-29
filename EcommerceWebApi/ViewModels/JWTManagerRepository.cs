using EcommerceWebApi.Interfaces;
using EcommerceWebApi.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceWebApi.ViewModels
{
    public class JWTManagerRepository : IJWTManagerRepository
    {
        Dictionary<string, string> UserRecords;

        private readonly IConfiguration configuration;
        private readonly EcommerceContext db;

        public JWTManagerRepository(IConfiguration _configuration, EcommerceContext _db)
        {
            db = _db;
            configuration = _configuration;
        }
        public Tokens Authenicate(LoginViewModel registerViewModel, bool IsRegister)
        {
            var _isAdmin = false;
            var _isUserExists = false;
            if (IsRegister)
            {
                if (db.TableLogins.Any(x => x.Username == registerViewModel.Username && x.Passwordd == registerViewModel.Passwordd))
                {
                    _isUserExists = true;
                    return new Tokens { IsUserExits = _isUserExists };
                }
                TableLogin tblLogin = new TableLogin();
                tblLogin.Username = registerViewModel.Username;
                tblLogin.Passwordd = registerViewModel.Passwordd;
                db.TableLogins.Add(tblLogin);
                db.SaveChanges();
            }
            else
            {
                _isAdmin = db.TableLogins.Any(x => x.Username == registerViewModel.Username && x.Passwordd == registerViewModel.Passwordd && x.IsAdmin == 1);
            }
            UserRecords = db.TableLogins.ToList().ToDictionary(x => x.Username, x => x.Passwordd);
            if (!UserRecords.Any(x => x.Key == registerViewModel.Username && x.Value == registerViewModel.Passwordd))
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenkey = Encoding.UTF8.GetBytes(configuration["JWT:Key"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] {
                new Claim(ClaimTypes.Name,registerViewModel.Username)
                }),
                Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenkey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new Tokens { Token = tokenHandler.WriteToken(token), IsAdmin = _isAdmin, IsUserExits = _isUserExists };
        }
    }
}
