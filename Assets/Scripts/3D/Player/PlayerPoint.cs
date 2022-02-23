using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPoint : MonoBehaviour {
    [System.NonSerialized]public Vector3 position;

    public PlayerPoint rightPoint;
    public PlayerPoint leftPoint;
    public PlayerPoint frontPoint;
    public PlayerPoint backPoint;

    public Clickable[] clickables;

    private void Awake() {
        position = transform.position;
    }

    public void SetCollisionClickables(bool value) {
        foreach (Clickable clickable in clickables) {
            clickable.collider.enabled = value;
        }
    }
}
