using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IMSMVC.Models
{
    public class Role
    {
        public byte Id { get; set; }
        public string Name { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
    }
}