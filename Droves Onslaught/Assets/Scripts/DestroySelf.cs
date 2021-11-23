using UnityEngine;

public class DestroySelf : MonoBehaviour
{
    public void Destroy()
    {
        {
            Destroy(transform.parent.gameObject);
        }
    }
}
