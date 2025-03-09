using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteAlways]
public class LightingManager : MonoBehaviour
{
    [SerializeField] private Light DirectionalLight;

    [SerializeField] private Lighting Preset;
    [SerializeField, Range(0, 24)] private float TimeOfDay=13f;

    [SerializeField] private float timeScale = 1f; // Чем меньше значение, тем медленнее смена суток

    private void Update()
    {
        if (Preset == null)
            return;
        if (Application.isPlaying)
        {
            TimeOfDay += Time.deltaTime * timeScale;
            TimeOfDay %= 24;
            UpdateLighting(TimeOfDay / 24f);
        }
        else
        {
            UpdateLighting(TimeOfDay / 24f);
        }
    }


    private void UpdateLighting(float timePercent)
    {
        RenderSettings.ambientLight=Preset.AmbientColor.Evaluate(timePercent);
        RenderSettings.fogColor=Preset.FogColor.Evaluate(timePercent);
        if (DirectionalLight != null)
        {
            DirectionalLight.color=Preset.DirectionalColor.Evaluate(timePercent);
            DirectionalLight.transform.localRotation=Quaternion.Euler(new Vector3((timePercent*360f)-90f,170f,0));
        }
    }
    private void OnValidate()
    {
        if(DirectionalLight!=null)
            return;
        if (RenderSettings.sun != null)
        {
            DirectionalLight = RenderSettings.sun;
        }
        else
        {
            Light[] lights = FindObjectsOfType<Light>();
            foreach (Light light in lights)
            {
                if (light.type == LightType.Directional)
                {
                    DirectionalLight = light;
                    return;
                }
                
            }
        }
    }
}
