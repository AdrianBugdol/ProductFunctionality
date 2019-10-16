using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Product.Models
{
    public class ProductUpdateInputModel
    {
        [Required]
        [Key]
        public Guid Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set;}
        [Required]
        public decimal Price { get; set; }
    }
}
