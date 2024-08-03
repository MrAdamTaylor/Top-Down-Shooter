using UnityEngine;

public class NewShootSystem : MonoBehaviour
{
    [Range(0.5f, 10f)][SerializeField] private float _delay = 5f;

    [SerializeField] private ParticleSystem _shootingSystem;
}