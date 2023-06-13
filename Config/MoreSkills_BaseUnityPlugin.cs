using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BepInEx;
using BepInEx.Configuration;
using JetBrains.Annotations;
using MoreSkills.Utility;
using ServerSync;

namespace MoreSkills.Config
{
    /*[BepInDependency("com.pipakin.SkillInjectorMod")]
    internal class MoreSkills_BaseUnityPlugin : BaseUnityPlugin
    {
        public const string Version = "0.9.6";
        public const string ModName = "MoreSkills";
        public const string GUID = "com.odinplusqol.mod";

        public static ConfigSync configSync = new(GUID) { DisplayName = ModName, CurrentVersion = Version };


        internal ConfigEntry<T> TextEntryConfig<T>(string group, string name, T value, string desc,
            bool synchronizedSetting = true)
        {
            ConfigurationManagerAttributes attributes = new()
            {
                CustomDrawer = Utilities.TextAreaDrawer
            };
            return config(group, name, value, new ConfigDescription(desc, null, attributes), synchronizedSetting);
        }

        internal ConfigEntry<T> config<T>(string group, string name, T value, ConfigDescription description,
            bool synchronizedSetting = true)
        {
            ConfigDescription extendedDescription =
                new(
                    description.Description +
                    (synchronizedSetting ? " [Synced with Server]" : " [Not Synced with Server]"),
                    description.AcceptableValues, description.Tags);
            ConfigEntry<T> configEntry = Config.Bind(group, name, value, extendedDescription);

            SyncedConfigEntry<T> syncedConfigEntry = configSync.AddConfigEntry(configEntry);
            syncedConfigEntry.SynchronizedConfig = synchronizedSetting;

            return configEntry;
        }

        private class ConfigurationManagerAttributes
        {
            [UsedImplicitly] public int? Order;
            [UsedImplicitly] public bool? Browsable;
            [UsedImplicitly] public string? Category;
            [UsedImplicitly] public Action<ConfigEntryBase>? CustomDrawer;
        }
    }*/
}
