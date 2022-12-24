using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int Score = 10;             //打爆坦克的分數
    public float Speed = 1.0f;         //移動速度
    public GameObject ExplosionPrefab; //爆炸預置物件

    GameManager gameManager;           //遊戲導演程式

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void FixedUpdate()
    {
        // Vector3.MoveTowards可以讓戰車逐漸靠近目標點
        // transform.position = Vector2.MoveTowards(p2, p1, Speed * Time.deltaTime);
        transform.Translate(0, Speed * Time.deltaTime, 0);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Check Point")
            transform.rotation = Quaternion.Lerp(transform.rotation, collision.transform.rotation, Time.deltaTime * 2);
        else if (collision.gameObject.tag == "Last Point")
        {
            gameManager.UpdateLife(-1);
            Destroy(gameObject);
        }                
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Check Point")
        {
            transform.rotation = collision.transform.rotation;
        }           
    }

    private void OnTriggerEnter2D(Collider2D collision)
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
}
