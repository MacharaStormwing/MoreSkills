using System.Collections.Generic;
using HarmonyLib;
using MoreSkills.Config;
using MoreSkills.Utility;
using UnityEngine;

namespace MoreSkills.ModSkills.NewSkills
{
    class MoreSkills_Fishing
    {
        [HarmonyPatch(typeof(FishingFloat), nameof(FishingFloat.Nibble))]
        public static class FishFloat_Nibble_Patch
        {
            public static void Postfix(ref FishingFloat __instance)
            {
                if (MoreSkills_Instances._player != null)
                {
                    if (MoreSkills_FishingConfig.EnableFishingSkill.Value)
                    {
                        float level = MoreSkills_Instances._player.GetSkillFactor((Skills.SkillType)MoreSkills_FishingConfig.FishingSkill_Type);
                        float AHLevel = (float)MoreSkills_FishingConfig.AutoHookLevel.Value;
                        float AHPercentage = (float)MoreSkills_FishingConfig.AutoHookPercentage.Value;

                        if (MoreSkills_FishingConfig.EnableFishingStaminaMod.Value)
                        {
                            if (level <= 0.5)
                                __instance.m_hookedStaminaPerSec = 1f - (2 * (level * 2));
                            else
                                __instance.m_hookedStaminaPerSec = -1 * ((level - 1f) * 2);

                            __instance.m_pullStaminaUse = 10f - (8f * level);
                        }

                        __instance.m_moveForce = 5f + (15f * level);

                        if (MoreSkills_FishingConfig.EnableFishingAutoHook.Value)
                        {
                            if (level <= (AHLevel / 100))
                            {
                                mathAutoHook = 1 - ((AHPercentage / 100) * ((100 / AHLevel) * level));
                            }
                            else
                                mathAutoHook = 1 - ((AHPercentage / 100) * 1);

                            if (Random.value > mathAutoHook)
                            {
                                if (__instance.m_nibbler != null && Time.time - __instance.m_nibbleTime < 5f && __instance.GetCatch() == null)
                                {
                                    Fish nibbler = __instance.m_nibbler;
                                    __instance.Message("$msg_fishing_hooked", true);
                                    __instance.SetCatch(nibbler);
                                }
                            }
                        }
                    }
                }
            }
            public static float mathAutoHook;
        }

        [HarmonyPatch(typeof(Fish), nameof(Fish.CustomFixedUpdate))]
        public static class Fish_SpeedNHeight_Patch
        {
            public static bool Prefix(ref float ___m_speed, ref float ___m_height, ref Fish __instance)
            {
                if (MoreSkills_Instances._player != null)
                {
                    if (MoreSkills_FishingConfig.EnableFishingSkill.Value)
                    {
                        if (MoreSkills_FishingConfig.EnableFishVariety.Value)
                        {
                            if (__instance.m_nview.GetZDO().GetFloat("fishspeed", -1.0f) != -1.0f)
                            {
                                ___m_speed = __instance.m_nview.GetZDO().GetFloat("fishspeed");
                                __instance.m_nview.GetZDO().Set("fishspeed", -1.0f);
                            }

                            if (__instance.m_nview.GetZDO().GetFloat("fishheight", -1.0f) != -1.0f)
                            {
                                ___m_height = __instance.m_nview.GetZDO().GetFloat("fishheight");
                                __instance.m_nview.GetZDO().Set("fishheight", -1.0f);
                            }
                        }
                    }
                }
                return true;
            }
        }

