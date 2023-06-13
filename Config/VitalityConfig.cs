using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using MoreSkills.UI;
using Pipakin.SkillInjectorMod;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace MoreSkills.Config
{
    [BepInPlugin("MoreSkills.VitalityConfig", "MoreSkills: Vitality", "0.0.3")]
    [BepInDependency("com.pipakin.SkillInjectorMod")]
    public class MoreSkills_VitalityConfig : BaseUnityPlugin
    {
        public void Awake()
        {
            Debug.Log("[MoreSkills] Loading Vitality Skill ...");
            //Enablers
            //Vitality.Health
            EnableHealthMod = base.Config.Bind<bool>("Enablers", "Enable Vitality Mod", true, "Enables or disables the Vitality Modification.");
            //Multipliers
            //Vitality
            VitalitySkillIncreaseMultiplier = base.Config.Bind<float>("Multipliers", "Multiply the Vitality Skill Increase per Damage", 1.0f, "The Skill Increase is based in the Damaged recieved 1/10, so if you recieve 100 damage you increase the skill by This allows you to multiply this number.");
            //Base Configs
            //Vitality
            BaseHealth = base.Config.Bind<float>("BaseConfigs", "Base Health", 25f, "Change the base Health. (Valheim Default is 25)");
            BaseMaxHealth = base.Config.Bind<float>("BaseConfigs", "Base Max Health", 100f, "Change the toal Health when Vitality is at level 100. (Valheim Default is 25)");

            //Inject.Strength
            if (EnableHealthMod.Value)
                try
                {
                    SkillInjector.RegisterNewSkill(701, "Vitality", "Endure and gain resistance as you recieve damage", 1f, SkillIcons.Load_VitalityIcon(), Skills.SkillType.Unarmed);
                }
                catch (Exception e)
                {
                    Debug.LogError("Error Registering new Skill 'Vitality'" + e.Message);
                }

            //--
            Debug.Log("[MoreSkills] Vitality Skill Patched!");
            harmonyVitality = new Harmony("MoreSkills.VitalityConfig.GuiriGuyMods");
            harmonyVitality.PatchAll();

            //Logs            
            if (!EnableHealthMod.Value)
                Debug.LogWarning("[MoreSkills] Health Mod Disabled");
            else
                Debug.Log("[MoreSkills] Health Mod Enabled");

            Debug.Log("[MoreSkills] Vitality Skill Loaded!");
            Debug.Log("[MoreSkills] Everything is Loaded. Hope you love the mod :D");
        }
        private void OnDestroy()
        {

            Debug.Log("[MoreSkills] Vitality Skill UnPatched!");
            harmonyVitality.UnpatchSelf();
        }

        // Stats Bases

        public static ConfigEntry<float> BaseHealth;

        public static ConfigEntry<float> BaseMaxHealth;

        private Harmony harmonyVitality;

        //Skill Increases Multpliers

        public static ConfigEntry<float> VitalitySkillIncreaseMultiplier;

        //Enables

        public static ConfigEntry<bool> EnableHealthMod;

        //Skills Types

        public const int VitalitySkill_Type = 701;

    }
}
