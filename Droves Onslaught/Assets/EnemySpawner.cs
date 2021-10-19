using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField] float StartTimer;
    private float Timer;

    [SerializeField] GameObject Enemy;

    private int Difficulty = 1;

    void Start()
    {
        Timer = StartTimer;
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

        int AmountOfEnemies = Random.Range(1 + Difficulty, 4 + Difficulty);

        for(int i = 0; i < AmountOfEnemies; i++)
        {
            float x = Random.Range(10, 20);
            float y = Random.Range(-15, 15);

            Vector2 Pos = new Vector2(x, y);

            Instantiate(Enemy, Pos, Enemy.transform.rotation);
        }

        Difficulty += 1;
    }
}
