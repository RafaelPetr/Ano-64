using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ApproachManager : MonoBehaviour {
    public static ApproachManager instance;

    [SerializeField]new private CinemachineVirtualCamera camera;

    private List<Approachable> cachedApproachs = new List<Approachable>();

    private bool moveCamera;
    private bool approach;
    private Vector3 targetPosition;

    public GameObject backButton;

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
        else {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate() {
        if (moveCamera) {
            if (approach) {
                camera.transform.position = Vector3.MoveTowards(camera.transform.position,targetPosition,Time.deltaTime*30f);
        
                if (Vector3.Distance(camera.transform.position,targetPosition) < 0.001f) {
                    moveCamera = false;
                }
            }
            else {
                camera.transform.localPosition = Vector3.MoveTowards(camera.transform.localPosition,targetPosition,Time.deltaTime*30f);
            
                if (Vector3.Distance(camera.transform.localPosition,targetPosition) < 0.001f) {
                    moveCamera = false;
                }
            }
            
        }
    }

    public void Approach(Approachable approachable, bool addToCache = true) {
        if (addToCache) {
            if (cachedApproachs.Count > 0) {
                cachedApproachs[0].AdjustParent(approachable);
                cachedApproachs[cachedApproachs.Count-1].LoseFocus();
            }

            cachedApproachs.Add(approachable);
        }

        else if (cachedApproachs.Count > 1) {
            cachedApproachs[0].AdjustParent(approachable);
        }

        ControlCamera(cachedApproachs[0]);
        backButton.SetActive(true);
    }

    public void Desapproach() {
        cachedApproachs[cachedApproachs.Count-1].Desapproach();
        cachedApproachs.RemoveAt(cachedApproachs.Count-1);

        if (cachedApproachs.Count > 0) {
            cachedApproachs[cachedApproachs.Count-1].Approach(false);
        }

        else {
            ControlCamera();
            backButton.SetActive(false);
        }
    }

    private void ControlCamera(Approachable target = null) {
        if (target != null) {
            approach = true;
            targetPosition = target.cameraPosition;
        }
        else {
            camera.LookAt = null;
            camera.transform.localEulerAngles = Vector3.zero;
            approach = false;
            targetPosition = Vector3.back;
        }

        moveCamera = true;
    }
}
