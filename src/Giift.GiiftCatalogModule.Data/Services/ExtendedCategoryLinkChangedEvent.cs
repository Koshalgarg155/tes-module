using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Giift.GiiftCatalogModule.Core.Models;
using Serialize.Linq.Nodes;
using VirtoCommerce.Platform.Core.Events;

namespace Giift.GiiftCatalogModule.Data.Services
{
    public class ExtendedCategoryLinkChangedEvent : GenericChangedEntryEvent<ExtendedCategoryLink>
    {
        public ExtendedCategoryLinkChangedEvent(IEnumerable<GenericChangedEntry<ExtendedCategoryLink>> changedEntries) : base(changedEntries)
        {
        }
    }
}
