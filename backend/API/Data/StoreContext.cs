using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class StoreContext(DbContextOptions<StoreContext> options) : DbContext(options)
    {
        public DbSet<Authentication.Models.SignInRequest> Users { get; set; }
    }
}