using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.VFX;

public class SceneController : MonoBehaviour {
    public static SceneController instance;

    public Animator transition;
    public float transitionTime = 1;

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
        else {
            Destroy(gameObject);
        }
    }

    public void NextScene() {
        Load(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void PreviousScene() {
        Load(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void Load(int scene) {
        PlayerData.instance.page = scene;
        StartCoroutine(LoadLevel(scene));
    }

    IEnumerator LoadLevel(int scene) {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(scene);
    }
}
