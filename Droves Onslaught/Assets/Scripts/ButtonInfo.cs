using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonInfo : MonoBehaviour
{
    public int Cost;

    public Sprite Sprite;

    public string Name;

    public List<GameObject> BuildingUpgrades = new List<GameObject>();

    public GameObject RangeVisual;

    public Vector2 Scale = new Vector2(1, 1);

    private void Start()
    {
        Sprite = GetComponent<SpriteRenderer>().sprite;
    }
}
