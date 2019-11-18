using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemyControll : MonoBehaviour {

    //Animatorコンポーネント
    public Animator myAnimator;

    //GameManager
    private GameObject gamemanager;

    //敵キャラの移動スピード
    private float movespeed = 0.03f;

    //敵キャラは今+/-のどちらに移動しているのか
    private bool IsMovePlus = true;


	// Use this for initialization
	void Start () {
        myAnimator = GetComponent<Animator>();
        gamemanager = GameObject.Find("GameManager");
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void FixedUpdate()
    {     
        EnemyMove();
    }


    private void EnemyMove()
    {
        if (gamemanager.GetComponent<GameManager>().currentstatus == GameManager.GameStatus.ScoreAttackonGoing
            || gamemanager.GetComponent<GameManager>().currentstatus == GameManager.GameStatus.TimeAttackonGoing
            || gamemanager.GetComponent<GameManager>().currentstatus == GameManager.GameStatus.CanGoal)
        {
            myAnimator.SetFloat("Speed", 1);
            Vector3 Pos = this.transform.position;
            if (IsMovePlus)
            {
                if (gameObject.tag == "HorizontalEnemy")
                {
                    this.transform.rotation = Quaternion.Euler(0, 90, 0);
                    this.transform.position = new Vector3(Pos.x + movespeed, Pos.y, Pos.z);
                    if (this.transform.position.x > 4f)
                    {
                        IsMovePlus = false;
                    }
                }
                if (gameObject.tag == "VerticalEnemy")
                {
                    this.transform.rotation = Quaternion.Euler(0, 0, 0);
                    this.transform.position = new Vector3(Pos.x, Pos.y, Pos.z + movespeed);
                    if (this.transform.position.z > 4f)
                    {
                        IsMovePlus = false;
                    }
                }
            }
            if (!IsMovePlus)
            {
                if (gameObject.tag == "HorizontalEnemy")
                {
                    this.transform.rotation = Quaternion.Euler(0, 270, 0);
                    this.transform.position = new Vector3(Pos.x - movespeed, Pos.y, Pos.z);
                    if (this.transform.position.x < -4f)
                    {
                        IsMovePlus = true;
                    }
                }
                if (gameObject.tag == "VerticalEnemy")
                {
                    this.transform.rotation = Quaternion.Euler(0, 180, 0);
                    this.transform.position = new Vector3(Pos.x, Pos.y, Pos.z - movespeed);
                    if (this.transform.position.z < -4f)
                    {
                        IsMovePlus = true;
                    }
                }
            }
        }
    }
}
