using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AniTest : MonoBehaviour
{
    public Transform lunpan;

    public float rotateSpeed = 10;
    
    void Update()
    {
        lunpan.rotation *= Quaternion.Euler(0, 0, rotateSpeed* Time.deltaTime) ;
    }
}
