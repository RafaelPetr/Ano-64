using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkLightManager : MonoBehaviour {
    public GameObject darkLight;
    public GameObject darkLightedObjects;

    private void Update() {
        if (Input.GetButtonDown("DarkLight") && PlayerData.instance.FindItem("DarkLight")) {
            SetDarkLight(!darkLight.activeSelf);
        }
    }

    private void SetDarkLight(bool value) {
        darkLight.SetActive(value);
        darkLightedObjects.SetActive(value);
    }
}
