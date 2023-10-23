using EntityFrameworkCore.Triggers;
using Giift.GiiftCatalogModule.Data.Models;
using Microsoft.EntityFrameworkCore;
using VirtoCommerce.CatalogModule.Data.Repositories;

namespace Giift.GiiftCatalogModule.Data.Repositories;

public class GiiftCatalogModuleDbContext : CatalogDbContext
{
    private const string _discriminatorName = "Discriminator";
    public GiiftCatalogModuleDbContext(DbContextOptions<GiiftCatalogModuleDbContext> options)
        : base(options)
    {
    }

    protected GiiftCatalogModuleDbContext(DbContextOptions options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ExtendedCategoryItemRelationEntity>().HasDiscriminator<string>("Discriminator").HasValue(nameof(ExtendedCategoryItemRelationEntity));
        modelBuilder.Entity<ExtendedCategoryItemRelationEntity>().Property("Discriminator").HasMaxLength(128);
        modelBuilder.Entity<ExtendedCategoryItemRelationEntity>().Property(x => x.IsItemRelationActive).HasDefaultValue(true);
        base.OnModelCreating(modelBuilder);
    }
}
