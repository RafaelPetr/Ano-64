using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class _OnApproachProperty : MonoBehaviour {
    [System.NonSerialized]public bool active;

    public virtual void Activate() {
        active = true;
        enabled = true;
    }

    public virtual void FixedUpdate() {
        if (!active) {
            return;
        }
    }

    public abstract void LoseFocus();

    public virtual void Deactivate() {
        active = false;    
        enabled = false;
    }
}
