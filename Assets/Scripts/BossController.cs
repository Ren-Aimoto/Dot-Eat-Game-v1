using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossController : MonoBehaviour {

    private GameObject TargetObject; // 目標オブジェクト
    NavMeshAgent EnemyAgent; //NavMeshAgent
    private Animator myAnimator;

    // Use this for initialization
    void Start () {
        EnemyAgent = GetComponent<NavMeshAgent>();
        TargetObject = GameObject.Find("Playerprefab");
        myAnimator = GetComponent<Animator>();
        myAnimator.SetFloat("Speed", 0.8f);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void FixedUpdate()
    {
        if (EnemyAgent.pathStatus != NavMeshPathStatus.PathInvalid)
        {
            //NavMeshAgentに目的地をセット
            EnemyAgent.SetDestination(TargetObject.transform.position);
        }
    }
}
