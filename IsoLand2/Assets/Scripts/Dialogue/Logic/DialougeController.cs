using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialougeController : MonoBehaviour
{
    public DialogData_SO dialogueEmpty;
    public DialogData_SO dialogueFinish;
    private Stack<string> dialogueEmptyStack;
    private Stack<string> dialogueFinishStack;
    private bool isTalking;

    private void Awake() {
        FillDialogueStack();
    }

    private void FillDialogueStack(){
        dialogueEmptyStack = new Stack<string>();
        dialogueFinishStack = new Stack<string>();

        for(int i = dialogueEmpty.dialogList.Count - 1; i > -1; i--){
            dialogueEmptyStack.Push(dialogueEmpty.dialogList[i]);
        }
        for(int i = dialogueFinish.dialogList.Count - 1; i > -1; i--){
            dialogueFinishStack.Push(dialogueFinish.dialogList[i]);
        }
    }

    public void ShowDialogueEmpty(){
        if(!isTalking)
            StartCoroutine(DialogueRoutine(dialogueEmptyStack));
    }

    public void ShowDialogueFinish(){
        if(!isTalking)
            StartCoroutine(DialogueRoutine(dialogueFinishStack));
    }

    private IEnumerator DialogueRoutine(Stack<string> data){
        isTalking = true;
        if(data.TryPop(out string result)){
            EvenHandler.CallShowDialogueEvent(result);
            yield return null;
            isTalking = false;
            EvenHandler.CallGameStateChangeEvent(GameState.Pause);
        }
        else{
            EvenHandler.CallShowDialogueEvent(string.Empty);
            FillDialogueStack();
            isTalking = false;
            EvenHandler.CallGameStateChangeEvent(GameState.GamePlay);
        }
    }
}
