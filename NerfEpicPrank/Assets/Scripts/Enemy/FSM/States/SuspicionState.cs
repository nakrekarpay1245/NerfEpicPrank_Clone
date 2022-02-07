using System;
using UnityEngine;

public class SuspicionState : IState
{
    public Action<int> Callback;

    public SuspicionState(Action<int> Callback)
    {
        this.Callback = Callback;
    }

    public void OnStateEnter()
    {
        Debug.Log("Suspicion Enter");
    }

    public void OnStateExit()
    {
        Debug.Log("Suspicion Exit");
    }

    public void OnStateFixedUpdate()
    {
        Debug.Log("Suspicion FixedUpdate");
    }

    public void OnStateUpdate()
    {
        if (!FieldOfView.instance.targetIsDetected)
        {
            if (FieldOfView.instance.targetInFieldOfView)
            {
                FieldOfView.instance.lastSeen = false;
                Callback(4);
            }
        }
        else if (FieldOfView.instance.targetIsDetected)
        {
            if (SuspicionToChaseAlarmControl())
            {
                Callback(3);
            }
        }
    }

    private bool SuspicionToChaseAlarmControl()
    {
        EnemyAI.instance.alarmTimer += Time.deltaTime;
        if (EnemyAI.instance.alarmTimer >= EnemyAI.instance.chaseAlarmTime)
        {
            Debug.Log("Suspicion to Chase");
            return true;
        }
        else
            return false;
    }
}
