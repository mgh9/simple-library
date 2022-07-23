using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace FinLib.DataLayer.Context
{
    /// <summary>
    /// Only used by EF Tooling
    /// </summary>
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<AppDbContext>();

            var configuration = new ConfigurationBuilder()
                      .SetBasePath(Directory.GetCurrentDirectory())
                      .AddJsonFile("appsettings.Development.json")
                      .Build();

            var connectionString = configuration.GetConnectionString("FinLib");
                //Models.Constants.Database.ConnectionStringNames.IdentityProvider);
            Console.WriteLine($"getting connectionString {connectionString} done.");

            builder.UseSqlServer(connectionString);

            return new AppDbContext(builder.Options);
        }
    }
}
