using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public GameObject myPlayer;
    public static bool isEnabled = true;
    public static Vector2 priestPosition;
    public static bool resetPosition = false;

    void Start() { }

    void Update()
    {
        if (isEnabled)
        {
            myPlayer.SetActive(true);
        }
        else
        {
            myPlayer.SetActive(false);
        }

        if (resetPosition)
        {
            resetPosition = false;
            myPlayer.transform.position = priestPosition;
        }
    }
}
