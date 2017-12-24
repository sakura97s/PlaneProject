using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif
public class MuneM : MonoBehaviour
{
    [SerializeField ]
    private string loadSceneName;
    [SerializeField]
    private CanvasGroup muneGroup;

    [SerializeField]
    private CanvasGroup optionGroup;

    [SerializeField]
    private CanvasGroup creditGroup;

    private Stack<CanvasGroup> canvasGroupStack = new Stack<CanvasGroup>();
    private List<CanvasGroup> canvasGroupList = new List<CanvasGroup>();

    private void Start()
    {
        UIManager.Instance.FaderOn(false, 2F);
        
        canvasGroupList.Add(muneGroup);
        canvasGroupList.Add(optionGroup);
        canvasGroupList.Add(creditGroup);

        canvasGroupStack.Push(muneGroup);

        DisPlayMenu();

        
    }

    private void Update()
    {      
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Esc();
        }
    }
    public void Esc()
    {
        if (canvasGroupStack.Count <= 1) return;

        canvasGroupStack.Pop();
        DisPlayMenu();
    }

    public void Option()
    {
        canvasGroupStack.Push(optionGroup);
        DisPlayMenu();
    }
    public void StartGame()
    {       
        StartCoroutine(LoadGameScene());
    }
    public void Credit()
    {
        canvasGroupStack.Push(creditGroup);
        DisPlayMenu();
    }
    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    private void DisPlayMenu()
    {
        foreach (var item in canvasGroupList)
        {
            item.alpha = 0;
            item.interactable = false;
            item.blocksRaycasts = false;
        }
        if (canvasGroupStack.Count > 0)
        {
            CanvasGroup cg = canvasGroupStack.Peek();
            cg.alpha = 1;
            cg.interactable = true;
            cg.blocksRaycasts = true;
        }
    }
    private IEnumerator LoadGameScene()
    {
        UIManager.Instance.FaderOn(true, 1f);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(loadSceneName);
    }
}