using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameController : Singleton<GameController>
{
    public UnityEvent OnFinish;
    public GameH2A_SO gameData;
    public GameH2A_SO[] gameDataArray;

    public GameObject lineParent;
    public LineRenderer linePrefab;
    public Ball ballPrefab;
    public Transform[] holderTransforms;

    private void OnEnable() {
        EvenHandler.CheckGameStateEvent += OnCheckGameStateEvent;
    }

    private void OnDisable() {
        EvenHandler.CheckGameStateEvent -= OnCheckGameStateEvent;
    }    

    // private void Start() {
    //     DrawLine();
    //     CreateBall();
    // }

    public void ResetGame(){
        foreach(var holder in holderTransforms){
            if(holder.childCount > 0){
                Destroy(holder.GetChild(0).gameObject);
            }
        }
        CreateBall();
    }

    private void  OnCheckGameStateEvent()
    {
        foreach(var ball in FindObjectsOfType<Ball>()){
            if(!ball.isMatch)
                return;
        }

        Debug.Log("遊戲結束");
        foreach(var holder in holderTransforms){
            holder.GetComponent<Collider2D>().enabled = false;
        }
        EvenHandler.CallGamePassEvent(gameData.gameName);
        OnFinish?.Invoke();
    }

    public void DrawLine(){
        foreach(var connections in gameData.lineConnections){
            var line = Instantiate(linePrefab, lineParent.transform);
            line.SetPosition(0, holderTransforms[connections.from].position);
            line.SetPosition(1, holderTransforms[connections.to].position);

            //創建每個Holder的連接關係
            holderTransforms[connections.from].GetComponent<Holder>().linkHolders.Add(holderTransforms[connections.to].GetComponent<Holder>());
            holderTransforms[connections.to].GetComponent<Holder>().linkHolders.Add(holderTransforms[connections.from].GetComponent<Holder>());
        }
    }
    
    public void CreateBall(){
        for(int i = 0; i < gameData.startBallOrder.Count; i++){
            if(gameData.startBallOrder[i] == BallName.None){
                holderTransforms[i].GetComponent<Holder>().isEmpty = true;
                continue;
            }
            Ball ball = Instantiate(ballPrefab, holderTransforms[i]);

            holderTransforms[i].GetComponent<Holder>().CheckBall(ball);
            holderTransforms[i].GetComponent<Holder>().isEmpty = false;
            ball.SetupBall(gameData.GetBallDetails(gameData.startBallOrder[i]));
        }
    }

    public void SetGameWeekData(int week){
        gameData = gameDataArray[week];
        DrawLine();
        CreateBall();
    }
}
