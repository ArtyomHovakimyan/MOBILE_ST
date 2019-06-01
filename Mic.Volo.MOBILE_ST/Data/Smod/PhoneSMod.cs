using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Mic.Volo.MOBILE_ST.Data.Smod
{
    public class PhoneSMod
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Phone Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Short Description")]
        [MaxLength(50)]
        public string ShortDescription { get; set; }

        [Required]
        [Display(Name = "Long Description")]
        [MaxLength(255)]
        public string LongDescription { get; set; }

        [Required]
        [Display(Name = "Price")]
        public decimal Price { get; set; }

        [Required]
        [Display(Name = "Image Url")]
        public string ImageUrl { get; set; }

        [Display(Name = "Is Phone Of the Week? ")]
        public bool IsCakeOfTheWeek { get; set; }

        [Display(Name = "Company")]
        public int CompanyId { get; set; }
    }
}
