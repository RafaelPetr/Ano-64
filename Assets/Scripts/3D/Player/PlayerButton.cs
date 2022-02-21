using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerButton : MonoBehaviour {
    [SerializeField]new private CinemachineVirtualCamera camera;

    [System.NonSerialized]public PlayerPoint targetPoint;

    public void RotatePlayer() {
        camera.gameObject.SetActive(true);

        if (targetPoint != null) {
            PlayerController.instance.SetTarget(targetPoint);
            PlayerController.instance.ShowMovement();
        }
    }

    public void MovePlayer() {
        PlayerController.instance.MoveForward();
    }
    
}
