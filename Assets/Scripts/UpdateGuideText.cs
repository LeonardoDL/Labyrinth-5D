using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateGuideText : MonoBehaviour
{
    public int rank;
    public Color[] colors;

    private delegate void CheckForRank();
    private CheckForRank cfr;

    void Start()
    {
        cfr = CheckForRank0;
        if (rank == 1)
            cfr = CheckForRank1;
        if (rank == 2)
            cfr = CheckForRank2;
    }
	
	// Update is called once per frame
	void Update ()
    {
        cfr();
	}

    private void CheckForRank0()
    {
        int a = PlayerMovement.playerCoord[0];
        switch (a)
        {
            case 0:
                gameObject.GetComponent<Text>().text = "\n\n\nEMILY'S UNIVERSE";
                gameObject.GetComponent<Text>().color = colors[0];
                break;
            case 1:
                gameObject.GetComponent<Text>().text = "\n\n\nCHARLIE'S UNIVERSE";
                gameObject.GetComponent<Text>().color = colors[1];
                break;
            case 2:
                gameObject.GetComponent<Text>().text = "\n\n\nJAMES'S UNIVERSE";
                gameObject.GetComponent<Text>().color = colors[2];
                break;
        }
    }

    private void CheckForRank1()
    {
        int a = PlayerMovement.playerCoord[1];
        switch (a)
        {
            case 0:
                gameObject.GetComponent<Text>().text = "\n\nPAST";
                gameObject.GetComponent<Text>().color = colors[0];
                break;
            case 1:
                gameObject.GetComponent<Text>().text = "\n\nPRESENT";
                gameObject.GetComponent<Text>().color = colors[1];
                break;
            case 2:
                gameObject.GetComponent<Text>().text = "\n\nFUTURE";
                gameObject.GetComponent<Text>().color = colors[2];
                break;
        }
    }

    private void CheckForRank2()
    {
        int a = PlayerMovement.playerCoord[2];
        switch (a)
        {
            case 0:
                gameObject.GetComponent<Text>().text = "\nBASEMENT";
                gameObject.GetComponent<Text>().color = colors[0];
                break;
            case 1:
                gameObject.GetComponent<Text>().text = "\nGROUND FLOOR";
                gameObject.GetComponent<Text>().color = colors[1];
                break;
            case 2:
                gameObject.GetComponent<Text>().text = "\nATTIC";
                gameObject.GetComponent<Text>().color = colors[2];
                break;
        }
    }
}
