using HarmonyLib;
using MoreSkills.Config;
using MoreSkills.Utility;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace MoreSkills.ModSkills
{
    class MoreSkills_DropsRocksWood
    {
        private static bool useNewDropMethod = false;
        private static char[] commaSeparator = new[] { ',' };


        [HarmonyPatch(typeof(DropTable), "GetDropList", new Type[] { typeof(int) })]
        public static class DropTable_DropsRocksWood
        {
            public static void Postfix(ref DropTable __instance, ref List<GameObject> __result, int amount)
            {
                if (MoreSkills_Instances._player != null)
                {
                    if (MoreSkills_OverhaulsConfig.EnableWoodCuttingDropMod.Value || MoreSkills_OverhaulsConfig.EnablePickaxeDropMod.Value || MoreSkills_HuntingConfig.EnableHuntingSkill.Value)
                    {
                        if (MoreSkills_Instances._DDAttacker == MoreSkills_Instances._player.GetZDOID()
                            || MoreSkills_Instances._MR5DAttacker == MoreSkills_Instances._player.GetZDOID()
                            || MoreSkills_Instances._TBDAttacker == MoreSkills_Instances._player.GetZDOID()
                            || MoreSkills_Instances._TLDAttacker == MoreSkills_Instances._player.GetZDOID())
                        {
                            //WoodCutting
                            float cBeechSeed = 0;
                            GameObject objectBeechSeed = null;

                            float cElderBark = 0;
                            GameObject objectElderBark = null;

                            float cFineWood = 0;
                            GameObject objectFineWood = null;

                            float cFirCone = 0;
                            GameObject objectFirCone = null;

                            float cPineCone = 0;
                            GameObject objectPineCone = null;

                            float cResin = 0;
                            GameObject objectResin = null;

                            float cRoundLog = 0;
                            GameObject objectRoundLog = null;

                            float cWood = 0;
                            GameObject objectWood = null;

                            //Minerals

                            float cChitin = 0;
                            GameObject objectChitin = null;

                            float cCopperOre = 0;
                            GameObject objectCopperOre = null;

                            float cIronScrap = 0;
                            GameObject objectIronScrap = null;

                            float cObsidian = 0;
                            GameObject objectObsidian = null;

                            float cSilverOre = 0;
                            GameObject objectSilverOre = null;

                            float cStone = 0;
                            GameObject objectStone = null;

                            float cTinOre = 0;
                            GameObject objectTinOre = null;

                            //Others

                            float cFeathers = 0;
                            GameObject objectFeathers = null;

                            float cGuck = 0;
                            GameObject objectGuck = null;

                            float cLeatherScraps = 0;
                            GameObject objectLeatherScraps = null;

                            float cWitheredBone = 0;
                            GameObject objectWitheredBone = null;

                            List<GameObject> Drops = new List<GameObject>();
                            foreach (GameObject objectDrops in __result)
                            {
                                /*Debug.LogWarning("Nombre: " + casa.name);
                                Debug.LogWarning("Chance: " + __instance.m_dropChance);
                                Debug.LogWarning("Min: " + __instance.m_dropMin);
                                Debug.LogWarning("Max: " + __instance.m_dropMax);*/

                                //Woods
                                if (objectDrops.name == "BeechSeeds")
                                {
                                    cBeechSeed += 1;
                                    objectBeechSeed = objectDrops;
                                }
                                else if (objectDrops.name == "ElderBark")
                                {
                                    cElderBark += 1;
                                    objectElderBark = objectDrops;
                                }
                                else if (objectDrops.name == "FineWood")
                                {
                                    cFineWood += 1;
                                    objectFineWood = objectDrops;
                                }
                                else if (objectDrops.name == "FirCone")
                                {
                                    cFirCone += 1;
                                    objectFirCone = objectDrops;
                                }
                                else if (objectDrops.name == "PineCone")
                                {
                                    cPineCone += 1;
                                    objectPineCone = objectDrops;
                                }
                                else if (objectDrops.name == "Resin")
                                {
                                    cResin += 1;
                                    objectResin = objectDrops;
                                }
                                else if (objectDrops.name == "RoundLog")
                                {
                                    cRoundLog += 1;
                                    objectRoundLog = objectDrops;
                                }
                                else if (objectDrops.name == "Wood")
                                {
                                    cWood += 1;
                                    objectWood = objectDrops;
                                }
                                //Minerals
                                else if (objectDrops.name == "Chitin")
                                {
                                    cChitin += 1;
                                    objectChitin = objectDrops;
                                }
                                else if (objectDrops.name == "CopperOre")
                                {
                                    cCopperOre += 1;
                                    objectCopperOre = objectDrops;
                                }
                                else if (objectDrops.name == "IronScrap")
                                {
                                    cIronScrap += 1;
                                    objectIronScrap = objectDrops;
                                }
                                else if (objectDrops.name == "Obsidian")
                                {
                                    cObsidian += 1;
                                    objectObsidian = objectDrops;
                                }
                                else if (objectDrops.name == "SilverOre")
                                {
                                    cSilverOre += 1;
                                    objectSilverOre = objectDrops;
                                }
                                else if (objectDrops.name == "TinOre")
                                {
                                    cTinOre += 1;
                                    objectTinOre = objectDrops;
                                }
                                else if (objectDrops.name == "Stone")
                                {
                                    cStone += 1;
                                    objectStone = objectDrops;
                                }
                                //Others
                                else if (objectDrops.name == "Feathers")
                                {
                                    cFeathers += 1;
                                    objectFeathers = objectDrops;
                                }
                                else if (objectDrops.name == "Guck")
                                {
                                    cGuck += 1;
                                    objectGuck = objectDrops;
                                }
                                else if (objectDrops.name == "LeatherScraps")
                                {
                                    cLeatherScraps += 1;
                                    objectLeatherScraps = objectDrops;
                                }
                                else if (objectDrops.name == "WitheredBone")
                                {
                                    cWitheredBone += 1;
                                    objectWitheredBone = objectDrops;
                                }
                                else
                                {
                                    Debug.LogError("[MoreSkills]: Report Missing Drop: " + objectDrops.name);
                                    Drops.Add(objectDrops);
                                }
                            }

                            //Wood
                            if (MoreSkills_OverhaulsConfig.EnableWoodCuttingDropMod.Value)
                            {
                                for (int i = 0; i < (int)(cBeechSeed + (cBeechSeed * ((MoreSkills_OverhaulsConfig.WoodCuttingMultiplier.Value - 1) * MoreSkills_Instances._player.GetSkillFactor(Skills.SkillType.WoodCutting)))); i++)
                                {
                                    Drops.Add(objectBeechSeed);
                                }
                                for (int i = 0; i < (int)(cElderBark + (cElderBark * ((MoreSkills_OverhaulsConfig.WoodCuttingMultiplier.Value - 1) * MoreSkills_Instances._player.GetSkillFactor(Skills.SkillType.WoodCutting)))); i++)
                                {
                                    Drops.Add(objectElderBark);
                                }
                                for (int i = 0; i < (int)(cFineWood + (cFineWood * ((MoreSkills_OverhaulsConfig.WoodCuttingMultiplier.Value - 1) * MoreSkills_Instances._player.GetSkillFactor(Skills.SkillType.WoodCutting)))); i++)
                                {
                                    Drops.Add(objectFineWood);
                                }
                                for (int i = 0; i < (int)(cFirCone + (cFirCone * ((MoreSkills_OverhaulsConfig.WoodCuttingMultiplier.Value - 1) * MoreSkills_Instances._player.GetSkillFactor(Skills.SkillType.WoodCutting)))); i++)
                                {
                                    Drops.Add(objectFirCone);
                                }
                                for (int i = 0; i < (int)(cPineCone + (cPineCone * ((MoreSkills_OverhaulsConfig.WoodCuttingMultiplier.Value - 1) * MoreSkills_Instances._player.GetSkillFactor(Skills.SkillType.WoodCutting)))); i++)
                                {
                                    Drops.Add(objectPineCone);
                                }
                                for (int i = 0; i < (int)(cResin + (cResin * ((MoreSkills_OverhaulsConfig.WoodCuttingMultiplier.Value - 1) * MoreSkills_Instances._player.GetSkillFactor(Skills.SkillType.WoodCutting)))); i++)
                                {
                                    Drops.Add(objectResin);
                                }
                                for (int i = 0; i < (int)(cRoundLog + (cRoundLog * ((MoreSkills_OverhaulsConfig.WoodCuttingMultiplier.Value - 1) * MoreSkills_Instances._player.GetSkillFactor(Skills.SkillType.WoodCutting)))); i++)
                                {
                                    Drops.Add(objectRoundLog);
                                }
                                for (int i = 0; i < (int)(cWood + (cWood * ((MoreSkills_OverhaulsConfig.WoodCuttingMultiplier.Value - 1) * MoreSkills_Instances._player.GetSkillFactor(Skills.SkillType.WoodCutting)))); i++)
                                {
                                    Drops.Add(objectWood);
                                }
                            }
                            else
                            {
                                for (int i = 0; i < (int)(cBeechSeed); i++)
                                {
                                    Drops.Add(objectBeechSeed);
                                }
                                for (int i = 0; i < (int)(cElderBark); i++)
                                {
                                    Drops.Add(objectElderBark);
                                }
                                for (int i = 0; i < (int)(cFineWood); i++)
                                {
                                    Drops.Add(objectFineWood);
                                }
                                for (int i = 0; i < (int)(cFirCone); i++)
                                {
                                    Drops.Add(objectFirCone);
                                }
                                for (int i = 0; i < (int)(cPineCone); i++)
                                {
                                    Drops.Add(objectPineCone);
                                }
                                for (int i = 0; i < (int)(cResin); i++)
                                {
                                    Drops.Add(objectResin);
                                }
                                for (int i = 0; i < (int)(cRoundLog); i++)
                                {
                                    Drops.Add(objectRoundLog);
                                }
                                for (int i = 0; i < (int)(cWood); i++)
                                {
                                    Drops.Add(objectWood);
                                }
                            }

                            //Minerals
                            if (MoreSkills_OverhaulsConfig.EnablePickaxeDropMod.Value)
                            {
                                for (int i = 0; i < (int)(cChitin + (cChitin * ((MoreSkills_OverhaulsConfig.PickaxeMultiplier.Value - 1) * MoreSkills_Instances._player.GetSkillFactor(Skills.SkillType.Pickaxes)))); i++)
                                {
                                    Drops.Add(objectChitin);
                                }
                                for (int i = 0; i < (int)(cCopperOre + (cCopperOre * ((MoreSkills_OverhaulsConfig.PickaxeMultiplier.Value - 1) * MoreSkills_Instances._player.GetSkillFactor(Skills.SkillType.Pickaxes)))); i++)
                                {
                                    Drops.Add(objectCopperOre);
                                }
                                for (int i = 0; i < (int)(cIronScrap + (cIronScrap * ((MoreSkills_OverhaulsConfig.PickaxeMultiplier.Value - 1) * MoreSkills_Instances._player.GetSkillFactor(Skills.SkillType.Pickaxes)))); i++)
                                {
                                    Drops.Add(objectIronScrap);
                                }
                                for (int i = 0; i < (int)(cObsidian + (cObsidian * ((MoreSkills_OverhaulsConfig.PickaxeMultiplier.Value - 1) * MoreSkills_Instances._player.GetSkillFactor(Skills.SkillType.Pickaxes)))); i++)
                                {
                                    Drops.Add(objectObsidian);
                                }
                                for (int i = 0; i < (int)(cSilverOre + (cSilverOre * ((MoreSkills_OverhaulsConfig.PickaxeMultiplier.Value - 1) * MoreSkills_Instances._player.GetSkillFactor(Skills.SkillType.Pickaxes)))); i++)
                                {
                                    Drops.Add(objectSilverOre);
                                }
                                for (int i = 0; i < (int)(cStone + (cStone * ((MoreSkills_OverhaulsConfig.PickaxeMultiplier.Value - 1) * MoreSkills_Instances._player.GetSkillFactor(Skills.SkillType.Pickaxes)))); i++)
                                {
                                    Drops.Add(objectStone);
                                }
                                for (int i = 0; i < (int)(cTinOre + (cTinOre * ((MoreSkills_OverhaulsConfig.PickaxeMultiplier.Value - 1) * MoreSkills_Instances._player.GetSkillFactor(Skills.SkillType.Pickaxes)))); i++)
                                {
                                    Drops.Add(objectTinOre);
                                }
                            }
                            else
                            {
                                for (int i = 0; i < (int)(cChitin); i++)
                                {
                                    Drops.Add(objectChitin);
                                }
                                for (int i = 0; i < (int)(cCopperOre); i++)
                                {
                                    Drops.Add(objectCopperOre);
                                }
                                for (int i = 0; i < (int)(cIronScrap); i++)
                                {
                                    Drops.Add(objectIronScrap);
                                }
                                for (int i = 0; i < (int)(cObsidian); i++)
                                {
                                    Drops.Add(objectObsidian);
                                }
                                for (int i = 0; i < (int)(cSilverOre); i++)
                                {
                                    Drops.Add(objectSilverOre);
                                }
                                for (int i = 0; i < (int)(cStone); i++)
                                {
                                    Drops.Add(objectStone);
                                }
                                for (int i = 0; i < (int)(cTinOre); i++)
                                {
                                    Drops.Add(objectTinOre);
                                }
                            }

                            //Others
                            if (MoreSkills_HuntingConfig.EnableHuntingSkill.Value)
                            {
                                for (int i = 0; i < (int)(cFeathers + (cFeathers * ((MoreSkills_HuntingConfig.HuntingDropMultiplier.Value - 1) * MoreSkills_Instances._player.GetSkillFactor((Skills.SkillType)MoreSkills_HuntingConfig.HuntingSkill_Type)))); i++)
                                {
                                    Drops.Add(objectFeathers);
                                }
                                for (int i = 0; i < (int)(cGuck + (cGuck * ((MoreSkills_HuntingConfig.HuntingDropMultiplier.Value - 1) * MoreSkills_Instances._player.GetSkillFactor((Skills.SkillType)MoreSkills_HuntingConfig.HuntingSkill_Type)))); i++)
                                {
                                    Drops.Add(objectGuck);
                                }
                                for (int i = 0; i < (int)(cLeatherScraps + (cLeatherScraps * ((MoreSkills_HuntingConfig.HuntingDropMultiplier.Value - 1) * MoreSkills_Instances._player.GetSkillFactor((Skills.SkillType)MoreSkills_HuntingConfig.HuntingSkill_Type)))); i++)
                                {
                                    Drops.Add(objectLeatherScraps);
                                }
                                for (int i = 0; i < (int)(cWitheredBone + (cWitheredBone * ((MoreSkills_HuntingConfig.HuntingDropMultiplier.Value - 1) * MoreSkills_Instances._player.GetSkillFactor((Skills.SkillType)MoreSkills_HuntingConfig.HuntingSkill_Type)))); i++)
                                {
                                    Drops.Add(objectWitheredBone);
                                }
                            }
                            else
                            {
                                for (int i = 0; i < (int)(cFeathers); i++)
                                {
                                    Drops.Add(objectFeathers);
                                }
                                for (int i = 0; i < (int)(cGuck); i++)
                                {
                                    Drops.Add(objectGuck);
                                }
                                for (int i = 0; i < (int)(cLeatherScraps); i++)
                                {
                                    Drops.Add(objectLeatherScraps);
                                }
                                for (int i = 0; i < (int)(cWitheredBone); i++)
                                {
                                    Drops.Add(objectWitheredBone);
                                }
                            }

                            __result = Drops;
                        }
                    }
                }
            }
        }

        //Old version

        /*[HarmonyPatch(typeof(MineRock5), "Damage")]
        public static class Pickaxe_DropMod
        {
            public static void Postfix(MineRock5 __instance, HitData hit)
            {
                if (MoreSkills_Config.EnablePickaxeDropMod.Value)
                {
                    if (MoreSkills_Instances._player != null && hit.m_attacker == MoreSkills_Instances._player.GetZDOID())
                    {
                        /*for (int i = 0; i < ((__instance.m_hitAreas.Count <= 128) ? __instance.m_hitAreas.Count : 128); i++)
                        {
                            int[] j = new int[i];

                            if (__instance.m_hitAreas[i].m_health > 0f)


                            __instance.m_nview.InvokeRPC("Damage", new object[]
                            {
                            hit,
                            i
                            });
                        }

                        float level = MoreSkills_Instances._player.GetSkillFactor(Skills.SkillType.Pickaxes);

                        if (__instance.m_name == "Rock")
                        {
                            if ((level * 100f) > 50)
                            {
                                float maxskill_inc = Mathf.Round(MoreSkills_Config.BaseMaxRock.Value * MoreSkills_Config.PickaxeMultiplier.Value);
                                __instance.m_dropItems.m_dropMax = (int)(MoreSkills_Config.BaseMaxRock.Value + maxskill_inc);
                                float skill = level * MoreSkills_Config.PickaxeMultiplier.Value;
                                float skill_inc = Mathf.Round(MoreSkills_Config.BaseMinRock.Value * skill);
                                __instance.m_dropItems.m_dropMin = (int)(MoreSkills_Config.BaseMinRock.Value + skill_inc);
                            }
                            else
                            {
                                float maxskill = (level * 2) * MoreSkills_Config.PickaxeMultiplier.Value;
                                float maxskill_inc = Mathf.Round(MoreSkills_Config.BaseMaxRock.Value * maxskill);
                                __instance.m_dropItems.m_dropMax = (int)(MoreSkills_Config.BaseMaxRock.Value + maxskill_inc);
                            }
                        }

                        if (__instance.m_name == "$piece_deposit_copper")
                        {
                            if ((level * 100f) > 50)
                            {
                                float maxskill_inc = Mathf.Round(MoreSkills_Config.BaseMaxCopperVein.Value * MoreSkills_Config.PickaxeMultiplier.Value);
                                __instance.m_dropItems.m_dropMax = (int)(MoreSkills_Config.BaseMaxCopperVein.Value + maxskill_inc);
                                float skill = level * MoreSkills_Config.PickaxeMultiplier.Value;
                                float skill_inc = Mathf.Round(MoreSkills_Config.BaseMinCopperVein.Value * skill);
                                __instance.m_dropItems.m_dropMin = (int)(MoreSkills_Config.BaseMinCopperVein.Value + skill_inc);
                            }
                            else
                            {
                                float maxskill = (level * 2) * MoreSkills_Config.PickaxeMultiplier.Value;
                                float maxskill_inc = Mathf.Round(MoreSkills_Config.BaseMaxCopperVein.Value * maxskill);
                                __instance.m_dropItems.m_dropMax = (int)(MoreSkills_Config.BaseMaxCopperVein.Value + maxskill_inc);
                            }
                        }

                        if (__instance.m_name == "$piece_mudpile")
                        {
                            if (MoreSkills_Config.EnablePickaxeChanceMod.Value)
                            {
                                __instance.m_dropItems.m_dropChance = MoreSkills_Config.BaseChanceMudPile.Value + (MoreSkills_Instances._player.GetSkillFactor(Skills.SkillType.Pickaxes));
                            }
                            if ((level * 100f) > 50)
                            {
                                float maxskill_inc = Mathf.Round(MoreSkills_Config.BaseMaxMudPile.Value * MoreSkills_Config.PickaxeMultiplier.Value);
                                __instance.m_dropItems.m_dropMax = (int)(MoreSkills_Config.BaseMaxMudPile.Value + maxskill_inc);
                                float skill = level * MoreSkills_Config.PickaxeMultiplier.Value;
                                float skill_inc = Mathf.Round(MoreSkills_Config.BaseMinMudPile.Value * skill);
                                __instance.m_dropItems.m_dropMin = (int)(MoreSkills_Config.BaseMinMudPile.Value + skill_inc);
                            }
                            else
                            {
                                float maxskill = (level * 2) * MoreSkills_Config.PickaxeMultiplier.Value;
                                float maxskill_inc = Mathf.Round(MoreSkills_Config.BaseMaxMudPile.Value * maxskill);
                                __instance.m_dropItems.m_dropMax = (int)(MoreSkills_Config.BaseMaxMudPile.Value + maxskill_inc);
                            }
                        }

                        if (__instance.m_name == "Silver vein")
                        {
                            if ((level * 100f) > 50)
                            {
                                float maxskill_inc = Mathf.Round(MoreSkills_Config.BaseMaxSilverVein.Value * MoreSkills_Config.PickaxeMultiplier.Value);
                                __instance.m_dropItems.m_dropMax = (int)(MoreSkills_Config.BaseMaxSilverVein.Value + maxskill_inc);
                                float skill = level * MoreSkills_Config.PickaxeMultiplier.Value;
                                float skill_inc = Mathf.Round(MoreSkills_Config.BaseMinSilverVein.Value * skill);
                                __instance.m_dropItems.m_dropMin = (int)(MoreSkills_Config.BaseMinSilverVein.Value + skill_inc);
                            }
                            else
                            {
                                float maxskill = (level * 2) * MoreSkills_Config.PickaxeMultiplier.Value;
                                float maxskill_inc = Mathf.Round(MoreSkills_Config.BaseMaxSilverVein.Value * maxskill);
                                __instance.m_dropItems.m_dropMax = (int)(MoreSkills_Config.BaseMaxSilverVein.Value + maxskill_inc);
                            }
                        }

                        if (__instance.m_name == null && __instance.m_hitAreas.Count > 10)
                        {
                            if ((level * 100f) > 50)
                            {
                                float maxskill_inc = Mathf.Round(MoreSkills_Config.BaseMaxBigRock.Value * MoreSkills_Config.PickaxeMultiplier.Value);
                                __instance.m_dropItems.m_dropMax = (int)(MoreSkills_Config.BaseMaxBigRock.Value + maxskill_inc);
                                float skill = level * MoreSkills_Config.PickaxeMultiplier.Value;
                                float skill_inc = Mathf.Round(MoreSkills_Config.BaseMinBigRock.Value * skill);
                                __instance.m_dropItems.m_dropMin = (int)(MoreSkills_Config.BaseMinBigRock.Value + skill_inc);
                            }
                            else
                            {
                                float maxskill = (level * 2) * MoreSkills_Config.PickaxeMultiplier.Value;
                                float maxskill_inc = Mathf.Round(MoreSkills_Config.BaseMaxBigRock.Value * maxskill);
                                __instance.m_dropItems.m_dropMax = (int)(MoreSkills_Config.BaseMaxBigRock.Value + maxskill_inc);
                            }
                        }

                        /*Debug.LogWarning("Nombre: " + __instance.m_name);
                        Debug.LogWarning("Cantidad de rocas: " + __instance.m_hitAreas.Count);
                        Debug.LogWarning("Chance: " + __instance.m_dropItems.m_dropChance);
                        Debug.LogWarning("Min: " + __instance.m_dropItems.m_dropMin);
                        Debug.LogWarning("Max: " + __instance.m_dropItems.m_dropMax);
                    }
                }



            }
        }*/
    }
}
