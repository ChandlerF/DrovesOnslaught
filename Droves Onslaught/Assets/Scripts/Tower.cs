using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public int Score = 0;

    private GameObject Manager;

    private void Start()
    {
        Manager = GameObject.FindGameObjectWithTag("Manager");

        Manager.GetComponent<Arrays>().BuildingDict["Tower"].Add(gameObject);
    }

    private void Update()
    {
        Manager.GetComponent<Player>().Points = Score;
    }

/*
    private void OnDestroy()    //Bug where it runs when entering/exiting the editor play mode
    {/*
        if (UnityEditor.EditorApplication.isPlayingOrWillChangePlaymode)
        {
            //GameOver
            Manager.GetComponent<Player>().Pause();
        }

        */





        /*
        Debug.Log("0");     //Called On Exit
        if (!UnityEditor.EditorApplication.isPlayingOrWillChangePlaymode)
        {
            Debug.Log("1");//Called On Exit
            if (Time.frameCount != 0 && Time.renderedFrameCount != 0)//not loading scene
            {
                Debug.Log("2");
            }
        }
        else

        {
            Debug.Log("3");     //Called only when GameObject is destroyed
                

            //Make market cost and make less    -   Weapons cost and make more

            //GameOver
            Manager.GetComponent<Player>().Pause();
        }*/
    //}
}
