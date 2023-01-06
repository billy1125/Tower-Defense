using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : EnemyController // 轟炸機的控制程式也是從EnemyController繼承而來
{
    public GameObject BombPrefab; //炸彈預置物件

    private float span = 1.0f;
    private float delta = 0;

    private void Start()
    {
        this.span += Random.Range(0.5f, 2.0f);
        //利用亂數來設計每一架轟炸機出現時，產生炸彈的時間都不同
    }

    public void FixedUpdate()
    {
        Move();  //繼承自EnemyController
        FireWeapon();
    }

    // 這裡就覆寫了繼承自EnemyController的FireWeapon的方法，因為轟炸機是要丟下炸彈，和坦克射擊是完全不同的方式
    public override void FireWeapon()
    {
        this.delta += Time.deltaTime;
        if (this.delta > this.span)
        {
            this.delta = 0;
            Instantiate(BombPrefab, this.transform.position, this.transform.rotation);
        }
    }
}
