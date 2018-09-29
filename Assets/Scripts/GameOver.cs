using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    private static bool isGameOver;
    private static GameObject go;
    // Use this for initialization
    void Start ()
    {
        isGameOver = false;
        go = gameObject;
        gameObject.SetActive(false);
    }

    public static void SetGameOver(bool b)
    {
        isGameOver = b;
        if (b)
            go.SetActive(true);
    }
    public static bool IsGameOver() { return isGameOver; }
}
