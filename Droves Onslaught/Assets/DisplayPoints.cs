using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayPoints : MonoBehaviour
{
    private Tower TowerScript;
    private TextMeshProUGUI UGUI;
    void Start()
    {
        TowerScript = GameObject.FindGameObjectWithTag("Tower").GetComponent<Tower>();
        UGUI = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        UGUI.text = TowerScript.Score.ToString();
    }
}
