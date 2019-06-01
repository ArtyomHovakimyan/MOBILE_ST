using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Mic.Volo.MOBILE_ST.Data.Models
{
    public class ShoppingCartItem
    {
        public int Id { get; set; }

        public int Qty { get; set; }

        public int CakeId { get; set; }

        public Phone Phone { get; set; }

        [Required]
        [StringLength(255)]
        public string ShoppingCartId { get; set; }
    }
}
