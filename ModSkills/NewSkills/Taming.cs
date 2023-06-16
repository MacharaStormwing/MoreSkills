using HarmonyLib;
using System.Collections.Generic;
using MoreSkills.Config;
using MoreSkills.Utility;
using UnityEngine;
using System.Linq;
using System;

namespace MoreSkills.ModSkills.NewSkills
{
    class MoreSkills_Taming
    {
        [HarmonyPatch(typeof(Tameable), "OnConsumedItem")]
        public static class SetTamer
        {
            public static void Postfix(ref Tameable __instance)
            {
                if (MoreSkills_Instances._player != null)
                {
                    if (MoreSkills_TamingConfig.EnableTamingSkill.Value)
                    {
                        //Utilities.LogWarning("ZDOID ANTES: " + Creature1);
                        if (__instance.GetTameness() < 100 && !__instance.m_character.IsTamed() && !__instance.m_character.IsPlayer())
                        {
                            Player closestPlayer = null;
                            ZDOID cPlayer = ZDOID.None;
                            //Utilities.LogWarning("ZDO" + __instance.m_nview.GetZDO().GetString("TamerID"));

                            if (__instance.m_nview.GetZDO().GetString("TamerID") == "")
                            {
                                try
                                {
                                    closestPlayer = Player.GetClosestPlayer(__instance.transform.position, 30f);
                                    cPlayer = closestPlayer.GetZDOID();
                                }
                                catch (Exception)
                                {
                                    closestPlayer = null;
                                    cPlayer = ZDOID.None;
                                }
                                if (cPlayer == MoreSkills_Instances._player.GetZDOID())
                                {
                                    if (MoreSkills_TamingConfig.EnableAllTamableCompatibility.Value)
                                    {
                                        string sCreature = __instance.name.Replace("(Clone)", "");
                                        switch (__instance.name.Replace("(Clone)", ""))
                                        {
                                            case "Blob":
                                                Masterlvl1 = MoreSkills_TamingConfig.BlobLevelMaster.Value;
                                                Tamerlvl1 = MoreSkills_TamingConfig.BlobLevelTamer.Value;
                                                Unlocklvl1 = MoreSkills_TamingConfig.BlobLevelTamer.Value;
                                                break;
                                            case "BlobElite":
                                                Masterlvl1 = MoreSkills_TamingConfig.BlobEliteLevelMaster.Value;
                                                Tamerlvl1 = MoreSkills_TamingConfig.BlobEliteLevelTamer.Value;
                                                Unlocklvl1 = MoreSkills_TamingConfig.BlobEliteLevelUnlock.Value;
                                                break;
                                            case "Boar":
                                                Masterlvl1 = MoreSkills_TamingConfig.BoarLevelMaster.Value;
                                                Tamerlvl1 = MoreSkills_TamingConfig.BoarLevelTamer.Value;
                                                Unlocklvl1 = MoreSkills_TamingConfig.BoarLevelUnlock.Value;
                                                break;
                                            case "Deathsquito":
                                                Masterlvl1 = MoreSkills_TamingConfig.DeathsquitoLevelMaster.Value;
                                                Tamerlvl1 = MoreSkills_TamingConfig.DeathsquitoLevelTamer.Value;
                                                Unlocklvl1 = MoreSkills_TamingConfig.DeathsquitoLevelUnlock.Value;
                                                break;
                                            case "Hatchling":
                                                Masterlvl1 = MoreSkills_TamingConfig.DrakeLevelMaster.Value;
                                                Tamerlvl1 = MoreSkills_TamingConfig.DrakeLevelTamer.Value;
                                                Unlocklvl1 = MoreSkills_TamingConfig.DrakeLevelUnlock.Value;
                                                break;
                                            case "Draugr":
                                                Masterlvl1 = MoreSkills_TamingConfig.DraugrLevelMaster.Value;
                                                Tamerlvl1 = MoreSkills_TamingConfig.DraugrLevelTamer.Value;
                                                Unlocklvl1 = MoreSkills_TamingConfig.DraugrLevelUnlock.Value;
                                                break;
                                            case "Draugr_Elite":
                                                Masterlvl1 = MoreSkills_TamingConfig.DraugrEliteLevelMaster.Value;
                                                Tamerlvl1 = MoreSkills_TamingConfig.DraugrEliteLevelTamer.Value;
                                                Unlocklvl1 = MoreSkills_TamingConfig.DraugrEliteLevelUnlock.Value;
                                                break;
                                            case "Fenring":
                                                Masterlvl1 = MoreSkills_TamingConfig.FenringLevelMaster.Value;
                                                Tamerlvl1 = MoreSkills_TamingConfig.FenringLevelTamer.Value;
                                                Unlocklvl1 = MoreSkills_TamingConfig.FenringLevelUnlock.Value;
                                                break;
                                            case "Goblin":
                                                Masterlvl1 = MoreSkills_TamingConfig.GoblinLevelMaster.Value;
                                                Tamerlvl1 = MoreSkills_TamingConfig.GoblinLevelTamer.Value;
                                                Unlocklvl1 = MoreSkills_TamingConfig.GoblinLevelUnlock.Value;
                                                break;
                                            case "GoblinBrute":
                                                Masterlvl1 = MoreSkills_TamingConfig.GoblinBruteLevelMaster.Value;
                                                Tamerlvl1 = MoreSkills_TamingConfig.GoblinBruteLevelTamer.Value;
                                                Unlocklvl1 = MoreSkills_TamingConfig.GoblinBruteLevelUnlock.Value;
                                                break;
                                            case "GoblinShaman":
                                                Masterlvl1 = MoreSkills_TamingConfig.GoblinShamanLevelMaster.Value;
                                                Tamerlvl1 = MoreSkills_TamingConfig.GoblinShamanLevelTamer.Value;
                                                Unlocklvl1 = MoreSkills_TamingConfig.GoblinShamanLevelUnlock.Value;
                                                break;
                                            case "Greydwarf":
                                                Masterlvl1 = MoreSkills_TamingConfig.GreydwarfLevelMaster.Value;
                                                Tamerlvl1 = MoreSkills_TamingConfig.GreydwarfLevelTamer.Value;
                                                Unlocklvl1 = MoreSkills_TamingConfig.GreydwarfLevelUnlock.Value;
                                                break;
                                            case "Greydwarf_Elite":
                                                Masterlvl1 = MoreSkills_TamingConfig.GoblinBruteLevelMaster.Value;
                                                Tamerlvl1 = MoreSkills_TamingConfig.GoblinBruteLevelTamer.Value;
                                                Unlocklvl1 = MoreSkills_TamingConfig.GoblinBruteLevelUnlock.Value;
                                                break;
                                            case "Greydwarf_Shaman":
                                                Masterlvl1 = MoreSkills_TamingConfig.GoblinShamanLevelMaster.Value;
                                                Tamerlvl1 = MoreSkills_TamingConfig.GoblinShamanLevelTamer.Value;
                                                Unlocklvl1 = MoreSkills_TamingConfig.GoblinShamanLevelUnlock.Value;
                                                break;
                                            case "Greyling":
                                                Masterlvl1 = MoreSkills_TamingConfig.GreylingLevelMaster.Value;
                                                Tamerlvl1 = MoreSkills_TamingConfig.GreylingLevelTamer.Value;
                                                Unlocklvl1 = MoreSkills_TamingConfig.GreylingLevelUnlock.Value;
                                                break;
                                            case "Leech":
                                                Masterlvl1 = MoreSkills_TamingConfig.LeechLevelMaster.Value;
                                                Tamerlvl1 = MoreSkills_TamingConfig.LeechLevelTamer.Value;
                                                Unlocklvl1 = MoreSkills_TamingConfig.LeechLevelUnlock.Value;
                                                break;
                                            case "Lox":
                                                Masterlvl1 = MoreSkills_TamingConfig.LoxLevelMaster.Value;
                                                Tamerlvl1 = MoreSkills_TamingConfig.LoxLevelTamer.Value;
                                                Unlocklvl1 = MoreSkills_TamingConfig.LoxLevelUnlock.Value;
                                                break;
                                            case "Neck":
                                                Masterlvl1 = MoreSkills_TamingConfig.NeckLevelMaster.Value;
                                                Tamerlvl1 = MoreSkills_TamingConfig.NeckLevelTamer.Value;
                                                Unlocklvl1 = MoreSkills_TamingConfig.NeckLevelUnlock.Value;
                                                break;
                                            case "Serpent":
                                                Masterlvl1 = MoreSkills_TamingConfig.SerpentLevelMaster.Value;
                                                Tamerlvl1 = MoreSkills_TamingConfig.SerpentLevelTamer.Value;
                                                Unlocklvl1 = MoreSkills_TamingConfig.SerpentLevelUnlock.Value;
                                                break;
                                            case "Skeleton":
                                                Masterlvl1 = MoreSkills_TamingConfig.SkeletonLevelMaster.Value;
                                                Tamerlvl1 = MoreSkills_TamingConfig.SkeletonLevelTamer.Value;
                                                Unlocklvl1 = MoreSkills_TamingConfig.SkeletonLevelUnlock.Value;
                                                break;
                                            case "Skeleton_Poison":
                                                Masterlvl1 = MoreSkills_TamingConfig.SkeletonPoisonLevelMaster.Value;
                                                Tamerlvl1 = MoreSkills_TamingConfig.SkeletonPoisonLevelTamer.Value;
                                                Unlocklvl1 = MoreSkills_TamingConfig.SkeletonPoisonLevelUnlock.Value;
                                                break;
                                            case "StoneGolem":
                                                Masterlvl1 = MoreSkills_TamingConfig.StoneGolemLevelMaster.Value;
                                                Tamerlvl1 = MoreSkills_TamingConfig.StoneGolemLevelTamer.Value;
                                                Unlocklvl1 = MoreSkills_TamingConfig.StoneGolemLevelUnlock.Value;
                                                break;
                                            case "Surtling":
                                                Masterlvl1 = MoreSkills_TamingConfig.SurtlingLevelMaster.Value;
                                                Tamerlvl1 = MoreSkills_TamingConfig.SurtlingLevelTamer.Value;
                                                Unlocklvl1 = MoreSkills_TamingConfig.SurtlingLevelUnlock.Value;
                                                break;
                                            case "Troll":
                                                Masterlvl1 = MoreSkills_TamingConfig.TrollLevelMaster.Value;
                                                Tamerlvl1 = MoreSkills_TamingConfig.TrollLevelTamer.Value;
                                                Unlocklvl1 = MoreSkills_TamingConfig.TrollLevelUnlock.Value;
                                                break;
                                            case "Wolf":
                                                Masterlvl1 = MoreSkills_TamingConfig.WolfLevelMaster.Value;
                                                Tamerlvl1 = MoreSkills_TamingConfig.WolfLevelTamer.Value;
                                                Unlocklvl1 = MoreSkills_TamingConfig.WolfLevelUnlock.Value;
                                                break;
                                            case "Wraith":
                                                Masterlvl1 = MoreSkills_TamingConfig.WraithLevelMaster.Value;
                                                Tamerlvl1 = MoreSkills_TamingConfig.WraithLevelTamer.Value;
                                                Unlocklvl1 = MoreSkills_TamingConfig.WraithLevelUnlock.Value;
                                                break;
                                            case "Eikthyr":
                                                Masterlvl1 = MoreSkills_TamingConfig.EikthyrLevelMaster.Value;
                                                Tamerlvl1 = MoreSkills_TamingConfig.EikthyrLevelTamer.Value;
                                                Unlocklvl1 = MoreSkills_TamingConfig.EikthyrLevelUnlock.Value;
                                                break;
                                            case "gd_king":
                                                Masterlvl1 = MoreSkills_TamingConfig.ElderLevelMaster.Value;
                                                Tamerlvl1 = MoreSkills_TamingConfig.ElderLevelTamer.Value;
                                                Unlocklvl1 = MoreSkills_TamingConfig.ElderLevelUnlock.Value;
                                                break;
                                            case "Bonemass":
                                                Masterlvl1 = MoreSkills_TamingConfig.BonemassLevelMaster.Value;
                                                Tamerlvl1 = MoreSkills_TamingConfig.BonemassLevelTamer.Value;
                                                Unlocklvl1 = MoreSkills_TamingConfig.BonemassLevelUnlock.Value;
                                                break;
                                            case "Dragon":
                                                Masterlvl1 = MoreSkills_TamingConfig.DragonLevelMaster.Value;
                                                Tamerlvl1 = MoreSkills_TamingConfig.DragonLevelTamer.Value;
                                                Unlocklvl1 = MoreSkills_TamingConfig.DragonLevelUnlock.Value;
                                                break;
                                            case "GoblinKing":
                                                Masterlvl1 = MoreSkills_TamingConfig.GoblinKingLevelMaster.Value;
                                                Tamerlvl1 = MoreSkills_TamingConfig.GoblinKingLevelTamer.Value;
                                                Unlocklvl1 = MoreSkills_TamingConfig.GoblinKingLevelUnlock.Value;
                                                break;
                                            case "RRR_GDThornweaver":
                                                Masterlvl1 = MoreSkills_TamingConfig.RRRGDThornweaverLevelMaster.Value;
                                                Tamerlvl1 = MoreSkills_TamingConfig.RRRGDThornweaverLevelTamer.Value;
                                                Unlocklvl1 = MoreSkills_TamingConfig.RRRGDThornweaverLevelUnlock.Value;
                                                break;
                                            case "RRR_GhostVengeful":
                                                Masterlvl1 = MoreSkills_TamingConfig.RRRGhostVengefulLevelMaster.Value;
                                                Tamerlvl1 = MoreSkills_TamingConfig.RRRGhostVengefulLevelTamer.Value;
                                                Unlocklvl1 = MoreSkills_TamingConfig.RRRGhostVengefulLevelUnlock.Value;
                                                break;
                                            case "RRR_TrollTosser":
                                                Masterlvl1 = MoreSkills_TamingConfig.RRRTrollTosserLevelMaster.Value;
                                                Tamerlvl1 = MoreSkills_TamingConfig.RRRTrollTosserLevelTamer.Value;
                                                Unlocklvl1 = MoreSkills_TamingConfig.RRRTrollTosserLevelUnlock.Value;
                                                break;
                                            case "Ghost":
                                                Masterlvl1 = MoreSkills_TamingConfig.GhostLevelMaster.Value;
                                                Tamerlvl1 = MoreSkills_TamingConfig.GhostLevelTamer.Value;
                                                Unlocklvl1 = MoreSkills_TamingConfig.GhostLevelUnlock.Value;
                                                break;
                                            default:
                                                Masterlvl1 = 30f;
                                                Tamerlvl1 = 60f;
                                                Unlocklvl1 = 90f;
                                                if (tLevels.Find(tLevel => tLevel.Creature == sCreature).Creature != sCreature)
                                                {
                                                    Utilities.LogWarning("Creature Tame Levels Missing: " + __instance.name.Replace("(Clone)", ""));
                                                    Utilities.LogWarning("Adding Creature to Temp with Default Needed Levels. Unlock 30. Tamer 60. Master 90.");
                                                    Utilities.LogWarning("Report it if you want custom numbers to the creature, and the levels. Thanks!");
                                                    tLevels.Add(new Helper.TamingLevels(
                                                        creature: sCreature,
                                                        master: Masterlvl1,
                                                        tamer: Tamerlvl1,
                                                        unlock: Unlocklvl1,
                                                        tametime: __instance.m_tamingTime));
                                                }
                                                break;
                                        }
                                        if (tLevels.Find(tLevel => tLevel.Creature == sCreature).Creature != sCreature)
                                        {
                                            Utilities.Log("Saved Taming Levels from : " + __instance.name.Replace("(Clone)", "") + " to the Temporal Database");
                                            tLevels.Add(new Helper.TamingLevels(
                                                creature: sCreature,
                                                master: Masterlvl1,
                                                tamer: Tamerlvl1,
                                                unlock: Unlocklvl1,
                                                tametime: __instance.m_tamingTime));
                                        }
                                    }
                                    else
                                    {
                                        string sCreature = __instance.name.Replace("(Clone)", "");
                                        switch (__instance.name.Replace("(Clone)", ""))
                                        {
                                            case "Boar":
                                                Masterlvl1 = MoreSkills_TamingConfig.BoarLevelMaster.Value;
                                                Tamerlvl1 = MoreSkills_TamingConfig.BoarLevelTamer.Value;
                                                Unlocklvl1 = MoreSkills_TamingConfig.BoarLevelUnlock.Value;
                                                break;
                                            case "Wolf":
                                                Masterlvl1 = MoreSkills_TamingConfig.WolfLevelMaster.Value;
                                                Tamerlvl1 = MoreSkills_TamingConfig.WolfLevelTamer.Value;
                                                Unlocklvl1 = MoreSkills_TamingConfig.WolfLevelUnlock.Value;
                                                break;
                                            case "Lox":
                                                Masterlvl1 = MoreSkills_TamingConfig.LoxLevelMaster.Value;
                                                Tamerlvl1 = MoreSkills_TamingConfig.LeechLevelTamer.Value;
                                                Unlocklvl1 = MoreSkills_TamingConfig.LeechLevelUnlock.Value;
                                                break;
                                        }
                                        if (tLevels.Find(tLevel => tLevel.Creature == sCreature).Creature != sCreature)
                                        {
                                            Utilities.Log("Saved Taming Levels from : " + __instance.name.Replace("(Clone)", "") + " to the Temporal Database");
                                            tLevels.Add(new Helper.TamingLevels(
                                                creature: sCreature,
                                                master: Masterlvl1,
                                                tamer: Tamerlvl1,
                                                unlock: Unlocklvl1,
                                                tametime: __instance.m_tamingTime));
                                        }
                                    }
                                    //Utilities.LogWarning("Nivel Master: " + Masterlvl1);
                                    //Utilities.LogWarning("Nivel Tamer: " + Tamerlvl1);
                                    //Utilities.LogWarning("Nivel Unlock: " + Unlocklvl1);
                                    string CreatureZDOID = string.Concat(__instance.name + ":" + __instance.m_character.GetZDOID());
                                    ZDOID TamerID = closestPlayer.GetZDOID();
                                    float tameTime = __instance.m_tamingTime;
                                    float eatTime = __instance.m_fedDuration;
                                    float Master = Masterlvl1;
                                    float Tamer = Tamerlvl1;
                                    float Unlock = Unlocklvl1;
                                    tSaves.Add(new Helper.TamingSaves(
                                        creaturezdoid: CreatureZDOID,
                                        tamerid: TamerID,
                                        tametime: tameTime,
                                        eattime: eatTime,
                                        master: Master,
                                        tamer: Tamer,
                                        unlock: Unlock));
                                    //Utilities.Log("You are now taming a " + (__instance.name.Replace("(Clone)", "")));
                                    MoreSkills_Instances._player.Message(MessageHud.MessageType.Center, "You are now taming a " + (__instance.name.Replace("(Clone)", "")), 0, null);
                                    __instance.m_nview.GetZDO().Set("TamerID", MoreSkills_Instances._player.GetZDOID().ToString());
                                }
                                else
                                {
                                    Utilities.LogWarning("You are not the tamer, or was the closest one when he first ate or close enough");
                                    MoreSkills_Instances._player.Message(MessageHud.MessageType.TopLeft, "<color=red>You are not the tamer, either you weren't the closest one when he first ate or close enough " + (__instance.name.Replace("(Clone)", "")) + "</color>", 0, null);
                                }
                                /*string Test = __instance.m_nview.GetZDO().GetString("TamerID");
                                Utilities.LogWarning("ZDOs" + Test);*/
                            }
                        }
                    }
                }
            }
        }

