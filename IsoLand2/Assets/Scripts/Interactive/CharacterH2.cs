using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DialougeController))]
public class CharacterH2 : Interactive
{
    private DialougeController dialougeController;

    private void Awake() {
        dialougeController = GetComponent<DialougeController>();
    }

    public override void EmptyClicked()
    {
        if(isDone)
            dialougeController.ShowDialogueFinish();
        else       
        //對話內容A
        dialougeController.ShowDialogueEmpty();
    }

    protected override void OnClickedAction()
    {
        //對話內容B
        dialougeController.ShowDialogueFinish();
    }
}
