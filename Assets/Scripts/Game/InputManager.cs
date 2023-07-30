using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{
    public bool InputDir;

    public Action<bool> OnInputDirStateChanged;

    public Action OnClickSpitBtn;

    public Vector2 dir = Vector2.zero;
}
