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

    [SerializeField] bool MoveForwards = false;

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
                float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - SpriteOffset;
                Quaternion q = Quaternion.AngleAxis(angle, transform.forward);
                transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * RotateSpeed);

                //transform.rotation == q
                //Debug.Log(transform.rotation + " - " + q);
                if (Quaternion.Angle(transform.rotation, q) <= AccuracyBeforeShooting)     //Might need to be more lenient    -   Might need to predict where it'll be (instead of where it is)
                {
                    DoneRotating = true;
                }
                else
                {
                    DoneRotating = false;
                }
            }
        }
        if (MoveForwards)
        {
            transform.position += transform.up * MoveSpeed;
        }
    }
}
