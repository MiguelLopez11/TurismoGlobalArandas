using Microsoft.EntityFrameworkCore;
using TurismoGlobalArandas.Models;

namespace TurismoGlobalArandas.Context
{
    public class TurismoGlobalContext :DbContext
    {
        public TurismoGlobalContext(DbContextOptions<TurismoGlobalContext> options) : base(options)
        {
        }
        public DbSet<Employees> Employees { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }

}
