using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class GraveSpawner : MonoBehaviour
{
    public GameObject gravePrefab;

    private void Start()
    {
        print("adding graves");
        Apply();
    }

    private void Apply()
    {
        var cemetery = ApiConnection.GetCemetery(1);
        print(cemetery);
        print(cemetery);
        List<Grave> graves = new List<Grave>
        {
            ApiConnection.GetGrave(1, 1)
            // new Grave(new Guest(DateTime.Now, DateTime.Today, "Janek"), 0, 3),
            // new Grave(new Guest(DateTime.Now, DateTime.Today, "Dupa"), 3, 6),
            // new Grave(new Guest(DateTime.Now, DateTime.Today, "Chuj"), -3, 3)
        };


        var currentPosition = transform.localPosition;
        foreach (var grave in graves)
        {
            var gravePosition = new Vector3(4 * grave.gridX, 0, 4 * grave.gridY);
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
}