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
    [BepInPlugin(Plugin_Name, "MoreSkills", "0.1.9.2")]
    [BepInDependency("com.pipakin.SkillInjectorMod", BepInDependency.DependencyFlags.HardDependency)]
    public class MoreSkills_Config : BaseUnityPlugin
    {
        public const String Plugin_Name = "MoreSkills.AGeneralConfig";

        public void Awake()
        {
            /*All Drops Configs
            //Minerals
            //Stone
            MoreSkills_Config.BaseStoneChance = base.Config.Bind<float>("4. BaseConfigs: Minerals", "Stone Base Chance", 1f);
            //CopperOre
            MoreSkills_Config.BaseCopperOreChance = base.Config.Bind<float>("4. BaseConfigs: Minerals", "Copper Ore Base Chance", 1f);
            //IronScrapOre
            MoreSkills_Config.BaseIronScrapChance = base.Config.Bind<float>("4. BaseConfigs: Minerals", "Iron Scrap Base Chance", 0.2f);
            //TinOre
            MoreSkills_Config.BaseTinOreChance = base.Config.Bind<float>("4. BaseConfigs: Minerals", "Tin Ore Base Chance", 1f);
            //SilverOre
            MoreSkills_Config.BaseSilverOreChance = base.Config.Bind<float>("4. BaseConfigs: Minerals", "Silver Ore Base Chance", 1f);
            //Obsidian
            MoreSkills_Config.BaseObsidianChance = base.Config.Bind<float>("4. BaseConfigs: Minerals", "Obsidian Base Chance", 1f);
            //Chitin
            MoreSkills_Config.BaseChitinChance = base.Config.Bind<float>("4. BaseConfigs: Minerals", "Chitin Base Chance", 1f);
            //Woods
            //Wood
            MoreSkills_Config.BaseWoodChance = base.Config.Bind<float>("4. BaseConfigs: Woods", "Wood Base Chance", 1f);
            //FineWood
            MoreSkills_Config.BaseFineWoodChance = base.Config.Bind<float>("4. BaseConfigs: Woods", "Fine Wood Base Chance", 1f);
            //RoundLog
            MoreSkills_Config.BaseRoundLogChance = base.Config.Bind<float>("4. BaseConfigs: Woods", "Round Log Base Chance", 1f);
            //BeechSeeds
            MoreSkills_Config.BaseBeechSeedChance = base.Config.Bind<float>("4. BaseConfigs: Woods", "Beech Seed Base Chance", 0.5f);
            //FirCone
            MoreSkills_Config.BaseFirConeChance = base.Config.Bind<float>("4. BaseConfigs: Woods", "Fir Cone Base Chance", 0.5f);
            //PineCone
            MoreSkills_Config.BasePineConeChance = base.Config.Bind<float>("4. BaseConfigs: Woods", "Pine Cone Base Chance", 0.5f);
            //ElderBark
            MoreSkills_Config.BaseElderBarkChance = base.Config.Bind<float>("4. BaseConfigs: Woods", "Elder Bark Base Chance", 1f);
            //Resin
            MoreSkills_Config.BaseResinChance = base.Config.Bind<float>("4. BaseConfigs: Woods", "Resin Base Chance", 0.5f);
            //Others
            //Feathers
            MoreSkills_Config.BaseFeathersChance = base.Config.Bind<float>("4. BaseConfigs: Others", "Feathers Base Chance", 1f);
            //Guck
            MoreSkills_Config.BaseGuckChance = base.Config.Bind<float>("4. BaseConfigs: Others", "Guck Base Chance", 1f);
            //LeatherScraps
            MoreSkills_Config.BaseLeatherScrapsChance = base.Config.Bind<float>("4. BaseConfigs: Others", "Leather Scraps Base Chance", 0.2f);
            //WitheredBone
            MoreSkills_Config.BaseWitheredBoneChance = base.Config.Bind<float>("4. BaseConfigs: Others", "Withered Bone Base Chance", 0.2f);*/


            /*//Rock             OLD
            MoreSkills_Config.BaseMinRock = base.Config.Bind<int>("4. AllRocksConfig: zRocks", "Min Drop of Rocks", 3);
            MoreSkills_Config.BaseMaxRock = base.Config.Bind<int>("4. AllRocksConfig: zRocks", "Max Drop of Rocks", 6);
            //Big Rock
            MoreSkills_Config.BaseMinBigRock = base.Config.Bind<int>("4. AllRocksConfig: zBigRocks", "Min Drop of Big Rocks", 4);
            MoreSkills_Config.BaseMaxBigRock = base.Config.Bind<int>("4. AllRocksConfig: zBigRocks", "Max Drop of Big Rocks", 8);
            //Copper Vein
            MoreSkills_Config.BaseMinCopperVein = base.Config.Bind<int>("4. AllRocksConfig: zCopperVein", "Min Drop of Copper Veins", 2);
            MoreSkills_Config.BaseMaxCopperVein = base.Config.Bind<int>("4. AllRocksConfig: zCopperVein", "Max Drop of Copper Veins", 4);
            //Mudpile (Iron)
            MoreSkills_Config.BaseMinMudPile = base.Config.Bind<int>("4. AllRocksConfig: zMudPile", "Min Drop of Mud Piles (Iron)", 1);
            MoreSkills_Config.BaseMaxMudPile = base.Config.Bind<int>("4. AllRocksConfig: zMudPile", "Max Drop of Mud Piles (Iron)", 1);
            MoreSkills_Config.BaseChanceMudPile = base.Config.Bind<float>("4. AllRocksConfig: zMudPile", "Chance of Mud Piles (Iron)", 0.3f);
            //Silver Vein
            MoreSkills_Config.BaseMinSilverVein = base.Config.Bind<int>("4. AllRocksConfig: zSilverVein", "Min Drop of Silver Vein", 2);
            MoreSkills_Config.BaseMaxSilverVein = base.Config.Bind<int>("4. AllRocksConfig: zSilverVein", "Max Drop of Silver Vein", 3);*/
        }        

        //Test

        public static Dictionary<string, Texture2D> cachedTextures = new Dictionary<string, Texture2D>();

        /*//Drops Bases
        //Pickaxe Mod

        public static ConfigEntry<float> BaseStoneChance;

            public static ConfigEntry<float> BaseCopperOreChance;

            public static ConfigEntry<float> BaseTinOreChance;

            public static ConfigEntry<float> BaseIronScrapChance;

            public static ConfigEntry<float> BaseWitheredBoneChance;

            public static ConfigEntry<float> BaseSilverOreChance;

            public static ConfigEntry<float> BaseObsidianChance;

            public static ConfigEntry<float> BaseChitinChance;

            //Hunting Skill

            public static ConfigEntry<float> BaseLeatherScrapsChance;

            public static ConfigEntry<float> BaseFeathersChance;

            public static ConfigEntry<float> BaseGuckChance;

            //WoodCutting Mod

            public static ConfigEntry<float> BaseWoodChance;

            public static ConfigEntry<float> BaseFineWoodChance;

            public static ConfigEntry<float> BaseRoundLogChance;

            public static ConfigEntry<float> BaseResinChance;

            public static ConfigEntry<float> BaseBeechSeedChance;

            public static ConfigEntry<float> BasePineConeChance;

            public static ConfigEntry<float> BaseFirConeChance;

            public static ConfigEntry<float> BaseElderBarkChance;*/

        /*//Rocks Bases          OLD

        public static ConfigEntry<int> BaseMinRock;

        public static ConfigEntry<int> BaseMaxRock;

        public static ConfigEntry<int> BaseMinBigRock;

        public static ConfigEntry<int> BaseMaxBigRock;

        public static ConfigEntry<int> BaseMinCopperVein;

        public static ConfigEntry<int> BaseMaxCopperVein;

        public static ConfigEntry<int> BaseMinMudPile;

        public static ConfigEntry<int> BaseMaxMudPile;

        public static ConfigEntry<int> BaseMinSilverVein;

        public static ConfigEntry<int> BaseMaxSilverVein;

        //Rocks Chances

        public static ConfigEntry<float> BaseChanceMudPile;*/

    }
}
