using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowards : MonoBehaviour
{
    public GameObject Target;

    //It's the opposite of Target, this gameobject is the target of shooter
    //Right now shooter does nothing, but can be an optimization, to set it's line renderer to null if target is destroyed (rather than call it for every building)
    public GameObject Shooter;

    [SerializeField] bool MoveTowardsTarget = true;
    [SerializeField] float MoveSpeed = 0.05f;

    [SerializeField] float RotateSpeed = 0.05f;
    [SerializeField] bool RotateTowards = false;

    public float Innaccuracy = 1f;
    public float Offset = 0;

    [SerializeField] bool MoveForwards = false;

    [SerializeField] bool DestroyIfTargetNull = false;

    public bool DoneRotating = false;

    private int SpriteOffset = 90;
    [SerializeField] float AccuracyBeforeShooting = 4;  //Lower number means more accurate befoe shooting

    [SerializeField] float AnimSpeed = 1f; //1 = Normal    -   Higher is faster

    [SerializeField] bool SpeedDecay = false;
    [SerializeField] float DecayMultiplier = 0.00025f;
    [SerializeField] float MinSpeed = 0.02f;

    private void Start()
    {
        if(GetComponent<Animator>() != null)
        {
            GetComponent<Animator>().speed = AnimSpeed;
        }
    }

    void FixedUpdate()
    {
        if(Target != null)
        {
            if (MoveTowardsTarget)
            {
                transform.position = Vector2.MoveTowards(transform.position, Target.transform.position, MoveSpeed);

                if (SpeedDecay && MoveSpeed > MinSpeed)
                {
                    MoveSpeed -= DecayMultiplier;
                }
            }


            if (RotateTowards)
            {
                Vector3 vectorToTarget = Target.transform.position - transform.position;

                float offsetX = Random.Range(-Offset, Offset);
                float offsetY = Random.Range(-Offset, Offset);
                Vector3 NewTargetVector = new Vector3(vectorToTarget.x + offsetX, vectorToTarget.y + offsetY, 0);
                
                //Debug.DrawLine(transform.position, vectorToTarget, Color.blue);
                //Debug.DrawLine(transform.position, NewTargetVector, Color.green);
                
                float angle = Mathf.Atan2(NewTargetVector.y, NewTargetVector.x) * Mathf.Rad2Deg - SpriteOffset;
                Quaternion TargetRot = Quaternion.AngleAxis(angle, transform.forward);
                transform.rotation = Quaternion.Slerp(transform.rotation, TargetRot, Time.deltaTime * RotateSpeed);


                if (Quaternion.Angle(transform.rotation, TargetRot) <= AccuracyBeforeShooting)     //Might need to be more lenient    -   Might need to predict where it'll be (instead of where it is)
                {
                    DoneRotating = true;
                }
                else
                {
                    DoneRotating = false;
                }
            }
        }

        else if (DestroyIfTargetNull)
        {
            Destroy(gameObject);
        }

        if (MoveForwards)
        {
            transform.position += transform.up * MoveSpeed;
        }
    }
}