        [HarmonyPatch(typeof(Tameable), "TamingUpdate")]
        public static class SetTamingTimes
        {
            public static void Postfix(ref Tameable __instance)
            {
                if (MoreSkills_Instances._player != null)
                {
                    if (MoreSkills_TamingConfig.EnableTamingSkill.Value)
                    {
                        string CreatureZDOID = string.Concat(__instance.name + ":" + __instance.m_character.GetZDOID());
                        float Distance = Vector3.Distance(__instance.transform.position, MoreSkills_Instances._player.transform.position);
                        float level = MoreSkills_Instances._player.GetSkillFactor((Skills.SkillType)MoreSkills_TamingConfig.TamingSkill_Type);

                        if (CreatureZDOID == tSaves.Find(tsave => tsave.CreatureZDOID == CreatureZDOID).CreatureZDOID && tSaves.Find(tsave => tsave.CreatureZDOID == CreatureZDOID).TamerID == MoreSkills_Instances._player.GetZDOID())
                        {
                            var getSave = tSaves.Find(tsave => tsave.CreatureZDOID == CreatureZDOID);
                            float Master = getSave.Master;
                            float Tamer = getSave.Tamer;
                            float Unlock = getSave.Unlock;
                            float oldTime = getSave.TameTime;
                            float oldEat = getSave.EatTime;
                            var Fixed = tFixed.Find(tfixed => tfixed.CreatureZDOID == CreatureZDOID);
                            var GotFar = tGotFar.Find(tgotfar => tgotfar.CreatureZDOID == CreatureZDOID);
                            var DoubleCheck = tDoubleCheck.Find(tdoublecheck => tdoublecheck.CreatureZDOID == CreatureZDOID);
                            //Utilities.LogWarning("CreatureZDOID: " + CreatureZDOID);
                            //Utilities.LogWarning("Creature: " + getSave.CreatureZDOID);
                            //Utilities.LogWarning("Master: " + Master);
                            //Utilities.LogWarning("Tamer: " + Tamer);
                            //Utilities.LogWarning("Unlock: " + Unlock);
                            //Utilities.LogWarning("Oldtime: " + oldTime);
                            //Utilities.LogWarning("OldEat: " + oldEat);
                            //Utilities.LogWarning("Fixed: " + Fixed.Fixed);
                            //Utilities.LogWarning("GotFar: " + GotFar.GotFar);
                            //Utilities.LogWarning("DoubleCheck: " + DoubleCheck.DoubleCheck);
                            if (__instance.GetTameness() < 100)
                            {
                                if (MoreSkills_TamingConfig.EnableTamingUnlocks.Value)
                                {
                                    if (MoreSkills_TamingConfig.EnableTamingTimeMod.Value)
                                    {
                                        if (tFixed.Find(x => x.CreatureZDOID == CreatureZDOID).CreatureZDOID != CreatureZDOID)
                                        {
                                            tFixed.Add(new Helper.TamingFix(
                                                creaturezdoid: CreatureZDOID,
                                                tfixed: false));
                                            //Utilities.LogWarning("Added default value of Fixed");
                                        }
                                        if (tGotFar.Find(x => x.CreatureZDOID == CreatureZDOID).CreatureZDOID != CreatureZDOID)
                                        {
                                            tGotFar.Add(new Helper.TamingGotFar(
                                                creaturezdoid: CreatureZDOID,
                                                gotfar: false));
                                            //Utilities.LogWarning("Added default value of GotFar");
                                        }
                                        if (tDoubleCheck.Find(x => x.CreatureZDOID == CreatureZDOID).CreatureZDOID != CreatureZDOID)
                                        {
                                            tDoubleCheck.Add(new Helper.TamingDoubleCheck(
                                                creaturezdoid: CreatureZDOID,
                                                doublecheck: 0));
                                            //Utilities.LogWarning("Added default value of Double");
                                        }

                                        if (level >= (Master / 100))
                                        {
                                            //Utilities.LogWarning("You are a Master at this my level " + level + " master level " + (Master/100));
                                            float tameTimeMath = ((oldTime / 2) + 120f) - (oldTime / 2);

                                            if (Distance < 75f)
                                            {
                                                __instance.m_tamingTime = tameTimeMath;

                                                //Utilities.LogWarning("Math: " + tameTimeMath);

                                                if (Fixed.Fixed == false)
                                                {
                                                    //Utilities.LogWarning("Fixed es false");
                                                    if (__instance.GetRemainingTime() != __instance.m_tamingTime)
                                                    {
                                                        __instance.m_nview.GetZDO().Set("TameTimeLeft", __instance.m_tamingTime);
                                                    }
                                                    //Utilities.LogWarning("Fixed b: " + Fixed.Fixed);
                                                    tFixed.Remove(Fixed);
                                                    tFixed.Add(new Helper.TamingFix(
                                                        creaturezdoid: CreatureZDOID,
                                                        tfixed: true));
                                                    //Utilities.LogWarning("Fixed a: " + Fixed.Fixed);

                                                }

                                                if (GotFar.GotFar == true)
                                                {
                                                    if (__instance.GetRemainingTime() > __instance.m_tamingTime)
                                                    {
                                                        float reset = __instance.m_tamingTime * ((__instance.m_nview.GetZDO().GetFloat("TameTimeLeft") * 100f) / (__instance.m_tamingTime * 2) / 100);
                                                        __instance.m_nview.GetZDO().Set("TameTimeLeft", reset);
                                                    }
                                                    else
                                                    {
                                                        //Utilities.LogWarning("GotFar b: " + GotFar.GotFar);
                                                        tGotFar.Remove(GotFar);
                                                        tGotFar.Add(new Helper.TamingGotFar(
                                                            creaturezdoid: CreatureZDOID,
                                                            gotfar: false));
                                                        //Utilities.LogWarning("GotFar a: " + GotFar.GotFar);
                                                    }
                                                    MoreSkills_Instances._player.Message(MessageHud.MessageType.TopLeft, "<color=Green>You are close to " + (__instance.name.Replace("(Clone)", "")) + " the tame time back to normal</color>", 0, null);
                                                }
                                            }
                                            else
                                            {
                                                __instance.m_tamingTime = (tameTimeMath * 2);

                                                if (GotFar.GotFar == false)
                                                {
                                                    float grow = __instance.m_tamingTime * ((__instance.m_nview.GetZDO().GetFloat("TameTimeLeft") * 100) / (__instance.m_tamingTime / 2) / 100);
                                                    __instance.m_nview.GetZDO().Set("TameTimeLeft", grow);
                                                    MoreSkills_Instances._player.Message(MessageHud.MessageType.TopLeft, "<color=red>You are getting far from " + (__instance.name.Replace("(Clone)", "")) + " this will double the tame time...</color>", 0, null);
                                                }
                                                tGotFar.Remove(GotFar);
                                                tGotFar.Add(new Helper.TamingGotFar(
                                                    creaturezdoid: CreatureZDOID,
                                                    gotfar: true));
                                            }
                                        }
                                        else if (level >= (Tamer / 100))
                                        {
                                            //Utilities.LogWarning("You are a good at this");
                                            float tameTimeMath = ((oldTime / 2) + 120f) - (((oldTime / 2) * ((level - (Tamer / 100)) * (1 / ((Master - Tamer) / 100)))));

                                            if (Distance < 75f)
                                            {
                                                __instance.m_tamingTime = tameTimeMath;

                                                if (Fixed.Fixed == false)
                                                {
                                                    if (__instance.GetRemainingTime() != __instance.m_tamingTime)
                                                    {
                                                        __instance.m_nview.GetZDO().Set("TameTimeLeft", __instance.m_tamingTime);
                                                    }
                                                    //Utilities.LogWarning("Fixed b: " + Fixed.Fixed);
                                                    tFixed.Remove(Fixed);
                                                    tFixed.Add(new Helper.TamingFix(
                                                        creaturezdoid: CreatureZDOID,
                                                        tfixed: true));
                                                    //Utilities.LogWarning("Fixed a: " + Fixed.Fixed);
                                                }

                                                if (GotFar.GotFar == true)
                                                {
                                                    if (__instance.GetRemainingTime() > __instance.m_tamingTime)
                                                    {
                                                        float reset = __instance.m_tamingTime * ((__instance.m_nview.GetZDO().GetFloat("TameTimeLeft") * 100f) / (__instance.m_tamingTime * 2) / 100);
                                                        __instance.m_nview.GetZDO().Set("TameTimeLeft", reset);
                                                    }
                                                    else
                                                    {
                                                        //Utilities.LogWarning("GotFar b: " + GotFar.GotFar);
                                                        tGotFar.Remove(GotFar);
                                                        tGotFar.Add(new Helper.TamingGotFar(
                                                            creaturezdoid: CreatureZDOID,
                                                            gotfar: false));
                                                        //Utilities.LogWarning("GotFar a: " + GotFar.GotFar);
                                                    }
                                                    MoreSkills_Instances._player.Message(MessageHud.MessageType.TopLeft, "<color=Green>You are close to " + (__instance.name.Replace("(Clone)", "")) + " the tame time back to normal</color>", 0, null);
                                                }
                                            }
                                            else
                                            {
                                                __instance.m_tamingTime = (tameTimeMath * 2);

                                                if (GotFar.GotFar == false)
                                                {
                                                    float grow = __instance.m_tamingTime * ((__instance.m_nview.GetZDO().GetFloat("TameTimeLeft") * 100) / (__instance.m_tamingTime / 2) / 100);
                                                    __instance.m_nview.GetZDO().Set("TameTimeLeft", grow);
                                                    MoreSkills_Instances._player.Message(MessageHud.MessageType.TopLeft, "<color=red>You are getting far from " + (__instance.name.Replace("(Clone)", "")) + " this will double the tame time...</color>", 0, null);
                                                }
                                                tGotFar.Remove(GotFar);
                                                tGotFar.Add(new Helper.TamingGotFar(
                                                    creaturezdoid: CreatureZDOID,
                                                    gotfar: true));
                                            }
                                        }
                                        else if (level >= (Unlock / 100))
                                        {
                                            //Utilities.LogWarning("You are a ok at this");
                                            float tameTimeMath = oldTime - ((oldTime / 2) * ((level - (Unlock / 100)) * (1 / ((Tamer - Unlock) / 100))));

                                            if (Distance < 75f)
                                            {
                                                __instance.m_tamingTime = tameTimeMath;

                                                if (Fixed.Fixed == false)
                                                {
                                                    if (__instance.GetRemainingTime() != __instance.m_tamingTime)
                                                    {
                                                        __instance.m_nview.GetZDO().Set("TameTimeLeft", __instance.m_tamingTime);
                                                    }
                                                    else
                                                    {
                                                        //Utilities.LogWarning("Fixed b: " + Fixed.Fixed);
                                                        tFixed.Remove(Fixed);
                                                        tFixed.Add(new Helper.TamingFix(
                                                            creaturezdoid: CreatureZDOID,
                                                            tfixed: true));
                                                        //Utilities.LogWarning("Fixed a: " + Fixed.Fixed);
                                                    }
                                                }

                                                if (GotFar.GotFar == true)
                                                {
                                                    if (__instance.GetRemainingTime() > __instance.m_tamingTime)
                                                    {
                                                        float reset = __instance.m_tamingTime * ((__instance.m_nview.GetZDO().GetFloat("TameTimeLeft") * 100f) / (__instance.m_tamingTime * 2) / 100);
                                                        __instance.m_nview.GetZDO().Set("TameTimeLeft", reset);
                                                    }
                                                    else
                                                    {
                                                        //Utilities.LogWarning("GotFar b: " + GotFar.GotFar);
                                                        tGotFar.Remove(GotFar);
                                                        tGotFar.Add(new Helper.TamingGotFar(
                                                            creaturezdoid: CreatureZDOID,
                                                            gotfar: false));
                                                        //Utilities.LogWarning("GotFar a: " + GotFar.GotFar);
                                                    }
                                                    MoreSkills_Instances._player.Message(MessageHud.MessageType.TopLeft, "<color=Green>You are close to " + (__instance.name.Replace("(Clone)", "")) + " the tame time back to normal</color>", 0, null);
                                                }
                                            }
                                            else
                                            {
                                                __instance.m_tamingTime = (tameTimeMath * 2);

                                                if (GotFar.GotFar == false)
                                                {
                                                    float grow = __instance.m_tamingTime * ((__instance.m_nview.GetZDO().GetFloat("TameTimeLeft") * 100) / (__instance.m_tamingTime / 2) / 100);
                                                    __instance.m_nview.GetZDO().Set("TameTimeLeft", grow);
                                                    MoreSkills_Instances._player.Message(MessageHud.MessageType.TopLeft, "<color=red>You are getting far from " + (__instance.name.Replace("(Clone)", "")) + " this will double the tame time...</color>", 0, null);
                                                }
                                                tGotFar.Remove(GotFar);
                                                tGotFar.Add(new Helper.TamingGotFar(
                                                    creaturezdoid: CreatureZDOID,
                                                    gotfar: true));
                                            }
                                        }
                                        else
                                        {
                                            //Utilities.LogWarning("You Dont know how to tame this");
                                            float tameTimeMath = oldTime * (Unlock - (level * 100));

                                            if (Distance < 75f)
                                            {
                                                __instance.m_tamingTime = tameTimeMath;

                                                if (Fixed.Fixed == false)
                                                {
                                                    if (__instance.GetRemainingTime() != __instance.m_tamingTime)
                                                    {
                                                        __instance.m_nview.GetZDO().Set("TameTimeLeft", __instance.m_tamingTime);
                                                    }
                                                    //Utilities.LogWarning("Fixed b: " + Fixed.Fixed);
                                                    tFixed.Remove(Fixed);
                                                    tFixed.Add(new Helper.TamingFix(
                                                        creaturezdoid: CreatureZDOID,
                                                        tfixed: true));
                                                    //Utilities.LogWarning("Fixed a: " + Fixed.Fixed);
                                                }

                                                if (GotFar.GotFar == true)
                                                {
                                                    if (__instance.GetRemainingTime() > __instance.m_tamingTime)
                                                    {
                                                        float reset = __instance.m_tamingTime * ((__instance.m_nview.GetZDO().GetFloat("TameTimeLeft") * 100f) / (__instance.m_tamingTime * 2) / 100);
                                                        __instance.m_nview.GetZDO().Set("TameTimeLeft", reset);
                                                    }
                                                    else
                                                    {
                                                        //Utilities.LogWarning("GotFar b: " + GotFar.GotFar);
                                                        tGotFar.Remove(GotFar);
                                                        tGotFar.Add(new Helper.TamingGotFar(
                                                            creaturezdoid: CreatureZDOID,
                                                            gotfar: false));
                                                        //Utilities.LogWarning("GotFar a: " + GotFar.GotFar);
                                                    }
                                                    MoreSkills_Instances._player.Message(MessageHud.MessageType.TopLeft, "<color=Green>You are close to " + (__instance.name.Replace("(Clone)", "")) + " the tame time back to normal</color>", 0, null);
                                                }
                                            }
                                            else
                                            {
                                                __instance.m_tamingTime = (tameTimeMath * 2);

                                                if (GotFar.GotFar == false)
                                                {
                                                    float grow = __instance.m_tamingTime * ((__instance.m_nview.GetZDO().GetFloat("TameTimeLeft") * 100) / (__instance.m_tamingTime / 2) / 100);
                                                    __instance.m_nview.GetZDO().Set("TameTimeLeft", grow);
                                                    MoreSkills_Instances._player.Message(MessageHud.MessageType.TopLeft, "<color=red>You are getting far from " + (__instance.name.Replace("(Clone)", "")) + " this will double the tame time...</color>", 0, null);
                                                }
                                                tGotFar.Remove(GotFar);
                                                tGotFar.Add(new Helper.TamingGotFar(
                                                    creaturezdoid: CreatureZDOID,
                                                    gotfar: true));
                                            }
                                        }
                                    }
                                    if (MoreSkills_TamingConfig.EnableTamingEatMod.Value)
                                    {
                                        if (level >= (Master / 100))
                                        {
                                            float tameEatMath = oldEat * 2;
                                            __instance.m_fedDuration = tameEatMath;
                                        }
                                        else if (level >= (Tamer / 100))
                                        {
                                            float tameEatMath = oldEat + (oldEat * ((level - (Tamer / 100)) * (1 / ((Master - Tamer) / 100))));
                                            __instance.m_fedDuration = tameEatMath;
                                        }
                                        else if (level >= (Unlock / 100))
                                        {
                                            float tameEatMath = (oldEat / 2) + ((oldEat / 2) * ((level - (Unlock / 100)) * (1 / ((Tamer - Unlock) / 100))));
                                            __instance.m_fedDuration = tameEatMath;
                                        }
                                        else
                                        {
                                            float tameEatMath = (oldEat / 2) / (Unlock - (level * 100));
                                            __instance.m_fedDuration = tameEatMath;
                                        }
                                    }
                                }
                                else
                                {

                                    if (MoreSkills_TamingConfig.EnableTamingTimeMod.Value)
                                    {
                                        float tameTimeMath = (oldTime + 120f) - (oldTime * level);

                                        if (Distance < 75f)
                                        {
                                            __instance.m_tamingTime = tameTimeMath;

                                            if (Fixed.Fixed == false)
                                            {
                                                if (__instance.GetRemainingTime() != __instance.m_tamingTime)
                                                {
                                                    __instance.m_nview.GetZDO().Set("TameTimeLeft", __instance.m_tamingTime);
                                                }
                                                //Utilities.LogWarning("Fixed b: " + Fixed.Fixed);
                                                tFixed.Remove(Fixed);
                                                tFixed.Add(new Helper.TamingFix(
                                                    creaturezdoid: CreatureZDOID,
                                                    tfixed: true));
                                                //Utilities.LogWarning("Fixed a: " + Fixed.Fixed);
                                            }

                                            if (GotFar.GotFar == true)
                                            {
                                                if (__instance.GetRemainingTime() != __instance.m_tamingTime)
                                                {
                                                    float reset = __instance.m_tamingTime * ((__instance.m_nview.GetZDO().GetFloat("TameTimeLeft") * 100f) / (__instance.m_tamingTime * 2) / 100);
                                                    __instance.m_nview.GetZDO().Set("TameTimeLeft", reset);
                                                }
                                                else
                                                {
                                                    //Utilities.LogWarning("GotFar b: " + GotFar.GotFar);
                                                    tGotFar.Remove(GotFar);
                                                    tGotFar.Add(new Helper.TamingGotFar(
                                                        creaturezdoid: CreatureZDOID,
                                                        gotfar: false));
                                                    //Utilities.LogWarning("GotFar a: " + GotFar.GotFar);
                                                }
                                                MoreSkills_Instances._player.Message(MessageHud.MessageType.TopLeft, "<color=Green>You are close to " + (__instance.name.Replace("(Clone)", "")) + " the tame time back to normal</color>", 0, null);
                                            }
                                        }
                                        else
                                        {
                                            __instance.m_tamingTime = (tameTimeMath * 2);

                                            if (GotFar.GotFar == false)
                                            {
                                                float grow = __instance.m_tamingTime * ((__instance.m_nview.GetZDO().GetFloat("TameTimeLeft") * 100) / (__instance.m_tamingTime / 2) / 100);
                                                __instance.m_nview.GetZDO().Set("TameTimeLeft", grow);
                                                MoreSkills_Instances._player.Message(MessageHud.MessageType.TopLeft, "<color=red>You are getting far from " + (__instance.name.Replace("(Clone)", "")) + " this will double the tame time...</color>", 0, null);
                                            }
                                            tGotFar.Remove(GotFar);
                                            tGotFar.Add(new Helper.TamingGotFar(
                                                creaturezdoid: CreatureZDOID,
                                                gotfar: true));
                                        }

                                    }
                                    if (MoreSkills_TamingConfig.EnableTamingEatMod.Value)
                                    {
                                        float tameEatMath = (oldEat / 5) + (oldEat * level);
                                        __instance.m_fedDuration = tameEatMath;

                                    }
                                }

                                //Utilities.LogWarning("TameTime: " + __instance.m_nview.GetZDO().GetFloat("TameTimeLeft"));
                            }
                            else if (__instance.m_character.GetHealth() <= 0)
                            {
                                var tsave = tSaves.Find(x => x.CreatureZDOID == CreatureZDOID);
                                tSaves.Remove(tsave);
                                var tfix = tFixed.Find(x => x.CreatureZDOID == CreatureZDOID);
                                tFixed.Remove(tfix);
                                var tgotfar = tGotFar.Find(x => x.CreatureZDOID == CreatureZDOID);
                                tGotFar.Remove(tgotfar);
                                var tdoublecheck = tDoubleCheck.Find(x => x.CreatureZDOID == CreatureZDOID);
                                tDoubleCheck.Remove(tdoublecheck);
                            }
                            else if (__instance.m_character.IsTamed())
                            {
                                TamingSkillIncrease += ((__instance.m_tamingTime / 200) * __instance.m_character.GetLevel()) * MoreSkills_TamingConfig.TamingSkillIncreaseMultiplier.Value;
                                Utilities.Log("Gained " + TamingSkillIncrease + " EXP at Taming");
                                var tsave = tSaves.Find(x => x.CreatureZDOID == CreatureZDOID);
                                tSaves.Remove(tsave);
                                var tfix = tFixed.Find(x => x.CreatureZDOID == CreatureZDOID);
                                tFixed.Remove(tfix);
                                var tgotfar = tGotFar.Find(x => x.CreatureZDOID == CreatureZDOID);
                                tGotFar.Remove(tgotfar);
                                var tdoublecheck = tDoubleCheck.Find(x => x.CreatureZDOID == CreatureZDOID);
                                tDoubleCheck.Remove(tdoublecheck);
                                __instance.m_nview.GetZDO().Set("TamerID", "");
                            }
                        }

                        if (tDoubleCheck.Find(tdoublecheck => tdoublecheck.CreatureZDOID == CreatureZDOID).DoubleCheck < 5 && CreatureZDOID == tSaves.Find(tsave => tsave.CreatureZDOID == CreatureZDOID).CreatureZDOID)
                        {
                            int doublecheck = tDoubleCheck.Find(x => x.CreatureZDOID == CreatureZDOID).DoubleCheck + 1;
                            var tdoublecheck = tDoubleCheck.Find(x => x.CreatureZDOID == CreatureZDOID);
                            tDoubleCheck.Remove(tdoublecheck);
                            tDoubleCheck.Add(new Helper.TamingDoubleCheck(
                                creaturezdoid: CreatureZDOID,
                                doublecheck: doublecheck));
                            var tfix = tFixed.Find(x => x.CreatureZDOID == CreatureZDOID);
                            tFixed.Remove(tfix);
                            tFixed.Add(new Helper.TamingFix(
                                creaturezdoid: CreatureZDOID,
                                tfixed: false));
                        }

                        if (TamingSkillIncrease > 0.5f)
                        {
                            MoreSkills_Instances._player.RaiseSkill((Skills.SkillType)MoreSkills_TamingConfig.TamingSkill_Type, TamingSkillIncrease);
                            TamingSkillIncrease = 0f;
                        }

                        /*if (__instance.GetTameness() < 100
                            && __instance.m_character.GetZDOID() == Creature1
                            || __instance.m_character.GetZDOID() == Creature2
                            || __instance.m_character.GetZDOID() == Creature3
                            || __instance.m_character.GetZDOID() == Creature4
                            || __instance.m_character.GetZDOID() == Creature5)
                        {
                            Utilities.LogWarning("TiempoZDO: " + __instance.m_nview.GetZDO().GetFloat("TameTimeLeft"));
                            Utilities.LogWarning("ID: " + __instance.m_character.GetZDOID());


                            Utilities.LogWarning("Animal: " + __instance.m_character.m_name);
                            Utilities.LogWarning("TA: " + __instance.m_tamingTime);
                            Utilities.LogWarning("Test2: " + __instance.m_fedDuration);

                            Utilities.LogWarning("Tameness: " + __instance.GetTameness());
                            Utilities.LogWarning("Distance from Creature: " + Distance);
                            Utilities.LogWarning("Tiempo Funciton: " + __instance.GetRemainingTime());
                        }*/
                    }
                }

            }
        }

