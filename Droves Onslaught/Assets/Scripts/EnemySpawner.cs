using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] bool FluctuateSpawnRate = true;
    

    [SerializeField] List<GameObject> Enemy = new List<GameObject>();
    [SerializeField] List<int> Amount = new List<int>();
    [SerializeField] List<float> DelayBefore = new List<float>();

    //Equivalent to "i" in a for loop (for the 3 lists)
    private int Round = 0;

    [SerializeField] Vector2 SpawnX;
    [SerializeField] Vector2 SpawnY;





    private void Update()
    {
        if(Round < Enemy.Count)
        {
            if (DelayBefore[Round] > 0)
            {
                DelayBefore[Round] -= Time.deltaTime;
            }

            else
            {
                Spawn(Enemy[Round], Amount[Round]);
            }
        }
        /*
        else
        {
            //Either restart and set round to 0 (and maybe change swarm enemies to brutes?)
            //Or repeat the last round forever, but increase enemy amount with round number (like old spawn system)
            //Or nothing and just have a lot of rounds per level
        }*/
    }




    private void Spawn(GameObject enemy, int amount)
    {

        if (FluctuateSpawnRate)
        {
            amount += Random.Range(-1, 1);
        }



        for (int i = 0; i < amount; i++)
        {
            float x = Random.Range(SpawnX.x, SpawnX.y);
            float y = Random.Range(SpawnY.x, SpawnY.y);

            Vector2 SpawnPos = new Vector2(x, y);

            Instantiate(enemy, SpawnPos, Quaternion.identity);
        }



        Round += 1;
    }
}
