using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemy
{

    int attackDamage { get; set; }
    float hitPoints { get; set; }
    float moveSpeed { get; set; }

    //GameObject target;

}
