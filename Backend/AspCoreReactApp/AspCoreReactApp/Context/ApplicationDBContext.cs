using AspCoreReactApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace AspCoreReactApp.Context
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

        }

        public  DbSet<ProductEntity> Products { get; set; }

    }
}
