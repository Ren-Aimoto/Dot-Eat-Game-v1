using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBlink : MonoBehaviour
{

	public float speed = 1.0f;

    private Text text;
    private float time;

    private GameObject gamemanager;

    // Start is called before the first frame update
    void Start()
    {
        text = this.gameObject.GetComponent<Text>();
        gamemanager = GameObject.Find("GameManager");
    }

    // Update is called once per frame
    void Update()
    {
        textBlink();
    }

    //Alpha値を更新してColorを返す
    Color GetAlphaColor(Color color)
    {
        time += Time.deltaTime * 5.0f * speed;
        color.a = Mathf.Sin(time) * 0.5f + 0.5f;

        return color;
    }

    Color AlphaColorChange(Color color)
    { 
        if (color.a <= 0)
        {
            color.a = 0;
        }
        else
        {
            color.a -= Time.deltaTime;
        }
        
        return color;
    }

    public IEnumerator GoalOpen()
    {
        text.color = GetAlphaColor(text.color);
        yield return new WaitForSeconds(2.2f);
        this.gameObject.SetActive(false);
    }

    public void textBlink()
    {
        if (gamemanager.GetComponent<GameManager>().currentstatus == GameManager.GameStatus.CanGoal)
        {
            StartCoroutine("GoalOpen");
        }
    }

    public void textBlinkforCarsole()
    {
        if (gamemanager.GetComponent<GameManager>().currentstatus == GameManager.GameStatus.TimeAttackonGoing
            || gamemanager.GetComponent<GameManager>().currentstatus == GameManager.GameStatus.ScoreAttackonGoing)
        {
            StartCoroutine("GoalOpen");
        }
    }
}
