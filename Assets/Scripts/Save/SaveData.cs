using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData {

    public string chapter;
    public string timestamp;
    public int difficulty;
    public int page;
    public string[] itemsFound;
    public PenSolved[] penSolved;

    public SaveData(PlayerData player) {
        chapter = player.chapter;
        timestamp = player.timestamp;
        difficulty = player.difficulty;
        page = player.page;
        itemsFound = player.itemsFound.ToArray();
        penSolved = player.penSolved.ToArray();
    }

}