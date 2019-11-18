using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageCreater : MonoBehaviour {

    //Itemクラスの宣言
    public class Item
    {
        public int xPosition;
        public int zPosition;

        public Item(int x, int z)
        {
            this.xPosition = x;
            this.zPosition = z;
        }
    }

    //Itemリストの宣言
    public List<Item> ItemList = new List<Item>();

    //妨害アイテムの最大生成数
    private int ItemLimit = 10;


    //z方向の最大値/最小値(int型にするため整数値、本来はかける0.5でfloatにキャストしてposition化する。)
    private int MaxVertical = 8;
    private int MinVertical = -8;

    //x方向の最大値/最小値
    private int MaxHorizontal = 8;
    private int MinHorizontal = -8;
    //ItemのY軸位置
    private float posY = 0.7f;

    //経過時間の変数
    private float seconds = 0f;
    //妨害アイテム生成の基準時間
    private float GenerateTime = 10f;
    //Dotの自動生成時間
    private float DotGenerationTime = 5f;

    //Burgerオブジェクトの割当
    public GameObject BurgerPrefab;
    //pizzaオブジェクト
    public GameObject PizzaPrefab;
    //ご飯オブジェクト
    public GameObject RicePrefab;
    //ダンベルオブジェクト
    public GameObject DumbbellPrefab;

    //Dotオブジェクトの割当
    public GameObject DotPrefab;

    //PlayerPrefab
    public GameObject playerprefab;

    //Gamemanager
    public GameObject gamemanager;


	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        ItemRegeneration();

    }

    public void StageCreation()
    {
        //既に生成されているアイテムの取得(FindGameObjectsWithTagは配列になるので注意）
        GameObject[] Burger = GameObject.FindGameObjectsWithTag("Burger");
        GameObject[] Pizza = GameObject.FindGameObjectsWithTag("Pizza");
        GameObject[] Rice = GameObject.FindGameObjectsWithTag("Rice");
        GameObject[] Dot = GameObject.FindGameObjectsWithTag("Dot");
        GameObject[] Dumbbell = GameObject.FindGameObjectsWithTag("Dumbbell");

        //削除
        foreach (GameObject burger in Burger)
        {
            Destroy(burger);
        }
        foreach (GameObject pizza in Pizza)
        {
            Destroy(pizza);
        }
        foreach (GameObject rice in Rice)
        {
            Destroy(rice);
        }
        foreach (GameObject dot in Dot)
        {
            Destroy(dot);
        }
        foreach (GameObject dumbbell in Dumbbell)
        {
            Destroy(dumbbell);
        }

        //ItemListのクリア
        ItemList.Clear();

        //ハンバーガー生成
        for (int i = 0; i < ItemLimit; i++)
        {
            int ItemDecider = Random.Range(1, 11);
            if (ItemDecider >= 2 && ItemDecider <= 5)
            { //ハンバーガー生成

                int X = Random.Range(-8, 8);
                int Z = Random.Range(-8, 8);

                float posX = X * 0.5f;
                float posZ = Z * 0.5f;

                //プレイヤーの初期位置の座標には生成しない。
                if (X == -8 && Z == -8)
                {
                    continue;
                }


                Instantiate(BurgerPrefab, new Vector3(posX, posY, posZ), Quaternion.identity);
                ItemList.Add(new Item(X, Z));
            }
            if (ItemDecider > 5 && ItemDecider <= 8)
            { //Pizza生成

                int X = Random.Range(-8, 8);
                int Z = Random.Range(-8, 8);

                float posX = X * 0.5f;
                float posZ = Z * 0.5f;

                //プレイヤーの初期位置の座標には生成しない。
                if (X == -8 && Z == -8)
                {
                    continue;
                }


                Instantiate(PizzaPrefab, new Vector3(posX, posY, posZ), Quaternion.identity);
                ItemList.Add(new Item(X, Z));
            }
            if (ItemDecider > 8 && ItemDecider <= 11)
            { //ご飯生成

                int X = Random.Range(-8, 8);
                int Z = Random.Range(-8, 8);

                float posX = X * 0.5f;
                float posZ = Z * 0.5f;

                //プレイヤーの初期位置の座標には生成しない。
                if (X == -8 && Z == -8)
                {
                    continue;
                }

                Instantiate(RicePrefab, new Vector3(posX, posY, posZ), Quaternion.identity);
                ItemList.Add(new Item(X, Z));
            }

        }

        //Dot生成
        for (int Z = MinVertical; Z <= MaxVertical; Z += 1)
        {
            for (int X = MinHorizontal; X <= MaxHorizontal; X += 1)
            {
                float posX = X * 0.5f;
                float posZ = Z * 0.5f;

                //プレイヤーの初期位置の座標には生成しない。
                if(X == -8 && Z == -8)
                {
                    continue;
                }

                if (!ItemExists(X, Z))
                {
                    Instantiate(DotPrefab, new Vector3(posX, posY, posZ), Quaternion.identity);
                    ItemList.Add(new Item(X, Z));
                }

            }
        }

    }

    private bool ItemExists(int x, int z)
    {
        for(int i = 0; i<ItemList.Count; i++)
        {
            if(ItemList[i].xPosition == x && ItemList[i].zPosition== z)
            {
                return true;
            }
        }
        return false;
    }

    public void ItemListUpdate(int x, int z)
    {
        //ItemListに入っている全てxとzが一致しているか確認
        for(int i = 0; i <= ItemList.Count; i++)
        {
            if(ItemList[i].xPosition != x && ItemList[i].zPosition != z)
            {
                ItemList.RemoveAt(i);
                break;
            }
        }
    }

    public void ItemRegeneration()
    {
        for (int i = 0; i < playerprefab.GetComponent<PlayerController>().RegeneList.Count; i++)
        {
            if (Time.time >= playerprefab.GetComponent<PlayerController>().RegeneList[i].DotRegenerationTime)
            {

                int ItemDecider = Random.Range(1,30);
                if (ItemDecider >= 2 && ItemDecider <= 4)
                { //ハンバーガー生成
                    GameObject burger = Instantiate(BurgerPrefab) as GameObject;
                    burger.transform.position = new Vector3(playerprefab.GetComponent<PlayerController>().RegeneList[i].xPosition, posY, playerprefab.GetComponent<PlayerController>().RegeneList[i].zPosition);
                    playerprefab.GetComponent<PlayerController>().RegeneList.RemoveAt(i);

                }
                if (ItemDecider > 4 && ItemDecider <= 6)
                { //Pizza生成
                    GameObject pizza = Instantiate(PizzaPrefab) as GameObject;
                    pizza.transform.position = new Vector3(playerprefab.GetComponent<PlayerController>().RegeneList[i].xPosition, posY, playerprefab.GetComponent<PlayerController>().RegeneList[i].zPosition);
                    playerprefab.GetComponent<PlayerController>().RegeneList.RemoveAt(i);
                }
                if (ItemDecider > 6 && ItemDecider <= 8)
                { //ご飯生成
                    GameObject rice = Instantiate(RicePrefab) as GameObject;
                    rice.transform.position = new Vector3(playerprefab.GetComponent<PlayerController>().RegeneList[i].xPosition, posY, playerprefab.GetComponent<PlayerController>().RegeneList[i].zPosition);
                    playerprefab.GetComponent<PlayerController>().RegeneList.RemoveAt(i);
                }
                if (ItemDecider > 10 && ItemDecider <= 30)
                { //Dot生成
                    GameObject dot = Instantiate(DotPrefab) as GameObject;
                    dot.transform.position = new Vector3(playerprefab.GetComponent<PlayerController>().RegeneList[i].xPosition, posY, playerprefab.GetComponent<PlayerController>().RegeneList[i].zPosition);
                    playerprefab.GetComponent<PlayerController>().RegeneList.RemoveAt(i);

                    dot.transform.Find("DotRegenerateEffect").GetComponent<ParticleSystem>().Play();
                }
                if (ItemDecider > 8 && ItemDecider <= 10)
                { //Dumbbell生成
                    GameObject dumbbell = Instantiate(DumbbellPrefab) as GameObject;
                    dumbbell.transform.position = new Vector3(playerprefab.GetComponent<PlayerController>().RegeneList[i].xPosition, posY, playerprefab.GetComponent<PlayerController>().RegeneList[i].zPosition);
                    playerprefab.GetComponent<PlayerController>().RegeneList.RemoveAt(i);
                }

            }
        }
    }
}


