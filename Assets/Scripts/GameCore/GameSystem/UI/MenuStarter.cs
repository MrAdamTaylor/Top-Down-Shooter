using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuStarter : MonoBehaviour
{
    [SerializeField] private Death _death;
    [SerializeField] private Canvas _canvas;
    private void Update()
    {
        if (_death.isDeath)
        {
            _canvas.gameObject.SetActive(true);
        }
    }
}
