using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraMan : MonoBehaviour {

    public CinemachineVirtualCamera right;
    public CinemachineVirtualCamera left;
    public CinemachineVirtualCamera front;
    public CinemachineVirtualCamera back;

    private void Start() {
        ResetCameras();
        front.gameObject.SetActive(true);
    }

    private void ResetCameras() {
        right.gameObject.SetActive(false);
        left.gameObject.SetActive(false);
        front.gameObject.SetActive(false);
        back.gameObject.SetActive(false);
    }

    public void ChangeCamera(CinemachineVirtualCamera camera) {

    }
}
