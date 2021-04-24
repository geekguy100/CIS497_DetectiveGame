using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCStateController : MonoBehaviour
{
    Quaternion startingPos;
    GameObject player;
    Vector3 lookHere;

    public INPCState idleState;
    public INPCState talkingState;
    public INPCState currentState;
    void Start()
    {
        idleState = new NPCStateIdle(this);
        talkingState = new NPCStateTalking(this);
        currentState = idleState;

        player = GameObject.FindGameObjectWithTag("Player");
        startingPos = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(transform.position.x - player.transform.position.x) < 6 && Mathf.Abs(transform.position.z - player.transform.position.z) < 6)
        {
            currentState = talkingState;
            LookAtPlayer();
        }
        else
        {
            currentState = idleState;
            Idle();
        }

    }

    public void TurnTowardPlayer()
    {
        lookHere = player.transform.position;
        lookHere.y = transform.position.y;
        transform.LookAt(lookHere);
    }
    public void TurnAwayFromPlayer()
    {
        transform.rotation = startingPos;
    }

    private void Idle()
    {
        currentState.Idle();
    }
    private void LookAtPlayer()
    {
        currentState.LookAtPlayer();
    }
}
