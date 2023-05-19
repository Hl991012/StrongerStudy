using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class TestStream : MonoBehaviour
{
    private Test1 Test1 = new();
    
    void Start()
    {

        Application.runInBackground = true;
        // using var fileStream = new FileStream("C:\\Users\\10573\\Desktop\\Test.txt", FileMode.OpenOrCreate);
        // // using var streamWriter = new StreamWriter(fileStream);
        // // streamWriter.WriteLine("1111111111");
        // // streamWriter.WriteLine("22222222222");
        //
        // // using var streamReader = new StreamReader(fileStream);
        // // Debug.LogError(streamReader.CurrentEncoding);
        // // Debug.LogError(streamReader.ReadToEnd());
        // // Debug.LogError(streamReader.ReadLine());
        // // Debug.LogError(streamReader.ReadLine());
        //
        // using BinaryReader binaryReader = new BinaryReader(fileStream);
        // var text = binaryReader.ReadBytes(binaryReader.ReadByte());
        // Debug.LogError(System.Text.Encoding.UTF8.GetString(text));

        // int a = 10;
        // int b = 0;
        // int c;
        // try
        // {
        //     c = a / b;
        // }
        // catch (Exception e)
        // {
        //     Debug.LogError(e.Message);
        //     //throw;
        // }
        // finally
        // {
        //     if (b == 0)
        //     {
        //         Debug.LogError("11111");
        //     }
        // }
        Test1.T();
    }
    
    private void Obsolete()
    {
        Debug.LogError("22222");
    }
}

[AttributeUsage(AttributeTargets.Method, Inherited = false)]
public class Test1 : Attribute
{
    public Test1()
    {
        
    }
    
    public void T()
    {
        Debug.LogError(111);
    }
}

public class Test2 : Test1
{
}
