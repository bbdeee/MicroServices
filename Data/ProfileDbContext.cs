using MicroServicesProject.Models;
using Microsoft.EntityFrameworkCore;

namespace MicroServicesProject.Data
{
    public class ProfileContext : DbContext
    {
        public DbSet<Profile> Profiles { get; set; }
        public ProfileContext(DbContextOptions options) : base(options)
        {

        }

    }
}

