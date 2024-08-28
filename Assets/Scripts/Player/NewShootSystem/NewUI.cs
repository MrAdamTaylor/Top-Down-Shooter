using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NewUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textMeshPro;

    [SerializeField] private NewAmmo _ammo;

    private void Start()
    {
        _ammo.ChangeAmmo += UpdateUI;
    }

    private void UpdateUI(int ammo)
    {
        _textMeshPro.text = Convert.ToString(ammo);
    }
}
