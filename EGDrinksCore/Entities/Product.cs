using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EGDrinksCore.Entities
{
    public class Product
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Product name is required")]
        [StringLength(100, ErrorMessage = "Product name cannot be longer than 100 characters")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Product Description is required")]
        [StringLength(100, ErrorMessage = "Product Descriptino cannot be longer than 100 characters")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Product Category is required")]
        [StringLength(100, ErrorMessage = "Product Category cannot be longer than 100 characters")]
        public string Category { get; set; }

        [Range(0.01, 500000, ErrorMessage = "Price must be between 0.01 and 500000")]
        public decimal Price { get; set; }
    }
}
