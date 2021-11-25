using UnityEngine;
using UnityEngine.SceneManagement;


//Is on camera, so in menu the screen continues shaking after scene restarts
public class DontDestroyOnScene : MonoBehaviour
{
    public static DontDestroyOnScene instance;
    public string SceneName;

    public bool StayInSameScene = true;
   
    private void Awake()
    {
        SceneName = SceneManager.GetActiveScene().name;

        //If I'm the only one of myself in scene
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        //If I'm not the original
        else if (instance != this)
        {
            if (instance.StayInSameScene)
            {
                //If instance is in a scene it didn't start in      (not sure how this works loading in multiple scenes at once)
                if (instance.SceneName != SceneManager.GetActiveScene().name)
                {
                    Destroy(instance.gameObject);
                }
                else
                {
                    Destroy(gameObject);
                }
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
