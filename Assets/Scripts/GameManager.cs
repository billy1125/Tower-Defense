using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject EnemyPrefab;
    public GameObject StartPoint;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("MakeTank", 0, 1);
       // Invoke("End", 10);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void MakeTank()
    {
        Instantiate(EnemyPrefab, StartPoint.transform.position, StartPoint.transform.rotation);
    }

    void End()
    {
        CancelInvoke();
    }
}
