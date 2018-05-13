using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombies : MonoBehaviour, IEnemy
{
    public float attackSpeed;
    private float timer;
    private bool attack;
    private PlayerHealthManager phm;
    private GameObject target;
    private int attackDamage;
    private float hitPoints;
    private float moveSpeed;
    
    
    public GameObject pickableObject;
    
    public int AttackDamage
    {
        get { return attackDamage; }
        private set { attackDamage = value; }
    }

    public float HitPoints
    {
        get { return hitPoints; }
        set { hitPoints = value; }
    }

    public float MoveSpeed
    {
        get { return moveSpeed; }
        set { moveSpeed = value; }
    }

    // Use this for initialization
    public void Start()
    {
        HitPoints = 100;
        AttackDamage = 5;
        MoveSpeed = 1.5f;
        target = GameObject.FindGameObjectWithTag("Player");
        phm = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealthManager>();
        attack = true;
    }

    // Update is called once per frame
    public void Update()
    {
        //Rotate to look at player
        transform.LookAt(target.transform.position);
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

        if (HitPoints <= 0)
        {
            Destroy(gameObject);
            var instaniatedPrefab = Instantiate(pickableObject, transform.position, transform.rotation);
            instaniatedPrefab.transform.localScale = new Vector3(2, 2, transform.position.z); //Scales up the object
        }
       
        if (Vector3.Distance(transform.position, target.transform.position) < 7f)
        {
            if (Vector3.Distance(transform.position, target.transform.position) > .6f)
            {       //move if distance from target is greater than 0.6
                transform.Translate(new Vector2(moveSpeed * Time.deltaTime, 0));
            }
            else
            {
                if (attack)
                {
                    phm.HurtPlayer(attackDamage);
                    attack = false;
                }
            }
        }       
    }
    
    
}