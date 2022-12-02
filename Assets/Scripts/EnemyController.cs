using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float Speed = 1.0f; //移動速度
    public GameObject ExplosionPrefab; //爆炸預置物件

    public GameObject[] checkPoints; //簽到點設定

    int i = 0;

    // Start is called before the first frame update
    void Start()
    {
        checkPoints = GameObject.Find("Check Point").GetComponent<CheckPoint>().CheckPoints;
    }

    void FixedUpdate()
    {
        // Vector3.MoveTowards可以讓戰車逐漸靠近目標點
        // transform.position = Vector2.MoveTowards(p2, p1, Speed * Time.deltaTime);
        transform.Translate(0, Speed * Time.deltaTime, 0);
    }

    public virtual void FireWeapon()
    {
        //這裡沒有設計任何的攻擊方法
    }

    public virtual void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Check Point")
          transform.rotation = Quaternion.Lerp(transform.rotation, collision.transform.rotation, Time.deltaTime * 2);
    }

    public virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Check Point")
        {
            transform.rotation = collision.transform.rotation;
            i += 1; //i加1，代表戰車要朝向下一個簽到點前進
        }           
    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet") //如果發生碰撞的物件標籤是Bullet，就產生爆炸，並且刪除自己
        {
            if (ExplosionPrefab != null)
            {
                Instantiate(ExplosionPrefab, this.transform.position, this.transform.rotation);
                Destroy(collision.gameObject); //刪除砲彈
                Destroy(gameObject);
            }
        }
        else if (collision.gameObject.tag == "Finish")
        {
            Destroy(gameObject);
        }
    }
}
