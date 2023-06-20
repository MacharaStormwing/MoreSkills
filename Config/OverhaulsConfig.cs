using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using MoreSkills.Utility;
using System;
using UnityEngine;

namespace MoreSkills.Config
{
    [BepInPlugin(Plugin_Name, "MoreSkills: Overhauls", "0.0.8")]
    [BepInDependency("com.pipakin.SkillInjectorMod")]
    public class MoreSkills_OverhaulsConfig : BaseUnityPlugin
    {
        public const String Plugin_Name = "MoreSkills.OverhaulsConfig";

        public void Awake()
        {
            Utilities.Log("Loading All Overhauls...");

            // Advanced Logging
            EnableDetailedLogging = base.Config.Bind<bool>("1. Enablers", "Enable Detailed Overhauls Logging", false, "Enables or disables additional logging (such as how drops are handled). This should be disabled unless you need it since it affects performance.");

            //Enablers
            //Sneak.CrouchSpeed
            EnableCrouchMod = base.Config.Bind<bool>("1. Enablers: Sneak", "Enable Crouch Speed Mod", true, "Enables or disables the Crouch Speed Modification");
            //Swim.Mod
            EnableSwimMod = base.Config.Bind<bool>("1. Enablers: Swim", "Enable Swim Mods", true, "Enables or disables the Complete Swim Modification");
            //Swim.Speed
            EnableSwimSpeedMod = base.Config.Bind<bool>("1. Enablers: Swim", "Enable Swim Speed Mod", true, "Enables or disables the Swim Speed Modification");
            //Swim.Stamina
            EnableSwimStaminaMod = base.Config.Bind<bool>("1. Enablers: Swim", "Enable Swim Stamina Mod", true, "Enables or disables the Swim Stamina Modification");
            //Pickaxe.Mod
            EnablePickaxeDropMod = base.Config.Bind<bool>("1. Enablers: Pickaxe", "Enable Pickaxe Drop Mod", true, "Enables or disables the Pickaxe Drops Modification");
            //WoodCutting.Mod
            EnableWoodCuttingDropMod = base.Config.Bind<bool>("1. Enablers: WoodCutting", "Enable WoodCutting Drop Mod", true, "Enables or disables the WoodCutting Drops Modification");
            //Jumping
            EnableJumpMod = base.Config.Bind<bool>("1. Enablers: Jump", "Enable Jump Mods", true, "Enables or disables the Jumping Modifications");
            //FallDamage
            EnableDecreaseFallDamageMod = base.Config.Bind<bool>("1. Enablers: Jump", "Enable Fall Damage Mods", true, "Enables or disables the Jumping Fall Damage Modifications");
            //RollonFall
            EnableRollOnFall = base.Config.Bind<bool>("1. Enablers: Jump", "Enable Roll on Fall Mod", true, "Enables or disables the Roll on Fall Modification");
            //HigherJump
            EnableHigherJump = base.Config.Bind<bool>("1. Enablers: Jump", "Enable Higher Jump Mod", true, "Enables or disables the Higher Jump Modification");
            //HaveToShift
            EnableHaveToShiftToHigherJump = base.Config.Bind<bool>("1. Enablers: Jump", "Enable Need to Shift for Higher Jump Mod", true, "Enables or disables Need to Shift (or custom key) to make a Higher Jump Modification");
            //MaxFallAltitude
            EnableMaxFallAltitude = base.Config.Bind<bool>("1. Enablers: Jump", "Enable Max Fall Altitude", true, "Enables or disables the Max Altitude a Character can fall without dying.");
            //Multipliers
            //Swim
            //Stamina
            SwimStaminaMultiplier = base.Config.Bind<float>("2. Multipliers: Swim", "Multiply the Swim Stamina Increase", 1.0f, "Multiply the amount of stamina you gain staying still in the water.");
            //Pickaxe
            PickaxeMultiplier = base.Config.Bind<float>("2. Multipliers: Pickaxe", "Multiplier based on Pickaxe Skill", 1.5f, "The based on level multipliers, so at level 100 you reach such number. At level 100 you got default x1.5 times the amount of drops than vanilla. This multiplier changes that number.");
            //WoodCutting
            //Drops
            WoodCuttingMultiplier = base.Config.Bind<float>("2. Multipliers: WoodCutting", "Multiplier based on WoodCutting Skill", 1.5f, "The based on level multipliers, so at level 100 you reach such number. At level 100 you got default x1.5 times the amount of drops than vanilla. This multiplier changes that number.");

            //Jumping
            //DamageDecrease
            FallDamageDecrease = base.Config.Bind<float>("2. Multipliers: Jump", "Decrease Multiplier based on Jumping Skill", 2.0f, "Decreases the Damage Recieved by Falling from a High Altitude (4 meters)");
            //DamageIncrease
            MaxFallDamageIncrease = base.Config.Bind<float>("2. Multipliers: Jump", "Increase Multiplier Per Meter over Max Fall Altitude Skill", 2.0f, "Increses the Damage Recieved per meter exceded from Max Falling Altitude");
            //Base Configs
            //Sneak
            BaseCrouchSpeed = base.Config.Bind<float>("3. BaseConfigs: Sneak", "Base Crouch Speed", 2f, "Change the base Crouch Speed. (Valheim Default is 2)");
            BaseMaxCrouchSpeed = base.Config.Bind<float>("3. BaseConfigs: Sneak", "Base Max Crouch Speed", 4f, "Change the base Max Crouch Speed at level 100. (Valheim Default is 2)");
            //Swim
            //Speed
            BaseSwimSpeed = base.Config.Bind<float>("3. BaseConfigs: Swim", "Base Swim Speed", 2f, "Change the base Swim Speed (Valheim Defailt is 2)");
            BaseMaxSwimSpeed = base.Config.Bind<float>("3. BaseConfigs: Swim", "Base Max Swim Speed", 4f, "Change the base Max Swim Speed at level 100. (Valheim Default is 2)");
            //Jump
            //Heights and Key
            BaseJumpForce = base.Config.Bind<float>("3. BaseConfigs: Jump", "Base Jump Force", 8f, "Change the base Jump Force (Valheim Defailt is 8)");
            BaseMaxJumpForce = base.Config.Bind<float>("3. BaseConfigs: Jump", "Base Max Jump Force", 12f, "Change the base Max Jump Force at level 100. (Valheim Default is 8)");
            HigherJumpKey = base.Config.Bind<KeyCode>("3. BaseConfigs: Jump", "Base Higher Jump Key Force", KeyCode.LeftShift, "Change the button to hold when wanted a Higher Jump. Keys: https://docs.unity3d.com/Manual/class-InputManager.html");
            BaseRollAltitude = base.Config.Bind<float>("3. BaseConfigs: Jump", "Base Roll Altitude", 4f, "Change the base altitude you need to reach for the Character roll on fall, not recieving any damage.");
            BaseMaxRollAltitude = base.Config.Bind<float>("3. BaseConfigs: Jump", "Base Max Roll Altitude", 8f, "Change the base Max altitude at jump level 100, at which the Character will roll on fall and not recieve any damage.");
            BaseMaxFallAltitude = base.Config.Bind<float>("3. BaseConfigs: Jump", "Base Max Fall Altitude", 30f, "Change the base Max fall altitude which if it's higher than that you will directly die. (Valheim Default is 20)");

            // Added by Machara Stormwing:

            // types of drops recognized when cutting wood using the WoodCutting skill with an axe
            // BeechSeeds,BirchCone,OakSeeds,Acorn,ElderBark,FineWood,FirCone,PineCone,Resin,RoundLog,Wood
            WoodCuttingApplyForItems = base.Config.Bind<String>("3. Baseconfigs: Woodcutting", "WoodCuttingApplyForItems",
                "BeechSeeds,BirchCone,OakSeeds,Acorn,ElderBark,FineWood,FirCone,PineCone,Resin,RoundLog,Wood,Uncut_Black_Stone,Uncut_Green_Stone,Uncut_Purple_Stone,Uncut_Blue_Stone,Uncut_Yellow_Stone,Uncut_Red_Stone,Uncut_Orange_Stone",
                "List of items that might drop when applying the skill 'WoodCutting' to apply the skill on and increase drop rates to based on skill level. If an item is not in the list the skill will not be applied. 'Enable WoodCutting Drop Mod' must be true for this to be applied.");

            // types of drops recognized when mining ore, stone or other items using the Pickaxes skill with a pickaxe
            // Chitin,CopperOre,IronScrap,Obsidian,SilverOre,TinOre,Stone
            PickaxeApplyForItems = base.Config.Bind<String>("3. Baseconfigs: Pickaxe", "PickaxeApplyForItems",
                "Chitin,CopperOre,IronScrap,Obsidian,SilverOre,TinOre,Stone",
                "List of items that might drop when applying the skill Pickaxe to apply the skill on and increase drop rates to based on skill level. If an item is not in the list the skill will not be applied. 'Enable Pickaxe Drop Mod' must be true for this to be applied.");

            // any other drops possible using the HuntingSkill_Type
            // Feathers,Guck,LeatherScraps,WitheredBone
            HuntingApplyForItems = base.Config.Bind<String>("3. Baseconfigs: Hunting", "HuntingApplyForItems",
                "Feathers,Guck,LeatherScraps,WitheredBone",
                "List of items that might drop when applying the skill 'Hunting' in HuntingConfig to apply the skill on and increase drop rates to based on skill level. If an item is not in the list the skill will not be applied. 'Enable Hunting Skill' in the HuntingConfig must be true for this to be applied.");

            //--
            Utilities.Log("Overhauls Patched!");
            harmonyOverhauls = new Harmony("MoreSkills.OverhaulsConfig.GuiriGuyMods");

            //Logs
            if (!EnableCrouchMod.Value)
                Utilities.LogWarning("Crouch Mod Disabled");
            else
                Utilities.Log("Crouch Mod Enabled");
            if (!EnableSwimMod.Value)
                Utilities.LogWarning("Swim Mod Disabled");
            else
            {
                Utilities.Log("Swim Mod Enabled");
                if (!EnableSwimSpeedMod.Value)
                    Utilities.LogWarning("Swim/Speed Mod Disabled");
                else
                    Utilities.Log("Swim/Speed Mod Enabled");
                if (!EnableSwimStaminaMod.Value)
                    Utilities.LogWarning("Swim/Stamina Mod Disabled");
                else
                    Utilities.Log("Swim/Stamina Mod Enabled");
            }
            if (!EnablePickaxeDropMod.Value)
                Utilities.LogWarning("Pickaxe Drop Mod Disabled");
            else
                Utilities.Log("Pickaxe Drop Mod Enabled");

            if (!EnableWoodCuttingDropMod.Value)
                Utilities.LogWarning("WoodCutting Drop Mod Disabled");
            else
                Utilities.Log("WoodCutting Drop Mod Enabled");

            if (!EnableJumpMod.Value)
                Utilities.LogWarning("Jump Mod Disabled");
            else
            {
                Utilities.Log("Jump Mod Enabled");
                if (!EnableHigherJump.Value)
                    Utilities.LogWarning("Jump/Higher Mod Disabled");
                else
                    Utilities.Log("Jump/Higher Mod Enabled");
                if (!EnableDecreaseFallDamageMod.Value)
                    Utilities.LogWarning("Jump/Decreased Fall Damage Mod Disabled");
                else
                    Utilities.Log("Jump/Decreased Fall Damage Mod Enabled");
                if (!EnableHaveToShiftToHigherJump.Value)
                    Utilities.LogWarning("Jump/Need to Shift for Higher Jump Mod Disabled");
                else
                    Utilities.Log("Jump/Need to Shift for Higher Jump Mod Enabled");
                if (!EnableRollOnFall.Value)
                    Utilities.LogWarning("Jump/Roll On Fall Mod Disabled");
                else
                    Utilities.Log("Jump/Roll On Fall Mod Enabled");
                if (!EnableMaxFallAltitude.Value)
                    Utilities.LogWarning("Jump/Max Fall Altitude Mod Disabled");
                else
                    Utilities.Log("Jump/Max Fall Altitude Mod Enabled");

            }

            Utilities.Log("All Overhauls Loaded!");
        }
        private void OnDestroy()
        {

            Utilities.Log("Overhauls UnPatched!");
            harmonyOverhauls.UnpatchSelf();
        }

