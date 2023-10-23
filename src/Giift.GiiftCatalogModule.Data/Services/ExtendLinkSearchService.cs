using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Giift.GiiftCatalogModule.Core.Models;
using Giift.GiiftCatalogModule.Data.Models;
using VirtoCommerce.CatalogModule.Core.Model;
using VirtoCommerce.CatalogModule.Core.Services;
using VirtoCommerce.CatalogModule.Data.Model;
using VirtoCommerce.CatalogModule.Data.Repositories;
using VirtoCommerce.CatalogModule.Data.Search;
using VirtoCommerce.Platform.Core.Common;

namespace Giift.GiiftCatalogModule.Data.Services
{
    public class ExtendLinkSearchService : LinkSearchService
    {
        public ExtendLinkSearchService(Func<ICatalogRepository> catalogRepositoryFactory, ICategoryService categoryService, ICatalogService catalogService) : base(catalogRepositoryFactory, categoryService, catalogService)
        {
        }

        protected override CategoryLink ToCategoryLink(CategoryItemRelationEntity categoryRelation)
        {
            var entry = base.ToCategoryLink(categoryRelation);
            if(entry is ExtendedCategoryLink extendedCategoryLink && categoryRelation is ExtendedCategoryItemRelationEntity extendedCategoryItemRelation )
            {
                extendedCategoryLink.IsItemRelationActive = extendedCategoryItemRelation.IsItemRelationActive;
            }

            return entry;
        }
    }
}
