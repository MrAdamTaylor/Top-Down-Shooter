using System;
using System.Collections;
using System.Collections.Generic;
using Player.ShootSystem;
using Unity.Services.Analytics;
using Unity.Services.Core;
using UnityEngine;
using UnityEngine.UI;

public class AnalyticsManager : MonoBehaviour
{
    private static AnalyticsManager Instance;
    private bool _isInitialized = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Сохраняем объект при смене сцен
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private async void Start()
    {
        try
        {
            Debug.Log("Initializing Unity Services...");
            await UnityServices.InitializeAsync();
            AnalyticsService.Instance.StartDataCollection();
            _isInitialized = true;
            Debug.Log("AnalyticsManager initialized successfully.");
        }
        catch (Exception e)
        {
            Debug.LogError($"Analytics initialization failed: {e.Message}");
        }
    }

    public void HelpButtonClicked()
    {
        if (!_isInitialized)
        {
            Debug.LogWarning("Analytics not initialized, skipping event.");
            return;
        }

        Debug.Log("Sending analytics event: HelpButtonClicked");
        AnalyticsService.Instance.RecordEvent("HelpButtonClicked");
        AnalyticsService.Instance.Flush();
    }
}