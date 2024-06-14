using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace APIUniformes.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<SizeTable> Size => Set<SizeTable>();
        public DbSet<UserTable> Usuario => Set<UserTable>();
        public DbSet<GarmentTable> Garment => Set<GarmentTable>();
        public DbSet<SchoolTable> School => Set<SchoolTable>();

    }


}