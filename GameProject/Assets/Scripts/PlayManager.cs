using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayManager : BaseManager
{
    public GameObject enemy;
    public GameObject canvas;

    public float speed;
    public float speedEnemy;
    public float spawnScreenRange;
    public int enemiesCount;
    public float distanceFromPlayer;

    // Start is called before the first frame update
    void Start()
    {
        GenerateMap(enemiesCount);
    }

    /*
     * Generowanie Mapy - enemies
     */

    private void GenerateMap(int enemiesCount)
    {
        for (int x = 0; x < enemiesCount; x++)
        {
            // Wspolrzednie dla generowanego obiektu
            float spawnX = Random.Range(-spawnScreenRange * Screen.width, spawnScreenRange * Screen.width);
            float spawnY = Random.Range(-spawnScreenRange * Screen.height, spawnScreenRange * Screen.height);

            Vector3 position = new Vector3(spawnX + Screen.width / 2, spawnY + Screen.height / 2, 0);

            // Spawn obiektu
            Instantiate(enemy, position, Quaternion.identity, canvas.transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
