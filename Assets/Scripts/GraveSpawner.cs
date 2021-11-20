using System;
using System.Collections.Generic;
using System.Linq;
using Backend;
using Models;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class GraveSpawner : MonoBehaviour
{
    public GameObject gravePrefab;
    public GameObject fencePrefab;
    public float scaleFactor;
    public float wallHeight;

    private void Start()
    {
        print("adding graves");
        App.Instance.GetBackend().GetCemetery(1, OnCemeteryFetched, OnCemeteryFetchFailed);
    }

    void spawnGraves(List<Grave> graves, Vector3 currentPosition)
    {
        foreach (var grave in graves)
        {
            var gravePosition = new Vector3(scaleFactor * grave.gridX, 0, scaleFactor * grave.gridY);
            var finalPosition = gravePosition + currentPosition;
            GameObject gravePrefabInstance =
                Instantiate(gravePrefab, finalPosition, Quaternion.identity, transform);

            gravePrefabInstance.name = gravePrefab.name + '_' + gravePosition.x + "_" + gravePosition.z;
            var texts = gravePrefabInstance.GetComponentsInChildren<TextMeshPro>();
            foreach (var text in texts)
            {
                if (text.name == "NameText")
                {
                    text.SetText(grave.guest.firstName + " " + grave.guest.lastName);
                }
                else if (text.name == "BirthText")
                {
                    text.SetText(grave.guest.birthDate);
                }
                else if (text.name == "DeadText")
                {
                    text.SetText(grave.guest.deathDate);
                }
            }
        }
    }

    private void spawnFence(Cemetery cemetery, Vector3 currentPosition)
    {
        Vector3[] cornersOfCemetery =
        {
            new Vector3(0, 0, 0),
            new Vector3(scaleFactor * cemetery.maxGridX, 0, 0),
            new Vector3(scaleFactor * cemetery.maxGridX, 0, scaleFactor * cemetery.maxGridY),
            new Vector3(0, 0, scaleFactor * cemetery.maxGridY),
        };

        var halfFactor = scaleFactor / 2;
        var halfOfXWallLength = cemetery.maxGridX * halfFactor;
        var halfOfYWallLength = cemetery.maxGridY * halfFactor;
        var xWallLength = cemetery.maxGridX * scaleFactor;
        var yWallLength = cemetery.maxGridY * scaleFactor;

        Vector3[] centersOfWalls =
        {
            new Vector3(halfOfXWallLength, 0, 0),
            new Vector3(xWallLength, 0, halfOfYWallLength),
            new Vector3(halfOfXWallLength, 0, yWallLength),
            new Vector3(0, 0, halfOfYWallLength),
        };

        var padding = new Vector3(-4,0,-4);
        var finalPosition = currentPosition + padding;
        
        var fenceA = Instantiate(fencePrefab, centersOfWalls[0] + finalPosition, Quaternion.identity, transform);
        fenceA.transform.localScale = new Vector3(xWallLength, wallHeight, 0.4f);
        fenceA.name = "a";

        var fenceB = Instantiate(fencePrefab, centersOfWalls[1] + finalPosition, Quaternion.identity, transform);
        fenceB.transform.localScale = new Vector3(0.4f, wallHeight, yWallLength);
        fenceB.name = "b";

        var fenceC = Instantiate(fencePrefab, centersOfWalls[2] + finalPosition, Quaternion.identity, transform);
        fenceC.transform.localScale = new Vector3(xWallLength, wallHeight, 0.4f);
        fenceC.name = "c";

        var fenceD = Instantiate(fencePrefab, centersOfWalls[3] + finalPosition, Quaternion.identity, transform);
        fenceD.transform.localScale = new Vector3(0.4f, wallHeight, yWallLength);
        fenceD.name = "d";
    }

    private void OnCemeteryFetched(Cemetery cemetery)
    {
        List<Grave> graves = cemetery.tombstones;
        var occupiedGraves = graves.Where(grave => grave.guest != null).ToList();
        var currentPosition = transform.localPosition;
        spawnGraves(occupiedGraves, currentPosition);
        spawnFence(cemetery, currentPosition);
    }

    private void OnCemeteryFetchFailed(ARequest request)
    {
        Debug.Log($"Failed to fetch cemetery {request._stringResult}");
    }
}