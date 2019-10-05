using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class MapGen : MonoBehaviour
{
    public GameObject[] emptyTiles;
    public GameObject[] blockedTiles;
    public GameObject[] otherTiles;
    public Transform Container;

    public Color badWorldColor;
    public Color goodWorldColor;
    public Material pixelator;

    private bool isGoodWorld = true;
    private Material mat;

    private float cPixelation;
    
    private void Awake()
    {
        Debug.Log("Started Environment Generation!");
        
        Generator.InitStandardTiles();
        var length = 25;
        var tm = Generator.GetTileMap(length, length, 527);
        for (var xIndex = 0; xIndex < length; xIndex++)
        {
            for (var yIndex = 0; yIndex < length; yIndex++)
            {
                var tile = tm[xIndex, yIndex];
                GameObject toInst = null;
                var rot = 0;
                
                if (tile.Equals(Generator.Tile2))
                {
                    toInst = emptyTiles[Random.Range(0, emptyTiles.Length - 1)];
                }
                else if (tile.Equals(Generator.Tile3))
                {
                    toInst = blockedTiles[Random.Range(0, blockedTiles.Length - 1)];
                    rot = Random.Range(0, 5) * 60;
                }
                else
                {
                    foreach (var tileTest in otherTiles)
                    {
                        if (!tileTest.GetComponent<MBTile>().tile.Equals(tile)) continue;

                        toInst = tileTest;
                        // TODO: Rotation
                        break;
                    }
                }

                if (toInst == null)
                {
                    Debug.Log("Not existing: " + tile.Name);
                    continue;
                }
                
                var obj = Instantiate(toInst, Container);
                obj.transform.rotation = Quaternion.Euler(-90, rot, 0);

                var xpos = 4.45f * yIndex/* + (Mathf.IsPowerOfTwo(xIndex) ? 0 : 4.45f / 2)*/;
                var zpos = 5.15f * xIndex + (yIndex / 2 == yIndex / 2f ? 0 : -2.55f);
                
                obj.transform.position = new Vector3(xpos, 0, zpos);
                
                Debug.Log("Generated one tile!");
                // 5.4-0,25  = 5,15
                // 1,75+2,75 = 4,5
                // 2,85-0,3  = 2,55
            }
        }
        Debug.Log("Finished Environment Generation!");

        goodWorldColor = transform.GetChild(0).GetComponent<Renderer>().material.color;
        mat = new Material(transform.GetChild(0).GetComponent<Renderer>().material);

        for (var xIndex = 0; xIndex < transform.childCount; xIndex++)
        {
            if (transform.GetChild(xIndex).GetComponent<Renderer>().material.name == "gazon (Instance)")
            {
                transform.GetChild(xIndex).GetComponent<Renderer>().material = mat;
            }
            else if (transform.GetChild(xIndex).GetComponent<Renderer>().materials[1].name == "gazon (Instance)")
            {
                var mats = transform.GetChild(xIndex).GetComponent<Renderer>().materials;
                mats[1] = mat;
                transform.GetChild(xIndex).GetComponent<Renderer>().materials = mats;
            }
        }

        cPixelation = 0.005f;
        pixelator.SetFloat("_Pixelation", cPixelation);
    }

    public IEnumerator ChangeWorldOnTiles()
    {
        while (cPixelation < 0.1f)
        {
            yield return 0;
            cPixelation += Time.deltaTime / 3;
            if (cPixelation > 0.1f)
                cPixelation = 0.1f;
            pixelator.SetFloat("_Pixelation", cPixelation);
        }
        
        mat.color = isGoodWorld ? badWorldColor : goodWorldColor;
        isGoodWorld = !isGoodWorld;
        yield return 0;
        
        while (cPixelation > 0.005f)
        {
            yield return 0;
            cPixelation -= Time.deltaTime / 3;
            if (cPixelation < 0.005f)
                cPixelation = 0.005f;
            pixelator.SetFloat("_Pixelation", cPixelation);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            StartCoroutine(ChangeWorldOnTiles());
        }
    }
}
