using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank02 : EnemyController // 坦克02的控制程式也是從EnemyController「繼承」而來
{

    public GameObject[] FirePoints;
    public GameObject BulletPrefab;
    public GameObject TankGun;
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

    // 這裡就覆寫了EnemyController的FireWeapon的方法
    public override void FireWeapon()
    {
        this.Delta += Time.deltaTime;
        if (this.Delta > this.Span)
        {
            foreach (var item in FirePoints) //我們希望第二種坦克一次發射兩顆砲彈，所以我們透過覆寫重新設計
            {
                Instantiate(BulletPrefab, item.transform.position, item.transform.rotation);
            }

            this.Delta = 0;
        }
    }
}
