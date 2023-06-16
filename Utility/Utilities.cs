using BepInEx.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MoreSkills.Utility
{
    public class Utilities
    {
        internal static void TextAreaDrawer(ConfigEntryBase entry)
        {
            GUILayout.ExpandHeight(true);
            GUILayout.ExpandWidth(true);
            entry.BoxedValue = GUILayout.TextArea((string)entry.BoxedValue, GUILayout.ExpandWidth(true),
                GUILayout.ExpandHeight(true));
        }

        public static void Log(string message, [CallerMemberName] string method = null)
        {
            Debug.Log($"[MoreSkills].[{method ?? "null"}] {message}");
        }

        public static void LogWarning(string message, [CallerMemberName] string method = null)
        {
            Debug.LogWarning($"[MoreSkills].[{method ?? "null"}] {message}");
        }

        public static void LogError(string message, [CallerMemberName] string method = null)
        {
            Debug.LogError($"[MoreSkills].[{method ?? "null"}] {message}");
        }
    }
}
