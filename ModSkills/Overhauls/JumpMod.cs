using HarmonyLib;
using MoreSkills.Config;
using MoreSkills.Utility;
using System.Reflection;
using UnityEngine;

namespace MoreSkills.ModSkills.Overhauls
{
    class MoreSkills_JumpMod
    {
        [HarmonyPatch]
        public static class TestJump
        {
            public static MethodBase TargetMethod()
            {
                return AccessTools.DeclaredMethod(typeof(Player), nameof(Player.UpdateStats), new System.Type[0]);
            }

            public static void Postfix(ref Character __instance)
            {
                if (MoreSkills_Instances._player != null)
                {
                    if (MoreSkills_OverhaulsConfig.EnableJumpMod.Value)
                    {
                        if (MoreSkills_OverhaulsConfig.EnableHigherJump.Value)
                        {
                            if (MoreSkills_OverhaulsConfig.EnableHaveToShiftToHigherJump.Value)
                            {
                                if (Input.GetKey(MoreSkills_OverhaulsConfig.HigherJumpKey.Value))
                                    MoreSkills_Instances._player.m_jumpForce = MoreSkills_OverhaulsConfig.BaseJumpForce.Value + (MoreSkills_OverhaulsConfig.BaseMaxJumpForce.Value - MoreSkills_OverhaulsConfig.BaseJumpForce.Value) * MoreSkills_Instances._player.GetSkillFactor(Skills.SkillType.Jump);
                                else
                                    MoreSkills_Instances._player.m_jumpForce = MoreSkills_OverhaulsConfig.BaseJumpForce.Value;
                            }
                            else if (!MoreSkills_OverhaulsConfig.EnableHaveToShiftToHigherJump.Value)
                            {
                                MoreSkills_Instances._player.m_jumpForce = MoreSkills_OverhaulsConfig.BaseJumpForce.Value + (MoreSkills_OverhaulsConfig.BaseMaxJumpForce.Value - MoreSkills_OverhaulsConfig.BaseJumpForce.Value) * MoreSkills_Instances._player.GetSkillFactor(Skills.SkillType.Jump);
                            }
                        }

                        if (MoreSkills_OverhaulsConfig.EnableRollOnFall.Value)
                        {
                            float MaxARoll = MoreSkills_OverhaulsConfig.BaseRollAltitude.Value + ((MoreSkills_OverhaulsConfig.BaseMaxRollAltitude.Value - MoreSkills_OverhaulsConfig.BaseRollAltitude.Value) * MoreSkills_Instances._player.GetSkillFactor(Skills.SkillType.Jump));
                            float Altitude = Mathf.Max(0f, __instance.m_maxAirAltitude - MoreSkills_Instances._player.transform.position.y);
                            if (Altitude > MoreSkills_OverhaulsConfig.BaseRollAltitude.Value && Altitude < MaxARoll)
                            {
                                inAir = true;
                                onGround = false;
                            }
                            else if (Altitude > 8f)
                            {
                                inAir = false;
                            }

                            if (inAir)
                            {
                                if (Altitude == 0f)
                                {
                                    onGround = true;
                                    if (onGround)
                                    {
                                        Vector3 rollDir = MoreSkills_Instances._player.m_moveDir;
                                        if (rollDir.magnitude < 0.1f)
                                        {
                                            rollDir = MoreSkills_Instances._player.m_lookDir;
                                            rollDir.y = 0f;
                                            rollDir.Normalize();
                                        }
                                        MoreSkills_Instances._player.Dodge(rollDir);
                                        inAir = false;
                                    }
                                }
                            }

                            //Utilities.LogWarning("Altitud: " + Altitude);
                        }
                    }
                }
            }
        }

