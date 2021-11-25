using UnityEngine.SceneManagement;
using UnityEngine;


//Restart scene when there's no buildings alive
public class NoBuildings : MonoBehaviour
{
    private Arrays Arr;

    void Start()
    {
        Arr = GetComponent<Arrays>();
    }

    void Update()
    {
        if(Arr.BuildingsList.Count <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
