using AspNetMVCFileUpload.Entities;
using System.Data.Entity;

namespace AspNetMVCFileUpload
{
    public class MainContext : DbContext
    {
        public DbSet<Test> Test { get; set; }
    }
}