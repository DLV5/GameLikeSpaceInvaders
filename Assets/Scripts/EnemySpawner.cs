using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemySpawnData _spawnData;

    private EnemySpawnZoneHandler _spawnZoneHandler;

    // Start is called before the first frame update
    void Start()
    {
        _spawnZoneHandler = new EnemySpawnZoneHandler(_spawnData);
       
        //Spawning enemies

        for (int i = 0; i < _spawnData.NumberOfRowsWithEnemies; i++)
        {
            //Creating enemy container for future movement
            GameObject _enemyContainer = new GameObject($"EnemyContainer {i}");
            _enemyContainer.transform.parent = transform;

            _enemyContainer.AddComponent<EnemyContainer>();
            EnemyContainer enemyContainerScript = _enemyContainer.GetComponent<EnemyContainer>();
            float yCoordinateToSpawn = _spawnZoneHandler.GetYCoondinateToSpawn(i);
            for (int j = 0; j < _spawnData.NumberOfEnemiesInColumn; j++)
            {
                float xCoordinateToSpawn = _spawnZoneHandler.GetXCoondinateToSpawn(j);
                GameObject enemy = Instantiate(_spawnData.EnemyPrefab,
                    new Vector2(xCoordinateToSpawn, yCoordinateToSpawn), Quaternion.identity);
                enemy.transform.parent = _enemyContainer.transform;
                enemyContainerScript.AddEnemyToContainer(enemy);
                //EnemyMovement currentEnemyMovementScript = enemy.GetComponent<EnemyMovement>();
                //if (_previousEnemyMovementScript != null)
                //{
                //    _previousEnemyMovementScript.RightNeighborEnemy = currentEnemyMovementScript;
                //}
                //_previousEnemyMovementScript = currentEnemyMovementScript;
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying)
            return;

        Vector2[] rectangularPoints = _spawnZoneHandler.SpawnZone.GetAllPoints();

        Gizmos.DrawLine(rectangularPoints[0], rectangularPoints[1]);
        Gizmos.DrawLine(rectangularPoints[1], rectangularPoints[2]);
        Gizmos.DrawLine(rectangularPoints[2], rectangularPoints[3]);
        Gizmos.DrawLine(rectangularPoints[3], rectangularPoints[0]);
    }
}
