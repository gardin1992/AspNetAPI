using Microsoft.EntityFrameworkCore;

namespace WebApplication3.Models
{
    public class FuncionarioContext : DbContext
    {
        public FuncionarioContext(DbContextOptions<FuncionarioContext> options)
            : base(options)
        {
        }

        public DbSet<FuncionarioItem> TodoItems { get; set; }
    }
}
