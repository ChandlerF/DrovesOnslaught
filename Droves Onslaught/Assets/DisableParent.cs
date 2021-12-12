using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableParent : MonoBehaviour
{
    public void DiasbleParent()
    {
        transform.parent.gameObject.SetActive(false);
        Time.timeScale = 1;
    }
}
