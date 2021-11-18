using System;
using System.Collections.Generic;
using UnityEngine;

namespace Graves
{
    public class Graves : MonoBehaviour
    {
        public GameObject myGameObject;

        public List<Vector3> positions;

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
            foreach (var position in positions)
            {
                var finalPosition = position + currentPosition;
                GameObject go = Instantiate(myGameObject, finalPosition, Quaternion.identity, transform) as GameObject;
                go.name = myGameObject.name + '_' + position.x + "_" + position.z;
            }
           
        }
    }
}