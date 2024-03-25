using UnityEditor.Scripting.Python;
using UnityEditor;
using UnityEngine;
using static System.Net.Mime.MediaTypeNames;

public class EnsureNaming
{
    [MenuItem("MyPythonScripts/Ensure Naming")]
    static void RunEnsureNaming()
    {
        PythonRunner.RunFile($"{Application.dataPath}/ensure_naming.py");
    }
}
