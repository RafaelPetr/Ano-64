using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using TMPro;

public class ZoomManager : SectionManager {
    public static ZoomManager selfInstance;

    public TextMeshProUGUI textBox;
    public Transform focus;
    public CinemachineVirtualCamera cam;

    private bool zooming;
    private int movingCounter;

    public override void Awake() {
        base.Awake();
        if (selfInstance == null) {
            selfInstance = this;
        }
        else {
            Destroy(gameObject);
        }
    }

    public override void FixedUpdate() {
        base.FixedUpdate();
        if (zooming && cam.m_Lens.OrthographicSize > 1f) {
            if (movingCounter > 0) {
                movingCounter--;
            }
            else {
                cam.m_Lens.OrthographicSize -= 0.05f;
            }
        }
        else if (!zooming && cam.m_Lens.OrthographicSize < 3f) {
            cam.m_Lens.OrthographicSize += 0.05f;
            if (cam.m_Lens.OrthographicSize >= 3f) {
                cam.m_Follow = focus;
            }
        }
    }

    public override void EnterSection(SectionObject sectionObject) {
        if (PlayerData.instance.FindItem("Magnifier")) {
            base.EnterSection(sectionObject);
        }
    }

    public override void ExecuteSection(SectionObject sectionObject) {
        ZoomObject zoomObject = (ZoomObject)sectionObject;
        movingCounter = 30;
        zooming = true;
        textBox.text = zoomObject.text;
        cam.m_Follow = zoomObject.transform;
    }

    public override void ExitSection() {
        base.ExitSection();
        zooming = false;
    }
}
