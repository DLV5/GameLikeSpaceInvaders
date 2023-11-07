using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject _playerSpawnPoint;
    [SerializeField] private GameObject _playerPrefab;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(_playerPrefab, _playerSpawnPoint.transform.position, Quaternion.identity);
    }
}
