using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CountDown : MonoBehaviour {

    //CountDownText
    public GameObject TextCountDown;

    //player Controller
    private GameObject player;

    //GameManager
    private GameObject Gamemanager;

    //カウントインのオーディオ
    public AudioSource CountInAudioSource;

    public AudioClip Count;
    public AudioClip CountGo;

    //How to メッセージのBG/テキスト
    public GameObject MessageBG;
    public GameObject Messagetext;
    public GameObject DotImage;


    // Use this for initialization
    void Start () {
        player = GameObject.Find("Playerprefab");
        Gamemanager = GameObject.Find("GameManager");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public IEnumerator CountdownCoroutine()
    {
        yield return new WaitForSeconds(1.0f);
        MessageBG.SetActive(true);
        Messagetext.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        DotImage.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        MessageBG.SetActive(false);
        Messagetext.SetActive(false);
        DotImage.SetActive(false);


        TextCountDown.SetActive(true);
        TextCountDown.GetComponent<Text>().text = "3";
        CountInAudioSource.clip = Count;
        CountInAudioSource.Play();
        yield return new WaitForSeconds(1.0f);

        TextCountDown.GetComponent<Text>().text = "2";
        CountInAudioSource.clip = Count;
        CountInAudioSource.Play();
        yield return new WaitForSeconds(1.0f);

        TextCountDown.GetComponent<Text>().text = "1";
        CountInAudioSource.clip = Count;
        CountInAudioSource.Play();
        yield return new WaitForSeconds(1.0f);

        TextCountDown.GetComponent<Text>().text = "Start!";
        CountInAudioSource.clip = CountGo;
        CountInAudioSource.Play();



        //GameStatusをアップデート
        if (Gamemanager.GetComponent<GameManager>().currentstatus == GameManager.GameStatus.CountForScoreAttack)
        {
            Gamemanager.GetComponent<GameManager>().currentstatus = GameManager.GameStatus.ScoreAttackonGoing;
        }
        if (Gamemanager.GetComponent<GameManager>().currentstatus == GameManager.GameStatus.CountForTimeAttack)
        {
            Gamemanager.GetComponent<GameManager>().currentstatus = GameManager.GameStatus.TimeAttackonGoing;
        }


        yield return new WaitForSeconds(1.0f);



        TextCountDown.SetActive(false);
    }
}
