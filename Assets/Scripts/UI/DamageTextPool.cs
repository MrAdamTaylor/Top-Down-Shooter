using System.Collections.Generic;
using UnityEngine;

public class DamageTextPool : MonoBehaviour
{
    public static DamageTextPool Instance; // Singleton для удобного доступа

    [SerializeField] private GameObject damageTextPrefab; // Префаб всплывающего текста
    [SerializeField] private int poolSize = 20; // Размер пула

    private readonly Queue<DamageText> _pool = new Queue<DamageText>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        InitializePool();
    }

    private void InitializePool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(damageTextPrefab, transform);
            DamageText damageText = obj.GetComponent<DamageText>();
            obj.SetActive(false);
            _pool.Enqueue(damageText);
        }
    }

    public DamageText GetDamageText()
    {
        if (_pool.Count > 0)
        {
            DamageText damageText = _pool.Dequeue();
            damageText.gameObject.SetActive(true);
            return damageText;
        }

        // Если пула недостаточно, создаем новый объект
        GameObject obj = Instantiate(damageTextPrefab, transform);
        return obj.GetComponent<DamageText>();
    }

    public void ReturnDamageText(DamageText damageText)
    {
        damageText.gameObject.SetActive(false);
        _pool.Enqueue(damageText);
    }
}
