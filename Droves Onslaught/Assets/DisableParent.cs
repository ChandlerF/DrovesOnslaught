using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableParent : MonoBehaviour
{
    public void DiasbleParent()
    {
        transform.parent.parent.parent.gameObject.SetActive(false);
        Time.timeScale = 1;
    }
}
