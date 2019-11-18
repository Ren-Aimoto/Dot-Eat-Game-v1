using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour {

    //GameManager
    public GameObject gamemanager;

    //StageCreater
    public GameObject stagecreater;

    //EnemyGenerator
    public GameObject enemygenerator;

    //ボタン群
    public GameObject PlayButton;
    public GameObject HowToButton;
    public GameObject BackButton;
    public GameObject TimeAttackButton;
    public GameObject ScoreAttackButton;
    public GameObject BacktoTitleButton;
    public GameObject TryAgainButton;
    public GameObject RankingButton;

    //How to Play UI
    public GameObject HowtoText;
    public GameObject HowtoBG;

    //TitleText
    public GameObject TitleText;

    //GameModeSelect UI
    public GameObject GamemodeBG;
    public GameObject TextGamemodeSelect;

    //ResultUI
    public GameObject ResultBG;
    public GameObject ResultText;

    //クリック時サウンドのオーディオソース
    public AudioSource ClickSound;
    //AudioClip
    public AudioClip ClickYes;
    public AudioClip ClickNo;

    //タイトルBGMのオーディオソース
    public AudioSource audioSource;
    public AudioClip TitleBGM;

    //カメラオブジェクト
    private GameObject Camera;




    // Use this for initialization
    void Start () {
        Camera = GameObject.Find("Main Camera");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PushPlayButton()
    {
        GamemodeBG.SetActive(true);
        TextGamemodeSelect.SetActive(true);
        PlayButton.SetActive(false);
        HowToButton.SetActive(false);
        TitleText.SetActive(false);
        ResultBG.SetActive(false);
        ResultText.SetActive(false);
        gamemanager.GetComponent<GameManager>().currentstatus = GameManager.GameStatus.Title;
        ClickSound.clip = ClickYes;
        ClickSound.PlayOneShot(ClickSound.clip);

        //カメラ位置で表示する背景を変える
        if(Camera.transform.position.y < 15f)
        {
            ResultBG.SetActive(true);
            GamemodeBG.SetActive(false);
        }

    }

    public void PushHowtoButton()
    {
        HowtoText.SetActive(true);
        HowtoBG.SetActive(true);
        PlayButton.SetActive(false);
        HowToButton.SetActive(false);
        TitleText.SetActive(false);
        ClickSound.clip = ClickYes;
        ClickSound.PlayOneShot(ClickSound.clip);
    }

    public void PushBackButton()
    {
        HowtoText.SetActive(false);
        HowtoBG.SetActive(false);
        PlayButton.SetActive(true);
        HowToButton.SetActive(true);
        TitleText.SetActive(true);
        ResultText.SetActive(false);
        gamemanager.GetComponent<GameManager>().currentstatus = GameManager.GameStatus.Title;
        ClickSound.clip = ClickNo;
        ClickSound.PlayOneShot(ClickSound.clip);
        audioSource.clip = TitleBGM;
        audioSource.Play();

        //カメラ位置で表示する背景を変える
        if (Camera.transform.position.y < 15f)
        {
            ResultBG.SetActive(true);
        }
    }

    public void PushTryAgainButton()
    {
        GamemodeBG.SetActive(true);
        TextGamemodeSelect.SetActive(true);
        ResultBG.SetActive(true);
        ResultText.SetActive(false);
        ClickSound.clip = ClickYes;
        ClickSound.PlayOneShot(ClickSound.clip);
    }


    public void PushTimeAttackButton()
    {
        stagecreater.GetComponent<StageCreater>().StageCreation();
        enemygenerator.GetComponent<EnemyGenerator>().EnemyGenerate();
        ResultBG.SetActive(false);
        GamemodeBG.SetActive(false);
        TextGamemodeSelect.SetActive(false);
        gamemanager.GetComponent<GameManager>().currentstatus = GameManager.GameStatus.CountForTimeAttack;
        gamemanager.GetComponent<GameManager>().GameStart();
        ClickSound.clip = ClickYes;
        ClickSound.PlayOneShot(ClickSound.clip);
    }

    public void PushScoreAttackButton()
    {
        stagecreater.GetComponent<StageCreater>().StageCreation();
        enemygenerator.GetComponent<EnemyGenerator>().EnemyGenerate();
        ResultBG.SetActive(false);
        GamemodeBG.SetActive(false);
        TextGamemodeSelect.SetActive(false);
        gamemanager.GetComponent<GameManager>().currentstatus = GameManager.GameStatus.CountForScoreAttack;
        gamemanager.GetComponent<GameManager>().GameStart();
        ClickSound.clip = ClickYes;
        ClickSound.PlayOneShot(ClickSound.clip);
    }

}
