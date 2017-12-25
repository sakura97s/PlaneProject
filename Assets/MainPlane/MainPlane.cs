using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPlane : MonoBehaviour, IHealth {
    private AudioSource Audio;
    private Animator anim;
    [SerializeField]
    private GameObject boom;
    [SerializeField]
    private float speed = 1f;
    [SerializeField]
    private GameObject bullet;
    private float MaxX;
    private float MaxY;
    private float MinY;
    private float MinX;
   
    private Transform trans;
    private Collider2D coll;
    private Vector3 vectorSpeed;
    [SerializeField]
    private int health=1;
    public int Health{
        get { return health; } }

    public delegate void OnDead();
    public event OnDead OnDeadEvent;

    private	void Awake () {
        trans = GetComponent<Transform>();
        Audio = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
        coll.enabled = false;
        StartCoroutine(CollDecorate ());
    }
    private void Start()
    {
        MaxX = ScreenXY.MaxX;
        MaxY = ScreenXY.MaxY;
        MinX = ScreenXY.MinX;
        MinY = ScreenXY.MinY;
      
    }

    private void Update()
    {
        
        ClampFrame();

        if (Input.GetButtonDown("Fire1"))
        {
            Fire1();
        }
        //float i = 1;
        if (Input.GetButton("Fire1"))
        {
            //    if (i >= 1)
            //    {
            //        i = 0;
            //        Fire1();
            //    }
            //    i += Time.deltaTime*5;
            FireStart();
            //StartCoroutine(FireStart ());
        }

        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        Move(direction);
    }
    private void FixUpdate()
    {
       
    }
    private IEnumerator CollDecorate()
    {
        yield return new WaitForSeconds(1.5F);
        coll.enabled = true;
        anim.SetTrigger("doIdle");
    }
    private void Move(Vector3 direction)
    {
        trans.Translate(direction * Time.deltaTime * speed);
    }
    float fireTimer;
    float fireRate = 0.2f;  
       private void Fire1()
    { if (LevelDirector.Instance.PlayerLifeCount <= 0) return;
        Instantiate(bullet, trans.position, Quaternion.identity);
        Audio.Play();
        fireTimer = 0;
    }
    //private IEnumerator FireStart()
    //{
    //    yield return new WaitForSeconds(0.2f);
    //    Instantiate(bullet, trans.position, Quaternion.identity);
    //}
    private void FireStart()
    {
        if (LevelDirector.Instance.PlayerLifeCount <= 0) return;
        fireTimer += Time.deltaTime;
        if (fireTimer > fireRate)
        {
            Instantiate(bullet, trans.position, Quaternion.identity);
            fireTimer = 0;
            Audio.Play();
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other .tag !="Coin")
        {         
            DestroySelf();                
        }
    }
    void ClampFrame()
    {
        trans.position = new Vector3(Mathf.Clamp(trans.position.x, MinX, MaxX),
            Mathf.Clamp(trans.position.y, MinY, MaxY),
            trans.position.z);
    }
    public void Damage(int val)
    {
        health -= val;
    }
    public void DestroySelf()
    {
        if (OnDeadEvent != null)
        {
            Instantiate(boom, trans.position, Quaternion.identity);
            OnDeadEvent();
            Destroy(this.gameObject);
    
        }
    }
}

