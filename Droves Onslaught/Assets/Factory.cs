using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : MonoBehaviour
{

    [SerializeField] int lengthOfLineRenderer = 2;

    public int Ore;

    [SerializeField] float StartTimer;
    private float Timer;


    [SerializeField] GameObject Ammo;
    [SerializeField] GameObject Weapon;

    

    void Start()
    {
        Timer = StartTimer;
        MakeLineRenderer(Weapon);
    }


    void Update()
    {
        if(Ore > 0)
        {
            if(Timer > 0)
            {
                Timer -= Time.deltaTime;
            }
            else
            {
                if(Weapon != null)
                {
                    SpawnAmmo();
                    Timer = StartTimer;
                }
            }
        }
    }


    private void SpawnAmmo()
    {
        GameObject SpawnedAmmo = Instantiate(Ammo, transform.position, Ammo.transform.rotation);
        SpawnedAmmo.GetComponent<MoveTowards>().Target = Weapon;

        Ore -= 1;
    }


    private void MakeLineRenderer(GameObject Target)
    {
        LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.widthMultiplier = 0.1f;
        lineRenderer.positionCount = lengthOfLineRenderer;


        // A simple 2 color gradient with a fixed alpha of 1.0f.
        float alpha = 1.0f;
        Gradient gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(transform.GetComponent<SpriteRenderer>().color, 0.0f), new GradientColorKey(Target.transform.GetComponent<SpriteRenderer>().color, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) }
        );
        lineRenderer.colorGradient = gradient;
        lineRenderer.sortingLayerName = "LineRenderer";

        //Set positions

        Vector3 Direction = (Target.transform.position - transform.position).normalized;

        //Higher the number, the bigger the gap
        float Multiplier = 0.4f;

        lineRenderer.SetPosition(0, transform.position + Direction * Multiplier);
        lineRenderer.SetPosition(1, Target.transform.position - Direction * Multiplier);
    }
}
