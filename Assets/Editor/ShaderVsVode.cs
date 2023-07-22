using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class ShaderVsVode
{
    [MenuItem("Tools/启用VSCode编辑Shader文件")]
    public static void OpenVSCode()
    {
        UnityEditor.Compilation.CompilationPipeline.RequestScriptCompilation();
    }
}