using System;
using System.Collections.Generic;
using UnityEngine;

public class GraveSpawner : MonoBehaviour
{
    public GameObject myGameObject;
    public List<Grave> graves; //TODO idk how to pass graves here :<

    private void Start()
    {
        print("adding graves");
        Apply();
    }

    private void Apply()
    {
        if (myGameObject == null)
        {
            print("Dupa zbita");
            return;
        }


        Renderer localRenderer = myGameObject.GetComponent<Renderer>();
        print(localRenderer);
        if (localRenderer != null)
        {
            foreach (Transform t in transform)
            {
                Destroy(t.gameObject);
            }
        }

        var currentPosition = transform.localPosition;
        foreach (var grave in graves)
        {
            var gravePosition = new Vector3(grave.x, 0, grave.y);
            var finalPosition = gravePosition + currentPosition;
            GameObject go = Instantiate(myGameObject, finalPosition, Quaternion.identity, transform) as GameObject;
            go.name = myGameObject.name + '_' + gravePosition.x + "_" + gravePosition.z;
            
        }
    }
}