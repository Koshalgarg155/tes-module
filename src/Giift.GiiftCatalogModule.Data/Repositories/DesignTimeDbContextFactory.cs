using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Giift.GiiftCatalogModule.Data.Repositories
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<GiiftCatalogModuleDbContext>
    {
        public GiiftCatalogModuleDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<GiiftCatalogModuleDbContext>();
            builder.UseSqlServer("Data Source=(local);Initial Catalog=VirtoCommerce66;Persist Security Info=True;MultipleActiveResultSets=True;Connect Timeout=30");
            return new GiiftCatalogModuleDbContext(builder.Options);
        }
    }
}
