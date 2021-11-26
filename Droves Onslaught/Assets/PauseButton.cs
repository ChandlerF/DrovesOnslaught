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
        SceneManager.LoadScene(0);

        Time.timeScale = 1;
    }
}
