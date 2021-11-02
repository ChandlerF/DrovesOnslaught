using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowards : MonoBehaviour
{
    public GameObject Target;

    [SerializeField] bool MoveTowardsTarget = true;
    [SerializeField] float MoveSpeed = 0.05f;

    [SerializeField] float RotateSpeed = 0.05f;
    [SerializeField] bool RotateTowards = false;

    public float Innaccuracy = 1f;
    public float Offset;

    [SerializeField] bool MoveForwards = false;

    [SerializeField] bool DestroyIfTargetNull = false;

    public bool DoneRotating = false;

    private int SpriteOffset = 90;
    [SerializeField] float AccuracyBeforeShooting = 4;  //Lower number means more accurate befoe shooting


    void FixedUpdate()
    {
        if(Target != null)
        {
            if (MoveTowardsTarget)
            {
                transform.position = Vector2.MoveTowards(transform.position, Target.transform.position, MoveSpeed);
            }


            if (RotateTowards)
            {
                Vector3 vectorToTarget = Target.transform.position - transform.position;
                
                Vector3 NewTargetVector = new Vector3(vectorToTarget.x + Offset, vectorToTarget.y + Offset, 0);
                
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
