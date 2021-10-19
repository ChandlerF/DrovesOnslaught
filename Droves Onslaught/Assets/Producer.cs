using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Producer : MonoBehaviour
{
    //Line Renderer
    public GameObject TargetBuilding = null;
    private int lengthOfLineRenderer = 2;


    //Ore - Now it's called product
    [SerializeField] float StartTimer;
    private float Timer;
    [SerializeField] GameObject Product;

    public int ProductInStock;      //Ore - for factories

    private bool HasDrawnLine = false;


    void Start()
    {
        Timer = StartTimer;

        if (TargetBuilding != null)
        {
            MakeLineRenderer(TargetBuilding);
        }
    }


    void Update()
    {
        if(Timer > 0)
        {
            Timer -= Time.deltaTime;
        }
        else
        {
            if(TargetBuilding != null)
            {
                if (!gameObject.CompareTag("Producer"))
                {
                    if (ProductInStock > 0)
                    {
                        SpawnProduct();
                        Timer = StartTimer;
                    }
                }
                else
                {
                    SpawnProduct();
                    Timer = StartTimer;
                }
            }
        }


        TargetBuilding = GetComponent<MoveTowards>().Target;

        if (TargetBuilding != null)
        {
            if (!HasDrawnLine)
            {
                MakeLineRenderer(TargetBuilding);
                HasDrawnLine = true;
            }
        }
        else
        {
            HasDrawnLine = false;
        }
    }


    private void SpawnProduct()
    {
        GameObject SpawnedOre = Instantiate(Product, transform.position, Product.transform.rotation);
        SpawnedOre.GetComponent<MoveTowards>().Target = TargetBuilding;

        if (!gameObject.CompareTag("Producer"))
        {
            ProductInStock -= 1;
        }
    }

    public void MakeLineRenderer(GameObject Target)
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

        SetLRPos(Target);
    }


    public void SetLRPos(GameObject Target)
    {
        LineRenderer lineRenderer = gameObject.GetComponent<LineRenderer>();


        Vector3 Direction = (Target.transform.position - transform.position).normalized;

        //Higher the number, the bigger the gap
        float Multiplier = 0.4f;

        lineRenderer.SetPosition(0, transform.position + Direction * Multiplier);
        lineRenderer.SetPosition(1, Target.transform.position - Direction * Multiplier);
    }





    /*
     * 
     *         //Set positions

        Vector3 Direction = (Target.transform.position - transform.position).normalized;

        //Higher the number, the bigger the gap
        float Multiplier = 0.4f;
        
        lineRenderer.SetPosition(0, transform.position + Direction * Multiplier);
        lineRenderer.SetPosition(1, Target.transform.position - Direction * Multiplier);

    */
}
