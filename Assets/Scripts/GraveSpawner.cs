using System;
using System.Collections.Generic;
using System.Linq;
using Models;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class GraveSpawner : MonoBehaviour
{
    public GameObject gravePrefab;
    public GameObject fencePrefab;
    public float scaleFactor;

    private void Start()
    {
        print("adding graves");
        Apply();
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
        var fenceHorizontalScaleVector = new Vector3(scaleFactor * cemetery.maxGridX, 0, 0.4f);
        var fenceVerticalScaleVector = new Vector3(scaleFactor * cemetery.maxGridY, 0, 0.4f);
    }


    private void Apply()
    {
        var cemetery = ApiConnection.GetCemetery(1);
        List<Grave> graves = cemetery.tombstones;
        var occupiedGraves = graves.Where(grave => grave.guest != null).ToList();
        var currentPosition = transform.localPosition;
        spawnGraves(occupiedGraves, currentPosition);
        spawnFence(cemetery, currentPosition);
    }
}