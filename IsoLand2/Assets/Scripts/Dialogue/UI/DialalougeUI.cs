using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialalougeUI : MonoBehaviour
{
    public GameObject panel;
    public Text dialoguText;

    private void OnEnable() {
        EvenHandler.ShowDialogueEvent += ShowDialogue;
    }

    private void OnDisable() {
        EvenHandler.ShowDialogueEvent -= ShowDialogue;
    }

    private void ShowDialogue(string dialogue){
        if(dialogue != string.Empty)
            panel.SetActive(true);
        else
            panel.SetActive(false);
        dialoguText.text = dialogue;
    }
}
