using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobController : MonoBehaviour
{
    //Animatorコンポーネント
    public Animator myAnimator;

    //GameManager
    private GameObject gamemanager;

    //敵キャラの移動スピード
    private float movespeed = 0.01f;

    //敵キャラは今+/-のどちらに移動しているのか
    private bool IsMovePlus = true;

    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        gamemanager = GameObject.Find("GameManager");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        MobMove();
    }


    private void MobMove()
    {
        if (gamemanager.GetComponent<GameManager>().currentstatus == GameManager.GameStatus.Title)
        {
            myAnimator.SetFloat("Speed", 0.2f);
            Vector3 Pos = this.transform.position;
            if (IsMovePlus)
            {
                if (gameObject.tag == "HorizontalEnemy")
                {
                    this.transform.rotation = Quaternion.Euler(0, 90, 0);
                    this.transform.position = new Vector3(Pos.x + movespeed, Pos.y, Pos.z);
                    if (this.transform.position.x > 8f)
                    {
                        IsMovePlus = false;
                    }
                }
                if (gameObject.tag == "VerticalEnemy")
                {
                    this.transform.rotation = Quaternion.Euler(0, 0, 0);
                    this.transform.position = new Vector3(Pos.x, Pos.y, Pos.z + movespeed);
                    if (this.transform.position.z > 8f)
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
                    if (this.transform.position.x < -8f)
                    {
                        IsMovePlus = true;
                    }
                }
                if (gameObject.tag == "VerticalEnemy")
                {
                    this.transform.rotation = Quaternion.Euler(0, 180, 0);
                    this.transform.position = new Vector3(Pos.x, Pos.y, Pos.z - movespeed);
                    if (this.transform.position.z < -8f)
                    {
                        IsMovePlus = true;
                    }
                }
            }
        }
    }
}
