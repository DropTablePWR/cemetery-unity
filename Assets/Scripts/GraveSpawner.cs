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

    private void Start()
    {
        print("adding graves");
        App.Instance.GetBackend().GetCemetery(1, OnCemeteryFetched, OnCemeteryFetchingFailed);
    }

    private void OnCemeteryFetched(Cemetery cemetery)
    {
        List<Grave> graves = cemetery.tombstones;

        var occupiedGraves = graves.Where(grave => grave.guest != null);
        var currentPosition = transform.localPosition;
        foreach (var grave in occupiedGraves)
        {
            var gravePosition = new Vector3(4 * grave.gridX, -3, 4 * grave.gridY);
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

    private void OnCemeteryFetchingFailed(ARequest request)
    {
        Debug.Log($"Failed to fetch cemetery: {request._stringResult}");
    }
}