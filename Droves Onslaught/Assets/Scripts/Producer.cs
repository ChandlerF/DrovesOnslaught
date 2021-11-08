using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Producer : MonoBehaviour
{
    //Line Renderer
    public GameObject TargetBuilding = null;
    private int lengthOfLineRenderer = 2;


    [SerializeField] float StartTimer;
    private float Timer;
    [SerializeField] GameObject Product;

    public int ProductInStock;      //for factories

    [SerializeField] int ProductConsumed = 1;

    private bool HasDrawnLine = false;

    private Animator Anim;


    void Start()
    {
        Anim = GetComponent<Animator>();

        Timer = StartTimer;

        if (TargetBuilding != null)
        {
            MakeLineRenderer(TargetBuilding);
        }
    }


    void Update()
    {
        if((Timer - 0.45f) > 0) //0.45 is how long the pulse animation is
        {
            Timer -= Time.deltaTime;
        }
        else
        {
                //If timer is done:
        }
        {
            if(TargetBuilding != null)
            {
                if (!gameObject.CompareTag("Producer"))     //Factory code:     
                {
                    if (ProductInStock >= ProductConsumed)
                    {
                        Anim.Play("Pulse");
                        Timer = StartTimer;
                    }
                }
                else   //Producer code:
                {
                    Anim.Play("Pulse");
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
            //Remove line renderer component
            Destroy(GetComponent<LineRenderer>());
            HasDrawnLine = false;
        }
    }


    private void SpawnProduct()
    {
        GameObject SpawnedOre = Instantiate(Product, transform.position, Product.transform.rotation);
        SpawnedOre.GetComponent<MoveTowards>().Target = TargetBuilding;
        SpawnedOre.GetComponent<Product>().Target = TargetBuilding;

        if (!gameObject.CompareTag("Producer"))
        {
            ProductInStock -= ProductConsumed;
        }


        SetAnimTrigger();
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
            new GradientColorKey[] { new GradientColorKey(Color.black, 0.0f), new GradientColorKey(Color.black, 1.0f) },
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
        float Multiplier = 0; //0.4f;

        lineRenderer.SetPosition(0, transform.position + Direction * Multiplier);
        lineRenderer.SetPosition(1, Target.transform.position - Direction * Multiplier);
    }

    private void SetAnimTrigger()   //Is called by event when animation is done
    {
        Anim.SetTrigger("Pulse");
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
