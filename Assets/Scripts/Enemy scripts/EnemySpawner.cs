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
            _enemyContainer.transform.position = new Vector2(0, _spawnZoneHandler.GetYCoondinateToSpawn(i));

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
            }
        }

        transform.position = new Vector2(transform.position.x, (_spawnZoneHandler.SpawnZone.Height / 2));
    }

    //I don't know exactly is it problem here in visualisatio or
    //in calculations, but calculations looks allright, so I guess guizmo
    //not working correctly, I will comment it by now
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
