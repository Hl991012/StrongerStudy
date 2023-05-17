using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(LevelConfigSo), menuName = "SO/" + nameof(LevelConfigSo), order = 0)]
public class LevelConfigSo : ScriptableObject
{
    public string levelId;
    
}
