using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerButton : MonoBehaviour {
    public Vector3 rotationValue;

    public void Rotate() {
        PlayerController.instance.Rotate(rotationValue);
    }

    public void Move() {
        PlayerController.instance.Move();
    }
}
