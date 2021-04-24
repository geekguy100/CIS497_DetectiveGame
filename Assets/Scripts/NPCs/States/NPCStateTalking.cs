using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCStateTalking : INPCState
{
    NPCStateController controller;
    public NPCStateTalking(NPCStateController controller)
    {
        this.controller = controller;
    }

    public void Idle()
    {
        controller.currentState = controller.idleState;
    }
    public void LookAtPlayer()
    {
        controller.currentState = controller.talkingState;

        controller.TurnTowardPlayer();
    }
}
