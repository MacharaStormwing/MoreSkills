﻿using System.IO;
using UnityEngine;
using UnityEngine.UI;
using MoreSkills.Utility;
using MoreSkills.Config;

namespace MoreSkills.UI
{
    static class SkillIcons
    {
        public static Sprite VitalityIcon;
        public static Sprite Load_VitalityIcon()
        {
            Stream iconVitality = EmbeddedAssets.LoadAssets("Assets.Icons.vitalityicon.png");
            Texture2D Vitalitytexture2D = Helper.LoadPng(iconVitality);
            VitalityIcon = Sprite.Create(Vitalitytexture2D, new Rect(0f, 0f, 32f, 32f), Vector2.zero);
            iconVitality.Dispose();
            return VitalityIcon;
        }

        public static Sprite StrengthIcon;
        public static Sprite Load_StrengthIcon()
        {
            Stream iconStrength = EmbeddedAssets.LoadAssets("Assets.Icons.strengthicon.png");
            Texture2D Strengthtexture2D = Helper.LoadPng(iconStrength);
            StrengthIcon = Sprite.Create(Strengthtexture2D, new Rect(0f, 0f, 32f, 32f), Vector2.zero);
            iconStrength.Dispose();
            return StrengthIcon;
        }

        public static Sprite SailingIcon;

        public static Sprite Load_SailingIcon()
        {
            Stream iconSailing = EmbeddedAssets.LoadAssets("Assets.Icons.sailingicon.png");
            Texture2D Sailingtexture2D = Helper.LoadPng(iconSailing);
            SailingIcon = Sprite.Create(Sailingtexture2D, new Rect(0f, 0f, 32f, 32f), Vector2.zero);
            iconSailing.Dispose();
            return SailingIcon;
        }

        public static Sprite CraftingIcon;

        public static Sprite Load_CraftingIcon()
        {
            Stream iconCrafting = EmbeddedAssets.LoadAssets("Assets.Icons.craftingicon.png");
            Texture2D Craftingtexture2D = Helper.LoadPng(iconCrafting);
            CraftingIcon = Sprite.Create(Craftingtexture2D, new Rect(0f, 0f, 32f, 32f), Vector2.zero);
            iconCrafting.Dispose();
            return CraftingIcon;
        }

        public static Sprite HuntingIcon;

        public static Sprite Load_HuntingIcon()
        {
            Stream iconHunting = EmbeddedAssets.LoadAssets("Assets.Icons.huntingicon.png");
            Texture2D Huntingtexture2D = Helper.LoadPng(iconHunting);
            HuntingIcon = Sprite.Create(Huntingtexture2D, new Rect(0f, 0f, 32f, 32f), Vector2.zero);
            iconHunting.Dispose();
            return HuntingIcon;
        }
    }
}
