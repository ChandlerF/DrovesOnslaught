using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] bool SpawnEnemies = true;

    //Equivalent to "i" in a for loop (for the 3 lists)
    [SerializeField] int Round = 0;

    public bool FluctuateSpawnRate = false;
    

    [SerializeField] List<GameObject> Enemy = new List<GameObject>();
    [SerializeField] List<int> Amount = new List<int>();
    [SerializeField] List<float> StartDelayBefore = new List<float>();

    private List<float> DelayBefore = new List<float>();


    //Could have 4 of these, (top, elft, right, bottom) so you don't have to make a circle, it's similar to a circle
    [SerializeField] Vector2 SpawnX;
    [SerializeField] Vector2 SpawnY;

    private Arrays ListScript;


    [SerializeField] bool RestartWhenRoundsFinish = true;
    [SerializeField] bool OnRestartUpgradeEnemies = true;

    [SerializeField] bool MenuSpawner = false;

    [SerializeField] GameObject SwarmGO;
    [SerializeField] GameObject FighterGO;
    [SerializeField] GameObject BruteGO;
    [SerializeField] GameObject TankGO;





    private void Start()
    {
        ListScript = GameObject.FindGameObjectWithTag("Manager").GetComponent<Arrays>();

        SetCurrentDelayList();

        if(Enemy.Count != Amount.Count || Amount.Count != StartDelayBefore.Count)
        {
            Debug.LogError("Enemy Spawner Inconsistencies");


            SetListToMin();
        }

        if(LevelManager.instance != null && !MenuSpawner)
        {
            //If currentLevel's second star is earned already       //So they can't earn 3rd star until they beat it without casualties
            if (LevelManager.instance.Stars[LevelManager.instance.CurrentLevel][1] == true)
            {
                FluctuateSpawnRate = true;
            }
        }
    }
    
    private void SetListToMin()
    {
        List<int> Counts = new List<int>();
        Counts.Add(Enemy.Count);
        Counts.Add(Amount.Count);
        Counts.Add(StartDelayBefore.Count);

        int Min = Counts.Min();

        while (Enemy.Count > Min)
        {
            Enemy.RemoveAt(Enemy.Count - 1);
        }
        while (Amount.Count > Min)
        {
            Amount.RemoveAt(Amount.Count - 1);
        }
        while (StartDelayBefore.Count > Min)
        {
            StartDelayBefore.RemoveAt(StartDelayBefore.Count - 1);
        }
    }
    
    private void Update()
    {
        if (SpawnEnemies)
        {
            if (Round < Enemy.Count)
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

            else if (RestartWhenRoundsFinish)
            {
                //Either restart and set round to 0 (and maybe change swarm enemies to brutes?)
                //Or repeat the last round forever, but increase enemy amount with round number (like old spawn system)
                //Or nothing and just have a lot of rounds per level

                if (OnRestartUpgradeEnemies)
                {
                    for (int i = 0; i < Enemy.Count; i++)
                    {
                        if (Enemy[i] == SwarmGO)
                        {
                            Enemy[i] = FighterGO;
                        }
                        else if (Enemy[i] == FighterGO)
                        {
                            Enemy[i] = BruteGO;
                        }
                    }
                }



                SetCurrentDelayList();
                Round = 0;
            }
            else
            {
                if(EnemyCount() <= 0)
                {
                    //GameOver
                    ListScript.gameObject.GetComponent<Player>().GameOver();
                }
            }
        }
    }

    
    private int EnemyCount()
    {
        int x = 0;

        for(int i = 0; i < ListScript.AllEnemyNames.Count; i++)
        {
            x += ListScript.BuildingDict[ListScript.AllEnemyNames[i]].Count;
        }

        return x;
    }

    private void SetCurrentDelayList()
    {
        //Clear list, then add start values (can't do start=current, it makes both lists share values constantly)
        DelayBefore.Clear();
        for (int i = 0; i < StartDelayBefore.Count; i++)
        {
            DelayBefore.Add(StartDelayBefore[i]);
        }
    }

    private void Spawn(GameObject enemy, int amount)
    {

        if (FluctuateSpawnRate)
        {
            amount += Random.Range(1, 4);
        }



        for (int i = 0; i < amount; i++)
        {
            float x = Random.Range(SpawnX.x, SpawnX.y);
            float y = Random.Range(SpawnY.x, SpawnY.y);

            Vector2 SpawnPos = new Vector2(x, y);

            GameObject SpawnedEnemy = Instantiate(enemy, SpawnPos, Quaternion.identity);

            SpawnedEnemy.GetComponent<FindBuildings>().ListScript = ListScript;
            //Add enemy to building dict
        }



        Round += 1;
    }
}
