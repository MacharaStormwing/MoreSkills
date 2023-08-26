using HarmonyLib;
using System;
using System.Collections.Generic;
using MoreSkills.Config;
using MoreSkills.Utility;
using UnityEngine;

namespace MoreSkills.ModSkills.NewSkills
{
    class MoreSkills_Hunting
    {
        [HarmonyPatch(typeof(CharacterDrop), nameof(CharacterDrop.Start))]
        public static class Hunting_UpdateDrops
        {
            public static void Postfix(ref CharacterDrop __instance)
            {
                if (MoreSkills_Instances._player != null)
                {
                    if (MoreSkills_HuntingConfig.EnableHuntingSkill.Value)
                    {
                        bool MinMax = MoreSkills_HuntingConfig.EnableHuntingMinMaxMod.Value;
                        bool Chance = MoreSkills_HuntingConfig.EnableHuntingChanceMod.Value;
                        //bool Trophy = MoreSkills_HuntingConfig.EnableHuntingTrophyMod.Value;
                        float level = MoreSkills_Instances._player.GetSkillFactor((Skills.SkillType)MoreSkills_HuntingConfig.HuntingSkill_Type);
                        float realmult = (MoreSkills_HuntingConfig.HuntingDropMultiplier.Value - 1);
                        foreach (CharacterDrop.Drop drop in __instance.m_drops)
                        {
                            float vMin = 0f;
                            float vMax = 0f;
                            float vChance = 0f;
                            string nameprefab = string.Concat(__instance.name + ":" + drop.m_prefab);
                            string sNameprefab = hDrops.Find(hDrop => hDrop.CreaturePrefab == nameprefab).CreaturePrefab;

                            if (nameprefab != hDrops.Find(hDrop => hDrop.CreaturePrefab == nameprefab).CreaturePrefab)
                            {
                                hDrops.Add(new Helper.HuntingDrops(
                                    creatureprefab: nameprefab,
                                    max: drop.m_amountMax,
                                    min: drop.m_amountMin,
                                    chance: drop.m_chance));
                                Utilities.Log("Hunting: Added Creature to the Temporal Hunting Database");
                            }

                            //Utilities.Log("Updating mob: " + __instance.name.Replace("(Clone)", "") + " Level: " + __instance.m_character.GetLevel() + " Item: " + drop.m_prefab.name.Replace("(UnityEngine.GameObject)", "") + " Min: " + drop.m_amountMin + " Max: " + drop.m_amountMax + " Chance: " + drop.m_chance);
                            //Utilities.LogWarning("Name ins: " + nameprefab + " Name list: " + hDrops.Find(hDrop => hDrop.CreaturePrefab == nameprefab).CreaturePrefab);

                            if (!MoreSkills_HuntingConfig.EnableHuntingNormalMobsMod.Value)
                                if (!drop.m_prefab.name.Contains("Trophy") || !drop.m_prefab.name.Contains("Blob"))
                                    break;

                            if (!MoreSkills_HuntingConfig.EnableHuntingTrophyMod.Value && drop.m_prefab.name.Contains("Trophy"))
                                break;

                            if (!MoreSkills_HuntingConfig.EnableHuntingBlobSpawn.Value && drop.m_prefab.name.Contains("Blob"))
                                break;

                            if (nameprefab == hDrops.Find(hDrop => hDrop.CreaturePrefab == nameprefab).CreaturePrefab)
                            {
                                vMin = (float)(hDrops.Find(hDrop => hDrop.CreaturePrefab == nameprefab).Min);
                                vMax = (float)(hDrops.Find(hDrop => hDrop.CreaturePrefab == nameprefab).Max);
                                vChance = (float)(hDrops.Find(hDrop => hDrop.CreaturePrefab == nameprefab).Chance);
                                if (MoreSkills_HuntingConfig.EnableTrialsOfOdinCompatibility.Value)
                                {
                                    if (__instance.m_character.GetLevel() >= MoreSkills_HuntingConfig.ToOLowLevelStart.Value && __instance.m_character.GetLevel() < MoreSkills_HuntingConfig.ToOMidLevelStart.Value)
                                        CLevel = 1;
                                    else if (__instance.m_character.GetLevel() >= MoreSkills_HuntingConfig.ToOMidLevelStart.Value && __instance.m_character.GetLevel() < MoreSkills_HuntingConfig.ToOHighLevelStart.Value)
                                        CLevel = 2;
                                    else if (__instance.m_character.GetLevel() >= MoreSkills_HuntingConfig.ToOHighLevelStart.Value)
                                        CLevel = 3;
                                }
                                else
                                    CLevel = __instance.m_character.GetLevel();

                                if (MinMax)
                                {
                                    if ((level) > 0.5)
                                    {
                                        float maxskill_inc = Mathf.Round(vMax * realmult);
                                        drop.m_amountMax = (int)((vMax + maxskill_inc) * CLevel);
                                        float skill = level * realmult;
                                        float skill_inc = Mathf.Round(vMin * skill);
                                        drop.m_amountMin = (int)((vMin + skill_inc) * CLevel);
                                    }
                                    else
                                    {
                                        float maxskill = (level * 2) * realmult;
                                        float maxskill_inc = Mathf.Round(vMax * maxskill);
                                        drop.m_amountMax = (int)((vMax + maxskill_inc) * CLevel);
                                    }
                                }
                                if (Chance)
                                    drop.m_chance = vChance + ((1 - vChance) * level);

                                //Utilities.Log("Updated mob: " + __instance.name.Replace("(Clone)", "") + " Level: " + __instance.m_character.GetLevel() + " Item: " + drop.m_prefab.name.Replace("(UnityEngine.GameObject)", "") + " Min: " + drop.m_amountMin + " Max: " + drop.m_amountMax + " Chance: " + drop.m_chance);
                            }
                        }
                        //Utilities.Log("Hunting: Updated mob's loot: " + __instance.name.Replace("(Clone)", "") + " Level: " + __instance.m_character.GetLevel());
                    }
                }
            }
        }

        [HarmonyPatch(typeof(CharacterDrop), nameof(CharacterDrop.OnDeath))]
        public static class Hunting_RaiseSkill
        {
            public static void Postfix(ref CharacterDrop __instance)
            {
                if (MoreSkills_Instances._player != null)
                {
                    if (MoreSkills_HuntingConfig.EnableHuntingSkill.Value)
                    {
                        if (MoreSkills_Instances._CDAttacker == MoreSkills_Instances._player.GetZDOID())
                        {
                            HuntIncrease += ((__instance.m_character.GetMaxHealth()) / 20) * CLevel;
                            //Utilities.LogWarning("Vida máx: " + __instance.m_character.GetMaxHealth() + " Nivel: " + __instance.m_character.GetLevel());
                        }

                        if (HuntIncrease > 0.1f)
                        {
                            MoreSkills_Instances._player.RaiseSkill((Skills.SkillType)MoreSkills_HuntingConfig.HuntingSkill_Type, HuntIncrease);
                            HuntIncrease = 0f;
                        }
                    }
                }
            }

            public static float HuntIncrease;
        }

        public static int CLevel;
        public static List<Helper.HuntingDrops> hDrops = new List<Helper.HuntingDrops>();
    }
}
