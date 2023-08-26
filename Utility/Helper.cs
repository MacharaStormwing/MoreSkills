using System.IO;
using UnityEngine;
using HarmonyLib;
using System.Dynamic;
using System;

namespace MoreSkills.Utility
{
    public static class Helper
    {
        public static Texture2D LoadPng(Stream fileStream)
        {
            Texture2D texture = null;
            if (fileStream != null)
            {
                using (var memoStream = new MemoryStream())
                {
                    fileStream.CopyTo(memoStream);
                    texture = new Texture2D(2, 2);
                    texture.LoadImage(memoStream.ToArray()); //This will auto-resize the texture dimensions.
                }
            }
            return texture;
        }

        public struct HuntingDrops
        {
            public string CreaturePrefab { get; }
            public int Max { get; }
            public int Min { get; }
            public float Chance { get; }

            public HuntingDrops(string creatureprefab, int max, int min, float chance)
            {
                CreaturePrefab = creatureprefab;
                Max = max;
                Min = min;
                Chance = chance;
            }
        }
        public struct TamingSaves
        {
            public string CreatureZDOID { get; }
            public ZDOID TamerID { get; }
            public float TameTime { get; }
            public float EatTime { get; }
            public float MasterLevel { get; }
            public float TamerLevel { get; }
            public float UnlockLevel { get; }

            public TamingSaves(string creatureZDOID, ZDOID tamerid, float tametime, float eattime, float unlockLevel, float tamerLevel, float masterLevel)
            {
                CreatureZDOID = creatureZDOID;
                TamerID = tamerid;
                TameTime = tametime;
                EatTime = eattime;
                UnlockLevel = unlockLevel;
                TamerLevel = tamerLevel;
                MasterLevel = masterLevel;
            }
        }
        public struct TamingLevels
        {
            public string CreatureName { get; }
            public float MasterLevel { get; }
            public float TamerLevel { get; }
            public float UnlockLevel { get; }
            public float TameTime { get; }

            public TamingLevels(string creatureName, float masterLevel, float tamerLevel, float unlockLevel, float tametime)
            {
                CreatureName = creatureName;
                MasterLevel = masterLevel;
                TamerLevel = tamerLevel;
                UnlockLevel = unlockLevel;
                TameTime = tametime;
            }
        }

        public struct CreatureTameLevels
        {
            public string CreatureName { get; }
            
            // minimum skill level required to tamke the creature
            public float UnlockLevel { get; }

            // skill level at which the tamer becomes good at taming the creature
            public float TamerLevel { get; }

            // skill level at which the tamer is a master at taming the creature
            public float MasterLevel { get; }

            public CreatureTameLevels(string creatureNamel, float unlockLevel, float tamerLevel, float masterLevel)
            {
                CreatureName = creatureNamel;
                UnlockLevel = unlockLevel;
                TamerLevel = tamerLevel;
                MasterLevel = masterLevel;
            }
        }

        public struct TamingFix
        {
            public string CreatureZDOID { get; }
            public bool Fixed { get; }

            public TamingFix(string creaturezdoid, bool tfixed)
            {
                CreatureZDOID = creaturezdoid;

                Fixed = tfixed;
            }
        }
        public struct TamingGotFar
        {
            public string CreatureZDOID { get; }
            public bool GotFar { get; }

            public TamingGotFar(string creaturezdoid, bool gotfar)
            {
                CreatureZDOID = creaturezdoid;

                GotFar = gotfar;
            }
        }
        public struct TamingDoubleCheck
        {
            public string CreatureZDOID { get; }
            public int DoubleCheck { get; }

            public TamingDoubleCheck(string creaturezdoid, int doublecheck)
            {
                CreatureZDOID = creaturezdoid;

                DoubleCheck = doublecheck;
            }
        }
        public struct CraftingSaves
        {
            public string ObjNumItem { get; }
            public float ObjNum { get; }
            public float ItemCNum { get; }
            public float ItemUNum { get; }

            public CraftingSaves(string objnumitem, float objnum, float itemcnum, float itemunum)
            {
                ObjNumItem = objnumitem;
                ObjNum = objnum;
                ItemCNum = itemcnum;
                ItemUNum = itemunum;
            }
        }
        public struct FishStats
        {
            public string FishName { get; }
            public float BHookChance { get; }
            public int ItemStackSize { get; }
            public float StaminaUse { get; }
            public FishStats(string fishname, float bhookchance, int itemstacksize, float staminause)
            {
                FishName = fishname;
                BHookChance = bhookchance;
                ItemStackSize = itemstacksize;
                StaminaUse = staminause;
            }
        }
        public struct FishSizes
        {
            public string FishNameZDOID { get; }
            public Vector3 FishSize { get; }
            public float FishHeight { get; }
            public float FishSpeed { get; }
            public FishSizes(string fishnamezdoid, Vector3 fishsize, float fishheight, float fishspeed)
            {
                FishNameZDOID = fishnamezdoid;
                FishSize = fishsize;
                FishHeight = fishheight;
                FishSpeed = fishspeed;
            }
        }
    }
}
