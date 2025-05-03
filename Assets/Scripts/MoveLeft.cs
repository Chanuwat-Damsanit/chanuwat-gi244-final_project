using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    public float originalSpeed = 10f;
    public float speed = 10f;

    private float leftBound = -15;

    private PlayerController playerController;

    public bool hasDash = false;
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    void Update()
    {
        if (!playerController.gameOver)
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }

        if (transform.position.x < leftBound && gameObject.CompareTag("Obstacle"))
        {
            //ObstacleObjectPool Return
            ObstacleObjectPool.GetInstance().Return(gameObject);
        }

        if (transform.position.x < leftBound && gameObject.CompareTag("HealthPotion"))
        {
            PotionObjectPool.GetPotionInstance().PotionReturn(gameObject);
        }

        if (playerController.gameOver == false && playerController.dashAction.IsPressed() && !hasDash) 
        {
            speed *= 2;
            hasDash = true;
        }
        else if (!playerController.dashAction.IsPressed() && hasDash) 
        {
            speed = originalSpeed;
            hasDash = false;
        }
    }
}
