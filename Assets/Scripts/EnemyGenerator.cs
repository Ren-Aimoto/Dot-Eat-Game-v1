using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour {

    //GameManager
    public GameObject gamemanager;

	//Enemyの配置最大数
	private const int MAX_ENEMYNUM = 4;

    //エネミープレハブの読み込み
    public GameObject NavigationEnemyPrefab;
    public GameObject VerticalEnemy;
    public GameObject HorizontalEnemy;
    public GameObject BossEnemyPrefab;

	private float VerticalLimit = 4.0f;
	private float HorizontalLimit = 4.0f;
    //敵キャラのy軸位置
    private float PosY = 0.5f;

    private int MaxEnemyNum = 5;

    private int MaxBossNum = 3;

    private float PassedTime;
    private float EnemyGenerationTime = 5;



	// Use this for initialization
	void Start () {

		
	}
	
	// Update is called once per frame
	void Update () {
        AutoEnemyGeneration();
        
	}

    public void EnemyGenerate()
    {
        //既に生成されている敵の取得

        GameObject[] NavEnemy = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject[] VerEnemy = GameObject.FindGameObjectsWithTag("VerticalEnemy");
        GameObject[] HorEnemy = GameObject.FindGameObjectsWithTag("HorizontalEnemy");
        GameObject[] BossEnemy = GameObject.FindGameObjectsWithTag("BossEnemy");

        //上で取得した敵の削除
        foreach(GameObject nav in NavEnemy)
        {
            Destroy(nav);
        }
        foreach(GameObject ver in VerEnemy)
        {
            Destroy(ver);
        }
        foreach(GameObject hor in HorEnemy)
        {
            Destroy(hor);
        }
        foreach(GameObject boss in BossEnemy)
        {
            Destroy(boss);
        }

        //スタート時にランダムで敵生成
        for (int i = 0; i < MaxEnemyNum; i++)
        {
            int Enemydecider = Random.Range(1, 11);
            if (Enemydecider > 1 && Enemydecider < 5)
            {
                float posX = (int)Random.Range(-8, 8) * 0.5f;
                float posZ = (int)Random.Range(-8, 8) * 0.5f;
                Instantiate(NavigationEnemyPrefab, new Vector3(posX, PosY, posZ), Quaternion.identity);
            }
            else if (Enemydecider >= 5 && Enemydecider < 8)
            {
                float posX = (int)Random.Range(-4, 4);
                float posZ = (int)Random.Range(-8, 8) * 0.5f;
                Instantiate(VerticalEnemy, new Vector3(posX, PosY, posZ), Quaternion.identity);
            }
            else if (Enemydecider >= 8)
            {
                float posX = (int)Random.Range(-8, 8) * 0.5f;
                float posZ = (int)Random.Range(-4, 4);
                Instantiate(HorizontalEnemy, new Vector3(posX, PosY, posZ), Quaternion.identity);
            }
        }
    }

    public void BossGenerate()
    {
            for (int i = 0; i < MaxBossNum; i++)
            {

                float posX = (int)Random.Range(-8, 8) * 0.5f;
                float posZ = (int)Random.Range(10, 13) * 0.5f;
                Instantiate(BossEnemyPrefab, new Vector3(posX,PosY, posZ), Quaternion.identity);
            }
    }

    public void AutoEnemyGeneration()
    {
        if (gamemanager.GetComponent<GameManager>().currentstatus == GameManager.GameStatus.CountForScoreAttack
            || gamemanager.GetComponent<GameManager>().currentstatus == GameManager.GameStatus.CountForTimeAttack)
        {

            PassedTime = 0;
        }

            if (gamemanager.GetComponent<GameManager>().currentstatus == GameManager.GameStatus.TimeAttackonGoing)
        {
            PassedTime += Time.deltaTime;
            if (PassedTime >= EnemyGenerationTime)
            {
                PassedTime = 0;
                int Enemydecider = Random.Range(1, 11);
                if (Enemydecider > 1 && Enemydecider < 5)
                {
                    float posX = (int)Random.Range(-8, 8) * 0.5f;
                    float posZ = (int)Random.Range(-8, 8) * 0.5f;
                    Instantiate(NavigationEnemyPrefab, new Vector3(posX, PosY, posZ), Quaternion.identity);
                }
                else if (Enemydecider >= 5 && Enemydecider < 8)
                {
                    float posX = (int)Random.Range(-4, 4);
                    float posZ = (int)Random.Range(-8, 8) * 0.5f;
                    Instantiate(VerticalEnemy, new Vector3(posX, PosY, posZ), Quaternion.identity);
                }
                else if (Enemydecider >= 8)
                {
                    float posX = (int)Random.Range(-8, 8) * 0.5f;
                    float posZ = (int)Random.Range(-4, 4);
                    Instantiate(HorizontalEnemy, new Vector3(posX, PosY, posZ), Quaternion.identity);
                }

            }
        }

        if (gamemanager.GetComponent<GameManager>().currentstatus == GameManager.GameStatus.ScoreAttackonGoing)
        {
            PassedTime += Time.deltaTime;
            if (PassedTime >= EnemyGenerationTime)
            {
                PassedTime = 0;
                int Enemydecider = Random.Range(1, 13);
                if (Enemydecider > 1 && Enemydecider < 5)
                {
                    float posX = (int)Random.Range(-8, 8) * 0.5f;
                    float posZ = (int)Random.Range(-8, 8) * 0.5f;
                    Instantiate(NavigationEnemyPrefab, new Vector3(posX, PosY, posZ), Quaternion.identity);
                }
                else if (Enemydecider >= 5 && Enemydecider < 8)
                {
                    float posX = (int)Random.Range(-4, 4);
                    float posZ = (int)Random.Range(-8, 8) * 0.5f;
                    Instantiate(VerticalEnemy, new Vector3(posX, PosY, posZ), Quaternion.identity);
                }
                else if (Enemydecider >= 8 && Enemydecider <12)
                {
                    float posX = (int)Random.Range(-8, 8) * 0.5f;
                    float posZ = (int)Random.Range(-4, 4);
                    Instantiate(HorizontalEnemy, new Vector3(posX, PosY, posZ), Quaternion.identity);
                }
                else if (Enemydecider >= 12)
                {
                    float posX = (int)Random.Range(-8, 8) * 0.5f;
                    float posZ = (int)Random.Range(-4, 4);
                    Instantiate(BossEnemyPrefab, new Vector3(posX, PosY, posZ), Quaternion.identity);
                }
            }
        }
    }
}