        [HarmonyPatch(typeof(Character), nameof(Character.Damage))]
        public static class TestDamage
        {
            public static void Prefix(ref Character __instance, ref HitData hit)
            {
                if (MoreSkills_Instances._player != null)
                {
                    if (MoreSkills_OverhaulsConfig.EnableJumpMod.Value && MoreSkills_OverhaulsConfig.EnableDecreaseFallDamageMod.Value)
                    {
                        float FDM = MoreSkills_OverhaulsConfig.FallDamageDecrease.Value;
                        float Level = MoreSkills_Instances._player.GetSkillFactor(Skills.SkillType.Jump);
                        float MaxAFall = MoreSkills_OverhaulsConfig.BaseMaxFallAltitude.Value;
                        float BaseARoll = MoreSkills_OverhaulsConfig.BaseRollAltitude.Value;
                        float MaxARoll = MoreSkills_OverhaulsConfig.BaseRollAltitude.Value + ((MoreSkills_OverhaulsConfig.BaseMaxRollAltitude.Value - MoreSkills_OverhaulsConfig.BaseRollAltitude.Value) * MoreSkills_Instances._player.GetSkillFactor(Skills.SkillType.Jump));
                        float Altitude = Mathf.Max(0f, __instance.m_maxAirAltitude - MoreSkills_Instances._player.transform.position.y);
                        float MDI;

                        if (MoreSkills_OverhaulsConfig.MaxFallDamageIncrease.Value <= 0)
                            MDI = 1f;
                        else
                            MDI = MoreSkills_OverhaulsConfig.MaxFallDamageIncrease.Value;

                        if (MoreSkills_OverhaulsConfig.EnableRollOnFall.Value)
                        {
                            if (Altitude > BaseARoll && Altitude < MaxARoll)
                            {
                                if (hit.m_attacker.UserID == 0)
                                {
                                    hit.m_damage.m_damage = 0;
                                }
                            }
                            else if (Altitude > MaxARoll)
                            {
                                if (MoreSkills_OverhaulsConfig.EnableMaxFallAltitude.Value)
                                {
                                    if (Altitude > MaxARoll && Altitude < MaxAFall)
                                    {
                                        if (hit.m_attacker.UserID == 0)
                                        {
                                            hit.m_damage.m_damage = ((100 / (MaxAFall - MaxARoll)) * (Altitude - MaxARoll)) / (FDM * Level);
                                        }
                                    }
                                    else if (Altitude > MaxAFall)
                                    {
                                        hit.m_damage.m_damage = (((100 / (MaxAFall - MaxARoll)) * (Altitude - MaxARoll)) / ((FDM * Level)) + (Altitude - MaxAFall) * MDI);
                                    }
                                }
                                else
                                {
                                    if (Altitude > MaxARoll)
                                    {
                                        if (Altitude > 20f)
                                        {
                                            if (hit.m_attacker.UserID == 0)
                                            {
                                                hit.m_damage.m_damage = 100f / (FDM * Level);
                                            }
                                        }
                                        else if (Altitude > MaxARoll && Altitude < 20f)
                                        {
                                            if (hit.m_attacker.UserID == 0)
                                            {
                                                hit.m_damage.m_damage = ((100 / (20 - MaxARoll)) * (Altitude - MaxARoll)) / (FDM * Level);
                                            }
                                        }
                                    }
                                }
                            }
                            //MoreSkills_Instances._player.Message(MessageHud.MessageType.Center, Localization.instance.Localize("Height: " + (Altitude - MaxARoll).ToString("F0") + "m" + " Damage: " + hit.m_damage.m_damage.ToString("F0")), 0, null);
                        }
                        else
                        {
                            if (MoreSkills_OverhaulsConfig.EnableMaxFallAltitude.Value)
                            {
                                if (Altitude > 4f && Altitude < MaxAFall)
                                {
                                    if (hit.m_attacker.UserID == 0)
                                        hit.m_damage.m_damage = ((100 / (MaxAFall - 4)) * (Altitude - 4)) / (FDM * Level);
                                }
                                else if (Altitude > MaxAFall)
                                {
                                    if (hit.m_attacker.UserID == 0)
                                        hit.m_damage.m_damage = (((100 / (MaxAFall - 4)) * (Altitude - 4)) / ((FDM * Level)) + (Altitude - MaxAFall) * MDI);
                                }
                            }
                            else
                            {
                                if (Altitude > 4f)
                                {
                                    if (hit.m_attacker.UserID == 0)
                                    {
                                        hit.m_damage.m_damage /= (FDM * Level);
                                    }
                                }
                            }
                            //MoreSkills_Instances._player.Message(MessageHud.MessageType.Center, Localization.instance.Localize("Height: " + (Altitude - MaxARoll).ToString("F0") + "m" + " Damage: " + hit.m_damage.m_damage.ToString("F0")), 0, null);
                        }
                    }
                }
            }
        }
        public static bool inAir;
        public static bool onGround;
    }
}
