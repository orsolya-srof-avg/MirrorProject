using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [SerializeField]
    private Button _replayButton;
    [SerializeField]
    private Button _quitButton;

    private void Awake() {
        _replayButton.onClick.AddListener(Replay);
        _quitButton.onClick.AddListener(Quit);
    }

    private void Replay() {
        SceneManager.LoadScene(0);
    }

    private void Quit() {
        Application.Quit();
    }

    private void OnDestroy() {
        _replayButton.onClick.RemoveListener(Replay);
        _quitButton.onClick.RemoveListener(Quit);
    }
}
