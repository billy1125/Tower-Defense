using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    public string TargetTagName; //目標物標籤名稱
    public float DetectMinDistance = 10; //最短的偵測距離

    public float Span = 1.0f;
    public float Delta = 0;
    public GameObject FirePoint;
    public GameObject BulletPrefab;

    private Transform target;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f); //另一種定時函數，你可以設定多少時間後，每一段時間重複做一件事
        InvokeRepeating("FireBullet", 0f, 1f);
    }

    void UpdateTarget() //追蹤最近的目標物
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

    void FireBullet()
    {
        if (target != null)
            Instantiate(BulletPrefab, FirePoint.transform.position, FirePoint.transform.rotation);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (target != null) //讓砲塔對準目標物
        {
            Vector3 dir = target.transform.position - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(dir, Vector3.back);
            Vector3 rotation = Quaternion.Lerp(this.transform.rotation, lookRotation, Time.deltaTime * 5).eulerAngles;
            this.transform.rotation = Quaternion.Euler(0, 0, rotation.z);
        }
    }
}
