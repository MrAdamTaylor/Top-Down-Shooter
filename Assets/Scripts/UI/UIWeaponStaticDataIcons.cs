using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "UIWeaponIcons", menuName = "UI/weaponICO")]
public class UIWeaponStaticDataIcons : ScriptableObject
{
    public List<WeaponUIConfig> IcoConfigs;
}

[System.Serializable]
public struct WeaponUIConfig
{
    public Sprite WeaponPicture;
    public WeaponType WeaponType;
}
