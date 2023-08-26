using HarmonyLib;
using MoreSkills.Config;
using MoreSkills.Utility;
using UnityEngine;

namespace MoreSkills.ModSkills
{
    class MoreSkills_Sailing
    {
        // TODO: veriy if change from UpdateShipControl to GetSailForce works
        //[HarmonyPatch(typeof(Player), "UpdateShipControl")]
        [HarmonyPatch(typeof(Ship), nameof(Ship.GetSailForce))]
        public static class Sailing_Skill
        {
            public static void Postfix()
            {
                if (MoreSkills_Instances._player != null)
                {
                    if (MoreSkills_SailingConfig.EnableSailingMod.Value)
                    {
                        if (MoreSkills_Instances._player.GetControlledShip() != null)
                        {
                            float sailing_skill = Mathf.Floor((MoreSkills_Instances._player.GetSkillFactor((Skills.SkillType)MoreSkills_SailingConfig.SailingSkill_Type) * 100f) + 0.000001f);
                            float bforce_skillinc = ((MoreSkills_SailingConfig.BaseMaxBForce.Value - MoreSkills_SailingConfig.BaseBForce.Value) / 100) * sailing_skill;
                            float rspeed_skillinc = ((MoreSkills_SailingConfig.BaseMaxRSpeed.Value - MoreSkills_SailingConfig.BaseRSpeed.Value) / 100) * sailing_skill;
                            float sforce_skillinc = ((MoreSkills_SailingConfig.BaseMaxSForce.Value - MoreSkills_SailingConfig.BaseSForce.Value) / 100) * sailing_skill;
                            float dsideways_skillinc = ((MoreSkills_SailingConfig.BaseMaxDSideways.Value - MoreSkills_SailingConfig.BaseDSideways.Value) / 100) * sailing_skill;
                            float mbforce = MoreSkills_Instances._player.GetControlledShip().m_backwardForce;
                            float msforce = MoreSkills_Instances._player.GetControlledShip().m_sailForceFactor;
                            float CrewMult = ((MoreSkills_SailingConfig.BaseBForce.Value + bforce_skillinc) + (((MoreSkills_SailingConfig.CrewMembersBForceAdd.Value * bforce_skillinc) / 5) * MoreSkills_Instances._player.GetControlledShip().m_players.Count));

                            if (!MoreSkills_SailingConfig.EnableShipAcceleration.Value)
                            {
                                if (!MoreSkills_SailingConfig.EnableShipCrew.Value)
                                    MoreSkills_Instances._player.GetControlledShip().m_backwardForce = MoreSkills_SailingConfig.BaseBForce.Value + bforce_skillinc;
                                else
                                    MoreSkills_Instances._player.GetControlledShip().m_backwardForce = (MoreSkills_SailingConfig.BaseBForce.Value + bforce_skillinc) + (((MoreSkills_SailingConfig.CrewMembersBForceAdd.Value * bforce_skillinc) / 5) * MoreSkills_Instances._player.GetControlledShip().m_players.Count);
                                MoreSkills_Instances._player.GetControlledShip().m_sailForceFactor = MoreSkills_SailingConfig.BaseSForce.Value + sforce_skillinc;
                            }
                            else
                            {
                                if (MoreSkills_Instances._player.GetControlledShip().GetSpeedSetting().ToString() == "Stop")
                                {
                                    MoreSkills_Instances._player.GetControlledShip().m_sailForceFactor = 0.01f;
                                    MoreSkills_Instances._player.GetControlledShip().m_backwardForce = 0.01f;
                                }
                                else if (msforce < (MoreSkills_SailingConfig.BaseSForce.Value + sforce_skillinc) && (MoreSkills_Instances._player.GetControlledShip().GetSpeedSetting().ToString() == "Half" || MoreSkills_Instances._player.GetControlledShip().GetSpeedSetting().ToString() == "Full"))
                                    MoreSkills_Instances._player.GetControlledShip().m_sailForceFactor += 0.0001f;
                                else if (mbforce < (MoreSkills_SailingConfig.BaseBForce.Value + bforce_skillinc) && !MoreSkills_SailingConfig.EnableShipCrew.Value && (MoreSkills_Instances._player.GetControlledShip().GetSpeedSetting().ToString() == "Slow" || MoreSkills_Instances._player.GetControlledShip().GetSpeedSetting().ToString() == "Back"))
                                    MoreSkills_Instances._player.GetControlledShip().m_backwardForce += 0.001f;
                                else if (mbforce < CrewMult && MoreSkills_SailingConfig.EnableShipCrew.Value && (MoreSkills_Instances._player.GetControlledShip().GetSpeedSetting().ToString() == "Slow" || MoreSkills_Instances._player.GetControlledShip().GetSpeedSetting().ToString() == "Back"))
                                    MoreSkills_Instances._player.GetControlledShip().m_backwardForce += 0.001f;
                            }

                            MoreSkills_Instances._player.GetControlledShip().m_rudderSpeed = MoreSkills_SailingConfig.BaseRSpeed.Value + rspeed_skillinc;
                            MoreSkills_Instances._player.GetControlledShip().m_dampingSideway = MoreSkills_SailingConfig.BaseDSideways.Value + dsideways_skillinc;

                            if (MoreSkills_Instances._player.GetVelocity().magnitude >= 1)
                                if (Captain < 0.50f)
                                {
                                    Captain += ((MoreSkills_Instances._player.GetVelocity().magnitude / 4000) * MoreSkills_SailingConfig.SailingSkillIncreaseMultiplier.Value);
                                }
                                else
                                {
                                    MoreSkills_Instances._player.RaiseSkill((Skills.SkillType)MoreSkills_SailingConfig.SailingSkill_Type, Captain);
                                    Captain = 0f;
                                }


                        }
                        else if (MoreSkills_Instances._player.GetStandingOnShip() != null || SittingInBoat)
                        {

                            if (MoreSkills_Instances._player.IsAttached())
                            {
                                SittingInBoat = true;
                                if (MoreSkills_Instances._player.GetVelocity().magnitude >= 1)
                                    if (SittingCrew < 0.50f)
                                    {
                                        SittingCrew += ((MoreSkills_Instances._player.GetVelocity().magnitude / 8000) * MoreSkills_SailingConfig.SailingSkillIncreaseMultiplier.Value);
                                    }
                                    else
                                    {
                                        MoreSkills_Instances._player.RaiseSkill((Skills.SkillType)MoreSkills_SailingConfig.SailingSkill_Type, SittingCrew);
                                        SittingCrew = 0f;
                                    }

                            }
                            else
                            {
                                SittingInBoat = false;
                                if (MoreSkills_Instances._player.GetVelocity().magnitude >= 1)
                                    if (StandingCrew < 0.50f)
                                    {
                                        StandingCrew += ((MoreSkills_Instances._player.GetVelocity().magnitude / 16000) * MoreSkills_SailingConfig.SailingSkillIncreaseMultiplier.Value);
                                    }
                                    else
                                    {
                                        MoreSkills_Instances._player.RaiseSkill((Skills.SkillType)MoreSkills_SailingConfig.SailingSkill_Type, StandingCrew);
                                        StandingCrew = 0f;
                                    }
                            }
                        }
                    }
                }
            }

            public static bool SittingInBoat;
            public static float Captain;
            public static float SittingCrew;
            public static float StandingCrew;
        }
    }
}
