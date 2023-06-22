using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondEnemyController : MonoBehaviour
{
    FSM<SeconEnemyStateEnnum> fsm;
    State<SeconEnemyStateEnnum> initState;
    private SecondEnemyModel enemyModel;
}
