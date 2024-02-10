using HandmadeShop.Context.Settings;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace HandmadeShop.Context.Factories;

public class DbContextOptionsFactory
{
    private const string MigrationProjectPrefix = "HandmadeShop.Context.Migrations.";

    public static DbContextOptions<AppDbContext> Create(string connStr, DbType dbType, bool detailedLogging = false)
    {
        var builder = new DbContextOptionsBuilder<AppDbContext>();

        Configure(connStr, dbType, detailedLogging).Invoke(builder);

        return builder.Options;
    }

    public static Action<DbContextOptionsBuilder> Configure(string connStr, DbType dbType, bool detailedLogging = false)
    {
        return builder =>
        {
            switch (dbType)
            {
                case DbType.MSSQL:
                    builder.UseSqlServer(connStr,
                        opts => opts
                            .CommandTimeout((int)TimeSpan.FromMinutes(10).TotalSeconds)
                            .MigrationsHistoryTable("_migrations")
                            .MigrationsAssembly($"{MigrationProjectPrefix}{DbType.MSSQL}")
                    );
                    break;

                case DbType.PgSql:
                    builder.UseNpgsql(connStr,
                        opts => opts
                            .CommandTimeout((int)TimeSpan.FromMinutes(10).TotalSeconds)
                            .MigrationsHistoryTable("_migrations")
                            .MigrationsAssembly($"{MigrationProjectPrefix}{DbType.PgSql}")
                    );
                    break;

                case DbType.MySql:
                    builder.UseMySql(connStr, ServerVersion.AutoDetect(connStr),
                        opts => opts
                            .CommandTimeout((int)TimeSpan.FromMinutes(10).TotalSeconds)
                            .SchemaBehavior(MySqlSchemaBehavior.Ignore)
                            .MigrationsHistoryTable("_migrations")
                            .MigrationsAssembly($"{MigrationProjectPrefix}{DbType.MySql}")
                    );
                    break;
            }

            if (detailedLogging)
            {
                builder.EnableSensitiveDataLogging();
            }

            // Attention!
            // It possible to use or LazyLoading or NoTracking at one time
            // Together this features don't work

            builder.UseLazyLoadingProxies(true);
            //bldr.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTrackingWithIdentityResolution);
        };
    }
}