using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GraveSetup : MonoBehaviour
{
    public Grave grave;

    void Start()
    {
        var texts = gameObject.GetComponentsInChildren<TextMeshPro>();
        foreach (var text in texts)
        {
            if (text.name == "NameText")
            {
                text.SetText(grave.guest.name);
            }
            else if (text.name == "BirthText")
            {
                text.SetText(grave.guest.birthDate.ToLongDateString());
            }
            else if (text.name == "DeadText")
            {
                text.SetText(grave.guest.deadDate.ToLongDateString());
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}