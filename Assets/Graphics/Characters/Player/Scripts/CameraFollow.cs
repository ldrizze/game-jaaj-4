using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    void LateUpdate()
    {
        Vector3 newPos = target.position;
        newPos.z = transform.position.z;
        transform.position = newPos;
    }
}
