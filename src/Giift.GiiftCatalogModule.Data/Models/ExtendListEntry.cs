using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtoCommerce.CatalogModule.Core.Model.ListEntry;
using VirtoCommerce.Platform.Core.Common;

namespace Giift.GiiftCatalogModule.Data.Models
{
    public class ExtendListEntry : CategoryListEntry
    {
        public override ListEntryBase FromModel(AuditableEntity entity)
        {
            return base.FromModel(entity);
        }

    }
}
