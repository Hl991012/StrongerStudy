using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(DefaultConfigSo), menuName = "SO/" + nameof(DefaultConfigSo))]
public class DefaultConfigSo : ScriptableObject
{
    public string defaultMainWeaponId = "HeavyMachineGuns";
}
