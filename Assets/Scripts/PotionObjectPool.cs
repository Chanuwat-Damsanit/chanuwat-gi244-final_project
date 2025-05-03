using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class PotionObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject[] potionPrefabs;
    [SerializeField] private int initialPotionPoolSize;

    private List<GameObject> potionPool = new();
    private static PotionObjectPool potionStaticInstance;

    private void Awake()
    {
        if (potionStaticInstance != null)
        {
            Destroy(potionStaticInstance);
            return;
        }
        potionStaticInstance = this;
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < initialPotionPoolSize; i++)
        {
            CreateNewPotion();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static PotionObjectPool GetPotionInstance()
    {
        return potionStaticInstance;
    }

    private void CreateNewPotion()
    {
        var potionIndex = Random.Range(0, potionPrefabs.Length);
        GameObject potion = Instantiate(potionPrefabs[potionIndex]);
        potion.SetActive(false);
        potionPool.Add(potion);
    }

    public GameObject PotionAcquire()
    {
        if (potionPool.Count == 0)
        {
            CreateNewPotion();
        }

        GameObject potion = potionPool[0];
        potionPool.RemoveAt(0);
        potion.SetActive(true);

        //Reset obstacle velocity and rotation
        Rigidbody rb = potion.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.rotation = Quaternion.identity;
        }

        return potion;
    }

    public void PotionReturn(GameObject potion)
    {
        Rigidbody rb = potion.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.rotation = Quaternion.identity;
        }

        potion.SetActive(false);
        potionPool.Add(potion);
    }
}
