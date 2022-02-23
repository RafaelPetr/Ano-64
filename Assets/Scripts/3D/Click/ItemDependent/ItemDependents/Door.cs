using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : ItemDependent {
    public Animator animator;
    public PlayerPoint openedAreaPoint;

    public override void Start() {
        base.Start();
        openedAreaPoint.gameObject.SetActive(false);
    }

    public override void OnClickFunction() {
        animator.SetTrigger("Open");
        openedAreaPoint.gameObject.SetActive(true);

        PlayerController.instance.SetTargetPoint();
    }
}
