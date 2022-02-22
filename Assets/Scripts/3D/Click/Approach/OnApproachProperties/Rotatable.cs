using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotatable : _OnApproachProperty {
    private float rotationSpeed = 800f;
    private Rigidbody rigidBody;
    private bool dragging;
    private bool reset;

    private void Start() {
        rigidBody = GetComponent<Rigidbody>();
    }

    private void SetDragging(bool drag) {
        ClickManager.instance.dragging = drag;
        dragging = drag;
    }

    private void OnMouseDrag() {
        SetDragging(true);
    }

    private void OnMouseUp() {
        SetDragging(false);
    }

    public void ResetRotation() {
        SetDragging(false);

        rigidBody.isKinematic = true;
        rigidBody.angularVelocity = Vector3.zero;
        reset = true;
    }

    public override void Activate() {
        base.Activate();
        rigidBody.isKinematic = false;
        reset = false;
    }

    public override void FixedUpdate() {
        base.FixedUpdate();

        if (reset) {
            transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.identity, Time.deltaTime * 5f);
        }

        else {
            if (dragging) {
                float x = Input.GetAxis("Mouse X") * rotationSpeed * Time.fixedDeltaTime;
                float y = Input.GetAxis("Mouse Y") * rotationSpeed * Time.fixedDeltaTime;
                rigidBody.AddTorque(Vector3.down*x);
                rigidBody.AddTorque(Vector3.right*y);
            }

            else {
                rigidBody.AddTorque(-rigidBody.angularVelocity/2);
            }
        }
    }

    public override void LoseFocus() {
        rigidBody.isKinematic = true;
    }

    public override void Deactivate() {
        ResetRotation();
    }
}
