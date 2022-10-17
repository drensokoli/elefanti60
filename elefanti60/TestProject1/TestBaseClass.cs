using elefanti60.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace TestProject1
{
    public class TestBaseClass
    {
        public AppDbContext _appDbContext;
        public TestBaseClass()
        {
            var builder = new ConfigurationBuilder()
                       .SetBasePath(Directory.GetCurrentDirectory())
                       .AddJsonFile("appSettings.json", optional: true, reloadOnChange: true);
            IConfiguration _configuration = builder.Build();
            var dbConnectionString = _configuration.GetConnectionString("SqlServer");

            _appDbContext = new AppDbContext(new DbContextOptionsBuilder<AppDbContext>()
                                .UseSqlServer(dbConnectionString)
                                .Options);
        }
    }
}
