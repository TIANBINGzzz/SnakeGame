using UnityEngine;

public class ConfigLoader : MonoBehaviour
{
    public static MapSizeConfig LoadMapConfig(string fileName = "mapConfig.json")
    {
        // 加载 JSON 文件
        TextAsset configFile = Resources.Load<TextAsset>(fileName.Replace(".json", ""));
        if (configFile == null)
        {
            Debug.LogError($"Config file '{fileName}' not found in Resources!");
            return null;
        }

        // 解析 JSON 数据
        MapSizeConfig config = JsonUtility.FromJson<MapSizeConfig>(configFile.text);
        if (config != null)
        {
            Debug.Log("Map Config Loaded: " +
                      $"MinX={config.mapSize.minX}, MaxX={config.mapSize.maxX}, " +
                      $"MinY={config.mapSize.minY}, MaxY={config.mapSize.maxY}");
        }
        return config;
    }
}
