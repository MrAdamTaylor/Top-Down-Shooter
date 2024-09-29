using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

[CreateAssetMenu(fileName = "UIWeaponIcons", menuName = "UI/weaponICO")]
public class UIWeaponStaticDataIcons : ScriptableObject
{
    public List<WeaponUIConfig> IcoConfigs;
}


[Serializable]
public struct WeaponUIConfig
{
    public Sprite WeaponPicture;
    public WeaponType WeaponType;
    public AnimationConfigs AnimationConfigs;
}

[Serializable]
public struct AnimationConfigs
{
    public AnimationSequenceType AnimationSequenceType;
    [Header("Duration if middle value is Active")]
    public float MiddleDuration;
    public ElementAnimationConfigs ImageConfigs;
    public ElementAnimationConfigs TextConfigs;
}

[Serializable]
public struct ElementAnimationConfigs
{
    public AnimationType AnimationType;
    public TweenConfigs StartConfigs;
    public TweenConfigs EndConfigs;
}

[Serializable]
public struct TweenConfigs
{
    [Header("Don't have change in End Configs if animation type is Shaking")]
    public Vector3 Scale;
    [Space]
    public float RillRate;
    public Ease EaseType;
}

public enum AnimationType
{
    Shaking,
    Scale
}

public enum AnimationSequenceType
{
    Start_Middle_End,
    StartMiddle_End,
    Start_MiddleEnd,
    Start_End
}


