using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{
    [SerializeField] GameObject[] enemyPrefab;
    [SerializeField] GameObject player;
    [SerializeField] List<GameObject> platformList = new List<GameObject>();
    [SerializeField] GameObject platformPrefab;
    [SerializeField] int numberOfPlatformToSpawn;
    [SerializeField] float width;
    float height = -2.5f;
    [SerializeField] float heightIncrease;
    private void Start()
    {
        GeneratePlatforms();
    }
    private void Update()
    {
        if (player.transform.position.y >= height/2)
        {
            GeneratePlatforms();
        }
    }

    int GetRandomNumber(int num1,int num2)
    {
        int resutl = Random.Range(num1,num2);
        return resutl;
    }
    void GeneratePlatforms()
    {
        for (int i = 0; i < numberOfPlatformToSpawn; i++)
        {
            GameObject newPlatform = Instantiate(platformPrefab, new Vector2(Random.Range(-width, width), height), Quaternion.identity);
            height += heightIncrease;
            platformList.Add(newPlatform);
            int chance01 = GetRandomNumber(0, 100);
            int chance02 = GetRandomNumber(0, 100);
            if (chance01 > 85)
            {
                Instantiate(enemyPrefab[0], new Vector2(newPlatform.transform.position.x, newPlatform.transform.position.y + 0.64f), Quaternion.identity);
            }
            if(chance02 > 95)
            {
                Instantiate(enemyPrefab[1], new Vector2(newPlatform.transform.position.x, newPlatform.transform.position.y + 1.64f), Quaternion.identity);
            }
        }
    }
}
