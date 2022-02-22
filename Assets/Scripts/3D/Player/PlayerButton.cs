using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerButton : MonoBehaviour {
    new public CinemachineVirtualCamera camera;

    private PlayerPoint targetPoint;

    public PlayerPoint GetTarget() {
        return targetPoint;
    }

    public void SetTarget(PlayerPoint point) {
        targetPoint = point;
    }

    public void RotatePlayer() {
        PlayerController.instance.Rotate(this);
        PlayerController.instance.UpdateUI(false, true);
    }

    public void MoveForward() {
        PlayerController.instance.MoveForward();
    }

    public void MoveBack() {
        PlayerController.instance.Rotate(this);
        PlayerController.instance.UpdateUI(true, false);
    }
}
