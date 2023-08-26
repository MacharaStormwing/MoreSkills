using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using MoreSkills.UI;
using MoreSkills.Utility;
using Pipakin.SkillInjectorMod;
using System;

namespace MoreSkills.Config
{
    [BepInPlugin(Plugin_Name, "MoreSkills: Crafting", "0.0.5")]
    [BepInDependency("com.pipakin.SkillInjectorMod")]
    public class MoreSkills_CraftingConfig : BaseUnityPlugin
    {
        public const String Plugin_Name = "MoreSkills.CraftingConfig";

        public void Awake()
        {
            Utilities.Log("Loading All MoreSkills Configs. Please, wait a moment...");
            Utilities.Log("Loading CraftingSkill");
            //1. Enablers
            //Crafting
            EnableCraftingSkill = base.Config.Bind<bool>("1. Enablers", "Enable Crafting Mod", true, "Enables or disables the Crafting Resources Modification");

            EnableDetailedLogging = base.Config.Bind<bool>("1. Enablers", "Enable Detailed Crafting Logging", false, "Enables or disables additional logging (such as calculating required item numbers based on crafting skill). This should be disabled unless you need it since it affects performance.");
            //2. Multipliers
            //Crafting
            //Skill
            CraftingSkillIncreaseMultiplier = base.Config.Bind<float>("2. Multipliers", "Multiply the Crafting Skill Increase", 1.5f, "Multiplies the Crafting Skill Increase that takes into count all the amount of resources used to craft the object");
            //ResourceChanges
            CraftingLevelMultiplier = base.Config.Bind<float>("2. Multipliers", "Change the Starting Multiplier in Crafting Mod", 1.2f, "(If Middle Level is 0, this number will not be counted). This is the Level 0 Multiplier at which objects cost will be multiplied at the begging of the game until reached the level you marked at config to go back to vanilla.");
            CraftingMiddleLevel = base.Config.Bind<int>("2. Multipliers", "Set the Middle Level of the High Levels Crafting Mod", 20, "This is the level where it will stop Multipling and will start Dividing the cost of objects. Can be set to 100 and never have a divider. Or 0 if you dont want any Multipliers at the start of a game.");
            CraftingLevelDivider = base.Config.Bind<float>("2. Multipliers", "Change the Ending Multiplier in the Crafting Mod", 1.5f, "(If Middle Level is 100, this number will not be counted). This is the Level 100 Divider at which objects cost will be divided at the end of the game once you reached the level you marked at config.");

            //Inject.Strength
            if (EnableCraftingSkill.Value)
                try
                {
                    SkillInjector.RegisterNewSkill(703, "Crafting", "You get better at this thing of crafting. You can probably even become more efficient...", 1f, SkillIcons.Load_CraftingIcon(), Skills.SkillType.Unarmed);
                }
                catch (Exception e)
                {
                    Utilities.LogError("Error Registering new Skill 'Crafting'" + e.Message);
                }

            //--
            Utilities.Log("Crafting Skill Patched!");
            harmonyCraft = new Harmony("MoreSkills.CraftingConfig.GuiriGuyMods");

            //Logs

            if (!EnableCraftingSkill.Value)
                Utilities.LogWarning("Crafting Mod Disabled");
            else
                Utilities.Log("Crafting Mod Enabled");

            Utilities.Log("Crafting Skill Loaded!");
        }

        private void OnDestroy()
        {

            Utilities.Log("Crafting Skill UnPatched!");
            harmonyCraft.UnpatchSelf();
        }


        // Stats Bases

        private Harmony harmonyCraft;

        //Multipliers

        public static ConfigEntry<float> CraftingLevelMultiplier;

        public static ConfigEntry<int> CraftingMiddleLevel;

        public static ConfigEntry<float> CraftingLevelDivider;
        //Skill Increases Multpliers

        public static ConfigEntry<float> CraftingSkillIncreaseMultiplier;

        //Enables

        public static ConfigEntry<bool> EnableCraftingSkill;

        public static ConfigEntry<bool> EnableDetailedLogging;

        //Skills Types

        public const int CraftingSkill_Type = 703;        
    }
}
