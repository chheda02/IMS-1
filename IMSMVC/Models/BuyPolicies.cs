using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IMSMVC.Models
{
    public class BuyPolicies
    {
        public int Id { get; set; }
        public Nullable<int> UserId { get; set; }
        public Nullable<int> PolicyId { get; set; }
        public Nullable<int> PolicyCategoryId { get; set; }
        public Nullable<double> AmountPaid { get; set; }
        public string RequiredDocument { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
    }
}