using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TestEditor : Editor
{
    [InitializeOnLoadMethod]
    public static void Test()
    {
        Debug.Log("Editor Startup");
    }
}
