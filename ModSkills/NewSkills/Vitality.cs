using HarmonyLib;
using MoreSkills.Config;
using MoreSkills.Utility;
using System.Reflection;
using UnityEngine;

namespace MoreSkills.ModSkills
{
    class MoreSkills_Vitality
    {
        [HarmonyPatch(typeof(Player), nameof(Player.SetMaxHealth))]
        public static class Vitality_ApplyBaseHealthMod
        {
            public static void Prefix(ref float health)
            {
                if (MoreSkills_Instances._player != null)
                {
                    if (MoreSkills_VitalityConfig.EnableHealthMod.Value)
                    {
                        float health_skill = Mathf.Floor((MoreSkills_Instances._player.GetSkillFactor((Skills.SkillType)MoreSkills_VitalityConfig.VitalitySkill_Type) * 100f) + 0.000001f);
                        float health_skillinc = ((MoreSkills_VitalityConfig.BaseMaxHealth.Value - MoreSkills_VitalityConfig.BaseHealth.Value) / 100) * health_skill;
                        health += (MoreSkills_VitalityConfig.BaseHealth.Value + health_skillinc) - 25f;
                    }
                }
            }
        }

        [HarmonyPatch]
        public static class Vitality_HealthRegen
        {
            public static MethodBase TargetMethod()
            {
                return AccessTools.DeclaredMethod(typeof(Player), nameof(Player.UpdateStats), new System.Type[0]);
            }

            public static void Postfix()
            {
                if (MoreSkills_Instances._player != null)
                {
                    if (MoreSkills_VitalityConfig.EnableHealthMod.Value)
                    {
                        if (MoreSkills_Instances._player.GetHealth() < MoreSkills_Instances._player.GetMaxHealth())
                        {
                            if (Damaged)
                            {
                                if (lastattackcounter > 1800)
                                {
                                    Damaged = false;
                                }
                                else
                                    lastattackcounter++;
                            }
                            else
                            {
                                if (healcounter > 600)
                                {
                                    MoreSkills_Instances._player.Heal(MoreSkills_Instances._player.GetSkillFactor((Skills.SkillType)MoreSkills_VitalityConfig.VitalitySkill_Type), true);
                                    healcounter = 0;
                                }
                                else
                                    healcounter++;
                            }
                        }

                    }
                }
            }
        }

        [HarmonyPatch(typeof(Player), nameof(Player.OnDamaged))]
        public static class Vitality_RaiseSkill
        {
            public static void Postfix(ref HitData hit)
            {
                if (MoreSkills_Instances._player != null)
                {
                    if (MoreSkills_VitalityConfig.EnableHealthMod.Value)
                    {
                        Damaged = true;
                        healcounter = 0;
                        lastattackcounter = 0;
                        if (IncVitality < 0.10f)
                        {
                            IncVitality += ((hit.GetTotalDamage() / 10) * MoreSkills_VitalityConfig.VitalitySkillIncreaseMultiplier.Value);
                        }
                        else
                        {
                            MoreSkills_Instances._player.RaiseSkill((Skills.SkillType)MoreSkills_VitalityConfig.VitalitySkill_Type, IncVitality);
                            IncVitality = 0f;
                        }

                    }
                }
            }
            public static float IncVitality;
        }
        public static int lastattackcounter;
        public static int healcounter;
        public static bool Damaged;
    }
}
