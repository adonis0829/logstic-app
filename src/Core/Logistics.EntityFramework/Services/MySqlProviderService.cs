﻿using System.Data.Common;
using Microsoft.Extensions.Logging;
using MySqlConnector;
using Logistics.Domain.Options;
using Logistics.Domain.Services;
using Logistics.Domain.Shared;

namespace Logistics.EntityFramework.Services;

public class MySqlProviderService : IDatabaseProviderService
{
    private readonly TenantsSettings _settings;
    private readonly ILogger<MySqlProviderService> _logger;

    public MySqlProviderService(
        TenantsSettings settings,
        ILogger<MySqlProviderService> logger)
    {
        _logger = logger;
        _settings = settings;
    }

    public string GenerateConnectionString(string databaseName)
    {
        return $"Server={_settings.DatabaseHost}; Database={databaseName}; Uid={_settings.DatabaseUserId}; Pwd={_settings.DatabasePassword}";
    }

    public async Task<bool> CreateDatabaseAsync(string connectionString)
    {
        try
        {
            await using var databaseContext = new TenantDbContext(connectionString);
            await databaseContext.Database.MigrateAsync();
            await AddTenantRoles(databaseContext);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError("Thrown exception in MySqlProviderService.CreateDatabaseAsync(): {Exception}", ex);
            return false;
        }
    }

    public async Task<bool> DeleteDatabaseAsync(string connectionString)
    {
        try
        {
            var connection = new DbConnectionStringBuilder
            {
                ConnectionString = connectionString
            };

            var database = connection["Initial Catalog"];
            var dropQuery = $"DROP DATABASE '{database}'";
            await using var mySqlCommand = new MySqlCommand(dropQuery);
            await mySqlCommand.ExecuteScalarAsync();
            return true;
        }
        catch (DbException ex)
        {
            _logger.LogError("Thrown exception in MySqlProviderService.DeleteDatabaseAsync(): {@Exception}", ex);
            return false;
        }
    }

    private async Task AddTenantRoles(DbContext context)
    {
        foreach (var tenantRole in TenantRoles.GetValues())
        {
            var role = new TenantRole(tenantRole.Value)
            {
                DisplayName = tenantRole.DisplayName
            };

            var existingRole = await context.Set<TenantRole>().FirstOrDefaultAsync(i => i.Name == role.Name);
            if (existingRole != null)
                continue;

            context.Set<TenantRole>().Add(role);
        }

        await context.SaveChangesAsync();
    }
}
