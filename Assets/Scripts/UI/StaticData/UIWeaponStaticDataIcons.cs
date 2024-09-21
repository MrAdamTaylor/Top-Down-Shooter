using System;
using System.Collections.Generic;
using UnityEngine;

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

    public ImageTweenConfigs ImageGonfigs;
    public TextTweenConfigs StartScale;
    public TextTweenConfigs EndScale;
}

[System.Serializable]
public struct TextTweenConfigs
{
    public Vector3 Scale;
    public float RillRate;
}

public enum ShakingPictureType
{
    TopDown,
    LeftRight,
    Scale2x
}

[Serializable]
public struct ImageTweenConfigs
{
    ShakingPictureType Type;
    
    
}
