using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombies : Enemy
{
    public float attackSpeed;
    private float timer;
    private bool attack;
    private Rigidbody2D myRigidBody2D;
    private PlayerHealthManager phm;

    // Use this for initialization
    void Start()
    {
        SetTarget(GameObject.FindGameObjectWithTag("Player"));
        phm = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealthManager>();
        SetAttackDamage(0);
        SethitPoints(50);
        SetMoveSpeed(1.5f);

        attack = true;
    }

    // Update is called once per frame
    void Update()
    {
        //Rotate to look at player
        transform.LookAt(GetTarget().transform.position);
        transform.Rotate(new Vector2(0, -90), Space.Self);

        //need to reset attack even if hes not in range of player
        if (attack == false)
        {
            timer += Time.deltaTime;
            if (timer > attackSpeed)
            {
                attack = true;
                timer = 0;
            }
        }

        if (Vector3.Distance(transform.position, GetTarget().transform.position) < 7f)
        {
            if (Vector3.Distance(transform.position, GetTarget().transform.position) > .6f)
            {       //move if distance from target is greater than 0.6
                transform.Translate(new Vector2(GetMoveSpeed() * Time.deltaTime, 0));
            }
            else
            {
                if (attack == true)
                {
                    phm.HurtPlayer(GetAttackDamage());
                    HurtEnemy(50);
                    attack = false;
                }
            }
        }
    }
}
