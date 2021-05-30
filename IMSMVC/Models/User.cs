using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IMSMVC.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public Nullable<long> PhoneNumber { get; set; }
        public Nullable<byte> RoleId { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
    }
}