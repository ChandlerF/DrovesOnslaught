using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Producer : MonoBehaviour
{
    //Line Renderer
    public GameObject TargetBuilding = null;
    private int lengthOfLineRenderer = 2;

    //Markets:
    private Player PlayerScript;
    public bool IsMarket = false;
    [SerializeField] int MarketScrap;

    [SerializeField] float InitialStartTimer;
    [SerializeField] float StartTimer;
    private float Timer;
    [SerializeField] GameObject Product;

    public int ProductInStock;      //for factories

    [SerializeField] int ProductConsumed = 1;

    private bool HasDrawnLine = false;

    private Animator Anim;


    [SerializeField] bool SpawnDecay = true;
    private float MaxDelay = 1.5f;


    void Start()
    {
        Anim = GetComponent<Animator>();
        PlayerScript = GameObject.FindGameObjectWithTag("Manager").GetComponent<Player>();

        Timer = InitialStartTimer;
        StartTimer = InitialStartTimer;
        MaxDelay = InitialStartTimer * 1.75f;

        if (TargetBuilding != null)
        {
            MakeLineRenderer(TargetBuilding);
        }
    }


    void Update()
    {

        TargetBuilding = GetComponent<MoveTowards>().Target;

        if ((Timer - 0.45f) > 0) //0.45 is how long the pulse animation is
        {
            Timer -= Time.deltaTime;
        }

        //When Timer Is Done:
        else
        {
            //has target || is market:
            if (TargetBuilding != null || IsMarket)
            {
                if (!gameObject.CompareTag("Producer"))     //Factory code:     
                {
                    //If has product
                    if (ProductInStock >= ProductConsumed)
                    {
                        Anim.Play("Pulse");
                        Timer = StartTimer;
                    }
                }
                else   //Miner code:
                {
                    Anim.Play("Pulse");
                    Timer = StartTimer;
                }
            }
        }





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
            //Remove line renderer component when theres no target
            Destroy(GetComponent<LineRenderer>());
            HasDrawnLine = false;
        }
    }


    private void SpawnProduct()
    {
        //if market and not tethered
        if (IsMarket && TargetBuilding == null)
        {
            //Add scrap
            GetComponent<Health>().SpawnText(MarketScrap);
        }

        else if(TargetBuilding != null)
        {
            GameObject SpawnedOre = Instantiate(Product, transform.position, Product.transform.rotation);
            SpawnedOre.GetComponent<MoveTowards>().Target = TargetBuilding;
            SpawnedOre.GetComponent<Product>().Target = TargetBuilding;

            if (!gameObject.CompareTag("Producer"))
            {
                ProductInStock -= ProductConsumed;
            }
        }



        SetAnimTrigger();
    }

    public void MakeLineRenderer(GameObject Target)
    {
        if (!GetComponent<LineRenderer>())
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


            SetSpawnRate(Target);
        }

    }

    public void SetLRPos(GameObject Target)
    {
        //if there's a line renderer
        if (gameObject.GetComponent<LineRenderer>())
        {
            LineRenderer lineRenderer = gameObject.GetComponent<LineRenderer>();


            Vector3 Direction = (Target.transform.position - transform.position).normalized;

            //Higher the number, the bigger the gap
            float Multiplier = 0; //0.4f;

            lineRenderer.SetPosition(0, transform.position + Direction * Multiplier);
            lineRenderer.SetPosition(1, Target.transform.position - Direction * Multiplier);

            SetSpawnRate(Target);
        }
        else
        {
            MakeLineRenderer(Target);
        }
    }
    //0.75 speed    1.3     5
    private void SetAnimTrigger()   //Is called by event when animation is done
    {
        Anim.SetTrigger("Pulse");
    }


    private void SetSpawnRate(GameObject target)
    {
        if (SpawnDecay)
        {
            float dist = Vector3.Distance(transform.position, target.transform.position);

            float maxDist = 6;  //A rough estimate of the max range

            float percent = dist / maxDist;

            float newTimer = (percent * InitialStartTimer) + InitialStartTimer;

            if (newTimer > MaxDelay)
            {
                StartTimer = MaxDelay;
            }
            else
            {

                StartTimer = newTimer;
            }
        }
    }
}
