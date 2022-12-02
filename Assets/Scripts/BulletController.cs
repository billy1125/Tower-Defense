using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float Speed = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 2);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(0, Time.deltaTime * Speed, 0);
    }
}
