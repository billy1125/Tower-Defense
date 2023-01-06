using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank01 : EnemyController // 坦克01的控制程式從EnemyController「繼承」而來
{
 
    public GameObject FirePoint;
    public GameObject BulletPrefab;
    public GameObject TankGun; //坦克砲塔


    private float Span = 1.0f;
    private float Delta = 0;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f); //另一種定時函數，你可以設定多少時間後，每一段時間重複做一件事
    }

    public void FixedUpdate()
    {
        Move();
        UpdateTarget();
        RotateGunTower();
    }

    void RotateGunTower()
    {
        if (target != null) //讓砲塔對準目標物
        {
            Vector3 dir = target.transform.position - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(dir, Vector3.back);
            Vector3 rotation = Quaternion.Lerp(TankGun.transform.rotation, lookRotation, Time.deltaTime * 5).eulerAngles;
            TankGun.transform.rotation = Quaternion.Euler(0, 0, rotation.z);

            FireWeapon();
        }
    }

    // 這裡重新「覆寫（override）」EnemyController的FireWeapon（發射武器）的方法
    // 所謂覆寫，意思就是重寫設計一個方法裡面的程式
    public override void FireWeapon()
    {
        this.Delta += Time.deltaTime; //依照過去產生物件的方式，射擊砲彈
        if (this.Delta > this.Span)
        {
            Instantiate(BulletPrefab, FirePoint.transform.position, FirePoint.transform.rotation);
            this.Delta = 0;
        }
    }

    // 假設你要重新設計碰撞事件處理？該怎麼做？
    //public override void OnTriggerEnter2D(Collider2D collision)    
}
