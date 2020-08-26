using System;
using UnityEngine;

namespace LazySamurais.Common
{
    [CreateAssetMenu(fileName = CLog.FILE_NAME, menuName = "Scriptable Objects/Clog Filters", order = 1)]
    [Serializable]
    public class LogData : ScriptableObject
    {
        [Tooltip("Add filters for unity developer console here.")] [Header("[Log Key]---[Show Log?]")]
        public StringBoolDictionary Filters = new StringBoolDictionary();
    }

    [Serializable]
    public class StringBoolDictionary : SerializableDictionary<string, bool>
    {
    }
}