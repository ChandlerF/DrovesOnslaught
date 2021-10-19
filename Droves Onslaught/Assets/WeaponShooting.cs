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

    void Start()
    {
        RotateScript = GetComponent<MoveTowards>();
        Timer = StartTimer;
    }


    void Update()
    {
        if(Timer > 0)
        {
            Timer -= Time.deltaTime;
        }
        else
        {
            if(Ammo > AmmoPerShot)
            {
                if (RotateScript.DoneRotating && RotateScript.Target != null)
                {
                    Shoot();
                    Timer = StartTimer;
                }
            }
        }
    }



    private void Shoot()
    {
        Instantiate(Bullet, transform.position, transform.rotation);//Might want to spawn it slighty ahead of the weapon (so it's not inside the weapon)
        Ammo -= AmmoPerShot;
    }
}
