using UnityEngine;
using TMPro;

public class DamageText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text; // Текст для отображения урона
    [SerializeField] private float moveSpeed = 2f; // Скорость движения
    [SerializeField] private float lifetime = 1f; // Время жизни текста

    private Vector3 _movementDirection = Vector3.up; // Направление движения текста
    private float _timer;

    public void Initialize(float damage, Vector3 worldPosition)
    {
        text.text = damage.ToString("F0"); // Отображаем число урона
        transform.position = Camera.main.WorldToScreenPoint(worldPosition); // Преобразуем мировую позицию в экранную
        _timer = 0f;
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= lifetime)
        {
            Destroy(gameObject); // Удаляем объект после окончания жизни
        }

        transform.Translate(_movementDirection * moveSpeed * Time.deltaTime);
    }
}
