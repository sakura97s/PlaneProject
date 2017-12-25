using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Boss : MonoBehaviour,IHealth 

{
    [SerializeField ]
    private float speed = 1;

    private float MaxX;
    //private float MaxY;
    private float MinX;
    //private float MinY;

    private Vector3 direction;
    private Transform trans;

    [SerializeField ]
    private GameObject monster;
    [SerializeField ]
    private int health = 100;
    [SerializeField]
    private GameObject boom;
    public int Health
    {
        get { return health; }
    }


    private void Awake()
    {
        trans = GetComponent<Transform>();
    }
    private void Start()
    {
        MaxX = ScreenXY.MaxX;
        //MaxY = ScreenXY.MaxY;
        MinX = ScreenXY.MinX;
        //MinY = ScreenXY.MinY;
        direction = Vector3.left;
    }
    float i = 1;
    private void Update()
    {
        if (i >= 1)
        {
            i = 0;
            Monster();
        }
        i += Time.deltaTime/4;
        if (transform .position .x >MaxX )
        {
            direction = Vector3.left;
        }
        else if (transform .position .x <MinX )
        {
            direction = Vector3.right;
        }
        Move();
    }

    public  void  OnTriggerEnter2D(Collider2D coll)
    {
           if (coll.tag =="Bullet")
            {
            if (health == 0)
            {
                Destroy(gameObject);
                Instantiate(boom, trans.position, Quaternion.identity);
                LevelDirector.Instance.Score += 10000;
            }
            else if (health > 0)
                { LevelDirector.Instance.Score += 100; }
        }
    }
    void Monster()
    {
        Instantiate(monster, trans.position, Quaternion.identity);
    }
    void Move()
    {
        transform.Translate(direction * Time.deltaTime * speed);
        transform.Translate(Vector3.down * Time.deltaTime * speed / 5);
    }
    public void Damage(int val)
    {
        health -= val;
        print("Boss血量" + Health);
    }
}