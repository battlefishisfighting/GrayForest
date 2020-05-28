using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BlackActor
{
    idle,
    run,
    jump,
    climb,
    climb_1,
    climb_2,
    climb_3,
    attack_start,
    attack_ing,
    attack_end,
    hurt,
    seprate,
    vetigo,
    dead
}

public enum BoyActor
{
    idle,
    walk,
    run,
    jump,
    pick,
    pick_ing,
    pick_over,
    suspend,
    falldown,
    hurt,
    attack,
    dead
}

public enum enemyActor
{
    idle,
    walk,
    run,
    patrol,
    alert,
    signal,
    attack,
    hurt,
    vertigo,
    dead
}

public class ActorBase : MonoBehaviour
{
    
}
