using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Giift.GiiftCatalogModule.Core;
using VirtoCommerce.CatalogModule.Core.Model;
using VirtoCommerce.CatalogModule.Data.Authorization;
using System.Threading.Tasks;
using System.Linq;
using VirtoCommerce.Platform.Core.Common;
using System.Collections.Generic;
using Giift.GiiftCatalogModule.Core.Models;
using VirtoCommerce.CatalogModule.Core.Services;
using VirtoCommerce.CatalogModule.Data.Services;
using VirtoCommerce.CoreModule.Core.Seo;

namespace Giift.GiiftCatalogModule.Web.Controllers.Api
{
    [Route("api/listentrylink")]
    public class GiiftCatalogModuleController : Controller
    {
        private readonly IItemService _itemsService;
        private readonly IAuthorizationService _authorizationService;
        private readonly ICatalogService _catalogService;
        public GiiftCatalogModuleController(IItemService itemService, IAuthorizationService authorizationService, ICatalogService catalogService)
        {
            _itemsService = itemService;
            _authorizationService = authorizationService;
            _catalogService = catalogService;
        }

        // GET: api/giift-catalog-module
        /// <summary>
        /// Get message
        /// </summary>
        /// <remarks>Return "Hello world!" message</remarks>
        [HttpPut]
        [Route("update")]
        [Authorize(ModuleConstants.Security.Permissions.Read)]
        public async Task<ActionResult> UpdateLinksStatus([FromBody] ExtendedCategoryLink[] links)
        {
            var entryIds = links.Select(x => x.EntryId).ToList();
            var authorizationResult = await _authorizationService.AuthorizeAsync(User, links, new CatalogAuthorizationRequirement(ModuleConstants.Security.Permissions.Update));
            if (!authorizationResult.Succeeded)
            {
                return Unauthorized();
            }
            
            var products = await _itemsService.GetAsync(entryIds, ItemResponseGroup.Full.ToString());

            foreach (var product in products)
            {
                foreach(var link in links)
                {
                    var productLink = product.Links.FirstOrDefault(p => p.CatalogId == link.CatalogId && p.CategoryId == link.CategoryId && p.EntryId == link.EntryId);
                    if(productLink != null && productLink is ExtendedCategoryLink extendedCategoryLink)
                    {
                        extendedCategoryLink.IsItemRelationActive = link.IsItemRelationActive;
                    }
                }
            }

            await InnerSaveProducts(products.ToArray());

            return Ok();
        }

        private async Task<CatalogProduct[]> InnerSaveProducts(CatalogProduct[] products)
        {
            var toSaveList = new List<CatalogProduct>();
            var catalogs = await _catalogService.GetAsync(products.Select(pr => pr.CatalogId).Distinct().ToList());
            foreach (var product in products)
            {
                if (product.IsTransient() && product.SeoInfos.IsNullOrEmpty())
                {
                    var slugUrl = GenerateProductDefaultSlugUrl(product);
                    if (!string.IsNullOrEmpty(slugUrl))
                    {
                        var catalog = catalogs.FirstOrDefault(c => c.Id.EqualsInvariant(product.CatalogId));
                        var defaultLanguageCode = catalog?.Languages.First(x => x.IsDefault).LanguageCode;
                        var seoInfo = AbstractTypeFactory<SeoInfo>.TryCreateInstance();
                        seoInfo.LanguageCode = defaultLanguageCode;
                        seoInfo.SemanticUrl = slugUrl;
                        product.SeoInfos = new[] { seoInfo };
                    }
                }

                toSaveList.Add(product);
            }

            if (!toSaveList.IsNullOrEmpty())
            {
                await _itemsService.SaveChangesAsync(toSaveList.ToArray());
            }

            return toSaveList.ToArray();
        }
        private string GenerateProductDefaultSlugUrl(CatalogProduct product)
        {
            var retVal = new List<string>
            {
                product.Name
            };
            if (product.Properties != null)
            {
                //foreach (var property in product.Properties.Where(x => x.Type == PropertyType.Variation && x.Values != null))
                //{
                //    retVal.AddRange(property.Values.Select(x => x.PropertyName + "-" + x.Value));
                //}
            }
            return string.Join(" ", retVal).GenerateSlug();
        }
    }
}
