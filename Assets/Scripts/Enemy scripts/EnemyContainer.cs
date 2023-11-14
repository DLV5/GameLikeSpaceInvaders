using UnityEngine;

[RequireComponent(typeof(EnemyContainerCalculator))]
[RequireComponent (typeof(EnemyContainerMovement))]
public class EnemyContainer : MonoBehaviour
{
    private EnemyContainerCalculator _calculator;
    private EnemyContainerMovement _mover;

    private void Awake()
    {
        _calculator = GetComponent<EnemyContainerCalculator>();
        _mover = GetComponent<EnemyContainerMovement>();
    }

    private void FixedUpdate()
    {
        if (_calculator.IsContainerExitedScreenBoundaries())
        {
            _mover.ChangeHorizontalDirection();
        }
    }
    public void AddEnemyToContainer(GameObject enemy)
    {
        _calculator.AddEnemyToContainer(enemy);
    }

}
