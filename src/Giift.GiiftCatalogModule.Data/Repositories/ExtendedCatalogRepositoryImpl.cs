using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Giift.GiiftCatalogModule.Data.Models;
using VirtoCommerce.CatalogModule.Data.Model;
using VirtoCommerce.CatalogModule.Data.Repositories;
namespace Giift.GiiftCatalogModule.Data.Repositories
{
    public class ExtendedCatalogRepositoryImpl : CatalogRepositoryImpl
    {
        public IQueryable<ExtendedCategoryItemRelationEntity> ExtendedCategoryItemRelations => DbContext.Set<ExtendedCategoryItemRelationEntity>();
        public ExtendedCatalogRepositoryImpl(GiiftCatalogModuleDbContext dbContext) : base(dbContext)
        {
        }
    }
}
