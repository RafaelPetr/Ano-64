using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemDependent : Clickable {
    [SerializeField]private string keyItem;

    public override void Click() {
        base.Click();

        if (PlayerData.instance.FindItem(keyItem)) {
            OnClickFunction();
        }
    }

    public abstract void OnClickFunction();
}
