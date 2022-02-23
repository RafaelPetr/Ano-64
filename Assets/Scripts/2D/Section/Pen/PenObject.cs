using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Word {
    public int textIndex;
    public string[] possibleValues = new string[9];
}

[CreateAssetMenu(menuName = "2dObject/Section/PenObject")]
public class PenObject : SectionObject {
    [TextArea(3,10)]public string text;
    public List<Word> words = new List<Word>();
    public string[] answers;
    
    public override void Execute(Transform transform = null) {
        if (PlayerData.instance.FindItem("Pen")) {
            PenManager.selfInstance.EnterSection(this);
        }
    }
}

[System.Serializable]
public class PenSolved {
    public string key;
    public string answer;

    public PenSolved(string key, string answer) {
        this.key = key;
        this.answer = answer;
    }
}