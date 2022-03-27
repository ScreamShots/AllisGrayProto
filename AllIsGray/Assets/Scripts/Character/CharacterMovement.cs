using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class CharacterMovement : MonoBehaviour
{
    enum MoveState { NoDestination, Walking, Blocked }

    [SerializeField]
    private NavMeshAgent agent;

    Vector3 destination;
    MoveState moveState;

    public void SetDestination(Vector3 _destination)
    {
        destination = _destination;
        agent.SetDestination(destination);
    }




}
