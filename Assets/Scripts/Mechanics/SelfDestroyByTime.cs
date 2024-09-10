using System.Collections;
using UnityEngine;
using Object = UnityEngine.Object;

public class SelfDestroyByTime : MonoBehaviour
{
    
    [SerializeField] private float _time;

    void Awake()
    {
        StartCoroutine(Waiter());
    }

    IEnumerator Waiter()
    {
        yield return new WaitForSeconds(_time);
        Object.Destroy(this.gameObject);
    }
}
