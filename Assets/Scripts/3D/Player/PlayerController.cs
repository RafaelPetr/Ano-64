using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class PlayerController : MonoBehaviour {
    public static PlayerController instance;

    private CinemachineVirtualCamera currentCamera;

    [SerializeField]private PlayerPoint currentPoint;
    private PlayerPoint targetPoint;

    [SerializeField]private GameObject rotateButtons;

    [SerializeField]private PlayerButton rightDirection;
    [SerializeField]private PlayerButton leftDirection;
    [SerializeField]private PlayerButton frontDirection;
    [SerializeField]private PlayerButton backDirection;

    [SerializeField]private PlayerButton backMove;

    private bool move;
    private float moveSpeed = 5f;

    private void Awake() {
        instance = this;
    }

    private void Start() {
        transform.position = currentPoint.position;
        SetDirectionPoints(currentPoint);
        targetPoint = currentPoint.frontPoint;
    }

    private void FixedUpdate() {
        if (move && targetPoint != null) {
            transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, Time.deltaTime * moveSpeed);
            if (Vector3.Distance(transform.position, targetPoint.position) <= .05f) {
                StopMovement();
            }
        }
    }

    private void SetDirectionPoints(PlayerPoint point) {
        rightDirection.SetTarget(point.rightPoint);
        leftDirection.SetTarget(point.leftPoint);
        frontDirection.SetTarget(point.frontPoint);
        backDirection.SetTarget(point.backPoint);

        backMove.SetTarget(point.frontPoint);
    }

    private void StopMovement() {
        move = false;
        UpdateUI(true, false);

        currentPoint = targetPoint;
        SetDirectionPoints(currentPoint);

        Rotate(frontDirection);
    }

    public CinemachineVirtualCamera GetCamera() {
        return currentCamera;
    }

    public void Rotate(PlayerButton rotateButtonClicked) {
        if (currentCamera != null) {
            currentCamera.gameObject.SetActive(false);
        }
        currentCamera = rotateButtonClicked.camera;
        currentCamera.gameObject.SetActive(true);

        targetPoint = rotateButtonClicked.GetTarget();
    }

    public void MoveForward() {
        move = true;
    }

    public void UpdateUI(bool rotate, bool back) {
        rotateButtons.SetActive(rotate);
        backMove.gameObject.SetActive(back);
    }
}