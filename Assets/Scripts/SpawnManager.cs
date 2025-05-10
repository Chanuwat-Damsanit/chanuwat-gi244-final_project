using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    //public GameObject[] obstaclePrefab;
    public Vector3 spawnPos = new(25, 0, 0);
    public Vector3 potionSpawnPos = new(25, 0, 0);

    public float startDelay = 2;
    public float repeatRate = 2;

    public float potionStartDelay = 3;
    public float potionRepeatRate = 3;

    private PlayerController playerController;

    void Start()
    {
        InvokeRepeating(nameof(SpawnObstacle), startDelay, repeatRate);
        InvokeRepeating(nameof(SpawnPotion), potionStartDelay, potionRepeatRate);

        playerController =  GameObject.Find("Player").GetComponent<PlayerController>();
    }

    void SpawnObstacle()
    {
        if (playerController.gameOver == false) 
        {
            //ObstacleObjectPool Acquire
            GameObject obstacle = ObstacleObjectPool.GetInstance().Acquire();

            //Set Position and Rotation
            obstacle.transform.SetPositionAndRotation(spawnPos, obstacle.transform.rotation);

        }
    }

    void SpawnPotion()
    {
        if (playerController.gameOver == false)
        {
            //ObstacleObjectPool Acquire
            GameObject potion = PotionObjectPool.GetPotionInstance().PotionAcquire();

            //Set Position and Rotation
            potion.transform.SetLocalPositionAndRotation(potionSpawnPos, potion.transform.rotation);

        }
    }
}
