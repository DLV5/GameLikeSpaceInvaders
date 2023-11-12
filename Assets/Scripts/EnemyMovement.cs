using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private EnemyMovement _rightNeighborEnemy;

    public EnemyMovement RightNeighborEnemy { get; set; }

    private void OnMouseDown()
    {
        Debug.Log(RightNeighborEnemy.gameObject);
    }
}
