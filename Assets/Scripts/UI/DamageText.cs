using UnityEngine;
using TMPro;

public class DamageText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text; // ����� ��� ����������� �����
    [SerializeField] private float moveSpeed = 2f; // �������� ��������
    [SerializeField] private float lifetime = 1f; // ����� ����� ������

    private Vector3 _movementDirection = Vector3.up; // ����������� �������� ������
    private float _timer;

    public void Initialize(float damage, Vector3 worldPosition)
    {
        text.text = damage.ToString("F0"); // ���������� ����� �����
        transform.position = Camera.main.WorldToScreenPoint(worldPosition); // ����������� ������� ������� � ��������
        _timer = 0f;
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= lifetime)
        {
            Destroy(gameObject); // ������� ������ ����� ��������� �����
        }

        transform.Translate(_movementDirection * moveSpeed * Time.deltaTime);
    }
}
