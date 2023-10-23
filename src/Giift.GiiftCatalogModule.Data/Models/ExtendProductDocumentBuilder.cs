using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Giift.GiiftCatalogModule.Core.Models;
using VirtoCommerce.CatalogModule.Core.Model;
using VirtoCommerce.CatalogModule.Core.Search;
using VirtoCommerce.CatalogModule.Core.Services;
using VirtoCommerce.CatalogModule.Data.Search.Indexing;
using VirtoCommerce.Platform.Core.Settings;
using VirtoCommerce.SearchModule.Core.Extenstions;
using VirtoCommerce.SearchModule.Core.Model;

namespace Giift.GiiftCatalogModule.Data.Models
{
    public class ExtendProductDocumentBuilder : ProductDocumentBuilder
    {
        public ExtendProductDocumentBuilder(ISettingsManager settingsManager, IItemService itemService, IProductSearchService productsSearchService) : base(settingsManager, itemService, productsSearchService)
        {
        }

        protected override IndexDocument CreateDocument(CatalogProduct product)
        {
            var doc = base.CreateDocument(product);
            var linksDoc = new List<string>();
            if(product.Links != null)
            {
                foreach(var link in product.Links)
                {
                    if(link is ExtendedCategoryLink docLink)
                    {
                        var key = string.IsNullOrEmpty(docLink.CategoryId) ? docLink.CatalogId : $"{docLink.CatalogId}_{docLink.CategoryId}";
                        linksDoc.Add($"{key}:{docLink.IsItemRelationActive}");
                    }
                }
                doc.AddFilterableValues("linkStatus",linksDoc);
            }
            return doc;
        }
    }
}
