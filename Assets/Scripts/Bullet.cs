using UnityEngine;

public class Bullet : MonoBehaviour, IMoving
{
    [SerializeField] private BulletData _data;

    private void Start()
    {
        InvokeRepeating("OnMove", 0f, GameManager.Instance.GameTick);
    }
    
    public void OnMove()
    {
        transform.position += (Vector3) (_data.Speed * Vector2.up * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamagable damagable = collision.GetComponent<IDamagable>();
        damagable?.OnDamage(_data.Damage);
        Destroy(gameObject);
    }
}
