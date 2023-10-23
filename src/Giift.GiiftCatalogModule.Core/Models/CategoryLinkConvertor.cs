using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using VirtoCommerce.CatalogModule.Core.Model;
using VirtoCommerce.Platform.Core.JsonConverters;

namespace Giift.GiiftCatalogModule.Core.Models
{
    public class CategoryLinkConvertor : PolymorphJsonConverter
    {
        public override bool CanConvert(Type type)
        {
            return typeof(CategoryLink).IsAssignableFrom(type);
        }
        public override bool CanWrite => true;
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.StartObject)
            {
                var jObject = JObject.Load(reader);
                var extendedCategoryLink = new ExtendedCategoryLink
                {
                    IsItemRelationActive = jObject.Value<bool>("isItemRelationActive"),
                    CatalogId = jObject.Value<string>("catalogId"),
                    CategoryId = jObject.Value<string>("categoryId"),
                    ListEntryId = jObject.Value<string>("listEntryId"),
                    ListEntryType = jObject.Value<string>("listEntryType"),
                    Priority = jObject.Value<int>("priority")
                };
                if (jObject["catalog"] != null)
                {
                    extendedCategoryLink.Catalog = jObject["catalog"].ToObject<Catalog>();
                }
                if (jObject["category"] != null)
                {
                    extendedCategoryLink.Category = jObject["category"].ToObject<Category>();
                }
                return extendedCategoryLink;
            }
            return null;
        }


        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            // Start writing the JSON object
            writer.WriteStartObject();
            if (value is ExtendedCategoryLink extendedCategoryLink)
            {
                // Serialize the properties of ExtendedCategoryLink
                writer.WritePropertyName("isItemRelationActive");
                serializer.Serialize(writer, extendedCategoryLink.IsItemRelationActive);
            }
            var categoryLink  = value as CategoryLink;
            // Serialize the properties of ExtendedCategoryLink
            writer.WritePropertyName("catalogId");
            serializer.Serialize(writer, categoryLink.CatalogId);
            writer.WritePropertyName("categoryId");
            serializer.Serialize(writer, categoryLink.CategoryId);
            writer.WritePropertyName("name");
            serializer.Serialize(writer, categoryLink.Name);
            writer.WritePropertyName("entryId");
            serializer.Serialize(writer, categoryLink.EntryId);
            writer.WritePropertyName("listEntryId");
            serializer.Serialize(writer, categoryLink.ListEntryId);
            writer.WritePropertyName("listEntryType");
            serializer.Serialize(writer, categoryLink.ListEntryType);
            writer.WritePropertyName("priority");
            serializer.Serialize(writer, categoryLink.Priority);
            writer.WritePropertyName("targetId");
            serializer.Serialize(writer, categoryLink.TargetId);
            writer.WritePropertyName("category");
            serializer.Serialize(writer, categoryLink.Category);
            writer.WritePropertyName("catalog");
            serializer.Serialize(writer, categoryLink.Catalog);

            // End writing the JSON object
            writer.WriteEndObject();
        }
    }
}
