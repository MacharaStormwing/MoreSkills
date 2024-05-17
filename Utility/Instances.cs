using HarmonyLib;
using System.Reflection;
using System.Runtime.Remoting.Metadata.W3cXsd2001;

namespace MoreSkills.Utility
{
    public class MoreSkills_Instances
    {

        public static Player _player;
        public static ObjectDB _objectDB;
        public static InventoryGui _inventoryGui;
        public static ZDOID _CDAttacker;
        public static ZDOID _DDAttacker;
        public static ZDOID _MR5DAttacker;
        public static ZDOID _TBDAttacker;
        public static ZDOID _TLDAttacker;

        // currently unused apparently
        private static Vagon _cart;
        private static Inventory _inventory;
        private static ItemDrop _itemDrop;
        private static ZNetView _zNetView;
        private static MineRock5 _mineRock5;
        private static Character _CDamage;
        private static Destructible _DDamage;
        private static MineRock5 _MR5Damage;
        private static TreeBase _TBDamage;
        private static TreeLog _TLDamage;

        [HarmonyPatch(typeof(Vagon), nameof(Vagon.UpdateMass))]
        public static class SI_Vagon
        {
            public static void Postfix(ref Vagon __instance)
            {
                if (__instance != null)
                    _cart = __instance;
            }
        }

        [HarmonyPatch]
        public static class SI_Player
        {
            public static MethodBase TargetMethod()
            {
                return AccessTools.DeclaredMethod(typeof(Player), nameof(Player.UpdateStats), new System.Type[0]);
            }

            public static void Postfix(ref Player __instance)
            {
                if (__instance != null)
                    _player = __instance;
            }
        }

        [HarmonyPatch(typeof(Inventory), nameof(Inventory.UpdateTotalWeight))]
        public static class SI_Inventory
        {
            public static void Postfix(ref Inventory __instance)
            {
                if (__instance != null)
                    _inventory = __instance;
            }
        }

        [HarmonyPatch(typeof(ItemDrop), nameof(ItemDrop.SlowUpdate))]
        public static class SI_ItemDrop
        {
            public static void Postfix(ref ItemDrop __instance)
            {
                if (__instance != null)
                    _itemDrop = __instance;
            }
        }

        [HarmonyPatch(typeof(ObjectDB), nameof(ObjectDB.Awake))]
        public static class SI_ObjectDB
        {
            public static void Postfix(ref ObjectDB __instance)
            {
                if (__instance != null)
                    _objectDB = __instance;
            }
        }

        [HarmonyPatch(typeof(InventoryGui), nameof(InventoryGui.Awake))]
        public static class SI_InventoryGui
        {
            public static void Postfix(ref InventoryGui __instance)
            {
                if (__instance != null)
                    _inventoryGui = __instance;
            }
        }

        [HarmonyPatch(typeof(ZNetView), nameof(ZNetView.Awake))]
        public static class SI_ZNetView
        {
            public static void Postfix(ref ZNetView __instance)
            {
                if (__instance != null)
                    _zNetView = __instance;
            }
        }

        /**
         *  Changed with Version 0.3.0 (Valheim 0.218.15)
         *  Using MineRock5.Awake instead of MineRock5.Start since the latter does no longer exist
         * This and other instances do not seem to be used however.
         */
        [HarmonyPatch(typeof(MineRock5), nameof(MineRock5.Awake))]
        public static class SI_MineRock5
        {
            public static void Postfix(ref MineRock5 __instance)
            {
                _mineRock5 = __instance;
            }
        }

        [HarmonyPatch(typeof(Character), nameof(Character.Damage))]
        public static class SI_CDamage
        {
            public static void Postfix(ref Character __instance, HitData hit)
            {
                if (MoreSkills_Instances._player != null)
                {
                    _CDamage = __instance;

                    _CDAttacker = hit.m_attacker;
                }
            }
        }

        [HarmonyPatch(typeof(Destructible), nameof(Destructible.Damage))]
        public static class Si_DDamage
        {
            public static void Postfix(ref Destructible __instance, HitData hit)
            {
                if (MoreSkills_Instances._player != null)
                {
                    _DDamage = __instance;

                    _DDAttacker = hit.m_attacker;
                }
            }
        }

        [HarmonyPatch(typeof(MineRock5), nameof(MineRock5.Damage))]
        public static class SI_MR5Damage
        {
            public static void Postfix(ref MineRock5 __instance, HitData hit)
            {
                if (MoreSkills_Instances._player != null)
                {
                    _MR5Damage = __instance;

                    _MR5DAttacker = hit.m_attacker;
                }
            }
        }

        [HarmonyPatch(typeof(TreeBase), nameof(TreeBase.Damage))]
        public static class SI_TBDamage
        {
            public static void Postfix(ref TreeBase __instance, HitData hit)
            {
                if (MoreSkills_Instances._player != null)
                {
                    _TBDamage = __instance;

                    _TBDAttacker = hit.m_attacker;
                }
            }
        }

        [HarmonyPatch(typeof(TreeLog), nameof(TreeLog.Damage))]
        public static class SI_TLDamage
        {
            public static void Postfix(ref TreeLog __instance, HitData hit)
            {
                if (MoreSkills_Instances._player != null)
                {
                    _TLDamage = __instance;

                    _TLDAttacker = hit.m_attacker;
                }
            }
        }
    }
}
