using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : Clickable {
    [SerializeField]private string key;
    public override void Click() {
        if (targeted) {
            base.Click();

            PlayerData.instance.AddItem(key);
            gameObject.SetActive(false);
        }
    }
}
