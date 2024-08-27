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
}
