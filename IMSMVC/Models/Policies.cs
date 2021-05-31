using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IMSMVC.Models
{
    public class Policies
    {
        public int Id { get; set; }
        public Nullable<byte> CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Nullable<int> DurationInMonths { get; set; }
        public Nullable<double> PremiumAmount { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
    }
}