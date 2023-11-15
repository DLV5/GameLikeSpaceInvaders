using UnityEngine;

public class GameManager : MonoBehaviour
{
    /// <summary>
    /// Defines move of all entities in game exept player
    /// </summary>
    [SerializeField] private float _gameTick = 0.25f;

    /// <summary>
    /// Defines move of all entities in game exept player
    /// </summary>
    public float GameTick 
    { 
        get => _gameTick; 
    } 

    public static GameManager Instance { get; set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
}
