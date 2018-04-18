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
    public GameObject pickableObject;

    // Use this for initialization
    void Start()
    {
        SetTarget(GameObject.FindGameObjectWithTag("Player"));
        phm = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealthManager>();
        SetAttackDamage(0);
        SethitPoints(50);
        SetMoveSpeed(1.5f);
                
        Debug.Log(pickableObject);

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
        
        if (GetHitPoints() <= 0)
        {
            Destroy(gameObject);
            var instaniatedPrefab = Instantiate(pickableObject, transform.position, transform.rotation);
            instaniatedPrefab.transform.localScale = new Vector3(2, 2, transform.position.z); //Scales up the object
        }

        if (Vector3.Distance(transform.position, GetTarget().transform.position) < 7f)
        {
            if (Vector3.Distance(transform.position, GetTarget().transform.position) > .6f)
            {       //move if distance from target is greater than 0.6
                transform.Translate(new Vector2(GetMoveSpeed() * Time.deltaTime, 0));
            }
            else
            {
                if (attack)
                {
                    phm.HurtPlayer(GetAttackDamage());
                    attack = false;
                }
            }
        }
    }
}
