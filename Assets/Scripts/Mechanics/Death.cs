using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Death : MonoBehaviour
{
    [SerializeField] private Transform _position;

    public bool isDeath;
    
    public void MakeDeath()
    {
        this.gameObject.SetActive(false);
        gameObject.transform.position = _position.position;
        isDeath = true;
    }
    
    
}
