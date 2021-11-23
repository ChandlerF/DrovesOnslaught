using UnityEngine;


//Is on camera, so in menu the screen continues shaking after scene restarts
public class DontDestroyOnScene : MonoBehaviour
{
    public static DontDestroyOnScene instance;
   
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
}
