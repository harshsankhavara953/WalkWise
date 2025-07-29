using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ShoeStoreMVC.Models
{
    public class Product
    {
         public int Id { get; set;}
        [MaxLength(50)]
        public string name { get; set;} = "";
        [MaxLength(50)]

        public string Brand { get; set;} = "";
        [MaxLength(50)]

        public string category { get; set;} = "";

        [Precision(16,2)]
        public decimal Price { get; set; }

        public string Description { get; set; } = "";

        public string ImageFileName { get; set; } = "";
        public DateTime CreateAt { get; set; }
            
    }
}
