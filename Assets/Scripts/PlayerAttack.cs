using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    //Amount of bullets shooted per second
    [SerializeField] private float _fireRate;
    [SerializeField] private InputAction _playerAttackActions;

    private bool _canShoot = true;

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
        Debug.Log("Shoot!");
        StartCoroutine(ProceedCooldown(_fireRate));
    }
    private IEnumerator ProceedCooldown(float fireRate)
    {
        _canShoot = false;

        yield return new WaitForSeconds(1 / fireRate);

        _canShoot = true;
    }

}
