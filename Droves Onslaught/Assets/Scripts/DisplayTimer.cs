using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayTimer : MonoBehaviour
{
    private float Timer = 0;
    private TextMeshProUGUI UGUI;
    void Start()
    {
        UGUI = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        Timer += Time.deltaTime;
        UGUI.text = Timer.ToString("F0");
    }
}
