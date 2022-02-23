using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerController : MonoBehaviour {
    public static PlayerController instance;

    [SerializeField]private GameObject frontButton;
    [SerializeField]private PlayerPoint currentPoint;

    private Quaternion targetRotation;
    private PlayerPoint targetPoint;

    private bool rotate;
    private bool move;
    private float moveSpeed = 5f;

    private void Awake() {
        instance = this;
    }

    private void Start() {
        targetPoint = currentPoint.frontPoint;
        frontButton.SetActive(targetPoint != null);

        transform.position = currentPoint.position;
        currentPoint.SetCollisionClickables(true);
    }

    private void FixedUpdate() {
        if (move) {
            transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, Time.deltaTime * moveSpeed);
            if (Vector3.Distance(transform.position, targetPoint.position) <= .05f) {
                StopMovement();
            }
        }
        if (rotate) {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * moveSpeed);
            if (Quaternion.Angle(transform.rotation, targetRotation) <= .05f) {
                StopRotation();
            }
        }
    }

    private void StopMovement() {
        move = false;

        currentPoint = targetPoint;
        transform.position = currentPoint.position;
        currentPoint.SetCollisionClickables(true);

        SetTargetPoint();
    }

    private void StopRotation() {
        rotate = false;
        transform.rotation = targetRotation;
    }

    public void Move() {
        if (!move && targetPoint != null) {
            currentPoint.SetCollisionClickables(false);
            move = true;
        }
    }

    public void Rotate(Vector3 rotation) {
        if (!move) {
            if (targetRotation != null) {
                transform.rotation = targetRotation;
            }
            
            targetRotation = Quaternion.Euler(rotation.x,rotation.y,rotation.z) * transform.rotation;

            SetTargetPoint();

            rotate = true;
        }
    }

    private void SetTargetPoint() {
        switch (targetRotation.eulerAngles.y/90) {
            case 0:
                targetPoint = currentPoint.frontPoint;
                break;
            case 1:
                targetPoint = currentPoint.rightPoint;
                break;
            case 2:
                targetPoint = currentPoint.backPoint;
                break;
            case 3:
                targetPoint = currentPoint.leftPoint;
                break;
        }

        frontButton.SetActive(targetPoint != null);
    }
}