using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Data.Context
{
    public class DbContextFactory : IDesignTimeDbContextFactory<DataContext>
    {
        public DataContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
            optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\yaaru\\Documents\\Database\\Project\\ProjectManagement\\Data\\Database\\DatabaseProjectManagement.mdf;Integrated Security=True;Connect Timeout=30");

            return new DataContext(optionsBuilder.Options);
        }
    }
}
