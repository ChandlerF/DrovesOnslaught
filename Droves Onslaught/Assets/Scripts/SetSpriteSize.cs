using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSpriteSize : MonoBehaviour
{

    void Start()
    {
        //Scale = Square root of MaxRange * 2
        float Scale = Mathf.Sqrt(gameObject.GetComponentInParent<FindEnemies>().MaxRange) * 2;

        transform.localScale = new Vector3(Scale, Scale, Scale);
        transform.position = transform.parent.position;

        GameObject.FindGameObjectWithTag("Manager").GetComponent<Arrays>().VisualsList.Add(gameObject);
        gameObject.SetActive(false);
    }
}
