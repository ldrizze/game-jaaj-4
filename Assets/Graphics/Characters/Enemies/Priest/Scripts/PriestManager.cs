using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PriestManager : MonoBehaviour
{
    static bool targetWasSet = false;

    void Start() { }

    public static bool Target
    {
        get { return targetWasSet; }
        set { targetWasSet = value; }
    }
}
