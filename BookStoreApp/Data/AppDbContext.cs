using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace BookStoreApp.Data{
    public class AppDbContext:IdentityDbContext<IdentityUser>{
        private readonly IOptions<ConnectionStringModel> _ConnectionString;

        public AppDbContext(DbContextOptions<AppDbContext> options,IOptions<ConnectionStringModel> ConnectionString): base(options){
            _ConnectionString=ConnectionString;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_ConnectionString.Value.DefaultConnection);
            base.OnConfiguring(optionsBuilder);
            
        }

    }
}