        [HarmonyPatch(typeof(Tameable), "GetHoverText")]
        public static class ShowTamingHoverText
        {
            public static void Postfix(ref string __result, ref Tameable __instance)
            {
                if (MoreSkills_Instances._player != null)
                {
                    if (MoreSkills_TamingConfig.EnableTamingSkill.Value)
                    {
                        if (MoreSkills_TamingConfig.EnableTamingUnlocks.Value)
                        {
                            if (__instance.GetTameness() <= 0 && !__instance.m_character.IsTamed() && !__instance.m_character.IsPlayer())
                            {
                                string sCreature = __instance.name.Replace("(Clone)", "");
                                if (MoreSkills_TamingConfig.EnableAllTamableCompatibility.Value)
                                {
                                    switch (__instance.name.Replace("(Clone)", ""))
                                    {
                                        case "Blob":
                                            Masterlvl1 = MoreSkills_TamingConfig.BlobLevelMaster.Value;
                                            Tamerlvl1 = MoreSkills_TamingConfig.BlobLevelTamer.Value;
                                            Unlocklvl1 = MoreSkills_TamingConfig.BlobLevelTamer.Value;
                                            break;
                                        case "BlobElite":
                                            Masterlvl1 = MoreSkills_TamingConfig.BlobEliteLevelMaster.Value;
                                            Tamerlvl1 = MoreSkills_TamingConfig.BlobEliteLevelTamer.Value;
                                            Unlocklvl1 = MoreSkills_TamingConfig.BlobEliteLevelUnlock.Value;
                                            break;
                                        case "Boar":
                                            Masterlvl1 = MoreSkills_TamingConfig.BoarLevelMaster.Value;
                                            Tamerlvl1 = MoreSkills_TamingConfig.BoarLevelTamer.Value;
                                            Unlocklvl1 = MoreSkills_TamingConfig.BoarLevelUnlock.Value;
                                            break;
                                        case "Deathsquito":
                                            Masterlvl1 = MoreSkills_TamingConfig.DeathsquitoLevelMaster.Value;
                                            Tamerlvl1 = MoreSkills_TamingConfig.DeathsquitoLevelTamer.Value;
                                            Unlocklvl1 = MoreSkills_TamingConfig.DeathsquitoLevelUnlock.Value;
                                            break;
                                        case "Hatchling":
                                            Masterlvl1 = MoreSkills_TamingConfig.DrakeLevelMaster.Value;
                                            Tamerlvl1 = MoreSkills_TamingConfig.DrakeLevelTamer.Value;
                                            Unlocklvl1 = MoreSkills_TamingConfig.DrakeLevelUnlock.Value;
                                            break;
                                        case "Draugr":
                                            Masterlvl1 = MoreSkills_TamingConfig.DraugrLevelMaster.Value;
                                            Tamerlvl1 = MoreSkills_TamingConfig.DraugrLevelTamer.Value;
                                            Unlocklvl1 = MoreSkills_TamingConfig.DraugrLevelUnlock.Value;
                                            break;
                                        case "Draugr_Elite":
                                            Masterlvl1 = MoreSkills_TamingConfig.DraugrEliteLevelMaster.Value;
                                            Tamerlvl1 = MoreSkills_TamingConfig.DraugrEliteLevelTamer.Value;
                                            Unlocklvl1 = MoreSkills_TamingConfig.DraugrEliteLevelUnlock.Value;
                                            break;
                                        case "Fenring":
                                            Masterlvl1 = MoreSkills_TamingConfig.FenringLevelMaster.Value;
                                            Tamerlvl1 = MoreSkills_TamingConfig.FenringLevelTamer.Value;
                                            Unlocklvl1 = MoreSkills_TamingConfig.FenringLevelUnlock.Value;
                                            break;
                                        case "Goblin":
                                            Masterlvl1 = MoreSkills_TamingConfig.GoblinLevelMaster.Value;
                                            Tamerlvl1 = MoreSkills_TamingConfig.GoblinLevelTamer.Value;
                                            Unlocklvl1 = MoreSkills_TamingConfig.GoblinLevelUnlock.Value;
                                            break;
                                        case "GoblinBrute":
                                            Masterlvl1 = MoreSkills_TamingConfig.GoblinBruteLevelMaster.Value;
                                            Tamerlvl1 = MoreSkills_TamingConfig.GoblinBruteLevelTamer.Value;
                                            Unlocklvl1 = MoreSkills_TamingConfig.GoblinBruteLevelUnlock.Value;
                                            break;
                                        case "GoblinShaman":
                                            Masterlvl1 = MoreSkills_TamingConfig.GoblinShamanLevelMaster.Value;
                                            Tamerlvl1 = MoreSkills_TamingConfig.GoblinShamanLevelTamer.Value;
                                            Unlocklvl1 = MoreSkills_TamingConfig.GoblinShamanLevelUnlock.Value;
                                            break;
                                        case "Greydwarf":
                                            Masterlvl1 = MoreSkills_TamingConfig.GreydwarfLevelMaster.Value;
                                            Tamerlvl1 = MoreSkills_TamingConfig.GreydwarfLevelTamer.Value;
                                            Unlocklvl1 = MoreSkills_TamingConfig.GreydwarfLevelUnlock.Value;
                                            break;
                                        case "Greydwarf_Elite":
                                            Masterlvl1 = MoreSkills_TamingConfig.GoblinBruteLevelMaster.Value;
                                            Tamerlvl1 = MoreSkills_TamingConfig.GoblinBruteLevelTamer.Value;
                                            Unlocklvl1 = MoreSkills_TamingConfig.GoblinBruteLevelUnlock.Value;
                                            break;
                                        case "Greydwarf_Shaman":
                                            Masterlvl1 = MoreSkills_TamingConfig.GoblinShamanLevelMaster.Value;
                                            Tamerlvl1 = MoreSkills_TamingConfig.GoblinShamanLevelTamer.Value;
                                            Unlocklvl1 = MoreSkills_TamingConfig.GoblinShamanLevelUnlock.Value;
                                            break;
                                        case "Greyling":
                                            Masterlvl1 = MoreSkills_TamingConfig.GreylingLevelMaster.Value;
                                            Tamerlvl1 = MoreSkills_TamingConfig.GreylingLevelTamer.Value;
                                            Unlocklvl1 = MoreSkills_TamingConfig.GreylingLevelUnlock.Value;
                                            break;
                                        case "Leech":
                                            Masterlvl1 = MoreSkills_TamingConfig.LeechLevelMaster.Value;
                                            Tamerlvl1 = MoreSkills_TamingConfig.LeechLevelTamer.Value;
                                            Unlocklvl1 = MoreSkills_TamingConfig.LeechLevelUnlock.Value;
                                            break;
                                        case "Lox":
                                            Masterlvl1 = MoreSkills_TamingConfig.LoxLevelMaster.Value;
                                            Tamerlvl1 = MoreSkills_TamingConfig.LoxLevelTamer.Value;
                                            Unlocklvl1 = MoreSkills_TamingConfig.LoxLevelUnlock.Value;
                                            break;
                                        case "Neck":
                                            Masterlvl1 = MoreSkills_TamingConfig.NeckLevelMaster.Value;
                                            Tamerlvl1 = MoreSkills_TamingConfig.NeckLevelTamer.Value;
                                            Unlocklvl1 = MoreSkills_TamingConfig.NeckLevelUnlock.Value;
                                            break;
                                        case "Serpent":
                                            Masterlvl1 = MoreSkills_TamingConfig.SerpentLevelMaster.Value;
                                            Tamerlvl1 = MoreSkills_TamingConfig.SerpentLevelTamer.Value;
                                            Unlocklvl1 = MoreSkills_TamingConfig.SerpentLevelUnlock.Value;
                                            break;
                                        case "Skeleton":
                                            Masterlvl1 = MoreSkills_TamingConfig.SkeletonLevelMaster.Value;
                                            Tamerlvl1 = MoreSkills_TamingConfig.SkeletonLevelTamer.Value;
                                            Unlocklvl1 = MoreSkills_TamingConfig.SkeletonLevelUnlock.Value;
                                            break;
                                        case "Skeleton_Poison":
                                            Masterlvl1 = MoreSkills_TamingConfig.SkeletonPoisonLevelMaster.Value;
                                            Tamerlvl1 = MoreSkills_TamingConfig.SkeletonPoisonLevelTamer.Value;
                                            Unlocklvl1 = MoreSkills_TamingConfig.SkeletonPoisonLevelUnlock.Value;
                                            break;
                                        case "StoneGolem":
                                            Masterlvl1 = MoreSkills_TamingConfig.StoneGolemLevelMaster.Value;
                                            Tamerlvl1 = MoreSkills_TamingConfig.StoneGolemLevelTamer.Value;
                                            Unlocklvl1 = MoreSkills_TamingConfig.StoneGolemLevelUnlock.Value;
                                            break;
                                        case "Surtling":
                                            Masterlvl1 = MoreSkills_TamingConfig.SurtlingLevelMaster.Value;
                                            Tamerlvl1 = MoreSkills_TamingConfig.SurtlingLevelTamer.Value;
                                            Unlocklvl1 = MoreSkills_TamingConfig.SurtlingLevelUnlock.Value;
                                            break;
                                        case "Troll":
                                            Masterlvl1 = MoreSkills_TamingConfig.TrollLevelMaster.Value;
                                            Tamerlvl1 = MoreSkills_TamingConfig.TrollLevelTamer.Value;
                                            Unlocklvl1 = MoreSkills_TamingConfig.TrollLevelUnlock.Value;
                                            break;
                                        case "Wolf":
                                            Masterlvl1 = MoreSkills_TamingConfig.WolfLevelMaster.Value;
                                            Tamerlvl1 = MoreSkills_TamingConfig.WolfLevelTamer.Value;
                                            Unlocklvl1 = MoreSkills_TamingConfig.WolfLevelUnlock.Value;
                                            break;
                                        case "Wraith":
                                            Masterlvl1 = MoreSkills_TamingConfig.WraithLevelMaster.Value;
                                            Tamerlvl1 = MoreSkills_TamingConfig.WraithLevelTamer.Value;
                                            Unlocklvl1 = MoreSkills_TamingConfig.WraithLevelUnlock.Value;
                                            break;
                                        case "Eikthyr":
                                            Masterlvl1 = MoreSkills_TamingConfig.EikthyrLevelMaster.Value;
                                            Tamerlvl1 = MoreSkills_TamingConfig.EikthyrLevelTamer.Value;
                                            Unlocklvl1 = MoreSkills_TamingConfig.EikthyrLevelUnlock.Value;
                                            break;
                                        case "gd_king":
                                            Masterlvl1 = MoreSkills_TamingConfig.ElderLevelMaster.Value;
                                            Tamerlvl1 = MoreSkills_TamingConfig.ElderLevelTamer.Value;
                                            Unlocklvl1 = MoreSkills_TamingConfig.ElderLevelUnlock.Value;
                                            break;
                                        case "Bonemass":
                                            Masterlvl1 = MoreSkills_TamingConfig.BonemassLevelMaster.Value;
                                            Tamerlvl1 = MoreSkills_TamingConfig.BonemassLevelTamer.Value;
                                            Unlocklvl1 = MoreSkills_TamingConfig.BonemassLevelUnlock.Value;
                                            break;
                                        case "Dragon":
                                            Masterlvl1 = MoreSkills_TamingConfig.DragonLevelMaster.Value;
                                            Tamerlvl1 = MoreSkills_TamingConfig.DragonLevelTamer.Value;
                                            Unlocklvl1 = MoreSkills_TamingConfig.DragonLevelUnlock.Value;
                                            break;
                                        case "GoblinKing":
                                            Masterlvl1 = MoreSkills_TamingConfig.GoblinKingLevelMaster.Value;
                                            Tamerlvl1 = MoreSkills_TamingConfig.GoblinKingLevelTamer.Value;
                                            Unlocklvl1 = MoreSkills_TamingConfig.GoblinKingLevelUnlock.Value;
                                            break;
                                        case "RRR_GDThornweaver":
                                            Masterlvl1 = MoreSkills_TamingConfig.RRRGDThornweaverLevelMaster.Value;
                                            Tamerlvl1 = MoreSkills_TamingConfig.RRRGDThornweaverLevelTamer.Value;
                                            Unlocklvl1 = MoreSkills_TamingConfig.RRRGDThornweaverLevelUnlock.Value;
                                            break;
                                        case "RRR_GhostVengeful":
                                            Masterlvl1 = MoreSkills_TamingConfig.RRRGhostVengefulLevelMaster.Value;
                                            Tamerlvl1 = MoreSkills_TamingConfig.RRRGhostVengefulLevelTamer.Value;
                                            Unlocklvl1 = MoreSkills_TamingConfig.RRRGhostVengefulLevelUnlock.Value;
                                            break;
                                        case "RRR_TrollTosser":
                                            Masterlvl1 = MoreSkills_TamingConfig.RRRTrollTosserLevelMaster.Value;
                                            Tamerlvl1 = MoreSkills_TamingConfig.RRRTrollTosserLevelTamer.Value;
                                            Unlocklvl1 = MoreSkills_TamingConfig.RRRTrollTosserLevelUnlock.Value;
                                            break;
                                        case "Ghost":
                                            Masterlvl1 = MoreSkills_TamingConfig.GhostLevelMaster.Value;
                                            Tamerlvl1 = MoreSkills_TamingConfig.GhostLevelTamer.Value;
                                            Unlocklvl1 = MoreSkills_TamingConfig.GhostLevelUnlock.Value;
                                            break;
                                        default:
                                            Masterlvl1 = 30f;
                                            Tamerlvl1 = 60f;
                                            Unlocklvl1 = 90f;
                                            if (tLevels.Find(tLevel => tLevel.Creature == sCreature).Creature != sCreature)
                                            {
                                                Utilities.LogWarning("Creature Tame Levels Missing: " + __instance.name.Replace("(Clone)", ""));
                                                Utilities.LogWarning("Adding Creature to Temp with Default Needed Levels. Unlock 30. Tamer 60. Master 90.");
                                                Utilities.LogWarning("Report it if you want custom numbers to the creature, and the levels. Thanks!");
                                                tLevels.Add(new Helper.TamingLevels(
                                                    creature: sCreature,
                                                    master: Masterlvl1,
                                                    tamer: Tamerlvl1,
                                                    unlock: Unlocklvl1,
                                                    tametime: __instance.m_tamingTime));
                                            }
                                            break;
                                    }
                                    if (tLevels.Find(tLevel => tLevel.Creature == sCreature).Creature != sCreature)
                                    {
                                        Utilities.Log("Saved Taming Levels from : " + __instance.name.Replace("(Clone)", "") + " to the Temporal Database");
                                        tLevels.Add(new Helper.TamingLevels(
                                            creature: sCreature,
                                            master: Masterlvl1,
                                            tamer: Tamerlvl1,
                                            unlock: Unlocklvl1,
                                            tametime: __instance.m_tamingTime));
                                    }
                                }
                                else
                                {
                                    switch (__instance.name.Replace("(Clone)", ""))
                                    {
                                        case "Boar":
                                            Masterlvl1 = MoreSkills_TamingConfig.BoarLevelMaster.Value;
                                            Tamerlvl1 = MoreSkills_TamingConfig.BoarLevelTamer.Value;
                                            Unlocklvl1 = MoreSkills_TamingConfig.BoarLevelUnlock.Value;
                                            break;
                                        case "Wolf":
                                            Masterlvl1 = MoreSkills_TamingConfig.WolfLevelMaster.Value;
                                            Tamerlvl1 = MoreSkills_TamingConfig.WolfLevelTamer.Value;
                                            Unlocklvl1 = MoreSkills_TamingConfig.WolfLevelUnlock.Value;
                                            break;
                                        case "Lox":
                                            Masterlvl1 = MoreSkills_TamingConfig.LoxLevelMaster.Value;
                                            Tamerlvl1 = MoreSkills_TamingConfig.LeechLevelTamer.Value;
                                            Unlocklvl1 = MoreSkills_TamingConfig.LeechLevelUnlock.Value;
                                            break;
                                    }
                                    if (tLevels.Find(tLevel => tLevel.Creature == sCreature).Creature != sCreature)
                                    {
                                        Utilities.Log("Saved Taming Levels from : " + __instance.name.Replace("(Clone)", "") + " to the Temporal Database");
                                        tLevels.Add(new Helper.TamingLevels(
                                            creature: sCreature,
                                            master: Masterlvl1,
                                            tamer: Tamerlvl1,
                                            unlock: Unlocklvl1,
                                            tametime: __instance.m_tamingTime));
                                    }
                                }
                                float oldTime = tLevels.Find(tlevel => tlevel.Creature == sCreature).TameTime;
                                float Master = tLevels.Find(tlevel => tlevel.Creature == sCreature).Master;
                                float Tamer = tLevels.Find(tlevel => tlevel.Creature == sCreature).Tamer;
                                float Unlock = tLevels.Find(tlevel => tlevel.Creature == sCreature).Unlock;
                                float level = MoreSkills_Instances._player.GetSkillFactor((Skills.SkillType)MoreSkills_TamingConfig.TamingSkill_Type);
                                float Dif = Unlock - (level * 100);
                                float MasterMath = ((oldTime / 2) + 120f) - (oldTime / 2);
                                float TamerMath = ((oldTime / 2) + 120f) - (((oldTime / 2) * ((level - (Tamer / 100)) * (1 / ((Master - Tamer) / 100)))));
                                float UnlockMath = oldTime - ((oldTime / 2) * ((level - (Unlock / 100)) * (1 / ((Tamer - Unlock) / 100))));
                                float UnderMath = oldTime * (Unlock - (level * 100));
                                float UnderTimeH;
                                float UnderTimeM;
                                float UnderTimeS;

                                if (Mathf.Floor(UnderMath / 3600) <= 0)
                                    UnderTimeH = 0;
                                else
                                    UnderTimeH = Mathf.Floor(UnderMath / 3600);
                                if ((Mathf.Floor(UnderMath / 60) - (UnderTimeH * 60)) <= 0)
                                    UnderTimeM = 0;
                                else
                                    UnderTimeM = Mathf.Floor(UnderMath / 60) - (UnderTimeH * 60);
                                if ((UnderMath - ((UnderTimeH * 3600) + (UnderTimeM * 60))) <= 0)
                                    UnderTimeS = 0;
                                else
                                    UnderTimeS = Mathf.Floor(UnderMath - ((UnderTimeH * 3600) + (UnderTimeM * 60)));

                                if (UnderTimeH >= 1 && UnderTimeM >= 1 && UnderTimeS >= 1)
                                    UnderTime = UnderTimeH + "h " + UnderTimeM + "m " + UnderTimeS + "s";
                                else if (UnderTimeH >= 1 && UnderTimeM >= 1 && UnderTimeS == 0)
                                    UnderTime = UnderTimeH + "h " + UnderTimeM + "m";
                                else if (UnderTimeH >= 1 && UnderTimeM == 0 && UnderTimeS >= 1)
                                    UnderTime = UnderTimeH + "h " + UnderTimeS + "s";
                                else if (UnderTimeH >= 1 && UnderTimeM == 0 && UnderTimeS == 0)
                                    UnderTime = UnderTimeH + "h";
                                else if (UnderTimeH == 0 && UnderTimeM >= 1 && UnderTimeS >= 1)
                                    UnderTime = UnderTimeM + "m " + UnderTimeS + "s";
                                else if (UnderTimeH == 0 && UnderTimeM >= 1 && UnderTimeS == 0)
                                    UnderTime = UnderTimeM + "m";
                                else if (UnderTimeH == 0 && UnderTimeM == 0 && UnderTimeS >= 1)
                                    UnderTime = UnderTimeS + "s";

                                float UnlockTimeH;
                                float UnlockTimeM;
                                float UnlockTimeS;
                                if (Mathf.Floor(UnlockMath / 3600) <= 0)
                                    UnlockTimeH = 0;
                                else
                                    UnlockTimeH = Mathf.Floor(UnlockMath / 3600);
                                if ((Mathf.Floor(UnlockMath / 60) - (UnlockTimeH * 60)) <= 0)
                                    UnlockTimeM = 0;
                                else
                                    UnlockTimeM = Mathf.Floor(UnlockMath / 60) - (UnlockTimeH * 60);
                                if ((UnlockMath - ((UnlockTimeH * 3600) + (UnlockTimeM * 60))) <= 0)
                                    UnlockTimeS = 0;
                                else
                                    UnlockTimeS = Mathf.Floor(UnlockMath - ((UnlockTimeH * 3600) + (UnlockTimeM * 60)));

                                if (UnlockTimeH >= 1 && UnlockTimeM >= 1 && UnlockTimeS >= 1)
                                    UnlockTime = UnlockTimeH + "h " + UnlockTimeM + "m " + UnlockTimeS + "s";
                                else if (UnlockTimeH >= 1 && UnlockTimeM >= 1 && UnlockTimeS == 0)
                                    UnlockTime = UnlockTimeH + "h " + UnlockTimeM + "m ";
                                else if (UnlockTimeH >= 1 && UnlockTimeM == 0 && UnlockTimeS >= 1)
                                    UnlockTime = UnlockTimeH + "h " + UnlockTimeS + "s";
                                else if (UnlockTimeH >= 1 && UnlockTimeM == 0 && UnlockTimeS == 0)
                                    UnlockTime = UnlockTimeH + "h";
                                else if (UnlockTimeH == 0 && UnlockTimeM >= 1 && UnlockTimeS >= 1)
                                    UnlockTime = UnlockTimeM + "m " + UnlockTimeS + "s";
                                else if (UnlockTimeH == 0 && UnlockTimeM >= 1 && UnlockTimeS == 0)
                                    UnlockTime = UnlockTimeM + "m";
                                else if (UnlockTimeH == 0 && UnlockTimeM == 0 && UnlockTimeS >= 1)
                                    UnlockTime = UnlockTimeS + "s";

                                float TamerTimeH;
                                float TamerTimeM;
                                float TamerTimeS;
                                if (Mathf.Floor(TamerMath / 3600) <= 0)
                                    TamerTimeH = 0;
                                else
                                    TamerTimeH = Mathf.Floor(TamerMath / 3600);
                                if ((Mathf.Floor(TamerMath / 60) - (TamerTimeH * 60)) <= 0)
                                    TamerTimeM = 0;
                                else
                                    TamerTimeM = Mathf.Floor(TamerMath / 60) - (TamerTimeH * 60);
                                if ((TamerMath - ((TamerTimeH * 3600) + (TamerTimeM * 60))) <= 0)
                                    TamerTimeS = 0;
                                else
                                    TamerTimeS = Mathf.Floor(TamerMath - ((TamerTimeH * 3600) + (TamerTimeM * 60)));

                                if (TamerTimeH >= 1 && TamerTimeM >= 1 && TamerTimeS >= 1)
                                    TamerTime = TamerTimeH + "h " + TamerTimeM + "m " + TamerTimeS + "s";
                                else if (TamerTimeH >= 1 && TamerTimeM >= 1 && TamerTimeS == 0)
                                    TamerTime = TamerTimeH + "h " + TamerTimeM + "m";
                                else if (TamerTimeH >= 1 && TamerTimeM == 0 && TamerTimeS >= 1)
                                    TamerTime = TamerTimeH + "h " + TamerTimeS + "s";
                                else if (TamerTimeH >= 1 && TamerTimeM == 0 && TamerTimeS == 0)
                                    TamerTime = TamerTimeH + "h";
                                else if (TamerTimeH == 0 && TamerTimeM >= 1 && TamerTimeS >= 1)
                                    TamerTime = TamerTimeM + "m " + TamerTimeS + "s";
                                else if (TamerTimeH == 0 && TamerTimeM >= 1 && TamerTimeS == 0)
                                    TamerTime = TamerTimeM + "m";
                                else if (TamerTimeH == 0 && TamerTimeM == 0 && TamerTimeS >= 1)
                                    TamerTime = TamerTimeS + "s";

                                float MasterTimeH;
                                float MasterTimeM;
                                float MasterTimeS;
                                if (Mathf.Floor(MasterMath / 3600) <= 0)
                                    MasterTimeH = 0;
                                else
                                    MasterTimeH = Mathf.Floor(MasterMath / 3600);
                                if ((Mathf.Floor(MasterMath / 60) - (MasterTimeH * 60)) <= 0)
                                    MasterTimeM = 0;
                                else
                                    MasterTimeM = Mathf.Floor(MasterMath / 60) - (MasterTimeH * 60);
                                if ((MasterMath - ((MasterTimeH * 3600) + (MasterTimeM * 60))) <= 0)
                                    MasterTimeS = 0;
                                else
                                    MasterTimeS = Mathf.Floor(MasterMath - ((MasterTimeH * 3600) + (MasterTimeM * 60)));

                                if (MasterTimeH >= 1 && MasterTimeM >= 1 && MasterTimeS >= 1)
                                    MasterTime = MasterTimeH + "h " + MasterTimeM + "m " + MasterTimeS + "s";
                                else if (MasterTimeH >= 1 && MasterTimeM >= 1 && MasterTimeS == 0)
                                    MasterTime = MasterTimeH + "h " + MasterTimeM + "m";
                                else if (MasterTimeH >= 1 && MasterTimeM == 0 && MasterTimeS >= 1)
                                    MasterTime = MasterTimeH + "h " + MasterTimeS + "s";
                                else if (MasterTimeH >= 1 && MasterTimeM == 0 && MasterTimeS == 0)
                                    MasterTime = MasterTimeH + "h";
                                else if (MasterTimeH == 0 && MasterTimeM >= 1 && MasterTimeS >= 1)
                                    MasterTime = MasterTimeM + "m " + MasterTimeS + "s";
                                else if (MasterTimeH == 0 && MasterTimeM >= 1 && MasterTimeS == 0)
                                    MasterTime = MasterTimeM + "m";
                                else if (MasterTimeH == 0 && MasterTimeM == 0 && MasterTimeS >= 1)
                                    MasterTime = MasterTimeS + "s";
                                if (level < (Unlock / 100))
                                {
                                    __result += Localization.instance.Localize(string.Concat(new string[]
                                        {
                                "\n<color=red><b>Tamable in ",
                                Dif.ToString("F0"),
                                " Levels</b> (",
                                UnderTime,
                                ")</color>"
                                        }));

                                }
                                else if (level < (Tamer / 100))
                                {
                                    __result += Localization.instance.Localize("\n<color=orange>Skill: <b>Novice Tamer</b> (" + UnlockTime + ")</color>");
                                }
                                else if (level < (Master / 100))
                                {
                                    __result += Localization.instance.Localize("\n<color=yellow>Skill: <b>Adept Tamer</b> (" + TamerTime + ")</color>");
                                }
                                else
                                {
                                    __result += Localization.instance.Localize("\n<color=green>Skill: <b>Master Tamer</b> (" + MasterTime + ")</color>");
                                }
                            }
                            
                            if (__instance.m_character.IsTamed() && !__instance.m_character.IsPlayer())
                            {
                                if (MoreSkills_TamingConfig.EnableCustomNames.Value)
                                {
                                    if (__instance.m_nview.GetZDO().GetString("TameName", "") == "")
                                    {
                                        __instance.m_nview.GetZDO().Set("TameName", "Type in chat, /name TameName, to name your tame");
                                    }
                                    if (MoreSkills_Instances._player.IsCrouching())
                                    {
                                        if (Name != null)
                                        {
                                            if (__instance.m_nview.GetZDO().GetString("TameName") == "Type in chat, /name TameName, to name your tame")
                                            {
                                                __instance.m_nview.GetZDO().Set("TameName", Name);
                                            }
                                        }

                                        if (__instance.m_nview.GetZDO().GetString("TameName") == "Type in chat, /name TameName, to name your tame")
                                            __result += Localization.instance.Localize("\n<color=green>" + __instance.m_nview.GetZDO().GetString("TameName") + "</color>");
                                        else if (__instance.m_character.m_name != __instance.m_nview.GetZDO().GetString("TameName"))
                                            __result += Localization.instance.Localize("\n<color=orange>" + "Get Far to Update the Name" + "</color>");

                                        __result += Localization.instance.Localize(string.Concat(
                                            "\nMaxHealth: " + __instance.m_character.GetMaxHealth(),
                                            "\nPhysicalResistance: " + __instance.m_character.GetDamageModifier(HitData.DamageType.Physical)));


                                        Name = null;
                                    }
                                    else
                                    {
                                        if (__instance.m_nview.GetZDO().GetString("TameName") == "Type in chat, /name TameName, to name your tame")
                                        {
                                            __result += Localization.instance.Localize("\n<color=orange>Crouch to change the Tame name or see his Stats</color>");
                                        }
                                        else
                                            __result += Localization.instance.Localize("\n<color=orange>Crouch to see Tame Stats</color>");
                                    }
                                }
                                //Utilities.LogWarning("Nombre: " + Name);
                            }
                            //Utilities.LogWarning("TestTameness: " + __instance.GetTameness() + " TestTamed: " + __instance.m_character.IsTamed() + " TestPlayer: " + __instance.m_character.IsPlayer());
                        }
                    }
                }
            }
        }

