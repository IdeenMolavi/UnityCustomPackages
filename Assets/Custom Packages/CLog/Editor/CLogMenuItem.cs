using UnityEditor;

namespace Rohaum.Common.Debug.Editor
{
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
}