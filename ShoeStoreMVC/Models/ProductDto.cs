using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ShoeStoreMVC.Models
{
    public class ProductDto
    {
        [Required,MaxLength(50)]

        public string Name { get; set; } = "";
        [Required,MaxLength(50)]

        public string Brand { get; set; } = "";
        [Required,MaxLength(50)]
            
        public string Category { get; set; } = "";
        [Required]
        public decimal Price { get; set; }
        [Required]
        public string Description { get; set; } = "";

        public IFormFile ? ImageFile { get; set; } 
    }
}