        [HarmonyPatch(typeof(Chat), "InputText")]
        public static class SetNameTame
        {
            public static void Postfix(ref Chat __instance)
            {
                if (MoreSkills_Instances._player != null)
                {
                    if (MoreSkills_TamingConfig.EnableCustomNames.Value)
                    {
                        if (MoreSkills_Instances._player.IsCrouching())
                        {
                            if (__instance.m_input.text.StartsWith("/name"))
                            {
                                Name = __instance.m_input.text.Replace("/name ", "");
                                //Utilities.LogWarning("Input: " + Name);
                                return;
                            }
                        }
                    }
                }
            }
        }

        [HarmonyPatch(typeof(Character), "GetHoverName")]
        public static class ChangeNameTame
        {
            public static void Prefix(ref Character __instance, ref string __result)
            {
                if (__instance != null)
                {
                    if (__instance.m_nview.GetZDO().GetString("TameName") != "" && __instance.m_nview.GetZDO().GetString("TameName") != "Type in chat, /name TameName, to name your tame")
                    {
                        __instance.m_name = __instance.m_nview.GetZDO().GetString("TameName");
                    }
                }
            }
        }

        public static float Masterlvl1;
        public static float Tamerlvl1;
        public static float Unlocklvl1;

        public static string Name;


        public static string MasterTime;
        public static string TamerTime;
        public static string UnlockTime;
        public static string UnderTime;

        public static float TamingSkillIncrease;

        public static List<Helper.TamingSaves> tSaves = new List<Helper.TamingSaves>();
        public static List<Helper.TamingLevels> tLevels = new List<Helper.TamingLevels>();
        public static List<Helper.TamingFix> tFixed = new List<Helper.TamingFix>();
        public static List<Helper.TamingGotFar> tGotFar = new List<Helper.TamingGotFar>();
        public static List<Helper.TamingDoubleCheck> tDoubleCheck = new List<Helper.TamingDoubleCheck>();
    }
}
