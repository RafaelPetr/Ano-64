using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class ClickManager : MonoBehaviour {
    public static ClickManager instance;
    private GameObject targetObject;

    [System.NonSerialized]public UnityEvent mouseEnter;
    [System.NonSerialized]public UnityEvent click;
    [System.NonSerialized]public UnityEvent mouseExit;
    [System.NonSerialized]public bool dragging;

    public virtual void Awake() {
        if (instance == null) {
            mouseEnter = new UnityEvent();
            click = new UnityEvent();
            mouseExit = new UnityEvent();
            instance = this;
        }
        else {
            Destroy(this);
        }
    }

    private void Update() {
        ShootRaycast();
    }

    private void ShootRaycast() {
        if (EventSystem.current.IsPointerOverGameObject()) {
            mouseExit.Invoke();
            return;
        }

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 100f) && hit.transform != null) {
            if (hit.transform.gameObject.CompareTag("Clickable")) {
                if (targetObject == null) {
                    targetObject = hit.transform.gameObject;
                    mouseEnter.Invoke();
                }
                if (Input.GetMouseButtonDown(0)) {
                    targetObject = null;
                    click.Invoke();
                }
                if (targetObject != hit.transform.gameObject && !dragging) {
                    targetObject = null;
                    mouseExit.Invoke();
                }
            }
        }
        else {
            targetObject = null;
            mouseExit.Invoke();
        }
    }

    public bool IsTargeting(GameObject obj) {
        return obj == targetObject;
    }

}
