using LazySamurais.Common;
using UnityEditor;
using UnityEngine;

public class CLogMenuItem : EditorWindow
{
    [MenuItem("Tools/Show CLog Filters")]
    static void ShowClogData()
    {
        LogData dataFile = CLog.LoadDataFile();
        if (dataFile == null)
        {
            CLog.CreateDataFile();
            dataFile = CLog.LoadDataFile();
        }

        Selection.activeObject = dataFile;
    }
}