using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class WaveInfo //利用「序列化Serializable」的類別，設計好幾種攻擊波段
{
    public GameObject EnemyStartPoint; //敵人的生成點，可以依照不同敵人類型設定生成點
    public GameObject EnemyTypePrefab; //敵人的類型
    public int Count; //每一個波段需要生成幾隻敵人
    public float Speed; //這個敵人的速度設定
}

public class WaveManager : MonoBehaviour
{
    public GameManager gameManager;
    public WaveInfo[] Waves; //攻擊波段的陣列物件，用來設定多種攻擊波段
    public Text CountDown;

    private bool ProduceEnemy = false;
    private float span = 1.0f;
    private float delta = 0;

    int countdown = 10;
    int i = 0;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Counting", 0f, 1.0f); //另一種定時函數，你可以設定多少時間後，每一段時間重複做一件事
    }

    // Update is called once per frame
    void Update()
    {
        this.delta += Time.deltaTime;
        if (this.delta > this.span)
        {
            //生成敵人的規則：1)現在遊戲生命值大於0；2)倒數完成，可以產生敵人；3)這次的波段敵人數用額還沒有用完
            if (gameManager.Life > 0 && ProduceEnemy == true && Waves[i].Count > 0)
            {
                this.delta = 0;
                Waves[i].EnemyTypePrefab.GetComponent<EnemyController>().Speed = Waves[i].Speed;
                Waves[i].Count -= 1;
                Instantiate(Waves[i].EnemyTypePrefab, Waves[i].EnemyStartPoint.transform.position, Waves[i].EnemyStartPoint.transform.rotation);
            }
        }

        //如果這一波的攻擊敵人數用額已用完，而且攻擊波段還沒完，就開始重新倒數
        if (Waves[i].Count == 0 && i < Waves.Length - 1)
        {
            ProduceEnemy = false;
            countdown = 10;
            InvokeRepeating("Counting", 0, 1.0f); //不斷倒數
            i += 1;
        }
    }

    void Counting()
    {
        countdown -= 1;
        CountDown.text = countdown.ToString();
        if (countdown == 0)
        {
            CancelInvoke(); //結束倒數
            ProduceEnemy = true;
        }
    }

    // 製造坦克
    void MakeTank()
    {
        //Instantiate(EnemyPrefab, StartPoint.transform.position, StartPoint.transform.rotation);
    }
}
