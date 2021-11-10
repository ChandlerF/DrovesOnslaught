using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayPoints : MonoBehaviour
{
    private Player MoneyScript;
    private TextMeshProUGUI UGUI;
    void Start()
    {
        MoneyScript = GameObject.FindGameObjectWithTag("Manager").GetComponent<Player>();
        UGUI = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        UGUI.text = MoneyScript.Points.ToString();
    }
}
