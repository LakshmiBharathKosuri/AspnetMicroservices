using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ordering.API.Extensions
{
   
    public static class HostExtensions
    {
        // IHost is from using Microsoft.Extensions.Hosting
        // MigrateDatabase method is generic type of extension, which uses the TContext and also seeder 
        // action with TContext & IServiceProvider.

        //where TContext is DbContext from entity frame work core.
        public static IHost MigrateDatabase<TContext>(this IHost host,
                                            Action<TContext, IServiceProvider> seeder,
                                            int? retry = 0) where TContext : DbContext
        {
            //to retry the mechanism of migrating DB
            int retryForAvailability = retry.Value;

            //CreateScope is from 'using Microsoft.Extensions.DependencyInjection'
            //ILogger is from using Microsoft.Extensions.Logging
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var logger = services.GetRequiredService<ILogger<TContext>>();
                var context = services.GetService<TContext>();

                try
                {
                    logger.LogInformation("Migrating database associated with context {DbContextName}", typeof(TContext).Name);

                    InvokeSeeder(seeder, context, services);

                    logger.LogInformation("Migrated database associated with context {DbContextName}", typeof(TContext).Name);
                }
                //SqlException is from 'using Microsoft.Data.SqlClient'
                catch (SqlException ex)
                {
                    logger.LogError(ex, "An error occurred while migrating the database used on context {DbContextName}", typeof(TContext).Name);

                    if (retryForAvailability < 50)
                    {
                        retryForAvailability++;
                        System.Threading.Thread.Sleep(2000);
                        MigrateDatabase<TContext>(host, seeder, retryForAvailability);
                    }
                }
            }
            return host;
        }

        //InvokeSeeder is will migrate the db and then seed the data.
        private static void InvokeSeeder<TContext>(Action<TContext, IServiceProvider> seeder,
                                                    TContext context,
                                                    IServiceProvider services)
                                                    where TContext : DbContext
        {
            /* context.Database.Migrate() will look for the migrations in application and then
               execute the migration scripts Up and Build model methods from Migration folder scripts.
            */
            context.Database.Migrate();

            /*
             Once the migration is completed, seeder method will perfrom the action of seeding the 
             data into the database tables.
             */
            seeder(context, services);
        }
    }
}
