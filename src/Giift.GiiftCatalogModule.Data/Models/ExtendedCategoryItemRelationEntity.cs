using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Giift.GiiftCatalogModule.Core.Models;
using Serialize.Linq.Nodes;
using VirtoCommerce.CatalogModule.Core.Model;
using VirtoCommerce.CatalogModule.Data.Model;
using VirtoCommerce.Platform.Core.Common;
using VirtoCommerce.Platform.Core.Domain;

namespace Giift.GiiftCatalogModule.Data.Models
{
    public class ExtendedCategoryItemRelationEntity : CategoryItemRelationEntity, IDataEntity<CategoryItemRelationEntity, CategoryLink>
    {
        public bool IsItemRelationActive { get; set; }

        public override CategoryLink ToModel(CategoryLink link)
        {
            base.ToModel(link);
            if(link is ExtendedCategoryLink extendedCategoryLink)
            {
                extendedCategoryLink.IsItemRelationActive = IsItemRelationActive;
            }
            
            return link;
        }

        public override CategoryItemRelationEntity FromModel(CategoryLink link)
        {
           if(link is ExtendedCategoryLink extendedCategoryLink)
           {
                IsItemRelationActive = extendedCategoryLink.IsItemRelationActive;
           }

           return base.FromModel(link);
        }

        public override void Patch(CategoryItemRelationEntity target)
        {
            base.Patch(target);
            if (target is ExtendedCategoryItemRelationEntity extendedTarget)
            {
                extendedTarget.IsItemRelationActive = IsItemRelationActive;
            }

        }

        public CategoryItemRelationEntity FromModel(CategoryLink model, PrimaryKeyResolvingMap pkMap)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            if (model is ExtendedCategoryLink extendedCategoryLink)
            {
                IsItemRelationActive = extendedCategoryLink.IsItemRelationActive;
            }
            ItemId = model.ListEntryId;
            CategoryId = model.CategoryId;
            CatalogId = model.CatalogId;
            Priority = model.Priority;

            return this;
        }
    }
}
