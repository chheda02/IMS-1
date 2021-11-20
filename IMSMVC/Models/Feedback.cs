using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IMSMVC.Models
{
    public class Feedback
    {
        public int Id { get; set; }
        [DisplayName("Email Id")]
        [Required(ErrorMessage = "Required Email Id")]
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Please enter Valid Email Id")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Required Message")]
        public string Message { get; set; }
        [DisplayName("Created Date")]
        public Nullable<System.DateTime> CreatedDate { get; set; }
    }
}