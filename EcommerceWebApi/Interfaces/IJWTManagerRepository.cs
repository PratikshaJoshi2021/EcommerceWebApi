using EcommerceWebApi.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceWebApi.Interfaces
{
    interface IJWTManagerRepository
    {
        Tokens Authenicate(ViewModels.LoginViewModel users, bool IsRegister);
    }
}
