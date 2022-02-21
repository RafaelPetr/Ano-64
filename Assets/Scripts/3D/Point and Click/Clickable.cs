using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clickable : MonoBehaviour {

    private Outline outline;
    [System.NonSerialized]public bool targetable = true;
    [System.NonSerialized]public bool targeted;

    private void Awake() {
        outline = gameObject.AddComponent<Outline>();

        gameObject.tag = "Clickable";

        BoxCollider collider = gameObject.GetComponent<BoxCollider>();
        collider.isTrigger = true;

        Rigidbody rigidBody = gameObject.AddComponent<Rigidbody>();

        rigidBody.useGravity = false;
        rigidBody.isKinematic = true;
    }

    public virtual void Start() {
        outline.enabled = false;
        outline.OutlineColor = Color.red;
        outline.OutlineWidth = 10f;

        ClickManager.instance.mouseEnter.AddListener(MouseEnter);
        ClickManager.instance.click.AddListener(Click);
        ClickManager.instance.mouseExit.AddListener(MouseExit);
    }

    private void MouseEnter() {
        if (ClickManager.instance.IsTargeting(this.gameObject) && targetable) {
            Target(true);
        }
    }

    public virtual void Click() {
        Target(false);
    }
    
    private void MouseExit() {
        Target(false);
    }

    private void Target(bool target) {
        targeted = target;
        outline.enabled = target;
    }
}
