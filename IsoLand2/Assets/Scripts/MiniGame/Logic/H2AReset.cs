using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class H2AReset : Interactive
{
    private Transform gearSprite;

    private void Awake(){
        gearSprite = transform.GetChild(0);
    }

    public override void EmptyClicked()
    {
        GameController.Instance.ResetGame();
        gearSprite.DOPunchRotation(Vector3.forward * 180, 1);
    }
}
