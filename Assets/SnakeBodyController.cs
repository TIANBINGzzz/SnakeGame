using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour
{
    public GameObject bodyPrefab; // 蛇身体的预制体
    public int initialLength = 2; // 初始长度
    public float moveSpeed = 5f; // 蛇头移动速度
    public float bodySpacing = 0.8f; // 身体块之间的距离

    private List<Transform> bodyParts = new List<Transform>(); // 存储身体块
    private List<Vector3> positionQueue = new List<Vector3>(); // 位置队列
    private Vector2 moveDirection = Vector2.right; // 移动方向
    private Vector2 currentDirection = Vector2.right; // 当前方向

    void Start()
    {
        // 初始化蛇头和身体
        bodyParts.Add(this.transform);

        for (int i = 0; i < initialLength; i++)
        {
            AddBodyPart();
        }
    }

    void Update()
    {
        // 获取输入方向
        HandleInput();

        // 更新蛇头位置
        MoveHead();

        // 更新队列
        UpdatePositionQueue();

        // 平滑移动身体
        MoveBody();
    }

    void HandleInput()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (Mathf.Abs(horizontal) > Mathf.Abs(vertical)) // 水平输入优先
        {
            if (horizontal > 0 && currentDirection != Vector2.left)
                moveDirection = Vector2.right;
            else if (horizontal < 0 && currentDirection != Vector2.right)
                moveDirection = Vector2.left;
        }
        else if (Mathf.Abs(vertical) > Mathf.Abs(horizontal)) // 垂直输入优先
        {
            if (vertical > 0 && currentDirection != Vector2.down)
                moveDirection = Vector2.up;
            else if (vertical < 0 && currentDirection != Vector2.up)
                moveDirection = Vector2.down;
        }
    }

    void MoveHead()
    {
        Vector3 movement = new Vector3(moveDirection.x, moveDirection.y, 0) * moveSpeed * Time.deltaTime;
        transform.Translate(movement, Space.World);
        currentDirection = moveDirection;
    }

    void UpdatePositionQueue()
    {
        // 将蛇头位置加入队列
        positionQueue.Insert(0, transform.position);

        // 保证队列长度不超过需要的位置数
        float timePerUnit = bodySpacing / moveSpeed; // 每移动一个单位需要的时间
        int maxQueueLength = Mathf.CeilToInt(1f / (timePerUnit * Time.deltaTime));

        if (positionQueue.Count > maxQueueLength)
        {
            positionQueue.RemoveAt(positionQueue.Count - 1);
        }
    }


    void MoveBody()
    {
        for (int i = 1; i < bodyParts.Count; i++)
        {
            // 获取目标位置
            int targetIndex = Mathf.Min(
                Mathf.RoundToInt(i * bodySpacing / moveSpeed / Time.deltaTime),
                positionQueue.Count - 1
            );
            Vector3 targetPosition = positionQueue[targetIndex];

            // 平滑移动身体块到目标位置
            bodyParts[i].position = Vector3.MoveTowards(
                bodyParts[i].position,
                targetPosition,
                moveSpeed * Time.deltaTime
            );
        }
    }



    public void AddBodyPart()
    {
        // 在最后一个身体块的位置生成一个新的身体块
        Transform lastBodyPart = bodyParts[bodyParts.Count - 1];
        Vector3 newPosition = lastBodyPart.position;
        GameObject newBodyPart = Instantiate(bodyPrefab, newPosition, Quaternion.identity);
        bodyParts.Add(newBodyPart.transform);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Food"))
        {
            AddBodyPart();
            Destroy(collision.gameObject);
        }
    }
}