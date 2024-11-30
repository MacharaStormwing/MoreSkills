using System;
using System.Collections.Generic;
using HarmonyLib;
using MoreSkills.Config;
using MoreSkills.Utility;
using UnityEngine;

namespace MoreSkills.ModSkills
{
    class MoreSkills_Crafting
    {
        [HarmonyPatch(typeof(InventoryGui), nameof(InventoryGui.Show))]
        public static class CraftingSkill_InventoryShow
        {
            public static void Postfix()
            {
                if (MoreSkills_Instances._player != null)
                {
                    if (MoreSkills_CraftingConfig.EnableCraftingSkill.Value)
                    {
                        IGUI = true;

                        if (!Saved)
                        {
                            foreach (Recipe recipe in MoreSkills_Instances._objectDB.m_recipes)
                            {
                                foreach (Piece.Requirement req in recipe.m_resources)
                                {
                                    if (recipe == null || recipe.m_item == null)
                                        continue;

                                    string objnumitem = string.Concat(recipe.m_item.name + ":" + recipe.m_amount + ":" + req.m_resItem.name);
                                    float objnum = recipe.m_amount;
                                    float itemcnum = req.m_amount;
                                    float itemunum = req.m_amountPerLevel;
                                    var getcSaves = cSaves.Find(csaves => csaves.ObjNumItem == objnumitem);

                                    if (getcSaves.ObjNumItem != objnumitem)
                                    {
                                        cSaves.Add(new Helper.CraftingSaves(
                                            objnumitem: objnumitem,
                                            objnum: objnum,
                                            itemcnum: itemcnum,
                                            itemunum: itemunum));

                                        //LogWarning("Added to Temp Database: " + objnumitem);
                                    }


                                    //LogWarning("Objeto: " + recipe.m_item.name + " Cantidad: " + recipe.m_amount + " Recurso: " + req.m_resItem.name + " Cantidad Crafteo: " + req.m_amount + " Cantidad Upgradeo: " + req.m_amountPerLevel + " Que es?: " + req.m_recover);
                                }
                            }
                            Saved = true;
                            Utilities.Log("Saved All Objects in DataBase");
                        }

                        bool advancedLogging = MoreSkills_CraftingConfig.EnableDetailedLogging.Value;
                        float appliedItemMultiplier = CalculateAppliedItemMultiplier(advancedLogging, "Show (Crafting)");

                        foreach (Recipe recipe in MoreSkills_Instances._objectDB.m_recipes)
                        {
                            foreach (Piece.Requirement req in recipe.m_resources)
                            {
                                if (recipe == null || recipe.m_item == null)
                                    continue;

                                string objnumitem = string.Concat(recipe.m_item.name + ":" + recipe.m_amount + ":" + req.m_resItem.name);
                                var getcSaves = cSaves.Find(csaves => csaves.ObjNumItem == objnumitem);

                                if (getcSaves.ObjNumItem == objnumitem)
                                {

                                    if (req.m_amount >= 1)
                                    {
                                        req.m_amount = Mathf.RoundToInt(getcSaves.ItemCNum * appliedItemMultiplier);
                                        if (req.m_amount < 1)
                                            req.m_amount = 1;
                                    }
                                    if (req.m_amountPerLevel >= 1)
                                    {
                                        req.m_amountPerLevel = Mathf.RoundToInt(getcSaves.ItemUNum * appliedItemMultiplier);
                                        if (req.m_amountPerLevel < 1)
                                            req.m_amountPerLevel = 1; 
                                    }

                                    if (advancedLogging)
                                        Utilities.Log("Show (Crafting): Handling ObjNumItem '"
                                            + getcSaves.ObjNumItem + "': old amount: "
                                            + getcSaves.ItemCNum + " new amount: " + req.m_amount
                                            + " (amountPerLevel: " + req.m_amountPerLevel
                                            + " ) after applying item multiplier: " + appliedItemMultiplier);
                                }
                                else
                                {
                                    Utilities.LogWarning("Show (Crafting): Stored objnumitem '"
                                        + getcSaves.ObjNumItem + "' does not match expected objnumitm '"
                                        + objnumitem + "'");
                                }
                            }
                        }

                    }
                }
            }
        }
                        

