using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IMSMVC.Models
{
    public class PoliciesClaim
    {
        public int Id { get; set; }
        public Nullable<int> UserId { get; set; }
        public Nullable<int> PolicyId { get; set; }
        public Nullable<double> ClaimAmount { get; set; }
        public string RequiredDocuments { get; set; }
        public Nullable<int> ClaimStatusID { get; set; }
        public string Reason { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
    }
}