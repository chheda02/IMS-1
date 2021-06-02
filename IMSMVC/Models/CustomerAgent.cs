using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IMSMVC.Models
{
    public class CustomerAgent
    {
        public int Id { get; set; }
        public Nullable<int> AgentId { get; set; }
        public Nullable<int> UserId { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
    }
}