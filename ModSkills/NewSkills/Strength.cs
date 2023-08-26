using HarmonyLib;
using MoreSkills.Config;
using MoreSkills.Utility;
using System.Reflection;
using UnityEngine;

namespace MoreSkills.ModSkills
{
    class MoreSkills_Strength
    {
        [HarmonyPatch]
        public static class RaiseSkill_Strength
        {
            public static MethodBase TargetMethod()
            {
                return AccessTools.DeclaredMethod(typeof(Player), nameof(Player.UpdateStats), new System.Type[0]);
            }

            public static void Postfix()
            {
                if (MoreSkills_Instances._player != null)
                {
                    if (MoreSkills_StrengthConfig.EnableStrengthMod.Value)
                    {
                        bool halfweight = MoreSkills_Instances._player.GetInventory().GetTotalWeight() > (MoreSkills_Instances._player.GetMaxCarryWeight() / 2);
                        float vel = MoreSkills_Instances._player.m_currentVel.magnitude;
                        bool moving = Mathf.Floor(vel) > 1;
                        float weight_skill = Mathf.Floor((MoreSkills_Instances._player.GetSkillFactor((Skills.SkillType)MoreSkills_StrengthConfig.StrengthSkill_Type) * 100f) + 0.000001f);
                        float weight_skillinc = ((MoreSkills_StrengthConfig.BaseMaxWeight.Value - MoreSkills_StrengthConfig.BaseWeight.Value) / 100f) * weight_skill;
                        float strengthstamina_skillinc = (weight_skill / ((MoreSkills_Instances._player.GetInventory().GetTotalWeight() - MoreSkills_Instances._player.GetMaxCarryWeight()) * 50) * MoreSkills_StrengthConfig.StrengthStaminaMultiplier.Value);

                        if (MoreSkills_StrengthConfig.EnableWeightMod.Value)
                            MoreSkills_Instances._player.m_maxCarryWeight = (MoreSkills_StrengthConfig.BaseWeight.Value + weight_skillinc);

                        if (MoreSkills_StrengthConfig.EnableStrengthStaminaMod.Value)
                            if (MoreSkills_Instances._player.IsEncumbered() && MoreSkills_Instances._player.GetVelocity().magnitude < 0.1f)
                            {
                                if (count > 200) count++;
                                MoreSkills_Instances._player.AddStamina(strengthstamina_skillinc);
                            }
                            else
                            {
                                count = 0;
                            }

                        if (halfweight && moving)
                            if (MoreSkills_Instances._player.IsEncumbered())
                            {
                                if (IncEncumbred < 0.10f)
                                    IncEncumbred += ((MoreSkills_Instances._player.GetInventory().GetTotalWeight() / 20000) * MoreSkills_StrengthConfig.StrengthSkillIncreaseMultiplier.Value);
                                else
                                {
                                    MoreSkills_Instances._player.RaiseSkill((Skills.SkillType)MoreSkills_StrengthConfig.StrengthSkill_Type, IncEncumbred);
                                    IncEncumbred = 0f;
                                }
                            }
                            else if (halfweight)
                            {
                                if (MoreSkills_Instances._player.IsRunning())
                                {
                                    if (IncHalfnRunning < 0.10f)
                                        IncHalfnRunning += ((MoreSkills_Instances._player.GetInventory().GetTotalWeight() / 40000) * MoreSkills_StrengthConfig.StrengthSkillIncreaseMultiplier.Value);
                                    else
                                    {
                                        MoreSkills_Instances._player.RaiseSkill((Skills.SkillType)MoreSkills_StrengthConfig.StrengthSkill_Type, IncHalfnRunning);
                                        IncHalfnRunning = 0f;
                                    }
                                }
                                else
                                {
                                    if (IncHalfnMoving < 0.10f)
                                        IncHalfnMoving += ((MoreSkills_Instances._player.GetInventory().GetTotalWeight() / 60000) * MoreSkills_StrengthConfig.StrengthSkillIncreaseMultiplier.Value);
                                    else
                                    {
                                        MoreSkills_Instances._player.RaiseSkill((Skills.SkillType)MoreSkills_StrengthConfig.StrengthSkill_Type, IncHalfnMoving);
                                        IncHalfnMoving = 0f;
                                    }
                                }
                            }
                    }
                }
            }

            public static float IncEncumbred;
            public static float IncHalfnRunning;
            public static float IncHalfnMoving;
            public static int count;
        }
    }
}
