using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionManager : Singleton<TransitionManager>, ISaveable
{
    [SceneName] public string startScene;
    public CanvasGroup fadeCanvasGroup;
    public float fadeDuration;
    private bool isFade;   
    private bool canTransation;

    private void OnEnable() {
        EvenHandler.GameStateChangeEvent += OnGameStateChangeEvent;      
        EvenHandler.StartNewGameEvent += OnStartNewGameEvent;  
    }

    private void OnDisable() {
        EvenHandler.GameStateChangeEvent -= OnGameStateChangeEvent;   
        EvenHandler.StartNewGameEvent -= OnStartNewGameEvent;  
    }

  
    private void Start() {
        ISaveable saveable = this;
        saveable.SaveableRegister();
    }

    private void OnStartNewGameEvent(int obj)
    {
        StartCoroutine(TransitionToScene("Menu", startScene));
    }

    private void OnGameStateChangeEvent(GameState gameState)
    {
        canTransation = gameState == GameState.GamePlay;
    }

    public void Transition(string from, string to){
        if(!isFade && canTransation)
            StartCoroutine(TransitionToScene(from, to));
    }

    private IEnumerator TransitionToScene(string from, string to){
        yield return Fade(1);                                   //StartCoroutine 同時執行
        if(from != string.Empty){
            EvenHandler.CallBeforeSceneUnloadEvent();
            yield return SceneManager.UnloadSceneAsync(from);
        }      
        yield return SceneManager.LoadSceneAsync(to, LoadSceneMode.Additive);

        //設置新場景為激活場景
        Scene newScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
        SceneManager.SetActiveScene(newScene);

        EvenHandler.CallAfterSceneloadedEvent();
        yield return Fade(0);
    }

    /// <summary>
    /// 淺入淺出場景
    /// </summary>
    /// <param name="targetAlpha"></param>
    /// <returns></returns>
    private IEnumerator Fade(float targetAlpha){
        isFade = true;

        fadeCanvasGroup.blocksRaycasts = true;

        float speed = Mathf.Abs(fadeCanvasGroup.alpha - targetAlpha)/ fadeDuration;

        while(!Mathf.Approximately(fadeCanvasGroup.alpha, targetAlpha)){                                            //Approximately 判斷是否相似
            fadeCanvasGroup.alpha = Mathf.MoveTowards(fadeCanvasGroup.alpha, targetAlpha, speed * Time.deltaTime);  //MoveTowards 緩慢的增加
            yield return null;
        }

        fadeCanvasGroup.blocksRaycasts = false;

        isFade = false;
    }

    public GameSaveData GenerateSaveData()
    {
        GameSaveData saveData = new GameSaveData();
        saveData.currentScene = SceneManager.GetActiveScene().name;
        return saveData;
    }

    public void RestoreGameData(GameSaveData saveData)
    {
        Transition("Menu", saveData.currentScene);
    }
}
