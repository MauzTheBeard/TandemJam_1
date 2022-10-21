using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GizmosManager : EditorWindow
{
    [MenuItem("Tools/Gizmos Manager")]
    static void Init()
    {
        // Get existing open window or if none, make a new one:
        GizmosManager window = (GizmosManager)EditorWindow.GetWindow(typeof(GizmosManager));
        window.Show();
    }
}
