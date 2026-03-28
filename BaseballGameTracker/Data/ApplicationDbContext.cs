using BaseballGameTracker.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace BaseballGameTracker.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole {
                    Id = "3049e059-9ea9-467f-9c30-3c3a07eaff79",
                    Name = "Fan", 
                    NormalizedName = "FAN"
                },
                new IdentityRole {
                    Id = "44513cbe-994c-402a-a163-9eaff914d035",
                    Name = "Administrator",
                    NormalizedName = "ADMINISTRATOR"
                });

            var hasher = new PasswordHasher<IdentityUser>();

            builder.Entity<IdentityUser>().HasData(new IdentityUser
            {

                Id = "30a785c6-45f3-4381-8c61-a302f6f8c65f",
                Email = "baseballtracker03@gmail.com",
                NormalizedEmail = "BASEBALLTRACKER03@GMAIL.COM",
                NormalizedUserName = "BASEBALLTRACKER03@GMAIL.COM",
                UserName = "BASEBALLTRACKER03@GMAIL.COM",
                PasswordHash = hasher.HashPassword(null, "Boone0425!"),
                EmailConfirmed = true

            });

            builder.Entity<IdentityUserRole<string>>().HasData(

                new IdentityUserRole<string>
                {
                    RoleId = "44513cbe-994c-402a-a163-9eaff914d035", 
                    UserId = "30a785c6-45f3-4381-8c61-a302f6f8c65f"
                }); 

        }


        public DbSet<BaseballGameTracker.Data.Game> Game { get; set; } = default!;
        public DbSet<BaseballGameTracker.Data.Email> Email { get; set; } = default!;



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .ConfigureWarnings(w => w.Ignore(RelationalEventId.PendingModelChangesWarning));
        }
    }
}
