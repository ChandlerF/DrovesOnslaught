using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShooting : MonoBehaviour
{
    [SerializeField] GameObject Bullet;
    public int Ammo = 0;

    [SerializeField] float StartTimer;
    private float Timer;

    private MoveTowards RotateScript;

    [SerializeField] int AmmoPerShot;

    private Animator Anim;

    [SerializeField] float DegreeVariation = 5f;

    void Start()
    {
        Anim = GetComponent<Animator>();
        RotateScript = GetComponent<MoveTowards>();
        Timer = StartTimer;
    }


    void Update()
    {
        if((Timer - 0.45f) > 0) // -0.45f because that's how long animation is
        {
            Timer -= Time.deltaTime;
        }
        else
        {
            if(Ammo > AmmoPerShot)
            {
                if (RotateScript.DoneRotating && RotateScript.Target != null)
                {
                    Anim.Play("Shoot");
                    Timer = StartTimer;
                }
            }
        }
    }



    private void Shoot()
    {
        float randomDegree = Random.Range(-DegreeVariation, DegreeVariation);
        Quaternion BulletRot = Quaternion.Euler(0, 0, randomDegree);
        Instantiate(Bullet, transform.position, transform.rotation * BulletRot);
        Ammo -= AmmoPerShot;
        RotateScript.Offset = Random.Range(-RotateScript.Innaccuracy, RotateScript.Innaccuracy);
    }

    private void SetAnimTrigger()   //Is called by event when animation is done
    {
        Anim.SetTrigger("Shoot");
    }
}
