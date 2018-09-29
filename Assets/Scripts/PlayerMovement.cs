using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public int[] initCoord = { 1, 1, 1, 0, 0 };
    public float pHeight = .5f; //Player Height
    public float camDist = 5f;
    public float speed = 5f;

    public static int[] playerCoord = { 1, 1, 1, 0, 0 };

    private int spacer;

    void Start()
    {
        spacer = WorldBuilder.spacer;
        MoveToPlayerCoord();
        AdjustCamera();
    }

    void Update()
    {
        if (GameOver.IsGameOver())
            return;

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (playerCoord[3] - 1 >= 0)
                if (WorldBuilder.mat[playerCoord[0], playerCoord[1], playerCoord[2], playerCoord[3] - 1, playerCoord[4]] != 1) //If there's no block in there, update position
                    playerCoord[3] -= 1;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (playerCoord[3] + 1 < WorldBuilder.mat.GetLength(3))
                if (WorldBuilder.mat[playerCoord[0], playerCoord[1], playerCoord[2], playerCoord[3] + 1, playerCoord[4]] != 1) //If there's no block in there, update position
                    playerCoord[3] += 1;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (playerCoord[4] - 1 >= 0)
                if (WorldBuilder.mat[playerCoord[0], playerCoord[1], playerCoord[2], playerCoord[3], playerCoord[4] - 1] != 1) //If there's no block in there, update position
                    playerCoord[4] -= 1;
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (playerCoord[4] + 1 < WorldBuilder.mat.GetLength(4))
                if (WorldBuilder.mat[playerCoord[0], playerCoord[1], playerCoord[2], playerCoord[3], playerCoord[4] + 1] != 1) //If there's no block in there, update position
                    playerCoord[4] += 1;
        }

        MoveToPlayerCoord();
        AdjustCamera();
        AdjustColor();
    }

    public void MoveToPlayerCoord()
    {
        gameObject.transform.position =
            new Vector3(((playerCoord[1] - 1) * WorldBuilder.spacer) + playerCoord[4],
                         ((playerCoord[2] - 1) * WorldBuilder.spacer) + pHeight,
                         ((playerCoord[0] - 1) * WorldBuilder.spacer) + playerCoord[3]);
    }

    public void AdjustCamera()
    {
        GameMaster.cams[GameMaster.currentCam].gameObject.transform.position =
            new Vector3(((playerCoord[1] - 1) * spacer) + playerCoord[4],
                         ((playerCoord[2] - 1) * spacer) + camDist,
                         ((playerCoord[0] - 1) * spacer) + playerCoord[3]);
    }

    public void AdjustColor()
    {
        gameObject.GetComponent<MeshRenderer>().material.color = Color.Lerp(gameObject.GetComponent<MeshRenderer>().material.color, Color.white, Time.deltaTime * speed);
    }

    public static bool IsOnFinishBlock()
    {
        if (WorldBuilder.mat[playerCoord[0], playerCoord[1], playerCoord[2], playerCoord[3], playerCoord[4]] == 4)
            return true;
        return false;
    }

    public void SetCoord(int[] coord)
    {
        try { for (int i = 0; i < playerCoord.Length; i++) playerCoord[i] = coord[i]; }
        catch (IndexOutOfRangeException e) { Debug.LogError("selfCoord and coord have differente sizes! Player"); }
    }

    public static void SetColorToRed()
    {
        GameObject.Find("Player").GetComponent<MeshRenderer>().material.color = new Color(1f, 0f, 0f, 1f);
    }
}
