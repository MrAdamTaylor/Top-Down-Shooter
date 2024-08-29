using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scores;


    public void SetAmmoText(Ammo component)
    {
        _scores.text = component.SetCurrentAmmo();
    }

    public void UpdateAmmoText(int ammo, bool infinity)
    {
        if (infinity)
        {
            _scores.text = "Infinity";
        }
        else
        {
            _scores.text = Convert.ToString(ammo);
        }
    }
}