        [HarmonyPatch(typeof(InventoryGui), nameof(InventoryGui.DoCrafting))]
        public static class CraftingSkillMod_SkillIncrease
        {
            public static void Postfix()
            {
                if (MoreSkills_Instances._player != null)
                {
                    if (MoreSkills_CraftingConfig.EnableCraftingSkill.Value)
                    {
                        bool advancedLogging = MoreSkills_CraftingConfig.EnableDetailedLogging.Value;
                        float appliedItemMultiplier = CalculateAppliedItemMultiplier(advancedLogging, "DoCrafting");

                        // TODO: this is very similar to what is done in Show, maybe generalize

                        foreach (Recipe recipe in MoreSkills_Instances._objectDB.m_recipes)
                        {
                            foreach (Piece.Requirement req in recipe.m_resources)
                            {
                                if (recipe == null || recipe.m_item == null)
                                    continue;

                                string objnumitem = string.Concat(recipe.m_item.name + ":" + recipe.m_amount + ":" + req.m_resItem.name);
                                var getcSaves = cSaves.Find(csaves => csaves.ObjNumItem == objnumitem);

                                if (getcSaves.ObjNumItem == objnumitem)
                                {
                                    
                                    if (req.m_amount >= 1)
                                    {
                                        req.m_amount = Mathf.RoundToInt(getcSaves.ItemCNum * appliedItemMultiplier);
                                        if (req.m_amount < 1)
                                            req.m_amount = 1;
                                    }
                                    if (req.m_amountPerLevel >= 1)
                                    {
                                        req.m_amountPerLevel = Mathf.RoundToInt(getcSaves.ItemUNum * appliedItemMultiplier);
                                        if (req.m_amountPerLevel < 1)
                                            req.m_amountPerLevel = 1;      
                                    }

                                    if (advancedLogging)
                                        Utilities.Log("DoCrafting: Handling ObjNumItem '"
                                        + getcSaves.ObjNumItem + "': old amount: "
                                        + getcSaves.ItemCNum + " new amount: " + req.m_amount
                                        + " (amountPerLevel: " + req.m_amountPerLevel
                                        + " ) after applying item multiplier: " + appliedItemMultiplier);
                                } else
                                {
                                    Utilities.LogWarning("DoCrafting: Stored objnumitem '"
                                        + getcSaves.ObjNumItem  + "' does not match expected objnumitm '"
                                        + objnumitem + "'");
                                }
                            }
                        }

                        int foundMatches = 0;
                        foreach (Recipe recipe in MoreSkills_Instances._objectDB.m_recipes)
                        {
                            if (recipe == null || recipe.m_item == null)
                                continue;

                            //if (recipe.m_item == MoreSkills_Instances._inventoryGui.m_selectedRecipe.Key.m_item)
                            if (recipe.m_item == MoreSkills_Instances._inventoryGui.m_selectedRecipe.Recipe.m_item)
                                {

                                foreach (Piece.Requirement req in recipe.m_resources)
                                {
                                    if (req == null || req.m_resItem == null)
                                        continue;

                                    CraftSkillInc += req.m_amount;
                                    CraftSkillInc += req.m_amountPerLevel;
                                    foundMatches++;
                                }
                            }
                        }

                        MoreSkills_Instances._player.RaiseSkill((Skills.SkillType)MoreSkills_CraftingConfig.CraftingSkill_Type, ((CraftSkillInc * MoreSkills_CraftingConfig.CraftingSkillIncreaseMultiplier.Value)) / 10);
                        Utilities.Log("Granted Crafting EXP: " + (CraftSkillInc * MoreSkills_CraftingConfig.CraftingSkillIncreaseMultiplier.Value)
                            + " found Recipe maches: " + foundMatches);
                        CraftSkillInc = 0;
                    }
                }
            }
        }

        public static bool IGUI;

        public static float CraftSkillInc;

        public static bool Saved;

        public static List<Helper.CraftingSaves> cSaves = new();

        private static float CalculateAppliedItemMultiplier(bool advancedLogging, string caltculatedAt)
        {
            // calculation of the item multiplication factor based on skill level,
            // apply multiplier factor if below middle level, apply divider factor if above
            float skillLevel = MoreSkills_Instances._player.GetSkillFactor((Skills.SkillType)MoreSkills_CraftingConfig.CraftingSkill_Type);
            // convert from values between 0 and 100 to 0 and 1
            // values below 0 are handled as 0, values above 100 are handled as 100
            float middle = (float) MoreSkills_CraftingConfig.CraftingMiddleLevel.Value / 100;
            if (middle < 0f)
            {
                middle = 0f;
            } else if (middle > 1f)
            {
                middle = 1f;
            }

            float appliedItemMultiplier;
            if (skillLevel < middle)
            {
                float multiplier = MoreSkills_CraftingConfig.CraftingLevelMultiplier.Value;

                appliedItemMultiplier = multiplier - ((skillLevel * (1f / middle)) * (multiplier - 1f));

                if (advancedLogging)
                    Utilities.Log(caltculatedAt + ": formula: appliedItemMultiplier = CraftingLevelMultiplier - ((SkillLevel * (1 / CraftingMiddleLevel)) * (CraftingLevelMultiplier - 1)) where configured middle (0-100) was divided by 100 to get 0.0-1.0");
                    Utilities.Log(caltculatedAt + ": formula with values: " + appliedItemMultiplier + " = " + multiplier + " - ((" + skillLevel + " * (1 / " + middle + ")) * (" + multiplier + " - 1))");
                    Utilities.Log(caltculatedAt + ": calculated appliedItemMultiplier "
                        + appliedItemMultiplier + " using crafing skill level " + skillLevel
                        + " and multiplier " + multiplier + " ) because skill is below " + middle);
            }
            else
            {
                float divider = MoreSkills_CraftingConfig.CraftingLevelDivider.Value;

                float appliedItemDivider = 1f + ((skillLevel - middle) * (1f / (1f - middle)) * (divider - 1f));
                appliedItemMultiplier = 1f / appliedItemDivider;

                if (advancedLogging)
                    Utilities.Log(caltculatedAt + ": formula: appliedItemDivider = 1 + ((SkillLevel - CraftingMiddleLevel) * (1 / (1 - CraftingMiddleLevel)) * (CraftingLevelDivider - 1)) where configured middle (0-100) was divided by 100 to get 0.0-1.0");
                    Utilities.Log(caltculatedAt + ": formula with values: " + appliedItemDivider + " = 1 + ((" + skillLevel + " - " + middle + ") * (1 / (1 - " + middle + ")) * (" + divider + " - 1))");

                    Utilities.Log(caltculatedAt + ": calculated appliedItemMultiplier "
                        + appliedItemMultiplier + " (based on '1 / appliedItemDivider': " + appliedItemDivider + ")"
                        + " using crafing skill level " + skillLevel
                        + " and divider " + divider + " because skill is aboveequal " + middle);
            }

            return appliedItemMultiplier;
        }

        
    }

}
