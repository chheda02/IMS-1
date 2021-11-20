using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IMSMVC.Models
{
    public class Policies
    {
        public int Id { get; set; }
        [DisplayName("Category Id")]
        [Required(ErrorMessage = "Required Category Id")]
        public Nullable<byte> CategoryId { get; set; }
        [DisplayName("Name")]
        [Required(ErrorMessage = "Required Name")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Name Must be Minimum 2 Charaters")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Required Description")]
        public string Description { get; set; }
        [DisplayName("Duration in months")]
        [Required(ErrorMessage = "Required Duration in months")]
        public Nullable<int> DurationInMonths { get; set; }
        [DisplayName("Premium Amount")]
        [Required(ErrorMessage = "Required Premium Amount")]
        public Nullable<double> PremiumAmount { get; set; }
        [DisplayName("Created Date")]
        public Nullable<System.DateTime> CreatedDate { get; set; }
    }
}