using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSpriteSize : MonoBehaviour
{

    void Start()
    {
        //Works, but has weird bug when it's spawned as a child, it's position gets set and it rotates around with it's parent

        //Scale = Square root of MaxRange * 2
        float Scale = Mathf.Sqrt(gameObject.GetComponentInParent<FindEnemies>().MaxRange) * 2;

        transform.localScale = new Vector3(Scale, Scale, Scale);
        transform.position = Vector3.zero;
    }
}
