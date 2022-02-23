using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PenManager : SectionManager {
    public static PenManager selfInstance;
    public TextMeshProUGUI textBox;
    public GameObject options;
    public GameObject close;
    public GameObject conclusion;

    private PenObject penObject;

    private List<TextMeshProUGUI> textButtons = new List<TextMeshProUGUI>();
    private int currentWordIndex;
    private int currentTextIndex;

    private string[] playerAnswers;

    public override void Awake() {
        base.Awake();

        Button[] optionButtons = options.GetComponentsInChildren<Button>();
        foreach (Button button in optionButtons) {
            textButtons.Add(button.GetComponentInChildren<TextMeshProUGUI>());
        }

        #region Singleton
        base.Awake();
        if (selfInstance == null) {
            selfInstance = this;
        }
        else {
            Destroy(gameObject);
        }
        #endregion
    }

    public override void ExecuteSection(SectionObject sectionObject) {
        penObject = (PenObject)sectionObject;

        foreach (PenSolved penSolved in PlayerData.instance.penSolved) {
            if (penSolved.key == penObject.name) {
                ShowConclusion(penSolved.answer);
                return;
            }
        }

        playerAnswers = new string[penObject.answers.Length];
        currentWordIndex = 0;
        currentTextIndex = 0;

        textBox.text = penObject.text;
        options.SetActive(true);
        close.SetActive(true);
        conclusion.SetActive(false);

        ExecuteWord();
    }

    public void ExecuteWord(int move = 0) {
        textBox.ForceMeshUpdate(true);

        currentWordIndex += move;

        if (currentWordIndex >= penObject.words.Count || currentWordIndex < 0) {
            currentWordIndex -= move;
            return;
        }

		Word word = penObject.words[currentWordIndex];
        FocusWord(word);
    }

    private void FocusWord(Word word) {
        currentTextIndex = word.textIndex;
        string text = "<color=red>" + textBox.textInfo.wordInfo[currentTextIndex].GetWord() + "</color>";

        for (int i = 0; i < word.possibleValues.Length; i++) {
			textButtons[i].text = word.possibleValues[i];
        }

        ChangeWord(text);
    }

    private void ChangeWord(string word) {
        textBox.text = textBox.text.Replace("<color=red>","");
        textBox.text = textBox.text.Replace("</color>","");

        List<char> charList = new List<char>(textBox.text.ToCharArray());

        int wordStart = textBox.textInfo.wordInfo[currentTextIndex].firstCharacterIndex;
        int wordEnd = textBox.textInfo.wordInfo[currentTextIndex].lastCharacterIndex + 1;

        charList.RemoveRange(wordStart,wordEnd-wordStart);

        for (int i = 0; i < word.Length; i++) {
            charList.Insert(wordStart + i, word[i]);
        }

        textBox.text = new string(charList.ToArray());
    }

    public override void HandleChoice(string text) {
        playerAnswers[currentWordIndex] = text;

        if (currentWordIndex + 1 >= penObject.words.Count) {
            text = "<color=red>" + text + "</color>";
        }
        ChangeWord(text);

        for (int i = 0; i < playerAnswers.Length; i++) {
            if (playerAnswers[i] != penObject.answers[i]) {
                ExecuteWord(1);
                return;
            }
        }
        
        PuzzleCorrect();
    }

    private void PuzzleCorrect() {
        string answer = textBox.text.Replace("<color=red>","");
        answer = answer.Replace("</color>","");
        answer = "<color=green>" + answer + "</color>";

        PenSolved penSolved = new PenSolved(penObject.name,answer);
        PlayerData.instance.SolvePuzzle(penSolved);

        ShowConclusion(answer);
    }

    private void ShowConclusion(string answer) {
        textBox.text = answer;

        textBox.ForceMeshUpdate(true);
        conclusion.SetActive(true);
        close.SetActive(false);
        options.SetActive(false);
    }
}
