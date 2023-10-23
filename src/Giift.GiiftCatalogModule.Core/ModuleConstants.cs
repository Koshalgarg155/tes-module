using System.Collections.Generic;
using VirtoCommerce.Platform.Core.Settings;

namespace Giift.GiiftCatalogModule.Core;

public static class ModuleConstants
{
    public static class Security
    {
        public static class Permissions
        {
            public const string Access = "GiiftCatalogModule:access";
            public const string Create = "GiiftCatalogModule:create";
            public const string Read = "GiiftCatalogModule:read";
            public const string Update = "GiiftCatalogModule:update";
            public const string Delete = "GiiftCatalogModule:delete";

            public static string[] AllPermissions { get; } =
            {
                Access,
                Create,
                Read,
                Update,
                Delete,
            };
        }
    }

    public static class Settings
    {
        public static class General
        {
            public static SettingDescriptor GiiftCatalogModuleEnabled { get; } = new SettingDescriptor
            {
                Name = "GiiftCatalogModule.GiiftCatalogModuleEnabled",
                GroupName = "GiiftCatalogModule|General",
                ValueType = SettingValueType.Boolean,
                DefaultValue = false,
            };

            public static SettingDescriptor GiiftCatalogModulePassword { get; } = new SettingDescriptor
            {
                Name = "GiiftCatalogModule.GiiftCatalogModulePassword",
                GroupName = "GiiftCatalogModule|Advanced",
                ValueType = SettingValueType.SecureString,
                DefaultValue = "qwerty",
            };

            public static IEnumerable<SettingDescriptor> AllGeneralSettings
            {
                get
                {
                    yield return GiiftCatalogModuleEnabled;
                    yield return GiiftCatalogModulePassword;
                }
            }
        }

        public static IEnumerable<SettingDescriptor> AllSettings
        {
            get
            {
                return General.AllGeneralSettings;
            }
        }
    }
}
