using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ApproachManager : MonoBehaviour {
    public static ApproachManager instance;

    private List<Approachable> cachedApproachs = new List<Approachable>();

    public GameObject backButton;

    private CinemachineVirtualCamera cinemachine;
    private Transform startTransform;
    private bool moveCamera;

    private Vector3 lookAtPosition;
    private Vector3 targetPosition;

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
            cinemachine.transform.position = Vector3.MoveTowards(cinemachine.transform.position,targetPosition,Time.deltaTime*30f);
        
            if (Vector3.Distance(cinemachine.transform.position,targetPosition) < 0.001f) {
                moveCamera = false;
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

        ControlCamera(cachedApproachs[0].transform,true);
        backButton.SetActive(true);
    }

    public void Desapproach() {
        cachedApproachs[cachedApproachs.Count-1].Desapproach();
        cachedApproachs.RemoveAt(cachedApproachs.Count-1);

        if (cachedApproachs.Count > 0) {
            cachedApproachs[cachedApproachs.Count-1].Approach(false);
        }

        else {
            ControlCamera(PlayerController.instance.transform,false);
            backButton.SetActive(false);
        }
    }

    private void ControlCamera(Transform target, bool approach) {
        cinemachine = PlayerController.instance.GetCamera();

        cinemachine.LookAt = target;
        lookAtPosition = cinemachine.LookAt.transform.position;

        if (approach) {
            targetPosition = new Vector3(lookAtPosition.x-10,lookAtPosition.y+1,lookAtPosition.z);
        }
        else {
            targetPosition = new Vector3(lookAtPosition.x-1,lookAtPosition.y,lookAtPosition.z);
        }

        moveCamera = true;
    }
}
