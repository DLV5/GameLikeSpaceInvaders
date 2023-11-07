using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private InputAction _playerMoveActions;

    private void OnEnable()
    {
        _playerMoveActions.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        if (_playerMoveActions.IsPressed())
        {
            OnMove();
        }
    }

    private void OnDisable()
    {
        _playerMoveActions.Disable();
    }

    private void OnMove()
    {
        transform.position += (Vector3) (Vector2.right * _playerMoveActions.ReadValue<float>() * _speed * Time.deltaTime);
    }
}
