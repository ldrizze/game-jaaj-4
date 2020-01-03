using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform myTarget;
    public GameObject myGO;

    void Start()
    {
        myTarget = myGO.transform;
    }
    
    void LateUpdate()
    {
        if (!myGO.activeSelf)
        {
            myTarget = GameObject.FindGameObjectWithTag("Target").GetComponent<Transform>();
        }
        else
        {
            //myGO = GameObject.FindGameObjectWithTag("Player");
            myTarget = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        }

        Vector3 player = myTarget.position;
        player.z = transform.position.z;
        transform.position = player;
    }
}
