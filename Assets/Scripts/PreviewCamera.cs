using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PreviewCamera : MonoBehaviour
{
    [HideInInspector]
    public int spacer;
    [HideInInspector]
    public float pCamDist;
    public int pos;
    public Image[] borders;
    public Image blockingIm;

    private delegate void PosN();
    private PosN pnfunc;
    private int[] selfCoord = { 1, 1, 1, 0, 0 };

    // Use this for initialization
    void Awake ()
    {
        blockingIm.enabled = false;
        spacer = WorldBuilder.spacer;

        switch (pos)
        {
            case 0:
                pnfunc = Pos0;
                break;
            case 1:
                pnfunc = Pos1;
                break;
            case 2:
                pnfunc = Pos2;
                break;
            case 3:
                pnfunc = Pos3;
                break;
            case 4:
                pnfunc = Pos4;
                break;
            case 5:
                pnfunc = Pos5;
                break;
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        FromPlayerToCamera();
        pnfunc();
        AdjustSelfPosition();
        CheckAndActivate();
	}

    private void FromPlayerToCamera()
    {
        selfCoord[0] = PlayerMovement.playerCoord[0];
        selfCoord[1] = PlayerMovement.playerCoord[1];
        selfCoord[2] = PlayerMovement.playerCoord[2];
        selfCoord[3] = PlayerMovement.playerCoord[3];
        selfCoord[4] = PlayerMovement.playerCoord[4];
    }

    private void AdjustSelfPosition()
    {
        GameMaster.prevCams[pos].gameObject.transform.position =
            new Vector3(((selfCoord[1] - 1) * WorldBuilder.spacer) + selfCoord[4],
                        ((selfCoord[2] - 1) * WorldBuilder.spacer) + pCamDist,
                        ((selfCoord[0] - 1) * WorldBuilder.spacer) + selfCoord[3]);
    }

    public void CheckAndActivate()
    {
        int a;
        try { a = WorldBuilder.mat[selfCoord[0], selfCoord[1], selfCoord[2], selfCoord[3], selfCoord[4]]; }
        catch (IndexOutOfRangeException e) { a = 1; }

        if (a == 1)  //If it has a block
        {
            //Debug.Log("Block on " + pos);
            foreach (Image q in borders)
                q.color = Color.white;
            blockingIm.enabled = true;
        }
        //else
        //if (a == -7)  //If it is out of bounds
        //{
        //    //Debug.Log("Out of Bounds on " + pos);
        //    foreach (Image q in borders)
        //        q.color = Color.red;
        //}
        else  //If it is free
        {
            //Debug.Log("Free on " + pos);
            foreach (Image q in borders)
                q.color = Color.white;
            blockingIm.enabled = false;
        }
    }

    private void Pos0() { selfCoord[2] = PlayerMovement.playerCoord[2] + 1; }   //Up Preview Camera
    private void Pos1() { selfCoord[2] = PlayerMovement.playerCoord[2] - 1; }   //Down Preview Camera
    private void Pos2() { selfCoord[1] = PlayerMovement.playerCoord[1] + 1; }   //Future Preview Camera
    private void Pos3() { selfCoord[1] = PlayerMovement.playerCoord[1] - 1; }   //Past Preview Camera

    private void Pos4() //Next Preview Camera
    {
        selfCoord[0] = PlayerMovement.playerCoord[0] + 1;
        if (selfCoord[0] > WorldBuilder.mat.GetLength(0))
            selfCoord[0] = 0;
    }
    private void Pos5() //Previous Preview Camera
    {
        selfCoord[0] = PlayerMovement.playerCoord[0] - 1;
        if (selfCoord[0] < 0)
            selfCoord[0] = WorldBuilder.mat.GetLength(0) - 1;
    }

}
