using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayPoints : MonoBehaviour
{
    private Player PlayerScript;
    private TextMeshProUGUI UGUI;
    void Start()
    {
        PlayerScript = GameObject.FindGameObjectWithTag("Manager").GetComponent<Player>();
        UGUI = GetComponent<TextMeshProUGUI>();

        if (!PlayerScript.UsesPoints)
        {
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        UGUI.text = PlayerScript.Points.ToString();
    }
}
