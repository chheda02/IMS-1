using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IMSMVC.Models
{
    public class PoliciesTransactions
    {
        public int Id { get; set; }
        public Nullable<int> UserId { get; set; }
        public Nullable<int> PolicyId { get; set; }
        public Nullable<double> Amount { get; set; }
        public Nullable<System.DateTime> ActualPremiumDate { get; set; }
        public Nullable<System.DateTime> ActualPaymentDate { get; set; }
    }
}