using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using MoreSkills.UI;
using MoreSkills.Utility;
using Pipakin.SkillInjectorMod;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace MoreSkills.Config
{
    [BepInPlugin(Plugin_Name, "MoreSkills: Taming", "0.0.6")]
    [BepInDependency("com.pipakin.SkillInjectorMod")]
    public class MoreSkills_TamingConfig : BaseUnityPlugin
    {
        public const String Plugin_Name = "MoreSkills.TamingConfig";

        public void Awake()
        {
            Utilities.Log("Loading Taming Skill...");

            // Advanced Logging
            EnableDetailedLogging = base.Config.Bind<bool>("1. Enablers", "Enable Detailed Taming Logging", false, "Enables or disables additional logging (such as taming start and updates) for taming. This should be disabled unless you need it since it can affect performance.");

            //1. Enablers
            //Taming
            EnableTamingSkill = base.Config.Bind<bool>("1. Enablers", "Enable Taming Skill", true, "Enables or disables the Taming Skill Modification");
            //AllTamingCompatibility
            EnableAllTamableCompatibility = base.Config.Bind<bool>("1. Enablers", "Enable Taming Compatibility With AllTamable Mod", false, "Enables or disables the Compatibility with AllTamable Mod by buzz");
            //TamingUnlocks
            EnableTamingUnlocks = base.Config.Bind<bool>("1. Enablers", "Enable Taming Unlocks Per Level", true, "Enables or disables the Unlock once you reach a level the ability to tame a New Creature. You will still be able if you don't meet the required level, but it will be really hard.");
            //TamingTime
            EnableTamingTimeMod = base.Config.Bind<bool>("1. Enablers", "Enable Taming Times Modification", true, "Enables or disables the time to tame modification based on level. The higher the less it takes to tame.");
            //TamingEat
            EnableTamingEatMod = base.Config.Bind<bool>("1. Enablers", "Enable Taming Eat Modification", true, "Enables or disables the rate time at the creature eats based on level. The lower the more it eats.");
            //CustomNames
            EnableCustomNames = base.Config.Bind<bool>("1. Enablers", "Enable Custom Tamed Names", true, "Enables or disables the ability to name your Tamed Creature.");
            //2. Multipliers
            //Taming
            //Skill
            TamingSkillIncreaseMultiplier = base.Config.Bind<float>("2. Multipliers", "Multiplies the Taming Skill Increase", 2.0f, "The Skill Increase is based on the time it takes to tame the Mob 1/1000, so if the mob takes 1800 seconds 1.8 * Level (If you level up, it will only level you up and loose the rest of exp, to yet be fixed). This allows you to multiply this number.");

            // fill directory of tame-ables (added by MacharaStormwing)
            // TODO: this is currently filled in once at the start, any changes to config values are likely not updated as of now during run-time
            CreatureTameLevelDirectory = new Dictionary<string, Helper.CreatureTameLevels>();
            AdvancedCreatureTameLevelDirectory = new Dictionary<string, Helper.CreatureTameLevels>();

            //Creatures Unlock Level
            //Default
            if (EnableAllTamableCompatibility.Value)
            {
                //Boar
                BoarLevelUnlock = base.Config.Bind<float>("3. CreatureLevels: AllTamableCompatibility", "Boar Unlock Level", 0f, "The level at which you know how to tame this creature.");
                BoarLevelTamer = base.Config.Bind<float>("3. CreatureLevels: AllTamableCompatibility", "Boar Tamer Level", 5f, "The level at which you are good at taming this creature. Reduces the amount of time by half");
                BoarLevelMaster = base.Config.Bind<float>("3. CreatureLevels: AllTamableCompatibility", "Boar Master Level", 15f, "The level at which you are a Master taming this creature. You tame the creature in 2 minutes time.");
                //Wolf
                WolfLevelUnlock = base.Config.Bind<float>("3. CreatureLevels: AllTamableCompatibility", "Wolf Unlock Level", 10f, "The level at which you know how to tame this creature.");
                WolfLevelTamer = base.Config.Bind<float>("3. CreatureLevels: AllTamableCompatibility", "Wolf Tamer Level", 20f, "The level at which you are good at taming this creature. Reduces the amount of time by half");
                WolfLevelMaster = base.Config.Bind<float>("3. CreatureLevels: AllTamableCompatibility", "Wolf Master Level", 30f, "The level at which you are a Master taming this creature. You tame the creature in 2 minutes time");
                //Lox
                LoxLevelUnlock = base.Config.Bind<float>("3. CreatureLevels: AllTamableCompatibility", "Lox Unlock Level", 60f, "The level at which you know how to tame this creature.");
                LoxLevelTamer = base.Config.Bind<float>("3. CreatureLevels: AllTamableCompatibility", "Lox Tamer Level", 70f, "The level at which you are good at taming this creature. Reduces the amount of time by half");
                LoxLevelMaster = base.Config.Bind<float>("3. CreatureLevels: AllTamableCompatibility", "Lox Master Level", 80f, "The level at which you are a Master taming this creature. You tame the creature in 2 minutes time");
            }
            else
            {
                //Boar
                BoarLevelUnlock = base.Config.Bind<float>("3. CreatureLevels: Defaults", "Boar Unlock Level", 0f, "The level at which you know how to tame this creature.");
                BoarLevelTamer = base.Config.Bind<float>("3. CreatureLevels: Defaults", "Boar Tamer Level", 10f, "The level at which you are good at taming this creature. Reduces the amount of time by half");
                BoarLevelMaster = base.Config.Bind<float>("3. CreatureLevels: Defaults", "Boar Master Level", 40f, "The level at which you are a Master taming this creature. You tame the creature in 2 minutes time.");
                //Wolf
                WolfLevelUnlock = base.Config.Bind<float>("3. CreatureLevels: Defaults", "Wolf Unlock Level", 10f, "The level at which you know how to tame this creature.");
                WolfLevelTamer = base.Config.Bind<float>("3. CreatureLevels: Defaults", "Wolf Tamer Level", 30f, "The level at which you are good at taming this creature. Reduces the amount of time by half");
                WolfLevelMaster = base.Config.Bind<float>("3. CreatureLevels: Defaults", "Wolf Master Level", 70f, "The level at which you are a Master taming this creature. You tame the creature in 2 minutes time");
                //Lox
                LoxLevelUnlock = base.Config.Bind<float>("3. CreatureLevels: Defaults", "Lox Unlock Level", 30f, "The level at which you know how to tame this creature.");
                LoxLevelTamer = base.Config.Bind<float>("3. CreatureLevels: Defaults", "Lox Tamer Level", 70f, "The level at which you are good at taming this creature. Reduces the amount of time by half");
                LoxLevelMaster = base.Config.Bind<float>("3. CreatureLevels: Defaults", "Lox Master Level", 100f, "The level at which you are a Master taming this creature. You tame the creature in 2 minutes time");
            }

            addCreatureTameLevels(Boar_Creature_Name,
                BoarLevelUnlock, BoarLevelTamer, BoarLevelMaster);
            addCreatureTameLevels(Wolf_Creature_Name,
                WolfLevelUnlock, WolfLevelTamer, WolfLevelMaster);
            addCreatureTameLevels(Lox_Creature_Name,
                LoxLevelUnlock, LoxLevelTamer, LoxLevelMaster);

            Unknown_LevelUnlock = base.Config.Bind<float>("3. CreatureLevels: Defaults", "Unknown Creature Unlock Level", 30f, "The level at which you know how to tame an undefined creature.");
            Unknown_LevelTamer = base.Config.Bind<float>("3. CreatureLevels: Defaults", "Unknown Creature Tamer Level", 60f, "The level at which you are good at taming an undefined creature. Reduces the amount of time by half");
            Unknown_LevelMaster = base.Config.Bind<float>("3. CreatureLevels: Defaults", "Unknown Creature Master Level", 90f, "The level at which you are a Master taming an undefined creature. You tame the creature in 2 minutes time");

            //AllTamableCompatibility
            //Neck
            NeckLevelUnlock = base.Config.Bind<float>("3. CreatureLevels: AllTamableCompatibility", "Neck Unlock Level", 0f, "NEEDS ALLTAMABLE MOD!. The level at which you know how to tame this creature.");
            NeckLevelTamer = base.Config.Bind<float>("3. CreatureLevels: AllTamableCompatibility", "Neck Tamer Level", 5f, "NEEDS ALLTAMABLE MOD!. The level at which you are good at taming this creature. Reduces the amount of time by half");
            NeckLevelMaster = base.Config.Bind<float>("3. CreatureLevels: AllTamableCompatibility", "Neck Master Level", 15f, "NEEDS ALLTAMABLE MOD!. The level at which you are a Master taming this creature. You tame the creature in 2 minutes time");
            addAdvancedCreatureTameLevels(Neck_Creature_Name,
               NeckLevelUnlock, NeckLevelTamer, NeckLevelMaster);
            //Deer
            DeerLevelUnlock = base.Config.Bind<float>("3. CreatureLevels: AllTamableCompatibility", "Deer Unlock Level", 0f, "NEEDS ALLTAMABLE MOD!. The level at which you know how to tame this creature.");
            DeerLevelTamer = base.Config.Bind<float>("3. CreatureLevels: AllTamableCompatibility", "Deer Tamer Level", 5f, "NEEDS ALLTAMABLE MOD!. The level at which you are good at taming this creature. Reduces the amount of time by half");
            DeerLevelMaster = base.Config.Bind<float>("3. CreatureLevels: AllTamableCompatibility", "Deer Master Level", 15f, "NEEDS ALLTAMABLE MOD!. The level at which you are a Master taming this creature. You tame the creature in 2 minutes time");
            addAdvancedCreatureTameLevels(Deer_Creature_Name,
               DeerLevelUnlock, DeerLevelTamer, DeerLevelMaster);
            //Blob
            BlobLevelUnlock = base.Config.Bind<float>("3. CreatureLevels: AllTamableCompatibility", "Blob Unlock Level", 10f, "NEEDS ALLTAMABLE MOD!. The level at which you know how to tame this creature.");
            BlobLevelTamer = base.Config.Bind<float>("3. CreatureLevels: AllTamableCompatibility", "Blob Tamer Level", 20f, "NEEDS ALLTAMABLE MOD!. The level at which you are good at taming this creature. Reduces the amount of time by half");
            BlobLevelMaster = base.Config.Bind<float>("3. CreatureLevels: AllTamableCompatibility", "Blob Master Level", 30f, "NEEDS ALLTAMABLE MOD!. The level at which you are a Master taming this creature. You tame the creature in 2 minutes time");
            addAdvancedCreatureTameLevels(Blob_Creature_Name,
               BlobLevelUnlock, BlobLevelTamer, BlobLevelMaster);
            //BlobElite
            BlobEliteLevelUnlock = base.Config.Bind<float>("3. CreatureLevels: AllTamableCompatibility", "Blob Elite Unlock Level", 30f, "NEEDS ALLTAMABLE MOD!. The level at which you know how to tame this creature.");
            BlobEliteLevelTamer = base.Config.Bind<float>("3. CreatureLevels: AllTamableCompatibility", "Blob Elite Tamer Level", 40f, "NEEDS ALLTAMABLE MOD!. The level at which you are good at taming this creature. Reduces the amount of time by half");
            BlobEliteLevelMaster = base.Config.Bind<float>("3. CreatureLevels: AllTamableCompatibility", "Blob Elite Master Level", 50f, "NEEDS ALLTAMABLE MOD!. The level at which you are a Master taming this creature. You tame the creature in 2 minutes time");
            addAdvancedCreatureTameLevels(BlobElite_Creature_Name,
               BlobEliteLevelUnlock, BlobEliteLevelTamer, BlobEliteLevelMaster);
            //Deathsquito
            DeathsquitoLevelUnlock = base.Config.Bind<float>("3. CreatureLevels: AllTamableCompatibility", "Deathsquito Unlock Level", 30f, "NEEDS ALLTAMABLE MOD!. The level at which you know how to tame this creature.");
            DeathsquitoLevelTamer = base.Config.Bind<float>("3. CreatureLevels: AllTamableCompatibility", "Deathsquito Tamer Level", 40f, "NEEDS ALLTAMABLE MOD!. The level at which you are good at taming this creature. Reduces the amount of time by half");
            DeathsquitoLevelMaster = base.Config.Bind<float>("3. CreatureLevels: AllTamableCompatibility", "Deathsquito Master Level", 50f, "NEEDS ALLTAMABLE MOD!. The level at which you are a Master taming this creature. You tame the creature in 2 minutes time");
            addAdvancedCreatureTameLevels(Deathsquito_Creature_Name,
               DeathsquitoLevelUnlock, DeathsquitoLevelTamer, DeathsquitoLevelMaster);
            //Drake
            DrakeLevelUnlock = base.Config.Bind<float>("3. CreatureLevels: AllTamableCompatibility", "Drake Unlock Level", 20f, "NEEDS ALLTAMABLE MOD!. The level at which you know how to tame this creature.");
            DrakeLevelTamer = base.Config.Bind<float>("3. CreatureLevels: AllTamableCompatibility", "Drake Tamer Level", 30f, "NEEDS ALLTAMABLE MOD!. The level at which you are good at taming this creature. Reduces the amount of time by half");
            DrakeLevelMaster = base.Config.Bind<float>("3. CreatureLevels: AllTamableCompatibility", "Drake Master Level", 40f, "NEEDS ALLTAMABLE MOD!. The level at which you are a Master taming this creature. You tame the creature in 2 minutes time");
            addAdvancedCreatureTameLevels(Drake_Creature_Name,
               DrakeLevelUnlock, DrakeLevelTamer, DrakeLevelMaster);
            //Draugr
            DraugrLevelUnlock = base.Config.Bind<float>("3. CreatureLevels: AllTamableCompatibility", "Draugr Unlock Level", 15f, "NEEDS ALLTAMABLE MOD!. The level at which you know how to tame this creature.");
            DraugrLevelTamer = base.Config.Bind<float>("3. CreatureLevels: AllTamableCompatibility", "Draugr Tamer Level", 25f, "NEEDS ALLTAMABLE MOD!. The level at which you are good at taming this creature. Reduces the amount of time by half");
            DraugrLevelMaster = base.Config.Bind<float>("3. CreatureLevels: AllTamableCompatibility", "Draugr Master Level", 35f, "NEEDS ALLTAMABLE MOD!. The level at which you are a Master taming this creature. You tame the creature in 2 minutes time");
            addAdvancedCreatureTameLevels(Draugr_Creature_Name,
               DraugrLevelUnlock, DraugrLevelTamer, DraugrLevelMaster);
            //Draugr Elite
            DraugrEliteLevelUnlock = base.Config.Bind<float>("3. CreatureLevels: AllTamableCompatibility", "Draugr Elite Unlock Level", 40f, "NEEDS ALLTAMABLE MOD!. The level at which you know how to tame this creature.");
            DraugrEliteLevelTamer = base.Config.Bind<float>("3. CreatureLevels: AllTamableCompatibility", "Draugr Elite Tamer Level", 50f, "NEEDS ALLTAMABLE MOD!. The level at which you are good at taming this creature. Reduces the amount of time by half");
            DraugrEliteLevelMaster = base.Config.Bind<float>("3. CreatureLevels: AllTamableCompatibility", "Draugr Elite Master Level", 60f, "NEEDS ALLTAMABLE MOD!. The level at which you are a Master taming this creature. You tame the creature in 2 minutes time");
            addAdvancedCreatureTameLevels(DraugrElite_Creature_Name,
               DraugrEliteLevelUnlock, DraugrEliteLevelTamer, DraugrEliteLevelMaster);
            //Fenring
            FenringLevelUnlock = base.Config.Bind<float>("3. CreatureLevels: AllTamableCompatibility", "Fenring Unlock Level", 20f, "NEEDS ALLTAMABLE MOD!. The level at which you know how to tame this creature.");
            FenringLevelTamer = base.Config.Bind<float>("3. CreatureLevels: AllTamableCompatibility", "Fenring Tamer Level", 30f, "NEEDS ALLTAMABLE MOD!. The level at which you are good at taming this creature. Reduces the amount of time by half");
            FenringLevelMaster = base.Config.Bind<float>("3. CreatureLevels: AllTamableCompatibility", "Fenring Master Level", 40f, "NEEDS ALLTAMABLE MOD!. The level at which you are a Master taming this creature. You tame the creature in 2 minutes time");
            addAdvancedCreatureTameLevels(Fenring_Creature_Name,
               FenringLevelUnlock, FenringLevelTamer, FenringLevelMaster);
            //Ghost
            GhostLevelUnlock = base.Config.Bind<float>("3. CreatureLevels: AllTamableCompatibility", "Ghost Unlock Level", 50f, "NEEDS ALLTAMABLE MOD!. The level at which you know how to tame this creature.");
            GhostLevelTamer = base.Config.Bind<float>("3. CreatureLevels: AllTamableCompatibility", "Ghost Tamer Level", 60f, "NEEDS ALLTAMABLE MOD!. The level at which you are good at taming this creature. Reduces the amount of time by half");
            GhostLevelMaster = base.Config.Bind<float>("3. CreatureLevels: AllTamableCompatibility", "Ghost Master Level", 70f, "NEEDS ALLTAMABLE MOD!. The level at which you are a Master taming this creature. You tame the creature in 2 minutes time");
            addAdvancedCreatureTameLevels(Ghost_Creature_Name,
               GhostLevelUnlock, GhostLevelTamer, GhostLevelMaster);
            //Goblin
            GoblinLevelUnlock = base.Config.Bind<float>("3. CreatureLevels: AllTamableCompatibility", "Goblin Unlock Level", 50f, "NEEDS ALLTAMABLE MOD!. The level at which you know how to tame this creature.");
            GoblinLevelTamer = base.Config.Bind<float>("3. CreatureLevels: AllTamableCompatibility", "Goblin Tamer Level", 60f, "NEEDS ALLTAMABLE MOD!. The level at which you are good at taming this creature. Reduces the amount of time by half");
            GoblinLevelMaster = base.Config.Bind<float>("3. CreatureLevels: AllTamableCompatibility", "Goblin Master Level", 70f, "NEEDS ALLTAMABLE MOD!. The level at which you are a Master taming this creature. You tame the creature in 2 minutes time");
            addAdvancedCreatureTameLevels(Goblin_Creature_Name,
               GoblinLevelUnlock, GoblinLevelTamer, GoblinLevelMaster);
            //Goblin Brute
            GoblinBruteLevelUnlock = base.Config.Bind<float>("3. CreatureLevels: AllTamableCompatibility", "Goblin Brute Unlock Level", 70f, "NEEDS ALLTAMABLE MOD!. The level at which you know how to tame this creature.");
            GoblinBruteLevelTamer = base.Config.Bind<float>("3. CreatureLevels: AllTamableCompatibility", "Goblin Brute Tamer Level", 80f, "NEEDS ALLTAMABLE MOD!. The level at which you are good at taming this creature. Reduces the amount of time by half");
            GoblinBruteLevelMaster = base.Config.Bind<float>("3. CreatureLevels: AllTamableCompatibility", "Goblin Brute Master Level", 90f, "NEEDS ALLTAMABLE MOD!. The level at which you are a Master taming this creature. You tame the creature in 2 minutes time");
            addAdvancedCreatureTameLevels(GoblinBrute_Creature_Name,
               GoblinBruteLevelUnlock, GoblinBruteLevelTamer, GoblinBruteLevelMaster);
            //Goblin Shaman
            GoblinShamanLevelUnlock = base.Config.Bind<float>("3. CreatureLevels: AllTamableCompatibility", "Goblin Shaman Unlock Level", 60f, "NEEDS ALLTAMABLE MOD!. The level at which you know how to tame this creature.");
            GoblinShamanLevelTamer = base.Config.Bind<float>("3. CreatureLevels: AllTamableCompatibility", "Goblin Shaman Tamer Level", 70f, "NEEDS ALLTAMABLE MOD!. The level at which you are good at taming this creature. Reduces the amount of time by half");
            GoblinShamanLevelMaster = base.Config.Bind<float>("3. CreatureLevels: AllTamableCompatibility", "Goblin Shaman Master Level", 80f, "NEEDS ALLTAMABLE MOD!. The level at which you are a Master taming this creature. You tame the creature in 2 minutes time");
            addAdvancedCreatureTameLevels(GoblinShaman_Creature_Name,
               GoblinShamanLevelUnlock, GoblinShamanLevelTamer, GoblinShamanLevelMaster);
            //Greydwarf
            GreydwarfLevelUnlock = base.Config.Bind<float>("3. CreatureLevels: AllTamableCompatibility", "Greydwarf Unlock Level", 5f, "NEEDS ALLTAMABLE MOD!. The level at which you know how to tame this creature.");
            GreydwarfLevelTamer = base.Config.Bind<float>("3. CreatureLevels: AllTamableCompatibility", "Greydwarf Tamer Level", 15f, "NEEDS ALLTAMABLE MOD!. The level at which you are good at taming this creature. Reduces the amount of time by half");
            GreydwarfLevelMaster = base.Config.Bind<float>("3. CreatureLevels: AllTamableCompatibility", "Greydwarf Master Level", 25f, "NEEDS ALLTAMABLE MOD!. The level at which you are a Master taming this creature. You tame the creature in 2 minutes time");
            addAdvancedCreatureTameLevels(Greydwarf_Creature_Name,
               GreydwarfLevelUnlock, GreydwarfLevelTamer, GreydwarfLevelMaster);
            //Greydwarf Brute
            GreydwarfBruteLevelUnlock = base.Config.Bind<float>("3. CreatureLevels: AllTamableCompatibility", "Greydwarf Brute Unlock Level", 15f, "NEEDS ALLTAMABLE MOD!. The level at which you know how to tame this creature.");
            GreydwarfBruteLevelTamer = base.Config.Bind<float>("3. CreatureLevels: AllTamableCompatibility", "Greydwarf Brute Tamer Level", 25f, "NEEDS ALLTAMABLE MOD!. The level at which you are good at taming this creature. Reduces the amount of time by half");
            GreydwarfBruteLevelMaster = base.Config.Bind<float>("3. CreatureLevels: AllTamableCompatibility", "Greydwarf Brute Master Level", 35f, "NEEDS ALLTAMABLE MOD!. The level at which you are a Master taming this creature. You tame the creature in 2 minutes time");
            addAdvancedCreatureTameLevels(GreydwarfBrute_Creature_Name,
               GreydwarfBruteLevelUnlock, GreydwarfBruteLevelTamer, GreydwarfBruteLevelMaster);
            //Greydwarf Shaman
            GreydwarfShamanLevelUnlock = base.Config.Bind<float>("3. CreatureLevels: AllTamableCompatibility", "Greydwarf Shaman Unlock Level", 10f, "NEEDS ALLTAMABLE MOD!. The level at which you know how to tame this creature.");
            GreydwarfShamanLevelTamer = base.Config.Bind<float>("3. CreatureLevels: AllTamableCompatibility", "Greydwarf Shaman Tamer Level", 30f, "NEEDS ALLTAMABLE MOD!. The level at which you are good at taming this creature. Reduces the amount of time by half");
            GreydwarfShamanLevelMaster = base.Config.Bind<float>("3. CreatureLevels: AllTamableCompatibility", "Greydwarf Shaman Master Level", 70f, "NEEDS ALLTAMABLE MOD!. The level at which you are a Master taming this creature. You tame the creature in 2 minutes time");
            addAdvancedCreatureTameLevels(GreydwarfShaman_Creature_Name,
               GreydwarfShamanLevelUnlock, GreydwarfShamanLevelTamer, GreydwarfShamanLevelMaster);
            //Greyling
            GreylingLevelUnlock = base.Config.Bind<float>("3. CreatureLevels: AllTamableCompatibility", "Greyling Unlock Level", 0f, "NEEDS ALLTAMABLE MOD!. The level at which you know how to tame this creature.");
            GreylingLevelTamer = base.Config.Bind<float>("3. CreatureLevels: AllTamableCompatibility", "Greyling Tamer Level", 10f, "NEEDS ALLTAMABLE MOD!. The level at which you are good at taming this creature. Reduces the amount of time by half");
            GreylingLevelMaster = base.Config.Bind<float>("3. CreatureLevels: AllTamableCompatibility", "Greyling Master Level", 20f, "NEEDS ALLTAMABLE MOD!. The level at which you are a Master taming this creature. You tame the creature in 2 minutes time");
            addAdvancedCreatureTameLevels(Greyling_Creature_Name,
               GreylingLevelUnlock, GreylingLevelTamer, GreylingLevelMaster);
            //Leech
            LeechLevelUnlock = base.Config.Bind<float>("3. CreatureLevels: AllTamableCompatibility", "Leech Unlock Level", 10f, "NEEDS ALLTAMABLE MOD!. The level at which you know how to tame this creature.");
            LeechLevelTamer = base.Config.Bind<float>("3. CreatureLevels: AllTamableCompatibility", "Leech Tamer Level", 30f, "NEEDS ALLTAMABLE MOD!. The level at which you are good at taming this creature. Reduces the amount of time by half");
            LeechLevelMaster = base.Config.Bind<float>("3. CreatureLevels: AllTamableCompatibility", "Leech Master Level", 70f, "NEEDS ALLTAMABLE MOD!. The level at which you are a Master taming this creature. You tame the creature in 2 minutes time");
            addAdvancedCreatureTameLevels(Leech_Creature_Name,
               LeechLevelUnlock, LeechLevelTamer, LeechLevelMaster);
            //Serpent
            SerpentLevelUnlock = base.Config.Bind<float>("3. CreatureLevels: AllTamableCompatibility", "Serpent Unlock Level", 30f, "NEEDS ALLTAMABLE MOD!. The level at which you know how to tame this creature.");
            SerpentLevelTamer = base.Config.Bind<float>("3. CreatureLevels: AllTamableCompatibility", "Serpent Tamer Level", 40f, "NEEDS ALLTAMABLE MOD!. The level at which you are good at taming this creature. Reduces the amount of time by half");
            SerpentLevelMaster = base.Config.Bind<float>("3. CreatureLevels: AllTamableCompatibility", "Serpent Master Level", 50f, "NEEDS ALLTAMABLE MOD!. The level at which you are a Master taming this creature. You tame the creature in 2 minutes time");
            addAdvancedCreatureTameLevels(Serpent_Creature_Name,
               SerpentLevelUnlock, SerpentLevelTamer, SerpentLevelMaster);
            //Skeleton
            SkeletonLevelUnlock = base.Config.Bind<float>("3. CreatureLevels: AllTamableCompatibility", "Skeleton Unlock Level", 10f, "NEEDS ALLTAMABLE MOD!. The level at which you know how to tame this creature.");
            SkeletonLevelTamer = base.Config.Bind<float>("3. CreatureLevels: AllTamableCompatibility", "Skeleton Tamer Level", 20f, "NEEDS ALLTAMABLE MOD!. The level at which you are good at taming this creature. Reduces the amount of time by half");
            SkeletonLevelMaster = base.Config.Bind<float>("3. CreatureLevels: AllTamableCompatibility", "Skeleton Master Level", 30f, "NEEDS ALLTAMABLE MOD!. The level at which you are a Master taming this creature. You tame the creature in 2 minutes time");
            addAdvancedCreatureTameLevels(Skeleton_Creature_Name,
               SkeletonLevelUnlock, SkeletonLevelTamer, SkeletonLevelMaster);
            //Skeleton Poison
            SkeletonPoisonLevelUnlock = base.Config.Bind<float>("3. CreatureLevels: AllTamableCompatibility", "Skeleton Poison Unlock Level", 15f, "NEEDS ALLTAMABLE MOD!. The level at which you know how to tame this creature.");
            SkeletonPoisonLevelTamer = base.Config.Bind<float>("3. CreatureLevels: AllTamableCompatibility", "Skeleton Poison Tamer Level", 25f, "NEEDS ALLTAMABLE MOD!. The level at which you are good at taming this creature. Reduces the amount of time by half");
            SkeletonPoisonLevelMaster = base.Config.Bind<float>("3. CreatureLevels: AllTamableCompatibility", "Skeleton Poison Master Level", 35f, "NEEDS ALLTAMABLE MOD!. The level at which you are a Master taming this creature. You tame the creature in 2 minutes time");
            addAdvancedCreatureTameLevels(SkeletonPoison_Creature_Name,
               SkeletonPoisonLevelUnlock, SkeletonPoisonLevelTamer, SkeletonPoisonLevelMaster);
            //StoneGolem
            StoneGolemLevelUnlock = base.Config.Bind<float>("3. CreatureLevels: AllTamableCompatibility", "Stone Golem Unlock Level", 50f, "NEEDS ALLTAMABLE MOD!. The level at which you know how to tame this creature.");
            StoneGolemLevelTamer = base.Config.Bind<float>("3. CreatureLevels: AllTamableCompatibility", "Stone Golem Tamer Level", 60f, "NEEDS ALLTAMABLE MOD!. The level at which you are good at taming this creature. Reduces the amount of time by half");
            StoneGolemLevelMaster = base.Config.Bind<float>("3. CreatureLevels: AllTamableCompatibility", "Stone Golem Master Level", 70f, "NEEDS ALLTAMABLE MOD!. The level at which you are a Master taming this creature. You tame the creature in 2 minutes time");
            addAdvancedCreatureTameLevels(StoneGolem_Creature_Name,
               StoneGolemLevelUnlock, StoneGolemLevelTamer, StoneGolemLevelMaster);
            //Surtling
            SurtlingLevelUnlock = base.Config.Bind<float>("3. CreatureLevels: AllTamableCompatibility", "Surtling Unlock Level", 15f, "NEEDS ALLTAMABLE MOD!. The level at which you know how to tame this creature.");
            SurtlingLevelTamer = base.Config.Bind<float>("3. CreatureLevels: AllTamableCompatibility", "Surtling Tamer Level", 25f, "NEEDS ALLTAMABLE MOD!. The level at which you are good at taming this creature. Reduces the amount of time by half");
            SurtlingLevelMaster = base.Config.Bind<float>("3. CreatureLevels: AllTamableCompatibility", "Surtling Master Level", 35f, "NEEDS ALLTAMABLE MOD!. The level at which you are a Master taming this creature. You tame the creature in 2 minutes time");
            addAdvancedCreatureTameLevels(Surtling_Creature_Name,
               SurtlingLevelUnlock, SurtlingLevelTamer, SurtlingLevelMaster);
            //Troll
            TrollLevelUnlock = base.Config.Bind<float>("3. CreatureLevels: AllTamableCompatibility", "Troll Unlock Level", 15f, "NEEDS ALLTAMABLE MOD!. The level at which you know how to tame this creature.");
            TrollLevelTamer = base.Config.Bind<float>("3. CreatureLevels: AllTamableCompatibility", "Troll Tamer Level", 25f, "NEEDS ALLTAMABLE MOD!. The level at which you are good at taming this creature. Reduces the amount of time by half");
            TrollLevelMaster = base.Config.Bind<float>("3. CreatureLevels: AllTamableCompatibility", "Troll Master Level", 35f, "NEEDS ALLTAMABLE MOD!. The level at which you are a Master taming this creature. You tame the creature in 2 minutes time");
            addAdvancedCreatureTameLevels(Troll_Creature_Name,
               TrollLevelUnlock, TrollLevelTamer, TrollLevelMaster);
            //Wraith
            WraithLevelUnlock = base.Config.Bind<float>("3. CreatureLevels: AllTamableCompatibility", "Wraith Unlock Level", 35f, "NEEDS ALLTAMABLE MOD!. The level at which you know how to tame this creature.");
            WraithLevelTamer = base.Config.Bind<float>("3. CreatureLevels: AllTamableCompatibility", "Wraith Tamer Level", 45f, "NEEDS ALLTAMABLE MOD!. The level at which you are good at taming this creature. Reduces the amount of time by half");
            WraithLevelMaster = base.Config.Bind<float>("3. CreatureLevels: AllTamableCompatibility", "Wraith Master Level", 55f, "NEEDS ALLTAMABLE MOD!. The level at which you are a Master taming this creature. You tame the creature in 2 minutes time");
            addAdvancedCreatureTameLevels(Wraith_Creature_Name,
               WraithLevelUnlock, WraithLevelTamer, WraithLevelMaster);
            //Eikthyr
            EikthyrLevelUnlock = base.Config.Bind<float>("3. CreatureLevels: AllTamableCompatibility", "Eikthyr Unlock Level", 40f, "NEEDS ALLTAMABLE MOD!. The level at which you know how to tame this creature.");
            EikthyrLevelTamer = base.Config.Bind<float>("3. CreatureLevels: AllTamableCompatibility", "Eikthyr Tamer Level", 60f, "NEEDS ALLTAMABLE MOD!. The level at which you are good at taming this creature. Reduces the amount of time by half");
            EikthyrLevelMaster = base.Config.Bind<float>("3. CreatureLevels: AllTamableCompatibility", "Eikthyr Master Level", 80f, "NEEDS ALLTAMABLE MOD!. The level at which you are a Master taming this creature. You tame the creature in 2 minutes time");
            addAdvancedCreatureTameLevels(Eikthyr_Creature_Name,
               EikthyrLevelUnlock, EikthyrLevelTamer, EikthyrLevelMaster);
            //Elder
            ElderLevelUnlock = base.Config.Bind<float>("3. CreatureLevels: AllTamableCompatibility", "The Elder Unlock Level", 45f, "NEEDS ALLTAMABLE MOD!. The level at which you know how to tame this creature.");
            ElderLevelTamer = base.Config.Bind<float>("3. CreatureLevels: AllTamableCompatibility", "The Elder Tamer Level", 65f, "NEEDS ALLTAMABLE MOD!. The level at which you are good at taming this creature. Reduces the amount of time by half");
            ElderLevelMaster = base.Config.Bind<float>("3. CreatureLevels: AllTamableCompatibility", "The Elder Master Level", 85f, "NEEDS ALLTAMABLE MOD!. The level at which you are a Master taming this creature. You tame the creature in 2 minutes time");
            addAdvancedCreatureTameLevels(Elder_Creature_Name,
               ElderLevelUnlock, ElderLevelTamer, ElderLevelMaster);
            //BoneMass
            BonemassLevelUnlock = base.Config.Bind<float>("3. CreatureLevels: AllTamableCompatibility", "Bonemass Unlock Level", 50f, "NEEDS ALLTAMABLE MOD!. The level at which you know how to tame this creature.");
            BonemassLevelTamer = base.Config.Bind<float>("3. CreatureLevels: AllTamableCompatibility", "Bonemass Tamer Level", 70f, "NEEDS ALLTAMABLE MOD!. The level at which you are good at taming this creature. Reduces the amount of time by half");
            BonemassLevelMaster = base.Config.Bind<float>("3. CreatureLevels: AllTamableCompatibility", "Bonemass Master Level", 90f, "NEEDS ALLTAMABLE MOD!. The level at which you are a Master taming this creature. You tame the creature in 2 minutes time");
            addAdvancedCreatureTameLevels(Bonemass_Creature_Name,
               BonemassLevelUnlock, BonemassLevelTamer, BonemassLevelMaster);
            //Dragon
            DragonLevelUnlock = base.Config.Bind<float>("3. CreatureLevels: AllTamableCompatibility", "Dragon Unlock Level", 55f, "NEEDS ALLTAMABLE MOD!. The level at which you know how to tame this creature.");
            DragonLevelTamer = base.Config.Bind<float>("3. CreatureLevels: AllTamableCompatibility", "Dragon Tamer Level", 75f, "NEEDS ALLTAMABLE MOD!. The level at which you are good at taming this creature. Reduces the amount of time by half");
            DragonLevelMaster = base.Config.Bind<float>("3. CreatureLevels: AllTamableCompatibility", "Dragon Master Level", 95f, "NEEDS ALLTAMABLE MOD!. The level at which you are a Master taming this creature. You tame the creature in 2 minutes time");
            addAdvancedCreatureTameLevels(Dragon_Creature_Name,
               DragonLevelUnlock, DragonLevelTamer, DragonLevelMaster);
            //Goblin King
            GoblinKingLevelUnlock = base.Config.Bind<float>("3. CreatureLevels: AllTamableCompatibility", "Greydwarf Unlock Level", 60f, "NEEDS ALLTAMABLE MOD!. The level at which you know how to tame this creature.");
            GoblinKingLevelTamer = base.Config.Bind<float>("3. CreatureLevels: AllTamableCompatibility", "Greydwarf Tamer Level", 80f, "NEEDS ALLTAMABLE MOD!. The level at which you are good at taming this creature. Reduces the amount of time by half");
            GoblinKingLevelMaster = base.Config.Bind<float>("3. CreatureLevels: AllTamableCompatibility", "Greydwarf Master Level", 100f, "NEEDS ALLTAMABLE MOD!. The level at which you are a Master taming this creature. You tame the creature in 2 minutes time");
            addAdvancedCreatureTameLevels(GoblinKing_Creature_Name,
               GoblinKingLevelUnlock, GoblinKingLevelTamer, GoblinKingLevelMaster);

            //RRRGDThronweaver
            RRRGDThornweaverLevelUnlock = base.Config.Bind<float>("4. CreatureLevels: AllTamableCompatibility + RRRMonsters", "Greydwarf Thornweaver Unlock Level", 20f, "NEEDS ALLTAMABLE MOD!. The level at which you know how to tame this creature.");
            RRRGDThornweaverLevelTamer = base.Config.Bind<float>("4. CreatureLevels: AllTamableCompatibility + RRRMonsters", "Greydwarf Thornweaver Tamer Level", 30f, "NEEDS ALLTAMABLE MOD!. The level at which you are good at taming this creature. Reduces the amount of time by half");
            RRRGDThornweaverLevelMaster = base.Config.Bind<float>("4. CreatureLevels: AllTamableCompatibility + RRRMonsters", "Greydwarf Thornweaver Master Level", 40f, "NEEDS ALLTAMABLE MOD!. The level at which you are a Master taming this creature. You tame the creature in 2 minutes time");
            addAdvancedCreatureTameLevels(RRRGDThornweaver_Creature_Name,
               RRRGDThornweaverLevelUnlock, RRRGDThornweaverLevelTamer, RRRGDThornweaverLevelMaster);
            //RRRGhostVengeful
            RRRGhostVengefulLevelUnlock = base.Config.Bind<float>("4. CreatureLevels: AllTamableCompatibility + RRRMonsters", "Ghost Vengeful Unlock Level", 20f, "NEEDS ALLTAMABLE MOD!. The level at which you know how to tame this creature.");
            RRRGhostVengefulLevelTamer = base.Config.Bind<float>("4. CreatureLevels: AllTamableCompatibility + RRRMonsters", "Ghost Vengeful Tamer Level", 30f, "NEEDS ALLTAMABLE MOD!. The level at which you are good at taming this creature. Reduces the amount of time by half");
            RRRGhostVengefulLevelMaster = base.Config.Bind<float>("4. CreatureLevels: AllTamableCompatibility + RRRMonsters", "Ghost Vengeful Master Level", 40f, "NEEDS ALLTAMABLE MOD!. The level at which you are a Master taming this creature. You tame the creature in 2 minutes time");
            addAdvancedCreatureTameLevels(RRRGhostVengeful_Creature_Name,
               RRRGhostVengefulLevelUnlock, RRRGhostVengefulLevelTamer, RRRGhostVengefulLevelMaster);
            //RRRTrollTosser
            RRRTrollTosserLevelUnlock = base.Config.Bind<float>("4. CreatureLevels: AllTamableCompatibility + RRRMonsters", "Troll Tosser Unlock Level", 20f, "NEEDS ALLTAMABLE MOD!. The level at which you know how to tame this creature.");
            RRRTrollTosserLevelTamer = base.Config.Bind<float>("4. CreatureLevels: AllTamableCompatibility + RRRMonsters", "Troll Tosser Tamer Level", 30f, "NEEDS ALLTAMABLE MOD!. The level at which you are good at taming this creature. Reduces the amount of time by half");
            RRRTrollTosserLevelMaster = base.Config.Bind<float>("4. CreatureLevels: AllTamableCompatibility + RRRMonsters", "Troll Tosser Master Level", 40f, "NEEDS ALLTAMABLE MOD!. The level at which you are a Master taming this creature. You tame the creature in 2 minutes time");
            addAdvancedCreatureTameLevels(RRRTrollTosser_Creature_Name,
               RRRTrollTosserLevelUnlock, RRRTrollTosserLevelTamer, RRRTrollTosserLevelMaster);

            //Inject.Strength
            if (EnableTamingSkill.Value)
                try
                {
                    SkillInjector.RegisterNewSkill(705, "Taming", "You're gonna get better at controling and taming these beasts.", 1f, SkillIcons.Load_TamingIcon(), Skills.SkillType.Unarmed);
                }
                catch (Exception e)
                {
                    Utilities.LogError("Error Registering new Skill 'Taming'" + e.Message);
                }

            //--
            Utilities.Log("Taming Skill Patched!");
            harmonyTaming = new Harmony("MoreSkills.TamingConfig.GuiriGuyMods");

            //Logs
            if (!EnableTamingSkill.Value)
                Utilities.LogWarning("Taming Skill Mod Disabled");
            else
            {
                Utilities.Log("Taming Skill Mod Enabled");
                if (!EnableAllTamableCompatibility.Value)
                {
                    Utilities.LogWarning("Taming/AllTamableCompatibility Disabled");
                    Utilities.LogWarning("IF YOU HAVE ALLTAMABLECOMPATIBILTY (by buzz) INSTALLED, I WOULD RECOMMEND ACTIVATING THIS IN THE CONFIG, OTHERWISE EVERYTHING IS OKAY :D");
                }
                else
                    Utilities.Log("Taming/AllTamableCompatibility Enabled");
                if (!EnableTamingUnlocks.Value)
                    Utilities.LogWarning("Taming/Unlocks Mod Disabled");
                else
                    Utilities.Log("Taming/Unlocks Mod Enabled");
                if (!EnableTamingTimeMod.Value)
                    Utilities.LogWarning("Taming/Time Mod Disabled");
                else
                    Utilities.Log("Taming/Time Mod Enabled");
                if (!EnableTamingEatMod.Value)
                    Utilities.LogWarning("Taming/Eat Mod Disabled");
                else
                    Utilities.Log("Taming/Eat Mod Enabled");
            }
            Utilities.Log("Taming Skill Loaded!");
        }
        private void OnDestroy()
        {

            Utilities.Log("Taming Skill UnPatched!");
            harmonyTaming.UnpatchSelf();
        }

        private void addCreatureTameLevels(string creatureName,
            ConfigEntry<float> unlockLevel,
            ConfigEntry<float> tamerLevel,
            ConfigEntry<float> masterLevel)
        {
            CreatureTameLevelDirectory.Add(creatureName,
                new Helper.CreatureTameLevels(creatureName,
                unlockLevel.Value, tamerLevel.Value, masterLevel.Value));
            AdvancedCreatureTameLevelDirectory.Add(creatureName,
                new Helper.CreatureTameLevels(creatureName,
                unlockLevel.Value, tamerLevel.Value, masterLevel.Value));
        }

        private void addAdvancedCreatureTameLevels(string creatureName,
            ConfigEntry<float> unlockLevel,
            ConfigEntry<float> tamerLevel,
            ConfigEntry<float> masterLevel)
        {
            AdvancedCreatureTameLevelDirectory.Add(creatureName,
                new Helper.CreatureTameLevels(creatureName,
                unlockLevel.Value, tamerLevel.Value, masterLevel.Value));
        }

        private Harmony harmonyTaming;


        // Log config

        public static ConfigEntry<bool> EnableDetailedLogging;

        //Skill Increases Multpliers

        public static ConfigEntry<float> TamingSkillIncreaseMultiplier;

        //Enables

        public static ConfigEntry<bool> EnableTamingSkill;

        public static ConfigEntry<bool> EnableTamingEatMod;

        public static ConfigEntry<bool> EnableTamingTimeMod;

        public static ConfigEntry<bool> EnableAllTamableCompatibility;

        public static ConfigEntry<bool> EnableTamingUnlocks;

        public static ConfigEntry<bool> EnableCustomNames;

        //Skills Types

        public const int TamingSkill_Type = 705;

        // dictionary of creature taming levels added by Machara Stormwing
        public static Dictionary<String, Helper.CreatureTameLevels> CreatureTameLevelDirectory;
        public static Dictionary<String, Helper.CreatureTameLevels> AdvancedCreatureTameLevelDirectory;

        //3. CreatureLevels
        public static string Boar_Creature_Name = "Boar";
        public static ConfigEntry<float> BoarLevelUnlock;
        public static ConfigEntry<float> BoarLevelTamer;
        public static ConfigEntry<float> BoarLevelMaster;

        public static string Wolf_Creature_Name = "Wolf";
        public static ConfigEntry<float> WolfLevelUnlock;
        public static ConfigEntry<float> WolfLevelTamer;
        public static ConfigEntry<float> WolfLevelMaster;

        public static string Lox_Creature_Name = "Lox";
        public static ConfigEntry<float> LoxLevelUnlock;
        public static ConfigEntry<float> LoxLevelTamer;
        public static ConfigEntry<float> LoxLevelMaster;
        

        public static string Unknown_Creature_Name = "UnknownCreature";
        public static ConfigEntry<float> Unknown_LevelUnlock;
        public static ConfigEntry<float> Unknown_LevelTamer;
        public static ConfigEntry<float> Unknown_LevelMaster;


        public static string Neck_Creature_Name = "Neck";
        public static ConfigEntry<float> NeckLevelUnlock;
        public static ConfigEntry<float> NeckLevelTamer;
        public static ConfigEntry<float> NeckLevelMaster;

        public static string Deer_Creature_Name = "Deer";
        public static ConfigEntry<float> DeerLevelUnlock;
        public static ConfigEntry<float> DeerLevelTamer;
        public static ConfigEntry<float> DeerLevelMaster;

        public static string Blob_Creature_Name = "Blob";
        public static ConfigEntry<float> BlobLevelUnlock;
        public static ConfigEntry<float> BlobLevelTamer;
        public static ConfigEntry<float> BlobLevelMaster;

        public static string BlobElite_Creature_Name = "BlobElite";
        public static ConfigEntry<float> BlobEliteLevelUnlock;
        public static ConfigEntry<float> BlobEliteLevelTamer;
        public static ConfigEntry<float> BlobEliteLevelMaster;

        public static string Deathsquito_Creature_Name = "Deathsquito";
        public static ConfigEntry<float> DeathsquitoLevelUnlock;
        public static ConfigEntry<float> DeathsquitoLevelTamer;
        public static ConfigEntry<float> DeathsquitoLevelMaster;

        public static string Drake_Creature_Name = "Hatchling";
        public static ConfigEntry<float> DrakeLevelUnlock;
        public static ConfigEntry<float> DrakeLevelTamer;
        public static ConfigEntry<float> DrakeLevelMaster;

        public static string Draugr_Creature_Name = "Draugr";
        public static ConfigEntry<float> DraugrLevelUnlock;
        public static ConfigEntry<float> DraugrLevelTamer;
        public static ConfigEntry<float> DraugrLevelMaster;

        public static string DraugrElite_Creature_Name = "Draugr_Elite";
        public static ConfigEntry<float> DraugrEliteLevelUnlock;
        public static ConfigEntry<float> DraugrEliteLevelTamer;
        public static ConfigEntry<float> DraugrEliteLevelMaster;

        public static string Fenring_Creature_Name = "Fenring";
        public static ConfigEntry<float> FenringLevelUnlock;
        public static ConfigEntry<float> FenringLevelTamer;
        public static ConfigEntry<float> FenringLevelMaster;

        public static string Ghost_Creature_Name = "Ghost";
        public static ConfigEntry<float> GhostLevelUnlock;
        public static ConfigEntry<float> GhostLevelTamer;
        public static ConfigEntry<float> GhostLevelMaster;

        public static string Goblin_Creature_Name = "Goblin";
        public static ConfigEntry<float> GoblinLevelUnlock;
        public static ConfigEntry<float> GoblinLevelTamer;
        public static ConfigEntry<float> GoblinLevelMaster;

        public static string GoblinBrute_Creature_Name = "GoblinBrute";
        public static ConfigEntry<float> GoblinBruteLevelUnlock;
        public static ConfigEntry<float> GoblinBruteLevelTamer;
        public static ConfigEntry<float> GoblinBruteLevelMaster;

        public static string GoblinShaman_Creature_Name = "GoblinShaman";
        public static ConfigEntry<float> GoblinShamanLevelUnlock;
        public static ConfigEntry<float> GoblinShamanLevelTamer;
        public static ConfigEntry<float> GoblinShamanLevelMaster;

        public static string Greydwarf_Creature_Name = "Greydwarf";
        public static ConfigEntry<float> GreydwarfLevelUnlock;
        public static ConfigEntry<float> GreydwarfLevelTamer;
        public static ConfigEntry<float> GreydwarfLevelMaster;

        public static string GreydwarfBrute_Creature_Name = "Greydwarf_Elite";
        public static ConfigEntry<float> GreydwarfBruteLevelUnlock;
        public static ConfigEntry<float> GreydwarfBruteLevelTamer;
        public static ConfigEntry<float> GreydwarfBruteLevelMaster;

        public static string GreydwarfShaman_Creature_Name = "Greydwarf_Shaman";
        public static ConfigEntry<float> GreydwarfShamanLevelUnlock;
        public static ConfigEntry<float> GreydwarfShamanLevelTamer;
        public static ConfigEntry<float> GreydwarfShamanLevelMaster;

        public static string Greyling_Creature_Name = "Greyling";
        public static ConfigEntry<float> GreylingLevelUnlock;
        public static ConfigEntry<float> GreylingLevelTamer;
        public static ConfigEntry<float> GreylingLevelMaster;

        public static string Leech_Creature_Name = "Leech";
        public static ConfigEntry<float> LeechLevelUnlock;
        public static ConfigEntry<float> LeechLevelTamer;
        public static ConfigEntry<float> LeechLevelMaster;

        public static string Serpent_Creature_Name = "Serpent";
        public static ConfigEntry<float> SerpentLevelUnlock;
        public static ConfigEntry<float> SerpentLevelTamer;
        public static ConfigEntry<float> SerpentLevelMaster;
        
        public static string Skeleton_Creature_Name = "Skeleton";
        public static ConfigEntry<float> SkeletonLevelUnlock;
        public static ConfigEntry<float> SkeletonLevelTamer;
        public static ConfigEntry<float> SkeletonLevelMaster;

        public static string SkeletonPoison_Creature_Name = "Skeleton_Poison";
        public static ConfigEntry<float> SkeletonPoisonLevelUnlock;
        public static ConfigEntry<float> SkeletonPoisonLevelTamer;
        public static ConfigEntry<float> SkeletonPoisonLevelMaster;

        public static string StoneGolem_Creature_Name = "StoneGolem";
        public static ConfigEntry<float> StoneGolemLevelUnlock;
        public static ConfigEntry<float> StoneGolemLevelTamer;
        public static ConfigEntry<float> StoneGolemLevelMaster;

        public static string Surtling_Creature_Name = "Surtling";
        public static ConfigEntry<float> SurtlingLevelUnlock;
        public static ConfigEntry<float> SurtlingLevelTamer;
        public static ConfigEntry<float> SurtlingLevelMaster;

        public static string Troll_Creature_Name = "Troll";
        public static ConfigEntry<float> TrollLevelUnlock;
        public static ConfigEntry<float> TrollLevelTamer;
        public static ConfigEntry<float> TrollLevelMaster;

        public static string Wraith_Creature_Name = "Wraith";
        public static ConfigEntry<float> WraithLevelUnlock;
        public static ConfigEntry<float> WraithLevelTamer;
        public static ConfigEntry<float> WraithLevelMaster;

        public static string Eikthyr_Creature_Name = "Eikthyr";
        public static ConfigEntry<float> EikthyrLevelUnlock;
        public static ConfigEntry<float> EikthyrLevelTamer;
        public static ConfigEntry<float> EikthyrLevelMaster;

        public static string Elder_Creature_Name = "gd_king";
        public static ConfigEntry<float> ElderLevelUnlock;
        public static ConfigEntry<float> ElderLevelTamer;
        public static ConfigEntry<float> ElderLevelMaster;

        public static string Bonemass_Creature_Name = "Bonemass";
        public static ConfigEntry<float> BonemassLevelUnlock;
        public static ConfigEntry<float> BonemassLevelTamer;
        public static ConfigEntry<float> BonemassLevelMaster;

        public static string Dragon_Creature_Name = "Dragon";
        public static ConfigEntry<float> DragonLevelUnlock;
        public static ConfigEntry<float> DragonLevelTamer;
        public static ConfigEntry<float> DragonLevelMaster;

        public static string GoblinKing_Creature_Name = "GoblinKing";
        public static ConfigEntry<float> GoblinKingLevelUnlock;
        public static ConfigEntry<float> GoblinKingLevelTamer;
        public static ConfigEntry<float> GoblinKingLevelMaster;

        public static string RRRGDThornweaver_Creature_Name = "RRR_GDThornweaver";
        public static ConfigEntry<float> RRRGDThornweaverLevelUnlock;
        public static ConfigEntry<float> RRRGDThornweaverLevelTamer;
        public static ConfigEntry<float> RRRGDThornweaverLevelMaster;

        public static string RRRGhostVengeful_Creature_Name = "RRR_GhostVengeful";
        public static ConfigEntry<float> RRRGhostVengefulLevelUnlock;
        public static ConfigEntry<float> RRRGhostVengefulLevelTamer;
        public static ConfigEntry<float> RRRGhostVengefulLevelMaster;

        public static string RRRTrollTosser_Creature_Name = "RRR_TrollTosser";
        public static ConfigEntry<float> RRRTrollTosserLevelUnlock;
        public static ConfigEntry<float> RRRTrollTosserLevelTamer;
        public static ConfigEntry<float> RRRTrollTosserLevelMaster;

    }
}
