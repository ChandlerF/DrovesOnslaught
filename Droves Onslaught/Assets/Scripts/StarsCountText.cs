using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StarsCountText : MonoBehaviour
{
    private TextMeshProUGUI Text;
    void Start()
    {
        SetStarsCount();
    }


    public void SetStarsCount()
    {
        Text = GetComponent<TextMeshProUGUI>();

        Text.text = LevelManager.instance.TotalStarsEarned.ToString();
    }
}
