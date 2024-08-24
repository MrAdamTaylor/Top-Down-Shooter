using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class SelfDestroyByTime : MonoBehaviour
{
    
    [SerializeField] private float _time;

    private void Awake()
    {
        StartCoroutine(Waiter());
    }

    IEnumerator Waiter()
    {
        yield return new WaitForSeconds(_time);
        Object.Destroy(this.gameObject);
    }
}
