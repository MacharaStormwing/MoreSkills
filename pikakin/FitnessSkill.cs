using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using MoreSkills.Config;
using MoreSkills.Utility;
using Pipakin.SkillInjectorMod;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;

namespace Pipakin.FitnessSkillMod
{
    [BepInPlugin("com.pipakin.FitnessSkillMod", "FitnessSkillMod", "2.0.1")]
    [BepInDependency("pfhoenix.modconfigenforcer", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("com.pipakin.SkillInjectorMod")]
    public class FitnessSkill : BaseUnityPlugin
    {
        const string MOD_ID = "com.pipakin.FitnessSkillMod";
        private readonly Harmony harmony = new Harmony(MOD_ID);

        private static FitnessConfig fitnessConfig = new FitnessConfig();
        private static Dictionary<string, Texture2D> cachedTextures = new Dictionary<string, Texture2D>();

        //hopefully this doesn't conflict.
        public const int SKILL_TYPE = 243;

        private static string iconPath = "Assets.Icons.";

        private static string iconFileName = "fitness.png";

        public static Sprite FitnessIcon;
        public static Sprite Load_FitnessIcon()
        {
            try {
                Stream iconFitness = EmbeddedAssets.LoadAssets(iconPath + iconFileName);
                Texture2D FitnessTexture2D = Helper.LoadPng(iconFitness);
                FitnessIcon = Sprite.Create(FitnessTexture2D, new Rect(0f, 0f, 32f, 32f), Vector2.zero);
                iconFitness.Dispose();
            } catch (Exception e) {
                Debug.LogError("Unable to load skill icon '" + iconFileName + "': " + e.Message);
            }
            return FitnessIcon;
        }

        void Awake()
        {
            fitnessConfig.InitConfig(MOD_ID, Config);

            harmony.PatchAll(typeof(ApplyFitnessEffects));
            harmony.PatchAll(typeof(ApplyFitnessRegen));
            harmony.PatchAll(typeof(IncreaseFitnessSkill));

            if (fitnessConfig.SkillEnabled)
            {
                SkillInjector.RegisterNewSkill(SKILL_TYPE, "Fitness", "Affects maximum stamina level", 1.0f, Load_FitnessIcon(), Skills.SkillType.Run);
            }
        }

        [HarmonyPatch(typeof(Player), nameof(Player.SetMaxStamina))]
        public static class ApplyFitnessEffects
        {

            [HarmonyPrefix]
            public static void Prefix(ref float stamina, Player __instance)
            {
                if (fitnessConfig.SkillEnabled)
                {
                    try
                    {
                        //adjust the amount by the multiplier
                        var factor = __instance.GetSkillFactor((Skills.SkillType)SKILL_TYPE);

                        var amount = (float)Math.Ceiling(factor * (fitnessConfig.MaxStaminaMultiplier - 1.0f) * fitnessConfig.BaseStamina);

                        stamina += amount + fitnessConfig.BaseStamina - 75f; //offset from base of 75.
                    }
                    catch (Exception e)
                    {
                        Debug.LogError("Error adusting base stamina: " + e.ToString());
                    }
                }
            }
        }

        [HarmonyPatch]
        public static class ApplyFitnessRegen
        {

            public static MethodBase TargetMethod()
            {
                return AccessTools.DeclaredMethod(typeof(Player), nameof(Player.UpdateStats), new System.Type[0]);
            }

            [HarmonyPrefix]
            public static void Prefix(Player __instance, ref float ___m_staminaRegen)
            {
                if (fitnessConfig.SkillEnabled)
                {
                    try
                    {
                        //adjust the amount by the multiplier
                        var factor = __instance.GetSkillFactor((Skills.SkillType)SKILL_TYPE);
                        var amount = (float)Math.Ceiling((factor * (fitnessConfig.RegenStaminaMultiplier - 1.0f) + 1.0f) * fitnessConfig.BaseStaminaRegen);

                        ___m_staminaRegen = amount;
                    }
                    catch (Exception e)
                    {
                        Debug.LogError("Error adusting stamina regen: " + e.ToString());
                    }
                }
            }
        }

        [HarmonyPatch(typeof(Player), nameof(Player.RPC_UseStamina))]
        public static class IncreaseFitnessSkill
        {

            [HarmonyPrefix]
            public static void Prefix(float v, Player __instance, ZNetView ___m_nview)
            {
                if (fitnessConfig.SkillEnabled)
                {
                    try
                    {
                        var progress = ___m_nview.GetZDO().GetFloat("fitness_progress", 0f);
                        //adjust the amount by the multiplier
                        progress += v;

                        if (progress > fitnessConfig.SkillGainIncrement)
                        {
                            var ratio = progress / __instance.GetMaxStamina();
                            __instance.RaiseSkill((Skills.SkillType)SKILL_TYPE, ratio * fitnessConfig.SkillIncrease);
                            ___m_nview.GetZDO().Set("fitness_progress", 0f);
                        }
                        else
                        {
                            ___m_nview.GetZDO().Set("fitness_progress", progress);
                        }
                    }
                    catch (Exception e)
                    {
                        Debug.LogError("Error increasing fitness skill: " + e.ToString());
                    }
                }
            }
        }
    }
}
