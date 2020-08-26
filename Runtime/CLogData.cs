using System;
using UnityEngine;

namespace LazySamurais.Common
{
    [CreateAssetMenu(fileName = "LogData", menuName = "Scriptable Objects/Log Data", order = 1)]
    [Serializable]
    public class CLogData : ScriptableObject
    {
        [Tooltip("Add filters for unity developer console here.")]
        [Header("[Log Key]---[Show Log?]")]
        public StringBoolDictionary Filters = new StringBoolDictionary();
    }

    [Serializable]
    public class StringBoolDictionary : SerializableDictionary<string, bool> { }
}