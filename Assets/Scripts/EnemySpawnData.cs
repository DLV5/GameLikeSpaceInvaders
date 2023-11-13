using UnityEngine;

[CreateAssetMenu(fileName = "EnemySpawnData", menuName = "Data/EnemySpawnerData", order = 1 )]
public class EnemySpawnData : ScriptableObject
{
    [Header("Spawn zone settings")]
    public float TopMargin;
    public float BottomMargin;
    public float LeftAndRightMargin;

    [Header("Enemy amount settings")]
    public int NumberOfEnemiesInColumn;
    public int NumberOfRowsWithEnemies;
    
    public GameObject EnemyPrefab;
}
