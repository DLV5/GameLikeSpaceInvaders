using System.Collections;
using UnityEngine;

public class EnemyContainerMovement : MonoBehaviour
{
    private float _horizontalSpeed = 15f;
    private float _verticalSpeed = 15f;

    /// <summary>
    /// "1" - to the right, "-1" - to the left
    /// </summary>
    private float _horizontalDirection = 1;

    /// <summary>
    /// "1" - to the top, "-1" - to the bottom
    /// </summary>
    private float _verticalDirection = -1;

    private float _changeDirectionCooldown = 1f;
    private bool _canChangeDirection = true;

    private void Start()
    {
        InvokeRepeating("MoveHorizontaly", 0f, GameManager.Instance.GameTick);
    }

    private void MoveHorizontaly()
    {
        transform.position += (Vector3) (_horizontalDirection * Vector2.right 
            * _horizontalSpeed * Time.deltaTime);
    }

    private void MoveVerticaly()
    {
        transform.position += (Vector3)(_verticalDirection * Vector2.up
            * _verticalSpeed * Time.deltaTime);
    }

    public void ChangeHorizontalDirection()
    {
        if( _canChangeDirection)
        {
            _horizontalDirection = -1 * _horizontalDirection;
            MoveVerticaly();
            StartCoroutine(ChangeDirectionCooldown(_changeDirectionCooldown));
        }
    }

    private IEnumerator ChangeDirectionCooldown(float changeDirectionCooldown)
    {
        _canChangeDirection = false;
        yield return new WaitForSeconds(changeDirectionCooldown);
        _canChangeDirection = true;
    }
}
