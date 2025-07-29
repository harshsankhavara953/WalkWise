using Microsoft.EntityFrameworkCore;
using ShoeStoreMVC.Models;

namespace ShoeStoreMVC.Services
{
    public class ApplicatinoDbConext:DbContext
    {
        public ApplicatinoDbConext(DbContextOptions options):base(options)
        {

        }
        public DbSet<Product> products { get; set; }
    }
}
