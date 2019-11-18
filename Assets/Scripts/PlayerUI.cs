using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [SerializeField]
    private Transform targetTfm;

    private RectTransform myRectTfm;

    //PLayerとの間隔
    private Vector3 ArrowOffsetY = new Vector3(0f,1.0f,0.5f);

    //追従するキャラクター情報
    private GameObject player;
    private float playerHeight;

    //gamemanager
    private GameObject gamemanager;

    // Start is called before the first frame update
    void Start()
    {
        gamemanager = GameObject.Find("GameManager");
        player = GameObject.Find("Playerprefab");
        myRectTfm = GetComponent<RectTransform>();
        targetTfm = player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (gamemanager.GetComponent<GameManager>().currentstatus != GameManager.GameStatus.Title &&
             gamemanager.GetComponent<GameManager>().currentstatus != GameManager.GameStatus.Result)
        {
            myRectTfm.position = RectTransformUtility.WorldToScreenPoint(Camera.main, targetTfm.position + ArrowOffsetY);
        }
    }
}
