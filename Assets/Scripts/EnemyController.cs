using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {

    private GameObject TargetObject; // 目標オブジェクト
    NavMeshAgent EnemyAgent; //NavMeshAgent
    private Animator myAnimator;

    //GameManager
    private GameObject gamemanager;
    

	// Use this for initialization
	void Start () {
        EnemyAgent = GetComponent<NavMeshAgent>();
        TargetObject = GameObject.Find("Playerprefab");
        myAnimator = GetComponent<Animator>();
        gamemanager = GameObject.Find("GameManager");


	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void FixedUpdate()
    {
        if (gamemanager.GetComponent<GameManager>().currentstatus == GameManager.GameStatus.ScoreAttackonGoing
            || gamemanager.GetComponent<GameManager>().currentstatus == GameManager.GameStatus.TimeAttackonGoing
            || gamemanager.GetComponent<GameManager>().currentstatus == GameManager.GameStatus.CanGoal)
        {
            myAnimator.SetFloat("Speed", 0.21f);
            if (EnemyAgent.pathStatus != NavMeshPathStatus.PathInvalid)
            {
                //NavMeshAgentに目的地をセット
                EnemyAgent.SetDestination(TargetObject.transform.position);
            }
        }
    }

}
