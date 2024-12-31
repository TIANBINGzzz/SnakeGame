[System.Serializable]
public class MapSize
{
    public float minX; // 地图最小X值
    public float maxX; // 地图最大X值
    public float minY; // 地图最小Y值
    public float maxY; // 地图最大Y值
}

[System.Serializable]
public class MapSizeConfig
{
    public MapSize mapSize;
}
