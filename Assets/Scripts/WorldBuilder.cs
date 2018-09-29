using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WorldBuilder : MonoBehaviour
{
    public static int[,,,,] mat;
    public static int dimensions = 5;
    public static int[] initCoord = { 1, 1, 1, 0, 0 };
    public static int[] endCoord = { 1, 1, 1, 4, 4 };
    public static int spacer = 5;

    public int[] initialCoordinates = { 1, 1, 1, 0, 0 };
    public int[] endCoordinates = { 1, 1, 1, 4, 4 };
    public int spacerValue = 5;
    public GameObject tile;
    public GameObject block;

    void Start()
    {
		Screen.SetResolution(768, 768, false);
        spacer = spacerValue;
        mat = new int[3, 3, 3, 5, 5];
        dimensions = mat.Rank;
        initCoord = initialCoordinates;
        endCoord = endCoordinates;
        AttRandom();
        SpawnPrefab(spacer, tile, block);
    }

    public static void AttRandom()
    {
        for (int i = 0; i < mat.GetLength(0); i++)
            for (int j = 0; j < mat.GetLength(1); j++)
                for (int k = 0; k < mat.GetLength(2); k++)
                    for (int l = 0; l < mat.GetLength(3); l++)
                        for (int m = 0; m < mat.GetLength(4); m++)
                        {
                            //int p = Random.Range(0, 2);
                            //if (p == 3) mat[i, j, k, l, m] = 1;
                            //else
                            mat[i, j, k, l, m] = Random.Range(0, 2); ;
                        }

        mat[initCoord[0], initCoord[1], initCoord[2], initCoord[3], initCoord[4]] = 3;
        mat[endCoord[0], endCoord[1], endCoord[2], endCoord[3], endCoord[4]] = 4;
    }

    public static void SpawnPrefab(int spacer, GameObject tile, GameObject block)
    {
        for (int i = 0; i < mat.GetLength(0); i++)
            for (int j = 0; j < mat.GetLength(1); j++)
                for (int k = 0; k < mat.GetLength(2); k++)
                    for (int l = 0; l < mat.GetLength(3); l++)
                        for (int m = 0; m < mat.GetLength(4); m++)
                        {
                            GameObject g = Instantiate(tile, new Vector3(((j - 1) * spacer) + m, (k - 1) * spacer, ((i - 1) * spacer) + l), tile.transform.rotation);
                            g.layer = mat.GetLength(0)*j + i + 9;
                            g.GetComponent<Tile>().SetV(mat[i, j, k, l, m]);
                            g.GetComponent<Tile>().SetCoord(new int[5] { i, j, k, l, m });
                            g.GetComponent<Tile>().SetP(block);
                        }
    }
}
