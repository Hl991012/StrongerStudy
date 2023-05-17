using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class SingletonMono<T> where T : Component
{
    private static T instance;

    protected SingletonMono() { }
    
    private static readonly Object lockObj = new Object();

    public static T Instance
    {
        get
        {
            lock (lockObj)
            {
                if (instance == null)
                {
                    instance = Object.FindObjectOfType(typeof(T)).GetComponent<T>();
                }

                if (instance == null)
                {
                    instance = new GameObject().AddComponent<T>();
                }

                return instance;   
            }
        }
    }
}   
