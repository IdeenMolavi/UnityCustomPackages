using System;
using System.Diagnostics;
using UnityEditor;
using UnityEngine;
using UDebug = UnityEngine.Debug;
using UObject = UnityEngine.Object;

namespace LazySamurais.Common
{
    public static class CLog
    {
        public const string FILE_NAME = "LogFilters";
        private const string CONDITION_1 = "DEVELOPMENT_BUILD";
        private const string CONDITION_2 = "UNITY_EDITOR";
        private const string Default = "Default";
        private static LogData _data;

        [Conditional(CONDITION_1)]
        [Conditional(CONDITION_2)]
        public static void Log(object message, string filter = Default, UObject sender = null)
        {
            if (!_IsEnable(filter)) return;
            string senderName = sender != null
                ? $" | <color=lime>{sender.name}</color> | <color=lime>{sender.GetInstanceID()}</color> "
                : string.Empty;

            var outputMessage = $"<b><color=blue>{filter}</color>{senderName} | {message}</b>";

            UDebug.Log(outputMessage);
        }

        [Conditional(CONDITION_1)]
        [Conditional(CONDITION_2)]
        public static void Warning(object message, string filter = Default, UObject sender = null)
        {
            if (!_IsEnable(filter)) return;
            string senderName = sender != null
                ? $" | <color=yellow>{sender.name}</color> | <color=yellow>{sender.GetInstanceID()}</color> "
                : string.Empty;

            var outputMessage = $"<b><color=yellow>{filter}</color>{senderName} | {message}</b>";

            UDebug.LogWarning(outputMessage);
        }

        [Conditional(CONDITION_1)]
        [Conditional(CONDITION_2)]
        public static void Error(object message, string filter = Default, UObject sender = null)
        {
            if (!_IsEnable(filter)) return;
            string senderName = sender != null
                ? $" | <color=red>{sender.name}</color> | <color=red>{sender.GetInstanceID()}</color> "
                : string.Empty;

            var outputMessage = $"<b><color=red>{filter}</color>{senderName} | {message}</b>";

            UDebug.LogError(outputMessage);
        }

        [Conditional(CONDITION_1)]
        [Conditional(CONDITION_2)]
        public static void Exception(object message, string filter = Default, UObject sender = null)
        {
            if (!_IsEnable(filter)) return;
            string senderName = sender != null
                ? $" | <color=red>{sender.name}</color> | <color=red>{sender.GetInstanceID()}</color> "
                : string.Empty;

            var outputMessage =
                $"<b><color=red> Exception | {filter}</color>{senderName} | <color=red>{DateTime.Now}</color> | {message}</b>";

            UDebug.LogError(outputMessage);
        }

        [Conditional(CONDITION_1)]
        [Conditional(CONDITION_2)]
        public static void Assert(bool condition, object message, string filter = Default, UObject sender = null)
        {
            if (!_IsEnable(filter) || condition) return;
            string senderName = sender != null
                ? $" | <color=red>{sender.name}</color> | <color=red>{sender.GetInstanceID()}</color> "
                : string.Empty;

            var outputMessage =
                $"<b><color=red> Exception | {filter}</color>{senderName} | <color=red>{DateTime.Now}</color> | {message}</b>";

            UDebug.Assert(condition, outputMessage);
        }

        private static void _LoadData()
        {
            if (_data == null) _data = LoadDataFile();
#if UNITY_EDITOR
            if (_data == null)
            {
                CreateDataFile();
                _data = LoadDataFile();
            }
#endif
        }

        private static bool _IsEnable(string filter)
        {
            if (filter == Default) return true;
            _LoadData();
            var result = true;
            if (_data != null)
                if (_data.Filters.TryGetValue(filter, out result) == false) //Add new filter if it does not exist.
                {
                    result = true;
                    _data.Filters[filter] = true;
                }

            return result;
        }
        
#if UNITY_EDITOR
        public static void CreateDataFile()
        {
            if (AssetDatabase.IsValidFolder("Assets/Resources"))
            {
                AssetDatabase.CreateAsset(ScriptableObject.CreateInstance(typeof(LogData)),
                    $"Assets/Resources/{FILE_NAME}.asset");
            }
            else
            {
                AssetDatabase.CreateFolder("Assets", "Resources");
                AssetDatabase.CreateAsset(ScriptableObject.CreateInstance(typeof(LogData)),
                    $"Assets/Resources/{FILE_NAME}.asset");
            }
        }

        public static LogData LoadDataFile()
        {
            return Resources.Load<LogData>(FILE_NAME);
        }
#endif
    }
}