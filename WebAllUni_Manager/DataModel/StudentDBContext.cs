using ClassLibrary;
using Microsoft.EntityFrameworkCore;

namespace WebAllUni_Manager.DataModel
{

    public class StudentDBContext : DbContext
    {

        public StudentDBContext(DbContextOptions<StudentDBContext> options) : base(options)
        {

        }

        public DbSet<StudentEF> StudentEF { get; set; }

    }

}