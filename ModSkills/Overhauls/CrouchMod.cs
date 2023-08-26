using HarmonyLib;
using MoreSkills.Config;
using MoreSkills.Utility;
using System;
using System.Reflection;

namespace MoreSkills.ModSkills
{
    class MoreSkills_CrouchMod
    {
        [HarmonyPatch]
        public static class Sneak_CrouchSpeedMod
        {
            public static MethodBase TargetMethod()
            {
                return AccessTools.DeclaredMethod(typeof(Player), nameof(Player.UpdateStats), new System.Type[0]);
            }

            public static void Postfix()
            {
                if (MoreSkills_OverhaulsConfig.EnableCrouchMod.Value)
                    if (MoreSkills_Instances._player != null)
                    {
                        if (MoreSkills_Instances._player.IsCrouching())
                        {
                            float sneak_skill = (float)Math.Floor((MoreSkills_Instances._player.GetSkillFactor((Skills.SkillType.Sneak)) * 100f) + 0.000001f);
                            float sneak_skillinc = ((MoreSkills_OverhaulsConfig.BaseMaxCrouchSpeed.Value - MoreSkills_OverhaulsConfig.BaseCrouchSpeed.Value) / 100) * sneak_skill;
                            MoreSkills_Instances._player.m_crouchSpeed = MoreSkills_OverhaulsConfig.BaseCrouchSpeed.Value + sneak_skillinc;
                        }
                        else
                            MoreSkills_Instances._player.m_crouchSpeed = 2f;

                        if (MoreSkills_Instances._player.IsEncumbered())
                        {
                            MoreSkills_Instances._player.SetCrouch(false);
                        }
                    }
            }
        }
    }
}
