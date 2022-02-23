using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {
    public string key;
    public bool show;
    private SpriteRenderer spriteRenderer;

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start() {
        ShowItem();
    }

    private void ShowItem() {
        if (!show) {
            spriteRenderer.enabled = show;
        }

        if (PlayerData.instance.FindItem(key)) {
            if (show) {
                gameObject.SetActive(!show);
            }
            else {
                spriteRenderer.enabled = !show;
            }
        }
    }

    public void PickUp() {
        PlayerData.instance.AddItem(key);
        ShowItem();
    }
}
