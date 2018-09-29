using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public int v = 0;
    public GameObject bPrefab;
    public GameObject block;
    private int[] selfCoord = { 0, 0, 0, 0, 0 };

    void Start()
    {
        if (block != null) { Destroy(block); block = null; } //Just in case
        if (selfCoord.Length != 5) Debug.LogError("You forgot to change this!");

        StartCoroutine(CheckForBlock());
    }

    // Update is called once per frame
    void Update ()
    {
		switch (v)
        {
            case 0:
                gameObject.GetComponent<MeshRenderer>().material.color = new Color(1f, 1f, 1f, 1f); //White
                break;
            case 1:
                gameObject.GetComponent<MeshRenderer>().material.color = new Color(.5f, .5f, .5f, 1f); //Gray
                break;
            case 2:
                gameObject.GetComponent<MeshRenderer>().material.color = new Color(1f, 0f, 0f, 1f); //Red
                break;
            case 3:
                gameObject.GetComponent<MeshRenderer>().material.color = new Color(0f, 1f, 0f, 1f); //Green
                break;
            case 4:
                gameObject.GetComponent<MeshRenderer>().material.color = new Color(0f, 0f, 1f, 1f); //Blue
                break;
            default:
                gameObject.GetComponent<MeshRenderer>().material.color = new Color(0f, 0f, 0f, 1f); //Black
                break;
        }
	}

    public IEnumerator CheckForBlock()
    {
        yield return new WaitForSeconds(0f); //Wait before starting

        while (true)
        {
            if (block == null) //Do I have a block above me?
                if (WorldBuilder.mat[selfCoord[0], selfCoord[1], selfCoord[2], selfCoord[3], selfCoord[4]] == 1) //Should I have a block above me?
                {
                    block = Instantiate(bPrefab, new Vector3(transform.position.x, transform.position.y + 0.6f, transform.position.z),
                        transform.rotation); //Summons it slightly above

                    block.GetComponent<MeshRenderer>().material.color = gameObject.GetComponent<MeshRenderer>().material.color;
                    block.layer = gameObject.layer;
                    block.GetComponent<MeshRenderer>().material.SetFloat("_Metallic", 0.5f);
                    block.GetComponent<MeshRenderer>().material.SetFloat("_Glossiness", 0.5f);
                    if (block.layer == 9 || block.layer == 10 || block.layer == 11)
                    {
                        block.GetComponent<MeshRenderer>().material.SetFloat("_Metallic", 1.0f);
                        block.GetComponent<MeshRenderer>().material.SetFloat("_Glossiness", 1.0f);
                    }
                    if (block.layer == 15 || block.layer == 16 || block.layer == 17)
                    {
                        block.GetComponent<MeshRenderer>().material.SetFloat("_Metallic", 0.0f);
                        block.GetComponent<MeshRenderer>().material.SetFloat("_Glossiness", 0.0f);
                    }


                }

            yield return new WaitForSeconds(1f); //Wait before repeating
        }
    }

    public void SetV(int newv) { v = newv; }
    public void SetCoord(int[] coord)
    {
        try { for (int i = 0; i < selfCoord.Length; i++) selfCoord[i] = coord[i]; }
        catch (IndexOutOfRangeException e) { Debug.LogError("selfCoord and coord have differente sizes! Tile"); }
    }
    public void SetP(GameObject newp) { bPrefab = newp; }
}
