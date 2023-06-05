using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonTest : Image
{
    public override bool IsRaycastLocationValid(Vector2 screenPoint, Camera eventCamera)
    {
        return GetComponent<Collider2D>().OverlapPoint(screenPoint);
    }
}


