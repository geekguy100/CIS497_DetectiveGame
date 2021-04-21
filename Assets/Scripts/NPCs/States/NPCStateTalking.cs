using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCStateTalking : MonoBehaviour
{
    Quaternion startingPos;
    GameObject player;
    Vector3 lookHere;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        startingPos = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(transform.position.x - player.transform.position.x) < 5 && Mathf.Abs(transform.position.z - player.transform.position.z) < 5)
        {
            lookHere = player.transform.position;
            lookHere.y = transform.position.y;
            transform.LookAt(lookHere);
        }
        else transform.rotation = startingPos;
        
    }
}
