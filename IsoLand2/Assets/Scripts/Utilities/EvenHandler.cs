using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class EvenHandler
{
    public static event Action<ItemDetails, int> UpdateUIEvent;

    public static void CallUpdateUIEvent(ItemDetails itemDetails, int index){
        UpdateUIEvent?.Invoke(itemDetails, index);
    }

    public static event Action BeforeSceneUnloadEvent;

    public static void CallBeforeSceneUnloadEvent(){
        BeforeSceneUnloadEvent?.Invoke();
    }

    public static event Action AfterSceneloadedEvent;

    public static void CallAfterSceneloadedEvent(){
        AfterSceneloadedEvent?.Invoke();
    }

    public static event Action<ItemDetails, bool> ItemSelectedEvent;
    
    public static void CallItemSelectedEvent(ItemDetails itemDetails, bool isSelected){
        ItemSelectedEvent?.Invoke(itemDetails, isSelected);
    }

    public static event Action<ItemName> ItemUseEvent;

    public static void CallItemUsedEvent(ItemName itemName){
        ItemUseEvent?.Invoke(itemName);
    }

    public static event Action<int> ChangeItemEvent;

    public static void CallChangeItemEven(int index){
        ChangeItemEvent?.Invoke(index);
    }

    public static event Action<string> ShowDialogueEvent;

    public static void CallShowDialogueEvent(string dialogue){
        ShowDialogueEvent?.Invoke(dialogue);
    }

    public static event Action<GameState> GameStateChangeEvent;

    public static void CallGameStateChangeEvent(GameState gameState){
        GameStateChangeEvent?.Invoke(gameState);
    }

    public static event Action CheckGameStateEvent;

    public static void CallCheckGameStateEvent(){
        CheckGameStateEvent?.Invoke();
    }

    public static event Action<string> GamePassEvent;

    public static void CallGamePassEvent(string gameName){
        GamePassEvent?.Invoke(gameName);
    }

    public static event Action<int> StartNewGameEvent;

    public static void CallStartNewGameEvent(int gameWeek){
        StartNewGameEvent?.Invoke(gameWeek);
    }
}
