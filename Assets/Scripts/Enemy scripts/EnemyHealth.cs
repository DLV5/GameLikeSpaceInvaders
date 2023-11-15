using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamagable
{
    public static event Action<float> Destroyed;
    [SerializeField] private EnemyData _data;

    private int _maxHealth;
    private int _currentHealth;

    private void Start()
    {
        _maxHealth = _data.EnemyHealth;
        _currentHealth = _maxHealth;
    }

    public void OnDamage(int amout)
    {
        _currentHealth -= amout;
        if(_currentHealth <= 0)
        {
            Destroyed!.Invoke(transform.position.x);
            Destroy(gameObject);
        }
    }
}
