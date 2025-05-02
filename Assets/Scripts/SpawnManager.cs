using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] obstaclePrefab;
    public Vector3 spawnPos = new(25, 0, 0);

    public float startDelay = 2;
    public float repeatRate = 2;

    private PlayerController playerController;

    void Start()
    {
        InvokeRepeating(nameof(SpawnObstacle), startDelay, repeatRate);

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
}
