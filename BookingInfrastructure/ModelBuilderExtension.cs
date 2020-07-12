using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace BookingInfrastructure
{
    public static class ModelBuilderExtension
    {


        ///// <summary>
        ///// Sets the snake case naming convention for table names, column names, foregin keys and index names.
        ///// </summary>
        ///// <param name="builder">The builder.</param>
        //public static void SetSnakeCaseNamingConvention(this ModelBuilder builder)
        //{
        //    foreach (var entity in builder.Model.GetEntityTypes())
        //    {
        //        // Replace table names
        //        entity.Relational().TableName = entity.Relational().TableName.ToSnakeCase();

        //        // Replace column names            
        //        foreach (var property in entity.GetProperties())
        //        {
        //            property.Relational().ColumnName = property.Name.ToSnakeCase();
        //        }

        //        foreach (var key in entity.GetKeys())
        //        {
        //            key.Relational().Name = key.Relational().Name.ToSnakeCase();
        //        }

        //        foreach (var key in entity.GetForeignKeys())
        //        {
        //            key.Relational().Name = key.Relational().Name.ToSnakeCase();
        //        }

        //        foreach (var index in entity.GetIndexes())
        //        {
        //            index.Relational().Name = index.Relational().Name.ToSnakeCase();
        //        }
        //    }
        //}

        /// <summary>
        /// Apply all configurtions to model builder.
        /// </summary>
        /// <param name="builder">The builder.</param>
        public static void LoadAllEntityConfigurations(this ModelBuilder builder)
        {
            var dalAssembly = Assembly.GetAssembly(typeof(ModelBuilderExtension));
            var configurations = dalAssembly.GetTypes()
                .Where(type => type.GetInterfaces().Any(
                    i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>))).ToList();

            foreach (var confguration in configurations)
            {
                dynamic configurationInstance = Activator.CreateInstance(confguration);
                builder.ApplyConfiguration(configurationInstance);
            }
        }

        ///// <summary>
        ///// Adds audit columns to all entites(created at, update at,deleted and legacy id)
        ///// </summary>
        ///// <param name="builder">The builder.</param>
        //public static void SetAuditColumns(this ModelBuilder builder)
        //{
        //    foreach (var entity in builder.Model.GetEntityTypes())
        //    {
        //        builder.Entity(entity.Name).Property<DateTime>(AuditColumnNames.CREATED_PROPERTY_NAME);
        //        builder.Entity(entity.Name).Property<DateTime?>(AuditColumnNames.MODIFIED_PROPERTY_NAME);
        //        builder.Entity(entity.Name).Property<bool>(AuditColumnNames.DELETED_PROPERTY_NAME);
        //    }
        //}

        /// <summary>
        /// Removes the cascade delete from all entites.
        /// </summary>
        /// <param name="modelBuilder">The model builder.</param>
        public static void RemoveCascadeDelete(this ModelBuilder modelBuilder)
        {
            var cascadeFKs = modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascadeFKs)
            {
                fk.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }

        /// <summary>
        /// Look up in all tracked models for <see cref="Attributes.UniqueAttribute"/> and set up unique constraints
        /// </summary>
        /// <param name="modelBuilder">The model builder.</param>
        //public static void SetUpUniqueColumns(this ModelBuilder modelBuilder)
        //{
        //    var entityTypes = modelBuilder.Model.GetEntityTypes().Select(p => p.ClrType);

        //    foreach (var entityType in entityTypes)
        //    {
        //        Dictionary<string, List<PropertyInfo>> uniqueProperties = entityType.FetchUniqeProperties();
        //        if (uniqueProperties != null && uniqueProperties.Any())
        //        {
        //            modelBuilder.SetUniqueColumnsForEntity(uniqueProperties, entityType);
        //        }
        //    }
        //}

        //private static void SetUniqueColumnsForEntity(this ModelBuilder modelBuilder, Dictionary<string, List<PropertyInfo>> uniqueProperties, Type entityType)
        //{
        //    //set simple unique constraints
        //    if (uniqueProperties.ContainsKey(string.Empty))
        //    {
        //        var properties = uniqueProperties[string.Empty];
        //        foreach (var property in properties)
        //        {
        //            modelBuilder.Entity(entityType).HasIndex(property.Name).IsUnique();
        //        }
        //    }

        //    //set compund unique constraints
        //    foreach (var key in uniqueProperties.Keys.Where(p => !string.IsNullOrEmpty(p)))
        //    {
        //        var columns = uniqueProperties[key].Select(p => p.Name).ToArray();
        //        modelBuilder.Entity(entityType).HasIndex(columns).IsUnique();
        //    }
        //}

        /// <summary>
        /// Sets the default value for identifier.
        /// </summary>
        /// <param name="builder">The builder.</param>
        //public static void SetDefaultValueForId(this ModelBuilder builder)
        //{
        //    //skip tables from identity core which have other PK
        //    List<string> tabelsToSkip = new List<string> { "IdentityRoleClaim", "IdentityUserClaim" };

        //    foreach (var entity in builder.Model.GetEntityTypes())
        //    {
        //        if (!tabelsToSkip.Any(p => entity.ClrType.Name.Contains(p)))
        //        {
        //            // builder.Entity(entity.Name).Property<long>(nameof(BaseEntity.Id)).ValueGeneratedOnAdd();
        //            // builder.Entity(entity.Name).Property<long>(nameof(BaseEntityBasic.Id)).ValueGeneratedOnAdd();
        //        }
        //    }
        //}
    }
}
    
