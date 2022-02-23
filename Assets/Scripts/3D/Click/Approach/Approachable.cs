using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Approachable : Clickable {

    public Vector3 cameraOffset;

    private List<Approachable> children = new List<Approachable>();
    private List<_OnApproachProperty> onApproachProperties = new List<_OnApproachProperty>();

    private bool rotateParent;
    private Quaternion parentTargetRotation;

    public override void Start() {
        base.Start();

        children = new List<Approachable>(GetComponentsInChildren<Approachable>());
        children.RemoveAt(0);

        onApproachProperties = new List<_OnApproachProperty>(GetComponents<_OnApproachProperty>());
    }

    private void FixedUpdate() {
        if (rotateParent) {
            transform.rotation = Quaternion.Lerp(transform.rotation, parentTargetRotation, Time.deltaTime * 5f);

            if (Quaternion.Angle(transform.rotation, parentTargetRotation) < 0.001f) {
                rotateParent = false;
            }
        }
    }

    public override void Click() {
        if (targeted) {
            base.Click();
            Approach();
        }
    }

    public void Approach(bool addToCache = true) {
        foreach (_OnApproachProperty property in onApproachProperties) {
            property.Activate();
        }

        targetable = false;

        foreach (Approachable child in children) {
            child.gameObject.SetActive(true);
        }

        ApproachManager.instance.Approach(this,addToCache);
    }

    public void Desapproach() {
        foreach (Approachable child in children) {
            child.gameObject.SetActive(false);
        }

        foreach (_OnApproachProperty property in onApproachProperties) {
            property.Deactivate();
        }

        targetable = true;
    }

    public void AdjustParent(Approachable child) {
        Vector3 angles = child.transform.localEulerAngles;

        Quaternion childRotation = Quaternion.Euler(-angles.x, -angles.y, -angles.z);
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(1f,1f,0f) - transform.position);

        parentTargetRotation = lookRotation * childRotation;
        rotateParent = true;
    }

    public void LoseFocus() {
        foreach (_OnApproachProperty property in onApproachProperties) {
            property.LoseFocus();
        }
    }
}
