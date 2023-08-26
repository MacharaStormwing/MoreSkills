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
        // new cleaner drop method that uses configurable item names that are to be handled: use it if the value is true
        private static bool useNewDropMethod = true;

        private static char[] commaSeparator = new[] { ',' };

        private static float getAppliedWoodLootFactor(float appliedWoodLootFactor)
        {
            if (appliedWoodLootFactor == 0)
            {
                float woodCuttingSkillFactor = MoreSkills_Instances._player.GetSkillFactor(Skills.SkillType.WoodCutting);
                appliedWoodLootFactor = ((MoreSkills_OverhaulsConfig.WoodCuttingMultiplier.Value - 1) * woodCuttingSkillFactor);
            }
            return appliedWoodLootFactor;
        }

        private static float getAppliedStoneLootFactor(float appliedStoneLootFactor)
        {
            if (appliedStoneLootFactor == 0)
            {
                float pickaxesSkillFactor = MoreSkills_Instances._player.GetSkillFactor(Skills.SkillType.Pickaxes);
                appliedStoneLootFactor = ((MoreSkills_OverhaulsConfig.PickaxeMultiplier.Value - 1) * pickaxesSkillFactor);
            }
            return appliedStoneLootFactor;
        }

        private static float getAppliedHuntingLootFactor(float appliedHuntingLootFactor)
        {
            if (appliedHuntingLootFactor == 0)
            {
                float huntingSkillFactor = MoreSkills_Instances._player.GetSkillFactor((Skills.SkillType)MoreSkills_HuntingConfig.HuntingSkill_Type);
                appliedHuntingLootFactor = ((MoreSkills_HuntingConfig.HuntingDropMultiplier.Value - 1) * huntingSkillFactor);
            }
            return appliedHuntingLootFactor;
        }


        [HarmonyPatch(typeof(DropTable), nameof(DropTable.GetDropList), new Type[] { typeof(int) })]
        public static class DropTable_DropsRocksWood
        {

            public static void Postfix(ref DropTable __instance, ref List<GameObject> __result, int amount)
            {
                if (MoreSkills_Instances._player != null && __result != null && __result.Count > 0)
                {
                    if (MoreSkills_OverhaulsConfig.EnableWoodCuttingDropMod.Value || MoreSkills_OverhaulsConfig.EnablePickaxeDropMod.Value || MoreSkills_HuntingConfig.EnableHuntingSkill.Value)
                    {
                        if (MoreSkills_Instances._DDAttacker == MoreSkills_Instances._player.GetZDOID()
                            || MoreSkills_Instances._MR5DAttacker == MoreSkills_Instances._player.GetZDOID()
                            || MoreSkills_Instances._TBDAttacker == MoreSkills_Instances._player.GetZDOID()
                            || MoreSkills_Instances._TLDAttacker == MoreSkills_Instances._player.GetZDOID())
                        {
                            bool advancedLogging = MoreSkills_OverhaulsConfig.EnableDetailedLogging.Value;

                            if (advancedLogging)
                                Utilities.Log("Starting DropsRocksWood.Postfix with " + __result.Count + " drops");

                            List<GameObject> Drops = new List<GameObject>();                            

                            float appliedWoodLootFactor = 0;
                            float appliedStoneLootFactor = 0;
                            float appliedHuntingLootFactor = 0;

                            if (useNewDropMethod)
                            {
                                // This is the new way of handling drop multipliers more generally by Machara Stormwing

                                // get the items to handle for each skill from configuration
                                string[] woodcuttingDroppedItemNames = MoreSkills_OverhaulsConfig.WoodCuttingApplyForItems.Value.Split(commaSeparator, StringSplitOptions.RemoveEmptyEntries);
                                string[] pickaxeDroppedItemNames = MoreSkills_OverhaulsConfig.PickaxeApplyForItems.Value.Split(commaSeparator, StringSplitOptions.RemoveEmptyEntries);
                                string[] huntingDroppedItemNames = MoreSkills_OverhaulsConfig.HuntingApplyForItems.Value.Split(commaSeparator, StringSplitOptions.RemoveEmptyEntries);
                               
                                string[] IgnoreDropItemNames = MoreSkills_OverhaulsConfig.IgnoreDropItemNames.Value.Split(commaSeparator, StringSplitOptions.RemoveEmptyEntries);
                                HashSet<string> IgnoreDropItemNamesSet = new HashSet<string>(IgnoreDropItemNames);

                                string[] DontDropItemNames = MoreSkills_OverhaulsConfig.DontDropItemNames.Value.Split(commaSeparator, StringSplitOptions.RemoveEmptyEntries);
                                HashSet<string> DontDropItemNamesSet = new HashSet<string>(DontDropItemNames);

                                Dictionary<string, DropObjectContainer> dropObjects = new Dictionary<string, DropObjectContainer>();
                                foreach (string itemName in woodcuttingDroppedItemNames) {
                                    if (!dropObjects.ContainsKey(itemName))
                                        dropObjects.Add(itemName, new DropObjectContainer(0, DropObjectAssocinatedSkillType.Woodworking, null));
                                    else
                                        Utilities.LogWarning("Key '" + itemName + "' already exists in '"
                                            + MoreSkills_OverhaulsConfig.WoodCuttingApplyForItems.Definition.Key + "' found in config '"
                                            + MoreSkills_OverhaulsConfig.Plugin_Name + ".cfg'. Did you configure it twice?");
                                }
                                foreach (string itemName in pickaxeDroppedItemNames)
                                {
                                    if (!dropObjects.ContainsKey(itemName))
                                        dropObjects.Add(itemName, new DropObjectContainer(0, DropObjectAssocinatedSkillType.Pickaxe, null));
                                    else
                                        Utilities.LogWarning("Key '" + itemName + "' already exists in '"
                                            + MoreSkills_OverhaulsConfig.PickaxeApplyForItems.Definition.Key + "' found in config '"
                                            + MoreSkills_OverhaulsConfig.Plugin_Name + ".cfg'. Did you configure it twice?");
                                }
                                foreach (string itemName in huntingDroppedItemNames)
                                {
                                    if (!dropObjects.ContainsKey(itemName))
                                        dropObjects.Add(itemName, new DropObjectContainer(0, DropObjectAssocinatedSkillType.Hunting, null));
                                    else
                                        Utilities.LogWarning("Key '" + itemName + "' already exists in '"
                                            + MoreSkills_OverhaulsConfig.HuntingApplyForItems.Definition.Key + "' found in config '"
                                            + MoreSkills_OverhaulsConfig.Plugin_Name + ".cfg'. Did you configure it twice?");
                                }

                                foreach (GameObject droppedObject in __result)
                                {
                                    if (droppedObject == null)
                                    {
                                        continue;
                                    }

                                    // register all dropped items into our Dictionary of known drop types
                                    if (dropObjects.TryGetValue(droppedObject.name, out var matchingDropContainer))
                                    {
                                        if (matchingDropContainer.DropItem == null)
                                        {
                                            matchingDropContainer.DropItem = droppedObject;
                                        }
                                        matchingDropContainer.NumItems++;
                                        // for some reason this is needed so the changes are stored in the Dictionary. Why?
                                        // solution: changing DropObjectContainer from struct to class
                                        //dropObjects[droppedObject.name] = matchingDropContainer;

                                        if (advancedLogging)
                                            Utilities.Log("Added drop '" + droppedObject.name + "' to dropObjects map (skillType="
                                            + matchingDropContainer.SkillType + ", NumItems=" + matchingDropContainer.NumItems  + ")");
                                    }
                                    else
                                    {
                                        if (DontDropItemNamesSet.Contains(droppedObject.name)) {
                                            // do not drop item at all, such as with crystal stones of the Jewelcrafting mod
                                            if (advancedLogging)
                                                Utilities.LogWarning("Not dropping Drop '" + droppedObject.name + "' because it was found in DontDropItemNames.");
                                        } else
                                        {
                                            if (!IgnoreDropItemNamesSet.Contains(droppedObject.name))
                                                Utilities.LogWarning("Unknown Drop '" + droppedObject.name
                                                    + "'. Ignoring skill and just dropping item.");

                                            Drops.Add(droppedObject);
                                        }
                                    }
                                }

                                appliedWoodLootFactor = getAppliedWoodLootFactor(appliedWoodLootFactor);
                                appliedStoneLootFactor = getAppliedStoneLootFactor(appliedStoneLootFactor);
                                appliedHuntingLootFactor = getAppliedHuntingLootFactor(appliedHuntingLootFactor);

                                foreach (String itemName in dropObjects.Keys)
                                {
                                    DropObjectContainer dropContainer;
                                    if (dropObjects.TryGetValue(itemName, out dropContainer) &&
                                        dropContainer.NumItems > 0 && dropContainer.DropItem != null)
                                    {
                                        // for each item check what type of skill it associates to,
                                        // and thus what the applied lootfactor should be
                                        // then caltulated number of items to be dropped (number of existing items multiplied by loot factor
                                        // and add necessary number of items to the drop
                                        int nrOfItems = 0;
                                        switch (dropContainer.SkillType)
                                        {
                                            case DropObjectAssocinatedSkillType.Woodworking:
                                                nrOfItems = MoreSkills_OverhaulsConfig.EnableWoodCuttingDropMod.Value ?
                                                    (int)(dropContainer.NumItems + (dropContainer.NumItems * appliedWoodLootFactor)) : dropContainer.NumItems;
                                                break;
                                            case DropObjectAssocinatedSkillType.Pickaxe:
                                                nrOfItems = MoreSkills_OverhaulsConfig.EnablePickaxeDropMod.Value ?
                                                    (int)(dropContainer.NumItems + (dropContainer.NumItems * appliedStoneLootFactor)) : dropContainer.NumItems;
                                                break;
                                            case DropObjectAssocinatedSkillType.Hunting:
                                                nrOfItems = MoreSkills_HuntingConfig.EnableHuntingSkill.Value ?
                                                    (int)(dropContainer.NumItems + (dropContainer.NumItems * appliedHuntingLootFactor)) : dropContainer.NumItems;
                                                break;
                                            default:
                                                Utilities.LogWarning("Unknown dropContainer Skilltype '" + dropContainer.SkillType + "'");
                                                break;
                                        }

                                        if (advancedLogging)
                                            Utilities.Log("Dropping " + nrOfItems + " " + dropContainer.DropItem.name + " (original drops: " + dropContainer.NumItems + ")");

                                        for (int i = 0; i < nrOfItems; i++)
                                        {
                                            Drops.Add(dropContainer.DropItem);
                                        }
                                    }
                                    else
                                    {
                                        /*if (advancedLogging)
                                            Utilities.Log("Ignoring DropItem type " + itemName + ":"
                                            + (dropContainer.NumItems == 0 ? " NumItems=0" : "")
                                            + (dropContainer.DropItem == null ? " DropItem=null" : ""));*/
                                    }
                                }
                            }
                            else
                            {
                                // old implementation for security (but with new added drop types) by guiriguy
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

                                // "BirchCone", "OakSeeds" and "Acorn" added by Machara Stormwing
                                float cBirchCone = 0;
                                GameObject objectBirchCone = null;

                                float cOakSeeds = 0;
                                GameObject objectOakSeeds = null;

                                float cAcorn = 0;
                                GameObject objectAcorn = null;

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

                                //Utilities.LogWarning("Chance: " + __instance.m_dropChance);
                                //Utilities.LogWarning("Min: " + __instance.m_dropMin);
                                //Utilities.LogWarning("Max: " + __instance.m_dropMax);

                                foreach (GameObject objectDrops in __result)
                                {
                                    if (objectDrops == null)
                                    {
                                        continue;
                                    }

                                    //Utilities.LogWarning("Drop Item Name: " + objectDrops.name);


                                    //Woods
                                    if (objectDrops.name == "BeechSeeds")
                                    {
                                        cBeechSeed += 1;
                                        objectBeechSeed = objectDrops;
                                    }
                                    else if (objectDrops.name == "BirchCone")
                                    {
                                        cBirchCone += 1;
                                        objectBirchCone = objectDrops;
                                    }
                                    else if (objectDrops.name == "OakSeeds")
                                    {
                                        cOakSeeds += 1;
                                        objectOakSeeds = objectDrops;
                                    }
                                    else if (objectDrops.name == "Acorn")
                                    {
                                        cAcorn += 1;
                                        objectAcorn = objectDrops;
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
                                        Utilities.LogWarning("Report Missing/Unknown Drop: '" + objectDrops.name + "'");
                                        Drops.Add(objectDrops);
                                    }
                                }

                                //Wood
                                if (MoreSkills_OverhaulsConfig.EnableWoodCuttingDropMod.Value)
                                {
                                    appliedWoodLootFactor = getAppliedWoodLootFactor(appliedWoodLootFactor);

                                    for (int i = 0; i < (int)(cBeechSeed + (cBeechSeed * appliedWoodLootFactor)); i++)
                                    {
                                        Drops.Add(objectBeechSeed);
                                    }
                                    for (int i = 0; i < (int)(cBirchCone + (cBirchCone * appliedWoodLootFactor)); i++)
                                    {
                                        Drops.Add(objectBirchCone);
                                    }
                                    for (int i = 0; i < (int)(cOakSeeds + (cOakSeeds * appliedWoodLootFactor)); i++)
                                    {
                                        Drops.Add(objectOakSeeds);
                                    }
                                    for (int i = 0; i < (int)(cAcorn + (cAcorn * appliedWoodLootFactor)); i++)
                                    {
                                        Drops.Add(objectAcorn);
                                    }
                                    for (int i = 0; i < (int)(cElderBark + (cElderBark * appliedWoodLootFactor)); i++)
                                    {
                                        Drops.Add(objectElderBark);
                                    }
                                    for (int i = 0; i < (int)(cFineWood + (cFineWood * appliedWoodLootFactor)); i++)
                                    {
                                        Drops.Add(objectFineWood);
                                    }
                                    for (int i = 0; i < (int)(cFirCone + (cFirCone * appliedWoodLootFactor)); i++)
                                    {
                                        Drops.Add(objectFirCone);
                                    }
                                    for (int i = 0; i < (int)(cPineCone + (cPineCone * appliedWoodLootFactor)); i++)
                                    {
                                        Drops.Add(objectPineCone);
                                    }
                                    for (int i = 0; i < (int)(cResin + (cResin * appliedWoodLootFactor)); i++)
                                    {
                                        Drops.Add(objectResin);
                                    }
                                    for (int i = 0; i < (int)(cRoundLog + (cRoundLog * appliedWoodLootFactor)); i++)
                                    {
                                        Drops.Add(objectRoundLog);
                                    }
                                    for (int i = 0; i < (int)(cWood + (cWood * appliedWoodLootFactor)); i++)
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
                                    for (int i = 0; i < (int)(cBirchCone); i++)
                                    {
                                        Drops.Add(objectBirchCone);
                                    }
                                    for (int i = 0; i < (int)(cOakSeeds); i++)
                                    {
                                        Drops.Add(objectOakSeeds);
                                    }
                                    for (int i = 0; i < (int)(cAcorn); i++)
                                    {
                                        Drops.Add(objectAcorn);
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
                                    appliedStoneLootFactor = getAppliedStoneLootFactor(appliedStoneLootFactor);

                                    for (int i = 0; i < (int)(cChitin + (cChitin * appliedStoneLootFactor)); i++)
                                    {
                                        Drops.Add(objectChitin);
                                    }
                                    for (int i = 0; i < (int)(cCopperOre + (cCopperOre * appliedStoneLootFactor)); i++)
                                    {
                                        Drops.Add(objectCopperOre);
                                    }
                                    for (int i = 0; i < (int)(cIronScrap + (cIronScrap * appliedStoneLootFactor)); i++)
                                    {
                                        Drops.Add(objectIronScrap);
                                    }
                                    for (int i = 0; i < (int)(cObsidian + (cObsidian * appliedStoneLootFactor)); i++)
                                    {
                                        Drops.Add(objectObsidian);
                                    }
                                    for (int i = 0; i < (int)(cSilverOre + (cSilverOre * appliedStoneLootFactor)); i++)
                                    {
                                        Drops.Add(objectSilverOre);
                                    }
                                    for (int i = 0; i < (int)(cStone + (cStone * appliedStoneLootFactor)); i++)
                                    {
                                        Drops.Add(objectStone);
                                    }
                                    for (int i = 0; i < (int)(cTinOre + (cTinOre * appliedStoneLootFactor)); i++)
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
                                    appliedStoneLootFactor = getAppliedHuntingLootFactor(appliedWoodLootFactor);

                                    for (int i = 0; i < (int)(cFeathers + (cFeathers * appliedHuntingLootFactor)); i++)
                                    {
                                        Drops.Add(objectFeathers);
                                    }
                                    for (int i = 0; i < (int)(cGuck + (cGuck * appliedHuntingLootFactor)); i++)
                                    {
                                        Drops.Add(objectGuck);
                                    }
                                    for (int i = 0; i < (int)(cLeatherScraps + (cLeatherScraps * appliedHuntingLootFactor)); i++)
                                    {
                                        Drops.Add(objectLeatherScraps);
                                    }
                                    for (int i = 0; i < (int)(cWitheredBone + (cWitheredBone * appliedHuntingLootFactor)); i++)
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
                            }

                            __result = Drops;

                            if (advancedLogging)
                            {
                                String dropStr = "";
                                foreach (GameObject drop in __result)
                                {
                                    dropStr += "'" + drop.name + "',";
                                }
                                Utilities.Log("Returning Drops: " + dropStr);
                            }
                        }
                    }
                }
            }

            private class DropObjectContainer
            {
                public int NumItems { set; get; }

                public DropObjectAssocinatedSkillType SkillType { set; get; }

                public GameObject DropItem { set; get; }

                public DropObjectContainer(int numItems, DropObjectAssocinatedSkillType skillType, GameObject dropItem)
                {
                    NumItems = numItems;
                    SkillType = skillType;
                    DropItem = dropItem;
                }
            }

            private enum DropObjectAssocinatedSkillType
            {
                Woodworking,
                Pickaxe,
                Hunting
            }
        }

        //Old version

        /*[HarmonyPatch(typeof(MineRock5), nameof(MineRock5.Damage))]
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

                        /*Utilities.LogWarning("Nombre: " + __instance.m_name);
                        Utilities.LogWarning("Cantidad de rocas: " + __instance.m_hitAreas.Count);
                        Utilities.LogWarning("Chance: " + __instance.m_dropItems.m_dropChance);
                        Utilities.LogWarning("Min: " + __instance.m_dropItems.m_dropMin);
                        Utilities.LogWarning("Max: " + __instance.m_dropItems.m_dropMax);
                    }
                }



            }
        }*/
    }
}
