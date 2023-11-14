using UnityEngine;

public class Rectangular
{
    private Vector2 _firstRectanglePoint;
    private Vector2 _secondRectanglePoint;
    private Vector2 _thirdRectanglePoint;
    private Vector2 _forthRectanglePoint;

    public Vector2 FirstRectanglePoint { get => _firstRectanglePoint; }
    public Vector2 SecondRectanglePoint { get => _secondRectanglePoint; }
    public Vector2 ThirdRectnaglePoint { get => _thirdRectanglePoint; }
    public Vector2 ForthRectanglePoint { get => _forthRectanglePoint; }

    public float Width
    {
        get => Vector2.Distance(_firstRectanglePoint, _secondRectanglePoint);
    }

    public float Height
    {
        get => Vector2.Distance(_secondRectanglePoint, _thirdRectanglePoint);
    }

    //Rectongular with points ABCD
    public Rectangular(Vector2 point1, Vector2 point2, Vector2 point3, Vector2 point4)
    {
        _firstRectanglePoint = point1;
        _secondRectanglePoint = point2;
        _thirdRectanglePoint = point3;
        _forthRectanglePoint = point4;
    }

    public Vector2[] GetAllPoints()
    {
        return new Vector2[] { _firstRectanglePoint, _secondRectanglePoint, _thirdRectanglePoint, _forthRectanglePoint };
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(_firstRectanglePoint, _secondRectanglePoint);
        Gizmos.DrawLine(_secondRectanglePoint, _thirdRectanglePoint);
        Gizmos.DrawLine(_thirdRectanglePoint, _forthRectanglePoint);
        Gizmos.DrawLine(_forthRectanglePoint, _firstRectanglePoint);
    }
}