using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawn zone settings")]
    [SerializeField] private float _topMargin;
    [SerializeField] private float _bottomMargin;
    [SerializeField] private float _leftAndRightMargin;

    [Header("Enemy amount settings")]
    [SerializeField] private int _numberOfEnemiesInRow;
    [SerializeField] private int _numberOfColumnsWithEnemies;

    [SerializeField] private GameObject _enemyPrefab;

    private Vector2 _firstRectanglePoint;
    private Vector2 _secondRectanglePoint;
    private Vector2 _thirdRectanglePoint;
    private Vector2 _forthRectanglePoint;

    private Rect _spawnZone;

    // Start is called before the first frame update
    void Start()
    {
        #region CalculatingSpawnZoneRectangle
        Vector2 _minScreenSize = Camera.main.ScreenToWorldPoint(Vector2.zero);
        Vector2 _maxScreenSize = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

        _firstRectanglePoint = new Vector2(_minScreenSize.x + _leftAndRightMargin, _maxScreenSize.y - _topMargin);
        _secondRectanglePoint = new Vector2(_maxScreenSize.x - _leftAndRightMargin, _maxScreenSize.y - _topMargin);
        _thirdRectanglePoint = new Vector2(_maxScreenSize.x - _leftAndRightMargin, _minScreenSize.y + _bottomMargin);
        _forthRectanglePoint = new Vector2(_minScreenSize.x + _leftAndRightMargin, _minScreenSize.y + _bottomMargin);

        float _spawnZoneWidth = Vector2.Distance(_firstRectanglePoint, _secondRectanglePoint);
        float _spawnZoneHeight = Vector2.Distance(_secondRectanglePoint, _thirdRectanglePoint);

        Debug.LogWarning(_spawnZoneWidth);
        Debug.LogWarning(_spawnZoneHeight);

        _spawnZone = new Rect(new Vector2(0, 1.5f), new Vector2(_spawnZoneWidth, _spawnZoneHeight - 1.5f));

        Debug.LogWarning(_spawnZone.width);
        Debug.LogWarning(_spawnZone.height);

        #endregion
        //Spawning enemies
        float sizeOfOneRow = _spawnZone.x / _numberOfEnemiesInRow;
        float sizeOfOneColumn = _spawnZone.y / _numberOfColumnsWithEnemies;
        for (int i = 0; i < _numberOfColumnsWithEnemies; i++)
        {
            float yCordinateToSpawn = sizeOfOneColumn * i + (sizeOfOneColumn - _enemyPrefab.transform.lossyScale.y - _spawnZone.y) / 2;
            for (int j = 0; j < _numberOfEnemiesInRow; j++)
            {
                float xCordinateToSpawn = sizeOfOneRow * j + (sizeOfOneRow - _enemyPrefab.transform.lossyScale.x - _spawnZone.x) / 2;
                Instantiate(_enemyPrefab, new Vector2(xCordinateToSpawn, yCordinateToSpawn), Quaternion.identity);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(_firstRectanglePoint, _secondRectanglePoint);
        Gizmos.DrawLine(_secondRectanglePoint, _thirdRectanglePoint);
        Gizmos.DrawLine(_thirdRectanglePoint, _forthRectanglePoint);
        Gizmos.DrawLine(_forthRectanglePoint, _firstRectanglePoint);

        Gizmos.DrawCube(_spawnZone.min, _spawnZone.max);
    }
}
