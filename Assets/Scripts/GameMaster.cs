using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public Camera[] cameras;
    public Camera[] prevCameras;
    public float lightIntensity = 1.2f;
    public float refining = .2f;
    public float pCamDist = 2f; //Preview Cams Dist

    public static Camera[] cams;
    public static Camera[] prevCams;
    public static int currentCam = 1;
    // Use this for initialization
    void Start()
    {
        AdjustLight();

        cams = cameras;
        prevCams = prevCameras;
        //cams = (Camera[]) cameras.Clone();
        foreach (Camera c in cams) //Deactivating all cams, just in case
            c.gameObject.SetActive(false);

        //int i = 0;
        foreach (Camera c in prevCams) //Activating all preview cams, just in case
        {
            c.gameObject.SetActive(true);
//            c.gameObject.GetComponent<PreviewCamera>().pos = i; i++;
            c.gameObject.GetComponent<PreviewCamera>().pCamDist = pCamDist;
        }

        cams[currentCam].gameObject.SetActive(true); //Activates the initial cam

        //int a = cams.Length;
        //if (WorldBuilder.mat.GetLength(0) != a)
        //    Debug.LogError("Number of Cams disn't equal to number of universes!");


    }

    // Update is called once per frame
    void Update()
    {
        if (GameOver.IsGameOver())
            return;

        if (Input.GetKeyDown(KeyCode.E)) // Go to Floor above
        {
            //Debug.Log("Going to Floor above - E");
            if (PlayerMovement.playerCoord[2] + 1 < WorldBuilder.mat.GetLength(2)) //Am I in the attic already?
            {
                if (WorldBuilder.mat[PlayerMovement.playerCoord[0], PlayerMovement.playerCoord[1], PlayerMovement.playerCoord[2] + 1, PlayerMovement.playerCoord[3], PlayerMovement.playerCoord[4]] != 1) //Is there a block on the floor above?
                    PlayerMovement.playerCoord[2]++;
                else
                    PlayerMovement.SetColorToRed();
            }
        }

        if (Input.GetKeyDown(KeyCode.D)) // Go to Floor below
        {
            //Debug.Log("Going to Floor below - D");
            if (PlayerMovement.playerCoord[2] - 1 >= 0) //Am I in the basement already?
            {
                if (WorldBuilder.mat[PlayerMovement.playerCoord[0], PlayerMovement.playerCoord[1], PlayerMovement.playerCoord[2] - 1, PlayerMovement.playerCoord[3], PlayerMovement.playerCoord[4]] != 1) //Is there a block on the floor below?
                    PlayerMovement.playerCoord[2]--;
                else
                    PlayerMovement.SetColorToRed();
            }
        }

        if (Input.GetKeyDown(KeyCode.S)) // Go forward in Time
        {
            //Debug.Log("Going forward in Time - W");
            if (PlayerMovement.playerCoord[1] + 1 < WorldBuilder.mat.GetLength(1)) //Am I in the future already?
            {
                if (WorldBuilder.mat[PlayerMovement.playerCoord[0], PlayerMovement.playerCoord[1] + 1, PlayerMovement.playerCoord[2], PlayerMovement.playerCoord[3], PlayerMovement.playerCoord[4]] != 1) //Is there a block in the future time?
                    PlayerMovement.playerCoord[1]++;
                else
                    PlayerMovement.SetColorToRed();
            }
        }

        if (Input.GetKeyDown(KeyCode.W)) // Go backward in Time
        {
            //Debug.Log("Going backward in Time - S");
            if (PlayerMovement.playerCoord[1] - 1 >= 0) //Am I in the past already?
            {
                if (WorldBuilder.mat[PlayerMovement.playerCoord[0], PlayerMovement.playerCoord[1] - 1, PlayerMovement.playerCoord[2], PlayerMovement.playerCoord[3], PlayerMovement.playerCoord[4]] != 1) //Is there a block in the past time?
                    PlayerMovement.playerCoord[1]--;
                else
                    PlayerMovement.SetColorToRed();
            }
        }

        if (Input.GetKeyDown(KeyCode.A)) // Go to next Universe
        {
            //Debug.Log("Going to next Universe - Q");

            if (currentCam + 1 < cams.Length)
            {
                if (WorldBuilder.mat[currentCam + 1, PlayerMovement.playerCoord[1], PlayerMovement.playerCoord[2], PlayerMovement.playerCoord[3], PlayerMovement.playerCoord[4]] != 1) //If there's no block in there, update position and camera
                {
                    cams[currentCam].gameObject.SetActive(false); currentCam++; //Deactivate Cam and update iterator
                    cams[currentCam].gameObject.SetActive(true); //Activate new current camera
                    PlayerMovement.playerCoord[0] = currentCam;
                }
                else
                    PlayerMovement.SetColorToRed();
            }
            else
            {
                if (WorldBuilder.mat[0, PlayerMovement.playerCoord[1], PlayerMovement.playerCoord[2], PlayerMovement.playerCoord[3], PlayerMovement.playerCoord[4]] != 1) //If there's no block in there, update position and camera
                {
                    cams[currentCam].gameObject.SetActive(false); currentCam = 0; //Deactivate Cam and update iterator
                    cams[currentCam].gameObject.SetActive(true); //Activate new current camera
                    PlayerMovement.playerCoord[0] = currentCam;
                }
                else
                    PlayerMovement.SetColorToRed();
            }
        }

        if (Input.GetKeyDown(KeyCode.Q)) // Go to previous Universe
        {
            //Debug.Log("Going to previous Universe - A");

            if (currentCam - 1 >= 0)
            {
                if (WorldBuilder.mat[currentCam - 1, PlayerMovement.playerCoord[1], PlayerMovement.playerCoord[2], PlayerMovement.playerCoord[3], PlayerMovement.playerCoord[4]] != 1) //If there's no block in there, update position
                {
                    cams[currentCam].gameObject.SetActive(false); currentCam--; //Deactivate Cam and update iterator
                    cams[currentCam].gameObject.SetActive(true); //Activate new current camera
                    PlayerMovement.playerCoord[0] = currentCam; //Update Player's position
                }
                else
                    PlayerMovement.SetColorToRed();
            }
            else
            {
                if (WorldBuilder.mat[cams.Length - 1, PlayerMovement.playerCoord[1], PlayerMovement.playerCoord[2], PlayerMovement.playerCoord[3], PlayerMovement.playerCoord[4]] != 1) //If there's no block in there, update position
                {
                    cams[currentCam].gameObject.SetActive(false); currentCam = cams.Length - 1; //Deactivate Cam and update iterator
                    cams[currentCam].gameObject.SetActive(true); //Activate new current camera
                    PlayerMovement.playerCoord[0] = currentCam; //Update Player's position
                }
                else
                    PlayerMovement.SetColorToRed();
            }
        }

        AdjustLight();

        if (PlayerMovement.IsOnFinishBlock())
            GameOver.SetGameOver(true);
    }

    public void AdjustLight()
    {
        foreach (GameObject l in GameObject.FindGameObjectsWithTag("Light"))
            l.GetComponent<Light>().intensity = lightIntensity + PlayerMovement.playerCoord[2] * refining;
    }
}
