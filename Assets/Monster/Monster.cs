using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour {
    [SerializeField]
    private float speed = 1;
    private Transform trans;
    private AudioSource coinAudio;
    private Renderer rend;
    private Collider2D coll;
    [SerializeField]
    private GameObject monsterBullet;
    float i = 1;
   
    private	void Awake () {
        coinAudio = GetComponent<AudioSource>();
        rend = GetComponent<Renderer>();
        coll = GetComponent<Collider2D>();

        trans = GetComponent<Transform>();
   }
    private void Start()
    {
        Destroy(gameObject, 8);
    }
    private void Update()
        {
            if (i >= 1)
            {
                i = 0;               
                MonsterBullet();
            }
            i += Time.deltaTime/4;

        trans.Translate(Vector3.down * Time.deltaTime * speed);
}
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Bullet")
        { coll.enabled = false;
            coinAudio.Play();
            rend.enabled = false;
            LevelDirector.Instance.Score += 100;
            Destroy(gameObject, coinAudio.clip.length);
        }
    }
    void MonsterBullet()
    {for (int j = 0; j <= 360; j += 45)
        {
            Quaternion a = Quaternion.Euler(0, 0, j);
            Instantiate(monsterBullet, transform.position, a);
        }
    }
}

