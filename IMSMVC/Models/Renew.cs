using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IMSMVC.Models
{
    public class Renew
    {
        int buyPolicyId { get; set; }
        double Anount { get; set; }
        int policyId { get; set; }
        DateTime ActualPremiumDate { get; set; }
    }
}