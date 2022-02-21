using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
    public static PlayerController instance;

    [SerializeField]private PlayerPoint originPoint;

    [SerializeField]private GameObject movementButtons;
    [SerializeField]private GameObject rotateButtons;

    [SerializeField]private PlayerButton rightButton;
    [SerializeField]private PlayerButton leftButton;
    [SerializeField]private PlayerButton frontButton;
    [SerializeField]private PlayerButton backButton;

    private bool move;
    private float moveSpeed = 5f;

    private void Awake() {
        instance = this;
    }

    private void Start() {
        ChangeMovement(originPoint);
        SetTarget(originPoint.frontPoint);
    }

    private void FixedUpdate() {
        if (move) {
            transform.position = Vector3.MoveTowards(transform.position, originPoint.position, Time.deltaTime * moveSpeed);
            if (Vector3.Distance(transform.position, originPoint.position) <= .05f) {
                move = false;
                ShowRotate();
            }
        }
    }

    public void SetTarget(PlayerPoint point) {
        originPoint = point;
    }

    private void ChangeMovement(PlayerPoint point) {
        rightButton.targetPoint = point.rightPoint;
        leftButton.targetPoint = point.leftPoint;
        frontButton.targetPoint = point.frontPoint;
        backButton.targetPoint = point.backPoint;
    }

    public void MoveForward() {
        ChangeMovement(originPoint);
        move = true;
    }

    public void ShowMovement() {
        rotateButtons.SetActive(false);
        movementButtons.SetActive(true);
    }

    private void ShowRotate() {
        rotateButtons.SetActive(true);
        movementButtons.SetActive(false);
    }

}