        // Log config

        public static ConfigEntry<bool> EnableDetailedLogging;

        // Stats Bases

        public static ConfigEntry<float> BaseCrouchSpeed;

        public static ConfigEntry<float> BaseMaxCrouchSpeed;

        public static ConfigEntry<float> BaseSwimSpeed;

        public static ConfigEntry<float> BaseMaxSwimSpeed;

        public static ConfigEntry<float> BaseJumpForce;

        public static ConfigEntry<float> BaseMaxJumpForce;

        public static ConfigEntry<float> BaseRollAltitude;

        public static ConfigEntry<float> BaseMaxRollAltitude;

        public static ConfigEntry<float> BaseMaxFallAltitude;

        private Harmony harmonyOverhauls;

        //Multipliers

        public static ConfigEntry<float> PickaxeMultiplier;

        public static ConfigEntry<float> SwimStaminaMultiplier;

        public static ConfigEntry<float> WoodCuttingMultiplier;

        public static ConfigEntry<float> FallDamageDecrease;

        public static ConfigEntry<float> MaxFallDamageIncrease;

        //Enables

        public static ConfigEntry<bool> EnableCrouchMod;

        public static ConfigEntry<bool> EnableSwimMod;

        public static ConfigEntry<bool> EnablePickaxeDropMod;

        public static ConfigEntry<bool> EnablePickaxeChanceMod;

        public static ConfigEntry<bool> EnableSwimSpeedMod;

        public static ConfigEntry<bool> EnableSwimStaminaMod;

        public static ConfigEntry<bool> EnableWoodCuttingDropMod;

        public static ConfigEntry<bool> EnableJumpMod;

        public static ConfigEntry<bool> EnableDecreaseFallDamageMod;

        public static ConfigEntry<bool> EnableHigherJump;

        public static ConfigEntry<bool> EnableRollOnFall;

        public static ConfigEntry<bool> EnableHaveToShiftToHigherJump;

        public static ConfigEntry<bool> EnableMaxFallAltitude;

        public static ConfigEntry<KeyCode> HigherJumpKey;

        // Added by Machara Stormwing
        public static ConfigEntry<String> WoodCuttingApplyForItems;

        public static ConfigEntry<String> PickaxeApplyForItems;

        public static ConfigEntry<String> HuntingApplyForItems;

    }
}