using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 敵人控制程式的 Class 類別
public class EnemyController : MonoBehaviour
{
    [Tooltip("打爆坦克的分數")] public int Score = 10;
    [Tooltip("移動速度")] public float Speed = 1.0f;               
    [Tooltip("最短的偵測距離")] public float DetectMinDistance = 10; 
    [Tooltip("打爆坦克的分數")] public string TargetTagName;              
    [Tooltip("爆炸預置物件")] public GameObject ExplosionPrefab;        
      
    [Tooltip("打爆坦克的分數")] public string CheckPoint;

    protected Transform target = null;

    public GameManager gameManager;

    // Start is called before the first frame update
    void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void FixedUpdate()
    {
        
    }

    // 坦克移動的方法
    public void Move()
    {
        transform.Translate(0, Speed * Time.deltaTime, 0);
    }

    // 坦克轉彎的方法
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == CheckPoint)
            transform.rotation = Quaternion.Lerp(transform.rotation, collision.transform.rotation, Time.deltaTime * 2);
        else if (collision.gameObject.tag == "Last Point")
        {
            gameManager.UpdateLife(-1);
            Destroy(gameObject);
        }                
    }

    // 離開打卡點的旋轉處理
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == CheckPoint)
        {
            transform.rotation = collision.transform.rotation;
        }           
    }

    // 碰到砲彈的處理
    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet") //如果發生碰撞的物件標籤是Bullet，就產生爆炸，並且刪除自己
        {
            if (ExplosionPrefab != null)
            {
                gameManager.UpdateScore(Score); //更新分數（加分
                gameManager.UpdateMoney(1); //更新金錢（加一塊錢）
                Instantiate(ExplosionPrefab, this.transform.position, this.transform.rotation);
                Destroy(collision.gameObject); //刪除砲彈
                Destroy(gameObject);
            }
        }
    }

    //追蹤最近的目標物
    protected void UpdateTarget() 
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(TargetTagName); //將場景中某一個特定標籤的物件全部找進來
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies) //計算每一個物件與砲塔之間的距離，把最近的物件找出來
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }
        // 如果最近的物件距離是低於最短的偵測距離，才當成是目標物
        if (nearestEnemy != null && shortestDistance <= DetectMinDistance)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }

    // 發射武器的虛擬方法(virtual method)，所謂虛擬方法可以視為敵人的預設行為，你可以不寫東西，也可以像OnTriggerEnter2D有一個預設的動作
    // 如果你要設計一種新敵人，就可以使用預設的攻擊方法，也可以重新設計
    public virtual void FireWeapon()
    {
        //這裡沒有設計任何的攻擊方法
    }    
}
