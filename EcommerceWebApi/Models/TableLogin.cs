using System;
using System.Collections.Generic;

#nullable disable

namespace EcommerceWebApi.Models
{
    public partial class TableLogin
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Passwordd { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? TypeId { get; set; }
    }
}
