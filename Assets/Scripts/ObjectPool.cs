using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [Range(0.1f, 30f)]
    [SerializeField] float spawnTimer = 1f;
    [Range(0, 50)]
    [SerializeField] int poolSize = 5;
    [SerializeField] GameObject enemyPrefab;
    // Start is called before the first frame update

    GameObject[] pool;

    private void Awake()
    {
        PopulatePool();
    }

    void PopulatePool()
    {
        pool = new GameObject[poolSize];
        for (int i = 0; i < poolSize; i++)
        {
            pool[i] = Instantiate(enemyPrefab, transform);
            pool[i].SetActive(false);
        }
    }

    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            EnableObjectInPool();
            yield return new WaitForSeconds(spawnTimer);
        }
    }

    void EnableObjectInPool()
    {
        for (int i = 0; i < pool.Length; i++)
            if (!pool[i].activeSelf)
            {
                pool[i].SetActive(true);
                return;
            }
    }
}