        [HarmonyPatch(typeof(Fish), nameof(Fish.CustomFixedUpdate))]
        public static class Fish_VarietyNMods_Patch
        {
            public static void Postfix(ref Fish __instance, ref float ___m_speed, ref float ___m_height)
            {
                if (MoreSkills_Instances._player != null)
                {
                    if (MoreSkills_FishingConfig.EnableFishingSkill.Value)
                    {
                        string FishName = __instance.name.Replace("(Clone)", "");
                        string FNZDOID = FishName + ":" + __instance.GetZDOID().ToString();
                        var fishStats = fStats.Find(fstat => fstat.FishName == FishName);
                        var fishSizes = fSizes.Find(fsize => fsize.FishNameZDOID == FNZDOID);
                        float level = MoreSkills_Instances._player.GetSkillFactor((Skills.SkillType)MoreSkills_FishingConfig.FishingSkill_Type);

                        /*if (!__instance.m_nview.GetZDO().m_floats.ContainsKey(-2070439128))
                            __instance.m_nview.GetZDO().m_floats.Add(-2070439128, ___m_speed);*/

                        if (MoreSkills_FishingConfig.EnableFishVariety.Value)
                        {
                            if (fishSizes.FishNameZDOID != FNZDOID)
                            {
                                float fScale = Random.Range(0.25f, 2f);
                                float fatscale = Random.Range(-1 * (fScale * 0.5f), fScale * 0.5f);
                                float longscale = Random.Range(-1 * (fScale * 0.5f), fScale * 0.5f);
                                Vector3 vScale;
                                vScale.x = fScale + fatscale;
                                vScale.y = fScale;
                                vScale.z = fScale + longscale;
                                __instance.m_body.transform.localScale = vScale;
                                __instance.m_nview.GetZDO().Set("fishspeed", (___m_speed / ((__instance.m_body.transform.localScale.x + __instance.m_body.transform.localScale.y + __instance.m_body.transform.localScale.z) / 3)));
                                __instance.m_nview.GetZDO().Set("fishheight", (___m_height * __instance.m_body.transform.localScale.y));
                                fSizes.Add(new Helper.FishSizes(
                                    fishnamezdoid: FNZDOID,
                                    fishsize: vScale,
                                    fishheight: __instance.m_height,
                                    fishspeed: __instance.m_speed));
                            }
                        }

                        /*if (fishSizes.FishNameZDOID == FNZDOID)
                        {
                            __instance.m_height = fishSizes.FishHeight * __instance.m_body.transform.localPosition.x;
                            __instance.m_speed = fishSizes.FishSpeed / __instance.m_body.transform.localPosition.x;
                            __instance.m_acceleration = fishSizes.FishAcceleration / __instance.m_body.transform.localPosition.x;
                            __instance.m_swimRange = fishSizes.FishSwimRange * __instance.m_body.transform.localPosition.x;
                        }*/

                        if (MoreSkills_FishingConfig.EnableFishingStaminaMod.Value || MoreSkills_FishingConfig.EnableFishStackMod.Value || MoreSkills_FishingConfig.EnableFishBaseHookMod.Value)
                        {
                            if (fishStats.FishName != FishName)
                            {
                                fStats.Add(new Helper.FishStats(
                                    fishname: FishName,
                                    bhookchance: __instance.m_baseHookChance,
                                    itemstacksize: __instance.m_pickupItemStackSize,
                                    staminause: __instance.m_staminaUse));
                            }

                            if (fishStats.FishName == FishName)
                            {
                                if (MoreSkills_FishingConfig.EnableFishBaseHookMod.Value)
                                    __instance.m_baseHookChance = fishStats.BHookChance + ((1 - fishStats.BHookChance) * level);
                                if (MoreSkills_FishingConfig.EnableFishStackMod.Value)
                                    __instance.m_pickupItemStackSize = fishStats.ItemStackSize + Mathf.RoundToInt(((fishStats.ItemStackSize * ((fishSizes.FishSize.x + fishSizes.FishSize.y + fishSizes.FishSize.z) / 3)) * (MoreSkills_FishingConfig.FishDropMultiplier.Value - 1)) * level);
                                if (MoreSkills_FishingConfig.EnableFishingStaminaMod.Value)
                                    __instance.m_staminaUse = ((fishStats.StaminaUse * (((fishSizes.FishSize.x + fishSizes.FishSize.y + fishSizes.FishSize.z) / 3) / 2)) - ((((fishStats.StaminaUse * (((fishSizes.FishSize.x + fishSizes.FishSize.y + fishSizes.FishSize.z) / 3) / 2)) / 4) * 3) * level));
                            }
                        }
                    }
                }
            }
        }

        [HarmonyPatch(typeof(FishingFloat), nameof(FishingFloat.FixedUpdate))]
        public class FishingSkill_RaiseSkill
        {
            public static void Postfix(ref FishingFloat __instance)
            {
                if (MoreSkills_Instances._player != null)
                {
                    if (MoreSkills_FishingConfig.EnableFishingSkill.Value)
                    {
                        if (__instance.GetCatch() != null)
                        {
                            string FNZDOID = (__instance.GetCatch().name.Replace("(Clone)", "") + ":" + __instance.GetCatch().GetZDOID());
                            var fishSizes = fSizes.Find(fsize => fsize.FishNameZDOID == FNZDOID);
                            if (!sOldLength)
                            {
                                oldLength = __instance.m_lineLength;
                                sOldLength = true;
                            }

                            if ((oldLength - __instance.m_lineLength) > 0)
                            {
                                FishingSkillIncrease += ((oldLength - __instance.m_lineLength) / 2) * ((fishSizes.FishSize.x + fishSizes.FishSize.y + fishSizes.FishSize.z) / 3);
                                oldLength = 0;
                                sOldLength = false;
                            }

                            if (FishingSkillIncrease > 0.5f)
                            {
                                MoreSkills_Instances._player.RaiseSkill((Skills.SkillType)MoreSkills_FishingConfig.FishingSkill_Type, FishingSkillIncrease);
                                FishingSkillIncrease = 0;
                            }
                        }
                        else
                        {
                            oldLength = 0;
                            sOldLength = false;
                            __instance.m_pullStaminaUse = 0f;
                        }
                    }
                }
            }
            public static float FishingSkillIncrease;
            public static float oldLength;
            public static bool sOldLength;
        }

        public static List<Helper.FishStats> fStats = new List<Helper.FishStats>();
        public static List<Helper.FishSizes> fSizes = new List<Helper.FishSizes>();
    }
}
