using Microsoft.EntityFrameworkCore;
using short_clips_web_api.Models;
using System.Reflection;

namespace short_clips_web_api
{
    public class AppDbContext : DbContext
    {
        DbSet<Video> Videos { get; set; }
        DbSet<Category> Categories {  get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
