using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Holder : Interactive
{
    public BallName matchball;
    private Ball currentBall;
    public HashSet<Holder> linkHolders = new HashSet<Holder>();
    public bool isEmpty;

    public void CheckBall(Ball ball)
    {
        currentBall = ball;
        if (ball.ballDetails.ballName == matchball)
        {
            currentBall.isMatch = true;
            currentBall.SetRight();
        }
        else
        {
            currentBall.isMatch = false;
            currentBall.SetWrong();
        }
    }

    public override void EmptyClicked()
    {
        foreach (var holder in linkHolders)
        {
            if (holder.isEmpty)
            {
                //移動球
                currentBall.transform.position = holder.transform.position;
                currentBall.transform.SetParent(holder.transform);

                //交換球
                holder.CheckBall(currentBall);
                this.currentBall = null;

                //改變狀態
                this.isEmpty = true;
                holder.isEmpty = false;

                EvenHandler.CallCheckGameStateEvent();
            }
        }
    }
}
