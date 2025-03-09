using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
[CreateAssetMenu(fileName = "Lighting Preset",menuName = "Scriptable Objects/Lighting Preset", order = 1)]
public class Lighting : ScriptableObject
{
    public Gradient AmbientColor;
    public Gradient DirectionalColor;
    public Gradient FogColor;
    // Start is called before the first frame update

}
