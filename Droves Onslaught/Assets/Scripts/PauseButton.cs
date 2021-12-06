using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseButton : MonoBehaviour
{
    private GameObject Manager;

    private void Start()
    {
        Manager = GameObject.FindGameObjectWithTag("Manager");
    }


    public void PauseGame()
    {
        Manager.GetComponent<Player>().Pause(gameObject);
    }


    public void LoadMenuScreen()
    {
        SceneManager.LoadScene("Menu");

        Time.timeScale = 1;
    }
    
    
    public void LoadLevelSelect()
    {
        Time.timeScale = 1;

        SceneManager.LoadScene("LevelSelect");
    }
    
    

    public void RestartLevel()
    {
        Time.timeScale = 1;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    //Set lvl manager current lvl
    public void LoadNextLevel()
    {
        string NextLvlName = System.IO.Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(SceneManager.GetActiveScene().buildIndex + 1));
        int NextLvl = SceneManager.GetActiveScene().buildIndex + 1;


        if(NextLvlName == "Level" + (LevelManager.instance.CurrentLevel + 1).ToString())
        {
            LevelManager.instance.CurrentLevel += 1;

            Time.timeScale = 1;

            SceneManager.LoadScene(NextLvl);
        }
        else
        {
            LoadLevelSelect();
        }
    }


    public void LoadShop()
    {
        Time.timeScale = 1;

        SceneManager.LoadScene("Shop");
    }
}
