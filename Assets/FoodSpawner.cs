using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    public GameObject foodPrefab; 
    public Vector2 spawnArea = new Vector2(10, 10); 

    void Start()
    {
        SpawnFood();
    }

    public void SpawnFood()
    {

        float x = Random.Range(-spawnArea.x, spawnArea.x);
        float y = Random.Range(-spawnArea.y, spawnArea.y);
        Vector2 spawnPosition = new Vector2(x, y);

        Instantiate(foodPrefab, spawnPosition, Quaternion.identity);
    }
}
