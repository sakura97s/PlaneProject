using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelDirector : MonoBehaviour {
    
    private static LevelDirector instance;
    public static LevelDirector Instance
    {
        get
        { if (instance == null)
            {
                throw new NullReferenceException("no LevelDirector in scence");
            } return instance;
        }
    }
    public  Action GameStartAction;
    public  Action gameOverAction;
    [SerializeField]
    private MainPlane mainPlane;
    [SerializeField]
    private GameObject boss;
    [SerializeField]
    private PlayerData data;
    private int score;
    private int maxScore;
    private int playerLifeCount = 3;
    public  int PlayerLifeCount { get { return playerLifeCount; } }
    public int Score
    {
        get { return score; }
        set
        {
            score = value;
            {
                if (maxScore < score)
                {
                    data.maxScore = value;
                    maxScore = value;
                }
            }
        }
    }
    public int MaxScore
    {
        get { return maxScore; }
    }
    private MainPlane currentPlane;

    private void Awake()
    {
        instance = this;
        Init();
    }
    private void Start()
    {
        StartCoroutine(Decorate());
    }
    private void Init()
    {
        mainPlane = Resources.Load<MainPlane>("Prefabs/Plane");
        boss = Resources.Load<GameObject>("Prefabs/Enemys/Boss");
        data = Resources.Load<PlayerData>("PlayerData");
        maxScore = data.maxScore;
    }
	private IEnumerator Decorate()
    {
        yield return new WaitForSeconds(2);
        currentPlane = Instantiate(mainPlane, mainPlane.transform.position, Quaternion.identity);
        currentPlane.OnDeadEvent += OnMainPlaneDead;
        //Instantiate(boss, boss.transform.position, Quaternion.identity);
    }
    private void OnMainPlaneDead()
    {
        playerLifeCount--;
        if (PlayerLifeCount > 0)
        {
            StartCoroutine(Decorate());
        }
        else { GameOver(); }
    }
    public void GameOver()
    {
        if (gameOverAction !=null)

        gameOverAction ();
    }
    private	void Update () {
		
	}
}
