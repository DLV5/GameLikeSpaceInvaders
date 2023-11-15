using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    //Amount of bullets shooted per second
    [SerializeField] private float _fireRate;
    [SerializeField] private InputAction _playerAttackActions;

    [SerializeField] private GameObject _leftShootPoint;
    [SerializeField] private GameObject _rightShootPoint;

    [SerializeField] private GameObject _bulletPrefab;

    private bool _canShoot = true;

    //Even number - left gun shootiong, odd number - right gun shooting
    private int _counter = 0;

    private void OnEnable()
    {
        _playerAttackActions.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        if (_playerAttackActions.IsPressed() && _canShoot)
        {
            OnShoot();
        }
    }

    private void OnDisable()
    {
        _playerAttackActions.Disable();
    }

    private void OnShoot()
    {
        if(_counter % 2 == 0)
        {
            Instantiate(_bulletPrefab, _leftShootPoint.transform.position, Quaternion.identity);
        } else
        {
            Instantiate(_bulletPrefab, _rightShootPoint.transform.position, Quaternion.identity);
        }
        _counter++;
        StartCoroutine(ProceedCooldown(_fireRate));
    }

    private IEnumerator ProceedCooldown(float fireRate)
    {
        _canShoot = false;

        yield return new WaitForSeconds(1 / fireRate);

        _canShoot = true;
    }

}
