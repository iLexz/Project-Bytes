using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour {

    NavMeshAgent navAgent;
    GameObject playerObject;

    private void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        playerObject = GameObject.FindWithTag("Player");
    }

}
