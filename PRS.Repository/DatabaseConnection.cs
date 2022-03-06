using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace PRS.Repository
{
    public class DatabaseConnection
    {

        public static IConfiguration connectionConfiguration
        {
            get
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetParent(Directory.GetCurrentDirectory()).ToString())
                    .AddJsonFile("dbsettings.json")
                    .Build();
                return configuration;
            }
        }

        public static Action<DbContextOptionsBuilder> contextOptions
        {
            get
            {
                DbContextOptionsBuilder contextOptionsBuilder = new DbContextOptionsBuilder();
                string strDBEngine = connectionConfiguration.GetSection("DatabaseSettings").GetSection("DBEngine").Value;
                int timeout = int.Parse(connectionConfiguration.GetSection("DatabaseSettings").GetSection("Timeout").Value);
                if (strDBEngine == "postgresql")
                {
                    return new Action<DbContextOptionsBuilder>(options => options.UseNpgsql(DatabaseConnection.connectionConfiguration
                        .GetConnectionString("PostgreSQLConnection"), options => options.CommandTimeout(timeout)).UseLazyLoadingProxies());
                }
                else if (strDBEngine == "oracle")
                {
                    return new Action<DbContextOptionsBuilder>(options => options.UseOracle(DatabaseConnection.connectionConfiguration
                        .GetConnectionString("OracleConnection"), options => options.CommandTimeout(timeout)).UseLazyLoadingProxies());
                }
                else if (strDBEngine == "sqlServer")
                {
                    return new Action<DbContextOptionsBuilder>(options => options.UseSqlServer(DatabaseConnection.connectionConfiguration
                        .GetConnectionString("SQLServerConnection"), options => options.CommandTimeout(timeout)).UseLazyLoadingProxies());
                }
                else
                {
                    return new Action<DbContextOptionsBuilder>(options => options.UseMySQL(DatabaseConnection.connectionConfiguration
                        .GetConnectionString("MySQLConnection"), options => options.CommandTimeout(timeout)).UseLazyLoadingProxies());
                }
            }
        }

    }
}
