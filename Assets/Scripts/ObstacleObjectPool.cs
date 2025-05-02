using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject[] obstaclePrefab;
    [SerializeField] private int initialPoolSize = 5;

    private List<GameObject> obstaclePool = new();
    private static ObstacleObjectPool staticInstance;

    private void Awake()
    {
        if (staticInstance != null)
        {
            Destroy(staticInstance);
            return;
        }
        staticInstance = this;
    }

    void Start()
    {
        for (int i = 0; i < initialPoolSize; i++)
        {
            CreateNewObstacle();
        }
    }

    public static ObstacleObjectPool GetInstance()
    {
        return staticInstance;
    }

    private void CreateNewObstacle()
    {
        var obstacleIndex = Random.Range(0, obstaclePrefab.Length);
        GameObject obstacle =  Instantiate(obstaclePrefab[obstacleIndex]);
        obstacle.SetActive(false);
        obstaclePool.Add(obstacle); 
    }

    public GameObject Acquire()
    {
        if (obstaclePool.Count == 0)
        {
            CreateNewObstacle();
        }

        GameObject obstacle = obstaclePool[0];
        obstaclePool.RemoveAt(0);
        obstacle.SetActive(true);

        //Reset obstacle velocity and rotation
        Rigidbody rb = obstacle.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.rotation = Quaternion.identity;
        }

        return obstacle;
    }

    public void Return(GameObject obstacle)
    {
        Rigidbody rb = obstacle.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.rotation = Quaternion.identity;
        }

        obstacle.SetActive(false);
        obstaclePool.Add(obstacle);
    }
}
