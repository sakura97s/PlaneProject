using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour {
    
    private  MainPlane mainPlane;
    [SerializeField]
    private Transform pk;
    private Vector3 fireDirection;
    private Transform trans;
    [SerializeField ]
    private  Transform barrelTrans;
    [SerializeField]
    private float rotatespeed = 1;
    [SerializeField]
    private float speed = 1;
    [SerializeField]
    private GameObject tankBullet;
    private float  i;


    private void Awake()
    {
        trans = GetComponent<Transform>();     
    }

    private	void Start () {
		
	}
	
	     
    private	void Update () {

        trans.Translate(Vector3.left * Time.deltaTime * speed);
        MonsterBullet();

        if (LevelDirector.Instance.currentPlane == null) return;
        mainPlane = LevelDirector.Instance.currentPlane;
        Fire();
       
    }
    float fireTimer;
    float fireRate = 1f;
    void MonsterBullet()
    {
        
        fireTimer += Time.deltaTime;
        if (fireTimer > fireRate)
        {
            Instantiate(tankBullet, pk.position, barrelTrans.rotation);
            fireTimer = 0;
        }          
    }
    void Fire()
    {

        fireDirection = barrelTrans.transform.position - mainPlane.transform .position;
        fireDirection.z = 0;
        fireDirection = fireDirection.normalized;

        barrelTrans.rotation = Quaternion.RotateTowards(barrelTrans.rotation, Quaternion.FromToRotation(Vector3.up, fireDirection), Time.deltaTime * rotatespeed);
         
    }
}
