using UnityEngine;

public class EnemySpawnZoneHandler
{
    private EnemySpawnData _spawnData;
    private Rectangular _spawnZone;

    public Rectangular SpawnZone { get { return _spawnZone; } }

    public float Width { get { return _spawnZone.Width; } }
    public float Height { get { return _spawnZone.Height; } }

    public float SizeOfOneRow { get; private set; }
    public float SizeOfOneColumn { get; private set; }

    public EnemySpawnZoneHandler(EnemySpawnData data)
    {
        _spawnData = data;

        CreateNewSpawnZone(_spawnData.TopMargin, _spawnData.BottomMargin, _spawnData.LeftAndRightMargin);
    }

    public void CreateNewSpawnZone(float topMargin, float bottomMargin, float leftAndRightMargin)
    {
        Vector2 _minScreenSize = Camera.main.ScreenToWorldPoint(Vector2.zero);
        Vector2 _maxScreenSize = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

        Vector2 _firstRectanglePoint = new Vector2(_minScreenSize.x + leftAndRightMargin, _maxScreenSize.y - topMargin);
        Vector2 _secondRectanglePoint = new Vector2(_maxScreenSize.x - leftAndRightMargin, _maxScreenSize.y - topMargin);
        Vector2 _thirdRectanglePoint = new Vector2(_maxScreenSize.x - leftAndRightMargin, _minScreenSize.y + bottomMargin);
        Vector2 _forthRectanglePoint = new Vector2(_minScreenSize.x + leftAndRightMargin, _minScreenSize.y + bottomMargin);

        _spawnZone = new Rectangular(_firstRectanglePoint, _secondRectanglePoint, _thirdRectanglePoint, _forthRectanglePoint);

        SizeOfOneColumn = CalculateSizeOfOneColumn();
        SizeOfOneRow = CalculateSizeOfOneRow();
    }
    //Calculating Y position for enemy in row
    public float GetYCoondinateToSpawn(int index) => SizeOfOneRow * index +
        (SizeOfOneRow - Height) / 2;

    //Calculating X position for each enemy in column
    public float GetXCoondinateToSpawn(int index) => SizeOfOneColumn * index +
        (SizeOfOneColumn - Width) / 2;


    private float CalculateSizeOfOneColumn() => _spawnZone.Width / _spawnData.NumberOfEnemiesInColumn;

    private float CalculateSizeOfOneRow() => _spawnZone.Height / _spawnData.NumberOfRowsWithEnemies;
}
