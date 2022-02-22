using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotebookTriggerManager : _TriggerManager {
    //private bool onTitle = true;

    public override void Awake() {
        base.Awake();
    }

    public override void FixedUpdate() {
        base.FixedUpdate();
    }

    private void NextPage() {
        SceneController.instance.NextScene();
    }

    private void PreviousPage() {
        SceneController.instance.PreviousScene();
    }

    public override void ExecuteSection(SectionObject sectionObject) {

        TriggerObject triggerObject = (TriggerObject)sectionObject;

        switch (triggerObject.function) {

            case "NextPage":
                NextPage();
                break;

            case "PreviousPage":
                PreviousPage();
                break;

        }
    }
}