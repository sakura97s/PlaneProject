using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract  class BulletC : MonoBehaviour
{
    [SerializeField]
    protected  float speed = 1f;
    [SerializeField]
    protected int power = 1;
    private string myTag;
    protected  Transform trans;

   
    private void Awake()
    {
        trans = GetComponent<Transform>();
        myTag = gameObject.tag;
    }
    private void Start()
    {
        Destroy(gameObject, 5);
    }

    private void Update()
    {
        Move();       
    }
    protected abstract void Move();
    protected virtual  void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<IHealth>() != null && !collision.CompareTag(myTag))
            collision.GetComponent<IHealth>().Damage(power);
        if (collision.tag != "Bullet" && collision.tag != "Coin"&& !collision.CompareTag(myTag))
            Destroy(this.gameObject);
    }
}
