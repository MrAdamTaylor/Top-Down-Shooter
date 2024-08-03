using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldShootSystem : MonoBehaviour
{
    [SerializeField]private Transform _spawn;
    
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shoot();
        }
    }

    public void Shoot()
    {
        Ray ray = new Ray(_spawn.position, -_spawn.forward);
        RaycastHit hit;

        float shootDistance = 20f;

        if (Physics.Raycast(ray, out hit, shootDistance))
        {
            if (hit.collider != null)
            {
                Debug.Log("Мы попали в объект");
            }

            shootDistance = hit.distance;
        }
        Debug.DrawRay(ray.origin, ray.direction * shootDistance, Color.red,1f);
    }
}