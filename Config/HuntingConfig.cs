using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using MoreSkills.UI;
using Pipakin.SkillInjectorMod;
using System;
using UnityEngine;

namespace MoreSkills.Config
{
    [BepInPlugin("MoreSkills.HuntingConfig", "MoreSkills: Hunting", "0.0.4")]
    [BepInDependency("com.pipakin.SkillInjectorMod")]

    public class MoreSkills_HuntingConfig : BaseUnityPlugin
    {
        public void Awake()
        {
            Debug.Log("[MoreSkills] Loading Hunting Skill...");
            //1. 1. Enablers
            //Hunting
            EnableHuntingSkill = base.Config.Bind<bool>("1. Enablers", "Enable Hunting Skill", true, "Enables or disables the Hunting Skill Modification");
            //Normal Mobs
            EnableHuntingNormalMobsMod = base.Config.Bind<bool>("1. Enablers", "Enable Hunting Mobs Mod", true, "Enables or disables the Hunting Skill Modification to All Mobs");
            //Bosses
            //EnableHuntingBossMod = base.Config.Bind<bool>("1. Enablers", "Enable Hunting Bosses Mod", false, "Enables or disables the Hunting Skill Modification affect to Bosses. RECOMMENDED FOR EPIC LOOTS");
            //MinMax Mods
            EnableHuntingMinMaxMod = base.Config.Bind<bool>("1. Enablers", "Enable Hunting Min and Max Drop Mod", true, "Enables or disables the Hunting Skill Modification to all Enabled Mobs Min and Max Item Drops");
            //Chance Mod
            EnableHuntingChanceMod = base.Config.Bind<bool>("1. Enablers", "Enable Hunting Chance Mod", true, "Enables or disables the Hunting Skill Modification to the Chances of Droping an Item");
            //Trophy Mods
            EnableHuntingTrophyMod = base.Config.Bind<bool>("1. Enablers", "Enable Hunting Trophy Mod", false, "Enables or disables the Hunting Skill Modification to affect Trophies. RECOMMENDED FOR EPIC LOOTS");
            //Blob Affect
            EnableHuntingBlobSpawn = base.Config.Bind<bool>("1. Enablers", "Enable Hunting Blob Spawn Mod", false, "Enables or disables the Hunting Skill Modification to affect Blobs to count as ItemDrops from the Blob Elite");
            //ToO
            EnableTrialsOfOdinCompatibility = base.Config.Bind<bool>("1. Enablers", "Enable Compatibility with Trials of Odin Mod", false, "Enables or disables the Compatibility with Trials of Odin's Levels Mod");
            //Hunting
            //Skill
            HuntingSkillMultiplier = base.Config.Bind<float>("2. Multipliers", "Multiplies the Hunting Skill Increase", 1.0f, "The Skill Increase is based on the max Health of the Mob 1/20, so if the mob is killed and had a max health of 500 you get 50 (If you level up, it will only level you up and loose the rest of exp, to yet be fixed). This allows you to multiply this number.");
            //Drops
            HuntingDropMultiplier = base.Config.Bind<float>("2. Multipliers", "Multiplies the Hunting Drops", 1.5f, "The based on level 2. Multipliers, so at level 100 you reach such number. At level 100 you recieve x1.5 at default to the amount of Drops from a Mob/Boss. This multiplier changes that number");
            //BaseConfigs
            //ToO
            ToOLowLevelStart = base.Config.Bind<int>("3. BaseConfig", "Set Low Level Start Set at Trials of Odin", 1, "You have to put the number at which you set the low level at Trials of Odin Mod");
            ToOMidLevelStart = base.Config.Bind<int>("3. BaseConfig", "Set Mid Level Start Set at Trials of Odin", 5, "You have to put the number at which you set the low level at Trials of Odin Mod");
            ToOHighLevelStart = base.Config.Bind<int>("3. BaseConfig", "Set High Level Start Set at Trials of Odin", 8, "You have to put the number at which you set the low level at Trials of Odin Mod");
            //5. All Mobs Configs
            //Hunting Skill
            //Blob
            //Ooze
            /*BaseMinBlobOoze = base.Config.Bind<int>("3. AllMobConfig Blob", "Min Drop of Ooze", 1);
            BaseMaxBlobOoze = base.Config.Bind<int>("3. AllMobConfig Blob", "Max Drop of Ooze", 2);
            BaseChanceBlobOoze = base.Config.Bind<float>("3. AllMobConfig Blob", "Chance Drop of Ooze", 1f);
            BaseLevelMultiplierBlobOoze = base.Config.Bind<bool>("3. AllMobConfig Blob", "Drop Multiplier per Level of Ooze", true);
            //Trophy
            BaseMinBlobTrophy = base.Config.Bind<int>("3. AllMobConfig Blob", "Min Drop of BlobTrophy", 1);
            BaseMaxBlobTrophy = base.Config.Bind<int>("3. AllMobConfig Blob", "Max Drop of BlobTrophy", 1);
            BaseChanceBlobTrophy = base.Config.Bind<float>("3. AllMobConfig Blob", "Chance Drop of BlobTrophy", 0.1f);
            BaseLevelMultiplierBlobTrophy = base.Config.Bind<bool>("3. AllMobConfig Blob", "Drop Multiplier per Level of BlobTrophy", false);
            //BlobElite
            //Ooze
            BaseMinBlobEliteOoze = base.Config.Bind<int>("3. AllMobConfig Blob Elite", "Min Drop of Ooze", 2);
            BaseMaxBlobEliteOoze = base.Config.Bind<int>("3. AllMobConfig Blob Elite", "Max Drop of Ooze", 3);
            BaseChanceBlobEliteOoze = base.Config.Bind<float>("3. AllMobConfig Blob Elite", "Chance Drop of Ooze", 1f);
            BaseLevelMultiplierBlobEliteOoze = base.Config.Bind<bool>("3. AllMobConfig Blob Elite", "Drop Multiplier per Level of Ooze", true);
            //IronScrap
            BaseMinBlobEliteIronScrap = base.Config.Bind<int>("3. AllMobConfig Blob Elite", "Min Drop of Iron Scrap", 1);
            BaseMaxBlobEliteIronScrap = base.Config.Bind<int>("3. AllMobConfig Blob Elite", "Max Drop of Iron Scrap", 1);
            BaseChanceBlobEliteIronScrap = base.Config.Bind<float>("3. AllMobConfig Blob Elite", "Chance Drop of Iron Scrap", 0.33f);
            BaseLevelMultiplierBlobEliteIronScrap = base.Config.Bind<bool>("3. AllMobConfig Blob Elite", "Drop Multiplier per Level of Iron Scrap", true);
            //Trophy same as Blob
            //Blob
            BaseMinBlobEliteBlob = base.Config.Bind<int>("3. AllMobConfig Blob Elite", "Min Drop of Blobs (Mob)", 2);
            BaseMaxBlobEliteBlob = base.Config.Bind<int>("3. AllMobConfig Blob Elite", "Max Drop of Blobs (Mob)", 2);
            BaseChanceBlobEliteBlob = base.Config.Bind<float>("3. AllMobConfig Blob Elite", "Chance Drop of Blobs (Mob)", 1f);
            BaseLevelMultiplierBlobEliteBlob = base.Config.Bind<bool>("3. AllMobConfig Blob Elite", "Drop Multiplier per Level of Blobs (Mob)", true);
            //Boar
            //RawMeat
            BaseMinBoarRawMeat = base.Config.Bind<int>("3. AllMobConfig Boar", "Min Drop of RawMeat", 1);
            BaseMaxBoarRawMeat = base.Config.Bind<int>("3. AllMobConfig Boar", "Max Drop of RawMeat", 1);
            BaseChanceBoarRawMeat = base.Config.Bind<float>("3. AllMobConfig Boar", "Chance Drop of RawMeat", 0.5f);
            BaseLevelMultiplierBoarRawMeat = base.Config.Bind<bool>("3. AllMobConfig Boar", "Drop Multiplier per Level of RawMeat", true);
            //Leather Scraps
            BaseMinBoarLeatherScraps = base.Config.Bind<int>("3. AllMobConfig Boar", "Min Drop of Leather Scraps", 1);
            BaseMaxBoarLeatherScraps = base.Config.Bind<int>("3. AllMobConfig Boar", "Max Drop of Leather Scraps", 1);
            BaseChanceBoarLeatherScraps = base.Config.Bind<float>("3. AllMobConfig Boar", "Chance Drop of Leather Scraps", 1f);
            BaseLevelMultiplierBoarLeatherScraps = base.Config.Bind<bool>("3. AllMobConfig Boar", "Drop Multiplier per Level of Leather Scraps", true);
            //Trophy
            BaseMinBoarTrophy = base.Config.Bind<int>("3. AllMobConfig Boar", "Min Drop of Trophy", 1);
            BaseMaxBoarTrophy = base.Config.Bind<int>("3. AllMobConfig Boar", "Max Drop of Trophy", 1);
            BaseChanceBoarTrophy = base.Config.Bind<float>("3. AllMobConfig Boar", "Chance Drop of Trophy", 0.15f);
            BaseLevelMultiplierBoarTrophy = base.Config.Bind<bool>("3. AllMobConfig Boar", "Drop Multiplier per Level of Trophy", false);
            //Deathsquito
            //Needle
            BaseMinDeathsquitoNeedle = base.Config.Bind<int>("3. AllMobConfig Deathsquito", "Min Drop of Needle", 1);
            BaseMaxDeathsquitoNeedle = base.Config.Bind<int>("3. AllMobConfig Deathsquito", "Max Drop of Needle", 1);
            BaseChanceDeathsquitoNeedle = base.Config.Bind<float>("3. AllMobConfig Deathsquito", "Chance Drop of Needle", 1f);
            BaseLevelMultiplierDeathsquitoNeedle = base.Config.Bind<bool>("3. AllMobConfig Deathsquito", "Drop Multiplier per Level of Needle", false);
            //Trophy
            BaseMinDeathsquitoTrophy = base.Config.Bind<int>("3. AllMobConfig Deathsquito", "Min Drop of Trophy", 1);
            BaseMaxDeathsquitoTrophy = base.Config.Bind<int>("3. AllMobConfig Deathsquito", "Max Drop of Trophy", 1);
            BaseChanceDeathsquitoTrophy = base.Config.Bind<float>("3. AllMobConfig Deathsquito", "Chance Drop of Trophy", 0.05f);
            BaseLevelMultiplierDeathsquitoTrophy = base.Config.Bind<bool>("3. AllMobConfig Deathsquito", "Drop Multiplier per Level of Trophy", false);
            //Deer
            //Raw Meat
            BaseMinDeerRawMeat = base.Config.Bind<int>("3. AllMobConfig Deer", "Min Drop of Raw Meat", 2);
            BaseMaxDeerRawMeat = base.Config.Bind<int>("3. AllMobConfig Deer", "Max Drop of Raw Meat", 2);
            BaseChanceDeerRawMeat = base.Config.Bind<float>("3. AllMobConfig Deer", "Chance Drop of Raw Meat", 1f);
            BaseLevelMultiplierDeerRawMeat = base.Config.Bind<bool>("3. AllMobConfig Deer", "Drop Multiplier per Level of Raw Meat", true);
            //Deer Hide
            BaseMinDeerHide = base.Config.Bind<int>("3. AllMobConfig Deer", "Min Drop of Deer Hide", 1);
            BaseMaxDeerHide = base.Config.Bind<int>("3. AllMobConfig Deer", "Max Drop of Deer Hide", 3);
            BaseChanceDeerHide = base.Config.Bind<float>("3. AllMobConfig Deer", "Chance Drop of Deer Hide", 1f);
            BaseLevelMultiplierDeerHide = base.Config.Bind<bool>("3. AllMobConfig Deer", "Drop Multiplier per Level of Deer Hide", true);
            //Trophy
            BaseMinDeerTrophy = base.Config.Bind<int>("3. AllMobConfig Deer", "Min Drop of Trophy", 1);
            BaseMaxDeerTrophy = base.Config.Bind<int>("3. AllMobConfig Deer", "Max Drop of Trophy", 1);
            BaseChanceDeerTrophy = base.Config.Bind<float>("3. AllMobConfig Deer", "Chance Drop of Trophy", 0.5f);
            BaseLevelMultiplierDeerTrophy = base.Config.Bind<bool>("3. AllMobConfig Deer", "Drop Multiplier per Level of Trophy", false);
            //Drake
            //Freeze Gland
            BaseMinDrakeFreezeGland = base.Config.Bind<int>("3. AllMobConfig Drake", "Min Drop of Freeze Gland", 1);
            BaseMaxDrakeFreezeGland = base.Config.Bind<int>("3. AllMobConfig Drake", "Max Drop of Freeze Gland", 2);
            BaseChanceDrakeFreezeGland = base.Config.Bind<float>("3. AllMobConfig Drake", "Chance Drop of Freeze Gland", 1f);
            BaseLevelMultiplierDrakeFreezeGland = base.Config.Bind<bool>("3. AllMobConfig Drake", "Drop Multiplier per Level of Freeze Gland", true);
            //Trophy
            BaseMinDrakeTrophy = base.Config.Bind<int>("3. AllMobConfig Drake", "Min Drop of Trophy", 1);
            BaseMaxDrakeTrophy = base.Config.Bind<int>("3. AllMobConfig Drake", "Max Drop of Trophy", 1);
            BaseChanceDrakeTrophy = base.Config.Bind<float>("3. AllMobConfig Drake", "Chance Drop of Trophy", 0.1f);
            BaseLevelMultiplierDrakeTrophy = base.Config.Bind<bool>("3. AllMobConfig Drake", "Drop Multiplier per Level of Trophy", false);
            //Draugr
            //Entrails
            BaseMinDraugrEntrails = base.Config.Bind<int>("3. AllMobConfig Draugr", "Min Drop of Entrails", 1);
            BaseMaxDraugrEntrails = base.Config.Bind<int>("3. AllMobConfig Draugr", "Max Drop of Entrails", 1);
            BaseChanceDraugrEntrails = base.Config.Bind<float>("3. AllMobConfig Draugr", "Chance Drop of Entrails", 1f);
            BaseLevelMultiplierDraugrEntrails = base.Config.Bind<bool>("3. AllMobConfig Draugr", "Drop Multiplier per Level of Entrails", true);
            //Trophy
            BaseMinDraugrTrophy = base.Config.Bind<int>("3. AllMobConfig Draugr", "Min Drop of Trophy", 1);
            BaseMaxDraugrTrophy = base.Config.Bind<int>("3. AllMobConfig Draugr", "Max Drop of Trophy", 1);
            BaseChanceDraugrTrophy = base.Config.Bind<float>("3. AllMobConfig Draugr", "Chance Drop of Trophy", 0.1f);
            BaseLevelMultiplierDraugrTrophy = base.Config.Bind<bool>("3. AllMobConfig Draugr", "Drop Multiplier per Level of Trophy", false);
            //Draugr Elite
            //Entrails
            BaseMinDraugrEliteEntrails = base.Config.Bind<int>("3. AllMobConfig Draugr Elite", "Min Drop of Entrails", 2);
            BaseMaxDraugrEliteEntrails = base.Config.Bind<int>("3. AllMobConfig Draugr Elite", "Max Drop of Entrails", 3);
            BaseChanceDraugrEliteEntrails = base.Config.Bind<float>("3. AllMobConfig Draugr Elite", "Chance Drop of Entrails", 1f);
            BaseLevelMultiplierDraugrEliteEntrails = base.Config.Bind<bool>("3. AllMobConfig Draugr Elite", "Drop Multiplier per Level of Entrails", true);
            //Trophy
            BaseMinDraugrEliteTrophy = base.Config.Bind<int>("3. AllMobConfig Draugr Elite", "Min Drop of Trophy", 1);
            BaseMaxDraugrEliteTrophy = base.Config.Bind<int>("3. AllMobConfig Draugr Elite", "Max Drop of Trophy", 1);
            BaseChanceDraugrEliteTrophy = base.Config.Bind<float>("3. AllMobConfig Draugr Elite", "Chance Drop of Trophy", 0.1f);
            BaseLevelMultiplierDraugrEliteTrophy = base.Config.Bind<bool>("3. AllMobConfig Draugr Elite", "Drop Multiplier per Level of Trophy", false);
            //Fenring
            //WolfFang
            BaseMinFenringWolfFang = base.Config.Bind<int>("3. AllMobConfig Fenring", "Min Drop of WolfFang", 1);
            BaseMaxFenringWolfFang = base.Config.Bind<int>("3. AllMobConfig Fenring", "Max Drop of WolfFang", 2);
            BaseChanceFenringWolfFang = base.Config.Bind<float>("3. AllMobConfig Fenring", "Chance Drop of WolfFang", 1f);
            BaseLevelMultiplierFenringWolfFang = base.Config.Bind<bool>("3. AllMobConfig Fenring", "Drop Multiplier per Level of WolfFang", true);
            //Trophy
            BaseMinFenringEliteTrophy = base.Config.Bind<int>("3. AllMobConfig Fenring", "Min Drop of Trophy", 1);
            BaseMaxFenringEliteTrophy = base.Config.Bind<int>("3. AllMobConfig Fenring", "Max Drop of Trophy", 1);
            BaseChanceFenringEliteTrophy = base.Config.Bind<float>("3. AllMobConfig Fenring", "Chance Drop of Trophy", 0.1f);
            BaseLevelMultiplierFenringEliteTrophy = base.Config.Bind<bool>("3. AllMobConfig Fenring", "Drop Multiplier per Level of Trophy", false);
            //Goblin
            //Coins
            BaseMinGoblinCoins = base.Config.Bind<int>("3. AllMobConfig Goblin", "Min Drop of Coins", 5);
            BaseMaxGoblinCoins = base.Config.Bind<int>("3. AllMobConfig Goblin", "Max Drop of Coins", 10);
            BaseChanceGoblinCoins = base.Config.Bind<float>("3. AllMobConfig Goblin", "Chance Drop of Coins", 0.25f);
            BaseLevelMultiplierGoblinCoins = base.Config.Bind<bool>("3. AllMobConfig Goblin", "Drop Multiplier per Level of Coins", true);
            //Black Metal Scrap
            BaseMinGoblinBMS = base.Config.Bind<int>("3. AllMobConfig Goblin", "Min Drop of Black Metal Scrap", 1);
            BaseMaxGoblinBMS = base.Config.Bind<int>("3. AllMobConfig Goblin", "Max Drop of Black Metal Scrap", 2);
            BaseChanceGoblinBMS = base.Config.Bind<float>("3. AllMobConfig Goblin", "Chance Drop of Black Metal Scrap", 1f);
            BaseLevelMultiplierGoblinBMS = base.Config.Bind<bool>("3. AllMobConfig Goblin", "Drop Multiplier per Level of Black Metal Scrap", true);
            //Trophy
            BaseMinGoblinTrophy = base.Config.Bind<int>("3. AllMobConfig Goblin", "Min Drop of Trophy", 1);
            BaseMaxGoblinTrophy = base.Config.Bind<int>("3. AllMobConfig Goblin", "Max Drop of Trophy", 1);
            BaseChanceGoblinTrophy = base.Config.Bind<float>("3. AllMobConfig Goblin", "Chance Drop of Trophy", 0.1f);
            BaseLevelMultiplierGoblinTrophy = base.Config.Bind<bool>("3. AllMobConfig Goblin", "Drop Multiplier per Level of Trophy", false);
            //Goblin Brute
            //Coins
            BaseMinGoblinBruteCoins = base.Config.Bind<int>("3. AllMobConfig Goblin Brute", "Min Drop of Coins", 5);
            BaseMaxGoblinBruteCoins = base.Config.Bind<int>("3. AllMobConfig Goblin Brute", "Max Drop of Coins", 20);
            BaseChanceGoblinBruteCoins = base.Config.Bind<float>("3. AllMobConfig Goblin Brute", "Chance Drop of Coins", 1f);
            BaseLevelMultiplierGoblinBruteCoins = base.Config.Bind<bool>("3. AllMobConfig Goblin Brute", "Drop Multiplier per Level of Coins", true);
            //Black Metal Scrap
            BaseMinGoblinBruteBMS = base.Config.Bind<int>("3. AllMobConfig Goblin Brute", "Min Drop of Black Metal Scrap", 3);
            BaseMaxGoblinBruteBMS = base.Config.Bind<int>("3. AllMobConfig Goblin Brute", "Max Drop of Black Metal Scrap", 5);
            BaseChanceGoblinBruteBMS = base.Config.Bind<float>("3. AllMobConfig Goblin Brute", "Chance Drop of Black Metal Scrap", 1f);
            BaseLevelMultiplierGoblinBruteBMS = base.Config.Bind<bool>("3. AllMobConfig Goblin Brute", "Drop Multiplier per Level of Black Metal Scrap", true);
            //Goblin Totem
            BaseMinGoblinBruteTotem = base.Config.Bind<int>("3. AllMobConfig Goblin Brute", "Min Drop of Goblin Totem", 1);
            BaseMaxGoblinBruteTotem = base.Config.Bind<int>("3. AllMobConfig Goblin Brute", "Max Drop of Goblin Totem", 1);
            BaseChanceGoblinBruteTotem = base.Config.Bind<float>("3. AllMobConfig Goblin Brute", "Chance Drop of Goblin Totem", 0.1f);
            BaseLevelMultiplierGoblinBruteTotem = base.Config.Bind<bool>("3. AllMobConfig Goblin Brute", "Drop Multiplier per Level of Goblin Totem", false);
            //Trophy
            BaseMinGoblinBruteTrophy = base.Config.Bind<int>("3. AllMobConfig Goblin Brute", "Min Drop of Trophy", 1);
            BaseMaxGoblinBruteTrophy = base.Config.Bind<int>("3. AllMobConfig Goblin Brute", "Max Drop of Trophy", 1);
            BaseChanceGoblinBruteTrophy = base.Config.Bind<float>("3. AllMobConfig Goblin Brute", "Chance Drop of Trophy", 0.05f);
            BaseLevelMultiplierGoblinBruteTrophy = base.Config.Bind<bool>("3. AllMobConfig Goblin Brute", "Drop Multiplier per Level of Trophy", false);
            //Goblin Shaman
            //Coins
            BaseMinGoblinShamanCoins = base.Config.Bind<int>("3. AllMobConfig Goblin Shaman", "Min Drop of Coins", 20);
            BaseMaxGoblinShamanCoins = base.Config.Bind<int>("3. AllMobConfig Goblin Shaman", "Max Drop of Coins", 40);
            BaseChanceGoblinShamanCoins = base.Config.Bind<float>("3. AllMobConfig Goblin Shaman", "Chance Drop of Coins", 0.25f);
            BaseLevelMultiplierGoblinShamanCoins = base.Config.Bind<bool>("3. AllMobConfig Goblin Shaman", "Drop Multiplier per Level of Coins", true);
            //Black Metal Scrap
            BaseMinGoblinShamanBMS = base.Config.Bind<int>("3. AllMobConfig Goblin Shaman", "Min Drop of Black Metal Scrap", 1);
            BaseMaxGoblinShamanBMS = base.Config.Bind<int>("3. AllMobConfig Goblin Shaman", "Max Drop of Black Metal Scrap", 2);
            BaseChanceGoblinShamanBMS = base.Config.Bind<float>("3. AllMobConfig Goblin Shaman", "Chance Drop of Black Metal Scrap", 1f);
            BaseLevelMultiplierGoblinShamanBMS = base.Config.Bind<bool>("3. AllMobConfig Goblin Shaman", "Drop Multiplier per Level of Black Metal Scrap", true);
            //Trophy
            BaseMinGoblinShamanTrophy = base.Config.Bind<int>("3. AllMobConfig Goblin Shaman", "Min Drop of Trophy", 1);
            BaseMaxGoblinShamanTrophy = base.Config.Bind<int>("3. AllMobConfig Goblin Shaman", "Max Drop of Trophy", 1);
            BaseChanceGoblinShamanTrophy = base.Config.Bind<float>("3. AllMobConfig Goblin Shaman", "Chance Drop of Trophy", 0.1f);
            BaseLevelMultiplierGoblinShamanTrophy = base.Config.Bind<bool>("3. AllMobConfig Goblin Shaman", "Drop Multiplier per Level of Trophy", false);
            //Greydwarf
            //Eyes
            BaseMinGreydwarfEyes = base.Config.Bind<int>("3. AllMobConfig Greydwarf", "Min Drop of Eyes", 1);
            BaseMaxGreydwarfEyes = base.Config.Bind<int>("3. AllMobConfig Greydwarf", "Max Drop of Eyes", 1);
            BaseChanceGreydwarfEyes = base.Config.Bind<float>("3. AllMobConfig Greydwarf", "Chance Drop of Eyes", 0.5f);
            BaseLevelMultiplierGreydwarfEyes = base.Config.Bind<bool>("3. AllMobConfig Greydwarf", "Drop Multiplier per Level of Eyes", true);
            //Resin
            BaseMinGreydwarfResin = base.Config.Bind<int>("3. AllMobConfig Greydwarf", "Min Drop of Resin", 1);
            BaseMaxGreydwarfResin = base.Config.Bind<int>("3. AllMobConfig Greydwarf", "Max Drop of Resin", 1);
            BaseChanceGreydwarfResin = base.Config.Bind<float>("3. AllMobConfig Greydwarf", "Chance Drop of Resin", 1f);
            BaseLevelMultiplierGreydwarfResin = base.Config.Bind<bool>("3. AllMobConfig Greydwarf", "Drop Multiplier per Level of Resin", true);
            //Stone
            BaseMinGreydwarfStone = base.Config.Bind<int>("3. AllMobConfig Greydwarf", "Min Drop of Stone", 1);
            BaseMaxGreydwarfStone = base.Config.Bind<int>("3. AllMobConfig Greydwarf", "Max Drop of Stone", 1);
            BaseChanceGreydwarfStone = base.Config.Bind<float>("3. AllMobConfig Greydwarf", "Chance Drop of Stone", 1f);
            BaseLevelMultiplierGreydwarfStone = base.Config.Bind<bool>("3. AllMobConfig Greydwarf", "Drop Multiplier per Level of Stone", true);
            //Wood
            BaseMinGreydwarfWood = base.Config.Bind<int>("3. AllMobConfig Greydwarf", "Min Drop of Wood", 1);
            BaseMaxGreydwarfWood = base.Config.Bind<int>("3. AllMobConfig Greydwarf", "Max Drop of Wood", 1);
            BaseChanceGreydwarfWood = base.Config.Bind<float>("3. AllMobConfig Greydwarf", "Chance Drop of Wood", 1);
            BaseLevelMultiplierGreydwarfWood = base.Config.Bind<bool>("3. AllMobConfig Greydwarf", "Drop Multiplier per Level of Wood", true);
            //Trophy
            BaseMinGreydwarfTrophy = base.Config.Bind<int>("3. AllMobConfig Greydwarf", "Min Drop of Trophy", 1);
            BaseMaxGreydwarfTrophy = base.Config.Bind<int>("3. AllMobConfig Greydwarf", "Max Drop of Trophy", 1);
            BaseChanceGreydwarfTrophy = base.Config.Bind<float>("3. AllMobConfig Greydwarf", "Chance Drop of Trophy", 0.05f);
            BaseLevelMultiplierGreydwarfTrophy = base.Config.Bind<bool>("3. AllMobConfig Greydwarf", "Drop Multiplier per Level of Trophy", false);
            //Greydwarf Shaman
            //Eyes
            BaseMinGreydwarfShamanEyes = base.Config.Bind<int>("3. AllMobConfig Greydwarf Shaman", "Min Drop of Eyes", 2);
            BaseMaxGreydwarfShamanEyes = base.Config.Bind<int>("3. AllMobConfig Greydwarf Shaman", "Max Drop of Eyes", 2);
            BaseChanceGreydwarfShamanEyes = base.Config.Bind<float>("3. AllMobConfig Greydwarf Shaman", "Chance Drop of Eyes", 1f);
            BaseLevelMultiplierGreydwarfShamanEyes = base.Config.Bind<bool>("3. AllMobConfig Greydwarf Shaman", "Drop Multiplier per Level of Eyes", true);
            //Wood
            BaseMinGreydwarfShamanWood = base.Config.Bind<int>("3. AllMobConfig Greydwarf Shaman", "Min Drop of Wood", 2);
            BaseMaxGreydwarfShamanWood = base.Config.Bind<int>("3. AllMobConfig Greydwarf Shaman", "Max Drop of Wood", 2);
            BaseChanceGreydwarfShamanWood = base.Config.Bind<float>("3. AllMobConfig Greydwarf Shaman", "Chance Drop of Wood", 1f);
            BaseLevelMultiplierGreydwarfShamanWood = base.Config.Bind<bool>("3. AllMobConfig Greydwarf Shaman", "Drop Multiplier per Level of Wood", true);
            //Resin
            BaseMinGreydwarfShamanResin = base.Config.Bind<int>("3. AllMobConfig Greydwarf Shaman", "Min Drop of Wood", 1);
            BaseMaxGreydwarfShamanResin = base.Config.Bind<int>("3. AllMobConfig Greydwarf Shaman", "Max Drop of Wood", 2);
            BaseChanceGreydwarfShamanResin = base.Config.Bind<float>("3. AllMobConfig Greydwarf Shaman", "Chance Drop of Wood", 1f);
            BaseLevelMultiplierGreydwarfShamanResin = base.Config.Bind<bool>("3. AllMobConfig Greydwarf Shaman", "Drop Multiplier per Level of Wood", true);
            //Trophy
            BaseMinGreydwarfShamanTrophy = base.Config.Bind<int>("3. AllMobConfig Greydwarf Shaman", "Min Drop of Trophy", 1);
            BaseMaxGreydwarfShamanTrophy = base.Config.Bind<int>("3. AllMobConfig Greydwarf Shaman", "Max Drop of Trophy", 1);
            BaseChanceGreydwarfShamanTrophy = base.Config.Bind<float>("3. AllMobConfig Greydwarf Shaman", "Chance Drop of Trophy", 0.1f);
            BaseLevelMultiplierGreydwarfShamanTrophy = base.Config.Bind<bool>("3. AllMobConfig Greydwarf Shaman", "Drop Multiplier per Level of Trophy", false);
            //Greydwarf Brute
            //Eyes
            BaseMinGreydwarfBruteEyes = base.Config.Bind<int>("3. AllMobConfig Greydwarf Brute", "Min Drop of Eyes", 2);
            BaseMaxGreydwarfBruteEyes = base.Config.Bind<int>("3. AllMobConfig Greydwarf Brute", "Max Drop of Eyes", 2);
            BaseChanceGreydwarfBruteEyes = base.Config.Bind<float>("3. AllMobConfig Greydwarf Brute", "Chance Drop of Eyes", 1f);
            BaseLevelMultiplierGreydwarfBruteEyes = base.Config.Bind<bool>("3. AllMobConfig Greydwarf Brute", "Drop Multiplier per Level of Eyes", true);
            //Stone
            BaseMinGreydwarfBruteStone = base.Config.Bind<int>("3. AllMobConfig Greydwarf Brute", "Min Drop of Stone", 2);
            BaseMaxGreydwarfBruteStone = base.Config.Bind<int>("3. AllMobConfig Greydwarf Brute", "Max Drop of Stone", 2);
            BaseChanceGreydwarfBruteStone = base.Config.Bind<float>("3. AllMobConfig Greydwarf Brute", "Chance Drop of Stone", 1f);
            BaseLevelMultiplierGreydwarfBruteStone = base.Config.Bind<bool>("3. AllMobConfig Greydwarf Brute", "Drop Multiplier per Level of Stone", true);
            //Wood
            BaseMinGreydwarfBruteWood = base.Config.Bind<int>("3. AllMobConfig Greydwarf Brute", "Min Drop of Wood", 3);
            BaseMaxGreydwarfBruteWood = base.Config.Bind<int>("3. AllMobConfig Greydwarf Brute", "Max Drop of Wood", 5);
            BaseChanceGreydwarfBruteWood = base.Config.Bind<float>("3. AllMobConfig Greydwarf Brute", "Chance Drop of Wood", 1f);
            BaseLevelMultiplierGreydwarfBruteWood = base.Config.Bind<bool>("3. AllMobConfig Greydwarf Brute", "Drop Multiplier per Level of Wood", true);
            //Dandelion
            BaseMinGreydwarfBruteDandelion = base.Config.Bind<int>("3. AllMobConfig Greydwarf Brute", "Min Drop of Dandelion", 1);
            BaseMaxGreydwarfBruteDandelion = base.Config.Bind<int>("3. AllMobConfig Greydwarf Brute", "Max Drop of Dandelion", 1);
            BaseChanceGreydwarfBruteDandelion = base.Config.Bind<float>("3. AllMobConfig Greydwarf Brute", "Chance Drop of Dandelion", 1f);
            BaseLevelMultiplierGreydwarfBruteDandelion = base.Config.Bind<bool>("3. AllMobConfig Greydwarf Brute", "Drop Multiplier per Level of Dandelion", true);
            //Ancient Seed
            BaseMinGreydwarfBruteAncientSeed = base.Config.Bind<int>("3. AllMobConfig Greydwarf Brute", "Min Drop of Ancient Seed", 1);
            BaseMaxGreydwarfBruteAncientSeed = base.Config.Bind<int>("3. AllMobConfig Greydwarf Brute", "Max Drop of Ancient Seed", 1);
            BaseChanceGreydwarfBruteAncientSeed = base.Config.Bind<float>("3. AllMobConfig Greydwarf Brute", "Chance Drop of Ancient Seed", 0.33f);
            BaseLevelMultiplierGreydwarfBruteAncientSeed = base.Config.Bind<bool>("3. AllMobConfig Greydwarf Brute", "Drop Multiplier per Level of Ancient Seed", true);
            //Trophy
            BaseMinGreydwarfBruteTrophy = base.Config.Bind<int>("3. AllMobConfig Greydwarf Brute", "Min Drop of Trophy", 1);
            BaseMaxGreydwarfBruteTrophy = base.Config.Bind<int>("3. AllMobConfig Greydwarf Brute", "Max Drop of Trophy", 1);
            BaseChanceGreydwarfBruteTrophy = base.Config.Bind<float>("3. AllMobConfig Greydwarf Brute", "Chance Drop of Trophy", 0.1f);
            BaseLevelMultiplierGreydwarfBruteTrophy = base.Config.Bind<bool>("3. AllMobConfig Greydwarf Brute", "Drop Multiplier per Level of Trophy", false);
            //Greyling
            //Resin
            BaseMinGreylingResin = base.Config.Bind<int>("3. AllMobConfig Greyling", "Min Drop of Resin", 1);
            BaseMaxGreylingResin = base.Config.Bind<int>("3. AllMobConfig Greyling", "Max Drop of Resin", 1);
            BaseChanceGreylingResin = base.Config.Bind<float>("3. AllMobConfig Greyling", "Chance Drop of Resin", 1f);
            BaseLevelMultiplierGreylingResin = base.Config.Bind<bool>("3. AllMobConfig Greyling", "Drop Multiplier per Level of Resin", true);
            //Leech
            //BloodBag
            BaseMinLeechBloodBag = base.Config.Bind<int>("3. AllMobConfig Leech", "Min Drop of BloodBag", 1);
            BaseMaxLeechBloodBag = base.Config.Bind<int>("3. AllMobConfig Leech", "Max Drop of BloodBag", 1);
            BaseChanceLeechBloodBag = base.Config.Bind<float>("3. AllMobConfig Leech", "Chance Drop of BloodBag", 1f);
            BaseLevelMultiplierLeechBloodBag = base.Config.Bind<bool>("3. AllMobConfig Leech", "Drop Multiplier per Level of BloodBag", true);
            //Trophy
            BaseMinLeechTrophy = base.Config.Bind<int>("3. AllMobConfig Leech", "Min Drop of Trophy", 1);
            BaseMaxLeechTrophy = base.Config.Bind<int>("3. AllMobConfig Leech", "Max Drop of Trophy", 1);
            BaseChanceLeechTrophy = base.Config.Bind<float>("3. AllMobConfig Leech", "Chance Drop of Trophy", 0.1f);
            BaseLevelMultiplierLeechTrophy = base.Config.Bind<bool>("3. AllMobConfig Leech", "Drop Multiplier per Level of Trophy", false);
            //Lox
            //Lox Meat
            BaseMinLoxMeat = base.Config.Bind<int>("3. AllMobConfig Lox", "Min Drop of Lox Meat", 4);
            BaseMaxLoxMeat = base.Config.Bind<int>("3. AllMobConfig Lox", "Max Drop of Lox Meat", 6);
            BaseChanceLoxMeat = base.Config.Bind<float>("3. AllMobConfig Lox", "Chance Drop of Lox Meat", 1f);
            BaseLevelMultiplierLoxMeat = base.Config.Bind<bool>("3. AllMobConfig Lox", "Drop Multiplier per Level of Lox Meat", true);
            //Lox Pelt
            BaseMinLoxPelt = base.Config.Bind<int>("3. AllMobConfig Lox", "Min Drop of Lox Pelt", 2);
            BaseMaxLoxPelt = base.Config.Bind<int>("3. AllMobConfig Lox", "Max Drop of Lox Pelt", 3);
            BaseChanceLoxPelt = base.Config.Bind<float>("3. AllMobConfig Lox", "Chance Drop of Lox Pelt", 1f);
            BaseLevelMultiplierLoxPelt = base.Config.Bind<bool>("3. AllMobConfig Lox", "Drop Multiplier per Level of Lox Pelt", true);
            //Trophy
            BaseMinLoxTrophy = base.Config.Bind<int>("3. AllMobConfig Lox", "Min Drop of Trophy", 1);
            BaseMaxLoxTrophy = base.Config.Bind<int>("3. AllMobConfig Lox", "Max Drop of Trophy", 1);
            BaseChanceLoxTrophy = base.Config.Bind<float>("3. AllMobConfig Lox", "Chance Drop of Trophy", 0.1f);
            BaseLevelMultiplierLoxTrophy = base.Config.Bind<bool>("3. AllMobConfig Lox", "Drop Multiplier per Level of Trophy", false);
            //Neck
            //Tail
            BaseMinNeckTail = base.Config.Bind<int>("3. AllMobConfig Neck", "Min Drop of Tail", 1);
            BaseMaxNeckTail = base.Config.Bind<int>("3. AllMobConfig Neck", "Max Drop of Tail", 1);
            BaseChanceNeckTail = base.Config.Bind<float>("3. AllMobConfig Neck", "Chance Drop of Tail", 0.7f);
            BaseLevelMultiplierNeckTail = base.Config.Bind<bool>("3. AllMobConfig Neck", "Drop Multiplier per Level of Tail", true);
            //Trophy
            BaseMinNeckTrophy = base.Config.Bind<int>("3. AllMobConfig Neck", "Min Drop of Trophy", 1);
            BaseMaxNeckTrophy = base.Config.Bind<int>("3. AllMobConfig Neck", "Max Drop of Trophy", 1);
            BaseChanceNeckTrophy = base.Config.Bind<float>("3. AllMobConfig Neck", "Chance Drop of Trophy", 0.05f);
            BaseLevelMultiplierNeckTrophy = base.Config.Bind<bool>("3. AllMobConfig Neck", "Drop Multiplier per Level of Trophy", false);
            //Serpent
            //Serpent Meat
            BaseMinSerpentMeat = base.Config.Bind<int>("3. AllMobConfig Serpent", "Min Drop of Serpent Meat", 6);
            BaseMaxSerpentMeat = base.Config.Bind<int>("3. AllMobConfig Serpent", "Max Drop of Serpent Meat", 8);
            BaseChanceSerpentMeat = base.Config.Bind<float>("3. AllMobConfig Serpent", "Chance Drop of Serpent Meat", 1f);
            BaseLevel2. MultiplierserpentMeat = base.Config.Bind<bool>("3. AllMobConfig Serpent", "Drop Multiplier per Level of Serpent Meat", true);
            //Serpent Scale
            BaseMinSerpentScale = base.Config.Bind<int>("3. AllMobConfig Serpent", "Min Drop of Serpent Scale", 8);
            BaseMaxSerpentScale = base.Config.Bind<int>("3. AllMobConfig Serpent", "Max Drop of Serpent Scale", 10);
            BaseChanceSerpentScale = base.Config.Bind<float>("3. AllMobConfig Serpent", "Chance Drop of Serpent Scale", 1f);
            BaseLevel2. MultiplierserpentScale = base.Config.Bind<bool>("3. AllMobConfig Serpent", "Drop Multiplier per Level of Serpent Scale", true);
            //Trophy
            BaseMinSerpentTrophy = base.Config.Bind<int>("3. AllMobConfig Serpent", "Min Drop of Trophy", 1);
            BaseMaxSerpentTrophy = base.Config.Bind<int>("3. AllMobConfig Serpent", "Max Drop of Trophy", 1);
            BaseChanceSerpentTrophy = base.Config.Bind<float>("3. AllMobConfig Serpent", "Chance Drop of Trophy", 0.33f);
            BaseLevel2. MultiplierserpentTrophy = base.Config.Bind<bool>("3. AllMobConfig Serpent", "Drop Multiplier per Level of Trophy", false);
            //Skeleton
            //Bone Fragments
            BaseMinSkeletonBones = base.Config.Bind<int>("3. AllMobConfig Skeleton", "Min Drop of Bone Fragments", 1);
            BaseMaxSkeletonBones = base.Config.Bind<int>("3. AllMobConfig Skeleton", "Max Drop of Bone Fragments", 1);
            BaseChanceSkeletonBones = base.Config.Bind<float>("3. AllMobConfig Skeleton", "Chance Drop of Bone Fragments", 1f);
            BaseLevel2. MultiplierskeletonBones = base.Config.Bind<bool>("3. AllMobConfig Skeleton", "Drop Multiplier per Level of Bone Fragments", true);
            //Trophy
            BaseMinSkeletonTrophy = base.Config.Bind<int>("3. AllMobConfig Skeleton", "Min Drop of Trophy", 1);
            BaseMaxSkeletonTrophy = base.Config.Bind<int>("3. AllMobConfig Skeleton", "Max Drop of Trophy", 1);
            BaseChanceSkeletonTrophy = base.Config.Bind<float>("3. AllMobConfig Skeleton", "Chance Drop of Trophy", 0.1f);
            BaseLevel2. MultiplierskeletonTrophy = base.Config.Bind<bool>("3. AllMobConfig Skeleton", "Drop Multiplier per Level of Trophy", false);
            //Skeketon Poison
            //Bone Fragments
            BaseMinSkeletonPoisonBones = base.Config.Bind<int>("3. AllMobConfig Skeketon Poison", "Min Drop of Bone Fragments", 1);
            BaseMaxSkeletonPoisonBones = base.Config.Bind<int>("3. AllMobConfig Skeketon Poison", "Max Drop of Bone Fragments", 1);
            BaseChanceSkeletonPoisonBones = base.Config.Bind<float>("3. AllMobConfig Skeketon Poison", "Chance Drop of Bone Fragments", 1f);
            BaseLevel2. MultiplierskeletonPoisonBones = base.Config.Bind<bool>("3. AllMobConfig Skeketon Poison", "Drop Multiplier per Level of Bone Fragments", true);
            //Trophy
            BaseMinSkeletonPoisonTrophy = base.Config.Bind<int>("3. AllMobConfig Skeketon Poison", "Min Drop of Trophy", 1);
            BaseMaxSkeletonPoisonTrophy = base.Config.Bind<int>("3. AllMobConfig Skeketon Poison", "Max Drop of Trophy", 1);
            BaseChanceSkeletonPoisonTrophy = base.Config.Bind<float>("3. AllMobConfig Skeketon Poison", "Chance Drop of Trophy", 0.1f);
            BaseLevel2. MultiplierskeletonPoisonTrophy = base.Config.Bind<bool>("3. AllMobConfig Skeketon Poison", "Drop Multiplier per Level of Trophy", false);
            //Stone Golem
            //Stone
            BaseMinStoneGolemStone = base.Config.Bind<int>("3. AllMobConfig Stone Golem", "Min Drop of Stone", 5);
            BaseMaxStoneGolemStone = base.Config.Bind<int>("3. AllMobConfig Stone Golem", "Max Drop of Stone", 10);
            BaseChanceStoneGolemStone = base.Config.Bind<float>("3. AllMobConfig Stone Golem", "Chance Drop of Stone", 1f);
            BaseLevel2. MultiplierstoneGolemStone = base.Config.Bind<bool>("3. AllMobConfig Stone Golem", "Drop Multiplier per Level of Stone", true);
            //Crystal
            BaseMinStoneGolemCrystal = base.Config.Bind<int>("3. AllMobConfig Stone Golem", "Min Drop of Crystal", 3);
            BaseMaxStoneGolemCrystal = base.Config.Bind<int>("3. AllMobConfig Stone Golem", "Max Drop of Crystal", 6);
            BaseChanceStoneGolemCrystal = base.Config.Bind<float>("3. AllMobConfig Stone Golem", "Chance Drop of Crystal", 1f);
            BaseLevel2. MultiplierstoneGolemCrystal = base.Config.Bind<bool>("3. AllMobConfig Stone Golem", "Drop Multiplier per Level of Crystal", true);
            //Trophy
            BaseMinStoneGolemTrophy = base.Config.Bind<int>("3. AllMobConfig Stone Golem", "Min Drop of Trophy", 1);
            BaseMaxStoneGolemTrophy = base.Config.Bind<int>("3. AllMobConfig Stone Golem", "Max Drop of Trophy", 1);
            BaseChanceStoneGolemTrophy = base.Config.Bind<float>("3. AllMobConfig Stone Golem", "Chance Drop of Trophy", 0.05f);
            BaseLevel2. MultiplierstoneGolemTrophy = base.Config.Bind<bool>("3. AllMobConfig Stone Golem", "Drop Multiplier per Level of Trophy", false);
            //Surtling
            //Coal
            BaseMinSurtlingCoal = base.Config.Bind<int>("3. AllMobConfig Surtling", "Min Drop of Coal", 4);
            BaseMaxSurtlingCoal = base.Config.Bind<int>("3. AllMobConfig Surtling", "Max Drop of Coal", 5);
            BaseChanceSurtlingCoal = base.Config.Bind<float>("3. AllMobConfig Surtling", "Chance Drop of Coal", 1);
            BaseLevel2. MultipliersurtlingCoal = base.Config.Bind<bool>("3. AllMobConfig Surtling", "Drop Multiplier per Level of Coal", true);
            //Surtling Core
            BaseMinSurtlingCore = base.Config.Bind<int>("3. AllMobConfig Surtling", "Min Drop of Surtling Core", 1);
            BaseMaxSurtlingCore = base.Config.Bind<int>("3. AllMobConfig Surtling", "Max Drop of Surtling Core", 1);
            BaseChanceSurtlingCore = base.Config.Bind<float>("3. AllMobConfig Surtling", "Chance Drop of Surtling Core", 0.5f);
            BaseLevel2. MultipliersurtlingCore = base.Config.Bind<bool>("3. AllMobConfig Surtling", "Drop Multiplier per Level of Surtling Core", true);
            //Trophy
            BaseMinSurtlingTrophy = base.Config.Bind<int>("3. AllMobConfig Surtling", "Min Drop of Trophy", 1);
            BaseMaxSurtlingTrophy = base.Config.Bind<int>("3. AllMobConfig Surtling", "Max Drop of Trophy", 1);
            BaseChanceSurtlingTrophy = base.Config.Bind<float>("3. AllMobConfig Surtling", "Chance Drop of Trophy", 0.05f);
            BaseLevel2. MultipliersurtlingTrophy = base.Config.Bind<bool>("3. AllMobConfig Surtling", "Drop Multiplier per Level of Trophy", false);
            //Troll
            //Coins
            BaseMinTrollCoins = base.Config.Bind<int>("3. AllMobConfig Troll", "Min Drop of Coins", 20);
            BaseMaxTrollCoins = base.Config.Bind<int>("3. AllMobConfig Troll", "Max Drop of Coins", 30);
            BaseChanceTrollCoins = base.Config.Bind<float>("3. AllMobConfig Troll", "Chance Drop of Coins", 1f);
            BaseLevelMultiplierTrollCoins = base.Config.Bind<bool>("3. AllMobConfig Troll", "Drop Multiplier per Level of Coins", true);
            //Troll Hide
            BaseMinTrollHide = base.Config.Bind<int>("3. AllMobConfig Troll", "Min Drop of Troll Hide", 5);
            BaseMaxTrollHide = base.Config.Bind<int>("3. AllMobConfig Troll", "Max Drop of Troll Hide", 5);
            BaseChanceTrollHide = base.Config.Bind<float>("3. AllMobConfig Troll", "Chance Drop of Troll Hide", 1f);
            BaseLevelMultiplierTrollHide = base.Config.Bind<bool>("3. AllMobConfig Troll", "Drop Multiplier per Level of Troll Hide", true);
            //Trophy
            BaseMinTrollTrophy = base.Config.Bind<int>("3. AllMobConfig Troll", "Min Drop of Trophy", 1);
            BaseMaxTrollTrophy = base.Config.Bind<int>("3. AllMobConfig Troll", "Max Drop of Trophy", 1);
            BaseChanceTrollTrophy = base.Config.Bind<float>("3. AllMobConfig Troll", "Chance Drop of Trophy", 0.5f);
            BaseLevelMultiplierTrollTrophy = base.Config.Bind<bool>("3. AllMobConfig Troll", "Drop Multiplier per Level of Trophy", false);
            //Wolf
            //Raw Meat
            BaseMinWolfRawMeat = base.Config.Bind<int>("3. AllMobConfig Wolf", "Min Drop of Raw Meat", 1);
            BaseMaxWolfRawMeat = base.Config.Bind<int>("3. AllMobConfig Wolf", "Max Drop of Raw Meat", 1);
            BaseChanceWolfRawMeat = base.Config.Bind<float>("3. AllMobConfig Wolf", "Chance Drop of Raw Meat", 0.4f);
            BaseLevelMultiplierWolfRawMeat = base.Config.Bind<bool>("3. AllMobConfig Wolf", "Drop Multiplier per Level of Raw Meat", true);
            //Wolf Pelt
            BaseMinWolfPelt = base.Config.Bind<int>("3. AllMobConfig Wolf", "Min Drop of Wolf Pelt", 1);
            BaseMaxWolfPelt = base.Config.Bind<int>("3. AllMobConfig Wolf", "Max Drop of Wolf Pelt", 2);
            BaseChanceWolfPelt = base.Config.Bind<float>("3. AllMobConfig Wolf", "Chance Drop of Wolf Pelt", 1f);
            BaseLevelMultiplierWolfPelt = base.Config.Bind<bool>("3. AllMobConfig Wolf", "Drop Multiplier per Level of Wolf Pelt", true);
            //Wolf Fang
            BaseMinWolfFang = base.Config.Bind<int>("3. AllMobConfig Wolf", "Min Drop of Wolf Fang", 1);
            BaseMaxWolfFang = base.Config.Bind<int>("3. AllMobConfig Wolf", "Max Drop of Wolf Fang", 1);
            BaseChanceWolfFang = base.Config.Bind<float>("3. AllMobConfig Wolf", "Chance Drop of Wolf Fang", 0.4f);
            BaseLevelMultiplierWolfFang = base.Config.Bind<bool>("3. AllMobConfig Wolf", "Drop Multiplier per Level of Wolf Fang", true);
            //Trophy
            BaseMinWolfTrophy = base.Config.Bind<int>("3. AllMobConfig Wolf", "Min Drop of Trophy", 1);
            BaseMaxWolfTrophy = base.Config.Bind<int>("3. AllMobConfig Wolf", "Max Drop of Trophy", 1);
            BaseChanceWolfTrophy = base.Config.Bind<float>("3. AllMobConfig Wolf", "Chance Drop of Trophy", 0.1f);
            BaseLevelMultiplierWolfTrophy = base.Config.Bind<bool>("3. AllMobConfig Wolf", "Drop Multiplier per Level of Trophy", false);
            //Wraith
            //Chain
            BaseMinWraithChain = base.Config.Bind<int>("3. AllMobConfig Wraith", "Min Drop of Chain", 1);
            BaseMaxWraithChain = base.Config.Bind<int>("3. AllMobConfig Wraith", "Max Drop of Chain", 1);
            BaseChanceWraithChain = base.Config.Bind<float>("3. AllMobConfig Wraith", "Chance Drop of Chain", 1f);
            BaseLevelMultiplierWraithChain = base.Config.Bind<bool>("3. AllMobConfig Wraith", "Drop Multiplier per Level of Chain", true);
            //Trophy
            BaseMinWraithTrophy = base.Config.Bind<int>("3. AllMobConfig Wraith", "Min Drop of Trophy", 1);
            BaseMaxWraithTrophy = base.Config.Bind<int>("3. AllMobConfig Wraith", "Max Drop of Trophy", 1);
            BaseChanceWraithTrophy = base.Config.Bind<float>("3. AllMobConfig Wraith", "Chance Drop of Trophy", 0.05f);
            BaseLevelMultiplierWraithTrophy = base.Config.Bind<bool>("3. AllMobConfig Wraith", "Drop Multiplier per Level of Trophy", false);
            //Eikthyr
            //Hard Antler
            BaseMinEikthyrHardAntler = base.Config.Bind<int>("AllBossConfig: Eikthyr", "Min Drop of Hard Antler", 3);
            BaseMaxEikthyrHardAntler = base.Config.Bind<int>("AllBossConfig: Eikthyr", "Max Drop of Hard Antler", 3);
            BaseChanceEikthyrHardAntler = base.Config.Bind<float>("AllBossConfig: Eikthyr", "Chance Drop of Hard Antler", 1f);
            BaseLevelMultiplierEikthyrHardAntler = base.Config.Bind<bool>("AllBossConfig: Eikthyr", "Drop Multiplier per Level of Hard Antler", false);
            //Trophy
            BaseMinEikthyrTrophy = base.Config.Bind<int>("AllBossConfig: Eikthyr", "Min Drop of Trophy", 1);
            BaseMaxEikthyrTrophy = base.Config.Bind<int>("AllBossConfig: Eikthyr", "Max Drop of Trophy", 1);
            BaseChanceEikthyrTrophy = base.Config.Bind<float>("AllBossConfig: Eikthyr", "Chance Drop of Trophy", 1f);
            BaseLevelMultiplierEikthyrTrophy = base.Config.Bind<bool>("AllBossConfig: Eikthyr", "Drop Multiplier per Level of Trophy", false);
            //The Elder
            //Crypt Key
            BaseMingdkingCryptKey = base.Config.Bind<int>("AllBossConfig: The Elder", "Min Drop of Crypt Key", 1);
            BaseMaxgdkingCryptKey = base.Config.Bind<int>("AllBossConfig: The Elder", "Max Drop of Crypt Key", 1);
            BaseChancegdkingCryptKey = base.Config.Bind<float>("AllBossConfig: The Elder", "Chance Drop of Crypt Key", 1f);
            BaseLevelMultipliergdkingCryptKey = base.Config.Bind<bool>("AllBossConfig: The Elder", "Drop Multiplier per Level of Crypt Key", false);
            //Trophy
            BaseMingdkingTrophy = base.Config.Bind<int>("AllBossConfig: The Elder", "Min Drop of Trophy", 1);
            BaseMaxgdkingTrophy = base.Config.Bind<int>("AllBossConfig: The Elder", "Max Drop of Trophy", 1);
            BaseChancegdkingTrophy = base.Config.Bind<float>("AllBossConfig: The Elder", "Chance Drop of Trophy", 1f);
            BaseLevelMultipliergdkingTrophy = base.Config.Bind<bool>("AllBossConfig: The Elder", "Drop Multiplier per Level of Trophy", false);
            //Bonemass
            //WishBone
            BaseMinBonemassWishbone = base.Config.Bind<int>("AllBossConfig: Bonemass", "Min Drop of WishBone", 1);
            BaseMaxBonemassWishbone = base.Config.Bind<int>("AllBossConfig: Bonemass", "Max Drop of WishBone", 1);
            BaseChanceBonemassWishbone = base.Config.Bind<float>("AllBossConfig: Bonemass", "Chance Drop of WishBone", 1f);
            BaseLevelMultiplierBonemassWishbone = base.Config.Bind<bool>("AllBossConfig: Bonemass", "Drop Multiplier per Level of WishBone", false);
            //Trophy
            BaseMinBonemassTrophy = base.Config.Bind<int>("AllBossConfig: Bonemass", "Min Drop of Trophy", 1);
            BaseMaxBonemassTrophy = base.Config.Bind<int>("AllBossConfig: Bonemass", "Max Drop of Trophy", 1);
            BaseChanceBonemassTrophy = base.Config.Bind<float>("AllBossConfig: Bonemass", "Chance Drop of Trophy", 1f);
            BaseLevelMultiplierBonemassTrophy = base.Config.Bind<bool>("AllBossConfig: Bonemass", "Drop Multiplier per Level of Trophy", false);
            //Dragon
            //Dragon Tear
            BaseMinDragonTear = base.Config.Bind<int>("AllBossConfig: Dragon", "Min Drop of Dragon Tear", 10);
            BaseMaxDragonTear = base.Config.Bind<int>("AllBossConfig: Dragon", "Max Drop of Dragon Tear", 10);
            BaseChanceDragonTear = base.Config.Bind<float>("AllBossConfig: Dragon", "Chance Drop of Dragon Tear", 1f);
            BaseLevelMultiplierDragonTear = base.Config.Bind<bool>("AllBossConfig: Dragon", "Drop Multiplier per Level of Dragon Tear", false);
            //Trophy
            BaseMinDragonQueenTrophy = base.Config.Bind<int>("AllBossConfig: Dragon", "Min Drop of Trophy", 1);
            BaseMaxDragonQueenTrophy = base.Config.Bind<int>("AllBossConfig: Dragon", "Max Drop of Trophy", 1);
            BaseChanceDragonQueenTrophy = base.Config.Bind<float>("AllBossConfig: Dragon", "Chance Drop of Trophy", 1f);
            BaseLevelMultiplierDragonQueenTrophy = base.Config.Bind<bool>("AllBossConfig: Dragon", "Drop Multiplier per Level of Trophy", false);
            //Goblin King
            //Yagluth
            BaseMinGoblinKingYagluthDrop = base.Config.Bind<int>("AllBossConfig: GoblinKing", "Min Drop of Yagluth", 1);
            BaseMaxGoblinKingYagluthDrop = base.Config.Bind<int>("AllBossConfig: GoblinKing", "Max Drop of Yagluth", 1);
            BaseChanceGoblinKingYagluthDrop = base.Config.Bind<float>("AllBossConfig: GoblinKing", "Chance Drop of Yagluth", 1f);
            BaseLevelMultiplierGoblinKingYagluthDrop = base.Config.Bind<bool>("AllBossConfig: GoblinKing", "Drop Multiplier per Level of Yagluth", false);
            //Trophy
            BaseMinGoblinKingTrophy = base.Config.Bind<int>("AllBossConfig: GoblinKing", "Min Drop of Trophy", 1);
            BaseMaxGoblinKingTrophy = base.Config.Bind<int>("AllBossConfig: GoblinKing", "Max Drop of Trophy", 1);
            BaseChanceGoblinKingTrophy = base.Config.Bind<float>("AllBossConfig: GoblinKing", "Chance Drop of Trophy", 1f);
            BaseLevelMultiplierGoblinKingTrophy = base.Config.Bind<bool>("AllBossConfig: GoblinKing", "Drop Multiplier per Level of Trophy", false);*/

            //Inject.Strength
            if (EnableHuntingSkill.Value)
                try
                {
                    SkillInjector.RegisterNewSkill(704, "Hunting", "You're gonna get better at gaining more resources when you kill something.", 1f, SkillIcons.Load_HuntingIcon(), Skills.SkillType.Unarmed);
                }
                catch (Exception e)
                {
                    Debug.LogError("Error Registering new Skill 'Hunting'" + e.Message);
                }

            //--
            Debug.Log("[MoreSkills] Hunting Skill Patched!");
            harmonyHunting = new Harmony("MoreSkills.HuntingConfig.GuiriGuyMods");

            //Logs
            if (!EnableHuntingSkill.Value)
                Debug.LogWarning("[MoreSkills] Hunting Skill Mod Disabled");
            else
            {
                Debug.Log("[MoreSkills] Hunting Skill Mod Enabled");
                if (!EnableHuntingNormalMobsMod.Value)
                    Debug.LogWarning("[MoreSkills] Hunting/Normal Mobs Mod Disabled");
                else
                    Debug.Log("[MoreSkills] Hunting/Normal Mobs Mod Enabled");
                /*if (!EnableHuntingBossMod.Value)
                    Debug.LogWarning("[MoreSkills] Hunting/Boss Mobs Mod Disabled");
                else
                    Debug.Log("[MoreSkills] Hunting/Boss Mobs Mod Enabled");*/
                if (!EnableHuntingTrophyMod.Value)
                    Debug.LogWarning("[MoreSkills] Hunting/Trophy Mod Disabled");
                else
                    Debug.Log("[MoreSkills] Hunting/Trophy Mod Enabled");
                if (!EnableHuntingMinMaxMod.Value)
                    Debug.LogWarning("[MoreSkills] Hunting/MinMax Mobs Mod Disabled");
                else
                    Debug.Log("[MoreSkills] Hunting/MinMax Mobs Mod Enabled");
                if (!EnableTrialsOfOdinCompatibility.Value)
                    Debug.LogWarning("[MoreSkills] Hunting/If you got Trials of Odin I highly recommend you to activate this. Otherwise don't read me. I know it's red. But it's all ok, don't worry :P");
                else
                    Debug.Log("[MoreSkills] Hunting/Trials of Odin Compatibility Enabled");
                if (!EnableHuntingChanceMod.Value)
                    Debug.LogWarning("[MoreSkills] Hunting/Chance Mod Disabled");
                else
                    Debug.Log("[MoreSkills] Hunting/Chance Mod Enabled");
                /*if (!EnableHuntingChanceMod.Value)
                    Debug.LogWarning("[MoreSkills] Hunting/Blob Spawn Mod Disabled");
                else
                    Debug.Log("[MoreSkills] Hunting/Blob Spawn Mod Enabled");*/
            }

            Debug.Log("[MoreSkills] Hunting Skill Loaded!");
        }
        private void OnDestroy()
        {

            Debug.Log("[MoreSkills] Hunting Skill UnPatched!");
            harmonyHunting.UnpatchSelf();
        }

