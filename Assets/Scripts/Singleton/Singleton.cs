using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> where T : new()
{
    private static T instance;

    protected Singleton(){ }
    
    private static readonly Object lockObj = new Object();

    public static T Instance
    {
        get
        {
            lock (lockObj)
            {
                return instance ?? new T();
            }
            
        }
    }
}   
