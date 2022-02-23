using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SaveSlot : MonoBehaviour {
    public int slotNumber;

    public TextMeshProUGUI chapter;
    public TextMeshProUGUI timestamp;
    public TextMeshProUGUI iconText;
    public Image icon;

    private void OnEnable() {
        PlayerData data = SaveSystem.LoadPlayer(slotNumber);

        if (data != null) {
            chapter.text = data.chapter;
            timestamp.text = data.timestamp;
            iconText.text = data.chapter;
        }

        else {
            iconText.text = "Vazio";
            chapter.text = "Vazio";
        }
    }

    public void Save() {
        SaveSystem.SavePlayer(slotNumber, PlayerData.instance);
        SectionManager.instance.ExitSection();
    } 

    public void Load() {
        PlayerData data = SaveSystem.LoadPlayer(slotNumber);

        if (data != null) {
            PlayerData.instance.chapter = data.chapter;
            PlayerData.instance.difficulty = data.difficulty;
            PlayerData.instance.page = data.page;
            PlayerData.instance.itemsFound = new List<string>(data.itemsFound);
            PlayerData.instance.penSolved = new List<PenSolved>(data.penSolved);
            SceneController.instance.Load(data.page);
        }
        SectionManager.instance.ExitSection();
    }
}
