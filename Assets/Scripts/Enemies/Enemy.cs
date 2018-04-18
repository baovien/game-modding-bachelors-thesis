using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour {

    private int attackDamage = 10;
    private float hitPoints = 50f;
    private float moveSpeed = 1.5f;

    private GameObject target;
    //public Animator anim;

    public GameObject GetTarget()
    {
        return target;
    }
    public void SetTarget(GameObject tar)
    {
        target = tar;
    }

    public int GetAttackDamage()
    {
        return attackDamage;
    }
    public void SetAttackDamage(int a)
    {
       attackDamage = a;
    }

    public float GetHitPoints()
    {
        return hitPoints;
    }
    public void SethitPoints(float a)
    {
        hitPoints = a;
    }

    public float GetMoveSpeed()
    {
        return moveSpeed;
    }
    public void SetMoveSpeed(float a)
    {
        moveSpeed = a;
    }

    public void HurtEnemy(float dmg)
    {
        hitPoints -= dmg;
    }
}
