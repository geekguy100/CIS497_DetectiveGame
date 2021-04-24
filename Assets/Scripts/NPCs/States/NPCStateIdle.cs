using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCStateIdle : INPCState
{
    NPCStateController controller;
    public NPCStateIdle(NPCStateController controller)
    {
        this.controller = controller;
    }

    public void Idle()
    {
        controller.currentState = controller.idleState;

        controller.TurnAwayFromPlayer();
    }
    public void LookAtPlayer()
    {
        controller.currentState = controller.talkingState;
    }
}
