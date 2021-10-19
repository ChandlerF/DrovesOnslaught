using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonInfo : MonoBehaviour
{
    public int Cost;

    public Sprite Sprite;

    public string Name;

    private void Start()
    {
        Sprite = GetComponent<SpriteRenderer>().sprite;
    }
}
