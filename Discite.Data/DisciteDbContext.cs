using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discite.Data.Models;

namespace Discite.Data
{
    public class DisciteDbContext: DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connString = ConfigurationManager.ConnectionStrings["project_discite"]?.ConnectionString;

#if DEBUG
            if (string.IsNullOrWhiteSpace(connString))
                optionsBuilder.UseMySql(
                    "server=localhost;database=project_discite;uid=root;pwd=;",
                    ServerVersion.Create(10, 4, 24, ServerType.MariaDb)
                );
            else
#endif
                optionsBuilder.UseMySql(connString, ServerVersion.AutoDetect(connString));
        }

        public DbSet<UserModel> Users { get; set; }
       
    }
}
