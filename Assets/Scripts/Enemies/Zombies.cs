using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombies : MonoBehaviour, IEnemy
{
    public float attackSpeed;
    private float timer;
    private bool attack;
    private PlayerHealthManager phm;
    public GameObject pickableObject;

    Rigidbody2D rb = new Rigidbody2D();
    BoxCollider2D bc = new BoxCollider2D();
    SpriteRenderer sr = new SpriteRenderer();

    public int attackDamage
    {
        get { return attackDamage = 5; }
        set { }
    }

    public float hitPoints
    {
        get { return hitPoints = 12312; }
        set { }
    }

    public float moveSpeed
    {
        get { return moveSpeed = 1; }
        set { }
    }

    // Use this for initialization
    void Start()
    {
        
        //SetTarget(GameObject.FindGameObjectWithTag("Player"));
        phm = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealthManager>();
        attack = true;
    }

    // Update is called once per frame
    void Update()
    {
        //Rotate to look at player
        //transform.LookAt(GetTarget().transform.position);
        //transform.Rotate(new Vector2(0, -90), Space.Self);

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

        if (hitPoints <= 0)
        {
            Destroy(gameObject);
            var instaniatedPrefab = Instantiate(pickableObject, transform.position, transform.rotation);
            instaniatedPrefab.transform.localScale = new Vector3(2, 2, transform.position.z); //Scales up the object
        }

        /*
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
        */
        
    }
}