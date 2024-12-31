using UnityEngine;

public class FoodController : MonoBehaviour
{
    public float speed = 5f;

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal2"); 
        float vertical = Input.GetAxis("Vertical2"); 

        Vector3 direction = new Vector3(horizontal, vertical, 0).normalized;

        if (direction.magnitude >= 0.1f)
        {
            transform.Translate(direction * speed * Time.deltaTime, Space.World);
        }
    }
}
