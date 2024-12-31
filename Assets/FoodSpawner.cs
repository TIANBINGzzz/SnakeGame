using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    public GameObject foodPrefab; // 食物预制体
    private MapSizeConfig mapConfig; // 地图配置

    void Start()
    {
        // 加载地图配置
        mapConfig = ConfigLoader.LoadMapConfig();
        if (mapConfig == null)
        {
            Debug.LogError("Failed to load map configuration!");
            return;
        }

        // 生成初始食物
        SpawnFood();
    }

    public void SpawnFood()
    {
        if (mapConfig == null) return;

        // 使用加载的地图范围生成随机位置
        float x = Random.Range(mapConfig.mapSize.minX, mapConfig.mapSize.maxX);
        float y = Random.Range(mapConfig.mapSize.minY, mapConfig.mapSize.maxY);
        Vector3 spawnPosition = new Vector3(x, y, 0);

        // 生成食物
        Instantiate(foodPrefab, spawnPosition, Quaternion.identity);
        Debug.Log($"Food spawned at: {spawnPosition}");
    }
}
