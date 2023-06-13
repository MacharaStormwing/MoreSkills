using HarmonyLib;
using MoreSkills.Config;
using MoreSkills.Utility;
using System;

namespace MoreSkills.ModSkills
{
    class MoreSkills_SwimMod
    {
        [HarmonyPatch(typeof(Player), "UpdateStats")]
        public static class Swim_SwimMod
        {
            public static void Postfix()
            {
                if (MoreSkills_OverhaulsConfig.EnableSwimMod.Value)
                    if (MoreSkills_Instances._player != null)
                    {
                        float swim_skill = (float)Math.Floor((MoreSkills_Instances._player.GetSkillFactor((Skills.SkillType.Swim)) * 100f) + 0.000001f);
                        float swimspeed_skillinc = ((MoreSkills_OverhaulsConfig.BaseMaxSwimSpeed.Value - MoreSkills_OverhaulsConfig.BaseSwimSpeed.Value) / 100) * swim_skill;
                        float swimstamina_skillinc = (swim_skill / 5000) * MoreSkills_OverhaulsConfig.SwimStaminaMultiplier.Value;

                        //Swim Speed Mod
                        if (MoreSkills_OverhaulsConfig.EnableSwimSpeedMod.Value)
                        {

                            MoreSkills_Instances._player.m_swimSpeed = MoreSkills_OverhaulsConfig.BaseSwimSpeed.Value + swimspeed_skillinc;
                        }

                        //Stamina Regen Swimming, and still.
                        if (MoreSkills_OverhaulsConfig.EnableSwimStaminaMod.Value)
                        {
                            Character _character = MoreSkills_Instances._player;
                            if (MoreSkills_Instances._player.IsSwimming() && MoreSkills_Instances._player.GetVelocity().magnitude < 0.6f)
                            {
                                if (count > 200) count++;
                                MoreSkills_Instances._player.AddStamina(swimstamina_skillinc);
                            }
                            else
                            {
                                count = 0;
                            }
                        }
                    }
            }
            public static int count;
        }
    }
}
