using AnadoluPrmPracticum.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace AnadoluPrmPracticum.Extentions
{
    public static class DbContextExtention
    {
        public static void AddDbContextDI(this IServiceCollection services, IConfiguration configuration)
        {
            var dbtype = configuration.GetConnectionString("DbType");
            if (dbtype == "SQL")
            {
                var dbConfig = configuration.GetConnectionString("DefaultConneciton");
                services.AddDbContext<AppDbContext>(options => options
                   .UseSqlServer(dbConfig)
                   );
            }
            else if (dbtype == "PostgreSQL")
            {
                var dbConfig = configuration.GetConnectionString("PostgreSqlConnection");
                services.AddDbContext<AppDbContext>(options => options
                   .UseNpgsql(dbConfig)
                   );
            }
        }
    }
}
