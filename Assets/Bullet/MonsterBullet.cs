using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBullet : MonoBehaviour
{
    [SerializeField]
    private float speed = 1f;
    private Transform trans;
    
    private void Awake()
    {
        trans = GetComponent<Transform>();
    }
    private void Start()
    {
        Destroy(gameObject, 10);
    }

    private void Update()
    {
        trans.Translate(Vector3 .down  * Time.deltaTime * speed);

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" || other.tag == "Bullet")
        {
            Destroy(gameObject);
        }
    }
}
