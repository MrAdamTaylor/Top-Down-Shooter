using System;
using UnityEngine;
using UnityEngine.UI;

public class ScreenLoader : MonoBehaviour
{
    [SerializeField] private Image progressImage;
    [SerializeField] private Text loadingText;

    public void SetProgress(string text, float progress)
    {
        progressImage.fillAmount = progress;
        loadingText.text = text + " :" +progress+"%";
    }
}