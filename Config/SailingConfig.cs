using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using MoreSkills.UI;
using MoreSkills.Utility;
using Pipakin.SkillInjectorMod;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace MoreSkills.Config
{
    [BepInPlugin(Plugin_Name, "MoreSkills: Sailing", "0.0.2")]
    [BepInDependency("com.pipakin.SkillInjectorMod")]
    public class MoreSkills_SailingConfig : BaseUnityPlugin
    {
        public const String Plugin_Name = "MoreSkills.SailingConfig";

        public void Awake()
        {
            Utilities.Log("Loading Sailing Skill...");
            //Enablers
            //Sailing
            EnableSailingMod = base.Config.Bind<bool>("Enablers", "Enable Sailing Mod", true, "Enables or disables the Sailing Modification");
            //Sailing.ShipAcceleration
            EnableShipAcceleration = base.Config.Bind<bool>("Enablers", "Enable Acceleration Mod", true, "Enables or disables Acceleration to the Ship");
            //Sailing.CrewMod
            EnableShipCrew = base.Config.Bind<bool>("Enablers", "Enable Ship Crew Mod", true, "Enables or disable the speed affect per player in the Ship");
            //Multipliers
            //Sailing
            SailingSkillIncreaseMultiplier = base.Config.Bind<float>("Multipliers", "Multiply the Sailing Skill Increase", 1.5f, "The Skill Increase is bases in the Speed of the Ship 1/2000. CrewMates sitiing in the ship 1/4000 and Crewmates standing 1/8000");
            //Base Configs
            //Sailing
            BaseBForce = base.Config.Bind<float>("BaseConfigs", "Base BackwardForce", 0.1f, "Changes the force when using the rudder, the higher the faster is the rudder");
            BaseMaxBForce = base.Config.Bind<float>("BaseConfigs", "Base Max BackwardForce", 0.5f, "Changes the Max force when Sailing is at level 100");
            CrewMembersBForceAdd = base.Config.Bind<float>("BaseConfigs", "Ammount of Add Up Per Crewmate", 1f, "Uses the Sailing Skill Level to calculate how does each player affect the BackForce when they are on-board of the ship. So if you are at level 100 and you have one more crewmate instead of having Default 0.5 Force you would have 0.6 Force");
            BaseRSpeed = base.Config.Bind<float>("BaseConfigs", "Base Rudder Speed", 0.5f, "Changes the Speed of the Rudder to reach the max turning");
            BaseMaxRSpeed = base.Config.Bind<float>("BaseConfigs", "Max Rudder Speed", 1.5f, "Changes the Max Speed of the Rudder to reach the max turning when Sailing is at Level 100.");
            BaseSForce = base.Config.Bind<float>("BaseConfigs", "Base Sail Force", 0.025f, "Changes the Base Sail Force, incresing it increases the speed with the Sail Down");
            BaseMaxSForce = base.Config.Bind<float>("BaseConfigs", "Max Sail Force", 0.5f, "Changes the Max Sail Force when Sailing is at Level 100.");
            BaseDSideways = base.Config.Bind<float>("BaseConfigs", "Base Damping Sideways", 0.3f, "Changes the Base Damping Sideways, at higher values te boat moves sideways more. Making it dangerous when Sailing at low levels in a storm.");
            BaseMaxDSideways = base.Config.Bind<float>("BaseConfigs", "Max Damping Sideways", 0.075f, "Changes the Max Damping Sideways when Sailing is at Level 100.");



            //Inject.Strength
            if (EnableSailingMod.Value)
                try
                {
                    SkillInjector.RegisterNewSkill(702, "Sailing", "You become a true viking with a great control of the boat in your adventures through seas", 1f, SkillIcons.Load_SailingIcon(), Skills.SkillType.Unarmed);
                }
                catch (Exception e)
                {
                    Utilities.LogError("Error Registering new Skill 'Sailing'" + e.Message);
                }

            //--
            Utilities.Log("Sailing Skill Patched!");
            harmonySailing = new Harmony("MoreSkills.SailingConfig.GuiriGuyMods");

            //Logs
            if (!EnableSailingMod.Value)
                Utilities.LogWarning("Sailing Skill Mod Disabled");
            else
            {
                Utilities.Log("Sailing Skill Mod Enabled");
                if (!EnableShipAcceleration.Value)
                    Utilities.LogWarning("Sailing/Ship Acceleration Mod Disabled");
                else
                    Utilities.Log("Sailing/Ship Acceleration Mod Enabled");
                if (!EnableShipCrew.Value)
                    Utilities.LogWarning("Sailing/CrewMates Mod Disabled");
                else
                    Utilities.Log("Sailing/CrewMates Mod Enabled");
            }
            Utilities.Log("Sailing Skill Loaded!");
        }
        private void OnDestroy()
        {

            Utilities.Log("Sailing Skill UnPatched!");
            harmonySailing.UnpatchSelf();
        }

        // Stats Bases

        public static ConfigEntry<float> BaseBForce;

        public static ConfigEntry<float> BaseMaxBForce;

        public static ConfigEntry<float> CrewMembersBForceAdd;

        public static ConfigEntry<float> BaseRSpeed;

        public static ConfigEntry<float> BaseMaxRSpeed;

        public static ConfigEntry<float> BaseSForce;

        public static ConfigEntry<float> BaseMaxSForce;

        public static ConfigEntry<float> BaseDSideways;

        public static ConfigEntry<float> BaseMaxDSideways;

        private Harmony harmonySailing;

        //Skill Increases Multpliers

        public static ConfigEntry<float> SailingSkillIncreaseMultiplier;
        //Enables

        public static ConfigEntry<bool> EnableSailingMod;

        public static ConfigEntry<bool> EnableShipAcceleration;

        public static ConfigEntry<bool> EnableShipCrew;

        //Skills Types

        public const int SailingSkill_Type = 702;
    }
}
