# Alicazum.Foundation.SqlServer

## 1) Infrastructure.DbContext

### 1.1) DataEntityDbContextBase

Base class to inherit from when composing the DbContext.

| Property | Description |
| ----------- | ----------- |
| abstract bool HasAuditTrail | Indicates if the context has audit trail |
| abstract bool HasPolicyChecks | Indicates if the context has policy checks |

If **HasAuditTrail** is true, an interception of type *SoftDeleteInterceptor* will be included on the interceptor collection of the context. If **HasPolicyChecks** is true, then the default *IMigrationsSqlGenerator* is replaced by *CustomSqlServerMigrationsSqlGenerator*.

### 1.2) DbSecurityContextProviderBase

This class provides a base for a security provider that is necessary to work with audit trail and policy checks.

| Method | Description |
| ----------- | ----------- |
| abstract string GetUserId() | Returns current user ID |
| abstract string UserNameOrProcess | Returns current user name or process name |

Do not forget to register an implementation using this base:

```
public static IServiceCollection AddMasterDbContext(this IServiceCollection services) 
{
    services.AddSingleton<IDbSecurityContextProvider, MasterDbContextSecurityProvider>();
    services.AddScoped<MasterDbContext>();
    return services;
}
```

## 2) Infrastructure.DbContext.Configurations

Base clases to help with entity configuration.

### 2.1) DataEntityTypeConfigurationBase

Base class for regular data entity configurations, registers the Id property of the entity as Primary Key.

### 2.2) AuditableDataEntityTypeConfigurationBase

Base class for a data entity configuration including 