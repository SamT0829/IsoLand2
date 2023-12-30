using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void QuitGame(){
        Application.Quit();
    }

    public void ContinueGame(){
        //加載遊戲進度
        SaveLoadManager.Instance.Load();
    }

    public void GoBackToMenu(){
        var currentScene = SceneManager.GetActiveScene().name;
        TransitionManager.Instance.Transition(currentScene, "Menu");

        //保存遊戲進度
        SaveLoadManager.Instance.Save();
    }

    public void StartGameWeek(int gameWeek){
        EvenHandler.CallStartNewGameEvent(gameWeek);
    }
}
