using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float _topMargin;
    [SerializeField] private float _bottomMargin;
    [SerializeField] private float _leftAndRightMargin;

    private Vector2 _firstRectanglePoint;
    private Vector2 _secondRectanglePoint;
    private Vector2 _thirdRectanglePoint;
    private Vector2 _forthRectanglePoint;

    private Rect _spawnZone;

    // Start is called before the first frame update
    void Start()
    {
        Vector2 _minScreenSize = Camera.main.ScreenToWorldPoint(Vector2.zero);
        Vector2 _maxScreenSize = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

        _firstRectanglePoint = new Vector2(_minScreenSize.x + _leftAndRightMargin, _maxScreenSize.y - _topMargin);
        _secondRectanglePoint = new Vector2(_maxScreenSize.x - _leftAndRightMargin, _maxScreenSize.y - _topMargin);
        _thirdRectanglePoint = new Vector2(_maxScreenSize.x - _leftAndRightMargin, _minScreenSize.y + _bottomMargin);
        _forthRectanglePoint = new Vector2(_minScreenSize.x + _leftAndRightMargin, _minScreenSize.y + _bottomMargin);

        float _spawnZoneWidth = (_forthRectanglePoint - _thirdRectanglePoint).magnitude;
        float _spawnZoneHeight = (_secondRectanglePoint - _firstRectanglePoint).magnitude;

        _spawnZone = new Rect(_thirdRectanglePoint, new Vector2(_spawnZoneWidth, _spawnZoneHeight));
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
    }
}
