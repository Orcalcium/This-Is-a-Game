using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField]
    GameObject enemyPrefab,mainRolePrefab;
    [SerializeField]
    int maxEnemy= 10,enemyCount;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        int generateInteval = 10;
        if(Time.time > generateInteval ) 
        {
            Vector3 generatePosition = Vector3.zero;
            generatePosition = new Vector3(Random.Range(1f, 0f), Random.Range(1f, 0f), Random.Range(1f, 0f));
            generatePosition = generatePosition.normalized * Random.Range(50f, 30f);
            generatePosition += enemyPrefab.transform.position;
            Quaternion generateRotation = Quaternion.Euler(0f, Random.Range(360f, 0f), 0f);
            if (enemyCount < maxEnemy) Instantiate(enemyPrefab, generatePosition, generateRotation);
        }
    }
}
