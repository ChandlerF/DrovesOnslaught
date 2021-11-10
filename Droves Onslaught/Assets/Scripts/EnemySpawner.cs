using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField] float StartTimer;
    private float Timer;

    [SerializeField] GameObject Enemy;

    private int Difficulty = 1;

    private Arrays ListScript;
    
    [SerializeField] Vector2 XSpawn = new Vector2(10, 20);
    [SerializeField] Vector2 YSpawn = new Vector2(-15, 15);

    void Start()
    {
        Timer = StartTimer;
        ListScript = GameObject.FindGameObjectWithTag("Manager").GetComponent<Arrays>();
    }

    void Update()
    {
        if(Timer > 0)
        {
            Timer -= Time.deltaTime;
        }
        else
        {
            Spawn();
            Timer = StartTimer;
        }
    }


    private void Spawn()
    {

        int AmountOfEnemies = Random.Range(1 + Difficulty, 3 + Difficulty);

        for(int i = 0; i < AmountOfEnemies; i++)
        {
            float x = Random.Range(XSpawn.x, XSpawn.y);
            float y = Random.Range(YSpawn.x, YSpawn.y);

            Vector2 Pos = new Vector2(x, y);

            GameObject SpawnedEnemy = Instantiate(Enemy, Pos, Enemy.transform.rotation);

            SpawnedEnemy.GetComponent<FindBuildings>().ListScript = ListScript;
        }

        Difficulty += 1;
    }
}
