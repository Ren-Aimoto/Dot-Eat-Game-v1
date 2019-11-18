using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerController : MonoBehaviour {
	//rigidbody取得
	private Rigidbody rigid;
	//アニメーター取得
	private Animator animator;
	//GameManager取得
	public GameObject gameManager;
	//GameClearText取得
	public GameObject gameclearText;
    //StageCreaterオブジェクト取得
    public GameObject StageCreater;
    //体力スライダー
    public Slider slider;
    //衝突時のサウンド再生
    public AudioSource audiosource;
    public AudioSource Dotaudiosource;
    public AudioClip Dotsound;
    public AudioClip EnemyCollisionSound;
    public AudioClip DumbbellSound;


    public class DotRegenerationInfo
    {
        public float DotRegenerationTime;
        public float xPosition;
        public float zPosition;

        public DotRegenerationInfo(float x, float y, float z)
        {
            this.DotRegenerationTime = x;
            this.xPosition = y;
            this.zPosition = z;
        }
    }

    //Dot再生成の時間、座標を格納するリスト
    public List<DotRegenerationInfo> RegeneList = new List<DotRegenerationInfo>();
    //Dot再生成の時間間隔
    private float DotRegenerationGap = 35f;

	private float moveSpeed = 1.8f; //Playerの基本移動速度
	private float rotateSpeed = 1.0f; //回転速度

    //プレイヤーのモチベーション（体力）/MAX体力
    public const float PLAYER_MAX_MOTIVATION = 200;
    public float PlayerMotivation = PLAYER_MAX_MOTIVATION;
    
    //敵に当たった時のモチベーション低下
    private float BurgerMotivation = 20;
    private float PizzaMotivation = 30;
    private float RiceMotivation = 50;
    private float EnemyMotivation = 100;
    //ダンベルによるモチベーション上昇
    private float DumbbellMotivation = 50;

    //時間経過によるモチベーションの回復
    private float AutoMotivationRecovery = 1f;
    //回復間隔
    private int RecoveryTime = 1;

	// Use this for initialization
	void Start () {
		this.animator = GetComponent<Animator>();
		this.rigid = GetComponent<Rigidbody> ();
        this.slider = GameObject.Find("SliderMotivation").GetComponent<Slider>();

        //体力ゲージを満タンにする
        MotivationReflesh();

	
	}
	
	// Update is called once per frame
	void Update () {
        PlayerLifeAutoRecover();

        //体力ゲージを最新化
        slider.value = PlayerMotivation / PLAYER_MAX_MOTIVATION;
    }

	void FixedUpdate(){
		PlayerMovement ();
	}
		

	public void PlayerMovement(){
        //Game Status がプレイ中のみコントロール可能
        if (gameManager.GetComponent<GameManager>().currentstatus == GameManager.GameStatus.TimeAttackonGoing ||
            gameManager.GetComponent<GameManager>().currentstatus == GameManager.GameStatus.ScoreAttackonGoing ||
            gameManager.GetComponent<GameManager>().currentstatus == GameManager.GameStatus.CanGoal)
            
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                //上方向への移動
                this.transform.rotation = Quaternion.Euler(0, 0, 0);
                rigid.velocity = new Vector3(rigid.velocity.x, rigid.velocity.y, moveSpeed);
                this.animator.SetFloat("Speed", 1);
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                //方向への移動
                this.transform.rotation = Quaternion.Euler(0, 180, 0);
                rigid.velocity = new Vector3(rigid.velocity.x, rigid.velocity.y, -moveSpeed);
                this.animator.SetFloat("Speed", 1);
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                //左方向への移動
                this.transform.rotation = Quaternion.Euler(0, 270, 0);
                rigid.velocity = new Vector3(-moveSpeed, rigid.velocity.y, rigid.velocity.z);
                this.animator.SetFloat("Speed", 1);
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                //上方向への移動
                this.transform.rotation = Quaternion.Euler(0, 90, 0);
                rigid.velocity = new Vector3(moveSpeed, rigid.velocity.y, rigid.velocity.z);
                this.animator.SetFloat("Speed", 1);
            }
            if (Input.GetKey(KeyCode.UpArrow))
            {
                //上方向への移動
                this.transform.rotation = Quaternion.Euler(0, 0, 0);
                rigid.velocity = new Vector3(rigid.velocity.x, rigid.velocity.y, moveSpeed);
                this.animator.SetFloat("Speed", 1);
            }
            if (Input.GetKeyUp(KeyCode.DownArrow))
            {
                this.animator.SetFloat("Speed", 0);
            }
            if (Input.GetKeyUp(KeyCode.LeftArrow))
            {
                this.animator.SetFloat("Speed", 0);
            }
            if (Input.GetKeyUp(KeyCode.RightArrow))
            {
                this.animator.SetFloat("Speed", 0);
            }
            if (Input.GetKeyUp(KeyCode.UpArrow))
            {
                this.animator.SetFloat("Speed", 0);
            }
        }
    }

	public void OnTriggerEnter(Collider col){
        //チートデイ発動前の場合
        if (gameManager.GetComponent<GameManager>().IsCheatDay == false)
        {

            if (col.gameObject.tag == "Dot")
            {
                gameManager.GetComponent<GameManager>().ScoreUpdate();
                //Audio再生
                Dotaudiosource.clip = Dotsound;
                Dotaudiosource.Play();
                //アイテム消去
                Destroy(col.gameObject);

                float collisionPosX = col.gameObject.transform.position.x * 2;
                float collisionPosZ = col.gameObject.transform.position.z * 2;

                //Dot再生成の時間をリスト管理（StageCreaterスクリプトでこのリストを呼び出してDot生成を行う。）
                RegeneList.Add(new DotRegenerationInfo(Time.time + DotRegenerationGap, collisionPosX * 0.5f, collisionPosZ * 0.5f));

            }
            else if (col.gameObject.name == "Goal")
            {
                gameclearText.SetActive(true);
                gameclearText.transform.DOScale(new Vector3(1, 1, 1), 1f);
                gameManager.GetComponent<GameManager>().currentstatus = GameManager.GameStatus.CLEAR;
            }
            else if (col.gameObject.tag == "VerticalEnemy" || col.gameObject.tag == "HorizontalEnemy" || col.gameObject.tag == "Enemy")
            {
                gameManager.GetComponent<GameManager>().EnemyCollision();
                //Particle再生
                col.gameObject.transform.Find("DestroyEffect").GetComponent<ParticleSystem>().Play();
                Destroy(col.gameObject);
                this.PlayerMotivation -= EnemyMotivation;
                //Audio再生
                audiosource.clip = EnemyCollisionSound;
                audiosource.Play();
            }
            else if(col.gameObject.tag == "BossEnemy")
            {
                gameManager.GetComponent<GameManager>().BossCollision();
                //Particle再生
                col.gameObject.transform.Find("DestroyEffect").GetComponent<ParticleSystem>().Play();
                Destroy(col.gameObject);
                PlayerMotivation -= EnemyMotivation;
                //Audio再生
                audiosource.clip = EnemyCollisionSound;
                audiosource.Play();
            }
            else if (col.gameObject.tag == "Burger")
            {
                gameManager.GetComponent<GameManager>().BurgerCollision();
                Destroy(col.gameObject);
                int collisionPosX = (int)col.gameObject.transform.position.x * 2;
                int collisionPosZ = (int)col.gameObject.transform.position.z * 2;

                this.PlayerMotivation -= BurgerMotivation;
                //Audio再生
                audiosource.clip = EnemyCollisionSound;
                audiosource.Play();
                //アイテム再生成の時間をリスト管理（StageCreaterスクリプトでこのリストを呼び出してアイテム生成を行う。）
                RegeneList.Add(new DotRegenerationInfo(Time.time + DotRegenerationGap, collisionPosX * 0.5f, collisionPosZ * 0.5f));
            }
            else if (col.gameObject.tag == "Pizza")
            {
                gameManager.GetComponent<GameManager>().PizzaCollision();
                Destroy(col.gameObject);
                int collisionPosX = (int)col.gameObject.transform.position.x * 2;
                int collisionPosZ = (int)col.gameObject.transform.position.z * 2;

                this.PlayerMotivation -= PizzaMotivation;
                //Audio再生
                audiosource.clip = EnemyCollisionSound;
                audiosource.Play();
                //アイテム再生成の時間をリスト管理（StageCreaterスクリプトでこのリストを呼び出してアイテム生成を行う。）
                RegeneList.Add(new DotRegenerationInfo(Time.time + DotRegenerationGap, collisionPosX * 0.5f, collisionPosZ * 0.5f));
            }
            else if (col.gameObject.tag == "Rice")
            {
                gameManager.GetComponent<GameManager>().RiceCollision();
                Destroy(col.gameObject);
                int collisionPosX = (int)col.gameObject.transform.position.x * 2;
                int collisionPosZ = (int)col.gameObject.transform.position.z * 2;

                this.PlayerMotivation -= RiceMotivation;
                //Audio再生
                audiosource.clip = EnemyCollisionSound;
                audiosource.Play();
                //アイテム再生成の時間をリスト管理（StageCreaterスクリプトでこのリストを呼び出してアイテム生成を行う。）
                RegeneList.Add(new DotRegenerationInfo(Time.time + DotRegenerationGap, collisionPosX * 0.5f, collisionPosZ * 0.5f));
            }
            else if (col.gameObject.tag == "Dumbbell")
            {
                Destroy(col.gameObject);
                int collisionPosX = (int)col.gameObject.transform.position.x * 2;
                int collisionPosZ = (int)col.gameObject.transform.position.z * 2;

                this.PlayerMotivation += DumbbellMotivation;
                //Audio再生(あとで変更)
                audiosource.clip = DumbbellSound;
                audiosource.Play();
                //アイテム再生成の時間をリスト管理（StageCreaterスクリプトでこのリストを呼び出してアイテム生成を行う。）
                RegeneList.Add(new DotRegenerationInfo(Time.time + DotRegenerationGap, collisionPosX * 0.5f, collisionPosZ * 0.5f));
            }
        }

        //チートデイ発動後
        if (gameManager.GetComponent<GameManager>().IsCheatDay == true)
        {

            if (col.gameObject.tag == "Dot")
            {
                gameManager.GetComponent<GameManager>().ScoreUpdate();
                Destroy(col.gameObject);
                gameManager.GetComponent<GameManager>().CheatingDaySetup();
                //Audio再生
                Dotaudiosource.clip = Dotsound;
                Dotaudiosource.Play();
                //Particle再生
                col.gameObject.transform.Find("DotDestroyEffect").GetComponent<ParticleSystem>().Play();

                float collisionPosX = col.gameObject.transform.position.x * 2;
                float collisionPosZ = col.gameObject.transform.position.z * 2;

                //Dot再生成の時間をリスト管理（StageCreaterスクリプトでこのリストを呼び出してDot生成を行う。）
                RegeneList.Add(new DotRegenerationInfo(Time.time + DotRegenerationGap, collisionPosX * 0.5f, collisionPosZ * 0.5f));

            }
            else if (col.gameObject.name == "Goal")
            {
                gameclearText.SetActive(true);
                gameclearText.transform.DOScale(new Vector3(1, 1, 1), 1f);
                gameManager.GetComponent<GameManager>().currentstatus = GameManager.GameStatus.CLEAR;
            }
            else if (col.gameObject.tag == "VerticalEnemy" || col.gameObject.tag == "HorizontalEnemy" || col.gameObject.tag == "Enemy")
            {
                gameManager.GetComponent<GameManager>().CheatEnemyCollision();
                //Particle再生
                col.gameObject.transform.Find("DestroyEffect").GetComponent<ParticleSystem>().Play();
                Destroy(col.gameObject);
                //Audio再生
                Dotaudiosource.clip = Dotsound;
                Dotaudiosource.Play();
            }
            else if (col.gameObject.tag == "Burger")
            {
                gameManager.GetComponent<GameManager>().CheatBurgerCollision();
                Destroy(col.gameObject);
                int collisionPosX = (int)col.gameObject.transform.position.x * 2;
                int collisionPosZ = (int)col.gameObject.transform.position.z * 2;

                //Audio再生
                Dotaudiosource.clip = Dotsound;
                Dotaudiosource.Play();
                //アイテム再生成の時間をリスト管理（StageCreaterスクリプトでこのリストを呼び出してアイテム生成を行う。）
                RegeneList.Add(new DotRegenerationInfo(Time.time + DotRegenerationGap, collisionPosX * 0.5f, collisionPosZ * 0.5f));
            }
            else if (col.gameObject.tag == "Pizza")
            {
                gameManager.GetComponent<GameManager>().CheatPizzaCollision();
                Destroy(col.gameObject);
                int collisionPosX = (int)col.gameObject.transform.position.x * 2;
                int collisionPosZ = (int)col.gameObject.transform.position.z * 2;

                //Audio再生
                Dotaudiosource.clip = Dotsound;
                Dotaudiosource.Play();
                //アイテム再生成の時間をリスト管理（StageCreaterスクリプトでこのリストを呼び出してアイテム生成を行う。）
                RegeneList.Add(new DotRegenerationInfo(Time.time + DotRegenerationGap, collisionPosX * 0.5f, collisionPosZ * 0.5f));
            }
            else if (col.gameObject.tag == "Rice")
            {
                gameManager.GetComponent<GameManager>().CheatRiceCollision();
                Destroy(col.gameObject);
                int collisionPosX = (int)col.gameObject.transform.position.x * 2;
                int collisionPosZ = (int)col.gameObject.transform.position.z * 2;

                //Audio再生
                Dotaudiosource.clip = Dotsound;
                Dotaudiosource.Play();
                //アイテム再生成の時間をリスト管理（StageCreaterスクリプトでこのリストを呼び出してアイテム生成を行う。）
                RegeneList.Add(new DotRegenerationInfo(Time.time + DotRegenerationGap, collisionPosX * 0.5f, collisionPosZ * 0.5f));
            }
            else if (col.gameObject.tag == "BossEnemy")
            {
                gameManager.GetComponent<GameManager>().CheatBossCollision();
                //Particle再生
                col.gameObject.transform.Find("DestroyEffect").GetComponent<ParticleSystem>().Play();
                Destroy(col.gameObject);
                PlayerMotivation -= EnemyMotivation;
                //Audio再生
                audiosource.clip = EnemyCollisionSound;
                audiosource.Play();
            }
            else if (col.gameObject.tag == "Dumbbell")
            {
                Destroy(col.gameObject);
                int collisionPosX = (int)col.gameObject.transform.position.x * 2;
                int collisionPosZ = (int)col.gameObject.transform.position.z * 2;

                this.PlayerMotivation += DumbbellMotivation;
                //Audio再生(あとで変更)
                audiosource.clip = DumbbellSound;
                audiosource.Play();
                //アイテム再生成の時間をリスト管理（StageCreaterスクリプトでこのリストを呼び出してアイテム生成を行う。）
                RegeneList.Add(new DotRegenerationInfo(Time.time + DotRegenerationGap, collisionPosX * 0.5f, collisionPosZ * 0.5f));
            }
        }

    }

    public void PlayerLifeAutoRecover()
    {
        if (gameManager.GetComponent<GameManager>().IsCheatDay == false)
        {
            if (PlayerMotivation < PLAYER_MAX_MOTIVATION
                && (gameManager.GetComponent<GameManager>().currentstatus == GameManager.GameStatus.ScoreAttackonGoing || gameManager.GetComponent<GameManager>().currentstatus == GameManager.GameStatus.TimeAttackonGoing))
            {
                PlayerMotivation += AutoMotivationRecovery * Time.deltaTime;
            }
        }
        if (gameManager.GetComponent<GameManager>().IsCheatDay == true)
        {
            if (PlayerMotivation < PLAYER_MAX_MOTIVATION
                && (gameManager.GetComponent<GameManager>().currentstatus == GameManager.GameStatus.ScoreAttackonGoing || gameManager.GetComponent<GameManager>().currentstatus == GameManager.GameStatus.TimeAttackonGoing))
            {
                PlayerMotivation += 2* AutoMotivationRecovery * Time.deltaTime;
            }
        }

    }

    public void MotivationReflesh()
    {
        PlayerMotivation = PLAYER_MAX_MOTIVATION;
    }

}
