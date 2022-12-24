using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int MaxGameLife = 10; //預設最大生命值
    public int MaxMoney = 100;   //預設最大金錢
    public Text LifeText;
    public Text ScoreText;
    public Text MoneyText;
    public GameObject GameOverUI;

    private int score = 0;

    // 金錢數值
    private int _money = 0;
    public int Money // 存取子
    {
        get // 唯讀
        {
            return _money;
        }
    }

    private int _life;
    public int Life
    {
        get
        {
            return _life;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("MakeTank", 0, 1);

        _life = MaxGameLife;
        LifeText.text = "Total Life: " + _life;
        ScoreText.text = score.ToString();
        _money = MaxMoney;
        MoneyText.text = "$" + _money;
    }

    // Update is called once per frame
    void Update()
    {
        if (_life == 0)
        {
            GameOverUI.SetActive(true);
        }
    }

    //更新生命值
    public void UpdateLife(int _amount)
    {
        if (_life > 0)
        {
            _life += _amount;
            LifeText.text = "Total Life: " + _life;
        }
    }

    //更新分數
    public void UpdateScore(int _amount)
    {
        score += _amount;
        ScoreText.text = score.ToString();
    }

    //更新金錢
    public void UpdateMoney(int _amount)
    {
        _money += _amount;
        MoneyText.text = "$" + _money.ToString();
    }
}
