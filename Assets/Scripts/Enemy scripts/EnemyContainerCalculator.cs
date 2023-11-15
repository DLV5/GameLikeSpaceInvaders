using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyContainer))]
[RequireComponent(typeof(BoxCollider2D))]
public class EnemyContainerCalculator : MonoBehaviour
{
    private List<GameObject> _enemiesInRow = new List<GameObject>();

    private BoxCollider2D _boxCollider;

    /// <summary>
    /// Most left enemy in row (by x)
    /// </summary>
    private GameObject _mostLeftEnemy;

    /// <summary>
    /// Most right enemy in row (by x)
    /// </summary>
    private GameObject _mostRightEnemy;

    /// <summary>
    /// Center moving by this value after most right enemy died
    /// </summary>
    private float _leftOffset = 0;

    /// <summary>
    /// This value applies to any center move and then resets
    /// </summary>
    private float _centerOffset = 0;

    /// <summary>
    /// Center moving by this value after most left enemy died
    /// </summary>
    private float _rightOffset = 0;

    private void OnEnable()
    {
        EnemyHealth.Destroyed += OnEnemyDestroyed;
    }

    private void Start()
    {
        _boxCollider = GetComponent<BoxCollider2D>();
        _boxCollider.isTrigger = true;
    }

    private void OnDisable()
    {
        EnemyHealth.Destroyed -= OnEnemyDestroyed;
    }

    public bool IsContainerExitedScreenBoundaries()
    {
        Vector2 maxScreenPoint = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        Vector2 minScreenPoint = Camera.main.ScreenToWorldPoint(new Vector2( 0, 0));

        bool isExitedLeftBoundary = _boxCollider.bounds.min.x < minScreenPoint.x;
        bool isExitedRightBoundary = _boxCollider.bounds.max.x > maxScreenPoint.x;
        bool isExitedTopBoundary = _boxCollider.bounds.max.y > maxScreenPoint.y;
        bool isExitedBottomBoundary = _boxCollider.bounds.min.y < minScreenPoint.y;

        return isExitedLeftBoundary || isExitedRightBoundary || isExitedTopBoundary || isExitedBottomBoundary;
    }

    private void OnEnemyDestroyed(float xPositionOfEnemy)
    {
        if (_mostLeftEnemy == null || _mostRightEnemy == null)
        {
            // Changing collider
            RecalculateColliderSize();
            RecalculateColliderOffset();
        }
        else
        {
            //Changing values which will be used to calculate offset later
            CalculateOffsetsValues(xPositionOfEnemy);
        }
    }


    public void AddEnemyToContainer(GameObject enemy)
    {
        _enemiesInRow.Add(enemy);
        if (_mostLeftEnemy == null || _mostLeftEnemy.transform.position.x > enemy.transform.position.x)
        {
            _mostLeftEnemy = enemy;
        }
        if (_mostRightEnemy == null || _mostRightEnemy.transform.position.x < enemy.transform.position.x)
        {
            _mostRightEnemy = enemy;
        }

        RecalculateColliderSize();
    }

    //Recalculating size according to remaining enemies
    private void RecalculateColliderSize()
    {
        if (_boxCollider == null)
        {
            _boxCollider = GetComponent<BoxCollider2D>();
        }

        if (_mostLeftEnemy != null && _mostRightEnemy != null)
        {
            _boxCollider.size = new Vector2(Vector2.Distance(_mostLeftEnemy.transform.position
                - _mostLeftEnemy.transform.lossyScale / 2,
                _mostRightEnemy.transform.position
                + _mostRightEnemy.transform.lossyScale / 2), 1);
        }
    }

    private void RecalculateColliderOffset()
    {
        if (_mostLeftEnemy == null)
        {
            //find new most left
            _boxCollider.offset += new Vector2(_rightOffset + _centerOffset, 0);
            _centerOffset = 0;
            _rightOffset = 0;
        }
        else
        {
            //find new most right
            _boxCollider.offset -= new Vector2(_leftOffset + _centerOffset, 0);
            _centerOffset = 0;
            _leftOffset = 0;
        }
    }

    /// <summary>
    /// Adds 1 to left, right, or center offsets
    /// </summary>
    /// <param name="xPositiomOfEnemy"></param>
    private void CalculateOffsetsValues(float xPositiomOfEnemy)
    {
        float centerOfCollider = _boxCollider.size.x / 2 + _boxCollider.offset.x;
        if (xPositiomOfEnemy == centerOfCollider)
        {
            _centerOffset += 1;
        }
        else if (xPositiomOfEnemy < centerOfCollider)
        {
            _rightOffset += 1;
        }
        else
        {
            _leftOffset += 1;
        }
    }
}