        // Stats Bases

        private Harmony harmonyHunting;

        //2. Multipliers

        public static ConfigEntry<float> HuntingDropMultiplier;
        //Skill Increases Multpliers

        public static ConfigEntry<float> HuntingSkillMultiplier;

        //Enables

        public static ConfigEntry<bool> EnableHuntingSkill;

        public static ConfigEntry<bool> EnableHuntingMinMaxMod;

        public static ConfigEntry<bool> EnableHuntingChanceMod;

        public static ConfigEntry<bool> EnableHuntingBossMod;

        public static ConfigEntry<bool> EnableHuntingNormalMobsMod;

        public static ConfigEntry<bool> EnableHuntingTrophyMod;

        public static ConfigEntry<bool> EnableHuntingBlobSpawn;

        public static ConfigEntry<bool> EnableTrialsOfOdinCompatibility;

        //BaseConfigs

        public static ConfigEntry<int> ToOLowLevelStart;
        public static ConfigEntry<int> ToOMidLevelStart;
        public static ConfigEntry<int> ToOHighLevelStart;

        public const int HuntingSkill_Type = 704;

        /*//Mobs Data Bases
        //Blob
        //Ooze
        public static ConfigEntry<int> BaseMinBlobOoze;
        public static ConfigEntry<int> BaseMaxBlobOoze;
        public static ConfigEntry<float> BaseChanceBlobOoze;
        public static ConfigEntry<bool> BaseLevelMultiplierBlobOoze;
        //TrophyBlob
        public static ConfigEntry<int> BaseMinBlobTrophy;
        public static ConfigEntry<int> BaseMaxBlobTrophy;
        public static ConfigEntry<float> BaseChanceBlobTrophy;
        public static ConfigEntry<bool> BaseLevelMultiplierBlobTrophy;
        //Blob Elite
        //Ooze
        public static ConfigEntry<int> BaseMinBlobEliteOoze;
        public static ConfigEntry<int> BaseMaxBlobEliteOoze;
        public static ConfigEntry<float> BaseChanceBlobEliteOoze;
        public static ConfigEntry<bool> BaseLevelMultiplierBlobEliteOoze;
        //Iron Scrap
        public static ConfigEntry<int> BaseMinBlobEliteIronScrap;
        public static ConfigEntry<int> BaseMaxBlobEliteIronScrap;
        public static ConfigEntry<float> BaseChanceBlobEliteIronScrap;
        public static ConfigEntry<bool> BaseLevelMultiplierBlobEliteIronScrap;
        //Blob
        public static ConfigEntry<int> BaseMinBlobEliteBlob;
        public static ConfigEntry<int> BaseMaxBlobEliteBlob;
        public static ConfigEntry<float> BaseChanceBlobEliteBlob;
        public static ConfigEntry<bool> BaseLevelMultiplierBlobEliteBlob;
        //Trophy same as blob
        //Boar
        //RawMeat
        public static ConfigEntry<int> BaseMinBoarRawMeat;
        public static ConfigEntry<int> BaseMaxBoarRawMeat;
        public static ConfigEntry<float> BaseChanceBoarRawMeat;
        public static ConfigEntry<bool> BaseLevelMultiplierBoarRawMeat;
        //LeatherScraps
        public static ConfigEntry<int> BaseMinBoarLeatherScraps;
        public static ConfigEntry<int> BaseMaxBoarLeatherScraps;
        public static ConfigEntry<float> BaseChanceBoarLeatherScraps;
        public static ConfigEntry<bool> BaseLevelMultiplierBoarLeatherScraps;
        //Trophy
        public static ConfigEntry<int> BaseMinBoarTrophy;
        public static ConfigEntry<int> BaseMaxBoarTrophy;
        public static ConfigEntry<float> BaseChanceBoarTrophy;
        public static ConfigEntry<bool> BaseLevelMultiplierBoarTrophy;
        //Deathsquito
        //Needle
        public static ConfigEntry<int> BaseMinDeathsquitoNeedle;
        public static ConfigEntry<int> BaseMaxDeathsquitoNeedle;
        public static ConfigEntry<float> BaseChanceDeathsquitoNeedle;
        public static ConfigEntry<bool> BaseLevelMultiplierDeathsquitoNeedle;
        //Trophy
        public static ConfigEntry<int> BaseMinDeathsquitoTrophy;
        public static ConfigEntry<int> BaseMaxDeathsquitoTrophy;
        public static ConfigEntry<float> BaseChanceDeathsquitoTrophy;
        public static ConfigEntry<bool> BaseLevelMultiplierDeathsquitoTrophy;
        //Deer
        //RawMeat
        public static ConfigEntry<int> BaseMinDeerRawMeat;
        public static ConfigEntry<int> BaseMaxDeerRawMeat;
        public static ConfigEntry<float> BaseChanceDeerRawMeat;
        public static ConfigEntry<bool> BaseLevelMultiplierDeerRawMeat;
        //DeerHide
        public static ConfigEntry<int> BaseMinDeerHide;
        public static ConfigEntry<int> BaseMaxDeerHide;
        public static ConfigEntry<float> BaseChanceDeerHide;
        public static ConfigEntry<bool> BaseLevelMultiplierDeerHide;
        //Trophy
        public static ConfigEntry<int> BaseMinDeerTrophy;
        public static ConfigEntry<int> BaseMaxDeerTrophy;
        public static ConfigEntry<float> BaseChanceDeerTrophy;
        public static ConfigEntry<bool> BaseLevelMultiplierDeerTrophy;
        //Drake
        //FreezeGland
        public static ConfigEntry<int> BaseMinDrakeFreezeGland;
        public static ConfigEntry<int> BaseMaxDrakeFreezeGland;
        public static ConfigEntry<float> BaseChanceDrakeFreezeGland;
        public static ConfigEntry<bool> BaseLevelMultiplierDrakeFreezeGland;
        //TrophyHatchling
        public static ConfigEntry<int> BaseMinDrakeTrophy;
        public static ConfigEntry<int> BaseMaxDrakeTrophy;
        public static ConfigEntry<float> BaseChanceDrakeTrophy;
        public static ConfigEntry<bool> BaseLevelMultiplierDrakeTrophy;
        //Draugr
        //Entrails
        public static ConfigEntry<int> BaseMinDraugrEntrails;
        public static ConfigEntry<int> BaseMaxDraugrEntrails;
        public static ConfigEntry<float> BaseChanceDraugrEntrails;
        public static ConfigEntry<bool> BaseLevelMultiplierDraugrEntrails;
        //Trophy
        public static ConfigEntry<int> BaseMinDraugrTrophy;
        public static ConfigEntry<int> BaseMaxDraugrTrophy;
        public static ConfigEntry<float> BaseChanceDraugrTrophy;
        public static ConfigEntry<bool> BaseLevelMultiplierDraugrTrophy;
        //Draugr Elite
        //Entrails
        public static ConfigEntry<int> BaseMinDraugrEliteEntrails;
        public static ConfigEntry<int> BaseMaxDraugrEliteEntrails;
        public static ConfigEntry<float> BaseChanceDraugrEliteEntrails;
        public static ConfigEntry<bool> BaseLevelMultiplierDraugrEliteEntrails;
        //Trophy
        public static ConfigEntry<int> BaseMinDraugrEliteTrophy;
        public static ConfigEntry<int> BaseMaxDraugrEliteTrophy;
        public static ConfigEntry<float> BaseChanceDraugrEliteTrophy;
        public static ConfigEntry<bool> BaseLevelMultiplierDraugrEliteTrophy;
        //Fenring
        //WolfFang
        public static ConfigEntry<int> BaseMinFenringWolfFang;
        public static ConfigEntry<int> BaseMaxFenringWolfFang;
        public static ConfigEntry<float> BaseChanceFenringWolfFang;
        public static ConfigEntry<bool> BaseLevelMultiplierFenringWolfFang;
        //Trophy
        public static ConfigEntry<int> BaseMinFenringEliteTrophy;
        public static ConfigEntry<int> BaseMaxFenringEliteTrophy;
        public static ConfigEntry<float> BaseChanceFenringEliteTrophy;
        public static ConfigEntry<bool> BaseLevelMultiplierFenringEliteTrophy;
        //Goblin
        //Coins
        public static ConfigEntry<int> BaseMinGoblinCoins;
        public static ConfigEntry<int> BaseMaxGoblinCoins;
        public static ConfigEntry<float> BaseChanceGoblinCoins;
        public static ConfigEntry<bool> BaseLevelMultiplierGoblinCoins;
        //Black Metal Scrap
        public static ConfigEntry<int> BaseMinGoblinBMS;
        public static ConfigEntry<int> BaseMaxGoblinBMS;
        public static ConfigEntry<float> BaseChanceGoblinBMS;
        public static ConfigEntry<bool> BaseLevelMultiplierGoblinBMS;
        //Trophy
        public static ConfigEntry<int> BaseMinGoblinTrophy;
        public static ConfigEntry<int> BaseMaxGoblinTrophy;
        public static ConfigEntry<float> BaseChanceGoblinTrophy;
        public static ConfigEntry<bool> BaseLevelMultiplierGoblinTrophy;
        //GoblinBrute
        //Coins
        public static ConfigEntry<int> BaseMinGoblinBruteCoins;
        public static ConfigEntry<int> BaseMaxGoblinBruteCoins;
        public static ConfigEntry<float> BaseChanceGoblinBruteCoins;
        public static ConfigEntry<bool> BaseLevelMultiplierGoblinBruteCoins;
        //Black Metal Scrap
        public static ConfigEntry<int> BaseMinGoblinBruteBMS;
        public static ConfigEntry<int> BaseMaxGoblinBruteBMS;
        public static ConfigEntry<float> BaseChanceGoblinBruteBMS;
        public static ConfigEntry<bool> BaseLevelMultiplierGoblinBruteBMS;
        //Goblin Totem
        public static ConfigEntry<int> BaseMinGoblinBruteTotem;
        public static ConfigEntry<int> BaseMaxGoblinBruteTotem;
        public static ConfigEntry<float> BaseChanceGoblinBruteTotem;
        public static ConfigEntry<bool> BaseLevelMultiplierGoblinBruteTotem;
        //Trophy
        public static ConfigEntry<int> BaseMinGoblinBruteTrophy;
        public static ConfigEntry<int> BaseMaxGoblinBruteTrophy;
        public static ConfigEntry<float> BaseChanceGoblinBruteTrophy;
        public static ConfigEntry<bool> BaseLevelMultiplierGoblinBruteTrophy;
        //GoblinShaman
        //Coins
        public static ConfigEntry<int> BaseMinGoblinShamanCoins;
        public static ConfigEntry<int> BaseMaxGoblinShamanCoins;
        public static ConfigEntry<float> BaseChanceGoblinShamanCoins;
        public static ConfigEntry<bool> BaseLevelMultiplierGoblinShamanCoins;
        //Black Metal Scrap
        public static ConfigEntry<int> BaseMinGoblinShamanBMS;
        public static ConfigEntry<int> BaseMaxGoblinShamanBMS;
        public static ConfigEntry<float> BaseChanceGoblinShamanBMS;
        public static ConfigEntry<bool> BaseLevelMultiplierGoblinShamanBMS;
        //Trophy
        public static ConfigEntry<int> BaseMinGoblinShamanTrophy;
        public static ConfigEntry<int> BaseMaxGoblinShamanTrophy;
        public static ConfigEntry<float> BaseChanceGoblinShamanTrophy;
        public static ConfigEntry<bool> BaseLevelMultiplierGoblinShamanTrophy;
        //Greydwarf
        //GreydwarfEyes
        public static ConfigEntry<int> BaseMinGreydwarfEyes;
        public static ConfigEntry<int> BaseMaxGreydwarfEyes;
        public static ConfigEntry<float> BaseChanceGreydwarfEyes;
        public static ConfigEntry<bool> BaseLevelMultiplierGreydwarfEyes;
        //Resin
        public static ConfigEntry<int> BaseMinGreydwarfResin;
        public static ConfigEntry<int> BaseMaxGreydwarfResin;
        public static ConfigEntry<float> BaseChanceGreydwarfResin;
        public static ConfigEntry<bool> BaseLevelMultiplierGreydwarfResin;
        //Stone
        public static ConfigEntry<int> BaseMinGreydwarfStone;
        public static ConfigEntry<int> BaseMaxGreydwarfStone;
        public static ConfigEntry<float> BaseChanceGreydwarfStone;
        public static ConfigEntry<bool> BaseLevelMultiplierGreydwarfStone;
        //Wood
        public static ConfigEntry<int> BaseMinGreydwarfWood;
        public static ConfigEntry<int> BaseMaxGreydwarfWood;
        public static ConfigEntry<float> BaseChanceGreydwarfWood;
        public static ConfigEntry<bool> BaseLevelMultiplierGreydwarfWood;
        //Trophy
        public static ConfigEntry<int> BaseMinGreydwarfTrophy;
        public static ConfigEntry<int> BaseMaxGreydwarfTrophy;
        public static ConfigEntry<float> BaseChanceGreydwarfTrophy;
        public static ConfigEntry<bool> BaseLevelMultiplierGreydwarfTrophy;
        //GreydwarfBrute
        //GreydwarfEyes
        public static ConfigEntry<int> BaseMinGreydwarfBruteEyes;
        public static ConfigEntry<int> BaseMaxGreydwarfBruteEyes;
        public static ConfigEntry<float> BaseChanceGreydwarfBruteEyes;
        public static ConfigEntry<bool> BaseLevelMultiplierGreydwarfBruteEyes;
        //Dandelion
        public static ConfigEntry<int> BaseMinGreydwarfBruteDandelion;
        public static ConfigEntry<int> BaseMaxGreydwarfBruteDandelion;
        public static ConfigEntry<float> BaseChanceGreydwarfBruteDandelion;
        public static ConfigEntry<bool> BaseLevelMultiplierGreydwarfBruteDandelion;
        //Stone
        public static ConfigEntry<int> BaseMinGreydwarfBruteStone;
        public static ConfigEntry<int> BaseMaxGreydwarfBruteStone;
        public static ConfigEntry<float> BaseChanceGreydwarfBruteStone;
        public static ConfigEntry<bool> BaseLevelMultiplierGreydwarfBruteStone;
        //Wood
        public static ConfigEntry<int> BaseMinGreydwarfBruteWood;
        public static ConfigEntry<int> BaseMaxGreydwarfBruteWood;
        public static ConfigEntry<float> BaseChanceGreydwarfBruteWood;
        public static ConfigEntry<bool> BaseLevelMultiplierGreydwarfBruteWood;
        //AncientSeed
        public static ConfigEntry<int> BaseMinGreydwarfBruteAncientSeed;
        public static ConfigEntry<int> BaseMaxGreydwarfBruteAncientSeed;
        public static ConfigEntry<float> BaseChanceGreydwarfBruteAncientSeed;
        public static ConfigEntry<bool> BaseLevelMultiplierGreydwarfBruteAncientSeed;
        //Trophy
        public static ConfigEntry<int> BaseMinGreydwarfBruteTrophy;
        public static ConfigEntry<int> BaseMaxGreydwarfBruteTrophy;
        public static ConfigEntry<float> BaseChanceGreydwarfBruteTrophy;
        public static ConfigEntry<bool> BaseLevelMultiplierGreydwarfBruteTrophy;
        //GreydwarfShaman
        //GreydwarfEyes
        public static ConfigEntry<int> BaseMinGreydwarfShamanEyes;
        public static ConfigEntry<int> BaseMaxGreydwarfShamanEyes;
        public static ConfigEntry<float> BaseChanceGreydwarfShamanEyes;
        public static ConfigEntry<bool> BaseLevelMultiplierGreydwarfShamanEyes;
        //Resin
        public static ConfigEntry<int> BaseMinGreydwarfShamanResin;
        public static ConfigEntry<int> BaseMaxGreydwarfShamanResin;
        public static ConfigEntry<float> BaseChanceGreydwarfShamanResin;
        public static ConfigEntry<bool> BaseLevelMultiplierGreydwarfShamanResin;
        //Wood
        public static ConfigEntry<int> BaseMinGreydwarfShamanWood;
        public static ConfigEntry<int> BaseMaxGreydwarfShamanWood;
        public static ConfigEntry<float> BaseChanceGreydwarfShamanWood;
        public static ConfigEntry<bool> BaseLevelMultiplierGreydwarfShamanWood;
        //Trophy
        public static ConfigEntry<int> BaseMinGreydwarfShamanTrophy;
        public static ConfigEntry<int> BaseMaxGreydwarfShamanTrophy;
        public static ConfigEntry<float> BaseChanceGreydwarfShamanTrophy;
        public static ConfigEntry<bool> BaseLevelMultiplierGreydwarfShamanTrophy;
        //Greyling
        //Resin
        public static ConfigEntry<int> BaseMinGreylingResin;
        public static ConfigEntry<int> BaseMaxGreylingResin;
        public static ConfigEntry<float> BaseChanceGreylingResin;
        public static ConfigEntry<bool> BaseLevelMultiplierGreylingResin;
        //Leech
        //Bloodbag
        public static ConfigEntry<int> BaseMinLeechBloodBag;
        public static ConfigEntry<int> BaseMaxLeechBloodBag;
        public static ConfigEntry<float> BaseChanceLeechBloodBag;
        public static ConfigEntry<bool> BaseLevelMultiplierLeechBloodBag;
        //Trophy
        public static ConfigEntry<int> BaseMinLeechTrophy;
        public static ConfigEntry<int> BaseMaxLeechTrophy;
        public static ConfigEntry<float> BaseChanceLeechTrophy;
        public static ConfigEntry<bool> BaseLevelMultiplierLeechTrophy;
        //Lox
        //Meat
        public static ConfigEntry<int> BaseMinLoxMeat;
        public static ConfigEntry<int> BaseMaxLoxMeat;
        public static ConfigEntry<float> BaseChanceLoxMeat;
        public static ConfigEntry<bool> BaseLevelMultiplierLoxMeat;
        //Pelt
        public static ConfigEntry<int> BaseMinLoxPelt;
        public static ConfigEntry<int> BaseMaxLoxPelt;
        public static ConfigEntry<float> BaseChanceLoxPelt;
        public static ConfigEntry<bool> BaseLevelMultiplierLoxPelt;
        //Trophy
        public static ConfigEntry<int> BaseMinLoxTrophy;
        public static ConfigEntry<int> BaseMaxLoxTrophy;
        public static ConfigEntry<float> BaseChanceLoxTrophy;
        public static ConfigEntry<bool> BaseLevelMultiplierLoxTrophy;
        //Neck
        //Tail
        public static ConfigEntry<int> BaseMinNeckTail;
        public static ConfigEntry<int> BaseMaxNeckTail;
        public static ConfigEntry<float> BaseChanceNeckTail;
        public static ConfigEntry<bool> BaseLevelMultiplierNeckTail;
        //Trophy
        public static ConfigEntry<int> BaseMinNeckTrophy;
        public static ConfigEntry<int> BaseMaxNeckTrophy;
        public static ConfigEntry<float> BaseChanceNeckTrophy;
        public static ConfigEntry<bool> BaseLevelMultiplierNeckTrophy;
        //Serpent
        //Meat
        public static ConfigEntry<int> BaseMinSerpentMeat;
        public static ConfigEntry<int> BaseMaxSerpentMeat;
        public static ConfigEntry<float> BaseChanceSerpentMeat;
        public static ConfigEntry<bool> BaseLevel2. MultiplierserpentMeat;
        //Scale
        public static ConfigEntry<int> BaseMinSerpentScale;
        public static ConfigEntry<int> BaseMaxSerpentScale;
        public static ConfigEntry<float> BaseChanceSerpentScale;
        public static ConfigEntry<bool> BaseLevel2. MultiplierserpentScale;
        //Trophy
        public static ConfigEntry<int> BaseMinSerpentTrophy;
        public static ConfigEntry<int> BaseMaxSerpentTrophy;
        public static ConfigEntry<float> BaseChanceSerpentTrophy;
        public static ConfigEntry<bool> BaseLevel2. MultiplierserpentTrophy;
        //Skeleton
        //BoneFragments
        public static ConfigEntry<int> BaseMinSkeletonBones;
        public static ConfigEntry<int> BaseMaxSkeletonBones;
        public static ConfigEntry<float> BaseChanceSkeletonBones;
        public static ConfigEntry<bool> BaseLevel2. MultiplierskeletonBones;
        //Trophy
        public static ConfigEntry<int> BaseMinSkeletonTrophy;
        public static ConfigEntry<int> BaseMaxSkeletonTrophy;
        public static ConfigEntry<float> BaseChanceSkeletonTrophy;
        public static ConfigEntry<bool> BaseLevel2. MultiplierskeletonTrophy;
        //SkeletonPoison
        //BoneFragments
        public static ConfigEntry<int> BaseMinSkeletonPoisonBones;
        public static ConfigEntry<int> BaseMaxSkeletonPoisonBones;
        public static ConfigEntry<float> BaseChanceSkeletonPoisonBones;
        public static ConfigEntry<bool> BaseLevel2. MultiplierskeletonPoisonBones;
        //Trophy
        public static ConfigEntry<int> BaseMinSkeletonPoisonTrophy;
        public static ConfigEntry<int> BaseMaxSkeletonPoisonTrophy;
        public static ConfigEntry<float> BaseChanceSkeletonPoisonTrophy;
        public static ConfigEntry<bool> BaseLevel2. MultiplierskeletonPoisonTrophy;
        //StoneGolem
        //Stone
        public static ConfigEntry<int> BaseMinStoneGolemStone;
        public static ConfigEntry<int> BaseMaxStoneGolemStone;
        public static ConfigEntry<float> BaseChanceStoneGolemStone;
        public static ConfigEntry<bool> BaseLevel2. MultiplierstoneGolemStone;
        //Crystal
        public static ConfigEntry<int> BaseMinStoneGolemCrystal;
        public static ConfigEntry<int> BaseMaxStoneGolemCrystal;
        public static ConfigEntry<float> BaseChanceStoneGolemCrystal;
        public static ConfigEntry<bool> BaseLevel2. MultiplierstoneGolemCrystal;
        //Trophy
        public static ConfigEntry<int> BaseMinStoneGolemTrophy;
        public static ConfigEntry<int> BaseMaxStoneGolemTrophy;
        public static ConfigEntry<float> BaseChanceStoneGolemTrophy;
        public static ConfigEntry<bool> BaseLevel2. MultiplierstoneGolemTrophy;
        //Surtling
        //Coal
        public static ConfigEntry<int> BaseMinSurtlingCoal;
        public static ConfigEntry<int> BaseMaxSurtlingCoal;
        public static ConfigEntry<float> BaseChanceSurtlingCoal;
        public static ConfigEntry<bool> BaseLevel2. MultipliersurtlingCoal;
        //SurtlingCore
        public static ConfigEntry<int> BaseMinSurtlingCore;
        public static ConfigEntry<int> BaseMaxSurtlingCore;
        public static ConfigEntry<float> BaseChanceSurtlingCore;
        public static ConfigEntry<bool> BaseLevel2. MultipliersurtlingCore;
        //Trophy
        public static ConfigEntry<int> BaseMinSurtlingTrophy;
        public static ConfigEntry<int> BaseMaxSurtlingTrophy;
        public static ConfigEntry<float> BaseChanceSurtlingTrophy;
        public static ConfigEntry<bool> BaseLevel2. MultipliersurtlingTrophy;
        //Troll
        //Coins
        public static ConfigEntry<int> BaseMinTrollCoins;
        public static ConfigEntry<int> BaseMaxTrollCoins;
        public static ConfigEntry<float> BaseChanceTrollCoins;
        public static ConfigEntry<bool> BaseLevelMultiplierTrollCoins;
        //TrollHide
        public static ConfigEntry<int> BaseMinTrollHide;
        public static ConfigEntry<int> BaseMaxTrollHide;
        public static ConfigEntry<float> BaseChanceTrollHide;
        public static ConfigEntry<bool> BaseLevelMultiplierTrollHide;
        //Trophy
        public static ConfigEntry<int> BaseMinTrollTrophy;
        public static ConfigEntry<int> BaseMaxTrollTrophy;
        public static ConfigEntry<float> BaseChanceTrollTrophy;
        public static ConfigEntry<bool> BaseLevelMultiplierTrollTrophy;
        //Wolf
        //RawMeat
        public static ConfigEntry<int> BaseMinWolfRawMeat;
        public static ConfigEntry<int> BaseMaxWolfRawMeat;
        public static ConfigEntry<float> BaseChanceWolfRawMeat;
        public static ConfigEntry<bool> BaseLevelMultiplierWolfRawMeat;
        //WolfPelt
        public static ConfigEntry<int> BaseMinWolfPelt;
        public static ConfigEntry<int> BaseMaxWolfPelt;
        public static ConfigEntry<float> BaseChanceWolfPelt;
        public static ConfigEntry<bool> BaseLevelMultiplierWolfPelt;
        //WolfFang
        public static ConfigEntry<int> BaseMinWolfFang;
        public static ConfigEntry<int> BaseMaxWolfFang;
        public static ConfigEntry<float> BaseChanceWolfFang;
        public static ConfigEntry<bool> BaseLevelMultiplierWolfFang;
        //Trophy
        public static ConfigEntry<int> BaseMinWolfTrophy;
        public static ConfigEntry<int> BaseMaxWolfTrophy;
        public static ConfigEntry<float> BaseChanceWolfTrophy;
        public static ConfigEntry<bool> BaseLevelMultiplierWolfTrophy;
        //Wraith
        //RawMeat
        public static ConfigEntry<int> BaseMinWraithChain;
        public static ConfigEntry<int> BaseMaxWraithChain;
        public static ConfigEntry<float> BaseChanceWraithChain;
        public static ConfigEntry<bool> BaseLevelMultiplierWraithChain;
        //Trophy
        public static ConfigEntry<int> BaseMinWraithTrophy;
        public static ConfigEntry<int> BaseMaxWraithTrophy;
        public static ConfigEntry<float> BaseChanceWraithTrophy;
        public static ConfigEntry<bool> BaseLevelMultiplierWraithTrophy;
        //Eikthyr
        //HardAntler
        public static ConfigEntry<int> BaseMinEikthyrHardAntler;
        public static ConfigEntry<int> BaseMaxEikthyrHardAntler;
        public static ConfigEntry<float> BaseChanceEikthyrHardAntler;
        public static ConfigEntry<bool> BaseLevelMultiplierEikthyrHardAntler;
        //Trophy
        public static ConfigEntry<int> BaseMinEikthyrTrophy;
        public static ConfigEntry<int> BaseMaxEikthyrTrophy;
        public static ConfigEntry<float> BaseChanceEikthyrTrophy;
        public static ConfigEntry<bool> BaseLevelMultiplierEikthyrTrophy;
        //TheElder (gdking)
        //CryptKey
        public static ConfigEntry<int> BaseMingdkingCryptKey;
        public static ConfigEntry<int> BaseMaxgdkingCryptKey;
        public static ConfigEntry<float> BaseChancegdkingCryptKey;
        public static ConfigEntry<bool> BaseLevelMultipliergdkingCryptKey;
        //Trophy
        public static ConfigEntry<int> BaseMingdkingTrophy;
        public static ConfigEntry<int> BaseMaxgdkingTrophy;
        public static ConfigEntry<float> BaseChancegdkingTrophy;
        public static ConfigEntry<bool> BaseLevelMultipliergdkingTrophy;
        //Bonemass
        //Wishbone
        public static ConfigEntry<int> BaseMinBonemassWishbone;
        public static ConfigEntry<int> BaseMaxBonemassWishbone;
        public static ConfigEntry<float> BaseChanceBonemassWishbone;
        public static ConfigEntry<bool> BaseLevelMultiplierBonemassWishbone;
        //Trophy
        public static ConfigEntry<int> BaseMinBonemassTrophy;
        public static ConfigEntry<int> BaseMaxBonemassTrophy;
        public static ConfigEntry<float> BaseChanceBonemassTrophy;
        public static ConfigEntry<bool> BaseLevelMultiplierBonemassTrophy;
        //Dragon
        //DragonTear
        public static ConfigEntry<int> BaseMinDragonTear;
        public static ConfigEntry<int> BaseMaxDragonTear;
        public static ConfigEntry<float> BaseChanceDragonTear;
        public static ConfigEntry<bool> BaseLevelMultiplierDragonTear;
        //Trophy
        public static ConfigEntry<int> BaseMinDragonQueenTrophy;
        public static ConfigEntry<int> BaseMaxDragonQueenTrophy;
        public static ConfigEntry<float> BaseChanceDragonQueenTrophy;
        public static ConfigEntry<bool> BaseLevelMultiplierDragonQueenTrophy;
        //GoblinKing
        //YagluthDrop
        public static ConfigEntry<int> BaseMinGoblinKingYagluthDrop;
        public static ConfigEntry<int> BaseMaxGoblinKingYagluthDrop;
        public static ConfigEntry<float> BaseChanceGoblinKingYagluthDrop;
        public static ConfigEntry<bool> BaseLevelMultiplierGoblinKingYagluthDrop;
        //Trophy
        public static ConfigEntry<int> BaseMinGoblinKingTrophy;
        public static ConfigEntry<int> BaseMaxGoblinKingTrophy;
        public static ConfigEntry<float> BaseChanceGoblinKingTrophy;
        public static ConfigEntry<bool> BaseLevelMultiplierGoblinKingTrophy;*/
    }
}
