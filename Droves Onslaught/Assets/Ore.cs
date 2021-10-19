using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ore : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Factory"))
        {
            collision.GetComponent<Producer>().ProductInStock += 1;
            Destroy(gameObject);
        }
    }
}
