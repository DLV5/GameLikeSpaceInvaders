using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawn zone settings")]
    [SerializeField] private float _topMargin;
    [SerializeField] private float _bottomMargin;
    [SerializeField] private float _leftAndRightMargin;

    [Header("Enemy amount settings")]
    [SerializeField] private int _numberOfEnemiesInColumn;
    [SerializeField] private int _numberOfRowsWithEnemies;

    [SerializeField] private GameObject _enemyPrefab;

    private Rectangular _spawnZone;

    // Start is called before the first frame update
    void Start()
    {
        #region CalculatingSpawnZoneRectangle
        Vector2 _minScreenSize = Camera.main.ScreenToWorldPoint(Vector2.zero);
        Vector2 _maxScreenSize = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

        Vector2 _firstRectanglePoint = new Vector2(_minScreenSize.x + _leftAndRightMargin, _maxScreenSize.y - _topMargin);
        Vector2 _secondRectanglePoint = new Vector2(_maxScreenSize.x - _leftAndRightMargin, _maxScreenSize.y - _topMargin);
        Vector2 _thirdRectanglePoint = new Vector2(_maxScreenSize.x - _leftAndRightMargin, _minScreenSize.y + _bottomMargin);
        Vector2 _forthRectanglePoint = new Vector2(_minScreenSize.x + _leftAndRightMargin, _minScreenSize.y + _bottomMargin);

        //float _spawnZoneWidth = Vector2.Distance(_firstRectanglePoint, _secondRectanglePoint);
        //float _spawnZoneHeight = Vector2.Distance(_secondRectanglePoint, _thirdRectanglePoint);

        _spawnZone = new Rectangular(_firstRectanglePoint, _secondRectanglePoint, _thirdRectanglePoint, _forthRectanglePoint);

        #endregion
        //Spawning enemies
        float sizeOfOneRow = _spawnZone.Width / _numberOfEnemiesInColumn;
        float sizeOfOneColumn = _spawnZone.Height / _numberOfRowsWithEnemies;
        for (int i = 0; i < _numberOfRowsWithEnemies; i++)
        {
            float yCordinateToSpawn = sizeOfOneColumn * i + (sizeOfOneColumn - _enemyPrefab.transform.lossyScale.y - _spawnZone.Height) / 2;
            for (int j = 0; j < _numberOfEnemiesInColumn; j++)
            {
                float xCordinateToSpawn = sizeOfOneRow * j + (sizeOfOneRow - _enemyPrefab.transform.lossyScale.x - _spawnZone.Width) / 2;
                Instantiate(_enemyPrefab, new Vector2(xCordinateToSpawn, yCordinateToSpawn), Quaternion.identity);
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying)
            return;

        Vector2[] rectangularPoints = _spawnZone.GetAllPoints();

        Gizmos.DrawLine(rectangularPoints[0], rectangularPoints[1]);
        Gizmos.DrawLine(rectangularPoints[1], rectangularPoints[2]);
        Gizmos.DrawLine(rectangularPoints[2], rectangularPoints[3]);
        Gizmos.DrawLine(rectangularPoints[3], rectangularPoints[0]);
    }
}
