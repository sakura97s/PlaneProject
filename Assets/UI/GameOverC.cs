using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverC : MonoBehaviour
{
    [SerializeField ]
    private CanvasGroup gameOverGroup;

    private LevelDirector director;

    void Start()
    {
        director = LevelDirector.Instance;
        director.GameOverAction += DisplayText;
        gameOverGroup.alpha = 0;
    }
    public  void DisplayText()
    {
        gameOverGroup.alpha = 1;
        StartCoroutine(GameOver()); 
    }
    private IEnumerator GameOver()
    {
        yield return new WaitForSeconds(1f);
        Time.timeScale = 0;
    }
}
